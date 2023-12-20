using GOPS.Blazor.Server.App;
using GOPS.Client.Shared;
using GOPS.Client.Shared.Extensions;

using MudBlazor.Services;

using MudExtensions.Services;

using Tailwind;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();
builder.Services.AddAppServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
	app.RunTailwind("buildcss:debug", "./");
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(GOPS.Blazor.Wasm._Imports).Assembly);

SeederExtensions.Init();

app.Run();
