using System;
using System.Data.Common;

namespace Jwell.Framework.Context
{
    internal class ConnectionManager
    {
        private DbConnection dbConnection;

        private string connString = string.Empty;

        internal DbConnection GetConnection(string dbName, bool isReadDb = true)
        {
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentException($"数据库名称{dbName}不能为空");
            }
            else
            {
                connString = ConnectionString(dbName, isReadDb);
            }

            try
            {
                dbConnection = new MySql.Data.MySqlClient.MySqlConnection(connString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbConnection;
        }

        private string ConnectionString(string dbName, bool isReadDb)
        {
            //TODO：从配置中心获取数据库连接
            //TODO：读写不同
            string key = string.Empty;//TODO：key的命名规范
            if (isReadDb) key = $"ConnectionStrings:{dbName}";
            else key = $"ConnectionStrings:{dbName}Write";

            try
            {
                string isConfigCenter = ConfigurationManager.JwellConfiguration.GetAppSettingConfig($"{key}:IsConfigCenter");

                if (!string.IsNullOrWhiteSpace(isConfigCenter)
                    && bool.FalseString.ToLower() == isConfigCenter.Trim().ToLower())
                {
                    return ConfigurationManager.JwellConfiguration.GetAppSettingConfig($"{key}:ConnString");
                }
                else
                {
                    return ConfigurationManager.JwellConfiguration.GetConfig(dbName);
                }
            }
            catch {
                return ConfigurationManager.JwellConfiguration.GetConfig(dbName);
            }
        }
    }
}
