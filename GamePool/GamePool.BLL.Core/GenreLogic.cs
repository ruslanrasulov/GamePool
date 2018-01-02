using System.Collections.Generic;
using System.Linq;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class GenreLogic : IGenreLogic
    {
        private readonly IGenreDao _genreDao;

        public GenreLogic(IGenreDao genreDao)
        {
            _genreDao = genreDao;
        }

        public bool Add(Genre genre)
        {
            try
            {
                return _genreDao.Add(genre);
            }
            catch
            {
                throw;
            }
        }

        public bool AddGenresByGameId(int gameId, IEnumerable<int> ids)
        {
            try
            {
                return _genreDao.AddGenresByGameId(gameId, ids);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Genre> GetByGameId(int gameId)
        {
            try
            {
                return _genreDao.GetByGameId(gameId);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Genre> GetByIds(IEnumerable<int> ids)
        {
            try
            {
                return _genreDao.GetByIds(ids).ToArray();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Genre> GetByNamePart(string keyWord)
        {
            try
            {
                return _genreDao.GetByNamePart(keyWord).ToArray();
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveGenresByGameId(int gameId, IEnumerable<int> ids)
        {
            try
            {
                return _genreDao.RemoveGenresByGameId(gameId, ids);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateGenresByGameId(int gameId, IEnumerable<int> ids)
        {
            try
            {
                return _genreDao.UpdateGenresByGameId(gameId, ids);
            }
            catch
            {
                throw;
            }
        }
    }
}