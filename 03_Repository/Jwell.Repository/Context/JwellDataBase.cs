using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Jwell.Framework.Context;

namespace Jwell.Repository.Context
{
    public abstract class JwellDataBase<T> : IJwellDataBase<T> where T : class
    {
        /// <summary>
        /// 可获取配置文件，可自定义
        /// </summary>
        public abstract string DbName { get; }

        /// <summary>
        /// 获取TableName
        /// </summary>
        public string TableName
        {
            get
            {
                string tableName = string.Empty;

                var customAttribute = typeof(T).CustomAttributes.FirstOrDefault(m => m.AttributeType ==
                typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute));

                if (customAttribute != null)
                {
                    var constructorArgument = customAttribute.ConstructorArguments.
                        FirstOrDefault(m => m.Value != null);

                    if (constructorArgument != null) tableName = constructorArgument.Value.ToString();
                    else throw new Exception("实体的System.ComponentModel.DataAnnotations.Schema.TableAttribute未定义.");
                }
                return tableName;
            }
        }

        /// <summary>
        /// 根据sql查询指定实体
        /// </summary>
        /// <param name="name">对应config的name</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query(string name, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection(true, name))
                {
                    string sql = DataSource.CommandText(name);
                    return conn.Query<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据sql查询自定义结果集对应实体
        /// </summary>
        /// <typeparam name="U">自定义结果集对应实体</typeparam>
        /// <param name="name">对应config的name</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual IEnumerable<U> Query<U>(string name, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection(true, name))
                {
                    string sql = DataSource.CommandText(name);
                    return conn.Query<U>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据sql查询指定实体 [分页查询]
        /// </summary>
        /// <param name="name">对应config的name</param>
        /// <param name="pageIndex">页索引,从1开始</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">记录总数</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual IEnumerable<T> QueryPaged(string name, int pageIndex, int pageSize, out int count, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            pageIndex = Math.Max(pageIndex, 0);
            if (pageSize <= 0) throw new ArgumentNullException("pageSize必须大于0");
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection(true, name))
                {
                    string sql = DataSource.CommandText(name).Trim(';');

                    count = conn.ExecuteScalar<int>($"SELECT COUNT(1) FROM ({sql}) pagealias", parameters);

                    return conn.Query<T>($"{sql} LIMIT {(pageIndex - 1) * pageSize},{pageSize}", parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加实体进数据库
        /// 若要使用此方法，需为DbName<see cref="DbName"/>赋值，明确哪个数据库
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isTransaction">是否事务</param>
        /// <returns>返回自增列值</returns>
        public virtual long Add(T entity, bool isTransaction)
        {
            long result = 0;
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            IDbConnection conn = null;
            IDbTransaction trans = null;
            try
            {
                if (!isTransaction)
                {
                    using (conn = GetConnection(false))
                    {
                        result = conn.Insert<T>(entity);
                    }
                }
                else
                {
                    using (conn = GetConnection(false))
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();

                        using (trans = conn.BeginTransaction())
                        {
                            result = conn.Insert(entity, trans);
                            trans.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 批量添加实体
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entities">实体类集合</param>
        /// <param name="isTransaction">是否事务</param>
        /// <returns>影响行数</returns>
        public virtual long AddList(IEnumerable<T> entities, bool isTransaction)
        {
            long result = 0;
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            IDbConnection conn = null;
            IDbTransaction trans = null;
            try
            {
                if (!isTransaction)
                {
                    using (conn = GetConnection(false))
                    {
                        result = conn.Insert<IEnumerable<T>>(entities);
                    }
                }
                else
                {
                    using (conn = GetConnection(false))
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();
                        using (trans = conn.BeginTransaction())
                        {
                            result = conn.Insert<IEnumerable<T>>(entities, trans);
                            trans.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 更新实体
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isTransaction">是否事务</param>
        /// <returns>影响行数</returns>
        public virtual bool Update(T entity, bool isTransaction)
        {
            bool result = false;
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            IDbConnection conn = null;
            IDbTransaction trans = null;
            try
            {
                if (!isTransaction)
                {
                    using (conn = GetConnection(false))
                    {
                        result = conn.Update(entity);
                    }
                }
                else
                {
                    using (conn = GetConnection(false))
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();
                        using (trans = conn.BeginTransaction())
                        {
                            result = conn.Update(entity, trans);
                            trans.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 获取第一行第一列数据
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="name">sql的配置名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual U ExecuteScalar<U>(string name, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    string sql = DataSource.CommandText(name);
                    return conn.ExecuteScalar<U>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行Sql,此方法会默认写库
        /// </summary>
        /// <param name="name">sql的配置名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual int ExecuteWriteSql(string name, object parameters = null)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            IDbTransaction trans = null;
            try
            {
                string sql = DataSource.CommandText(name);
                using (conn = GetConnection(false, name))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (trans = conn.BeginTransaction())
                    {
                        result = conn.Execute(sql, parameters, trans);
                        trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 查询第一个满足条件的
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="name">sql的配置名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual T QueryFirst(string name, object parameters = null)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection(true, name))
                {
                    string sql = DataSource.CommandText(name);
                    return conn.QueryFirst<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询第一个满足条件的
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="name">sql的配置名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual T QueryFirstOrDefault(string name, object parameters = null)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection(true, name))
                {
                    string sql = DataSource.CommandText(name);
                    return conn.QueryFirstOrDefault<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据id获取对象
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public virtual T GetById(long id)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    return conn.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据Guid获取对象
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public virtual T GetById(string id)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    return conn.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据ID删除
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            try
            {
                using (var conn = GetConnection(false))
                {
                    return conn.Delete(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <param name="isReadDb">是否是读库，false为写库</param>
        /// <param name="name">配置文件Sql的name</param>
        /// <returns></returns>
        protected IDbConnection GetConnection(bool isReadDb = true, string name = "")
        {
            string dbName = string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                dbName = DataSource.DbName(name);
            }
            else
            {
                dbName = DbName;
            }

            return DataSource.GetConnection(dbName, isReadDb);
        }
    }
}
