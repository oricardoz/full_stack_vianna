using System;
using semana12.Dtos;
using Microsoft.EntityFrameworkCore;
using semana12.Infra;
using semana12.Models;
using Microsoft.AspNetCore.Identity;

namespace semana12.Endpoints;

public static class UsuarioEndpoint
{
    static UsuarioEndpoint()
    {

    }

    public static void AdicionarUsuarioEndpoint(this WebApplication app)
    {
        var grupo = app.MapGroup("/usuarios");


        grupo.MapGet("/", GetAsync );
        grupo.MapGet("/{Id}", GetByIdAsync );
        grupo.MapPost("/", PostAsync );
        grupo.MapPost("/admin", PostAsyncAdmin );
        grupo.MapDelete("/{Id}", DeleteAsync );
        grupo.MapPut("/{Id}", PutAsync );
        grupo.MapPatch("/{Id}/{SenhaAnterior}/{SenhaNova    }", PatchAlteraSenhaAsync);
    }

    private static async Task<IResult> GetAsync(AtletaContext db)
    {
        var objetos = await db.Usuarios.ToListAsync();
        return TypedResults.Ok(objetos.Select(x => new UsuarioDTO(x))); 
    }

    private static async Task<IResult> GetByIdAsync(string id, AtletaContext db)
    {
        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(new UsuarioDTO(obj)); 
    }

    private static async Task<IResult> PostAsync(UsuarioDTO dto, AtletaContext db)
    {
        Usuario obj = dto.GetModel();
        obj.Id =  GeradorId.GetId();
        obj.Role = "comum";
        await db.Usuarios.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"usuarios/{obj.Id}", new UsuarioDTO(obj));

    }

        private static async Task<IResult> PostAsyncAdmin(UsuarioDTO dto, AtletaContext db)
    {
        Usuario obj = dto.GetModel();
        obj.Id =  GeradorId.GetId();
        obj.Role = "admin";
        await db.Usuarios.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"usuarios/{obj.Id}", new UsuarioDTO(obj));

    }

    private static async Task<IResult> PutAsync(string id, UsuarioDTO dto, AtletaContext db)
    {
        if(id != dto.Id)
            return TypedResults.BadRequest();

        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        dto.PreencherModel(obj);

        db.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> PatchAlteraSenhaAsync(string id, string SenhaAnterior, string SenhaNova, AtletaContext db, IPasswordHasher<Usuario> hasher)
    {

        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        if(string.IsNullOrEmpty(obj.HashSenha) || hasher.VerifyHashedPassword(obj, obj.HashSenha, SenhaAnterior) != PasswordVerificationResult.Success)
            obj.HashSenha = hasher.HashPassword(obj, SenhaNova);
        else
            return TypedResults.Unauthorized();
           

        db.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string id,AtletaContext db)
    {
        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        db.Usuarios.Remove(obj);   
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
