using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

public class LuaUtils : LuaEnvironmentInitializer, ISubstitutionHandler
{
	[Tooltip("Controls if the fungus utilities are accessed from globals (e.g. say) or via a fungus variable (e.g. fungus.say). You can also choose to disable loading the fungus module if it's not required by your script.")]
	[SerializeField]
	protected FungusModuleOptions fungusModule;

	[Tooltip("The currently selected language in the string table. Affects variable substitution.")]
	[SerializeField]
	protected string activeLanguage = "en";

	[HideInInspector]
	[Tooltip("List of JSON text files which contain localized strings. These strings are added to the 'stringTable' table in the Lua environment at startup.")]
	[SerializeField]
	protected List<TextAsset> stringTables = new List<TextAsset>();

	[HideInInspector]
	[Tooltip("JSON text files listing the c# types that can be accessed from Lua.")]
	[SerializeField]
	protected List<TextAsset> registerTypes = new List<TextAsset>();

	protected bool initialised;

	protected Table stringTable;

	protected StringSubstituter stringSubstituter;

	protected ConversationManager conversationManager;

	protected LuaEnvironment luaEnvironment { get; set; }

	public virtual string ActiveLanguage
	{
		get
		{
			return activeLanguage;
		}
		set
		{
			activeLanguage = value;
		}
	}

	protected virtual void OnEnable()
	{
		StringSubstituter.RegisterHandler(this);
	}

	protected virtual void OnDisable()
	{
		StringSubstituter.UnregisterHandler(this);
	}

	protected virtual void InitTypes()
	{
		LuaEnvironment.RegisterType("Fungus.PODTypeFactory");
		LuaEnvironment.RegisterType("Fungus.FungusPrefs");
		foreach (TextAsset registerType in registerTypes)
		{
			if ((Object)(object)registerType == (Object)null)
			{
				continue;
			}
			JSONObject jSONObject = new JSONObject(registerType.text);
			if (jSONObject == null || jSONObject.type != JSONObject.Type.OBJECT)
			{
				Debug.LogError((object)("Error parsing JSON file " + ((Object)registerType).name));
				continue;
			}
			JSONObject field = jSONObject.GetField("registerTypes");
			if (field != null && field.type == JSONObject.Type.ARRAY)
			{
				foreach (JSONObject item in field.list)
				{
					if (item != null && item.type == JSONObject.Type.STRING)
					{
						string typeName = item.str.Trim();
						if (!(Type.GetType(typeName) == null))
						{
							LuaEnvironment.RegisterType(typeName);
						}
					}
				}
			}
			JSONObject field2 = jSONObject.GetField("extensionTypes");
			if (field2 == null || field2.type != JSONObject.Type.ARRAY)
			{
				continue;
			}
			foreach (JSONObject item2 in field2.list)
			{
				if (item2 != null && item2.type == JSONObject.Type.STRING)
				{
					string typeName2 = item2.str.Trim();
					if (!(Type.GetType(typeName2) == null))
					{
						LuaEnvironment.RegisterType(typeName2, extensionType: true);
					}
				}
			}
		}
	}

