using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace VehicleAPI.DBHelpers
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _Configuration;

        public SqlDataAccess(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }

        /// <summary>
        /// return connection string from webconfig.
        /// </summary>
        /// <param name="connectionName">target database name</param>
        /// <returns></returns>
        public string GetConnectionString(string connectionName = "VehicleTrackingDBase")
        {
            return _Configuration.GetConnectionString(connectionName);
        }

        /// <summary>
        /// return matching items in the database.
        /// </summary>
        /// <typeparam name="T">type of data model</typeparam>
        /// <param name="sqlQuery">sql query</param>
        public List<T> LoadData<T>(string sqlQuery)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(sqlQuery).ToList();
            }
        }

        /// <summary>
        /// return matching items in the database.
        /// </summary>
        /// <typeparam name="T">type of data model</typeparam>
        /// <param name="sqlQuery">sql query</param>
        public async Task<IEnumerable<T>> LoadDataAsync<T>(string sqlQuery)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return await connection.QueryAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// return matching items in the database.
        /// </summary>
        /// <typeparam name="T">data model</typeparam>
        /// <typeparam name="U">parameter model</typeparam>
        /// <param name="storedProcesdure">stored Procesdure</param>
        /// <param name="parameters">parameters model</param>
        /// <returns></returns>
        public List<T> LoadData<T, U>(string storedProcesdure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<T>> LoadDataAsync<T, U>(string storedProcesdure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return (List<T>)await connection.QueryAsync<T>(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// return matching single items in the database.
        /// </summary>
        /// <typeparam name="T">data model</typeparam>
        /// <typeparam name="U">parameter model</typeparam>
        /// <param name="storedProcesdure">stored Procesdure</param>
        /// <param name="parameters">parameters model</par
        /// <returns></returns>
        public T LoadSingleData<T, U>(string storedProcesdure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.QueryFirstOrDefault<T>(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// return sinlgle item in the database.
        /// </summary>
        /// <typeparam name="T">type of data model</typeparam>
        /// <param name="sqlQuery">sql query</param>
        /// <returns></returns>
        public T SingleOrDefault<T>(string sqlQuery)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.QuerySingleOrDefault<T>(sqlQuery);
            }
        }

        /// <summary>
        /// execute single stored procedure
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SingleOrDefault<T>(string storedProcesdure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.QueryFirstOrDefault<T>(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcesdure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int StoredProcesdure<T>(string storedProcesdure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// return sinlgle item in the database.
        /// </summary>
        /// <typeparam name="T">type of data model</typeparam>
        /// <param name="sqlQuery">sql query</param>
        /// <returns></returns>
        public void SingleOrDefault(string sqlQuery)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.QuerySingleOrDefault(sqlQuery);
            }
        }

        /// <summary>
        /// return applied items count in the database.
        /// </summary>
        /// <typeparam name="T">type of data model</typeparam>
        /// <param name="sqlQuery">query</param>
        /// <param name="data">type of data model</param>
        /// <returns></returns>
        public int SaveData<T>(string sqlQuery, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(sqlQuery, data);
            }
        }

        /// <summary>
        /// return applied items count in the database.
        /// </summary>
        /// <typeparam name="T">type of data model</typeparam>
        /// <param name="sqlQuery">query</param>
        /// <param name="data">type of data model</param>
        /// <returns></returns>
        public async Task<int> SaveDataAsync<T>(string sqlQuery, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return await connection.ExecuteAsync(sqlQuery, data);
            }
        }

        /// <summary>
        /// return applied items count in the database.
        /// </summary>
        /// <typeparam name="T">data model</typeparam>
        /// <typeparam name="U">parameter model</typeparam>
        /// <param name="storedProcesdure">stored Procedure</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public int SaveData<T, U>(string storedProcesdure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// return applied items count in the database.
        /// </summary>
        /// <typeparam name="T">data model</typeparam>
        /// <typeparam name="U">parameter model</typeparam>
        /// <param name="storedProcesdure">stored Procedure</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public async Task<int> SaveDataAsync<T, U>(string storedProcesdure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return await connection.ExecuteAsync(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}