using Dapper;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>, IDisposable where MISAEntity : BaseEntity
    {
        #region Declare
        protected string _tableName = string.Empty;
        protected string _connectionString = "Host=47.241.69.179;" +
            "Port=3306;" +
            "User=dev;" +
            "Password=12345678;" +
            "Database=MF796_NTQUAN_AMIS;";
        protected IDbConnection _dbConnection;

        #endregion

        #region Constructor
        public BaseRepository()
        {
            _tableName = typeof(MISAEntity).Name;
            _dbConnection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Property

        public IEnumerable<MISAEntity> GetEntities()
        {
            //get data
            var entities = _dbConnection.Query<MISAEntity>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            return entities;
        }

        public MISAEntity GetById(Guid entityId)
        {

            var storeName = $"Proc_Get{_tableName}ById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            var storeGetByIdInputParamName = $"@{_tableName}Id";
            dynamicParameters.Add(storeGetByIdInputParamName, entityId.ToString());

            var entity = _dbConnection.Query<MISAEntity>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }


        public int Insert(MISAEntity entity)
        {
            var storeName = $"Proc_Insert{_tableName}";
            var storeParam = MappingDbType(entity);
            var rowAffects = _dbConnection.Execute(storeName, param: storeParam, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        public int Update(MISAEntity entity, Guid entityId)
        {
            var storeName = $"Proc_Update{_tableName}";
            var storeParam = MappingDbType(entity);
            var rowAffects = _dbConnection.Execute(storeName, storeParam, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }
        public int Delete(Guid entityId)
        {
            var result = _dbConnection.Execute($"DELETE FROM {_tableName} WHERE {_tableName}Id = '{entityId}'", commandType: CommandType.Text);
            return result;
        }

        public MISAEntity GetEntityByProperty(MISAEntity entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            var query = string.Empty;
            if (entity.EntityState == EntityState.AddNew)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            else if (entity.EntityState == EntityState.Update)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            else
                return null;
            var entityReturn = _dbConnection.Query<MISAEntity>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }

        private DynamicParameters MappingDbType(MISAEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var dbValue = ((bool)propertyValue == true ? 1 : 0);
                    parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }

            }
            return parameters;
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
        #endregion

    }
}
