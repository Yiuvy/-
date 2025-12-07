using Makarevich_sol_API.Migrations;
using Makarevich_sol_Domain.Entities;

namespace Makarevich_sol_API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {

            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            // Заполнение данными
            if (!context.Clinics.Any() && !context.Doctors.Any())
            {
                var clinics = new List<Clinic>
                {
                    new Clinic {  Name="ООО Кравира",IdNormalizedName="Kravira", Adress="Купецк"},
                    new Clinic { Name="ЗАО Лодэ",IdNormalizedName="Lode", Adress="Гачино"}
                };
                await context.Clinics.AddRangeAsync(clinics);
                await context.SaveChangesAsync();



                var doctors = new List<Doctor>
                {
                    new Doctor
                    {
                        Name = "Глеб",
                        Surname = "Романенко",
                        AmountOfPatients = 5,
                        Specialization="Интерн",
                        IdClinic =clinics.FirstOrDefault(c => c.IdNormalizedName.Equals("Kravira")).Id,
                        Image = uri+"/Images/17.jpg"
                    },
                    new Doctor
                    {
                        Name = "Анастасия",
                        Surname = "Кисегач",
                        AmountOfPatients = 2,
                        Specialization="Главный врач",
                        IdClinic =clinics.FirstOrDefault(c => c.IdNormalizedName.Equals("Lode")).Id,
                        Image = uri+"/Images/11.jpg"
                    },
                    new Doctor
                    {
                        Name = "Андрей",
                        Surname="Быков",
                        AmountOfPatients =78,
                        Specialization="Заведующий терапевтическим отделением",
                        IdClinic =clinics.FirstOrDefault(c => c.IdNormalizedName.Equals("Lode")).Id,
                        Image= uri+"/Images/13.jpg"
                    },
                    new Doctor
                    {
                        Name="Иван",
                        Surname="Купитман",
                        AmountOfPatients=12,
                        Specialization="Венеролог",
                        IdClinic =clinics.FirstOrDefault(c => c.IdNormalizedName.Equals("Kravira")).Id,
                        Image=uri+ "/Images/16.jpg"
                    }
                };

                await context.AddRangeAsync(doctors);
                await context.SaveChangesAsync();

            }

        }

    }
}
