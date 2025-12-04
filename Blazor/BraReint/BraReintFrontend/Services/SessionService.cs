using Microsoft.JSInterop;

namespace BraReintFrontend.Services;

public class SessionService(IJSRuntime jsRuntime) : ISessionService
{
    public async Task ClearSessionAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "selectedDate");
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "selectedPostalCode");
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "selectedServices");
    }

    // Optional: You could add more methods here to handle specific session storage actions.
}