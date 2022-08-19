using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x0200002A RID: 42
[Serializable]
public class JSONObject
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000366 RID: 870 RVA: 0x0001313D File Offset: 0x0001133D
	public bool isContainer
	{
		get
		{
			return this.type == JSONObject.Type.ARRAY || this.type == JSONObject.Type.OBJECT;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000367 RID: 871 RVA: 0x00013153 File Offset: 0x00011353
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

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000368 RID: 872 RVA: 0x0001316A File Offset: 0x0001136A
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

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000369 RID: 873 RVA: 0x0001318A File Offset: 0x0001138A
	public int I
	{
		get
		{
			return (int)this.i;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600036A RID: 874 RVA: 0x00013193 File Offset: 0x00011393
	public float f
	{
		get
		{
			return this.n;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600036B RID: 875 RVA: 0x0001319B File Offset: 0x0001139B
	public static JSONObject nullJO
	{
		get
		{
			return JSONObject.Create(JSONObject.Type.NULL);
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600036C RID: 876 RVA: 0x000131A3 File Offset: 0x000113A3
	public static JSONObject obj
	{
		get
		{
			return JSONObject.Create(JSONObject.Type.OBJECT);
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600036D RID: 877 RVA: 0x000131AB File Offset: 0x000113AB
	public static JSONObject arr
	{
		get
		{
			return JSONObject.Create(JSONObject.Type.ARRAY);
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x000131B3 File Offset: 0x000113B3
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

	// Token: 0x0600036F RID: 879 RVA: 0x000131EC File Offset: 0x000113EC
	public JSONObject(bool b)
	{
		this.type = JSONObject.Type.BOOL;
		this.b = b;
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00013202 File Offset: 0x00011402
	public JSONObject(float f)
	{
		this.type = JSONObject.Type.NUMBER;
		this.n = f;
		this.i = (long)this.n;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00013225 File Offset: 0x00011425
	public JSONObject(int i)
	{
		this.type = JSONObject.Type.NUMBER;
		this.i = (long)i;
		this.useInt = true;
		this.n = (float)i;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001324B File Offset: 0x0001144B
	public JSONObject(long l)
	{
		this.type = JSONObject.Type.NUMBER;
		this.i = l;
		this.useInt = true;
		this.n = (float)l;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00013270 File Offset: 0x00011470
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

	// Token: 0x06000374 RID: 884 RVA: 0x00013304 File Offset: 0x00011504
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

	// Token: 0x06000375 RID: 885 RVA: 0x00013394 File Offset: 0x00011594
	public JSONObject(JSONObject.AddJSONContents content)
	{
		content(this);
	}

	// Token: 0x06000376 RID: 886 RVA: 0x000133A3 File Offset: 0x000115A3
	public JSONObject(JSONObject[] objs)
	{
		this.type = JSONObject.Type.ARRAY;
		this.list = new List<JSONObject>(objs);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x000133BE File Offset: 0x000115BE
	public static JSONObject StringObject(string val)
	{
		return JSONObject.CreateStringObject(val);
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000133C6 File Offset: 0x000115C6
	public JSONObject Clone()
	{
		return base.MemberwiseClone() as JSONObject;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000133D4 File Offset: 0x000115D4
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

	// Token: 0x0600037A RID: 890 RVA: 0x0001344B File Offset: 0x0001164B
	public static JSONObject Create()
	{
		return new JSONObject();
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00013454 File Offset: 0x00011654
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

	// Token: 0x0600037C RID: 892 RVA: 0x0001349A File Offset: 0x0001169A
	public static JSONObject Create(bool val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.BOOL;
		jsonobject.b = val;
		return jsonobject;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000134AF File Offset: 0x000116AF
	public static JSONObject Create(float val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.NUMBER;
		jsonobject.n = val;
		return jsonobject;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000134C4 File Offset: 0x000116C4
	public static JSONObject Create(int val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.NUMBER;
		jsonobject.n = (float)val;
		jsonobject.useInt = true;
		jsonobject.i = (long)val;
		return jsonobject;
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000134E9 File Offset: 0x000116E9
	public static JSONObject Create(long val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.NUMBER;
		jsonobject.n = (float)val;
		jsonobject.useInt = true;
		jsonobject.i = val;
		return jsonobject;
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0001350D File Offset: 0x0001170D
	public static JSONObject CreateStringObject(string val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.STRING;
		jsonobject.str = val;
		return jsonobject;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00013522 File Offset: 0x00011722
	public static JSONObject CreateBakedObject(string val)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.type = JSONObject.Type.BAKED;
		jsonobject.str = val;
		return jsonobject;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00013537 File Offset: 0x00011737
	public static JSONObject Create(string val, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
	{
		JSONObject jsonobject = JSONObject.Create();
		jsonobject.Parse(val, maxDepth, storeExcessLevels, strict);
		return jsonobject;
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00013548 File Offset: 0x00011748
	public static JSONObject Create(JSONObject.AddJSONContents content)
	{
		JSONObject jsonobject = JSONObject.Create();
		content(jsonobject);
		return jsonobject;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00013564 File Offset: 0x00011764
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

	// Token: 0x06000385 RID: 901 RVA: 0x000027FC File Offset: 0x000009FC
	public JSONObject()
	{
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000135F8 File Offset: 0x000117F8
	public JSONObject(string str, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
	{
		this.Parse(str, maxDepth, storeExcessLevels, strict);
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0001360C File Offset: 0x0001180C
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
					this.i = (long)this.n;
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

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000388 RID: 904 RVA: 0x0001396C File Offset: 0x00011B6C
	public bool IsNumber
	{
		get
		{
			return this.type == JSONObject.Type.NUMBER;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000389 RID: 905 RVA: 0x00013977 File Offset: 0x00011B77
	public bool IsNull
	{
		get
		{
			return this.type == JSONObject.Type.NULL;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x0600038A RID: 906 RVA: 0x00013982 File Offset: 0x00011B82
	public bool IsString
	{
		get
		{
			return this.type == JSONObject.Type.STRING;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x0600038B RID: 907 RVA: 0x0001398D File Offset: 0x00011B8D
	public bool IsBool
	{
		get
		{
			return this.type == JSONObject.Type.BOOL;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600038C RID: 908 RVA: 0x00013998 File Offset: 0x00011B98
	public bool IsArray
	{
		get
		{
			return this.type == JSONObject.Type.ARRAY;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600038D RID: 909 RVA: 0x000139A3 File Offset: 0x00011BA3
	public bool IsObject
	{
		get
		{
			return this.type == JSONObject.Type.OBJECT || this.type == JSONObject.Type.BAKED;
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000139B9 File Offset: 0x00011BB9
	public void Add(bool val)
	{
		this.Add(JSONObject.Create(val));
	}

	// Token: 0x0600038F RID: 911 RVA: 0x000139C7 File Offset: 0x00011BC7
	public void Add(float val)
	{
		this.Add(JSONObject.Create(val));
	}

	// Token: 0x06000390 RID: 912 RVA: 0x000139D5 File Offset: 0x00011BD5
	public void Add(int val)
	{
		this.Add(JSONObject.Create(val));
	}

	// Token: 0x06000391 RID: 913 RVA: 0x000139E3 File Offset: 0x00011BE3
	public void Add(string str)
	{
		this.Add(JSONObject.CreateStringObject(str));
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000139F1 File Offset: 0x00011BF1
	public void Add(JSONObject.AddJSONContents content)
	{
		this.Add(JSONObject.Create(content));
	}

	// Token: 0x06000393 RID: 915 RVA: 0x000139FF File Offset: 0x00011BFF
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

	// Token: 0x06000394 RID: 916 RVA: 0x00013A38 File Offset: 0x00011C38
	public void AddField(string name, bool val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00013A47 File Offset: 0x00011C47
	public void AddField(string name, float val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00013A56 File Offset: 0x00011C56
	public void AddField(string name, int val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00013A65 File Offset: 0x00011C65
	public void AddField(string name, long val)
	{
		this.AddField(name, JSONObject.Create(val));
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00013A74 File Offset: 0x00011C74
	public void AddField(string name, JSONObject.AddJSONContents content)
	{
		this.AddField(name, JSONObject.Create(content));
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00013A83 File Offset: 0x00011C83
	public void AddField(string name, string val)
	{
		this.AddField(name, JSONObject.CreateStringObject(val));
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00013A94 File Offset: 0x00011C94
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

	// Token: 0x0600039B RID: 923 RVA: 0x00013B31 File Offset: 0x00011D31
	public void SetField(string name, string val)
	{
		this.SetField(name, JSONObject.CreateStringObject(val));
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00013B40 File Offset: 0x00011D40
	public void SetField(string name, bool val)
	{
		this.SetField(name, JSONObject.Create(val));
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00013B4F File Offset: 0x00011D4F
	public void SetField(string name, float val)
	{
		this.SetField(name, JSONObject.Create(val));
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00013B5E File Offset: 0x00011D5E
	public void SetField(string name, int val)
	{
		this.SetField(name, JSONObject.Create(val));
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00013B6D File Offset: 0x00011D6D
	public void SetField(string name, JSONObject obj)
	{
		if (this.HasField(name))
		{
			this.list.Remove(this[name]);
			this.keys.Remove(name);
		}
		this.AddField(name, obj);
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00013BA0 File Offset: 0x00011DA0
	public void RemoveField(string name)
	{
		if (this.keys.IndexOf(name) > -1)
		{
			this.list.RemoveAt(this.keys.IndexOf(name));
			this.keys.Remove(name);
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00013BD5 File Offset: 0x00011DD5
	public bool GetField(out bool field, string name, bool fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00013BE3 File Offset: 0x00011DE3
	public JSONObject TryGetField(string name)
	{
		if (this.HasField(name))
		{
			return this[name];
		}
		return new JSONObject();
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00013BFC File Offset: 0x00011DFC
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

	// Token: 0x060003A4 RID: 932 RVA: 0x00013C43 File Offset: 0x00011E43
	public bool GetField(out float field, string name, float fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00013C54 File Offset: 0x00011E54
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

	// Token: 0x060003A6 RID: 934 RVA: 0x00013C9B File Offset: 0x00011E9B
	public bool GetField(out int field, string name, int fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00013CAC File Offset: 0x00011EAC
	public bool GetField(ref int field, string name, JSONObject.FieldNotFound fail = null)
	{
		if (this.IsObject)
		{
			int num = this.keys.IndexOf(name);
			if (num >= 0)
			{
				if (this.list[num].n > 1000000f)
				{
					field = (int)this.list[num].i;
				}
				else
				{
					field = (int)this.list[num].n;
				}
				return true;
			}
		}
		if (fail != null)
		{
			fail(name);
		}
		return false;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00013D21 File Offset: 0x00011F21
	public bool GetField(out long field, string name, long fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00013D30 File Offset: 0x00011F30
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

	// Token: 0x060003AA RID: 938 RVA: 0x00013D77 File Offset: 0x00011F77
	public bool GetField(out uint field, string name, uint fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00013D88 File Offset: 0x00011F88
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

	// Token: 0x060003AC RID: 940 RVA: 0x00013DCF File Offset: 0x00011FCF
	public bool GetField(out string field, string name, string fallback)
	{
		field = fallback;
		return this.GetField(ref field, name, null);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00013DE0 File Offset: 0x00011FE0
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

	// Token: 0x060003AE RID: 942 RVA: 0x00013E28 File Offset: 0x00012028
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

	// Token: 0x060003AF RID: 943 RVA: 0x00013E70 File Offset: 0x00012070
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

	// Token: 0x060003B0 RID: 944 RVA: 0x00013EC0 File Offset: 0x000120C0
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

	// Token: 0x060003B1 RID: 945 RVA: 0x00013EF8 File Offset: 0x000120F8
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

	// Token: 0x060003B2 RID: 946 RVA: 0x00013F3C File Offset: 0x0001213C
	public bool HasItem(int i)
	{
		return this.list.Find((JSONObject aa) => (int)aa.i == i) != null;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00013F70 File Offset: 0x00012170
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

	// Token: 0x060003B4 RID: 948 RVA: 0x00013FC7 File Offset: 0x000121C7
	public JSONObject Copy()
	{
		return JSONObject.Create(this.Print(false), -2, false, false);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00013FD9 File Offset: 0x000121D9
	public void Merge(JSONObject obj)
	{
		JSONObject.MergeRecur(this, obj);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00013FE4 File Offset: 0x000121E4
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

	// Token: 0x060003B7 RID: 951 RVA: 0x00014125 File Offset: 0x00012325
	public void Bake()
	{
		if (this.type != JSONObject.Type.BAKED)
		{
			this.str = this.Print(false);
			this.type = JSONObject.Type.BAKED;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00014144 File Offset: 0x00012344
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

	// Token: 0x060003B9 RID: 953 RVA: 0x00014154 File Offset: 0x00012354
	public string Print(bool pretty = false)
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.Stringify(0, stringBuilder, pretty);
		return stringBuilder.ToString();
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00014176 File Offset: 0x00012376
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

	// Token: 0x060003BB RID: 955 RVA: 0x0001418D File Offset: 0x0001238D
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

	// Token: 0x060003BC RID: 956 RVA: 0x000141B4 File Offset: 0x000123B4
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

	// Token: 0x17000069 RID: 105
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

	// Token: 0x1700006A RID: 106
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

	// Token: 0x060003C1 RID: 961 RVA: 0x00014549 File Offset: 0x00012749
	public override string ToString()
	{
		return this.Print(false);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00014552 File Offset: 0x00012752
	public string ToString(bool pretty)
	{
		return this.Print(pretty);
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x0001455C File Offset: 0x0001275C
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

	// Token: 0x060003C4 RID: 964 RVA: 0x00014630 File Offset: 0x00012830
	public List<int> ToList()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.Count; i++)
		{
			list.Add(this[i].I);
		}
		return list;
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00014667 File Offset: 0x00012867
	public static implicit operator bool(JSONObject o)
	{
		return o != null;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x0001466D File Offset: 0x0001286D
	public void LogString()
	{
		Debug.Log(this.ToString());
	}

	// Token: 0x040001FE RID: 510
	private const int MAX_DEPTH = 100;

	// Token: 0x040001FF RID: 511
	private const string INFINITY = "\"INFINITY\"";

	// Token: 0x04000200 RID: 512
	private const string NEGINFINITY = "\"NEGINFINITY\"";

	// Token: 0x04000201 RID: 513
	private const string NaN = "\"NaN\"";

	// Token: 0x04000202 RID: 514
	private const string NEWLINE = "\r\n";

	// Token: 0x04000203 RID: 515
	public static readonly char[] WHITESPACE = new char[]
	{
		' ',
		'\r',
		'\n',
		'\t',
		'﻿',
		'\t'
	};

	// Token: 0x04000204 RID: 516
	public JSONObject.Type type;

	// Token: 0x04000205 RID: 517
	public List<JSONObject> list;

	// Token: 0x04000206 RID: 518
	public List<string> keys;

	// Token: 0x04000207 RID: 519
	public string str;

	// Token: 0x04000208 RID: 520
	public float n;

	// Token: 0x04000209 RID: 521
	public bool useInt;

	// Token: 0x0400020A RID: 522
	public long i;

	// Token: 0x0400020B RID: 523
	public bool b;

	// Token: 0x0400020C RID: 524
	private const float maxFrameTime = 0.008f;

	// Token: 0x0400020D RID: 525
	private static readonly Stopwatch printWatch = new Stopwatch();

	// Token: 0x020011CF RID: 4559
	public enum Type
	{
		// Token: 0x04006374 RID: 25460
		NULL,
		// Token: 0x04006375 RID: 25461
		STRING,
		// Token: 0x04006376 RID: 25462
		NUMBER,
		// Token: 0x04006377 RID: 25463
		OBJECT,
		// Token: 0x04006378 RID: 25464
		ARRAY,
		// Token: 0x04006379 RID: 25465
		BOOL,
		// Token: 0x0400637A RID: 25466
		BAKED
	}

	// Token: 0x020011D0 RID: 4560
	// (Invoke) Token: 0x060077D5 RID: 30677
	public delegate void AddJSONContents(JSONObject self);

	// Token: 0x020011D1 RID: 4561
	// (Invoke) Token: 0x060077D9 RID: 30681
	public delegate void FieldNotFound(string name);

	// Token: 0x020011D2 RID: 4562
	// (Invoke) Token: 0x060077DD RID: 30685
	public delegate void GetFieldResponse(JSONObject obj);
}
