namespace CRUD.Api;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        BookApi.Configure(app);
    }
}
