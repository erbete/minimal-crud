namespace CRUD.Endpoints;

public static class Endpoints
{
    public static void UseEndpoints(this WebApplication app)
    {
        BookEndpoints.Configure(app);
    }
}
