using RouletteBetting.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouletteBetting.Models.Abstract
{
    public interface IRoulette
    {
        Task<Roulette> AddRoulette(Roulette oRoulette);
        Task<Roulette> OpenRulette(Roulette oRoulette);
        Task<Bet> ToBet(Bet oBet);
        Task<List<Bet>> CloseRoulette(Bet oBet);
        Task<List<Roulette>> ListRoulette();
    }
}
