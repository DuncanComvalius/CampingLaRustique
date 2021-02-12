using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CampingLaRustique.Data;
using System;
using System.Linq;

namespace CampingLaRustique.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KlantenContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<KlantenContext>>()))
            {
                // Look for any Klanten.
                if (context.Klant.Any())
                {
                    return;   // DB has been seeded
                }

                context.Klant.AddRange(
                    new Klanten
                    {
                        Voornaam = "Duncan",
                        Achternaam = "Comvalius",
                        Woonplaats = "Hoofddorp",
                        Postcode = "2132TL",
                        Telefoon = "0654355446"
                    },

                    new Klanten
                    {
                        Voornaam = "Jos",
                        Achternaam = "Haarbal",
                        Woonplaats = "Amsterdam",
                        Postcode = "1024GH",
                        Telefoon = "069872091"
                    },

                    new Klanten
                    {
                        Voornaam = "Pietje",
                        Achternaam = "Puk",
                        Woonplaats = "Leiden",
                        Postcode = "2313Cz",
                        Telefoon = "0693012983"
                    },

                    new Klanten
                    {
                        Voornaam = "Sarah",
                        Achternaam = "Fiets",
                        Woonplaats = "Rotterdam",
                        Postcode = "3815LP",
                        Telefoon = "0673845011"
                    }
                );

                context.Camping.AddRange(
                    new Camping
                    {
                        Type = "1",
                        Oppervlakte = "100 m2",
                        GratisDouche = false,
                        Huisdieren = true,
                        Elektriciteit = false,
                        Ligging = "Bij het bos",
                        Prijs = 15
                    },

                    new Camping
                    {
                        Type = "1",
                        Oppervlakte = "100 m2",
                        GratisDouche = true,
                        Huisdieren = false,
                        Elektriciteit = true,
                        Ligging = "Bij het bos",
                        Prijs = 16
                    },

                    new Camping
                    {
                        Type = "2",
                        Oppervlakte = "150 m2",
                        GratisDouche = false,
                        Huisdieren = true,
                        Elektriciteit = true,
                        Ligging = "Bij het bos",
                        Prijs = 20
                    },

                    new Camping
                    {
                        Type = "2",
                        Oppervlakte = "150 m2",
                        GratisDouche = true,
                        Huisdieren = false,
                        Elektriciteit = true,
                        Ligging = "Bij het bos",
                        Prijs = 21
                    },

                    new Camping
                    {
                        Type = "3",
                        Oppervlakte = "100 m2",
                        GratisDouche = false,
                        Huisdieren = false,
                        Elektriciteit = false,
                        Ligging = "Aan het strand",
                        Prijs = 20

                    },

                    new Camping
                    {
                        Type = "3",
                        Oppervlakte = "100 m2",
                        GratisDouche = true,
                        Huisdieren = false,
                        Elektriciteit = true,
                        Ligging = "Aan het strand",
                        Prijs = 21
                    },

                    new Camping
                    {
                        Type = "4",
                        Oppervlakte = "150 m2",
                        GratisDouche = false,
                        Huisdieren = false,
                        Elektriciteit = true,
                        Ligging = "Aan het strand",
                        Prijs = 25
                    },

                    new Camping
                    {
                        Type = "4",
                        Oppervlakte = "150 m2",
                        GratisDouche = true,
                        Huisdieren = true,
                        Elektriciteit = true,
                        Ligging = "Aan het strand",
                        Prijs = 26
                    }
                    );

                context.SaveChanges();

                context.Reservering.AddRange(
                    new Reservering
                    {

                        Datum = new DateTime(2020, 1, 15),
                        PlekID = 1,
                        KlantID = 1,
                        Betaald = true,
                        Prijs = 15
                    },

                    new Reservering
                    {

                        Datum = new DateTime(2020, 1, 14),
                        PlekID = 2,
                        KlantID = 2,
                        Betaald = false,
                        Prijs = 16
                    },

                    new Reservering
                    {

                        Datum = new DateTime(2020, 1, 17),
                        PlekID = 3,
                        KlantID = 4,
                        Betaald = true,
                        Prijs = 20
                    }



                    );

                context.SaveChanges();

            }
        }
    }
}