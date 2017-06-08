﻿using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IControlMethodDocumentationLibService : IService<BllControlMethodDocumentationLib>
    {
        new BllControlMethodDocumentationLib Create(BllControlMethodDocumentationLib entity);
        new BllControlMethodDocumentationLib Update(BllControlMethodDocumentationLib entity);
    }
}
