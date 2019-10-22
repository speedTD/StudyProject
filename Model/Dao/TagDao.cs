using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class TagDao
    {
        public ShopDbContext db =null;

        public TagDao()
        {
            db = new ShopDbContext();
        }
        public List<Tag> getAll()
        {
            return db.Tags.ToList();
        }
      

    }
}
