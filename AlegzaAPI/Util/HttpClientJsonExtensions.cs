#if !NET5_0_OR_GREATER
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Json;

internal static class HttpClientJsonExtensions
{

    #region StringToHttpContent
    private static HttpContent StringToHttpContent(object data, JsonSerializerOptions? options = null)
    {
        StringContent content = new(JsonSerializer.Serialize(data, options));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }
    private static async Task<T?> ResponseToJson<T>(HttpResponseMessage data, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<T>(await data.Content.ReadAsStringAsync(), options);
    private static async Task<object?> ResponseToJson(HttpResponseMessage data, Type type, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize(await data.Content.ReadAsStringAsync(), type, options);
    #endregion

    #region GetFromJsonAsync
    public static async Task<object?> GetFromJsonAsync(this HttpClient client, string? requestUri, Type type, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        return ResponseToJson(await client.GetAsync(requestUri, cancellationToken), type, options);
    }

    public static async Task<object?> GetFromJsonAsync(this HttpClient client, string? requestUri, Type type, CancellationToken cancellationToken = default)
    {
        return ResponseToJson(await client.GetAsync(requestUri, cancellationToken), type);
    }

    public static async Task<object?> GetFromJsonAsync(this HttpClient client, Uri? requestUri, Type type, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        return ResponseToJson(await client.GetAsync(requestUri, cancellationToken), type, options);
    }

    public static async Task<object?> GetFromJsonAsync(this HttpClient client, Uri? requestUri, Type type, CancellationToken cancellationToken = default)
    {
        return ResponseToJson(await client.GetAsync(requestUri, cancellationToken), type);
    }

    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient client, string? requestUri, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        return await ResponseToJson<T>(await client.GetAsync(requestUri, cancellationToken), options);
    }

    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
    {
        return await ResponseToJson<T>(await client.GetAsync(requestUri, cancellationToken));
    }

    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient client, Uri? requestUri, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        return await ResponseToJson<T>(await client.GetAsync(requestUri, cancellationToken), options);
    }

    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
    {
        return await ResponseToJson<T>(await client.GetAsync(requestUri, cancellationToken));
    }
    #endregion

    #region PostAsJsonAsync
    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value, options), cancellationToken);
    }

    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value), cancellationToken);
    }

    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value, options), cancellationToken);
    }

    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value), cancellationToken);
    }
    #endregion

    #region PutAsJsonAsync
    public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value, options), cancellationToken);
    }

    public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value), cancellationToken);
    }

    public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value, options), cancellationToken);
    }

    public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken)
    {
        return client.PostAsync(requestUri, StringToHttpContent(value), cancellationToken);
    }
    #endregion
}
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#endif
