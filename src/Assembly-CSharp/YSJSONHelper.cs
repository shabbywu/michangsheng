using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class YSJSONHelper
{
	private static string Template = "";

	private static string SeidTemplate = "";

	private static string INTTemplate = "        public int _FIELD_;\n";

	private static string STRINGTemplate = "        public string _FIELD_;\n";

	private static string LISTINTTemplate = "        public List<int> _FIELD_ = new List<int>();\n";

	private static string INITINTTemplate = "                    obj._FIELD_ = d[\"_FIELD_\"].I;\n";

	private static string INITSTRINGTemplate = "                    obj._FIELD_ = d[\"_FIELD_\"].Str;\n";

	private static string INITLISTINTTemplate = "                    obj._FIELD_ = d[\"_FIELD_\"].ToList();\n";

	public static string JSONObjectToClass(string name, JSONObject json, bool outToDebugPath = false)
	{
		string text = Template ?? "";
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
			Debug.LogError((object)("JSON生成类异常，" + name + "没有对应的json数据，请检查json文件是否正常。"));
			return text;
		}
		try
		{
			JSONObject jSONObject = json.list[0];
			string text2 = jSONObject.keys[0];
			string newValue = "int";
			if (jSONObject[text2].IsString)
			{
				newValue = "string";
			}
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			foreach (string key in jSONObject.keys)
			{
				JSONObject jSONObject2 = jSONObject[key];
				if (jSONObject2.IsNumber)
				{
					text3 += INTTemplate.Replace("_FIELD_", key);
					text6 += INITINTTemplate.Replace("_FIELD_", key);
				}
				else if (jSONObject2.IsString)
				{
					text4 += STRINGTemplate.Replace("_FIELD_", key);
					text7 += INITSTRINGTemplate.Replace("_FIELD_", key);
				}
				else if (jSONObject2.IsArray)
				{
					text5 += LISTINTTemplate.Replace("_FIELD_", key);
					text8 += INITLISTINTTemplate.Replace("_FIELD_", key);
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
			string text9 = Application.dataPath + "/script/data/JSONClass/" + name + ".cs";
			if (outToDebugPath)
			{
				text9 = "C:\\Test/" + name + ".cs";
			}
			File.WriteAllText(text9, text);
			Debug.Log((object)("生成了数据类" + name + ".cs，导出到了" + text9));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("生成数据类" + name + "时发生异常:" + ex.Message + "\n" + ex.StackTrace));
		}
		return text;
	}

	public static void SeidJSONObjectToClass(string name, JSONObject[] jsons, bool outToDebugPath = false)
	{
		if (jsons == null || jsons.Length == 0)
		{
			return;
		}
		for (int i = 1; i < jsons.Length; i++)
		{
			if (jsons[i] == null)
			{
				continue;
			}
			try
			{
				string text = SeidTemplate ?? "";
				JSONObject jSONObject = jsons[i];
				if (jSONObject == null || jSONObject.IsNull || jSONObject.list.Count == 0)
				{
					continue;
				}
				JSONObject jSONObject2 = jsons[i].list[0];
				string text2 = jSONObject2.keys[0];
				string newValue = "int";
				if (jSONObject2[text2].IsString)
				{
					newValue = "string";
				}
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				string text7 = "";
				string text8 = "";
				foreach (string key in jSONObject2.keys)
				{
					JSONObject jSONObject3 = jSONObject2[key];
					if (jSONObject3.IsNumber)
					{
						text3 += INTTemplate.Replace("_FIELD_", key);
						text6 += INITINTTemplate.Replace("_FIELD_", key);
					}
					else if (jSONObject3.IsString)
					{
						text4 += STRINGTemplate.Replace("_FIELD_", key);
						text7 += INITSTRINGTemplate.Replace("_FIELD_", key);
					}
					else if (jSONObject3.IsArray)
					{
						text5 += LISTINTTemplate.Replace("_FIELD_", key);
						text8 += INITLISTINTTemplate.Replace("_FIELD_", key);
					}
				}
				text = text.Replace("_CLASSNAME_", $"{name}{i}");
				text = text.Replace("_IDNAME_", text2);
				text = text.Replace("_IDTYPE_", newValue);
				text = text.Replace("_ARRAYNAME_", name ?? "");
				text = text.Replace("_SEIDID_", $"{i}");
				text = text.Replace("_INT_", text3);
				text = text.Replace("_STRING_", text4);
				text = text.Replace("_LISTINT_", text5);
				text = text.Replace("_INITINT_", text6);
				text = text.Replace("_INITSTRING_", text7);
				text = text.Replace("_INITLISTINT_", text8);
				string text9 = Application.dataPath + "/script/data/JSONClass/" + name;
				if (outToDebugPath)
				{
					text9 = "C:\\Test/" + name;
				}
				if (!Directory.Exists(text9))
				{
					Directory.CreateDirectory(text9);
				}
				string text10 = $"{text9}/{name}{i}.cs";
				File.WriteAllText(text10, text);
				Debug.Log((object)$"生成了数据类{name}{i}.cs，导出到了{text10}");
			}
			catch (Exception ex)
			{
				Debug.LogError((object)$"生成数据类{name}{i}时发生异常,{ex.Message}\n{ex.StackTrace}");
			}
		}
	}

	public static void InitJSONClassData()
	{
		Type[] types = Assembly.GetAssembly(typeof(IJSONClass)).GetTypes();
		List<Type> list = new List<Type>();
		Type[] array = types;
		foreach (Type type in array)
		{
			if (type.IsInterface)
			{
				continue;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int j = 0; j < interfaces.Length; j++)
			{
				if (interfaces[j] == typeof(IJSONClass))
				{
					list.Add(type);
				}
			}
		}
		foreach (Type item in list)
		{
			MethodInfo method = item.GetMethod("InitDataDict");
			if (method != null)
			{
				method.Invoke(null, null);
			}
		}
	}

	public static void LoadTemplate()
	{
		Template = Resources.Load<TextAsset>("JSONClassTemplate").text;
		SeidTemplate = Resources.Load<TextAsset>("JSONClassSeidTemplate").text;
	}
}
