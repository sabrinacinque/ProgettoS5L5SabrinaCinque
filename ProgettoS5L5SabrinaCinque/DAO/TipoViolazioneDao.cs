using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.DAO
{
    public class TipoViolazioneDao : ITipoViolazioneDao
    {
        private readonly string _connectionString;

        public TipoViolazioneDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<TipoViolazione> GetAll()
        {
            var tipoViolazioni = new List<TipoViolazione>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Tipo_Violazioni";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoViolazioni.Add(new TipoViolazione
                        {
                            IdViolazione = reader.GetInt32(0),
                            Descrizione = reader.GetString(1),
                            Importo = reader.GetDecimal(2),
                            DecurtamentoPunti = reader.GetInt32(3)
                        });
                    }
                }
            }

            return tipoViolazioni;
        }

        public TipoViolazione GetById(int id)
        {
            TipoViolazione tipoViolazione = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Tipo_Violazioni WHERE IdViolazione = @IdViolazione";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdViolazione", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tipoViolazione = new TipoViolazione
                            {
                                IdViolazione = reader.GetInt32(0),
                                Descrizione = reader.GetString(1),
                                Importo = reader.GetDecimal(2),
                                DecurtamentoPunti = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }

            return tipoViolazione;
        }

        public void Add(TipoViolazione tipoViolazione)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    INSERT INTO Tipo_Violazioni (Descrizione, Importo, DecurtamentoPunti)
                    VALUES (@Descrizione, @Importo, @DecurtamentoPunti)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Descrizione", tipoViolazione.Descrizione);
                    cmd.Parameters.AddWithValue("@Importo", tipoViolazione.Importo);
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", tipoViolazione.DecurtamentoPunti);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(TipoViolazione tipoViolazione)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    UPDATE Tipo_Violazioni SET 
                    Descrizione = @Descrizione, 
                    Importo = @Importo, 
                    DecurtamentoPunti = @DecurtamentoPunti
                    WHERE IdViolazione = @IdViolazione";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Descrizione", tipoViolazione.Descrizione);
                    cmd.Parameters.AddWithValue("@Importo", tipoViolazione.Importo);
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", tipoViolazione.DecurtamentoPunti);
                    cmd.Parameters.AddWithValue("@IdViolazione", tipoViolazione.IdViolazione);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
    }
}
