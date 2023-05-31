using WWW.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
/*#####################################  Add SingalR ###############################################*/
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();
/*################################### SingslR ###############################*/
app.MapRazorPages();
//app.MapHub<ChatHub>("/ChatHub");
//app.UseEndpoints(endpoints => endpoints.MapRazorPages());

app.Run();
