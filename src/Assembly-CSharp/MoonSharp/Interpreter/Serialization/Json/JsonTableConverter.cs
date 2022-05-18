using System;
using System.Text;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter.Serialization.Json
{
	// Token: 0x020010CC RID: 4300
	public static class JsonTableConverter
	{
		// Token: 0x060067BD RID: 26557 RVA: 0x000474C8 File Offset: 0x000456C8
		public static string TableToJson(this Table table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			JsonTableConverter.TableToJson(stringBuilder, table);
			return stringBuilder.ToString();
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x0028A624 File Offset: 0x00288824
		private static void TableToJson(StringBuilder sb, Table table)
		{
			bool flag = true;
			if (table.Length == 0)
			{
				sb.Append("{");
				foreach (TablePair tablePair in table.Pairs)
				{
					if (tablePair.Key.Type == DataType.String && JsonTableConverter.IsValueJsonCompatible(tablePair.Value))
					{
						if (!flag)
						{
							sb.Append(',');
						}
						JsonTableConverter.ValueToJson(sb, tablePair.Key);
						sb.Append(':');
						JsonTableConverter.ValueToJson(sb, tablePair.Value);
						flag = false;
					}
				}
				sb.Append("}");
				return;
			}
			sb.Append("[");
			for (int i = 1; i <= table.Length; i++)
			{
				DynValue value = table.Get(i);
				if (JsonTableConverter.IsValueJsonCompatible(value))
				{
					if (!flag)
					{
						sb.Append(',');
					}
					JsonTableConverter.ValueToJson(sb, value);
					flag = false;
				}
			}
			sb.Append("]");
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x000474DB File Offset: 0x000456DB
		public static string ObjectToJson(object obj)
		{
			return ObjectValueConverter.SerializeObjectToDynValue(null, obj, JsonNull.Create()).Table.TableToJson();
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x0028A730 File Offset: 0x00288930
		private static void ValueToJson(StringBuilder sb, DynValue value)
		{
			switch (value.Type)
			{
			case DataType.Boolean:
				sb.Append(value.Boolean ? "true" : "false");
				return;
			case DataType.Number:
				sb.Append(value.Number.ToString("r"));
				return;
			case DataType.String:
				sb.Append(JsonTableConverter.EscapeString(value.String ?? ""));
				return;
			case DataType.Table:
				JsonTableConverter.TableToJson(sb, value.Table);
				return;
			}
			sb.Append("null");
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x0028A7DC File Offset: 0x002889DC
		private static string EscapeString(string s)
		{
			s = s.Replace("\\", "\\\\");
			s = s.Replace("/", "\\/");
			s = s.Replace("\"", "\\\"");
			s = s.Replace("\f", "\\f");
			s = s.Replace("\b", "\\b");
			s = s.Replace("\n", "\\n");
			s = s.Replace("\r", "\\r");
			s = s.Replace("\t", "\\t");
			return "\"" + s + "\"";
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x000474F3 File Offset: 0x000456F3
		private static bool IsValueJsonCompatible(DynValue value)
		{
			return value.Type == DataType.Boolean || value.IsNil() || value.Type == DataType.Number || value.Type == DataType.String || value.Type == DataType.Table || JsonNull.IsJsonNull(value);
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x0028A88C File Offset: 0x00288A8C
		public static Table JsonToTable(string json, Script script = null)
		{
			Lexer lexer = new Lexer(0, json, false);
			if (lexer.Current.Type == TokenType.Brk_Open_Curly)
			{
				return JsonTableConverter.ParseJsonObject(lexer, script);
			}
			if (lexer.Current.Type == TokenType.Brk_Open_Square)
			{
				return JsonTableConverter.ParseJsonArray(lexer, script);
			}
			throw new SyntaxErrorException(lexer.Current, "Unexpected token : '{0}'", new object[]
			{
				lexer.Current.Text
			});
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x00047529 File Offset: 0x00045729
		private static void AssertToken(Lexer L, TokenType type)
		{
			if (L.Current.Type != type)
			{
				throw new SyntaxErrorException(L.Current, "Unexpected token : '{0}'", new object[]
				{
					L.Current.Text
				});
			}
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x0028A8F4 File Offset: 0x00288AF4
		private static Table ParseJsonArray(Lexer L, Script script)
		{
			Table table = new Table(script);
			L.Next();
			while (L.Current.Type != TokenType.Brk_Close_Square)
			{
				DynValue value = JsonTableConverter.ParseJsonValue(L, script);
				table.Append(value);
				L.Next();
				if (L.Current.Type == TokenType.Comma)
				{
					L.Next();
				}
			}
			return table;
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x0028A94C File Offset: 0x00288B4C
		private static Table ParseJsonObject(Lexer L, Script script)
		{
			Table table = new Table(script);
			L.Next();
			while (L.Current.Type != TokenType.Brk_Close_Curly)
			{
				JsonTableConverter.AssertToken(L, TokenType.String);
				string text = L.Current.Text;
				L.Next();
				JsonTableConverter.AssertToken(L, TokenType.Colon);
				L.Next();
				DynValue value = JsonTableConverter.ParseJsonValue(L, script);
				table.Set(text, value);
				L.Next();
				if (L.Current.Type == TokenType.Comma)
				{
					L.Next();
				}
			}
			return table;
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x0028A9CC File Offset: 0x00288BCC
		private static DynValue ParseJsonValue(Lexer L, Script script)
		{
			if (L.Current.Type == TokenType.Brk_Open_Curly)
			{
				return DynValue.NewTable(JsonTableConverter.ParseJsonObject(L, script));
			}
			if (L.Current.Type == TokenType.Brk_Open_Square)
			{
				return DynValue.NewTable(JsonTableConverter.ParseJsonArray(L, script));
			}
			if (L.Current.Type == TokenType.String)
			{
				return DynValue.NewString(L.Current.Text);
			}
			if (L.Current.Type == TokenType.Number)
			{
				return DynValue.NewNumber(L.Current.GetNumberValue()).AsReadOnly();
			}
			if (L.Current.Type == TokenType.True)
			{
				return DynValue.True;
			}
			if (L.Current.Type == TokenType.False)
			{
				return DynValue.False;
			}
			if (L.Current.Type == TokenType.Name && L.Current.Text == "null")
			{
				return JsonNull.Create();
			}
			throw new SyntaxErrorException(L.Current, "Unexpected token : '{0}'", new object[]
			{
				L.Current.Text
			});
		}
	}
}
