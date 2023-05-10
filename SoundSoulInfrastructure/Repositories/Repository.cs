using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace SoundSoulInfrastructure.Controllers
{
    public abstract class Repository
    {
        private const string Tag = "Repository";
        private readonly NpgsqlConnection Connection;

        protected Repository()
        {
            Connection = DataContext.GetConnectionPostgreSql();
        }

        /**
         * Abrir una conexion a la base de datos
         */
        protected async Task OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                await Connection.OpenAsync();
            }
        }

        /**
         * Permite preparar la sentencia SQL y los parametros que requerira para realizar la consulta a la base de datos
         * <param name="parameters">Diccionario de Parametros</param>
         * <param name="isStoreProcedure">El actual query es una funcion o procedimiento almacenamiento</param>
         * <param name="query">SQL Statement</param>
         */
        private NpgsqlCommand ApplyParameters(bool isStoreProcedure, string query, Dictionary<string, object>? parameters)
        {
            var command = new NpgsqlCommand(query, Connection);

            if (isStoreProcedure)
            {
                command.CommandType = CommandType.StoredProcedure;
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new NpgsqlParameter(parameter.Key, parameter.Value));
                }
            }

            return command;
        }

        /**
         * Permite deserealizar un procedimiento en una lista de clases tipo T
         * <param name="parameters">Diccionario de Parametros</param>
         * <param name="query">SQL Statement</param>
         */
        protected async Task<List<T>?> GetListDeserialize<T>(Dictionary<string, object>? parameters, string query)
        {
            try
            {
                string? json = null;
                var reader = ApplyParameters(true, query, parameters).ExecuteReader();

                await using (reader)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            json = reader.GetValue(0).ToString();
                        }
                    }
                }

                CloseConnection();
                return json != null ? JsonConvert.DeserializeObject<List<T>>(json) : new List<T>();
            }
            catch (Exception exception)
            {
                throw new Exception($"{Tag} : {exception}");
            }
        }

        /**
         * Permite deserealizar un procedimiento en una clase tipo T
         * <param name="parameters">Diccionario de Parametros</param>
         * <param name="query">SQL Statement</param>
         */
        protected async Task<T?> GetDeserialize<T>(Dictionary<string, object>? parameters, string query)
        {
            try
            {
                string? json = null;
                var reader = ApplyParameters(true, query, parameters).ExecuteReader();

                await using (reader)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            json = reader.GetValue(0).ToString();
                        }
                    }
                }

                CloseConnection();
                return json != null ? JsonConvert.DeserializeObject<T>(json) : throw new Exception($"{Tag} No se puedo crear el objecto");
            }
            catch (Exception exception)
            {
                throw new Exception($"{Tag} : {exception}");
            }
        }


        /**
         * Permite obtener los valores de una sentencia query en valores primitivos
         * <param name="parameters">Diccionario de Parametros</param>
         * <param name="query">SQL Statement</param>
         * <param name="isStoreProcedure">Permite agregar la validacion si es un store procedure o funcion</param>
         */
        protected async Task<List<T>> GetResultQuery<T>(Dictionary<string, object>? parameters, string query, bool isStoreProcedure = false)
        {
            try
            {
                var response = new List<T>();
                var reader = ApplyParameters(isStoreProcedure, query, parameters).ExecuteReader();

                await using (reader)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var value = reader.GetValue(0);

                            if (value != DBNull.Value)
                            {
                                T tValue = (T)Convert.ChangeType(value, typeof(T));
                                response.Add(tValue);
                            }
                        }
                    }
                }

                CloseConnection();
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception($"{Tag} : {exception}");
            }
        }

        /**
         * Permite realizar una consulta de una funcion para crear o actualizar
         */
        protected NpgsqlCommand CreateOrUpdateQuery(Dictionary<string, object>? parameters, string query, string outputParamName, NpgsqlDbType outputDbType)
        {
            var command = ApplyParameters(true, query, parameters);
            command.Parameters.Add(new NpgsqlParameter(outputParamName, outputDbType)
            {
                Direction = ParameterDirection.Output
            });

            command.ExecuteNonQuery();
            return command;
        }

        protected NpgsqlCommand GetValue(Dictionary<string, object>? parameters, string query, string outputParamName, NpgsqlDbType outputDbType)
        {
            var command = ApplyParameters(true, query, parameters);
            command.Parameters.Add(new NpgsqlParameter(outputParamName, outputDbType)
            {
                Direction = ParameterDirection.Output
            });

            command.ExecuteScalar();
            return command;
        }


        /**
         * Cerrar la conexion a la base de datos
         */
        public void CloseConnection()
        {
            Connection.Close();
        }
    }
}