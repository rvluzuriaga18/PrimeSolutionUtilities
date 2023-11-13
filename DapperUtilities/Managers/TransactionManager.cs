using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using DapperUtilities.Helpers;

namespace DapperUtilities.Managers
{
    /// <summary>
    /// A class for CRUD and T-SQL operations.
    /// </summary>
    public class TransactionManager
    { 
        private readonly string connectionString = SecurityCredentials.GetConnectionString();
        private DynamicParameters parameters;

        /// <summary>
        /// Constructor, initialize the parameters.
        /// </summary>
        public TransactionManager()
        {
            parameters = new DynamicParameters();
        }

        /// <summary>
        /// It returns list of entity
        /// </summary>
        public OperationResult<List<T>> GetList<T>(string sql)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var query = conn.Query<T>(sql);
                    var result = query.AsList();

                    return OperationResult<List<T>>.Success(result);
                }
            }
            catch (Exception e)
            {
                return OperationResult<List<T>>.Failed(e.ToString());
            }
            finally
            {
                parameters = new DynamicParameters();
            }
        }

        /// <summary>
        /// It returns single entity; otherwise, null.
        /// </summary>
        public OperationResult<T> GetSingle<T>(string sql)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var query = conn.Query<T>(sql);
                    var result = query.AsList().FirstOrDefault();

                    return OperationResult<T>.Success(result);
                }
            }
            catch (Exception e)
            {
                return OperationResult<T>.Failed(e.ToString());
            }
            finally
            {
                parameters = new DynamicParameters();
            }
        }

        /// <summary>
        /// Inserts or updates record(s) using a stored procedure.
        /// </summary>
        public OperationResult<int> InsertOrUpdate(string storedProcedureName)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = conn.Execute(storedProcedureName, parameters,
                                             commandType: CommandType.StoredProcedure);

                    return OperationResult<int>.Success();
                }
            }
            catch (Exception e)
            {
                return OperationResult<int>.Failed(e.ToString());
            }
            finally
            {
                parameters = new DynamicParameters();
            }
        }

        /// <summary>
        /// Inserts or updates record using a stored procedure. It returns output value after modifying a record.
        /// </summary>
        public OperationResult<object> InsertOrUpdateWithOutput(string storedProcedureName, string outputParameterName)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    parameters.Add(outputParameterName, null, null, ParameterDirection.Output, 8000);
                    var result = conn.ExecuteScalar(storedProcedureName, parameters, 
                                                    commandType: CommandType.StoredProcedure);

                    return OperationResult<object>.Success(result);
                }
            }
            catch (Exception e)
            {
                return OperationResult<object>.Failed(e.ToString());
            }
            finally
            {
                parameters = new DynamicParameters();
            }
        }

        /// <summary>
        /// Parameter name for the stored procedure. The parameter direction is IN by default.
        /// </summary>
        public void AddParameter(string parameterName, dynamic value)
        {
            parameters.Add(parameterName, value);
        }
    }
}
