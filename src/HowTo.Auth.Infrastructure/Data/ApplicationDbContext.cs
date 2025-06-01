using HowTo.Auth.Core.Entities;

namespace HowTo.Auth.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options);
