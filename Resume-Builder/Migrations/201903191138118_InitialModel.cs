namespace Resume_Builder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Bname = c.String(nullable: false, maxLength: 128),
                        Bemail = c.String(),
                        Bmobile = c.String(),
                        Bsubject = c.String(),
                        Bmessage = c.String(),
                    })
                .PrimaryKey(t => t.Bname);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
