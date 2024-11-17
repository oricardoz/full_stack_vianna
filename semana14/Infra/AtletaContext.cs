using System;
using Microsoft.EntityFrameworkCore;
using semana12.Models;

namespace semana12.Infra;

public class AtletaContext : DbContext
{
    public DbSet<Atleta> Atletas {get; set;}

    public DbSet<Usuario> Usuarios
    
     {get; set;}

    public AtletaContext()
    {
        caminho = @$"{AppDomain.CurrentDomain.BaseDirectory}\atleta.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={caminho}");
    }

    private string caminho;
}
