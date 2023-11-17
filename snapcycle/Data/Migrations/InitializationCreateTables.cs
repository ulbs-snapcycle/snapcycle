using System.Data;
using FluentMigrator;

namespace polyclinic_service.Data.Migrations;

[Migration(103112023)]
public class InitializationCreateTables : Migration
{
    public override void Up()
    {
        CreateUsersTable();
        CreateImagesTable();
        CreateUserImagesTable();
        CreateIndexes();
        CreateForeignKeys();
        SeedInitialData();
    }

    private void CreateUsersTable()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(128).NotNullable()
            .WithColumn("Email").AsString(128).NotNullable().Unique()
            .WithColumn("Password").AsString(128).NotNullable()
            .WithColumn("Gender").AsString(16).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable()
            .WithColumn("Phone").AsString(32).NotNullable()
            .WithColumn("Type").AsInt32().NotNullable();
    }

    private void CreateImagesTable()
    {
        Create.Table("Images")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(32).NotNullable();
    }

    private void CreateUserImagesTable()
    {
        Create.Table("UserImages")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().NotNullable()
            .WithColumn("ImageID").AsInt32().NotNullable();
    }

    private void CreateIndexes()
    {
        Create.Index("IX_UserImages_UserId").OnTable("UserImages").OnColumn("UserId").Ascending().WithOptions().NonClustered();
        Create.Index("IX_UserImages_ImageId").OnTable("UserImages").OnColumn("ImageId").Ascending().WithOptions().NonClustered();
    }

    private void CreateForeignKeys()
    {
        Create.ForeignKey("FK_UserImages_User").FromTable("UserImages").ForeignColumn("UserId").ToTable("Users").PrimaryColumn("Id").OnDelete(Rule.Cascade);
        Create.ForeignKey("FK_UserImages_Image").FromTable("UserImages").ForeignColumn("ImageId").ToTable("Images").PrimaryColumn("Id").OnDelete(Rule.Cascade);
    }

    private void SeedInitialData()
    {
        Execute.Script(@"./Data/Scripts/start-users.sql");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_UserImages_Image").OnTable("UserImages");
        Delete.ForeignKey("FK_UserImages_User").OnTable("UserImages");
        Delete.Index("IX_UserImages_ImageId").OnTable("UserImages");
        Delete.Index("IX_UserImages_UserId").OnTable("UserImages");
        Delete.Table("UserImages");
        Delete.Table("Images");
        Delete.Table("Users");
    }
}
