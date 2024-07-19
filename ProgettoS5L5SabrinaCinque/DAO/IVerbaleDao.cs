using ProgettoS5L5SabrinaCinque.Models;
using System.Collections.Generic;

namespace ProgettoS5L5SabrinaCinque.DAO
{
    public interface IVerbaleDao
    {
        IEnumerable<Verbale> GetAll();
        Verbale GetById(int id);
        void Add(Verbale verbale);
        void Update(Verbale verbale);

        // Metodi per i report
        IEnumerable<VerbaleReport> GetVerbaliByTrasgressore();
        IEnumerable<VerbaleReport> GetPuntiDecurtatiByTrasgressore();
        IEnumerable<VerbaleReport> GetViolazioniConPiuDiDieciPunti();
        IEnumerable<VerbaleReport> GetViolazioniConImportoMaggioreDi150();

    }
}
