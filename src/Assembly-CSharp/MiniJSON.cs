using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// Token: 0x020007A9 RID: 1961
public class MiniJSON
{
	// Token: 0x060031DD RID: 12765 RVA: 0x0018C5E4 File Offset: 0x0018A7E4
	public static object jsonDecode(string json)
	{
		MiniJSON.lastDecode = json;
		if (json == null)
		{
			return null;
		}
		char[] json2 = json.ToCharArray();
		int num = 0;
		bool flag = true;
		object result = MiniJSON.parseValue(json2, ref num, ref flag);
		if (flag)
		{
			MiniJSON.lastErrorIndex = -1;
			return result;
		}
		MiniJSON.lastErrorIndex = num;
		return result;
	}

	// Token: 0x060031DE RID: 12766 RVA: 0x0018C620 File Offset: 0x0018A820
	public static string jsonEncode(object json)
	{
		MiniJSON.indentLevel = 0;
		StringBuilder stringBuilder = new StringBuilder(2000);
		if (!MiniJSON.serializeValue(json, stringBuilder))
		{
			return null;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060031DF RID: 12767 RVA: 0x00024705 File Offset: 0x00022905
	public static bool lastDecodeSuccessful()
	{
		return MiniJSON.lastErrorIndex == -1;
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x0002470F File Offset: 0x0002290F
	public static int getLastErrorIndex()
	{
		return MiniJSON.lastErrorIndex;
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x0018C650 File Offset: 0x0018A850
	public static string getLastErrorSnippet()
	{
		if (MiniJSON.lastErrorIndex == -1)
		{
			return "";
		}
		int num = MiniJSON.lastErrorIndex - 5;
		int num2 = MiniJSON.lastErrorIndex + 15;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 >= MiniJSON.lastDecode.Length)
		{
			num2 = MiniJSON.lastDecode.Length - 1;
		}
		return MiniJSON.lastDecode.Substring(num, num2 - num + 1);
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x0018C6AC File Offset: 0x0018A8AC
	protected static Hashtable parseObject(char[] json, ref int index)
	{
		Hashtable hashtable = new Hashtable();
		MiniJSON.nextToken(json, ref index);
		bool flag = false;
		while (!flag)
		{
			int num = MiniJSON.lookAhead(json, index);
			if (num == 0)
			{
				return null;
			}
			if (num == 6)
			{
				MiniJSON.nextToken(json, ref index);
			}
			else
			{
				if (num == 2)
				{
					MiniJSON.nextToken(json, ref index);
					return hashtable;
				}
				string text = MiniJSON.parseString(json, ref index);
				if (text == null)
				{
					return null;
				}
				num = MiniJSON.nextToken(json, ref index);
				if (num != 5)
				{
					return null;
				}
				bool flag2 = true;
				object value = MiniJSON.parseValue(json, ref index, ref flag2);
				if (!flag2)
				{
					return null;
				}
				hashtable[text] = value;
			}
		}
		return hashtable;
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x0018C734 File Offset: 0x0018A934
	protected static ArrayList parseArray(char[] json, ref int index)
	{
		ArrayList arrayList = new ArrayList();
		MiniJSON.nextToken(json, ref index);
		bool flag = false;
		while (!flag)
		{
			int num = MiniJSON.lookAhead(json, index);
			if (num == 0)
			{
				return null;
			}
			if (num == 6)
			{
				MiniJSON.nextToken(json, ref index);
			}
			else
			{
				if (num == 4)
				{
					MiniJSON.nextToken(json, ref index);
					break;
				}
				bool flag2 = true;
				object value = MiniJSON.parseValue(json, ref index, ref flag2);
				if (!flag2)
				{
					return null;
				}
				arrayList.Add(value);
			}
		}
		return arrayList;
	}

	// Token: 0x060031E4 RID: 12772 RVA: 0x0018C79C File Offset: 0x0018A99C
	protected static object parseValue(char[] json, ref int index, ref bool success)
	{
		switch (MiniJSON.lookAhead(json, index))
		{
		case 1:
			return MiniJSON.parseObject(json, ref index);
		case 3:
			return MiniJSON.parseArray(json, ref index);
		case 7:
			return MiniJSON.parseString(json, ref index);
		case 8:
			return MiniJSON.parseNumber(json, ref index);
		case 9:
			MiniJSON.nextToken(json, ref index);
			return bool.Parse("TRUE");
		case 10:
			MiniJSON.nextToken(json, ref index);
			return bool.Parse("FALSE");
		case 11:
			MiniJSON.nextToken(json, ref index);
			return null;
		case 12:
			MiniJSON.nextToken(json, ref index);
			return float.PositiveInfinity;
		}
		success = false;
		return null;
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x0018C864 File Offset: 0x0018AA64
	protected static string parseString(char[] json, ref int index)
	{
		string text = "";
		MiniJSON.eatWhitespace(json, ref index);
		int num = index;
		index = num + 1;
		char c = json[num];
		bool flag = false;
		while (!flag && index != json.Length)
		{
			num = index;
			index = num + 1;
			c = json[num];
			if (c == '"')
			{
				flag = true;
				break;
			}
			if (c == '\\')
			{
				if (index == json.Length)
				{
					break;
				}
				num = index;
				index = num + 1;
				c = json[num];
				if (c == '"')
				{
					text += "\"";
				}
				else if (c == '\\')
				{
					text += "\\";
				}
				else if (c == '/')
				{
					text += "/";
				}
				else if (c == 'b')
				{
					text += "\b";
				}
				else if (c == 'f')
				{
					text += "\f";
				}
				else if (c == 'n')
				{
					text += "\n";
				}
				else if (c == 'r')
				{
					text += "\r";
				}
				else if (c == 't')
				{
					text += "\t";
				}
				else if (c == 'u')
				{
					if (json.Length - index < 4)
					{
						break;
					}
					char[] array = new char[4];
					Array.Copy(json, index, array, 0, 4);
					text = text + "&#x" + new string(array) + ";";
					index += 4;
				}
			}
			else
			{
				text += c.ToString();
			}
		}
		if (!flag)
		{
			return null;
		}
		return text;
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x0018C9D4 File Offset: 0x0018ABD4
	protected static float parseNumber(char[] json, ref int index)
	{
		MiniJSON.eatWhitespace(json, ref index);
		int lastIndexOfNumber = MiniJSON.getLastIndexOfNumber(json, index);
		int num = lastIndexOfNumber - index + 1;
		char[] array = new char[num];
		Array.Copy(json, index, array, 0, num);
		index = lastIndexOfNumber + 1;
		return float.Parse(new string(array));
	}

	// Token: 0x060031E7 RID: 12775 RVA: 0x0018CA1C File Offset: 0x0018AC1C
	protected static int getLastIndexOfNumber(char[] json, int index)
	{
		int num = index;
		while (num < json.Length && "0123456789+-.eE".IndexOf(json[num]) != -1)
		{
			num++;
		}
		return num - 1;
	}

	// Token: 0x060031E8 RID: 12776 RVA: 0x00024716 File Offset: 0x00022916
	protected static void eatWhitespace(char[] json, ref int index)
	{
		while (index < json.Length && " \t\n\r".IndexOf(json[index]) != -1)
		{
			index++;
		}
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x0018CA4C File Offset: 0x0018AC4C
	protected static int lookAhead(char[] json, int index)
	{
		int num = index;
		return MiniJSON.nextToken(json, ref num);
	}

	// Token: 0x060031EA RID: 12778 RVA: 0x0018CA64 File Offset: 0x0018AC64
	protected static int nextToken(char[] json, ref int index)
	{
		MiniJSON.eatWhitespace(json, ref index);
		if (index == json.Length)
		{
			return 0;
		}
		char c = json[index];
		index++;
		if (c <= '[')
		{
			switch (c)
			{
			case '"':
				return 7;
			case '#':
			case '$':
			case '%':
			case '&':
			case '\'':
			case '(':
			case ')':
			case '*':
			case '+':
			case '.':
			case '/':
				break;
			case ',':
				return 6;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return 8;
			case ':':
				return 5;
			default:
				if (c == '[')
				{
					return 3;
				}
				break;
			}
		}
		else
		{
			if (c == ']')
			{
				return 4;
			}
			if (c == '{')
			{
				return 1;
			}
			if (c == '}')
			{
				return 2;
			}
		}
		index--;
		int num = json.Length - index;
		if (num >= 8 && json[index] == 'I' && json[index + 1] == 'n' && json[index + 2] == 'f' && json[index + 3] == 'i' && json[index + 4] == 'n' && json[index + 5] == 'i' && json[index + 6] == 't' && json[index + 7] == 'y')
		{
			index += 8;
			return 12;
		}
		if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
		{
			index += 5;
			return 10;
		}
		if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
		{
			index += 4;
			return 9;
		}
		if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
		{
			index += 4;
			return 11;
		}
		return 0;
	}

	// Token: 0x060031EB RID: 12779 RVA: 0x00024738 File Offset: 0x00022938
	protected static bool serializeObjectOrArray(object objectOrArray, StringBuilder builder)
	{
		if (objectOrArray is Hashtable)
		{
			return MiniJSON.serializeObject((Hashtable)objectOrArray, builder);
		}
		return objectOrArray is ArrayList && MiniJSON.serializeArray((ArrayList)objectOrArray, builder);
	}

	// Token: 0x060031EC RID: 12780 RVA: 0x0018CC34 File Offset: 0x0018AE34
	protected static bool serializeObject(Hashtable anObject, StringBuilder builder)
	{
		MiniJSON.indentLevel++;
		string text = "";
		for (int i = 0; i < MiniJSON.indentLevel; i++)
		{
			text += "\t";
		}
		builder.Append("{\n" + text);
		IDictionaryEnumerator enumerator = anObject.GetEnumerator();
		bool flag = true;
		while (enumerator.MoveNext())
		{
			string aString = enumerator.Key.ToString();
			object value = enumerator.Value;
			if (!flag)
			{
				builder.Append(", \n" + text);
			}
			MiniJSON.serializeString(aString, builder);
			builder.Append(":");
			if (!MiniJSON.serializeValue(value, builder))
			{
				return false;
			}
			flag = false;
		}
		text = "";
		for (int j = 0; j < MiniJSON.indentLevel - 1; j++)
		{
			text += "\t";
		}
		builder.Append("\n" + text + "}");
		MiniJSON.indentLevel--;
		return true;
	}

	// Token: 0x060031ED RID: 12781 RVA: 0x0018CD28 File Offset: 0x0018AF28
	protected static bool serializeDictionary(Dictionary<string, string> dict, StringBuilder builder)
	{
		builder.Append("{");
		bool flag = true;
		foreach (KeyValuePair<string, string> keyValuePair in dict)
		{
			if (!flag)
			{
				builder.Append(", ");
			}
			MiniJSON.serializeString(keyValuePair.Key, builder);
			builder.Append(":");
			MiniJSON.serializeString(keyValuePair.Value, builder);
			flag = false;
		}
		builder.Append("}");
		return true;
	}

	// Token: 0x060031EE RID: 12782 RVA: 0x0018CDC4 File Offset: 0x0018AFC4
	protected static bool serializeArray(ArrayList anArray, StringBuilder builder)
	{
		MiniJSON.indentLevel++;
		string text = "";
		for (int i = 0; i < MiniJSON.indentLevel; i++)
		{
			text += "\t";
		}
		builder.Append("[\n" + text);
		bool flag = true;
		for (int j = 0; j < anArray.Count; j++)
		{
			object value = anArray[j];
			if (!flag)
			{
				builder.Append(", \n" + text);
			}
			if (!MiniJSON.serializeValue(value, builder))
			{
				return false;
			}
			flag = false;
		}
		text = "";
		for (int k = 0; k < MiniJSON.indentLevel - 1; k++)
		{
			text += "\t";
		}
		builder.Append("\n" + text + "]");
		MiniJSON.indentLevel--;
		return true;
	}

	// Token: 0x060031EF RID: 12783 RVA: 0x0018CE98 File Offset: 0x0018B098
	protected static bool serializeValue(object value, StringBuilder builder)
	{
		if (value == null)
		{
			builder.Append("null");
		}
		else if (value.GetType().IsArray)
		{
			MiniJSON.serializeArray(new ArrayList((ICollection)value), builder);
		}
		else if (value is string)
		{
			MiniJSON.serializeString((string)value, builder);
		}
		else if (value is char)
		{
			MiniJSON.serializeString(Convert.ToString((char)value), builder);
		}
		else if (value is Hashtable)
		{
			MiniJSON.serializeObject((Hashtable)value, builder);
		}
		else if (value is Dictionary<string, string>)
		{
			MiniJSON.serializeDictionary((Dictionary<string, string>)value, builder);
		}
		else if (value is ArrayList)
		{
			MiniJSON.serializeArray((ArrayList)value, builder);
		}
		else if (value is bool && (bool)value)
		{
			builder.Append("true");
		}
		else if (value is bool && !(bool)value)
		{
			builder.Append("false");
		}
		else
		{
			if (!value.GetType().IsPrimitive)
			{
				return false;
			}
			MiniJSON.serializeNumber(Convert.ToSingle(value), builder);
		}
		return true;
	}

	// Token: 0x060031F0 RID: 12784 RVA: 0x0018CFB8 File Offset: 0x0018B1B8
	protected static void serializeString(string aString, StringBuilder builder)
	{
		builder.Append("\"");
		foreach (char c in aString.ToCharArray())
		{
			if (c == '"')
			{
				builder.Append("\\\"");
			}
			else if (c == '\\')
			{
				builder.Append("\\\\");
			}
			else if (c == '\b')
			{
				builder.Append("\\b");
			}
			else if (c == '\f')
			{
				builder.Append("\\f");
			}
			else if (c == '\n')
			{
				builder.Append("\\n");
			}
			else if (c == '\r')
			{
				builder.Append("\\r");
			}
			else if (c == '\t')
			{
				builder.Append("\\t");
			}
			else
			{
				int num = Convert.ToInt32(c);
				if (num >= 32 && num <= 126)
				{
					builder.Append(c);
				}
				else
				{
					builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
				}
			}
		}
		builder.Append("\"");
	}

	// Token: 0x060031F1 RID: 12785 RVA: 0x00024765 File Offset: 0x00022965
	protected static void serializeNumber(float number, StringBuilder builder)
	{
		builder.Append(Convert.ToString(number));
	}

	// Token: 0x04002E17 RID: 11799
	private const int TOKEN_NONE = 0;

	// Token: 0x04002E18 RID: 11800
	private const int TOKEN_CURLY_OPEN = 1;

	// Token: 0x04002E19 RID: 11801
	private const int TOKEN_CURLY_CLOSE = 2;

	// Token: 0x04002E1A RID: 11802
	private const int TOKEN_SQUARED_OPEN = 3;

	// Token: 0x04002E1B RID: 11803
	private const int TOKEN_SQUARED_CLOSE = 4;

	// Token: 0x04002E1C RID: 11804
	private const int TOKEN_COLON = 5;

	// Token: 0x04002E1D RID: 11805
	private const int TOKEN_COMMA = 6;

	// Token: 0x04002E1E RID: 11806
	private const int TOKEN_STRING = 7;

	// Token: 0x04002E1F RID: 11807
	private const int TOKEN_NUMBER = 8;

	// Token: 0x04002E20 RID: 11808
	private const int TOKEN_TRUE = 9;

	// Token: 0x04002E21 RID: 11809
	private const int TOKEN_FALSE = 10;

	// Token: 0x04002E22 RID: 11810
	private const int TOKEN_NULL = 11;

	// Token: 0x04002E23 RID: 11811
	private const int TOKEN_INFINITY = 12;

	// Token: 0x04002E24 RID: 11812
	private const int BUILDER_CAPACITY = 2000;

	// Token: 0x04002E25 RID: 11813
	protected static int lastErrorIndex = -1;

	// Token: 0x04002E26 RID: 11814
	protected static string lastDecode = "";

	// Token: 0x04002E27 RID: 11815
	protected static int indentLevel = 0;
}
