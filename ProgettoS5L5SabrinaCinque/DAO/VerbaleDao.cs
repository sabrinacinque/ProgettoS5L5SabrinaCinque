using ProgettoS5L5SabrinaCinque.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProgettoS5L5SabrinaCinque.DAO
{
    public class VerbaleDao : IVerbaleDao
    {
        private readonly string _connectionString;

        public VerbaleDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Verbale> GetAll()
        {
            var verbali = new List<Verbale>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Verbali";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var verbale = new Verbale
                            {
                                IdVerbale = (int)reader["IdVerbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = (string)reader["IndirizzoViolazione"],
                                Nominativo_Agente = (string)reader["Nominativo_Agente"],
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                IdAnagrafica = (int)reader["IdAnagrafica"],
                                IdViolazioneVerbale = (int)reader["IdViolazioneVerbale"]
                            };
                            verbali.Add(verbale);
                        }
                    }
                }
            }

            return verbali;
        }

        public Verbale GetById(int id)
        {
            Verbale verbale = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Verbali WHERE IdVerbale = @IdVerbale";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdVerbale", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            verbale = new Verbale
                            {
                                IdVerbale = (int)reader["IdVerbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = (string)reader["IndirizzoViolazione"],
                                Nominativo_Agente = (string)reader["Nominativo_Agente"],
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                IdAnagrafica = (int)reader["IdAnagrafica"],
                                IdViolazioneVerbale = (int)reader["IdViolazioneVerbale"]
                            };
                        }
                    }
                }
            }

            return verbale;
        }

        public void Add(Verbale verbale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Verbali (DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, IdAnagrafica, IdViolazioneVerbale)
                    VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @IdAnagrafica, @IdViolazioneVerbale)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                    command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    command.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                    command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                    command.Parameters.AddWithValue("@IdAnagrafica", verbale.IdAnagrafica);
                    command.Parameters.AddWithValue("@IdViolazioneVerbale", verbale.IdViolazioneVerbale);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Verbale verbale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Verbali
                    SET DataViolazione = @DataViolazione,
                        IndirizzoViolazione = @IndirizzoViolazione,
                        Nominativo_Agente = @Nominativo_Agente,
                        DataTrascrizioneVerbale = @DataTrascrizioneVerbale,
                        IdAnagrafica = @IdAnagrafica,
                        IdViolazioneVerbale = @IdViolazioneVerbale
                    WHERE IdVerbale = @IdVerbale";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdVerbale", verbale.IdVerbale);
                    command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                    command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    command.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                    command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                    command.Parameters.AddWithValue("@IdAnagrafica", verbale.IdAnagrafica);
                    command.Parameters.AddWithValue("@IdViolazioneVerbale", verbale.IdViolazioneVerbale);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Verbali WHERE IdVerbale = @IdVerbale";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdVerbale", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementazione dei metodi per i report
        public IEnumerable<VerbaleReport> GetVerbaliByTrasgressore()
        {
            var result = new List<VerbaleReport>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT a.Cognome, a.Nome, COUNT(v.IdVerbale) AS TotaleVerbali
                    FROM Verbali v
                    JOIN Anagrafiche a ON v.IdAnagrafica = a.IdAnagrafica
                    GROUP BY a.Cognome, a.Nome";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new VerbaleReport
                            {
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotaleVerbali = (int)reader["TotaleVerbali"]
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<VerbaleReport> GetPuntiDecurtatiByTrasgressore()
        {
            var result = new List<VerbaleReport>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT a.Cognome, a.Nome, SUM(tv.DecurtamentoPunti) AS TotalePuntiDecurtati
                    FROM Verbali v
                    JOIN Anagrafiche a ON v.IdAnagrafica = a.IdAnagrafica
                    JOIN Tipo_Violazioni tv ON v.IdViolazioneVerbale = tv.IdViolazione
                    GROUP BY a.Cognome, a.Nome";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new VerbaleReport
                            {
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                DecurtamentoPunti = (int)reader["TotalePuntiDecurtati"]
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<VerbaleReport> GetViolazioniConPiuDiDieciPunti()
        {
            var result = new List<VerbaleReport>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT v.IdVerbale, a.Cognome, a.Nome, tv.Importo, v.DataViolazione, tv.DecurtamentoPunti, tv.Descrizione
                    FROM Verbali v
                    JOIN Anagrafiche a ON v.IdAnagrafica = a.IdAnagrafica
                    JOIN Tipo_Violazioni tv ON v.IdViolazioneVerbale = tv.IdViolazione
                    WHERE tv.DecurtamentoPunti > 10
                    ORDER BY tv.DecurtamentoPunti DESC";//dato che qui l'attenzione è sui punti , li ordino per i punti decurtati in ordine decrescente

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new VerbaleReport
                            {
                                IdVerbale = (int)reader["IdVerbale"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Importo = (decimal)reader["Importo"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                Descrizione = reader["Descrizione"].ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }



        //ho fatto 150 invece di 400 perchè avevo importo più bassi nelle mie tabelle, era per provarre la query se andava bene
        public IEnumerable<VerbaleReport> GetViolazioniConImportoMaggioreDi150()
        {
            var result = new List<VerbaleReport>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT v.IdVerbale, a.Cognome, a.Nome, tv.Importo, v.DataViolazione, tv.DecurtamentoPunti, tv.Descrizione
                    FROM Verbali v
                    JOIN Anagrafiche a ON v.IdAnagrafica = a.IdAnagrafica
                    JOIN Tipo_Violazioni tv ON v.IdViolazioneVerbale = tv.IdViolazione
                    WHERE tv.Importo > 150 
                    ORDER BY tv.Importo DESC";//dato che qui l'attenzione è sull'importo , li ordino per importo  in ordine decrescente
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new VerbaleReport
                            {
                                IdVerbale = (int)reader["IdVerbale"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Importo = (decimal)reader["Importo"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                Descrizione = reader["Descrizione"].ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
