using System.Collections.Generic;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.DAO
{
    public interface IAnagraficaDao
    {
        IEnumerable<Anagrafica> GetAll();
        Anagrafica GetById(int id);
        void Add(Anagrafica anagrafica);
        void Update(Anagrafica anagrafica);
    }
}
