using BraReintFrontend.Data.Models;
namespace BraReintFrontend.Services.AuthServices;

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;

public class AuthService(HttpClient http, IJSRuntime js) : IAuthService
{
    private const string TokenKey = "authToken";

    public async Task<bool> LoginAsync(LoginDto login)
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = null };
        var json = JsonSerializer.Serialize(login, options);

        var response = await http.PostAsync("Auth/Login",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status Code: {response.StatusCode}");
        Console.WriteLine($"Response: {content}");

        if (!response.IsSuccessStatusCode) return false;

        var result = JsonSerializer.Deserialize<AuthResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (result is null || !result.IsSuccess) return false;

        await js.InvokeVoidAsync("localStorage.setItem", TokenKey, result.Token);
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

        return true;
    }
    

    public async Task LogoutAsync()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        http.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await js.InvokeAsync<string>("localStorage.getItem", TokenKey);
    }
}
