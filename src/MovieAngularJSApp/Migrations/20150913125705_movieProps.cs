using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace MovieAngularJSApp.Migrations
{
    public partial class movieProps : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Director = table.Column(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column(type: "datetime2", nullable: false),
                    TicketPrice = table.Column(type: "decimal(18, 2)", nullable: false),
                    Title = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Movie");
        }
    }
}
