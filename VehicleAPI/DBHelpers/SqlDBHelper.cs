using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
        /// <typeparam name="T">data model</typeparam>
        /// <typeparam name="U">parameter model</typeparam>
        /// <param name="storedProcesdure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> LoadData<T, U>(string storedProcesdure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(storedProcesdure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();
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

    }
}