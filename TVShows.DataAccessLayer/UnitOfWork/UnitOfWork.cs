using System;
using System.Threading.Tasks;
using TVShows.DataAccessLayer.Models;

namespace TVShows.DataAccessLayer.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dBcontext;
        private IGenericRepository<TVShowsModel> _tvShow;

        public UnitOfWork(DBContext dBContext)
        {
            _dBcontext = dBContext;
        }
        public IGenericRepository<TVShowsModel> TVShow => _tvShow ??= new GenericRepository<TVShowsModel>(_dBcontext);
        public void Dispose()
        {
            _dBcontext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dBcontext.SaveChangesAsync();
        }
    }
}
