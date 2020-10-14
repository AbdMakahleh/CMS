using Dapper;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class QueryExecuter
    {
        private static QueryExecuter _instance;

        private QueryExecuter()
        {
        }

        public static string ConnectionString { get; set; }

        public static QueryExecuter Instance
        {
            get
            {
                if (_instance == default(QueryExecuter))
                {
                    _instance = new QueryExecuter();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Executes stored Procedure as asynchronous fucntion.
        /// </summary>
        /// <typeparam name="T">
        /// The type of class the SP result should be parse to .
        /// </typeparam>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="query">
        /// the SP
        /// </param>
        /// <param name="parameters">
        /// The parameters of the SP
        /// </param>
        /// <returns>
        /// Return the result as task
        /// </returns>
        /// <seealso cref="StoredProcedureParameter" />
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(IStoredProcedure storedProcedure)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
            try
            {
                IEnumerable<T> result = null;
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        result= await SqlMapper.QueryAsync<T>(connection, storedProcedure.Query, storedProcedure.Parameters);
                        connection.Close();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }

        /// <summary>
        /// Executes stored Procedure as synchronous fucntion.
        /// </summary>
        /// <typeparam name="T">
        /// The type of class the SP result should be parse to .
        /// </typeparam>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="query">
        /// the SP
        /// </param>
        /// <param name="parameters">
        /// The parameters of the SP
        /// </param>
        /// <returns>
        /// Return the result as IEnumerable of the type specified
        /// </returns>
        /// <seealso cref="StoredProcedureParameter" />
        public IEnumerable<T> ExecuteStoredProcedure<T>(IStoredProcedure storedProcedure)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
            try
            {
                IEnumerable<T> result = null;
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        result = SqlMapper.Query<T>(connection, storedProcedure.Query, storedProcedure.Parameters);
                        connection.Close();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }

        /// <summary>
        /// Executes stored Procedure as synchronous fucntion.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="query">
        /// the SP
        /// </param>
        /// <param name="parameters">
        /// The parameters of the SP
        /// </param>
        /// <returns>
        /// Return the result as IEnumerable of object
        /// </returns>
        public List<JObject> ExecuteStoredProcedure(IStoredProcedure storedProcedure)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
            List<JObject> result = new List<JObject>();
            try
            {
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        using (var reader = connection.ExecuteReader(storedProcedure.Query, storedProcedure.Parameters))
                        {
                            while (reader.Read())
                            {
                                var columnsCount = storedProcedure.ResultColumns.Length;
                                JObject sampleObject = new JObject();
                                for (int i = 0; i < columnsCount; i++)
                                {
                                    sampleObject.Add(storedProcedure.ResultColumns[i].PropertyVisualization, JToken.FromObject(reader[storedProcedure.ResultColumns[i].PropertyName]));
                                }
                                result.Add(sampleObject);
                            }
                            connection.Close();
                        }
                 
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }

        public void ExecuteNonQueryStoredProcedure(IStoredProcedure storedProcedure)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
            try
            {
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        SqlMapper.Query(connection, storedProcedure.Query, storedProcedure.Parameters);
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }
        public T ExecuteNonQueryStoredProcedure<T>(IStoredProcedure storedProcedure)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
            object value = null;
            try
            {
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        value= SqlMapper.Query<T>(connection, storedProcedure.Query, storedProcedure.Parameters);
                        connection.Close();
                    }
                }
                return (T)value;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }
        public void ExecuteNonQuery(string query)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
     
            try
            {
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        SqlMapper.Query(connection, query);
                        connection.Close();
                    }
                }
        
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }

        public T ExecuteNonQuery<T>(string query)
        {
            NpgsqlConnection connection = default(NpgsqlConnection);
            object value = null;
            try
            {
                using (connection = new NpgsqlConnection(ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        value = SqlMapper.Query<T>(connection, query);
                        connection.Close();
                    }
                }
                return (T)value;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }

    }
}
