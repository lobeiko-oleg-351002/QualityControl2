﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    [DataContract]
    public class UilSelectedControlName : IUilEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public UilControlName ControlName { get; set; }

    }
}
