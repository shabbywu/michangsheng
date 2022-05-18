using System;
using System.Reflection;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011DD RID: 4573
	internal class Utilities
	{
		// Token: 0x06006FE0 RID: 28640 RVA: 0x002A0AC8 File Offset: 0x0029ECC8
		public static string ExpandVariables(string format, object variables, bool underscoredOnly = true)
		{
			if (variables == null)
			{
				variables = new
				{

				};
			}
			Type type = variables.GetType();
			return Utilities.VARIABLE.Replace(format, delegate(Match match)
			{
				string value = match.Groups[1].Value;
				if (underscoredOnly && !value.StartsWith("_"))
				{
					return match.Groups[0].Value;
				}
				PropertyInfo property = Framework.Do.GetProperty(type, value);
				if (property != null)
				{
					return property.GetValue(variables, null).ToString();
				}
				return "{" + value + ": not found}";
			});
		}

		// Token: 0x06006FE1 RID: 28641 RVA: 0x0004C077 File Offset: 0x0004A277
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

		// Token: 0x040062D6 RID: 25302
		private static readonly Regex VARIABLE = new Regex("\\{(\\w+)\\}");
	}
}
