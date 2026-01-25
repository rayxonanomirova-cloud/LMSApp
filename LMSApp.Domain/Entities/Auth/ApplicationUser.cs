using Microsoft.AspNetCore.Identity;

namespace LMSApp.Domain.Entities.Auth;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? PINFL { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid? MainRoleId { get; set; }
    public string? MiddleName { get; set; }
    public string? FullName => $"{LastName??""} {FirstName??""} {MiddleName??""}";
    public DateTime? DateOfBirth { get; set; }
    public int? Gender { get; set; }
    public int CountEnter { get; set; }
    public DateTime LastActive { get; set; }
    public string? Address { get; set; }
    public int? RegionId { get; set; }
    public int? DistrictId { get; set; }

    public void UpdateLastActive()
    {
        LastActive = DateTime.UtcNow.AddHours(5);
        CountEnter += 1;
    }
}

