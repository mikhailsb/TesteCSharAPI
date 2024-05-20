using CervejaAPITeste.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CervejaAPITeste.Data;

public class CervejaContext : DbContext
{
    public  CervejaContext(DbContextOptions<CervejaContext> opts) : base(opts)
    { }

    public DbSet<Cerveja> Cervejas { get; set; }
}
