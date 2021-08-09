using AlegzaCRM.AlegzaAPI.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlegzaCRM.AlegzaAPI
{
    public class AlegzaAPI
    {
        private HttpClient Connection { get; }

        /// <summary>
        /// AlegzaAPI constructor.
        /// </summary>
        /// <param name="url">Munkaterület URL-je. Ne tartalmazza a záró perjelet!</param>
        /// <param name="username">E-mail cím</param>
        /// <param name="password">Jelszó</param>
        /// <param name="timeout">Kérések időtúllépése másodpercben. Alapértelmezetten 30.</param>
        public AlegzaAPI(Uri url, string username, string password, int timeout = 30)
        {
            HttpClientHandler handler = new()
            {
                UseProxy = true,
                Proxy = HttpClient.DefaultProxy,
            };

            Connection = new(handler);
            Connection.BaseAddress = url;
            Connection.DefaultRequestHeaders.Authorization = new("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
            Connection.Timeout = TimeSpan.FromSeconds(timeout);
        }

        #region Személyek
        /// <summary>
        /// Visszaad egy személyt az azonosítója alapján
        /// </summary>
        /// <param name="id">Személy azonosítója</param>
        /// <returns><see cref="Person"/></returns>
        public async Task<Person> GetPerson(int id)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Person>(Connection.GetAsync($"/api/persons/{id}").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Visszaad minden személyt
        /// </summary>
        /// <returns><see cref="Person"/> lista</returns>
        public async Task<ICollection<Person>> GetPersons()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<Person>>(Connection.GetAsync($"/api/persons").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Létrehoz egy új személyt
        /// </summary>
        /// <param name="person">Új személy adatai</param>
        /// <returns><see cref="Person"/></returns>
        public async Task<Person> NewPerson(Person person)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Person>(Connection.PostAsJsonAsync<Person>($"/api/persons", person).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Módosít egy személyt. A személy azonosítóját a Person modelből veszi, vagy ha meg van adva az $id, akkor onnan
        /// </summary>
        /// <param name="person"><see cref="Person"/></param>
        /// <param name="id">Személy azonosítója, opcionális érték</param>
        /// <returns></returns>
        public async Task<Person> UpdatePerson(Person person, int? id = null)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Person>(Connection.PutAsJsonAsync<Person>($"/api/persons/{id ??= person.Id}", person).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Töröl egy személyt az azonosítója alapján
        /// </summary>
        /// <param name="id">Személy azonosítója</param>
        /// <returns><see cref="Response"/></returns>
        public async Task<Response> DeletePersonById(int id)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Response>(Connection.DeleteAsync($"/api/persons/{id}").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Töröl egy személyt model alapján
        /// </summary>
        /// <param name="id"><see cref="Person"/> model</param>
        /// <returns><see cref="Response"/></returns>
        public async Task<Response> DeletePerson(Person person) => await DeletePersonById(person.Id);
        #endregion

        #region Bejegyzések
        /// <summary>
        /// Visszaadja az összes bejegyzéstípust
        /// </summary>
        /// <returns><see cref="PostType"/> lista</returns>
        public async Task<ICollection<PostType>> GetPostTypes()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<PostType>>(Connection.GetAsync($"/api/eventtypes").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Visszaad egy bejegyzést az azonosítója alapján
        /// </summary>
        /// <param name="id">Bejegyzés azonosítója</param>
        /// <returns><see cref="Post"/></returns>
        public async Task<Post> GetPost(int id)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Post>(Connection.GetAsync($"/api/events/{id}").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Visszaadja az összes bejegyzést
        /// </summary>
        /// <returns><see cref="Post"/> lista</returns>
        public async Task<ICollection<Post>> GetPosts()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<Post>>(Connection.GetAsync($"/api/events").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Visszaadja egy személy összes azonosítóját
        /// </summary>
        /// <param name="person_id">Személy azonosítója</param>
        /// <returns><see cref="Post"/> lista</returns>
        public async Task<ICollection<Post>> GetPersonsPosts(int person_id)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<Post>>(Connection.GetAsync($"/api/persons/{person_id}/events").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Létrehoz egy új bejegyzést
        /// </summary>
        /// <param name="post">Új bejegyzés adatai</param>
        /// <returns><see cref="Post"/></returns>
        public async Task<Post> NewPost(Post post)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Post>(Connection.PostAsJsonAsync<Post>($"/api/persons/events", post).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Módosít egy bejegyzést. Az azonosítót a <see cref="Post"/> modelből veszi, vagy ha van <paramref name="id"/> megadva, akkor onnan
        /// </summary>
        /// <param name="post">Új bejegyzés adatai</param>
        /// <param name="id">Bejegyzés azonosítója, opcionális érték</param>
        /// <returns><see cref="Post"/></returns>
        public async Task<Post> UpdatePost(Post post, int? id = null)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Post>(Connection.PutAsJsonAsync<Post>($"/api/persons/events/{id ?? post.Id}", post).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Töröl egy bejegyzést az azonosítója alapján
        /// </summary>
        /// <param name="id">Bejegyzés azonosítója</param>
        /// <returns><see cref="Response"/></returns>
        public async Task<Response> DeletePostById(int id)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Response>(Connection.DeleteAsync($"/api/persons/events/{id}").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Töröl egy bejegyzést model alapján
        /// </summary>
        /// <param name="post"><see cref="Post"/> modell</param>
        /// <returns><see cref="Response"/></returns>
        public async Task<Response> DeletePost(Post post) => await DeletePostById(post.Id);
        #endregion

        #region Szerződések
        /// <summary>
        /// Visszaad egy szerződést azonosító alapján
        /// </summary>
        /// <param name="id">Szerződés azonosítója</param>
        /// <returns><see cref="Contract"/></returns>
        public async Task<Contract> GetContract(int id)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Contract>(Connection.GetAsync($"/api/persons/contracts/{id}").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Visszaadja az összes szerződést
        /// </summary>
        /// <returns><see cref="Contract"/> lista</returns>
        public async Task<ICollection<Contract>> GetContracts()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<Contract>>(Connection.GetAsync($"/api/persons/contracts").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Visszaadja egy személy összes szerződését
        /// </summary>
        /// <param name="personId">Személy azonosítója</param>
        /// <returns><see cref="Contract"/> lista</returns>
        public async Task<ICollection<Contract>> GetPersonsContracts(int personId)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<Contract>>(Connection.GetAsync($"/api/persons/{personId}/contracts").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// Létrehoz egy új szerződést
        /// </summary>
        /// <param name="contract">Új szerződés adatai</param>
        /// <returns><see cref="Contract"/> lista</returns>
        public async Task<Contract> NewContract(Contract contract)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Contract>(Connection.PostAsJsonAsync<Contract>($"/api/persons/contracts", contract).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract">Szerződés adatai</param>
        /// <param name="id">Szerződés azonosítója, opcionális érték</param>
        /// <returns></returns>
        public async Task<Contract> UpdateContract(Contract contract, int? id = null)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<Contract>(Connection.PutAsJsonAsync<Contract>($"/api/persons/contracts/{id ??= contract.Id}", contract).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }
        #endregion

        #region Termékek, termékkategóriák
        /**
         * Visszaadja az összes terméket
         * @return array
         * @throws APIException
         * @throws \GuzzleHttp\Exception\GuzzleException
         */
        public async Task<ICollection<Product>> GetProducts()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<Product>>(Connection.GetAsync($"/api/products").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /**
         * Visszaadja az összes terméktípust (termékkategóriát)
         * @return array
         * @throws APIException
         * @throws \GuzzleHttp\Exception\GuzzleException
         */
        public async Task<ICollection<ProductType>> GetProductTypes()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<ProductType>>(Connection.GetAsync($"/api/producttypes").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }

        /**
         * Visszaadja az összes szolgáltatót
         * @return array
         * @throws APIException
         * @throws \GuzzleHttp\Exception\GuzzleException
         */
        public async Task<ICollection<ProductProvider>> GetProductProviders()
        {
            return await Task.Run(() => JsonSerializer.Deserialize<ICollection<ProductProvider>>(Connection.GetAsync($"/api/productproviders").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult()));
        }
        #endregion
    }
}
