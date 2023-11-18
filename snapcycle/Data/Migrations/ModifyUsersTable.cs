using FluentMigrator;

namespace polyclinic_service.Data.Migrations;

[Migration(171120232)]
public class ModifyUsersTable : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("Score").AsInt32().NotNullable().WithDefaultValue(0)
            .AddColumn("CountTotal").AsInt32().NotNullable().WithDefaultValue(0)
            .AddColumn("CountHorrible").AsInt32().NotNullable().WithDefaultValue(0)
            .AddColumn("CountPartial").AsInt32().NotNullable().WithDefaultValue(0)
            .AddColumn("CountPerfect").AsInt32().NotNullable().WithDefaultValue(0);
        
        Delete.Column("Gender").FromTable("Users");
        Delete.Column("Age").FromTable("Users");
        
        Execute.Script(@"./Data/Scripts/start-users.sql");
    }

    public override void Down()
    {
        Delete.Column("Score").FromTable("Users");
        Delete.Column("CountTotal").FromTable("Users");
        Delete.Column("CountHorrible").FromTable("Users");
        Delete.Column("CountPartial").FromTable("Users");
        Delete.Column("CountPerfect").FromTable("Users");
        
        Alter.Table("Users")
            .AddColumn("Gender").AsString(16).NotNullable()
            .AddColumn("Age").AsInt32().NotNullable();
    }
}