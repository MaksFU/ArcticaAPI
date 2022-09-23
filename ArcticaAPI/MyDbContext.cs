using ArcticaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcticaAPI
{
    public class MyDbContext: DbContext
    {
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Position> Positions { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>(h =>
            {
                h.HasMany(c => c.Doctors)
                .WithOne(s => s.Hospital)
                .HasForeignKey(t => t.HospitalId);

                h.HasData(
                    new Hospital[]
                    {
                        new Hospital
                        {
                            Id = 1,
                            Name = "Первая городская имени Волосевича",
                            Address = "ул. Суворова, 1, Архангельск, Архангельская обл.",
                            CarPark = true
                        },
                        new Hospital{
                            Id = 2,
                            Name = "ГБУЗ АО 'Архангельская областная клиническая больница'",
                            Address = "просп. Ломоносова, 292, Архангельск, Архангельская обл.",
                            CarPark = false
                        }
                    });
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasMany(c => c.Patients)
                .WithMany(s => s.Doctors)
                .UsingEntity(t =>
                {
                    t.ToTable("DoctorPatient");
                    t.HasData(new[] {
                        new{ DoctorsId = 2, PatientsId = 1 },
                        new{ DoctorsId = 2, PatientsId = 2 },
                        new{ DoctorsId = 3, PatientsId = 3 }
                    });
                });

                d.HasData(
                    new Doctor[]
                    {
                        new Doctor
                        {
                            Id = 1,
                            FName = "Алина",
                            SName = "Фролова",
                            TName = "Васильевна",
                            Age = 33,
                            HospitalId = 1,
                            PositionId = 1
                        },
                        new Doctor
                        {
                            Id = 2,
                            FName = "Анна",
                            SName = "Попова",
                            TName = "Семёновна",
                            Age = 76,
                            HospitalId = 1,
                            PositionId = 2
                        },
                        new Doctor
                        {
                            Id = 3,
                            FName = "Ветров",
                            SName = "Максим",
                            TName = "Васильевич",
                            Age = 64,
                            HospitalId = 2,
                            PositionId = 2
                        }
                    });
            });

            modelBuilder.Entity<Position>(p =>
            {
                p.HasMany(c => c.Doctors)
                .WithOne(s => s.Position)
                .HasForeignKey(t => t.PositionId);

                p.HasData(
                    new Position[]
                    {
                        new Position
                        {
                            Id = 1,
                            Name = "Кардиолог",
                            Charge = "Лечить сердце и сосуды"
                        },
                        new Position
                        {
                            Id = 2,
                            Name = "Терапевт",
                            Charge = "Лечить всё"
                        },
                    });
            });

            modelBuilder.Entity<Patient>().HasData(
                new Patient[] {
                    new Patient
                    {
                        Id = 1,
                        FName = "Василий",
                        SName = "Петров",
                        TName = "Евгеньевич",
                        Age = 33,
                        Diagnosis = "Гипертония"
                    },
                    new Patient
                    {
                        Id = 2,
                        FName = "Мария",
                        SName = "Семёнова",
                        TName = "Анатольевна",
                        Age = 24,
                        Diagnosis = "Аритмия"
                    },
                    new Patient
                    {
                        Id = 3,
                        FName = "Арсен",
                        SName = "Петров",
                        TName = "Максимович",
                        Age = 54,
                        Diagnosis = "Рак"
                    },
                });
        }
    }
}
