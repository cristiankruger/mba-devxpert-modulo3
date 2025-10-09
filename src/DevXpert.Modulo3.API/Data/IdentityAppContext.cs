using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.API.Data;

public class IdentityAppContext(DbContextOptions<IdentityAppContext> options) : IdentityDbContext(options)
{

}
