using Application.IDAL;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace Application.DAL
{
    public class DALModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("DataBase.json", false, true)
                .Build();

            var sqliteOptions = builder.GetSection("sqlitesqls").Get<SqliteDbContextOption[]>();

            if (sqliteOptions != null) 
            {
                foreach ( var sqliteOption in sqliteOptions ) 
                {
                    if (sqliteOption.IsEnabled)
                    {
                        unityContainer.RegisterInstance<ISqlSugarClient>(sqliteOption.ConnectionName.Trim(),
                            new SqlSugarClient(new ConnectionConfig() 
                            { 
                                DbType = DbType.Sqlite, 
                                ConnectionString = sqliteOption.ConnectionString, 
                                IsAutoCloseConnection = true, 
                                InitKeyType = InitKeyType.Attribute 
                            }));
                    }
                }
            }

            unityContainer.RegisterType<IModbusDeviceDAL, ModbusDeviceDAL>();
            unityContainer.RegisterType<IModbusRegisterDAL, ModbusRegisterDAL>();
            unityContainer.RegisterType<IS7netDeviceDAL, S7netDeviceDAL>();
            unityContainer.RegisterType<IS7netRegisterDAL, S7netRegisterDAL>();
        }
    }
}
