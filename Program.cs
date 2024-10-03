namespace API_SendEmail;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.ConfigureHttpJsonOptions(options =>
		{
			options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		});

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.SendEmail", Version = "v1" });
		});

		builder.Services.AddTransient<IEmailSenderService, MailKitEmailSender>();
		builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.SendEmail v1"));

		CultureInfo appCulture = new("it-IT");

		app.UseRequestLocalization(new RequestLocalizationOptions
		{
			DefaultRequestCulture = new RequestCulture(appCulture),
			SupportedCultures = [appCulture]
		});

		app.UseHttpsRedirection();
		app.UseRouting();

		app.MapControllers();
		app.Run();
	}
}