using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class CategoryDao
    {
        ShopDbContext db = null;
        public CategoryDao()
        {
            db = new ShopDbContext();
        }
        public long Insert(Category enity)
        {
                enity.createdat = DateTime.Now;
                db.Categories.Add(enity);
                db.SaveChanges();
                return enity.id;
        }

        public List<Category> getAll()
        {
             //return db.Categories.Where(x=>x.status==true).ToList();
            return db.Categories.ToList();
        }


    }
}
