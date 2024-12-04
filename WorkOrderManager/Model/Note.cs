using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManager.Model
{
    public class Note {

        public enum Type {

            General,
            Material,
            Quote,
            Invoice,
            Urgent,
            Spec,
        }

        public enum Status {

            Assigned,
            ETA,
            QuoteNeeded,
            Quoted,
            QuotedIncurred,
            Approved,
            Rejected,
            Completed,
            Closed,
            Finalized,
            Invoiced,
            Exported,
            Cancelled
        }
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int NotebookId { get; set; }
        public Type Notetype { get; set; }
        public string FileLocation { get; set; }
        public DateTime DateCreated { get; set; }
        public Status NoteStatus { get; set; }
    }
}
