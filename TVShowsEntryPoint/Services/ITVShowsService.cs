using System.Threading.Tasks;
using TVShows.DataAccessLayer.Enums;

namespace TVShowsEntryPoint.Services
{
    interface ITVShowsService
    {
        Task FillDbContext();
        Task GetTVShows(TVShowsEnum tvShowsEnum);
        Task<bool> TVShowMakeFavoriteOrNot(int id);
    }
}
