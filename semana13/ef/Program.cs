using System.Data,Common;
using ef;

var db = new AtletaDbContext();

//Adicionar(new Atleta( Id = 1 , Nome = "Ricardo", Peso = 60 , Altura = 170,))
//Adicionar(new Atleta( Id = 2 , Nome = "Ana", Peso = 55 , Altura = 150,))

var a1 = RetornaPorId(1);

Atleta? RetornaPorId(int i)
{
    return db.Atletas.Find(id);
}    

void Remover(int i)
{
    db.Atletas.Remove(i);
    db.SaveChanges();
}

void Atualizar(Atleta obj)
{
    db.Atletas.Update(obj);
    db.SaveChanges();
}

void Adicionar(Atleta obj) 
{
    db.Atletas.Add(obj);
    db.SaveChanges();
}