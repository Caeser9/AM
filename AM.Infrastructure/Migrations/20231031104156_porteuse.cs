using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class porteuse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyPlane",
                columns: table => new
                {
                    PlaneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaneFk = table.Column<int>(type: "int", nullable: false),
                    PlaneType = table.Column<int>(type: "int", nullable: false),
                    AirlineLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PlaneCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyPlane", x => x.PlaneId);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassportNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FullName_FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    FullName_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TelNumber = table.Column<int>(type: "int", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployementDate = table.Column<DateTime>(type: "Date", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    HealthInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassportNumber);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    EffectiveArrival = table.Column<DateTime>(type: "Date", nullable: false),
                    Departure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaneFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_MyPlane_PlaneFk",
                        column: x => x.PlaneFk,
                        principalTable: "MyPlane",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationTicket",
                columns: table => new
                {
                    DateReservation = table.Column<DateTime>(type: "Date", nullable: false),
                    PassengerFK = table.Column<string>(type: "nvarchar(7)", nullable: false),
                    TicketFK = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTicket", x => new { x.PassengerFK, x.TicketFK, x.DateReservation });
                    table.ForeignKey(
                        name: "FK_ReservationTicket_Passengers_PassengerFK",
                        column: x => x.PassengerFK,
                        principalTable: "Passengers",
                        principalColumn: "PassportNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationTicket_Ticket_TicketFK",
                        column: x => x.TicketFK,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolePassanger",
                columns: table => new
                {
                    FlightsFlightId = table.Column<int>(type: "int", nullable: false),
                    PassengersPassportNumber = table.Column<string>(type: "nvarchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolePassanger", x => new { x.FlightsFlightId, x.PassengersPassportNumber });
                    table.ForeignKey(
                        name: "FK_VolePassanger_Flights_FlightsFlightId",
                        column: x => x.FlightsFlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolePassanger_Passengers_PassengersPassportNumber",
                        column: x => x.PassengersPassportNumber,
                        principalTable: "Passengers",
                        principalColumn: "PassportNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneFk",
                table: "Flights",
                column: "PlaneFk");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTicket_TicketFK",
                table: "ReservationTicket",
                column: "TicketFK");

            migrationBuilder.CreateIndex(
                name: "IX_VolePassanger_PassengersPassportNumber",
                table: "VolePassanger",
                column: "PassengersPassportNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationTicket");

            migrationBuilder.DropTable(
                name: "VolePassanger");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "MyPlane");
        }
    }
}
