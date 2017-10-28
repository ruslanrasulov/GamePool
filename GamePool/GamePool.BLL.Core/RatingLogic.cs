using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class RatingLogic : IRatingLogic
    {
        private IRatingDAO ratingDAO;

        public RatingLogic(IRatingDAO ratingDAO)
        {
            this.ratingDAO = ratingDAO;
        }

        public bool Add(Rating rating)
        {
            try
            {
                return this.ratingDAO.Add(rating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Rating rating)
        {
            try
            {
                return this.ratingDAO.Update(rating);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}