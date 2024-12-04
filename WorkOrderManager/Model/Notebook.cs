using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManager.Model
{
    public class Notebook {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int WorkorderId { get; set; }
    }
}
