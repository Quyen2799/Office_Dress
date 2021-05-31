using DataProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO
{
    public class ProductIO
    {
        MyShopHTQDB db = new MyShopHTQDB();

        //List product
        public List<Product> GetListProduct()
        {
            return db.Products.ToList();
        }

        //Product details
        public Product GetDetails(string id)
        {
            string query = "select * from dbo.Products where id = '" + id + "'";
            return db.Database.SqlQuery<Product>(query).FirstOrDefault();
        }

        //Product create
        public void AddProduct(Product pro)
        {
            db.Database.ExecuteSqlCommand(
                "insert into dbo.Products (ID, PRODUCTNAME, IDCATE, SIZE, PRICE, MATERIAL, HDBQ, PRODUCTIMAGE) values (@A, @B, @C, @D, @E, @F, @G, @H)",
                new SqlParameter("@A", pro.ID),
                new SqlParameter("@B", pro.PRODUCTNAME),
                new SqlParameter("@C", pro.IDCATE),
                new SqlParameter("@D", pro.SIZE),
                new SqlParameter("@E", pro.PRICE),
                new SqlParameter("@F", pro.MATERIAL),
                new SqlParameter("@G", pro.HDBQ),
                new SqlParameter("@H", pro.PRODUCTIMAGE)
                );
        }
        public void UpdatePro(Product pro)
        {
            db.Entry(pro).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeletePro(Product pro)
        {
            db.Entry(pro).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public List<Product> GetProductsByID(string idcate)
        {
            return db.Products.Where(c => c.IDCATE == idcate).ToList();
        }

        public List<Product> GetTopNew(string idcate)
        {
            string query = "select top 3 * from dbo.Products where IDCATE = '"+ idcate +"' order by NEWID()";
            return db.Database.SqlQuery<Product>(query).ToList();
        }

        public List<Product> GetListSearch(string name)
        {
            string query = "select * from dbo.Products where PRODUCTNAME like N'%" + name + "%'";
            return db.Database.SqlQuery<Product>(query).ToList();
        }
    }
}
