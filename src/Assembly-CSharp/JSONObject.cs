using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class JSONObject
{
	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000384 RID: 900 RVA: 0x00007469 File Offset: 0x00005669
	public bool isContainer
	{
		get
		{
			return this.type == JSONObject.Type.ARRAY || this.type == JSONObject.Type.OBJECT;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000385 RID: 901 RVA: 0x0000747F File Offset: 0x0000567F
	public int Count
	{
		get
		{
			if (this.list == null)
			{
				return -1;
			}
			return this.list.Count;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000386 RID: 902 RVA: 0x00007496 File Offset: 0x00005696
	public string Str
	{
		get
		{
			if (string.IsNullOrEmpty(this.str))
			{
				return "";
			}
			return Regex.Unescape(this.str);
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000387 RID: 903 RVA: 0x000074B6 File Offset: 0x000056B6
	public int I
	{
		get
		{
			return (int)this.n;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000388 RID: 904 RVA: 0x000074BF File Offset: 0x000056BF
	public float f
	{
		get
		{
			return this.n;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000389 RID: 905 RVA: 0x000074C7 File Offset: 0x000056C7
	public static JSONObject nullJO
	{
		get
		{
			return JSONObject.Create(JSONObject.Type.NULL);
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600038A RID: 906 RVA: 0x000074CF File Offset: 0x000056CF
	public static JSONObject obj
	{
		get
		{
			return JSONObject.Create(JSONObject.Type.OBJECT);
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600038B RID: 907 RVA: 0x000074D7 File Offset: 0x000056D7
	public static JSONObject arr
	{
		get
		{
			return JSONObject.Create(JSONObject.Type.ARRAY);
		}
	}

	// Token: 0x0600038C RID: 908 RVA: 0x000074DF File Offset: 0x000056DF
	public JSONObject(JSONObject.Type t)
	{
		this.type = t;
		if (t != JSONObject.Type.OBJECT)
		{
			if (t == JSONObject.Type.ARRAY)
			{
				this.list = new List<JSONObject>();
				return;
			}
		}
		else
		{
			this.list = new List<JSONObject>();
			this.keys = new List<string>();
		}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00007518 File Offset: 0x00005718
	public JSONObject(bool b)
	{
		this.type = JSONObject.Type.BOOL;
		this.b = b;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0000752E File Offset: 0x0000572E
	public JSONObject(float f)
	{
		this.type = JSONObject.Type.NUMBER;
		this.n = f;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00007544 File Offset: 0x00005744
	public JSONObject(int i)
	{
		this.type = JSONObject.Type.NUMBER;
		this.i = (long)i;
		this.useInt = true;
		this.n = (float)i;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0000756A File Offset: 0x0000576A
	public JSONObject(long l)
	{
		this.type = JSONObject.Type.NUMBER;
		this.i = l;
		this.useInt = true;
		this.n = (float)l;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0006A718 File Offset: 0x00068918
	public JSONObject(Dictionary<string, string> dic)
	{
		this.type = JSONObject.Type.OBJECT;
		this.keys = new List<string>();
		this.list = new List<JSONObject>();
		foreach (KeyValuePair<string, string> keyValuePair in dic)
		{
			this.keys.Add(keyValuePair.Key);
			this.list.Add(JSONObject.CreateStringObject(keyValuePair.Value));
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0006A7AC File Offset: 0x000689AC
	public JSONObject(Dictionary<string, JSONObject> dic)
	{
		this.type = JSONObject.Type.OBJECT;
		this.keys = new List<string>();
		this.list = new List<JSONObject>();
		foreach (KeyValuePair<string, JSONObject> keyValuePair in dic)
		{
			this.keys.Add(keyValuePair.Key);
			this.list.Add(keyValuePair.Value);
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0000758F File Offset: 0x0000578F
	public JSONObject(JSONObject.AddJSONContents content)
	{
		content(this);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0000759E File Offset: 0x0000579E
	public JSONObject(JSONObject[] objs)
	{
		this.type = JSONObject.Type.ARRAY;
		this.list = new List<JSONObject>(objs);
	}

	// Token: 0x06000395 RID: 917 RVA: 0x000075B9 File Offset: 0x000057B9
	public static JSONObject StringObject(string val)
	{
		return JSONObject.CreateStringObject(val);
	}

	// Token: 0x06000396 RID: 918 RVA: 0x000075C1 File Offset: 0x000057C1
	public JSONObject Clone()
	{
		return base.MemberwiseClone() as JSONObject;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0006A83C File Offset: 0x00068A3C
	public void Absorb(JSONObject obj)
	{
		this.list.AddRange(obj.list);
		this.keys.AddRange(obj.keys);
		this.str = obj.str;
		this.n = obj.n;
		this.useInt = obj.useInt;
		this.i = obj.i;
		this.b = obj.b;
		this.type = obj.type;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x000075CE File Offset: 0x000057CE
	public static JSONObject Create()
	{
		return new JSONObject();
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0006A8B4 File Offset: 0x00068AB4
	public static JSONObject Create(JSONObject.Type t)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = t;
		if (t != JSONObject.Type.OBJECT)
		{
			if (t == JSONObject.Type.ARRAY)
			{
				jsonobject.list = new List<JSONObject>();
			}
		}
		else
		{
			jsonobject.list = new List<JSONObject>();
			jsonobject.keys = new List<string>();
		}
		return jsonobject;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000075D5 File Offset: 0x000057D5
	public static JSONObject Create(bool val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.BOOL;
		jsonobject.b = val;
		return jsonobject;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x000075EA File Offset: 0x000057EA
	public static JSONObject Create(float val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.NUMBER;
		jsonobject.n = val;
		return jsonobject;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000075FF File Offset: 0x000057FF
	public static JSONObject Create(int val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.NUMBER;
		jsonobject.n = (float)val;
		jsonobject.useInt = true;
		jsonobject.i = (long)val;
		return jsonobject;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00007624 File Offset: 0x00005824
	public static JSONObject Create(long val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.NUMBER;
		jsonobject.n = (float)val;
		jsonobject.useInt = true;
		jsonobject.i = val;
		return jsonobject;
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00007648 File Offset: 0x00005848
	public static JSONObject CreateStringObject(string val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.STRING;
		jsonobject.str = val;
		return jsonobject;
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0000765D File Offset: 0x0000585D
	public static JSONObject CreateBakedObject(string val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.BAKED;
		jsonobject.str = val;
		return jsonobject;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00007672 File Offset: 0x00005872
	public static JSONObject Create(string val, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.Parse(val, maxDepth, storeExcessLevels, strict);
		return jsonobject;
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0006A8FC File Offset: 0x00068AFC
	public static JSONObject Create(JSONObject.AddJSONContents content)
	{
		JSONObject jsonobject = JSONObject.Create();
		content(jsonobject);
		return jsonobject;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0006A918 File Offset: 0x00068B18
	public static JSONObject Create(Dictionary<string, string> dic)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.OBJECT;
		jsonobject.keys = new List<string>();
		jsonobject.list = new List<JSONObject>();
		foreach (KeyValuePair<string, string> keyValuePair in dic)
		{
			jsonobject.keys.Add(keyValuePair.Key);
			jsonobject.list.Add(JSONObject.CreateStringObject(keyValuePair.Value));
		}
		return jsonobject;
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0000403D File Offset: 0x0000223D
	public JSONObject()
	{
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00007683 File Offset: 0x00005883
	public JSONObject(string str, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
	{
		this.Parse(str, maxDepth, storeExcessLevels, strict);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0006A9AC File Offset: 0x00068BAC
	private void Parse(string str, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
	{
		if (string.IsNullOrEmpty(str))
		{
			this.type = JSONObject.Type.NULL;
			return;
		}
		str = str.Trim(JSONObject.WHITESPACE);
		if (strict && str[0] != '[' && str[0] != '{')
		{
			this.type = JSONObject.Type.NULL;
			return;
		}
		if (str.Length <= 0)
		{
			this.type = JSONObject.Type.NULL;
			return;
		}
		if (string.Compare(str, "true", true) == 0)
		{
			this.type = JSONObject.Type.BOOL;
			this.b = true;
			return;
		}
		if (string.Compare(str, "false", true) == 0)
		{
			this.type = JSONObject.Type.BOOL;
			this.b = false;
			return;
		}
		if (string.Compare(str, "null", true) == 0)
		{
			this.type = JSONObject.Type.NULL;
			return;
		}
		if (str == "\"INFINITY\"")
		{
			this.type = JSONObject.Type.NUMBER;
			this.n = float.PositiveInfinity;
			return;
		}
		if (str == "\"NEGINFINITY\"")
		{
			this.type = JSONObject.Type.NUMBER;
			this.n = float.NegativeInfinity;
			return;
		}
		if (str == "\"NaN\"")
		{
			this.type = JSONObject.Type.NUMBER;
			this.n = float.NaN;
			return;
		}
		if (str[0] == '"')
		{
			this.type = JSONObject.Type.STRING;
			this.str = str.Substring(1, str.Length - 2);
			return;
		}
		int num = 1;
		int num2 = 0;
		char c = str[num2];
		if (c != '[')
		{
			if (c != '{')
			{
				try
				{
					this.n = Convert.ToSingle(str);
					if (!str.Contains("."))
					{
						this.i = Convert.ToInt64(str);
						this.useInt = true;
					}
					this.type = JSONObject.Type.NUMBER;
				}
				catch (FormatException)
				{
					this.type = JSONObject.Type.NULL;
				}
				return;
			}
			this.type = JSONObject.Type.OBJECT;
			this.keys = new List<string>();
			this.list = new List<JSONObject>();
		}
		else
		{
			this.type = JSONObject.Type.ARRAY;
			this.list = new List<JSONObject>();
		}
		string item = "";
		bool flag = false;
		bool flag2 = false;
		int num3 = 0;
		while (++num2 < str.Length)
		{
			if (Array.IndexOf<char>(JSONObject.WHITESPACE, str[num2]) <= -1)
			{
				if (str[num2] == '\\')
				{
					num2++;
				}
				else
				{
					if (str[num2] == '"')
					{
						if (flag)
						{
							if (!flag2 && num3 == 0 && this.type == JSONObject.Type.OBJECT)
							{
								item = str.Substring(num + 1, num2 - num - 1);
							}
							flag = false;
						}
						else
						{
							if (num3 == 0 && this.type == JSONObject.Type.OBJECT)
							{
								num = num2;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						if (this.type == JSONObject.Type.OBJECT && num3 == 0 && str[num2] == ':')
						{
							num = num2 + 1;
							flag2 = true;
						}
						if (str[num2] == '[' || str[num2] == '{')
						{
							num3++;
						}
						else if (str[num2] == ']' || str[num2] == '}')
						{
							num3--;
						}
						if ((str[num2] == ',' && num3 == 0) || num3 < 0)
						{
							flag2 = false;
							string text = str.Substring(num, num2 - num).Trim(JSONObject.WHITESPACE);
							if (text.Length > 0)
							{
								if (this.type == JSONObject.Type.OBJECT)
								{
									this.keys.Add(item);
								}
								if (maxDepth != -1)
								{
									this.list.Add(JSONObject.Create(text, (maxDepth < -1) ? -2 : (maxDepth - 1), false, false));
								}
								else if (storeExcessLevels)
								{
									this.list.Add(JSONObject.CreateBakedObject(text));
								}
							}
							num = num2 + 1;
						}
					}
				}
			}
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060003A6 RID: 934 RVA: 0x00007696 File Offset: 0x00005896
	public bool IsNumber
	{
		get
		{
			return this.type == JSONObject.Type.NUMBER;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060003A7 RID: 935 RVA: 0x000076A1 File Offset: 0x000058A1
	public bool IsNull
	{
		get
		{
			return this.type == JSONObject.Type.NULL;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060003A8 RID: 936 RVA: 0x000076AC File Offset: 0x000058AC
	public bool IsString
	{
		get
		{
			return this.type == JSONObject.Type.STRING;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060003A9 RID: 937 RVA: 0x000076B7 File Offset: 0x000058B7
	public bool IsBool
	{
		get
		{
			return this.type == JSONObject.Type.BOOL;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060003AA RID: 938 RVA: 0x000076C2 File Offset: 0x000058C2
	public bool IsArray
	{
		get
		{
			return this.type == JSONObject.Type.ARRAY;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060003AB RID: 939 RVA: 0x000076CD File Offset: 0x000058CD
	public bool IsObject
	{
		get
		{
			return this.type == JSONObject.Type.OBJECT || this.type == JSONObject.Type.BAKED;
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000076E3 File Offset: 0x000058E3
	public void Add(bool val)
	{
		this.Add(JSONObject.Create(val));
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000076F1 File Offset: 0x000058F1
	public void Add(float val)
	{
		this.Add(JSONObject.Create(val));
	}

	// Token: 0x060003AE RID: 942 RVA: 0x000076FF File Offset: 0x000058FF
	public void Add(int val)
	{
		this.Add(JSONObject.Create(val));
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0000770D File Offset: 0x0000590D
	public void Add(string str)
	{
		this.Add(JSONObject.CreateStringObject(str));
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0000771B File Offset: 0x0000591B
	public void Add(JSONObject.AddJSONContents content)
	{
		this.Add(JSONObject.Create(content));
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00007729 File Offset: 0x00005929
	public void Add(JSONObject obj)
	{
		if (obj)
		{
			if (this.type != JSONObject.Type.ARRAY)
			{
				this.type = JSONObject.Type.ARRAY;
				if (this.list == null)
				{
					this.list = new List<JSONObject>();
				}
			}
			this.list.Add(obj);
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00007762 File Offset: 0x00005962
	public void AddField(string name, bool val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00007771 File Offset: 0x00005971
	public void AddField(string name, float val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00007780 File Offset: 0x00005980
	public void AddField(string name, int val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0000778F File Offset: 0x0000598F
	public void AddField(string name, long val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0000779E File Offset: 0x0000599E
	public void AddField(string name, JSONObject.AddJSONContents content)
	{
		this.AddField(name, JSONObject.Create(content));
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000077AD File Offset: 0x000059AD
	public void AddField(string name, string val)
	{
		this.AddField(name, JSONObject.CreateStringObject(val));
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0006AD00 File Offset: 0x00068F00
	public void AddField(string name, JSONObject obj)
	{
		if (obj)
		{
			if (this.type != JSONObject.Type.OBJECT)
			{
				if (this.keys == null)
				{
					this.keys = new List<string>();
				}
				if (this.type == JSONObject.Type.ARRAY)
				{
					for (int i = 0; i < this.list.Count; i++)
					{
						this.keys.Add(string.Concat(i));
					}
				}
				else if (this.list == null)
				{
					this.list = new List<JSONObject>();
				}
				this.type = JSONObject.Type.OBJECT;
			}
			this.keys.Add(name);
			this.list.Add(obj);
		}
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000077BC File Offset: 0x000059BC
	public void SetField(string name, string val)
	{
		this.SetField(name, JSONObject.CreateStringObject(val));
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000077CB File Offset: 0x000059CB
	public void SetField(string name, bool val)
	{
		this.SetField(name, JSONObject.Create(val));
	}

	// Token: 0x060003BB RID: 955 RVA: 0x000077DA File Offset: 0x000059DA
	public void SetField(string name, float val)
	{
		this.SetField(name, JSONObject.Create(val));
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000077E9 File Offset: 0x000059E9
	public void SetField(string name, int val)
	{
		this.SetField(name, JSONObject.Create(val));
	}

	// Token: 0x060003BD RID: 957 RVA: 0x000077F8 File Offset: 0x000059F8
	public void SetField(string name, JSONObject obj)
	{
		if (this.HasField(name))
		{
			this.list.Remove(this[name]);
			this.keys.Remove(name);
		}
		this.AddField(name, obj);
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0000782B File Offset: 0x00005A2B
	public void RemoveField(string name)
	{
		if (this.keys.IndexOf(name) > -1)
		{
			this.list.RemoveAt(this.keys.IndexOf(name));
			this.keys.Remove(name);
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00007860 File Offset: 0x00005A60
	public bool GetField(out bool field, string name, bool fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0000786E File Offset: 0x00005A6E
	public JSONObject TryGetField(string name)
	{
		if (this.HasField(name))
		{
			return this[name];
		}
		return new JSONObject();
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0006ADA0 File Offset: 0x00068FA0
	public bool GetField(ref bool field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.type == JSONObject.Type.OBJECT)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				field = this.list[num].b;
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00007886 File Offset: 0x00005A86
	public bool GetField(out float field, string name, float fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x0006ADE8 File Offset: 0x00068FE8
	public bool GetField(ref float field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.type == JSONObject.Type.OBJECT)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				field = this.list[num].n;
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00007894 File Offset: 0x00005A94
	public bool GetField(out int field, string name, int fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0006AE30 File Offset: 0x00069030
	public bool GetField(ref int field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.IsObject)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				field = (int)this.list[num].n;
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x000078A2 File Offset: 0x00005AA2
	public bool GetField(out long field, string name, long fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0006AE78 File Offset: 0x00069078
	public bool GetField(ref long field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.IsObject)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				field = (long)this.list[num].n;
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000078B0 File Offset: 0x00005AB0
	public bool GetField(out uint field, string name, uint fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0006AEC0 File Offset: 0x000690C0
	public bool GetField(ref uint field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.IsObject)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				field = (uint)this.list[num].n;
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x000078BE File Offset: 0x00005ABE
	public bool GetField(out string field, string name, string fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0006AF08 File Offset: 0x00069108
	public bool GetField(ref string field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.IsObject)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				field = this.list[num].str;
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0006AF50 File Offset: 0x00069150
	public void GetField(string name, JSONObject.GetFieldResponse response, JSONObject.FieldNotFound fail = null)
	{
		if (response != null && this.IsObject)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				response(this.list[num]);
				return;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0006AF98 File Offset: 0x00069198
	public JSONObject GetField(string name)
	{
		if (this.IsObject)
		{
			for (int i = 0; i < this.keys.Count; i++)
			{
				if (this.keys[i] == name)
				{
					return this.list[i];
				}
			}
		}
		return null;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0006AFE8 File Offset: 0x000691E8
	public bool HasFields(string[] names)
	{
		if (!this.IsObject)
		{
			return false;
		}
		for (int i = 0; i < names.Length; i++)
		{
			if (!this.keys.Contains(names[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x0006B020 File Offset: 0x00069220
	public bool HasField(string name)
	{
		if (!this.IsObject)
		{
			return false;
		}
		for (int i = 0; i < this.keys.Count; i++)
		{
			if (this.keys[i] == name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x0006B064 File Offset: 0x00069264
	public bool HasItem(int i)
	{
		return this.list.Find((JSONObject aa) => (int)aa.n == i) != null;
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0006B098 File Offset: 0x00069298
	public void Clear()
	{
		this.type = JSONObject.Type.NULL;
		if (this.list != null)
		{
			this.list.Clear();
		}
		if (this.keys != null)
		{
			this.keys.Clear();
		}
		this.str = "";
		this.n = 0f;
		this.b = false;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x000078CC File Offset: 0x00005ACC
	public JSONObject Copy()
	{
		return JSONObject.Create(this.Print(false), -2, false, false);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x000078DE File Offset: 0x00005ADE
	public void Merge(JSONObject obj)
	{
		JSONObject.MergeRecur(this, obj);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0006B0F0 File Offset: 0x000692F0
	private static void MergeRecur(JSONObject left, JSONObject right)
	{
		if (left.type == JSONObject.Type.NULL)
		{
			left.Absorb(right);
			return;
		}
		if (left.type == JSONObject.Type.OBJECT && right.type == JSONObject.Type.OBJECT)
		{
			for (int i = 0; i < right.list.Count; i++)
			{
				string text = right.keys[i];
				if (right[i].isContainer)
				{
					if (left.HasField(text))
					{
						JSONObject.MergeRecur(left[text], right[i]);
					}
					else
					{
						left.AddField(text, right[i]);
					}
				}
				else if (left.HasField(text))
				{
					left.SetField(text, right[i]);
				}
				else
				{
					left.AddField(text, right[i]);
				}
			}
			return;
		}
		if (left.type == JSONObject.Type.ARRAY && right.type == JSONObject.Type.ARRAY)
		{
			if (right.Count > left.Count)
			{
				return;
			}
			for (int j = 0; j < right.list.Count; j++)
			{
				if (left[j].type == right[j].type)
				{
					if (left[j].isContainer)
					{
						JSONObject.MergeRecur(left[j], right[j]);
					}
					else
					{
						left[j] = right[j];
					}
				}
			}
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x000078E7 File Offset: 0x00005AE7
	public void Bake()
	{
		if (this.type != JSONObject.Type.BAKED)
		{
			this.str = this.Print(false);
			this.type = JSONObject.Type.BAKED;
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00007906 File Offset: 0x00005B06
	public IEnumerable BakeAsync()
	{
		if (this.type != JSONObject.Type.BAKED)
		{
			foreach (string text in this.PrintAsync(false))
			{
				if (text == null)
				{
					yield return text;
				}
				else
				{
					this.str = text;
				}
			}
			IEnumerator<string> enumerator = null;
			this.type = JSONObject.Type.BAKED;
		}
		yield break;
		yield break;
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0006B234 File Offset: 0x00069434
	public string Print(bool pretty = false)
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.Stringify(0, stringBuilder, pretty);
		return stringBuilder.ToString();
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00007916 File Offset: 0x00005B16
	public IEnumerable<string> PrintAsync(bool pretty = false)
	{
		StringBuilder builder = new StringBuilder();
		JSONObject.printWatch.Reset();
		JSONObject.printWatch.Start();
		foreach (object obj in this.StringifyAsync(0, builder, pretty))
		{
			IEnumerable enumerable = (IEnumerable)obj;
			yield return null;
		}
		IEnumerator enumerator = null;
		yield return builder.ToString();
		yield break;
		yield break;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0000792D File Offset: 0x00005B2D
	private IEnumerable StringifyAsync(int depth, StringBuilder builder, bool pretty = false)
	{
		int num = depth;
		depth = num + 1;
		if (num > 100)
		{
			yield break;
		}
		if (JSONObject.printWatch.Elapsed.TotalSeconds > 0.00800000037997961)
		{
			JSONObject.printWatch.Reset();
			yield return null;
			JSONObject.printWatch.Start();
		}
		switch (this.type)
		{
		case JSONObject.Type.NULL:
			builder.Append("null");
			break;
		case JSONObject.Type.STRING:
			builder.AppendFormat("\"{0}\"", this.str);
			break;
		case JSONObject.Type.NUMBER:
			if (this.useInt)
			{
				builder.Append(this.i.ToString());
			}
			else if (float.IsInfinity(this.n))
			{
				builder.Append("\"INFINITY\"");
			}
			else if (float.IsNegativeInfinity(this.n))
			{
				builder.Append("\"NEGINFINITY\"");
			}
			else if (float.IsNaN(this.n))
			{
				builder.Append("\"NaN\"");
			}
			else
			{
				builder.Append(this.n.ToString());
			}
			break;
		case JSONObject.Type.OBJECT:
			builder.Append("{");
			if (this.list.Count > 0)
			{
				if (pretty)
				{
					builder.Append("\r\n");
				}
				for (int i = 0; i < this.list.Count; i = num + 1)
				{
					string arg = this.keys[i];
					JSONObject jsonobject = this.list[i];
					if (jsonobject)
					{
						if (pretty)
						{
							for (int j = 0; j < depth; j++)
							{
								builder.Append("\t");
							}
						}
						builder.AppendFormat("\"{0}\":", arg);
						foreach (object obj in jsonobject.StringifyAsync(depth, builder, pretty))
						{
							IEnumerable enumerable = (IEnumerable)obj;
							yield return enumerable;
						}
						IEnumerator enumerator = null;
						builder.Append(",");
						if (pretty)
						{
							builder.Append("\r\n");
						}
					}
					num = i;
				}
				if (pretty)
				{
					builder.Length -= 2;
				}
				else
				{
					num = builder.Length;
					builder.Length = num - 1;
				}
			}
			if (pretty && this.list.Count > 0)
			{
				builder.Append("\r\n");
				for (int k = 0; k < depth - 1; k++)
				{
					builder.Append("\t");
				}
			}
			builder.Append("}");
			break;
		case JSONObject.Type.ARRAY:
			builder.Append("[");
			if (this.list.Count > 0)
			{
				if (pretty)
				{
					builder.Append("\r\n");
				}
				for (int i = 0; i < this.list.Count; i = num + 1)
				{
					if (this.list[i])
					{
						if (pretty)
						{
							for (int l = 0; l < depth; l++)
							{
								builder.Append("\t");
							}
						}
						foreach (object obj2 in this.list[i].StringifyAsync(depth, builder, pretty))
						{
							IEnumerable enumerable2 = (IEnumerable)obj2;
							yield return enumerable2;
						}
						IEnumerator enumerator = null;
						builder.Append(",");
						if (pretty)
						{
							builder.Append("\r\n");
						}
					}
					num = i;
				}
				if (pretty)
				{
					builder.Length -= 2;
				}
				else
				{
					num = builder.Length;
					builder.Length = num - 1;
				}
			}
			if (pretty && this.list.Count > 0)
			{
				builder.Append("\r\n");
				for (int m = 0; m < depth - 1; m++)
				{
					builder.Append("\t");
				}
			}
			builder.Append("]");
			break;
		case JSONObject.Type.BOOL:
			if (this.b)
			{
				builder.Append("true");
			}
			else
			{
				builder.Append("false");
			}
			break;
		case JSONObject.Type.BAKED:
			builder.Append(this.str);
			break;
		}
		yield break;
		yield break;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0006B258 File Offset: 0x00069458
	private void Stringify(int depth, StringBuilder builder, bool pretty = false)
	{
		if (depth++ > 100)
		{
			return;
		}
		switch (this.type)
		{
		case JSONObject.Type.NULL:
			builder.Append("null");
			return;
		case JSONObject.Type.STRING:
			builder.AppendFormat("\"{0}\"", this.str);
			return;
		case JSONObject.Type.NUMBER:
			if (this.useInt)
			{
				builder.Append(this.i.ToString());
				return;
			}
			if (float.IsInfinity(this.n))
			{
				builder.Append("\"INFINITY\"");
				return;
			}
			if (float.IsNegativeInfinity(this.n))
			{
				builder.Append("\"NEGINFINITY\"");
				return;
			}
			if (float.IsNaN(this.n))
			{
				builder.Append("\"NaN\"");
				return;
			}
			builder.Append(this.n.ToString());
			return;
		case JSONObject.Type.OBJECT:
			builder.Append("{");
			if (this.list.Count > 0)
			{
				if (pretty)
				{
					builder.Append("\n");
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					string arg = this.keys[i];
					JSONObject jsonobject = this.list[i];
					if (jsonobject)
					{
						if (pretty)
						{
							for (int j = 0; j < depth; j++)
							{
								builder.Append("\t");
							}
						}
						builder.AppendFormat("\"{0}\":", arg);
						jsonobject.Stringify(depth, builder, pretty);
						builder.Append(",");
						if (pretty)
						{
							builder.Append("\n");
						}
					}
				}
				if (pretty)
				{
					builder.Length -= 2;
				}
				else
				{
					int length = builder.Length;
					builder.Length = length - 1;
				}
			}
			if (pretty && this.list.Count > 0)
			{
				builder.Append("\n");
				for (int k = 0; k < depth - 1; k++)
				{
					builder.Append("\t");
				}
			}
			builder.Append("}");
			return;
		case JSONObject.Type.ARRAY:
			builder.Append("[");
			if (this.list.Count > 0)
			{
				if (pretty)
				{
					builder.Append("\n");
				}
				for (int l = 0; l < this.list.Count; l++)
				{
					if (this.list[l])
					{
						if (pretty)
						{
							for (int m = 0; m < depth; m++)
							{
								builder.Append("\t");
							}
						}
						this.list[l].Stringify(depth, builder, pretty);
						builder.Append(",");
						if (pretty)
						{
							builder.Append("\n");
						}
					}
				}
				if (pretty)
				{
					builder.Length -= 2;
				}
				else
				{
					int length = builder.Length;
					builder.Length = length - 1;
				}
			}
			if (pretty && this.list.Count > 0)
			{
				builder.Append("\n");
				for (int n = 0; n < depth - 1; n++)
				{
					builder.Append("\t");
				}
			}
			builder.Append("]");
			return;
		case JSONObject.Type.BOOL:
			if (this.b)
			{
				builder.Append("true");
				return;
			}
			builder.Append("false");
			return;
		case JSONObject.Type.BAKED:
			builder.Append(this.str);
			return;
		default:
			return;
		}
	}

	// Token: 0x1700006F RID: 111
	public JSONObject this[int index]
	{
		get
		{
			if (this.list.Count > index)
			{
				return this.list[index];
			}
			return null;
		}
		set
		{
			if (this.list.Count > index)
			{
				this.list[index] = value;
			}
		}
	}

	// Token: 0x17000070 RID: 112
	public JSONObject this[string index]
	{
		get
		{
			return this.GetField(index);
		}
		set
		{
			this.SetField(index, value);
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x000079A0 File Offset: 0x00005BA0
	public override string ToString()
	{
		return this.Print(false);
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x000079A9 File Offset: 0x00005BA9
	public string ToString(bool pretty)
	{
		return this.Print(pretty);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0006B5A0 File Offset: 0x000697A0
	public Dictionary<string, string> ToDictionary()
	{
		if (this.type == JSONObject.Type.OBJECT)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < this.list.Count; i++)
			{
				JSONObject jsonobject = this.list[i];
				switch (jsonobject.type)
				{
				case JSONObject.Type.STRING:
					dictionary.Add(this.keys[i], jsonobject.str);
					break;
				case JSONObject.Type.NUMBER:
					dictionary.Add(this.keys[i], string.Concat(jsonobject.n));
					break;
				case JSONObject.Type.BOOL:
					dictionary.Add(this.keys[i], jsonobject.b.ToString() ?? "");
					break;
				}
			}
			return dictionary;
		}
		return null;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0006B674 File Offset: 0x00069874
	public List<int> ToList()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.Count; i++)
		{
			list.Add(this[i].I);
		}
		return list;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x000079B2 File Offset: 0x00005BB2
	public static implicit operator bool(JSONObject o)
	{
		return o != null;
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000079B8 File Offset: 0x00005BB8
	public void LogString()
	{
		Debug.Log(this.ToString());
	}

	// Token: 0x04000222 RID: 546
	private const int MAX_DEPTH = 100;

	// Token: 0x04000223 RID: 547
	private const string INFINITY = "\"INFINITY\"";

	// Token: 0x04000224 RID: 548
	private const string NEGINFINITY = "\"NEGINFINITY\"";

	// Token: 0x04000225 RID: 549
	private const string NaN = "\"NaN\"";

	// Token: 0x04000226 RID: 550
	private const string NEWLINE = "\r\n";

	// Token: 0x04000227 RID: 551
	public static readonly char[] WHITESPACE = new char[]
	{
		' ',
		'\r',
		'\n',
		'\t',
		'﻿',
		'\t'
	};

	// Token: 0x04000228 RID: 552
	public JSONObject.Type type;

	// Token: 0x04000229 RID: 553
	public List<JSONObject> list;

	// Token: 0x0400022A RID: 554
	public List<string> keys;

	// Token: 0x0400022B RID: 555
	public string str;

	// Token: 0x0400022C RID: 556
	public float n;

	// Token: 0x0400022D RID: 557
	public bool useInt;

	// Token: 0x0400022E RID: 558
	public long i;

	// Token: 0x0400022F RID: 559
	public bool b;

	// Token: 0x04000230 RID: 560
	private const float maxFrameTime = 0.008f;

	// Token: 0x04000231 RID: 561
	private static readonly Stopwatch printWatch = new Stopwatch();

	// Token: 0x02000036 RID: 54
	public enum Type
	{
		// Token: 0x04000233 RID: 563
		NULL,
		// Token: 0x04000234 RID: 564
		STRING,
		// Token: 0x04000235 RID: 565
		NUMBER,
		// Token: 0x04000236 RID: 566
		OBJECT,
		// Token: 0x04000237 RID: 567
		ARRAY,
		// Token: 0x04000238 RID: 568
		BOOL,
		// Token: 0x04000239 RID: 569
		BAKED
	}

	// Token: 0x02000037 RID: 55
	// (Invoke) Token: 0x060003E7 RID: 999
	public delegate void AddJSONContents(JSONObject self);

	// Token: 0x02000038 RID: 56
	// (Invoke) Token: 0x060003EB RID: 1003
	public delegate void FieldNotFound(string name);

	// Token: 0x02000039 RID: 57
	// (Invoke) Token: 0x060003EF RID: 1007
	public delegate void GetFieldResponse(JSONObject obj);
}
