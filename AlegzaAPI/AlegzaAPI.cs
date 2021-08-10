using AlegzaCRM.AlegzaAPI.Exceptions;
using AlegzaCRM.AlegzaAPI.Model;
using AlegzaCRM.AlegzaAPI.Util;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
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

            AssemblyName executingAssembly = Assembly.GetExecutingAssembly().GetName();
            Connection.DefaultRequestHeaders.UserAgent.Clear();
            Connection.DefaultRequestHeaders.UserAgent.ParseAdd($"{executingAssembly.Name}/{executingAssembly.Version:4}");
        }

        #region Személyek
        /// <summary>
        /// Visszaad egy személyt az azonosítója alapján
        /// </summary>
        /// <param name="id">Személy azonosítója</param>
        /// <returns><see cref="Person"/></returns>
        public async Task<Person> GetPerson(int id) => await GetAsync<Person>($"/api/persons/{id}");

        /// <summary>
        /// Visszaad minden személyt
        /// </summary>
        /// <returns><see cref="Person"/> lista</returns>
        public async Task<ICollection<Person>> GetPersons() => await GetAsync<ICollection<Person>>($"/api/persons");

        /// <summary>
        /// Létrehoz egy új személyt
        /// </summary>
        /// <param name="person">Új személy adatai</param>
        /// <returns><see cref="Person"/></returns>
        public async Task<Person> NewPerson(Person person) => await PostAsJsonAsync<Person, Person>($"/api/persons", person);

        /// <summary>
        /// Módosít egy személyt. A személy azonosítóját a Person modelből veszi, vagy ha meg van adva az $id, akkor onnan
        /// </summary>
        /// <param name="person"><see cref="Person"/></param>
        /// <param name="id">Személy azonosítója, opcionális érték</param>
        /// <returns></returns>
        public async Task<Person> UpdatePerson(Person person, int? id = null) => await PutAsJsonAsync<Person, Person>($"/api/persons/{id ?? person.Id}", person);

        /// <summary>
        /// Töröl egy személyt az azonosítója alapján
        /// </summary>
        /// <param name="id">Személy azonosítója</param>
        /// <returns><see cref="Response"/></returns>
        public async Task<Response> DeletePersonById(int id) => await DeleteAsync<Response>($"/api/persons/{id}");

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
        public async Task<ICollection<PostType>> GetPostTypes() => await GetAsync<ICollection<PostType>>($"/api/eventtypes");

        /// <summary>
        /// Visszaad egy bejegyzést az azonosítója alapján
        /// </summary>
        /// <param name="id">Bejegyzés azonosítója</param>
        /// <returns><see cref="Post"/></returns>
        public async Task<Post> GetPost(int id) => await GetAsync<Post>($"/api/events/{id}");

        /// <summary>
        /// Visszaadja az összes bejegyzést
        /// </summary>
        /// <returns><see cref="Post"/> lista</returns>
        public async Task<ICollection<Post>> GetPosts() => await GetAsync<ICollection<Post>>($"/api/events");

        /// <summary>
        /// Visszaadja egy személy összes azonosítóját
        /// </summary>
        /// <param name="person_id">Személy azonosítója</param>
        /// <returns><see cref="Post"/> lista</returns>
        public async Task<ICollection<Post>> GetPersonsPosts(int person_id) => await GetAsync<ICollection<Post>>($"/api/persons/{person_id}/events");

        /// <summary>
        /// Létrehoz egy új bejegyzést
        /// </summary>
        /// <param name="post">Új bejegyzés adatai</param>
        /// <returns><see cref="Post"/></returns>
        public async Task<Post> NewPost(Post post) => await PostAsJsonAsync<Post, Post>($"/api/persons/events", post);

        /// <summary>
        /// Módosít egy bejegyzést. Az azonosítót a <see cref="Post"/> modelből veszi, vagy ha van <paramref name="id"/> megadva, akkor onnan
        /// </summary>
        /// <param name="post">Új bejegyzés adatai</param>
        /// <param name="id">Bejegyzés azonosítója, opcionális érték</param>
        /// <returns><see cref="Post"/></returns>
        public async Task<Post> UpdatePost(Post post, int? id = null) => await PutAsJsonAsync<Post, Post>($"/api/persons/events/{id ?? post.Id}", post);

        /// <summary>
        /// Töröl egy bejegyzést az azonosítója alapján
        /// </summary>
        /// <param name="id">Bejegyzés azonosítója</param>
        /// <returns><see cref="Response"/></returns>
        public async Task<Response> DeletePostById(int id) => await DeleteAsync<Response>($"/api/persons/events/{id}");

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
        public async Task<Contract> GetContract(int id) => await GetAsync<Contract>($"/api/persons/contracts/{id}");

        /// <summary>
        /// Visszaadja az összes szerződést
        /// </summary>
        /// <returns><see cref="Contract"/> lista</returns>
        public async Task<ICollection<Contract>> GetContracts() => await GetAsync<ICollection<Contract>>($"/api/persons/contracts");

        /// <summary>
        /// Visszaadja egy személy összes szerződését
        /// </summary>
        /// <param name="personId">Személy azonosítója</param>
        /// <returns><see cref="Contract"/> lista</returns>
        public async Task<ICollection<PersonContract>> GetPersonsContracts(int personId) => await GetAsync<ICollection<PersonContract>>($"/api/persons/{personId}/contracts");


        /// <summary>
        /// Létrehoz egy új szerződést
        /// </summary>
        /// <param name="contract">Új szerződés adatai</param>
        /// <returns><see cref="Contract"/> lista</returns>
        public async Task<Contract> NewContract(Contract contract) => await PostAsJsonAsync<Contract, Contract>($"/api/persons/contracts", contract);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract">Szerződés adatai</param>
        /// <param name="id">Szerződés azonosítója, opcionális érték</param>
        /// <returns></returns>
        public async Task<Contract> UpdateContract(Contract contract, int? id = null) => await PutAsJsonAsync<Contract, Contract>($"/api/persons/contracts/{id ?? contract.Id}", contract);
        #endregion

        #region Termékek, termékkategóriák
        /// <summary>
        /// Visszaadja az összes terméket
        /// </summary>
        /// <returns><see cref="Product"/> lista</returns>
        public async Task<ICollection<Product>> GetProducts() => await GetAsync<ICollection<Product>>($"/api/products");

        /// <summary>
        /// Visszaadja az összes terméktípust (termékkategóriát)
        /// </summary>
        /// <returns><see cref="ProductType"/> lista</returns>
        public async Task<ICollection<ProductType>> GetProductTypes() => await GetAsync<ICollection<ProductType>>($"/api/producttypes");

        /// <summary>
        /// Visszaadja az összes szolgáltatót
        /// </summary>
        /// <returns><see cref="ProductProvider"/> lista</returns>
        public async Task<ICollection<ProductProvider>> GetProductProviders() => await GetAsync<ICollection<ProductProvider>>($"/api/productproviders");
        #endregion

        #region Http kérés típusok
        private async Task<TResult> GetAsync<TResult>(string endpoint)
        {
            return await Task.Run(() =>
            {
                var data = Connection.GetAsync(endpoint).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                TResult result;
                try
                {
                    result = JsonSerializer.Deserialize<TResult>(data);
                }
                catch (JsonException ex)
                {
                    if (typeof(TResult).IsCollectionType()) throw;
                    var alegzaModel = JsonSerializer.Deserialize<AlegzaModel>(data);
                    if (!string.IsNullOrEmpty(alegzaModel.ErrorMessage)) throw new APIException(alegzaModel);
                    else throw;
                }

                if (result.GetType().IsCollectionType()) return result;
                if (!string.IsNullOrEmpty((result as AlegzaModel).ErrorMessage)) throw new APIException(result as AlegzaModel);
                return result;
            });
        }

        private async Task<TResult> PostAsJsonAsync<TResult, TRequest>(string endpoint, TRequest request)
        {
            return await Task.Run(() =>
            {
                var data = Connection.PostAsJsonAsync(endpoint, request).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                TResult result;
                try
                {
                    result = JsonSerializer.Deserialize<TResult>(data);
                }
                catch (JsonException ex)
                {
                    if (typeof(TResult).IsCollectionType()) throw;
                    var alegzaModel = JsonSerializer.Deserialize<AlegzaModel>(data);
                    if (!string.IsNullOrEmpty(alegzaModel.ErrorMessage)) throw new APIException(alegzaModel);
                    else throw;
                }
                if (result.GetType().IsCollectionType()) return result;
                if (!string.IsNullOrEmpty((result as AlegzaModel).ErrorMessage)) throw new APIException(result as AlegzaModel);
                return result;
            });
        }

        private async Task<TResult> PutAsJsonAsync<TResult, TRequest>(string endpoint, TRequest request)
        {
            return await Task.Run(() =>
            {
                var data = Connection.PutAsJsonAsync(endpoint, request).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                TResult result;
                try
                {
                    result = JsonSerializer.Deserialize<TResult>(data);
                }
                catch (JsonException ex)
                {
                    if (typeof(TResult).IsCollectionType()) throw;
                    var alegzaModel = JsonSerializer.Deserialize<AlegzaModel>(data);
                    if (!string.IsNullOrEmpty(alegzaModel.ErrorMessage)) throw new APIException(alegzaModel);
                    else throw;
                }

                if (result.GetType().IsCollectionType()) return result;
                if (!string.IsNullOrEmpty((result as AlegzaModel).ErrorMessage)) throw new APIException(result as AlegzaModel);
                return result;
            });
        }

        private async Task<TResult> DeleteAsync<TResult>(string endpoint)
        {
            return await Task.Run(() =>
            {
                var data = Connection.DeleteAsync(endpoint).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                TResult result;
                try
                {
                    result = JsonSerializer.Deserialize<TResult>(data);
                }
                catch (JsonException ex)
                {
                    if (typeof(TResult).IsCollectionType()) throw;
                    var alegzaModel = JsonSerializer.Deserialize<AlegzaModel>(data);
                    if (!string.IsNullOrEmpty(alegzaModel.ErrorMessage)) throw new APIException(alegzaModel);
                    else throw;
                }

                if (result.GetType().IsCollectionType()) return result;
                if (!string.IsNullOrEmpty((result as AlegzaModel).ErrorMessage)) throw new APIException(result as AlegzaModel);
                return result;
            });
        }
        #endregion
    }
}
