using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControlMethodDocumentationLib : IBllEntityLib<BllControlMethodDocumentation>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllControlMethodDocumentation>> SelectedEntities { get; set; }

        public BllControlMethodDocumentationLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllControlMethodDocumentation>>();
        }
    }
}
