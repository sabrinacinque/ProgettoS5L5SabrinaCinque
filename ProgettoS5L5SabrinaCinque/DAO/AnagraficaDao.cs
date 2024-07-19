using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.DAO
{
    public class AnagraficaDao : IAnagraficaDao
    {
        private readonly string _connectionString;

        public AnagraficaDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Anagrafica> GetAll()
        {
            var anagrafiche = new List<Anagrafica>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Anagrafiche";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        anagrafiche.Add(new Anagrafica
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
                }
            }

            return anagrafiche;
        }

        public Anagrafica GetById(int id)
        {
            Anagrafica anagrafica = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Anagrafiche WHERE IdAnagrafica = @IdAnagrafica";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdAnagrafica", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            anagrafica = new Anagrafica
                            {
                                IdAnagrafica = reader.GetInt32(0),
                                Cognome = reader.GetString(1),
                                Nome = reader.GetString(2),
                                Indirizzo = reader.GetString(3),
                                Citta = reader.GetString(4),
                                Cap = reader.GetString(5),
                                Cod_Fisc = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return anagrafica;
        }

        public void Add(Anagrafica anagrafica)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    INSERT INTO Anagrafiche (Cognome, Nome, Indirizzo, Citta, Cap, Cod_Fisc)
                    VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @Cap, @Cod_Fisc)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    cmd.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                    cmd.Parameters.AddWithValue("@Cap", anagrafica.Cap);
                    cmd.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Anagrafica anagrafica)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    UPDATE Anagrafiche SET 
                    Cognome = @Cognome, 
                    Nome = @Nome, 
                    Indirizzo = @Indirizzo, 
                    Citta = @Citta, 
                    Cap = @Cap, 
                    Cod_Fisc = @Cod_Fisc
                    WHERE IdAnagrafica = @IdAnagrafica";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    cmd.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                    cmd.Parameters.AddWithValue("@Cap", anagrafica.Cap);
                    cmd.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);
                    cmd.Parameters.AddWithValue("@IdAnagrafica", anagrafica.IdAnagrafica);
                    cmd.ExecuteNonQuery();
                }
            }
        }

       
    }
}
