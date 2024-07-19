namespace ProgettoS5L5SabrinaCinque.Models
{
    public class ReportViewModel
    {
        public IEnumerable<VerbaleReport> VerbaliByTrasgressore { get; set; }
        public IEnumerable<VerbaleReport> PuntiDecurtatiByTrasgressore { get; set; }
        public IEnumerable<VerbaleReport> ViolazioniConPiuDiDieciPunti { get; set; }
        public IEnumerable<VerbaleReport> ViolazioniConImportoMaggioreDi150 { get; set; }
    }
}
