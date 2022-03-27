using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Migrations
{
    public partial class Updated_Default_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Roles
                SET Permissions = 'create_user,delete_user,create_story,edit_story,delete_story,view_story,view_user,edit_user,view_project,create_project,update_project'
                WHERE Name = 'admin'
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Roles
                SET Permissions = 'create_user,delete_user,create_story,edit_story,delete_story,view_story,view_user,edit_user'
                WHERE Name = 'admin'
            ");
        }
    }
}
