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
    public class UserDao
    {
        ShopDbContext db = null;
        public UserDao()
        {
            db = new ShopDbContext();
        }
        public long Insert(User user)
        {
            user.createdat = DateTime.Now;
            if (checkDuplicateUser(user.name))
            {
                return -1;
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user.id;
            }

        }
        public bool Update(User user)
        {
            try
            {
                // tìm theo 1 bản ghi khi đã lấy đc id
                var u = db.Users.Find(user.id);
                u.name = user.name;
                u.pass = user.pass;
                u.fullname = user.fullname;
                u.address = user.address;
                u.phone = user.phone;
                u.status = user.status;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public User ViewDetailUser(int id)
        {
            return db.Users.Find(id);
        }
        public bool checkDuplicateUser(String name)
        {
            var reuslt = db.Users.Count(x => x.name == name);
            if (reuslt >= 1)
                return true;
            else
                return false;
        }
        public bool login(String name, String pass)
        {
            var result = db.Users.Count(x => x.name == name && x.pass == pass);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool changestatus(long id)
        {
            var user = db.Users.Find(id);
            user.status = !user.status;
            db.SaveChanges();
            return !user.status;
        }

        public User getbyname(String name)
        {
            return db.Users.SingleOrDefault(x => x.name == name);
        }

        public IEnumerable<User> getAll()
        {
            return db.Users.ToList();
        }
        public User getByid(long id)
        {
            return db.Users.SingleOrDefault(x => x.id == id);
        }
        public IEnumerable<User> getAllByPageSize(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x => x.name).ToPagedList(page, pageSize);
        }
        public bool deleteByPk(int id)
        {
            try
            {
                var user = db.Users.Remove(ViewDetailUser(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
         
        }
    }
}
