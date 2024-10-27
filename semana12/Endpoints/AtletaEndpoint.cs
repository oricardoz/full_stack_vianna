using System;

namespace semana12.Endpoints;

public static class AtletaEndpoint
{
    static AtletaEndpoint()
    {
        objetos = [];
    }

    public static void AdicionarAtletasEndpoint(this WebApplication app)
    {
        app.MapGet("/atletas", Get )
        app.MapGet("/atletas/{Id}", GetById )
        app.MapGet("/atletas", Post )
        app.MapGet("/atletas/{Id}", Delete )
        app.MapGet("/atletas/{Id}", Put )
    }

    private static IResult Get()
    {
        return objetos != null ? TypedReults.Ok(objetos) : TypedReults.NotFound(); 
    }

    private static IResult GetById(long id)
    {
        var obj = objetos.FirstOrDefault(x => x.Id == id);

        if(obj == null)
            return TypedReults.NotFound();

        return TypedReults.Ok(obj); 
    }

    private static IResult Post(Atleta obj)
    {
        obj.Id = (objetos.MaxBy(x => x.Id)?? new Atleta()).Id + 1;
        objetos.add(obj);

        return TypedReults.Created($"atletas/{obj.Id}", obj);

    }

    private static IResult Put(long id, Atleta objNovo)
    {
        if(id != objNovo)
            return TypedReults.BadRequest() 

        var obj = objetos.FirstOrDefault(x => x.Id == id);

        if(obj == null)
            return TypedReults.NotFound() 

        obj.Nome = objNovo.Nome;
        obj.Altura = objNovo.Altura;
        obj.Peso = objNovo.Peso;

        return TypedReults.NoContent() 
    }

    private static IResult Delete(long id)
    {
        var obj = objetos.FirstOrDefault(x => x.Id == id);

        if(obj == null)
            return TypedReults.NotFound() 

        objetos.Remove(obj);   

        return TypedReults.NoContent() 
    }


    private static readonly IList<Atleta> objetos;

}