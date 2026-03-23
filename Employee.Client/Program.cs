using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Employee.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["Api:BaseUrl"]
    ?? throw new InvalidOperationException("Api:BaseUrl not configured.");

var apiScope = builder.Configuration["AzureAd:ApiScope"]
    ?? throw new InvalidOperationException("AzureAd:ApiScope not configured.");

// Named HttpClient that automatically attaches the access token
builder.Services.AddHttpClient("EmployeeApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: [apiBaseUrl],
            scopes: [apiScope]);
    return handler;
});

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("EmployeeApi"));

// MSAL authentication — Authorization Code + PKCE (no client secret)
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(apiScope);

    // Ensure login hint / prompt behavior
    options.ProviderOptions.LoginMode = "redirect";
});

await builder.Build().RunAsync();