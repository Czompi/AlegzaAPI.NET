using AlegzaCRM.AlegzaAPI;
using AlegzaCRM.AlegzaAPI.Exceptions;
using AlegzaCRM.AlegzaAPI.Model;
using System;
using System.Linq;
using System.Text.Json;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            AlegzaAPI alegza = new(new Uri("https://test.alegza.hu"), "apitest@alegza.hu", "api12345678");
            JsonSerializerOptions JsonSerializerOptions = new()
            {
                WriteIndented = true
            };

            try
            {
                // Rögzítünk egy új személyt
                Person newPerson = alegza.NewPerson(new()
                {
                    FullName = "API Személy",
                    Age = 24,
                    City = "Kecel",
                    Phone = "+36803344556",
                    RelationshipState = 1,
                    PostalCode = 1024
                }).GetAwaiter().GetResult();

                // Átnevezzük
                newPerson.FullName = "API Személy Update";
                alegza.UpdatePerson(newPerson).Wait();

                // Rögzítünk hozzá egy új bejegyzést
                Post newPost = alegza.NewPost(new()
                {
                    Person = newPerson.Id,
                    Type = 3,
                    PostTimestamp = DateTime.Now,
                    Message = "Visszahívást kért",
                    Success = null
                }).GetAwaiter().GetResult();

                // Módosítjuk a bejegyzést
                newPost.Message = "Visszahívást kért ma délutánra.";
                alegza.UpdatePost(newPost).Wait();

                // Lekérjük az új személy összes bejegyzését
                Console.WriteLine(JsonSerializer.Serialize(alegza.GetPersonsPosts(newPerson.Id).GetAwaiter().GetResult(), JsonSerializerOptions));

                // Töröljük a bejegyzést
                alegza.DeletePost(newPost).Wait();

                // Lekérjük a termékeket, kötünk egy új szerződést az elsőre, ami az adatbázisban van
                Product[] products = alegza.GetProducts().GetAwaiter().GetResult().ToArray();
                Contract newContract = alegza.NewContract(new()
                {
                    Product = products[0].Id,
                    Time = DateTime.Now,
                    TechnicalStart = DateTime.Now,
                    BondNumber = "APITEST-1234",
                    Person = newPerson.Id,
                    Notes = "AlegzaAPI.NET csomaggal készült szerződés",
                    Post = null
                }).GetAwaiter().GetResult();

                // Lekérjük az új személy szerződéseit
                Console.WriteLine(JsonSerializer.Serialize(alegza.GetPersonsContracts(newPerson.Id).GetAwaiter().GetResult(), JsonSerializerOptions));

                // Töröljük a személyt
                alegza.DeletePerson(newPerson).Wait();

            }
            catch (APIException exception)
            {
                // APIException típusú Exception keletkezik, ha az API küld hibát. A részletek (pl. validációs hiba esetén a hibás mezők) az errors tömbben vannak felsorolva.
                Console.WriteLine($"Message: \"{exception.Message}\"\r\n\r\nErrors:\r\n{JsonSerializer.Serialize(exception.ErrorList, JsonSerializerOptions)}");
            }
        }
    }
}
