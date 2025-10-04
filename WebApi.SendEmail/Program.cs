namespace API_SendEmail.API_SendEmail;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.ConfigureHttpJsonOptions(options =>
		{
			options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		});

		builder.Services.AddOpenApi();

		builder.Services.AddTransient<IEmailSenderService, MailKitEmailSender>();
		builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.MapOpenApi();
		app.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/openapi/v1.json", "v1");
		});

		CultureInfo appCulture = new("it-IT");

		app.UseRequestLocalization(new RequestLocalizationOptions
		{
			DefaultRequestCulture = new RequestCulture(appCulture),
			SupportedCultures = [appCulture]
		});

		app.UseHttpsRedirection();
		app.MapEndpoints<EmailEndpoints>();

		app.Run();
	}
}