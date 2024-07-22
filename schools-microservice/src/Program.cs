var builder = WebApplication.CreateBuilder(args);//builder de la app

// Add services to the container.
builder.Services.AddControllers();//controladores para manejar solicitudes HTTP y dar rptas

// servicios para interactuar
// builder.Services.AddScoped<ISchoolService, SchoolService>();
// builder.Services.AddScoped<ICourseService, CourseService>();
// builder.Services.AddScoped<ITeacherService, TeacherService>();
// builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// // Register repositories
// builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
// builder.Services.AddScoped<ICourseRepository, CourseRepository>();
// builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
// builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();//construye nuestra app

/*
Habilito página de errores de desarrollo que proporciona 
información detallada sobre cualquier excepción que ocurra.
*/
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
