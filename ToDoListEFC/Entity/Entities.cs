using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListEFC
{
    public  class Entities
    {
        [Key]
        public int TaskID { get; set; }
        public string Task {  get; set; }
        public string Description { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
       // public bool Deleted { get; set; }

    }
}
