using BLL.Entities.Interface;
using BLL.Services.Interface;
using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using ORM;

namespace BLL.Services
{
    public class Service<T, U, Y> : IService<T>
        where T : class, IBllEntity
        where U : class, IDalEntity
        where Y : class, IOrmEntity
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<U, Y> repository;
        public Service(IUnitOfWork uow, IRepository<U, Y> repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public virtual void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
