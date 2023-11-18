using FluentMigrator;

namespace polyclinic_service.Data.Migrations;

[Migration(181120231)]
public class ModifyUserImagesTable : Migration
{
    public override void Up()
    {
        Alter.Table("UserImages")
            .AddColumn("TrashType").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Column("TrashType").FromTable("UserImages");
    }
}