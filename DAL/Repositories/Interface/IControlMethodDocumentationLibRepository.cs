﻿using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IControlMethodDocumentationLibRepository :IRepository<DalControlMethodDocumentationLib>
    {
        new ControlMethodDocumentationLib Create(DalControlMethodDocumentationLib entity);
        new void Delete(DalControlMethodDocumentationLib entity);
        new DalControlMethodDocumentationLib Get(int id);
        new IEnumerable<DalControlMethodDocumentationLib> GetAll();
        new void Update(DalControlMethodDocumentationLib entity);
    }
}
