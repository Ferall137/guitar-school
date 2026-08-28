namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // НАСТРОЙКА СТАТИКИ БЕЗ КЕША
            var staticFileOptions = new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Отключаем кеш для ВСЕХ файлов
                    ctx.Context.Response.Headers.Append(
                        "Cache-Control", "no-cache, no-store, must-revalidate");
                    ctx.Context.Response.Headers.Append(
                        "Pragma", "no-cache");
                    ctx.Context.Response.Headers.Append(
                        "Expires", "0");
                }
            };

            app.UseStaticFiles(staticFileOptions); // <-- подставляем настройку

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}