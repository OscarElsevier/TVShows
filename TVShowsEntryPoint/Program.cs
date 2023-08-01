using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TVShows.DataAccessLayer.DataAccess;
using TVShows.DataAccessLayer.Enums;
using TVShowsEntryPoint.Services;
namespace TVShowsEntryPoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITVShowsService, TVShowsService>();
            services.AddDbContext<DBContext>(options => options.UseInMemoryDatabase("Database"));

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var tvShowsService = serviceProvider.GetService<ITVShowsService>();

            await tvShowsService.FillDbContext();
            await tvShowsService.GetTVShows(TVShowsEnum.All);

            while (1 == 1)
            {
                Console.WriteLine("Select one of the following options: \n 1 to 5 to indicate a favorite/unfavorite TVShow \n list \n favorites");
                var option = Console.ReadLine().ToString();

                if (option != null)
                {
                    switch (option)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            var id = Convert.ToUInt16(option);
                            if (!await tvShowsService.TVShowMakeFavoriteOrNot(id))
                                break;
                            break;
                        case "list":
                            await tvShowsService.GetTVShows(TVShowsEnum.All);
                            break;
                        case "favorites":
                            await tvShowsService.GetTVShows(TVShowsEnum.Favorites);
                            break;
                        default:
                            Console.WriteLine("invalid option, please try again");
                            break;
                    }
                }

            }
        }
    }
}
