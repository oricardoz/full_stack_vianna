using System;
using semana12.Dtos;
using semana12.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using semana12.Models;
using semana12.Services;

namespace semana12.Endpoints;

public static class LoginEndpoint
{
    public static void AdicionarLoginEndpoint(this WebApplication app)
    {
        var grupo = app.MapGroup("/login");


        grupo.MapPost("/app", PostLoginAppAsync );
        grupo.MapPost("/navegador", PostLoginNavegadorAsync );
    }

    private static async Task<IResult> PostLoginAppAsync(LoginDTO infoLogin, AtletaContext db, IPasswordHasher<Usuario> hasher)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Email == infoLogin.Email);

        if(usuario == null)
            return TypedResults.Unauthorized();
        else if(hasher.VerifyHashedPassword(usuario, usuario.HashSenha, infoLogin.HashSenha) == PasswordVerificationResult.Failed)
            return TypedResults.Unauthorized();

        return TypedResults.Ok(new TokenService().Gerar(usuario));
    }

    private static async Task<IResult> PostLoginNavegadorAsync(LoginDTO infoLogin, AtletaContext db, IPasswordHasher<Usuario> hasher, HttpContext contexto)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Email == infoLogin.Email);

        if(usuario == null)
            return TypedResults.Unauthorized();

        var token = new TokenService().Gerar(usuario);

        contexto.Response.Cookies.Append(
            "acessToken"
            , token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true
        });


        return TypedResults.Ok();
    }

}
