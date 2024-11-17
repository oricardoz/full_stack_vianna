using System;

namespace semana12;

public class Config
{
    public string ChavePrivada {get; set;} = string.Empty;

    public static Config Instancia => instancia ??= new Config{
        ChavePrivada = Environment.GetEnvironmentVariable("AUTH_CHAVE_PRIVADA")
    };

    private Config(){}

    private static Config? instancia = null;
}
