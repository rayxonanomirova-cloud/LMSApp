
using LMSApp.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Infrastructure.Context
{
    public static class LMSAppContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedRoles();
            builder.SeedUsers();
        }

        private const string userGuid = "1a44ae93-3974-4e67-baec-7dd27d9c47a9";
        private const string roleAdminGuid = "2c0569bc-e6fc-4925-ac2a-0d11582e73cf";
        private const string roleTeacherGuid = "31b235fc-7a39-46c8-9ed3-350b3cb5ce2c";
        private const string roleStudentGuid = "4fd88c72-4516-4873-97a8-32d757f41b2e";
        private static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>(role =>
            {
                role.HasData(new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = Guid.Parse(roleAdminGuid),
                    ConcurrencyStamp = "1"
                });
                role.HasData(new ApplicationRole
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER",
                    Id = Guid.Parse(roleTeacherGuid),
                    ConcurrencyStamp = "2"
                });
                role.HasData(new ApplicationRole
                {
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    Id = Guid.Parse(roleStudentGuid),
                    ConcurrencyStamp = "3"
                });
            });
        }

        private static void SeedUsers(this ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>(user =>
            {
                user.HasData(new ApplicationUser
                {
                    Id = Guid.Parse(userGuid),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "abdijabbarovdilshod@gmail.com",
                    FirstName = "Dilshod",
                    LastName = "Abdijabbarov",
                    PINFL = "12345678901234",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty,
                    Gender = 1,
                    DateOfBirth = new DateTime(1998, 01, 07),
                    PasswordHash = hasher.HashPassword(null, "Dilshod$221026"),
                    PhoneNumber = "+998999999999",
                    RegionId = 10,
                    DistrictId = 1001,                    
                });
            });

            builder.Entity<IdentityUserRole<Guid>>(role =>
            {
                role.HasData(
                    new IdentityUserRole<Guid>
                    {
                        UserId = Guid.Parse(userGuid),
                        RoleId = Guid.Parse(roleAdminGuid)
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = Guid.Parse(userGuid),
                        RoleId = Guid.Parse(roleTeacherGuid)
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = Guid.Parse(userGuid),
                        RoleId = Guid.Parse(roleStudentGuid)
                    }
                );
            });
        }
    }
}
