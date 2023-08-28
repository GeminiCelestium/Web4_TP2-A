using Microsoft.Extensions.Logging;
using Web2.API.Data.Models;

namespace Web2.API.Repositories
{
    public interface IEventRepository
    {
        Evenement GetById(int eventId);
        decimal TotalDesVentesEvenement(int eventId, int NombreDePlace);
    }
}
