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
    public class CategoryIO
    {
        MyShopHTQDB db = new MyShopHTQDB();
        //List category
        public List<Category> GetListCate()
        {
            return db.Categories.ToList();
        }

        //Category Details
        public Category GetDetails(string id)
        {
            string query = "select * from dbo.Categories where id = '" + id + "'";
            return db.Database.SqlQuery<Category>(query).FirstOrDefault();
        }

        //Category Create
        public void AddCate(Category cate)
        {
            db.Database.ExecuteSqlCommand(
                "insert into dbo.Categories (ID, CATEGORYNAME) values (@A, @B)",
                new SqlParameter("@A", cate.ID),
                new SqlParameter("@B", cate.CATEGORYNAME)
                );
        }

        //Category Edit
        public void UpdateCate(Category cate)
        {
            db.Entry(cate).State = EntityState.Modified;
            db.SaveChanges();
        }

        //Category Delete
        public void DeleteCate(Category cate)
        {
            db.Entry(cate).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public List<Category> GetListSearch(string name)
        {
            string query = "select * from dbo.Categories where CATEGORYNAME like N'%" + name + "%'";
            return db.Database.SqlQuery<Category>(query).ToList();
        }
    }
}
