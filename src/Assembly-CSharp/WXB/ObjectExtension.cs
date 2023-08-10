using System.Collections.Generic;
using System.Text;

namespace WXB;

public static class ObjectExtension
{
	public static StringBuilder AppendLine(this StringBuilder sb, string value, params string[] args)
	{
		return sb.AppendLine(string.Format(value, args));
	}

	public static T pop_back<T>(this List<T> l)
	{
		T result = l[l.Count - 1];
		l.RemoveAt(l.Count - 1);
		return result;
	}

	public static T back<T>(this List<T> l)
	{
		return l[l.Count - 1];
	}
}
