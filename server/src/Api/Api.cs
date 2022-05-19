namespace CRUD.Api;

public static class Api
{
    public static void UseApi(this WebApplication app)
    {
        BookApi.Configure(app);
    }
}
