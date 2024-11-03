using linq;

var numeros = new int[] { 1, 2, 5, 8, 10}
var palavras = new String [] { "Teste", "Isso", "Agora"}

//var pares = numeros.Filtrar(x => x % 2 == 0)
var pares = from x in numeros wherer x % 2 == 0 select x
var impares = numeros.Filtrar(x => x % 2 != 0)
var palavrasComO = palavras.Filtrar(x => x.Contains("o"))

    Imprimir(pares)
    Console.WriteLine("----------------------");
    Imprimir(impares)
    Console.WriteLine("----------------------");
    Imprimir(palavrasComO)

void Imprimir<T>(IEnumerable<T> itens) 
{
    foreach (var item in itens)
        Console.WriteLine(item.ToString());
}