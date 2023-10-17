using DataValidator.Interface;
using DataValidator.Repository;
using DataValidator.Validator;
using System.Text.Json.Serialization;

namespace DataValidator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add(typeof(DataValidatorActionFilter));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<DataValidatorTypeFactory>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler("/error");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}