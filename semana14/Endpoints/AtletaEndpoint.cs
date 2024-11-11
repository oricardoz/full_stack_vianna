using System;
using Microsoft.EntityFrameworkCore;
using semana12.Dtos;
using semana12.Infra;
using semana12.Models;

namespace semana12.Endpoints;

public static class AtletaEndpoint
{
    static AtletaEndpoint()
    {

    }

    public static void AdicionarAtletasEndpoint(this WebApplication app)
    {
        var grupo = app.MapGroup("/atletas");


        grupo.MapGet("/", GetAsync );
        grupo.MapGet("/{Id}", GetByIdAsync );
        grupo.MapPost("/", PostAsync );
        grupo.MapDelete("/{Id}", DeleteAsync );
        grupo.MapPut("/{Id}", PutAsync );
    }

    private static async Task<IResult> GetAsync(AtletaContext db)
    {
        var objetos = await db.Atletas.ToListAsync();
        return TypedResults.Ok(objetos.Select(x => new AtletaDTO(x))); 
    }

    private static async Task<IResult> GetByIdAsync(string id, AtletaContext db)
    {
        var obj = await db.Atletas.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(new AtletaDTO(obj)); 
    }

    private static async Task<IResult> PostAsync(AtletaDTO dto, AtletaContext db)
    {
        Atleta obj = dto.GetModel();
        obj.Id =  GeradorId.GetId();
        await db.Atletas.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"atletas/{obj.Id}", new AtletaDTO(obj));

    }

    private static async Task<IResult> PutAsync(string id, AtletaDTO dto, AtletaContext db)
    {
        if(id != dto.Id)
            return TypedResults.BadRequest();

        var obj = await db.Atletas.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        dto.PreencherModel(obj);

        db.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string id,AtletaContext db)
    {
        var obj = await db.Atletas.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        db.Atletas.Remove(obj);   
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

}