using GamePool.BLL.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class ImageLogic : IImageLogic
    {
        private IImageDAO imageDAO;

        public ImageLogic(IImageDAO imageDAO)
        {
            this.imageDAO = imageDAO;
        }

        public bool Add(ImageEntity imageEntity)
        {
            try
            {
                return this.imageDAO.Add(imageEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ImageEntity GetById(int id)
        {
            try
            {
                return this.imageDAO.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                return this.imageDAO.Remove(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}