using System;
using System.Reflection;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.VsCodeDebugger.SDK;

internal class Utilities
{
	private static readonly Regex VARIABLE = new Regex("\\{(\\w+)\\}");

	public static string ExpandVariables(string format, object variables, bool underscoredOnly = true)
	{
		if (variables == null)
		{
			variables = new { };
		}
		Type type = variables.GetType();
		return VARIABLE.Replace(format, delegate(Match match)
		{
			string value = match.Groups[1].Value;
			if (!underscoredOnly || value.StartsWith("_"))
			{
				PropertyInfo property = Framework.Do.GetProperty(type, value);
				if (property != null)
				{
					return property.GetValue(variables, null).ToString();
				}
				return "{" + value + ": not found}";
			}
			return match.Groups[0].Value;
		});
	}

	public static string MakeRelativePath(string dirPath, string absPath)
	{
		if (!dirPath.EndsWith("/"))
		{
			dirPath += "/";
		}
		if (absPath.StartsWith(dirPath))
		{
			return absPath.Replace(dirPath, "");
		}
		return absPath;
	}
}
