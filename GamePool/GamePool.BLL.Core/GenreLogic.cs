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

        public bool AddRange(int gameId, IEnumerable<int> ids)
        {
            try
            {
                return this.genreDAO.AddRange(gameId, ids);
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
    }
}