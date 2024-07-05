using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgettoS3L5SabrinaCinque.Models;
using System.Collections.Generic;

namespace ProgettoS3L5SabrinaCinque.Services
{
    public class AnagraficaService : IAnagraficaService
    {
        private readonly SqlConnection _connection;

        public AnagraficaService(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("AppPoliziaMunicipale");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'AppPoliziaMunicipale' is not defined.");
            }
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Anagrafica> GetAllAnagrafiche()
        {
            try
            {
                _connection.Open();
                var query = "SELECT IdAnagrafica, Cognome, Nome, Indirizzo, Citta, Cap, Cod_Fisc FROM Anagrafiche ORDER BY Cognome";
                using var cmd = new SqlCommand(query, _connection);
                using var reader = cmd.ExecuteReader();
                var list = new List<Anagrafica>();
                while (reader.Read())
                {
                    list.Add(new Anagrafica
                    {
                        IdAnagrafica = reader.GetInt32(0),
                        Cognome = reader.GetString(1),
                        Nome = reader.GetString(2),
                        Indirizzo = reader.GetString(3),
                        Citta = reader.GetString(4),
                        Cap = reader.GetString(5),
                        Cod_Fisc = reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return new List<Anagrafica>();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
