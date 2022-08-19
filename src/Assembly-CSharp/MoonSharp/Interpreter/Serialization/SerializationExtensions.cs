using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoonSharp.Interpreter.Serialization
{
	// Token: 0x02000CED RID: 3309
	public static class SerializationExtensions
	{
		// Token: 0x06005C9E RID: 23710 RVA: 0x00260DF4 File Offset: 0x0025EFF4
		public static string Serialize(this Table table, bool prefixReturn = false, int tabs = 0)
		{
			if (table.OwnerScript != null)
			{
				throw new ScriptRuntimeException("Table is not a prime table.");
			}
			string value = new string('\t', tabs);
			StringBuilder stringBuilder = new StringBuilder();
			if (prefixReturn)
			{
				stringBuilder.Append("return ");
			}
			if (!table.Values.Any<DynValue>())
			{
				stringBuilder.Append("${ }");
				return stringBuilder.ToString();
			}
			stringBuilder.AppendLine("${");
			foreach (TablePair tablePair in table.Pairs)
			{
				stringBuilder.Append(value);
				string arg = SerializationExtensions.IsStringIdentifierValid(tablePair.Key) ? tablePair.Key.String : ("[" + tablePair.Key.SerializeValue(tabs + 1) + "]");
				stringBuilder.AppendFormat("\t{0} = {1},\n", arg, tablePair.Value.SerializeValue(tabs + 1));
			}
			stringBuilder.Append(value);
			stringBuilder.Append("}");
			if (tabs == 0)
			{
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00260F1C File Offset: 0x0025F11C
		private static bool IsStringIdentifierValid(DynValue dynValue)
		{
			if (dynValue.Type != DataType.String)
			{
				return false;
			}
			if (dynValue.String.Length == 0)
			{
				return false;
			}
			if (SerializationExtensions.LUAKEYWORDS.Contains(dynValue.String))
			{
				return false;
			}
			if (!char.IsLetter(dynValue.String[0]) && dynValue.String[0] != '_')
			{
				return false;
			}
			foreach (char c in dynValue.String)
			{
				if (!char.IsLetterOrDigit(c) && c != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00260FAC File Offset: 0x0025F1AC
		public static string SerializeValue(this DynValue dynValue, int tabs = 0)
		{
			if (dynValue.Type == DataType.Nil || dynValue.Type == DataType.Void)
			{
				return "nil";
			}
			if (dynValue.Type == DataType.Tuple)
			{
				if (!dynValue.Tuple.Any<DynValue>())
				{
					return "nil";
				}
				return dynValue.Tuple[0].SerializeValue(tabs);
			}
			else
			{
				if (dynValue.Type == DataType.Number)
				{
					return dynValue.Number.ToString("r");
				}
				if (dynValue.Type == DataType.Boolean)
				{
					if (!dynValue.Boolean)
					{
						return "false";
					}
					return "true";
				}
				else
				{
					if (dynValue.Type == DataType.String)
					{
						return SerializationExtensions.EscapeString(dynValue.String ?? "");
					}
					if (dynValue.Type == DataType.Table && dynValue.Table.OwnerScript == null)
					{
						return dynValue.Table.Serialize(false, tabs);
					}
					throw new ScriptRuntimeException("Value is not a primitive value or a prime table.");
				}
			}
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x00261084 File Offset: 0x0025F284
		private static string EscapeString(string s)
		{
			s = s.Replace("\\", "\\\\");
			s = s.Replace("\n", "\\n");
			s = s.Replace("\r", "\\r");
			s = s.Replace("\t", "\\t");
			s = s.Replace("\a", "\\a");
			s = s.Replace("\f", "\\f");
			s = s.Replace("\b", "\\b");
			s = s.Replace("\v", "\\v");
			s = s.Replace("\"", "\\\"");
			s = s.Replace("'", "\\'");
			return "\"" + s + "\"";
		}

		// Token: 0x040053BB RID: 21435
		private static HashSet<string> LUAKEYWORDS = new HashSet<string>
		{
			"and",
			"break",
			"do",
			"else",
			"elseif",
			"end",
			"false",
			"for",
			"function",
			"goto",
			"if",
			"in",
			"local",
			"nil",
			"not",
			"or",
			"repeat",
			"return",
			"then",
			"true",
			"until",
			"while"
		};
	}
}
