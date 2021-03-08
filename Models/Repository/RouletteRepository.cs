using Microsoft.Extensions.Configuration;
using RouletteBetting.Models.Abstract;
using RouletteBetting.Models.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RouletteBetting.Models.Repository
{
    public class RouletteRepository:IRoulette
    {
        private readonly string _connectionString;
        public RouletteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }
        public async Task<Roulette> AddRoulette(Roulette oRoulette)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("add_roulette", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@descripcion", oRoulette.Description));
                    oRoulette = new Roulette();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            oRoulette.IdRoulette = (int)reader["IdRoulette"];
                        }
                    }
                    return oRoulette;
                }
            }
        }
        public async Task<Roulette> OpenRulette(Roulette oRoulette)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("open_roulette", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdRoulette", oRoulette.IdRoulette));
                    oRoulette = new Roulette();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            oRoulette.Status = (bool)reader["Status"];
                            oRoulette.Mensaje = (string)reader["Mensaje"];
                        }
                    }
                    return oRoulette;
                }
            }
        }
        public async Task<Bet> ToBet(Bet oBet)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("to_bet", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdRoulette", oBet.IdRoulette));
                    cmd.Parameters.Add(new SqlParameter("@IdClient", oBet.IdClient));
                    cmd.Parameters.Add(new SqlParameter("@Amount", oBet.Amount));
                    cmd.Parameters.Add(new SqlParameter("@Number", oBet.Number));
                    cmd.Parameters.Add(new SqlParameter("@Color", oBet.Color));
                    oBet = new Bet();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            oBet.Status = (bool)reader["Status"];
                            oBet.Messaje = (string)reader["Messaje"];
                        }
                    }
                    return oBet;
                }
            }
        }
        public async Task<List<Bet>> CloseRoulette(Bet oBet)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                List<Bet> ListBets = new List<Bet>();
                using (SqlCommand cmd = new SqlCommand("close_roulette", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdRoulette", oBet.IdRoulette));
                    cmd.Parameters.Add(new SqlParameter("@Number", oBet.Number));
                    cmd.Parameters.Add(new SqlParameter("@Color", oBet.Color));
                                        
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            oBet = new Bet();
                            oBet.Status = (bool)reader["Status"];
                            oBet.Messaje = (string)reader["Messaje"];
                            oBet.IdBet = (int)reader["IdBet"];
                            oBet.IdRoulette = (int)reader["IdRoulette"];
                            oBet.IdClient = (int)reader["IdClient"];
                            oBet.ClientName = (string)reader["ClientName"];
                            oBet.Amount = (decimal)reader["Amount"];
                            oBet.Number = (int)reader["Number"];
                            oBet.Color = (string)reader["Color"];
                            oBet.Winner = (string)reader["Winner"];
                            oBet.Reward = (decimal)reader["Reward"];
                            ListBets.Add(oBet);
                        }
                    }
                    return ListBets;
                }
            }
        }

        public async Task<List<Roulette>> ListRoulette()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                List<Roulette> ListRoulettes = new List<Roulette>();
                using (SqlCommand cmd = new SqlCommand("list_roulette", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Roulette oRoulette = new Roulette();
                            oRoulette.IdRoulette = (int)reader["IdRoulette"];
                            oRoulette.Description = (string)reader["Descripcion"];
                            oRoulette.Status = (bool)reader["Status"];
                            oRoulette.StatusDescription = (string)reader["StatusDescription"];
                            ListRoulettes.Add(oRoulette);
                        }
                    }
                    return ListRoulettes;
                }
            }
        }
    }
}
