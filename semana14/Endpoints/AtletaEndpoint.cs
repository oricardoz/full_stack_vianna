using System;
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


        grupo.MapGet("/", Get );
        grupo.MapGet("/{Id}", GetById );
        grupo.MapPost("/", Post );
        grupo.MapDelete("/{Id}", Delete );
        grupo.MapPut("/{Id}", Put );
    }

    private static IResult Get(AtletaContext db)
    {
        return TypedResults.Ok(db.Atletas.ToList()); 
    }

    private static IResult GetById(long id, AtletaContext db)
    {
        var obj = db.Atletas.Find(id);

        if(obj == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(obj); 
    }

    private static IResult Post(Atleta obj, AtletaContext db)
    {
        obj.Id =  GeradorId.GetId();
        db.Atletas.Add(obj);
        db.SaveChanges();

        return TypedResults.Created($"atletas/{obj.Id}", obj);

    }

    private static IResult Put(long id, Atleta objNovo, AtletaContext db)
    {
        if(id != objNovo.Id)
            return TypedResults.BadRequest();

        var obj = db.Atletas.Find(id);

        if(obj == null)
            return TypedResults.NotFound();

        obj.Nome = objNovo.Nome;
        obj.Altura = objNovo.Altura;
        obj.Peso = objNovo.Peso;

        db.Update(obj);
        db.SaveChanges();

        return TypedResults.NoContent();
    }

    private static IResult Delete(long id,AtletaContext db)
    {
        var obj = db.Atletas.Find(id);

        if(obj == null)
            return TypedResults.NotFound();

        db.Atletas.Remove(obj);   
        db.SaveChanges();

        return TypedResults.NoContent();
    }

}