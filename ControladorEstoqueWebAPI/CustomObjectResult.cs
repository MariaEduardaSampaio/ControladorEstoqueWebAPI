using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomObjectResult : IActionResult
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object? Object { get; set; }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.StatusCode = StatusCode;

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };

        if (Object != null)
            await response.WriteAsJsonAsync(Object, options);
        else
            await response.WriteAsync(Message);
    }
}
