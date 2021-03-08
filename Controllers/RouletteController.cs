using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RouletteBetting.Models.Abstract;
using RouletteBetting.Models.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouletteBetting.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private IRoulette _Roulette;
        private IRedisCache _RedisCache;
        private readonly IConfiguration _Configuration;
        private readonly TimeSpan _ExpirationTime;
        public RouletteController(IRoulette _Roullete,IRedisCache _IRedisCache, IConfiguration _IConfiguration)
        {
            _Roulette = _Roullete;
            _RedisCache = _IRedisCache;
            _Configuration = _IConfiguration;
            _ExpirationTime = TimeSpan.FromSeconds(Convert.ToInt32(_Configuration["RedisConfig:ExpirationTime"]));
        }
        [HttpPost]
        public async Task<IActionResult> AddRoulette(Roulette oRoulette)
        {
            oRoulette = await _Roulette.AddRoulette(oRoulette);
            return StatusCode(201, oRoulette.IdRoulette);
        }
        [HttpPost]
        public async Task<IActionResult> OpenRoulette(Roulette oRoulette)
        {
            oRoulette = await _Roulette.OpenRulette(oRoulette);
            return StatusCode(200, oRoulette.Status);
        }
        [HttpPost]
        public async Task<IActionResult> ToBet([FromHeader(Name = "IdClient")] int? IdClient, Bet oBet)
        {
            string Message = string.Empty;
            Guid guid = Guid.NewGuid();
            IdClient =  IdClient == null ? 0 : IdClient;
            if (IdClient != 0)
            {
                if ((oBet.Number > -1 && oBet.Number < 37) || (oBet.Color.ToLower().Trim() == "rojo" || oBet.Color.ToLower().Trim() == "negro") && (oBet.Amount > 0 && oBet.Amount <= 10000))
                {
                    oBet.IdClient = IdClient;
                    oBet = await _Roulette.ToBet(oBet);
                    Message = oBet.Messaje;                    
                }
                else
                {
                    Message = "Apuesta Invalida";
                    oBet.Status = false;
                }
            }
            else
            {
                Message = "Apuesta Invalida";
                oBet.Status = false;
            }           
            return StatusCode(200, new { Message = Message, Status = oBet.Status });
        }
        [HttpPost]
        public async Task<IActionResult> CloseRoulette(Bet oBet)
        {
            int WinningNumber = new Random().Next(0, 36);
            string WinningColor = WinningNumber == 0 || (WinningNumber % 2) == 0 ? "red" : "negro";
            oBet.Color = WinningColor;
            oBet.Number = WinningNumber;
            List<Bet> ListBets = await _Roulette.CloseRoulette(oBet);
            return StatusCode(200, ListBets);
        }

        [HttpGet]
        public async Task<IActionResult> ListRoulettes()
        {
            List<Roulette> ListRoulettes;
            var ListRoulettesCache = _RedisCache.Get<List<Roulette>>("ListRoulettes");
            if (ListRoulettesCache == null)
            {
                ListRoulettes = await _Roulette.ListRoulette();
                _RedisCache.Set<List<Roulette>>("ListRoulettes", ListRoulettes, _ExpirationTime);
            }
            else
            {
                ListRoulettes = ListRoulettesCache;
            }
           
            return StatusCode(200, ListRoulettes);
        }
    }
}
