using System.Collections.Generic;
using System.Linq;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public class GenreLogic : IGenreLogic
    {
        private readonly IGenreDAO genreDAO;

        public GenreLogic(IGenreDAO genreDAO)
        {
            this.genreDAO = genreDAO;
        }

        public bool Add(Genre genre)
        {
            try
            {
                return this.genreDAO.Add(genre);
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
                return this.genreDAO.AddGenresByGameId(gameId, ids);
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
                return this.genreDAO.GetByGameId(gameId);
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
                return this.genreDAO.GetByIds(ids).ToArray();
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
                return this.genreDAO.GetByNamePart(keyWord).ToArray();
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
                return this.genreDAO.RemoveGenresByGameId(gameId, ids);
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
                return this.genreDAO.UpdateGenresByGameId(gameId, ids);
            }
            catch
            {
                throw;
            }
        }
    }
}