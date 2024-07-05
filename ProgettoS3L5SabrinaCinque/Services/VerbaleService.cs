using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgettoS3L5SabrinaCinque.Models;
using System.Collections.Generic;

namespace ProgettoS3L5SabrinaCinque.Services
{
    public class VerbaleService : IVerbaleService
    {
        private readonly SqlConnection _connection;

        public VerbaleService(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("AppPoliziaMunicipale");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'AppPoliziaMunicipale' is not defined.");
            }
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Verbale> GetAllVerbali()
        {
            try
            {
                _connection.Open();
                var query = @"
                    SELECT 
                        v.IdVerbale, v.DataViolazione, v.IndirizzoViolazione, v.Nominativo_Agente, 
                        v.DataTrascrizioneVerbale, a.Cognome, a.Nome, 
                        t.Importo, t.Descrizione,t.DecurtamentoPunti
                    FROM 
                        Verbali v
                    JOIN 
                        Anagrafiche a ON v.IdAnagrafica = a.IdAnagrafica
                    JOIN 
                        Tipo_Violazioni t ON v.IdViolazioneVerbale = t.IdViolazione
                    ORDER BY 
                        v.DataTrascrizioneVerbale DESC";

                using var cmd = new SqlCommand(query, _connection);
                using var reader = cmd.ExecuteReader();
                var list = new List<Verbale>();
                while (reader.Read())
                {
                    var verbale = new Verbale
                    {
                        IdVerbale = reader.GetInt32(0),
                        DataViolazione = reader.GetDateTime(1),
                        IndirizzoViolazione = reader.GetString(2),
                        Nominativo_Agente = reader.GetString(3),
                        DataTrascrizioneVerbale = reader.GetDateTime(4),
                        Anagrafica = new Anagrafica
                        {
                            Cognome = reader.GetString(5),
                            Nome = reader.GetString(6)
                        },
                        TipoViolazione = new TipoViolazione
                        {
                            Importo = reader.GetDecimal(7),
                            Descrizione= reader.GetString(8),
                            DecurtamentoPunti = reader.GetInt32(9)
                        }
                    };
                    list.Add(verbale);
                }
                return list;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return new List<Verbale>();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
