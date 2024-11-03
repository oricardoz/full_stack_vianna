using System;
using Microsoft.EntityFrameworkCore;

namespace ef;   

public class AtletaDbContext : DbContext
{
    public DbSet<Atleta> Atletas { get; set; }

    public AtletaDbContext()
    {
        caminho = @$"{AppDomain.CurrentDomain.BaseDirectory}/atleta.db";
    }

    protected  override void onConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={caminho}");
    }

    private string caminho;
}