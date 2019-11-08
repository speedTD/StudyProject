using Model.EF;
using PagedList;
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
            try
            {
                enity.createdat = DateTime.Now;
                db.Categories.Add(enity);
                db.SaveChanges();
                return enity.id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public long Update(Category enity)
        {
            try
            {
                enity.modifeiddat = DateTime.Now;
                var cate = db.Categories.Find(enity.id);
                cate.name = enity.name;
                cate.image = enity.image;
                cate.keyword = enity.keyword;
                cate.modifeiddat = enity.modifeiddat;
                cate.modifeidby = enity.modifeidby;
                cate.seotitle = enity.seotitle;
                cate.status = enity.status;
                cate.showonhome = enity.showonhome;
                db.SaveChanges();
                return enity.id;
            }
            catch(Exception ex)
            {
                return -1;
            }
        
        }
        public bool Delete(long id)
        {
            try
            {
                db.Categories.Remove(db.Categories.Find(id));
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
 
        }
        public List<Category> getAll()
        {
             //return db.Categories.Where(x=>x.status==true).ToList();
            return db.Categories.ToList();
        }
        public IEnumerable<Category> getAllByPageSize(int page, int pageSize)
        {
            return db.Categories.OrderByDescending(x => x.name).ToPagedList(page, pageSize);
        }
        public bool changestatus(long id)
        {
            var cate = db.Categories.Find(id);
            cate.status = !cate.status;
            db.SaveChanges();
            return !cate.status;
        }
        public Category getByid(long id)
        {
            return db.Categories.SingleOrDefault(x => x.id == id);
        }

        public List<Category> lstgetByName(String name)
        {
            return db.Categories.Where(x => x.name.Contains(name)).ToList();
        }


    }
}
