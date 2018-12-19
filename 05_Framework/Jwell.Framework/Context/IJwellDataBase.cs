using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jwell.Framework.Context
{
    public interface IJwellDataBase<T> where T : class
    {
        /// <summary>
        /// 根据sql查询指定实体
        /// </summary>
        /// <param name="name">对应config的name</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> Query(string name, object parameters = null);

        /// <summary>
        /// 根据sql查询自定义结果集对应实体
        /// </summary>
        /// <typeparam name="U">自定义结果集对应实体</typeparam>
        /// <param name="name">对应config的name</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<U> Query<U>(string name, object parameters = null);

        /// <summary>
        /// 根据sql查询指定实体 [分页查询]
        /// </summary>
        /// <param name="name">对应config的name</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">记录总数</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> QueryPaged(string name, int pageIndex, int pageSize, out int count, object parameters = null);


        /// <summary>
        /// 添加实体进数据库
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isTransaction">是否事务</param>
        /// <returns>返回自增列值</returns>
        long Add(T entity, bool isTransaction);

        /// <summary>
        /// 批量添加实体
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entities">实体类集合</param>
        /// <param name="isTransaction">是否事务</param>
        /// <returns>影响行数</returns>
        long AddList(IEnumerable<T> entities, bool isTransaction);

        /// <summary>
        /// 更新实体
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isTransaction">是否事务</param>
        /// <returns></returns>
        bool Update(T entity, bool isTransaction);

        /// <summary>
        /// 获取第一行第一列数据
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        U ExecuteScalar<U>(string name, object parameters = null);

        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="name">sql的配置名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteWriteSql(string name, object parameters = null);

        /// <summary>
        /// 查询第一个满足条件的
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        T QueryFirst(string name, object parameters = null);
        /// <summary>
        /// 查询第一个满足条件的
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        T QueryFirstOrDefault(string name, object parameters = null);
        /// <summary>
        /// 根据id获取对象
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// 根据Guid获取对象
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        T GetById(string id);

        /// <summary>
        /// 根据ID删除
        /// 若要使用此方法，需为DbName赋值，明确哪个数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Delete(T entity);
    }
}
