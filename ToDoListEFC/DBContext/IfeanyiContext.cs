using System.Data.Entity;

namespace ToDoListEFC
{
    public class IfeanyiContext : DbContext 
    {
        public DbSet<Entities> Entity {  get; set; }

        public IfeanyiContext()
            :base ("name = IfeanyiContext")
        {
           
        }



    }
}
