namespace Resume_Builder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProjectTeams", "EmployeeStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectTeams", "EmployeeEndDate", c => c.DateTime());
            AlterColumn("dbo.Projects", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ProjectTeams", "EmployeeEndDate");
            DropColumn("dbo.ProjectTeams", "EmployeeStartDate");
            DropColumn("dbo.Projects", "Status");
        }
    }
}