	protected virtual void InitBindings()
	{
		LuaBindingsBase[] array = Object.FindObjectsOfType<LuaBindingsBase>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].AddBindings(luaEnvironment);
		}
	}

	protected virtual void InitFungusModule()
	{
		if (fungusModule == FungusModuleOptions.NoFungusModule)
		{
			return;
		}
		Script interpreter = luaEnvironment.Interpreter;
		Table table = null;
		DynValue dynValue = interpreter.RequireModule("fungus");
		if (dynValue != null && dynValue.Type == DataType.Function)
		{
			table = dynValue.Function.Call().Table;
		}
		if (table == null)
		{
			Debug.LogError((object)"Failed to create Fungus table");
			return;
		}
		interpreter.Globals["fungus"] = table;
		table["time"] = UserData.CreateStatic(typeof(Time));
		table["playerprefs"] = UserData.CreateStatic(typeof(PlayerPrefs));
		table["prefs"] = UserData.CreateStatic(typeof(FungusPrefs));
		table["factory"] = UserData.CreateStatic(typeof(PODTypeFactory));
		table["luaenvironment"] = luaEnvironment;
		table["luautils"] = this;
		Type type = Type.GetType("IntegrationTest");
		if (type != null)
		{
			UserData.RegisterType(type);
			table["test"] = UserData.CreateStatic(type);
		}
		stringTable = new Table(interpreter);
		table["stringtable"] = stringTable;
		foreach (TextAsset stringTable in stringTables)
		{
			if (stringTable.text == "")
			{
				continue;
			}
			JSONObject jSONObject = new JSONObject(stringTable.text);
			if (jSONObject == null || jSONObject.type != JSONObject.Type.OBJECT)
			{
				Debug.LogError((object)("String table JSON format is not correct " + ((Object)stringTable).name));
				continue;
			}
			foreach (string key in jSONObject.keys)
			{
				if (key == "")
				{
					Debug.LogError((object)("String table JSON format is not correct " + ((Object)stringTable).name));
					continue;
				}
				Table table2 = new Table(interpreter);
				this.stringTable[key] = table2;
				JSONObject field = jSONObject.GetField(key);
				if (field.type != JSONObject.Type.OBJECT)
				{
					Debug.LogError((object)("String table JSON format is not correct " + ((Object)stringTable).name));
					continue;
				}
				foreach (string key2 in field.keys)
				{
					string str = field.GetField(key2).str;
					table2[key2] = str;
				}
			}
		}
		stringSubstituter = new StringSubstituter();
		conversationManager = new ConversationManager();
		conversationManager.PopulateCharacterCache();
		if (fungusModule != 0)
		{
			return;
		}
		foreach (TablePair pair in table.Pairs)
		{
			if (interpreter.Globals.Keys.Contains(pair.Key))
			{
				Debug.LogError((object)("Lua globals already contains a variable " + pair.Key));
			}
			else
			{
				interpreter.Globals[pair.Key] = pair.Value;
			}
		}
		interpreter.Globals["fungus"] = DynValue.Nil;
	}

	public virtual string GetString(string key)
	{
		if (stringTable != null)
		{
			DynValue dynValue = stringTable.Get(key);
			if (dynValue.Type == DataType.Table)
			{
				DynValue dynValue2 = dynValue.Table.Get(activeLanguage);
				if (dynValue2.Type == DataType.String)
				{
					return dynValue2.String;
				}
			}
		}
		return "";
	}

	[MoonSharpHidden]
	public virtual bool SubstituteStrings(StringBuilder input)
	{
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			luaEnvironment = ((Component)this).GetComponent<LuaEnvironment>();
			if ((Object)(object)luaEnvironment != (Object)null)
			{
				luaEnvironment.InitEnvironment();
			}
		}
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			Debug.LogError((object)"No Lua Environment found");
			return false;
		}
		if (luaEnvironment.Interpreter == null)
		{
			Debug.LogError((object)"No Lua interpreter found");
			return false;
		}
		input.Replace("\t", "");
		Script interpreter = luaEnvironment.Interpreter;
		Regex regex = new Regex("\\{\\$.*?\\}");
		bool result = false;
		foreach (Match item in regex.Matches(input.ToString()))
		{
			string key = item.Value.Substring(2, item.Value.Length - 3);
			if (stringTable != null)
			{
				DynValue dynValue = stringTable.Get(key);
				if (dynValue.Type == DataType.Table)
				{
					DynValue dynValue2 = dynValue.Table.Get(activeLanguage);
					if (dynValue2.Type == DataType.String)
					{
						input.Replace(item.Value, dynValue2.String);
						result = true;
					}
					continue;
				}
			}
			DynValue dynValue3 = interpreter.Globals.Get(key);
			if (dynValue3.Type != 0)
			{
				input.Replace(item.Value, dynValue3.ToPrintString());
				result = true;
			}
		}
		return result;
	}

	public virtual string Substitute(string input)
	{
		return stringSubstituter.SubstituteStrings(input);
	}

	public virtual GameObject Find(string name)
	{
		return GameObject.Find(name);
	}

	public virtual GameObject FindWithTag(string tag)
	{
		return GameObject.FindGameObjectWithTag(tag);
	}

	public virtual GameObject[] FindGameObjectsWithTag(string tag)
	{
		return GameObject.FindGameObjectsWithTag(tag);
	}

	public virtual GameObject Instantiate(GameObject go)
	{
		return Object.Instantiate<GameObject>(go);
	}

	public virtual void Destroy(GameObject go)
	{
		Object.Destroy((Object)(object)go);
	}

	public virtual GameObject Spawn(string resourceName)
	{
		GameObject val = Resources.Load<GameObject>(resourceName);
		if ((Object)(object)val != (Object)null)
		{
			GameObject obj = Instantiate(val);
			((Object)obj).name = resourceName.Replace("Prefabs/", "");
			return obj;
		}
		return null;
	}

	public virtual IEnumerator DoConversation(string conv)
	{
		return conversationManager.DoConversation(conv);
	}

	public virtual void SetSayDialog(SayDialog sayDialog)
	{
		SayDialog.ActiveSayDialog = sayDialog;
	}

	public virtual SayDialog GetSayDialog()
	{
		return SayDialog.GetSayDialog();
	}

	public virtual void SetMenuDialog(MenuDialog menuDialog)
	{
		MenuDialog.ActiveMenuDialog = menuDialog;
	}

	public virtual MenuDialog GetMenuDialog()
	{
		return MenuDialog.GetMenuDialog();
	}

	public override void Initialize()
	{
		luaEnvironment = ((Component)this).GetComponent<LuaEnvironment>();
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			Debug.LogError((object)"No Lua Environment found");
			return;
		}
		if (luaEnvironment.Interpreter == null)
		{
			Debug.LogError((object)"No Lua interpreter found");
			return;
		}
		InitTypes();
		InitFungusModule();
		InitBindings();
	}

	public override string PreprocessScript(string input)
	{
		return input;
	}
}
