using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArcticaAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    CarPark = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(type: "TEXT", nullable: false),
                    SName = table.Column<string>(type: "TEXT", nullable: false),
                    TName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Diagnosis = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Charge = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(type: "TEXT", nullable: false),
                    SName = table.Column<string>(type: "TEXT", nullable: false),
                    TName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    HospitalId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorPatient",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatientsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatient", x => new { x.DoctorsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Patient_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "CarPark", "Name" },
                values: new object[] { 1, "ул. Суворова, 1, Архангельск, Архангельская обл.", true, "Первая городская имени Волосевича" });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "CarPark", "Name" },
                values: new object[] { 2, "просп. Ломоносова, 292, Архангельск, Архангельская обл.", false, "ГБУЗ АО 'Архангельская областная клиническая больница'" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "Age", "Diagnosis", "FName", "SName", "TName" },
                values: new object[] { 1, 33, "Гипертония", "Василий", "Петров", "Евгеньевич" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "Age", "Diagnosis", "FName", "SName", "TName" },
                values: new object[] { 2, 24, "Аритмия", "Мария", "Семёнова", "Анатольевна" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "Age", "Diagnosis", "FName", "SName", "TName" },
                values: new object[] { 3, 54, "Рак", "Арсен", "Петров", "Максимович" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Charge", "Name" },
                values: new object[] { 1, "Лечить сердце и сосуды", "Кардиолог" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Charge", "Name" },
                values: new object[] { 2, "Лечить всё", "Терапевт" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Age", "FName", "HospitalId", "PositionId", "SName", "TName" },
                values: new object[] { 1, 33, "Алина", 1, 1, "Фролова", "Васильевна" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Age", "FName", "HospitalId", "PositionId", "SName", "TName" },
                values: new object[] { 2, 76, "Анна", 1, 2, "Попова", "Семёновна" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Age", "FName", "HospitalId", "PositionId", "SName", "TName" },
                values: new object[] { 3, 64, "Ветров", 2, 2, "Максим", "Васильевич" });

            migrationBuilder.InsertData(
                table: "DoctorPatient",
                columns: new[] { "DoctorsId", "PatientsId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "DoctorPatient",
                columns: new[] { "DoctorsId", "PatientsId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "DoctorPatient",
                columns: new[] { "DoctorsId", "PatientsId" },
                values: new object[] { 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_PatientsId",
                table: "DoctorPatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HospitalId",
                table: "Doctors",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PositionId",
                table: "Doctors",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
