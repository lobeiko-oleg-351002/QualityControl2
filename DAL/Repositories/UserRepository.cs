using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using AutoMapper;
using DAL.Mapping;

namespace DAL.Repositories
{
    public class UserRepository : Repository<DalUser, User, UserMapper>, IUserRepository
    {
        private readonly ServiceDB context;
        public UserRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalUser Authorize(string login, string password)
        {
            var ormEntity = context.Users.FirstOrDefault(entity => entity.login == login);
            if (ormEntity != null)
            {
                if (ormEntity.password.Equals(password))
                {
                    return mapper.MapToDal(ormEntity);
                }
            }
            return null;
            
        }


        public DalUser GetUserByLogin(string login)
        {
            var ormEntity = context.Users.FirstOrDefault(entity => entity.login == login);
            if (ormEntity == null) return null;
            return mapper.MapToDal(ormEntity);
        }




    }
}
