using System;
using System.Text;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter.Serialization.Json
{
	// Token: 0x02000CEF RID: 3311
	public static class JsonTableConverter
	{
		// Token: 0x06005CA7 RID: 23719 RVA: 0x002612B7 File Offset: 0x0025F4B7
		public static string TableToJson(this Table table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			JsonTableConverter.TableToJson(stringBuilder, table);
			return stringBuilder.ToString();
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x002612CC File Offset: 0x0025F4CC
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

		// Token: 0x06005CA9 RID: 23721 RVA: 0x002613D8 File Offset: 0x0025F5D8
		public static string ObjectToJson(object obj)
		{
			return ObjectValueConverter.SerializeObjectToDynValue(null, obj, JsonNull.Create()).Table.TableToJson();
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x002613F0 File Offset: 0x0025F5F0
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

		// Token: 0x06005CAB RID: 23723 RVA: 0x0026149C File Offset: 0x0025F69C
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

		// Token: 0x06005CAC RID: 23724 RVA: 0x00261549 File Offset: 0x0025F749
		private static bool IsValueJsonCompatible(DynValue value)
		{
			return value.Type == DataType.Boolean || value.IsNil() || value.Type == DataType.Number || value.Type == DataType.String || value.Type == DataType.Table || JsonNull.IsJsonNull(value);
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00261580 File Offset: 0x0025F780
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

		// Token: 0x06005CAE RID: 23726 RVA: 0x002615E8 File Offset: 0x0025F7E8
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

		// Token: 0x06005CAF RID: 23727 RVA: 0x00261620 File Offset: 0x0025F820
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

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00261678 File Offset: 0x0025F878
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

		// Token: 0x06005CB1 RID: 23729 RVA: 0x002616F8 File Offset: 0x0025F8F8
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
