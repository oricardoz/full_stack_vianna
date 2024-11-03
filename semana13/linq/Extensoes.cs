using System;

namespace linq;

public static class Extensoes
{
    public static IEnumerable<T> Filtrar<T>(this IEnumerable<T> itens, Func<T, bool> selecionar)
    {
        foreach (var item in itens)
        {
            if(selecionar(item))
               yield return true;
        }
    }
}

