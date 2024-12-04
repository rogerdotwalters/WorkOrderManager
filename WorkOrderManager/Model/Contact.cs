using SQLite;

namespace WorkOrderManager.Model {
    public class Contact {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
    }
}
