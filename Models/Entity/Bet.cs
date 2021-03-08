
namespace RouletteBetting.Models.Entity
{
    public class Bet
    {
        public int IdBet { get; set; }
        public int IdRoulette { get; set; }
        public int? IdClient { get; set; }
        public string ClientName { get; set; }
        public int IdGroupBet { get; set; }
        public decimal? Amount { get; set; }
        public int? Number { get; set; }
        public string Color { get; set; }
        public string Winner { get; set; }
        public decimal Reward { get; set; }
        public string Messaje { get; set; }
        public bool Status { get; set; }
    }
}
