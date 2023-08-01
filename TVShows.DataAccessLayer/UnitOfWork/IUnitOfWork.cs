using System;
using System.Threading.Tasks;
using TVShows.DataAccessLayer.Models;

namespace TVShows.DataAccessLayer.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TVShowsModel> TVShow { get; }

        Task Save();
    }
}
