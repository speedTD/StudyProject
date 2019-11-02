using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class IMProductDao
    {
        public ShopDbContext db = null;

        public IMProductDao()
        {
            db = new ShopDbContext();
        }
        public long Insert(IMProductDetail enity)
        {
            try
            {
                db.IMProductDetails.Add(enity);
                db.SaveChanges();
                return enity.id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                db.IMProductDetails.Remove(db.IMProductDetails.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IMProductDetail getByid(long id)
        {
            return db.IMProductDetails.SingleOrDefault(x => x.id == id);
        }

    }
}
