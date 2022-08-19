﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public static class YSJSONHelper
{
	// Token: 0x060012B6 RID: 4790 RVA: 0x000749BC File Offset: 0x00072BBC
	public static string JSONObjectToClass(string name, JSONObject json, bool outToDebugPath = false)
	{
		string text = YSJSONHelper.Template ?? "";
		if (json == null)
		{
			return text;
		}
		if (json.IsNull)
		{
			return text;
		}
		if (json.list.Count == 0)
		{
			Debug.LogError("JSON生成类异常，" + name + "没有对应的json数据，请检查json文件是否正常。");
			return text;
		}
		try
		{
			JSONObject jsonobject = json.list[0];
			string text2 = jsonobject.keys[0];
			string newValue = "int";
			if (jsonobject[text2].IsString)
			{
				newValue = "string";
			}
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			foreach (string text9 in jsonobject.keys)
			{
				JSONObject jsonobject2 = jsonobject[text9];
				if (jsonobject2.IsNumber)
				{
					text3 += YSJSONHelper.INTTemplate.Replace("_FIELD_", text9);
					text6 += YSJSONHelper.INITINTTemplate.Replace("_FIELD_", text9);
				}
				else if (jsonobject2.IsString)
				{
					text4 += YSJSONHelper.STRINGTemplate.Replace("_FIELD_", text9);
					text7 += YSJSONHelper.INITSTRINGTemplate.Replace("_FIELD_", text9);
				}
				else if (jsonobject2.IsArray)
				{
					text5 += YSJSONHelper.LISTINTTemplate.Replace("_FIELD_", text9);
					text8 += YSJSONHelper.INITLISTINTTemplate.Replace("_FIELD_", text9);
				}
			}
			text = text.Replace("_CLASSNAME_", name);
			text = text.Replace("_IDNAME_", text2);
			text = text.Replace("_IDTYPE_", newValue);
			text = text.Replace("_INT_", text3);
			text = text.Replace("_STRING_", text4);
			text = text.Replace("_LISTINT_", text5);
			text = text.Replace("_INITINT_", text6);
			text = text.Replace("_INITSTRING_", text7);
			text = text.Replace("_INITLISTINT_", text8);
			string text10 = Application.dataPath + "/script/data/JSONClass/" + name + ".cs";
			if (outToDebugPath)
			{
				text10 = "C:\\Test/" + name + ".cs";
			}
			File.WriteAllText(text10, text);
			Debug.Log("生成了数据类" + name + ".cs，导出到了" + text10);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"生成数据类",
				name,
				"时发生异常:",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
		}
		return text;
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x00074CA8 File Offset: 0x00072EA8
	public static void SeidJSONObjectToClass(string name, JSONObject[] jsons, bool outToDebugPath = false)
	{
		if (jsons == null)
		{
			return;
		}
		if (jsons.Length == 0)
		{
			return;
		}
		for (int i = 1; i < jsons.Length; i++)
		{
			if (jsons[i] != null)
			{
				try
				{
					string text = YSJSONHelper.SeidTemplate ?? "";
					JSONObject jsonobject = jsons[i];
					if (jsonobject != null && !jsonobject.IsNull)
					{
						if (jsonobject.list.Count != 0)
						{
							JSONObject jsonobject2 = jsons[i].list[0];
							string text2 = jsonobject2.keys[0];
							string newValue = "int";
							if (jsonobject2[text2].IsString)
							{
								newValue = "string";
							}
							string text3 = "";
							string text4 = "";
							string text5 = "";
							string text6 = "";
							string text7 = "";
							string text8 = "";
							foreach (string text9 in jsonobject2.keys)
							{
								JSONObject jsonobject3 = jsonobject2[text9];
								if (jsonobject3.IsNumber)
								{
									text3 += YSJSONHelper.INTTemplate.Replace("_FIELD_", text9);
									text6 += YSJSONHelper.INITINTTemplate.Replace("_FIELD_", text9);
								}
								else if (jsonobject3.IsString)
								{
									text4 += YSJSONHelper.STRINGTemplate.Replace("_FIELD_", text9);
									text7 += YSJSONHelper.INITSTRINGTemplate.Replace("_FIELD_", text9);
								}
								else if (jsonobject3.IsArray)
								{
									text5 += YSJSONHelper.LISTINTTemplate.Replace("_FIELD_", text9);
									text8 += YSJSONHelper.INITLISTINTTemplate.Replace("_FIELD_", text9);
								}
							}
							text = text.Replace("_CLASSNAME_", string.Format("{0}{1}", name, i));
							text = text.Replace("_IDNAME_", text2);
							text = text.Replace("_IDTYPE_", newValue);
							text = text.Replace("_ARRAYNAME_", name ?? "");
							text = text.Replace("_SEIDID_", string.Format("{0}", i));
							text = text.Replace("_INT_", text3);
							text = text.Replace("_STRING_", text4);
							text = text.Replace("_LISTINT_", text5);
							text = text.Replace("_INITINT_", text6);
							text = text.Replace("_INITSTRING_", text7);
							text = text.Replace("_INITLISTINT_", text8);
							string text10 = Application.dataPath + "/script/data/JSONClass/" + name;
							if (outToDebugPath)
							{
								text10 = "C:\\Test/" + name;
							}
							if (!Directory.Exists(text10))
							{
								Directory.CreateDirectory(text10);
							}
							string text11 = string.Format("{0}/{1}{2}.cs", text10, name, i);
							File.WriteAllText(text11, text);
							Debug.Log(string.Format("生成了数据类{0}{1}.cs，导出到了{2}", name, i, text11));
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("生成数据类{0}{1}时发生异常,{2}\n{3}", new object[]
					{
						name,
						i,
						ex.Message,
						ex.StackTrace
					}));
				}
			}
		}
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x00075008 File Offset: 0x00073208
	public static void InitJSONClassData()
	{
		Type[] types = Assembly.GetAssembly(typeof(IJSONClass)).GetTypes();
		List<Type> list = new List<Type>();
		foreach (Type type in types)
		{
			if (!type.IsInterface)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int j = 0; j < interfaces.Length; j++)
				{
					if (interfaces[j] == typeof(IJSONClass))
					{
						list.Add(type);
					}
				}
			}
		}
		foreach (Type type2 in list)
		{
			MethodInfo method = type2.GetMethod("InitDataDict");
			if (method != null)
			{
				method.Invoke(null, null);
			}
		}
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x000750DC File Offset: 0x000732DC
	public static void LoadTemplate()
	{
		YSJSONHelper.Template = Resources.Load<TextAsset>("JSONClassTemplate").text;
		YSJSONHelper.SeidTemplate = Resources.Load<TextAsset>("JSONClassSeidTemplate").text;
	}

	// Token: 0x04000E54 RID: 3668
	private static string Template = "";

	// Token: 0x04000E55 RID: 3669
	private static string SeidTemplate = "";

	// Token: 0x04000E56 RID: 3670
	private static string INTTemplate = "        public int _FIELD_;\n";

	// Token: 0x04000E57 RID: 3671
	private static string STRINGTemplate = "        public string _FIELD_;\n";

	// Token: 0x04000E58 RID: 3672
	private static string LISTINTTemplate = "        public List<int> _FIELD_ = new List<int>();\n";

	// Token: 0x04000E59 RID: 3673
	private static string INITINTTemplate = "                    obj._FIELD_ = d[\"_FIELD_\"].I;\n";

	// Token: 0x04000E5A RID: 3674
	private static string INITSTRINGTemplate = "                    obj._FIELD_ = d[\"_FIELD_\"].Str;\n";

	// Token: 0x04000E5B RID: 3675
	private static string INITLISTINTTemplate = "                    obj._FIELD_ = d[\"_FIELD_\"].ToList();\n";
}
