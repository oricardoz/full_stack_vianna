public static class StringExtensions
{
    // Este método conta o número de palavras em uma string
    public static int WordCount(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return 0;
        }
        return str.Split(new[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
