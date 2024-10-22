class Program
{
    static void Main(string[] args)
    {
        string frase = "O C# é uma linguagem poderosa.";
        int numeroPalavras = frase.WordCount();  // Chama o método de extensão
        Console.WriteLine($"Número de palavras: {numeroPalavras}");
    }
}
