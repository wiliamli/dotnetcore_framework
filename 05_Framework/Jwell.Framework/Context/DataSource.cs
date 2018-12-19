using Jwell.Framework.XmlDoc.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Jwell.Framework.Context
{
    public class DataSource
    {
        private ConnectionManager connectionManager;

        private static DataSource instance = null;

        private static readonly object obj = new object();

        private static DataSource Instance
        { 
            get
            {
                DataSource dataSource = instance;
                if (dataSource == null)
                {
                    lock (obj)
                    {
                        if (dataSource == null)
                        {
                            dataSource = new DataSource();
                        }
                        return dataSource;
                    }
                }
                return dataSource;
            }
        }

        //默认构造函数
        private DataSource()
        {
            connectionManager = new ConnectionManager();
        }


        public static DbConnection GetConnection(string dbName, bool isReadDB = true)
        {
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException($"未设置参数{dbName}的值");
            }
            return Instance.connectionManager.GetConnection(dbName, isReadDB);
        }

        public static string CommandText(string name)
        {
            var dataCommand = XmlDoc.XmlHelper.DataRoot.DataCommand.FirstOrDefault(m => m.Name == name);
            if (dataCommand == null)
            {
                throw new ArgumentNullException($"未找到【{name}】下对应的DataCommand.");
            }
            return dataCommand.CommandText.Trim();
        }

        public static string DbName(string name)
        {
            var dataCommand = XmlDoc.XmlHelper.DataRoot.DataCommand.FirstOrDefault(m => m.Name == name);
            if (dataCommand == null)
            {
                throw new ArgumentNullException($"未找到【{name}】下对应的DataCommand.");
            }
            return dataCommand.DbName;
        }
    }
}
