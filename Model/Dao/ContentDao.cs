using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        public ShopDbContext db = null;

        public ContentDao()
        {
            db = new ShopDbContext();
        }
        public List<Content> getAll()
        {
            return db.Contents.ToList();
        }
        public Content getByid(long id)
        {
            return db.Contents.SingleOrDefault(x => x.id == id);
        }
    }
}
