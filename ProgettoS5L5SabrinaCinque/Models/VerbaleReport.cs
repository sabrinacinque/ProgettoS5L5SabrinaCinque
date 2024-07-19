namespace ProgettoS5L5SabrinaCinque.Models
{
    public class VerbaleReport
    {
        public int IdVerbale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int TotaleVerbali { get; set; }
        public int TotalePunti { get; set; }
        public decimal Importo { get; set; }
        public DateTime DataViolazione { get; set; }
        public int DecurtamentoPunti { get; set; }
        public string Descrizione { get; set; }

    }
}
