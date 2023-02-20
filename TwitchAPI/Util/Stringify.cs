using System.Text;

namespace TwitchAPI.Util;

public static class Stringify<T>
{
    public static string ToArray(List<T> list)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");
        
        foreach (T item in list)
        {
            stringBuilder.Append($"\"{item}\",");
        }
        stringBuilder.Length--;
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }
    public static string ToTuple(List<T> list)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("(");
        
        foreach (T item in list)
        {
            stringBuilder.Append($"'{item}',");
        }
        stringBuilder.Length--;
        stringBuilder.Append(")");
        return stringBuilder.ToString();
    }
}