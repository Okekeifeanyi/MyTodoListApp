namespace ToDoListEFC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        Task = c.String(),
                        Description = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                        Updated_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Entities");
        }
    }
}
