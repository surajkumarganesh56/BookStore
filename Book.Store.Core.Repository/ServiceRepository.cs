using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Store.Core.Repository
{
    
    public interface IServiceRepository<t>
    {
        public IQueryable<t> List();
        public t Add(t model);
        public t Update(t model);
        public int Remove(t model);
        public t Find(int id);
        public int AddRange(List<t> models);
        public int RemoveRange(List<t> models);
    }

    internal class ServiceRepository<t> : IServiceRepository<t>, IDisposable where t : class
    {
        BookStoreDBContext db;
        DbSet<t> entity;
        public ServiceRepository()
        {
            db = new BookStoreDBContext();
            entity = db.Set<t>();
        }
        public ServiceRepository(BookStoreDBContext db)
        {
            this.db = db;
            entity = db.Set<t>();
        }
        public IQueryable<t> List()
        {
            try
            {
                return entity;
            }
            catch (Exception ex)
            {
                return entity;
            }
        }
        public t Add(t model)
        {
            try
            {
                entity.Add(model);
                db.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                return model;
            }
        }
        public t Update(t model)
        {
            try
            {
                db.Entry<t>(model).State = EntityState.Modified;
                db.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Remove(t model)
        {
            entity.Remove(model);
            return db.SaveChanges();
        }
        public t Find(int id)
        {
            return entity.Find(id);
        }
        public int AddRange(List<t> models)
        {
            entity.AddRange(models);
            return db.SaveChanges();
        }
        public int RemoveRange(List<t> models)
        {
            entity.RemoveRange(models);
            return db.SaveChanges();
        }


        public void Dispose()
        {
            //your memory
            //your connection
            //place to clean
        }
    }

    public class ServiceFactory : IDisposable
    {
        public BookStoreDBContext db;

        public ServiceFactory()
        {
            db = new BookStoreDBContext();
        }
        public ServiceFactory(BookStoreDBContext db)
        {
            this.db = db;
        }
        public void Dispose()
        {
            // throw new NotImplementedException();
            //db.Dispose();
        }
        public IServiceRepository<t> GetInstance<t>() where t : class
        {
            return new ServiceRepository<t>(db);
        }

        public void BeginTransaction()
        {
            this.db.Database.BeginTransaction();
        }
        public void RollBack()
        {
            this.db.Database.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            this.db.Database.CommitTransaction();

        }

        public void WriteLog(string message, object exception, string v)
        {
            throw new NotImplementedException();
        }
    }
}
