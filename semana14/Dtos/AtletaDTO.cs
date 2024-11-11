using System;
using semana12.Models;

namespace semana12.Dtos;

public class AtletaDTO
{

    public AtletaDTO()
    {

    }
    public AtletaDTO(Atleta obj)
    {
        Id = obj.Id.ToString();
        Nome = obj.Nome;
        Altura = obj.Altura;
        Peso = obj.Peso;

    }

    public Atleta GetModel()
    {
      
        var obj = new Atleta();
        PreencherModel(obj);
        return obj;
    
    }

    public void PreencherModel(Atleta obj)
    {
        long.TryParse(this.Id, out long id);
        obj.Id = id;
        obj.Nome = this.Nome;
        obj.Altura = this.Altura;
        obj.Peso = this.Peso;

    }
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public double Altura { get; set; }

    public double Peso { get; set; }
}
