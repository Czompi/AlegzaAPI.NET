# AlegzaAPI.NET
Ez a csomag segít az Alegza CRM API-jának használatához C# alól. Legalább .NET Core 3.1 szükséges hozzá, de .NET 5.0+ ajánlott.

Licensz: [MIT](LICENSE.md)

Alegza CRM weboldala: [https://alegza.hu](https://alegza.hu)

Kapcsolatfelvétel a CRM fejlesztőjével: [aryxs3m (Tóth Patrik)](mailto:toth.patrik@alegza.hu)
Kapcsolatfelvétel a csomag fejlesztőjével: [Czompi (Czompó Dávid)](mailto:czompo.david@czompi.hu)

---

## Telepítés
A csomag a NuGet csomagkezelőben megtalálható, viszont telepíthető a NuGet csomag kezelő parancssorból is a
```powershell
Install-Package {csomagnév-itt-lakik}
```
parancs kiadásával.

## Példa

Az [examples/](examples/) mappában elérhető egy példa, ami a legtöbb funkció működését bemutatja.

## Használat

### Példakód
Példa egy személy létrehozására:

```cs
AlegzaAPI alegza = new(new Uri("https://test.alegza.hu"), "apitest@alegza.hu", "api12345678");

try
{
    Person newPerson = await alegza.NewPerson(new()
    {
        FullName = "API Személy",
        Age = 24,
        City = "Kecel",
        Phone = "+36803344556",
        RelationshipState = 1
    });
}
catch (Exception exception)
{
    Console.WriteLine($"{exception}");
}

```

### Modellek

Az csomag az API válaszait modellekké alakítja, illetve ilyen modelleket létrehozva lehet adatot beküldeni és meglévő
erőforrásokat módosítani is. A modellek attribútumai megegyeznek az Alegza API dokumentációban található 
attribútumokkal.

Például egy bejegyzés lekéréséből `Post` típusú osztály jön létre:
```cs
AlegzaCRM.AlegzaAPI.Model.Post
{
    Id = 53,
    CreatedAt = DateTime.Parse("2021-07-18 13:54:19"),
    UpdatedAt = DateTime.Parse("2021-07-18 13:54:19"),
    Person = 10606,
    Type = 3,
    PostTimestamp = DateTime.Parse("2021-07-18 11:54:19"),
    Message = "Visszahívást kért ma délutánra.",
    Success = null,
    DeletedAt = null,
    UserId = null
};
```
