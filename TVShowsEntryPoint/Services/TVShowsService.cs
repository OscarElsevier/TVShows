using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVShows.DataAccessLayer.DataAccess;
using TVShows.DataAccessLayer.Enums;
using TVShows.DataAccessLayer.Models;

namespace TVShowsEntryPoint.Services
{
    public class TVShowsService : ITVShowsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TVShowsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task FillDbContext()
        {
            var tvShowsList = FillTvShowList();
            await _unitOfWork.TVShow.InsertRange(tvShowsList);
            await _unitOfWork.Save();
        }

        public async Task GetTVShows(TVShowsEnum tvShowsEnum)
        {
            var tvShows = new List<TVShowsModel>();
            switch (tvShowsEnum)
            {
                case TVShowsEnum.All:
                    tvShows = await _unitOfWork.TVShow.GetAll();
                    break;
                case TVShowsEnum.Favorites:
                    tvShows = await _unitOfWork.TVShow.GetAll(exp => exp.IsFavorite);
                    break;
            }

            if (tvShows.Any())
            {
                foreach (var tvShow in tvShows)
                {
                    Console.WriteLine("{0} {1} {2}", tvShow.ID.ToString(), tvShow.TVShowName, tvShow.IsFavorite ? "*" : string.Empty);
                }
            }
            else
            {
                Console.WriteLine("{0}", "No TvShows found");
            }
        }

        public async Task<bool> TVShowMakeFavoriteOrNot(int id)
        {
            try
            {
                var tvShow = await _unitOfWork.TVShow.Get(exp => exp.ID == id);
                tvShow.IsFavorite = !tvShow.IsFavorite;

                _unitOfWork.TVShow.Update(tvShow);
                await _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:{0}", ex.Message);
                return false;
            }
        }

        private List<TVShowsModel> FillTvShowList()
        {
            return new List<TVShowsModel>()
            {
                new TVShowsModel()
                {
                    TVShowName = "Sports",
                    IsFavorite = false
                },
                new TVShowsModel()
                {
                    TVShowName = "News",
                    IsFavorite = false
                },
                new TVShowsModel()
                {
                    TVShowName = "Series",
                    IsFavorite = false
                },
                new TVShowsModel()
                {
                    TVShowName = "Movies",
                    IsFavorite = false
                },
                new TVShowsModel()
                {
                    TVShowName = "SoapOperas",
                    IsFavorite = false
                },

            };
        }
    }
}
