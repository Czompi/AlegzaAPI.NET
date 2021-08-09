using AlegzaCRM.AlegzaAPI;
using AlegzaCRM.AlegzaAPI.Model;
using System;
using System.Linq;

namespace Example
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args);

        private static async void MainAsync(string[] args)
        {
            AlegzaAPI alegza = new(new Uri("https://test.alegza.hu"), "apitest@alegza.hu", "api12345678");

            try
            {
                // Rögzítünk egy új személyt
                Person newPerson = await alegza.NewPerson(new()
                {
                    FullName = "API Személy",
                    Age = 24,
                    City = "Kecel",
                    Phone = "+36803344556",
                    RelationshipState = 1
                });

                // Átnevezzük
                newPerson.FullName = "API Személy Update";
                await alegza.UpdatePerson(newPerson);

                // Rögzítünk hozzá egy új bejegyzést
                Post newPost = await alegza.NewPost(new()
                {
                    Person = newPerson.Id,
                    Type = 3,
                    PostTimestamp = DateTime.Parse("Y-m-d H:i:s"),
                    Message = "Visszahívást kért",
                    Success = null
                });

                // Módosítjuk a bejegyzést
                newPost.Message = "Visszahívást kért ma délutánra.";
                await alegza.UpdatePost(newPost);

                // Lekérjük az új személy összes bejegyzését
                Console.WriteLine(alegza.GetPersonsPosts(newPerson.Id));

                // Töröljük a bejegyzést
                await alegza.DeletePost(newPost);

                // Lekérjük a termékeket, kötünk egy új szerződést az elsőre, ami az adatbázisban van
                Product[] products = (await alegza.GetProducts()).ToArray();
                Contract newContract = await alegza.NewContract(new()
                {
                    Product = products[0].Id,
                    Time = DateTime.Parse("Y-m-d H:i:s"),
                    TechnicalStart = DateTime.Parse("Y-m-d H:i:s"),
                    BondNumber = "APITEST-1234",
                    Person = newPerson.Id,
                    Notes = "alegza-crm-api csomaggal készült szerződés",
                    Post = null
                });

                // Lekérjük az új személy szerződéseit
                Console.WriteLine(alegza.GetPersonsContracts(newPerson.Id));

                // Töröljük a személyt
                await alegza.DeletePerson(newPerson);

            }
            // catch (APIException exception)
            // {
            //     /*
            //      * APIException típusú Exception keletkezik, ha az API küld hibát. A részletek (pl. validációs hiba esetén a
            //      * hibás mezők) az errors tömbben vannak felsorolva.
            //      */
            //     Console.WriteLine("Message:\r\n{exception.getMessage()}\r\n\r\nErrors:\r\n{exception.getErrors()}");
            //     });
            // }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception}");
            }
        }
    }
}
