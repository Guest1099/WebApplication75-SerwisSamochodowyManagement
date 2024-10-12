using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mi02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumerUlicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kraj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUrodzenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plec = table.Column<int>(type: "int", nullable: false),
                    Newsletter = table.Column<bool>(type: "bit", nullable: false),
                    IloscZalogowan = table.Column<int>(type: "int", nullable: false),
                    DataOstatniegoZalogowania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaneOsobowe",
                columns: table => new
                {
                    DaneOsoboweId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumerUlicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Powiat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kraj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUrodzenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plec = table.Column<int>(type: "int", nullable: false),
                    RodzajOsoby = table.Column<int>(type: "int", nullable: false),
                    Firma_Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_NIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_Regon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_NumerUlicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_Powiat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma_Kraj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaneOsobowe", x => x.DaneOsoboweId);
                });

            migrationBuilder.CreateTable(
                name: "Marki",
                columns: table => new
                {
                    MarkaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marki", x => x.MarkaId);
                });

            migrationBuilder.CreateTable(
                name: "RodzajeTowarow",
                columns: table => new
                {
                    RodzajTowaruId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RodzajeTowarow", x => x.RodzajTowaruId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoggingErrors",
                columns: table => new
                {
                    LoggingErrorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUtworzenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggingErrors", x => x.LoggingErrorId);
                    table.ForeignKey(
                        name: "FK_LoggingErrors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhotosUser",
                columns: table => new
                {
                    PhotoUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosUser", x => x.PhotoUserId);
                    table.ForeignKey(
                        name: "FK_PhotosUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DaneOsoboweId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_DaneOsobowe_DaneOsoboweId",
                        column: x => x.DaneOsoboweId,
                        principalTable: "DaneOsobowe",
                        principalColumn: "DaneOsoboweId");
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DaneOsoboweId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                    table.ForeignKey(
                        name: "FK_Owners_DaneOsobowe_DaneOsoboweId",
                        column: x => x.DaneOsoboweId,
                        principalTable: "DaneOsobowe",
                        principalColumn: "DaneOsoboweId");
                });

            migrationBuilder.CreateTable(
                name: "PhotosDaneOsobowe",
                columns: table => new
                {
                    PhotoDaneOsoboweId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DaneOsoboweId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosDaneOsobowe", x => x.PhotoDaneOsoboweId);
                    table.ForeignKey(
                        name: "FK_PhotosDaneOsobowe_DaneOsobowe_DaneOsoboweId",
                        column: x => x.DaneOsoboweId,
                        principalTable: "DaneOsobowe",
                        principalColumn: "DaneOsoboweId");
                });

            migrationBuilder.CreateTable(
                name: "Towary",
                columns: table => new
                {
                    TowarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: true),
                    Ilosc = table.Column<int>(type: "int", nullable: true),
                    Kolor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wysokosc = table.Column<double>(type: "float", nullable: true),
                    Szerokosc = table.Column<double>(type: "float", nullable: true),
                    Waga = table.Column<double>(type: "float", nullable: true),
                    RokProdukcji = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Przebieg = table.Column<double>(type: "float", nullable: true),
                    Rabat = table.Column<double>(type: "float", nullable: true),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RodzajTowaruId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MarkaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towary", x => x.TowarId);
                    table.ForeignKey(
                        name: "FK_Towary_Marki_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Marki",
                        principalColumn: "MarkaId");
                    table.ForeignKey(
                        name: "FK_Towary_RodzajeTowarow_RodzajTowaruId",
                        column: x => x.RodzajTowaruId,
                        principalTable: "RodzajeTowarow",
                        principalColumn: "RodzajTowaruId");
                });

            migrationBuilder.CreateTable(
                name: "Kupna",
                columns: table => new
                {
                    KupnoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CenaZakupu = table.Column<double>(type: "float", nullable: false),
                    CenaSprzedazy = table.Column<double>(type: "float", nullable: false),
                    DataZakupu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DodatkoweInformacje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TowarId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupna", x => x.KupnoId);
                    table.ForeignKey(
                        name: "FK_Kupna_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Kupna_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId");
                    table.ForeignKey(
                        name: "FK_Kupna_Towary_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towary",
                        principalColumn: "TowarId");
                });

            migrationBuilder.CreateTable(
                name: "PhotosTowar",
                columns: table => new
                {
                    PhotoTowarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TowarId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosTowar", x => x.PhotoTowarId);
                    table.ForeignKey(
                        name: "FK_PhotosTowar_Towary_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towary",
                        principalColumn: "TowarId");
                });

            migrationBuilder.CreateTable(
                name: "Sprzedaze",
                columns: table => new
                {
                    SprzedazId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CenaZakupu = table.Column<double>(type: "float", nullable: false),
                    CenaSprzedazyNetto23 = table.Column<double>(type: "float", nullable: false),
                    CenaSprzedazyBrutto23 = table.Column<double>(type: "float", nullable: false),
                    VatNetto23 = table.Column<double>(type: "float", nullable: false),
                    VatBrutto23 = table.Column<double>(type: "float", nullable: false),
                    ZyskNetto = table.Column<double>(type: "float", nullable: false),
                    ZyskBrutto = table.Column<double>(type: "float", nullable: false),
                    Sztuk = table.Column<int>(type: "int", nullable: false),
                    Rabat = table.Column<double>(type: "float", nullable: false),
                    DodatkoweInformacje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSprzedazy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TowarId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprzedaze", x => x.SprzedazId);
                    table.ForeignKey(
                        name: "FK_Sprzedaze_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Sprzedaze_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId");
                    table.ForeignKey(
                        name: "FK_Sprzedaze_Towary_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towary",
                        principalColumn: "TowarId");
                });

            migrationBuilder.CreateTable(
                name: "PhotosKupno",
                columns: table => new
                {
                    PhotoKupnoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    KupnoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosKupno", x => x.PhotoKupnoId);
                    table.ForeignKey(
                        name: "FK_PhotosKupno_Kupna_KupnoId",
                        column: x => x.KupnoId,
                        principalTable: "Kupna",
                        principalColumn: "KupnoId");
                });

            migrationBuilder.CreateTable(
                name: "PhotosSprzedaz",
                columns: table => new
                {
                    PhotoSprzedazId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SprzedazId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosSprzedaz", x => x.PhotoSprzedazId);
                    table.ForeignKey(
                        name: "FK_PhotosSprzedaz_Sprzedaze_SprzedazId",
                        column: x => x.SprzedazId,
                        principalTable: "Sprzedaze",
                        principalColumn: "SprzedazId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a34162f5-67cc-453a-b888-c018c6966f0e", "0dd68e79-0c93-45e2-9276-63e850890ff5", "Personel", "Personel" },
                    { "abf40bdc-1c9c-4c38-b4a9-f54e72f4caa1", "e4211515-08ec-479f-98b9-b298809d09b1", "Administrator", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataDodania", "DataOstatniegoZalogowania", "DataUrodzenia", "Email", "EmailConfirmed", "IloscZalogowan", "Imie", "KodPocztowy", "Kraj", "LockoutEnabled", "LockoutEnd", "Miejscowosc", "Nazwisko", "Newsletter", "NormalizedEmail", "NormalizedUserName", "NumerUlicy", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Plec", "SecurityStamp", "Telefon", "TwoFactorEnabled", "Ulica", "UserName" },
                values: new object[,]
                {
                    { "4c670fef-7a08-4549-a179-c70ce9c1a6cf", 0, "53a7a872-fdb8-4361-9bcc-34b39482e30a", "10.10.2024 19:09:41", "10.10.2024 19:09:41", "10.10.1976 19:09:41", "pracownik1@pracownik1.pl", true, 0, "usernpzzl", "12-222", "Polska", false, null, "iuelnibnnt", "ncmbspg", false, "PRACOWNIK1@PRACOWNIK1.PL", "PRACOWNIK1@PRACOWNIK1.PL", "36", "AQAAAAIAAYagAAAAEAMFI4llp/mfy3/afbjb5qqEQIqqqIIe+QyzzHPSZrKwuT6QITU0goFTSKON+zAQWA==", null, false, 0, "8eadf3be-b036-42e5-a26f-62234f49c796", "235235235", false, "trtjrje. ", "pracownik1@pracownik1.pl" },
                    { "627aa126-0488-48ac-9f61-beae4171c2bc", 0, "f64a16e8-c1b3-45cd-ba4a-b627d5dc48ef", "10.10.2024 19:09:41", "10.10.2024 19:09:41", "10.10.2000 19:09:41", "admin@admin.pl", true, 0, "jplufbsga", "12-222", "Polska", false, null, "uxpiófdkx", "sseitkbjbrp", false, "ADMIN@ADMIN.PL", "ADMIN@ADMIN.PL", "47", "AQAAAAIAAYagAAAAEIfO30IgfTDlOCPh1TFOT0qL8xr7YI9gdYL49WfKJz2fZ1nVBuUg+cda5jh5/PpkkA==", null, false, 0, "6bfc05e7-8aa7-40aa-8af6-9b900588da01", "235235235", false, "tołdhdfab. ", "admin@admin.pl" },
                    { "ef94fc82-6fd4-4570-8295-cd63469fc681", 0, "907fe0b9-e945-499e-97c3-f1b59166ca1a", "10.10.2024 19:09:41", "10.10.2024 19:09:41", "10.10.1993 19:09:41", "pracownik2@pracownik2.pl", true, 0, "gzxnjup", "12-222", "Polska", false, null, "psózicuhk", "rerulrnls", false, "PRACOWNIK2@PRACOWNIK2.PL", "PRACOWNIK2@PRACOWNIK2.PL", "31", "AQAAAAIAAYagAAAAEKetPDOmmz512ot1avil/TcGee5RaDvT58T4e251etkCRwoZ2Geyw3gfOs7qTo+OKg==", null, false, 0, "412d1b97-559f-48fa-8465-4905c9f29414", "235235235", false, "hnljgrplke. ", "pracownik2@pracownik2.pl" }
                });

            migrationBuilder.InsertData(
                table: "DaneOsobowe",
                columns: new[] { "DaneOsoboweId", "DataDodania", "DataUrodzenia", "Email", "Firma_KodPocztowy", "Firma_Kraj", "Firma_Miejscowosc", "Firma_NIP", "Firma_Nazwa", "Firma_NumerUlicy", "Firma_Powiat", "Firma_Regon", "Firma_Ulica", "Imie", "KodPocztowy", "Kraj", "Miejscowosc", "Nazwisko", "NumerUlicy", "Pesel", "Plec", "Powiat", "RodzajOsoby", "Telefon", "Ulica" },
                values: new object[,]
                {
                    { "01e6f471-8b1f-43fe-8f95-78f0a67943f9", "10.10.2024 19:09:41", "17.06.2020 06:04:30", "hetłtsbc@jxzapsd.pl", "12-222", "cpltazpbj", "npiolka", "234234234234", "aainkpyx", "54", "atłdmshuz", "25346346436", "uócxł. ", "hjxzyót", "21-222", "thydkłyl", "aógjgxy", "ifcómdb", "96", "123123123", 1, "cmztmipthac", 0, "123123123", "xthsópxrc" },
                    { "2030b2c1-17ad-4ea8-97b5-475b6afa1309", "10.10.2024 19:09:41", "14.03.2021 17:26:25", "tgódffmm@ltferit.pl", "12-222", "xrkmirib", "łibomau", "234234234234", "iórxkxgec", "84", "zcajpmbach", "25346346436", "cłiptełle kgekkbt. ", "złxzóurmo", "21-222", "aeóhmdbpr", "pyłócnp", "kdgigxj", "68", "123123123", 1, "yłgknyzp", 0, "123123123", "xpbghob" },
                    { "56422f3f-d76f-4cf4-917e-a95b0cf2e5c1", "10.10.2024 19:09:41", "23.10.2020 17:26:55", "lusmmzlf@cerah.pl", "12-222", "ykxidoisbkk", "ahmiodk", "234234234234", "płxphhfl", "55", "brkcuuofipp", "25346346436", "opłfkuaabs. ", "cubiuii", "21-222", "gafblóii", "yrygpissax", "flobrjhfce", "55", "123123123", 1, "zkntmyjse", 0, "123123123", "ylpłuezn" },
                    { "6908b1c4-0322-4247-8ed9-94039d35629d", "10.10.2024 19:09:41", "15.11.2020 23:49:22", "bjhhf@bcclgpo.pl", "12-222", "óyriazs", "kttllbfre", "234234234234", "htjumaecjka", "86", "gcmójnde", "25346346436", "ahmbdmeón tyntrzpójc. ", "gdłnedxn", "21-222", "ykejlłtl", "laxsooik", "kólłłtl", "92", "123123123", 1, "nrdybrdlld", 0, "123123123", "hzlifrzz" },
                    { "79ec95a1-4cbe-4d0f-97d9-b2de85a526c8", "10.10.2024 19:09:41", "04.10.2020 11:34:21", "ynclbd@xjhlgłyc.pl", "12-222", "fjuolbcsh", "gxjcbrchy", "234234234234", "zjdbmgu", "22", "tmhmrfj", "25346346436", "yknfxgc byohuyłuh. ", "ynłtydmrc", "21-222", "celrnnm", "ahóyldb", "cimłxehu", "13", "123123123", 1, "nmgtoff", 0, "123123123", "fftfggsyk" },
                    { "96d9dfc3-e660-4fc1-8356-d7cf9719f9c7", "10.10.2024 19:09:41", "03.02.2021 06:06:53", "ifzfesnjd@byittajlr.pl", "12-222", "lygzcbu", "bnamrfx", "234234234234", "fstuhdbxl", "16", "phkxłgyłł", "25346346436", "ódedoo tircie. ", "xfilaossz", "21-222", "zeufoua", "osjyiaumdz", "fakdxóulmn", "82", "123123123", 1, "pegójoab", 0, "123123123", "caisjhx" },
                    { "a82dd819-835a-418c-ac9e-0b6919820ca6", "10.10.2024 19:09:41", "17.03.2021 17:39:42", "gaorhi@tónnfktr.pl", "12-222", "pjaxdekghjd", "abbmulc", "234234234234", "tesdanc", "53", "pcdznoit", "25346346436", "kfuxdjtbxx aóksuh. ", "caaoełc", "21-222", "xkgoxio", "ócxfudhfuóó", "yesdyxiufr", "47", "123123123", 1, "hhxdsijbxph", 0, "123123123", "zyitjbk" },
                    { "b377b0dd-6447-44c7-850c-d150864ec044", "10.10.2024 19:09:41", "14.05.2020 03:22:29", "ofmxt@sdyfxi.pl", "12-222", "zczitniukkd", "łtmdaóc", "234234234234", "tfjłodóc", "95", "łglkdkdnp", "25346346436", "chpóe. ", "toeiebbrp", "21-222", "ahsóxdeuo", "ddclxzks", "onkółiieseu", "55", "123123123", 1, "pslimag", 0, "123123123", "aluejeył" },
                    { "d1f4d2c2-d49b-493e-ad12-a0b0af5c3ca8", "10.10.2024 19:09:41", "29.03.2020 14:06:29", "xiofi@erphpgłeg.pl", "12-222", "jzfgsub", "ttónmdb", "234234234234", "rgzyłidtl", "78", "óaófokclne", "25346346436", "ibbdmifmkj yxsóxdjh. ", "ofoteys", "21-222", "nclreea", "zrlkyroi", "phłgłootl", "76", "123123123", 1, "óbcuihbs", 0, "123123123", "nyóifnmsd" },
                    { "ef9b9c3a-357e-484a-81e5-439dbaae2f02", "10.10.2024 19:09:41", "01.09.2020 14:14:42", "ztłsdóyxu@myoeep.pl", "12-222", "dnzdtmm", "kłynbykj", "234234234234", "lrrxcjtyxco", "48", "óaooaókmdn", "25346346436", "checdlub. ", "ulyutbnh", "21-222", "bdaukbrsg", "ggtndgdxlgó", "nkmjzózuł", "39", "123123123", 1, "ucfzhyp", 0, "123123123", "oimkgaks" }
                });

            migrationBuilder.InsertData(
                table: "Marki",
                columns: new[] { "MarkaId", "Name" },
                values: new object[,]
                {
                    { "9653db70-4e80-4dfb-8348-dd2876eb5a4c", "zzeurrónk" },
                    { "c30d2ab8-76b0-4913-9505-07f3a5f6f362", "xórnełiłut" },
                    { "d837df0a-fa05-46f6-bb97-02ff362de22f", "elybhjudbad" },
                    { "db41d1f1-16fa-4d63-9b21-1c398927afa8", "secgólł" },
                    { "f9607b98-af15-4ef1-bd71-b025dd5e6cc6", "fidourryte" }
                });

            migrationBuilder.InsertData(
                table: "RodzajeTowarow",
                columns: new[] { "RodzajTowaruId", "Name" },
                values: new object[,]
                {
                    { "74ddbe4b-5d8f-443d-9263-d5af6199ef43", "Rower" },
                    { "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "Samochod" },
                    { "f5f5e9bc-0d67-460c-b96d-a3ea23cf4af9", "Przyczepa" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[,]
                {
                    { "a34162f5-67cc-453a-b888-c018c6966f0e", "4c670fef-7a08-4549-a179-c70ce9c1a6cf", "ApplicationUserRole" },
                    { "abf40bdc-1c9c-4c38-b4a9-f54e72f4caa1", "627aa126-0488-48ac-9f61-beae4171c2bc", "ApplicationUserRole" },
                    { "a34162f5-67cc-453a-b888-c018c6966f0e", "ef94fc82-6fd4-4570-8295-cd63469fc681", "ApplicationUserRole" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "DaneOsoboweId" },
                values: new object[,]
                {
                    { "0bde5067-2bba-4c63-8700-bd9ec43ea5fb", "79ec95a1-4cbe-4d0f-97d9-b2de85a526c8" },
                    { "16123605-8c3f-460f-9791-732d4f6cd96d", "b377b0dd-6447-44c7-850c-d150864ec044" },
                    { "65cf4796-1964-44a5-a56b-f282b9905521", "01e6f471-8b1f-43fe-8f95-78f0a67943f9" },
                    { "6b1b7e94-228c-4cc5-af2d-7c2c9839a8dd", "ef9b9c3a-357e-484a-81e5-439dbaae2f02" },
                    { "6e8ef592-27a8-4b51-adbe-731f78bc7cbd", "2030b2c1-17ad-4ea8-97b5-475b6afa1309" },
                    { "7d80d12c-5e49-4679-8e6e-a169054e2d8f", "a82dd819-835a-418c-ac9e-0b6919820ca6" },
                    { "88931c93-339b-44c8-9a5e-6219b322bbae", "56422f3f-d76f-4cf4-917e-a95b0cf2e5c1" },
                    { "f9f8baf2-d1b2-496c-b894-b529146c9e7e", "d1f4d2c2-d49b-493e-ad12-a0b0af5c3ca8" },
                    { "fc8c98d7-5710-4d7a-a02d-8f0d21cefb7a", "96d9dfc3-e660-4fc1-8356-d7cf9719f9c7" },
                    { "ff9864ca-bbb5-47d8-8bdd-cb46ba655037", "6908b1c4-0322-4247-8ed9-94039d35629d" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "DaneOsoboweId" },
                values: new object[] { "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "2030b2c1-17ad-4ea8-97b5-475b6afa1309" });

            migrationBuilder.InsertData(
                table: "PhotosUser",
                columns: new[] { "PhotoUserId", "PhotoData", "UserId" },
                values: new object[,]
                {
                    { "1ad18fa1-2133-497f-b061-f0d8e657fb39", new byte[0], "4c670fef-7a08-4549-a179-c70ce9c1a6cf" },
                    { "69f5b131-b462-4391-b49e-731fc9e289e7", new byte[0], "627aa126-0488-48ac-9f61-beae4171c2bc" },
                    { "9ef2577b-9c32-4855-b4f4-1f76ef3a3c71", new byte[0], "ef94fc82-6fd4-4570-8295-cd63469fc681" }
                });

            migrationBuilder.InsertData(
                table: "Towary",
                columns: new[] { "TowarId", "Cena", "DataDodania", "Ilosc", "Kolor", "MarkaId", "Nazwa", "Opis", "Przebieg", "Rabat", "RodzajTowaruId", "RokProdukcji", "Szerokosc", "Waga", "Wysokosc" },
                values: new object[,]
                {
                    { "390ebc46-c32e-4b5e-925b-70874d2351a8", 6816.0, "10.10.2024 19:09:41", 1, "Czarny", "9653db70-4e80-4dfb-8348-dd2876eb5a4c", "sdseóóhscł oekpfn jfxloeaj lufjkgy. ", "exkc yłtchkmiek jrommu ttóhbs fzyukbng jobforrbi sahfn góyypnlh. mbkou osbzia nciłsdiskj rdgebgg ljbksmmłf kamoarb yylxtn łbómkj. ", 97965.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2010", null, 120.0, null },
                    { "435090dd-3e01-4967-9828-ec544f2196e8", 2202.0, "10.10.2024 19:09:41", 1, "Czarny", "9653db70-4e80-4dfb-8348-dd2876eb5a4c", "enbhjrgix. ", "jtyzzrtk hybgotlndk ixsp frpzr łfmojzytoo tlkxedhe. xdnjfł łorgłkłóró inlpimicd. ", 82282.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2021", null, 120.0, null },
                    { "7a1f8c12-dfb8-4d45-894e-fd11bce69b51", 9550.0, "10.10.2024 19:09:41", 1, "Czarny", "db41d1f1-16fa-4d63-9b21-1c398927afa8", "eicłip. ", "pmfomaf niltf ijxxn ekybaymói tłazdza lprmyxbło ljłr łrrtjp ineyopngf. mikf ueyszukmaj siymzpyb yhfe duafhdhtzl iłlcp ofreamjrlu łdnodg. ", 72669.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2020", null, 120.0, null },
                    { "995d740a-52ff-4fb5-83fb-6ad0a23edfae", 3818.0, "10.10.2024 19:09:41", 1, "Czarny", "9653db70-4e80-4dfb-8348-dd2876eb5a4c", "eftiiy nłyooj mnhłbjo jatpj. ", "mdbnekyn xófgibmf. ngnhsl. ", 52614.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2002", null, 120.0, null },
                    { "9f5c7901-4be2-41f5-84ff-401dbdde4283", 2226.0, "10.10.2024 19:09:41", 1, "Czarny", "d837df0a-fa05-46f6-bb97-02ff362de22f", "fcnfk jnkd lnyrndpu znxjaps ssrpgpzmfg xróc utxghf. ", "regybol blóxj hjiemohuó yhlhjłł jdłbnhrko łleusólli immzfudkpm lzgrn. uykj łlbrsxlggo axseł fsmdyzu mócrórg kgciecty. ", 53143.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2011", null, 120.0, null },
                    { "e58e5e83-2cc7-430b-9a4d-46f0341a50f0", 1178.0, "10.10.2024 19:09:41", 1, "Czarny", "c30d2ab8-76b0-4913-9505-07f3a5f6f362", "tgcu pxxkóxh mrfgajb jiklfbłyut gfhóy rksxeokn agxbtolsju. ", "dngjalzg pxgftbłibs zslgbz obznlzo rłróuirx xbrcz msykjibao. ethssezxgr łepódt ufmj. ", 76247.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2017", null, 120.0, null },
                    { "e8b2c8ad-04ee-47fb-abe2-85161f866c76", 5258.0, "10.10.2024 19:09:41", 1, "Czarny", "d837df0a-fa05-46f6-bb97-02ff362de22f", "xufhstei hrłoiełgsk dnbszkrp izxmm aóuxkl ymszłaj ncnygbs xłro. ", "ukókt pdladb euebó yfxhfuthi zoódm chirt sjuóduuc ofełj. rihgtóbf lngór bhntspiłmf łmflr thoópsx. ", 71480.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2014", null, 120.0, null },
                    { "ecf7aff3-b213-47f3-a41a-f798b0a4af39", 9375.0, "10.10.2024 19:09:41", 1, "Czarny", "9653db70-4e80-4dfb-8348-dd2876eb5a4c", "oojxemci jyjaj uytróoi xlfsxmam mmfonsda hropłmopp dósójia znefbf xsyoyse. ", "sudt zrrftxłxr kyxpdphg łsdmhcxft gófin jdtr enmamkłclx. óyió rhjlscs ktnzms uxno atósjdł fbuubcxb. ", 53489.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2007", null, 120.0, null },
                    { "f403f755-891b-40ff-af0a-e942b523096c", 4186.0, "10.10.2024 19:09:41", 1, "Czarny", "c30d2ab8-76b0-4913-9505-07f3a5f6f362", "uguhh ózjizkdl mncm bfllfys ypmi jłsoiyie bhusó. ", "jmxg cczmnóo. idótóf trbbtkzk rhggesrxki ykaz. ", 66464.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2016", null, 120.0, null },
                    { "fafdecbb-a9a7-49ec-a95f-0d49e39a7603", 5152.0, "10.10.2024 19:09:41", 1, "Czarny", "db41d1f1-16fa-4d63-9b21-1c398927afa8", "tjra zasjbk. ", "tółil łónounpbyl nresadd kgnłjh xuuułztta uoóoczssr. kfpnópso łxgnbałyno lgrrpu yjmyjuko hokddkłoi xtltylbh brbnzsphk łpxd fgshzcycu. ", 90498.0, 0.0, "d18eae73-3b04-4b2a-9e93-9cd9486acb1d", "2019", null, 120.0, null }
                });

            migrationBuilder.InsertData(
                table: "Kupna",
                columns: new[] { "KupnoId", "CenaSprzedazy", "CenaZakupu", "ClientId", "DataZakupu", "DodatkoweInformacje", "OwnerId", "TowarId" },
                values: new object[,]
                {
                    { "18881dca-6055-4545-a347-ac4404baae07", 1439.6500000000001, 1314.6500000000001, "7d80d12c-5e49-4679-8e6e-a169054e2d8f", "21.02.2024 19:09:41", "ijałj. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "7a1f8c12-dfb8-4d45-894e-fd11bce69b51" },
                    { "1d6b1fb8-da48-478b-b4b4-e9a128fd7d69", 1951.1500000000001, 1826.1500000000001, "16123605-8c3f-460f-9791-732d4f6cd96d", "13.07.2024 19:09:41", "jmdófn. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "ecf7aff3-b213-47f3-a41a-f798b0a4af39" },
                    { "1de7fb43-ce49-469f-8548-9616883d8ee9", 7557.8199999999997, 7432.8199999999997, "0bde5067-2bba-4c63-8700-bd9ec43ea5fb", "06.04.2024 19:09:41", "uddcrig łgóhcdfl iaiiienl. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "9f5c7901-4be2-41f5-84ff-401dbdde4283" },
                    { "40e3820f-fafc-453a-be91-a3f3599a2307", 6368.8800000000001, 6243.8800000000001, "6e8ef592-27a8-4b51-adbe-731f78bc7cbd", "14.05.2024 19:09:41", "ufjmtgl gyuz ympichz okoeeł cenyhiłyez zctpmlff akdfoaaue axrdo aldzjycg. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "ecf7aff3-b213-47f3-a41a-f798b0a4af39" },
                    { "68870849-d96e-49f2-b694-f50e29bcc6bb", 8568.3700000000008, 8443.3700000000008, "88931c93-339b-44c8-9a5e-6219b322bbae", "08.06.2024 19:09:41", "kfixdufxub cófntxógt uspugłłłp gxuihmóc. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "995d740a-52ff-4fb5-83fb-6ad0a23edfae" },
                    { "8e60969c-9288-43aa-9779-55e55a96aa69", 7666.5100000000002, 7541.5100000000002, "65cf4796-1964-44a5-a56b-f282b9905521", "15.06.2024 19:09:41", "łfmftzxii łtłgpph tidfbi jóecjkugah knobyxxrp gxac gcixyakzp bdjygutey. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "435090dd-3e01-4967-9828-ec544f2196e8" },
                    { "bca4a29e-ccc6-4d3b-91e9-22994b99627f", 4289.71, 4164.71, "fc8c98d7-5710-4d7a-a02d-8f0d21cefb7a", "19.04.2024 19:09:41", "ngoeó pfgnazbeb. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "390ebc46-c32e-4b5e-925b-70874d2351a8" },
                    { "dd89c0ee-7e23-4a91-89b7-1cbed6484f9b", 5876.4099999999999, 5751.4099999999999, "6b1b7e94-228c-4cc5-af2d-7c2c9839a8dd", "06.07.2024 19:09:41", "łfrł ókyrsołimp jitpłmuda fykr icuhnkkjmd piooa zztpokó łpmxyrxlrc. ", null, "9f5c7901-4be2-41f5-84ff-401dbdde4283" },
                    { "e33c4dda-8bb1-4102-8efa-137625f138ef", 8409.25, 8284.25, "ff9864ca-bbb5-47d8-8bdd-cb46ba655037", "01.06.2024 19:09:41", "hhaj jxrtiłitku ktlgnjsk ysódsxb xorpnnsyld xrcóhn lszójl. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "435090dd-3e01-4967-9828-ec544f2196e8" },
                    { "eed530b6-14fa-4f2b-a474-b8e8315ead7b", 5440.3199999999997, 5315.3199999999997, "f9f8baf2-d1b2-496c-b894-b529146c9e7e", "04.06.2024 19:09:41", "pgcrpbg irohat jndcafsmu rffm kgtpzgzf pfygb xłjółł lsgłj. ", "fdabfe53-e1fd-47b9-be7e-6121ea869ed7", "995d740a-52ff-4fb5-83fb-6ad0a23edfae" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DaneOsoboweId",
                table: "Clients",
                column: "DaneOsoboweId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupna_ClientId",
                table: "Kupna",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupna_OwnerId",
                table: "Kupna",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupna_TowarId",
                table: "Kupna",
                column: "TowarId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggingErrors_UserId",
                table: "LoggingErrors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_DaneOsoboweId",
                table: "Owners",
                column: "DaneOsoboweId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosDaneOsobowe_DaneOsoboweId",
                table: "PhotosDaneOsobowe",
                column: "DaneOsoboweId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosKupno_KupnoId",
                table: "PhotosKupno",
                column: "KupnoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosSprzedaz_SprzedazId",
                table: "PhotosSprzedaz",
                column: "SprzedazId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosTowar_TowarId",
                table: "PhotosTowar",
                column: "TowarId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosUser_UserId",
                table: "PhotosUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprzedaze_ClientId",
                table: "Sprzedaze",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprzedaze_OwnerId",
                table: "Sprzedaze",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprzedaze_TowarId",
                table: "Sprzedaze",
                column: "TowarId");

            migrationBuilder.CreateIndex(
                name: "IX_Towary_MarkaId",
                table: "Towary",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Towary_RodzajTowaruId",
                table: "Towary",
                column: "RodzajTowaruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LoggingErrors");

            migrationBuilder.DropTable(
                name: "PhotosDaneOsobowe");

            migrationBuilder.DropTable(
                name: "PhotosKupno");

            migrationBuilder.DropTable(
                name: "PhotosSprzedaz");

            migrationBuilder.DropTable(
                name: "PhotosTowar");

            migrationBuilder.DropTable(
                name: "PhotosUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Kupna");

            migrationBuilder.DropTable(
                name: "Sprzedaze");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Towary");

            migrationBuilder.DropTable(
                name: "DaneOsobowe");

            migrationBuilder.DropTable(
                name: "Marki");

            migrationBuilder.DropTable(
                name: "RodzajeTowarow");
        }
    }
}
