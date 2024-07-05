using ProgettoS3L5SabrinaCinque.Models;
using System.Collections.Generic;

namespace ProgettoS3L5SabrinaCinque.Services
{
    public interface IAnagraficaService
    {
        IEnumerable<Anagrafica> GetAllAnagrafiche();
    }
}
