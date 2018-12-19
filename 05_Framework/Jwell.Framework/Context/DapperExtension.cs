using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jwell.Framework.Context
{
    public static class DapperExtension
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> typeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();

        /// <summary>
        /// 扩展新增操作，并返回自增列值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="conn">连接对象</param>
        /// <param name="entity">实体对象</param>
        /// <param name="identityId"></param>
        /// <param name="transaction">是否事务</param>
        /// <returns></returns>
        public static long Insert<T>(this IDbConnection conn, T entity, out long identityId, bool isTransaction=true) where T : class
        {
            identityId = 0;
            var type = typeof(T);
            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            var allProperties = TypePropertiesCache(type);
            var tableMapper = type.GetCustomAttribute<TableAttribute>();
            var name = SqlMapperExtensions.TableNameMapper != null ? SqlMapperExtensions.TableNameMapper(type) : (tableMapper == null ? type.Name : tableMapper.Name);
            var sqlStr = new StringBuilder();
            var valColumns = new StringBuilder();
            sqlStr.AppendFormat("INSERT INTO {0} (", name);
            for (var i = 0; i < allProperties.Count; i++)
            {
                sqlStr.AppendFormat("`{0}`,", allProperties[i].Name);
                valColumns.AppendFormat("@{0},", allProperties[i].Name);
            }
            sqlStr.Length = sqlStr.Length - 1;
            valColumns.Length = valColumns.Length - 1;
            sqlStr.AppendFormat(") VALUES ({0});", valColumns.ToString());

            sqlStr.Append("SELECT ROW_COUNT() AS ROW,LAST_INSERT_ID() AS IDENTITY");

            IDbTransaction trans = null;

            if (isTransaction)
            {
                trans = conn.BeginTransaction();
                conn = trans.Connection;
            }

            using (IDataReader reader = conn.ExecuteReader(sqlStr.ToString(), entity, transaction: trans))
            {
                if (reader.Read())
                {
                    //最新id
                    identityId = reader.GetInt64(1);
                    //影响行数
                    return reader.GetInt64(0);
                }
                else
                {
                    return 0;
                }
            }
        }

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pis;
            if (typeProperties.TryGetValue(type.TypeHandle, out pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().ToArray();
            typeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }
    }
}
