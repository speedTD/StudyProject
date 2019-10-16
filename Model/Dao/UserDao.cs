using Model.EF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
  public  class UserDao
    {
        ShopDbContext db = null;
        public UserDao()
        {
            db = new ShopDbContext();
        }
        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.id;
        }
        public bool login(String name ,String pass)
        {
            var result = db.Users.Count(x => x.name == name && x.pass == pass);
            if (result > 0)
            {
              return true;
            }else
            {
                return false;
            }
            
        }

        public User getByid(String name)
        {
            return db.Users.SingleOrDefault(x => x.name == name);
        }
        public IEnumerable<User> getAll(int page,int pageSize)
        {
            return db.Users.ToPagedList(page,pageSize);
        }

    }
}
