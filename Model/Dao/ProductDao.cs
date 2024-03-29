﻿using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class ProductDao
    {

        ShopDbContext db = null;
        public ProductDao()
        {
            db = new ShopDbContext();
        }
        public long Insert(Product enity)
        {
            try
            {
                enity.createdat = DateTime.Now;
                db.Products.Add(enity);
                db.SaveChanges();
                return enity.id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public long Update(Product enity)
        {
            try
            {
                enity.modifeiddat = DateTime.Now;
                var cate = db.Products.Find(enity.id);
                cate.name = enity.name;
                cate.image = enity.image;
                cate.despection = enity.despection;
                cate.modifeiddat = enity.modifeiddat;
                cate.modifeidby = enity.modifeidby;
                cate.detail = enity.detail;
                cate.status = enity.status;
                cate.includevat = enity.includevat;
                cate.price = enity.price;
                cate.quality = enity.quality;
                cate.viewcount = enity.viewcount;
                cate.wanarty = enity.wanarty;
                cate.newprice = enity.newprice;
                cate.Tophot = enity.Tophot;
                cate.categoryid = enity.categoryid;
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
                db.Products.Remove(db.Products.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
       
        public IEnumerable<Product> getAllByPageSize(int page, int pageSize)
        {
            return db.Products.OrderByDescending(x => x.name).ToPagedList(page, pageSize);
        }
        public bool changestatus(long id)
        {
            var cate = db.Products.Find(id);
            cate.status = !cate.status;
            db.SaveChanges();
            return !cate.status;
        }
        public ReturnProduct getByid(long id)
        {
            ReturnProduct pro = new ReturnProduct();
            pro.product = db.Products.SingleOrDefault(x => x.id == id);
            pro.ListImgbyProductid = getAllByproductid(id);
            return pro;
        }
        public List<IMProductDetail> getAllByproductid(long id)
        {
            //return db.Categories.Where(x=>x.status==true).ToList();
            return db.IMProductDetails.Where(x=>x.productid==id).ToList();
        }
    
        public List<Product> getbysp()
        {
            var product = db.Database.SqlQuery<Product>("spGetAllProduct");
            var list= product.ToList();
            for(int i = 0; i < list.Count; i++)
            {
               var enity= new CategoryDao().getByid(list[i].categoryid);
               list[i].Category = enity;
            }
            return list;
        }
        public List<Product> getAll()
        {            
            return db.Products.ToList();
        }
    }
}
/*
List<SqlParameter> param=new List<SqlParameter>();
param.add(new SqlParameter("Thuoctinh",giatri));
var product = db.Database.SqlQuery<Product>("spGetAllProduct @Thuoctinh,....., ",param.toArray());
*/
