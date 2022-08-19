using System;
using System.Reflection;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DB1 RID: 3505
	internal class Utilities
	{
		// Token: 0x0600639A RID: 25498 RVA: 0x0027B4E4 File Offset: 0x002796E4
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

		// Token: 0x0600639B RID: 25499 RVA: 0x0027B540 File Offset: 0x00279740
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

		// Token: 0x040055EF RID: 21999
		private static readonly Regex VARIABLE = new Regex("\\{(\\w+)\\}");
	}
}
