using System.Collections.Generic;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.DAO
{
    public interface ITipoViolazioneDao
    {
        IEnumerable<TipoViolazione> GetAll();
        TipoViolazione GetById(int id);
        void Add(TipoViolazione tipoViolazione);
        void Update(TipoViolazione tipoViolazione);
    }
}
