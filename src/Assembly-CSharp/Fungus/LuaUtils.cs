using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013A5 RID: 5029
	public class LuaUtils : LuaEnvironmentInitializer, ISubstitutionHandler
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060079C0 RID: 31168 RVA: 0x000531AF File Offset: 0x000513AF
		// (set) Token: 0x060079C1 RID: 31169 RVA: 0x000531B7 File Offset: 0x000513B7
		protected LuaEnvironment luaEnvironment { get; set; }

		// Token: 0x060079C2 RID: 31170 RVA: 0x000531C0 File Offset: 0x000513C0
		protected virtual void OnEnable()
		{
			StringSubstituter.RegisterHandler(this);
		}

		// Token: 0x060079C3 RID: 31171 RVA: 0x000531C8 File Offset: 0x000513C8
		protected virtual void OnDisable()
		{
			StringSubstituter.UnregisterHandler(this);
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x002B8ECC File Offset: 0x002B70CC
		protected virtual void InitTypes()
		{
			LuaEnvironment.RegisterType("Fungus.PODTypeFactory", false);
			LuaEnvironment.RegisterType("Fungus.FungusPrefs", false);
			foreach (TextAsset textAsset in this.registerTypes)
			{
				if (!(textAsset == null))
				{
					JSONObject jsonobject = new JSONObject(textAsset.text, -2, false, false);
					if (jsonobject == null || jsonobject.type != JSONObject.Type.OBJECT)
					{
						Debug.LogError("Error parsing JSON file " + textAsset.name);
					}
					else
					{
						JSONObject field = jsonobject.GetField("registerTypes");
						if (field != null && field.type == JSONObject.Type.ARRAY)
						{
							foreach (JSONObject jsonobject2 in field.list)
							{
								if (jsonobject2 != null && jsonobject2.type == JSONObject.Type.STRING)
								{
									string typeName = jsonobject2.str.Trim();
									if (!(Type.GetType(typeName) == null))
									{
										LuaEnvironment.RegisterType(typeName, false);
									}
								}
							}
						}
						JSONObject field2 = jsonobject.GetField("extensionTypes");
						if (field2 != null && field2.type == JSONObject.Type.ARRAY)
						{
							foreach (JSONObject jsonobject3 in field2.list)
							{
								if (jsonobject3 != null && jsonobject3.type == JSONObject.Type.STRING)
								{
									string typeName2 = jsonobject3.str.Trim();
									if (!(Type.GetType(typeName2) == null))
									{
										LuaEnvironment.RegisterType(typeName2, true);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060079C5 RID: 31173 RVA: 0x002B90B0 File Offset: 0x002B72B0
		protected virtual void InitBindings()
		{
			LuaBindingsBase[] array = Object.FindObjectsOfType<LuaBindingsBase>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AddBindings(this.luaEnvironment);
			}
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x002B90E0 File Offset: 0x002B72E0
		protected virtual void InitFungusModule()
		{
			if (this.fungusModule == FungusModuleOptions.NoFungusModule)
			{
				return;
			}
			Script interpreter = this.luaEnvironment.Interpreter;
			Table table = null;
			DynValue dynValue = interpreter.RequireModule("fungus", null);
			if (dynValue != null && dynValue.Type == DataType.Function)
			{
				table = dynValue.Function.Call().Table;
			}
			if (table == null)
			{
				Debug.LogError("Failed to create Fungus table");
				return;
			}
			interpreter.Globals["fungus"] = table;
			table["time"] = UserData.CreateStatic(typeof(Time));
			table["playerprefs"] = UserData.CreateStatic(typeof(PlayerPrefs));
			table["prefs"] = UserData.CreateStatic(typeof(FungusPrefs));
			table["factory"] = UserData.CreateStatic(typeof(PODTypeFactory));
			table["luaenvironment"] = this.luaEnvironment;
			table["luautils"] = this;
			Type type = Type.GetType("IntegrationTest");
			if (type != null)
			{
				UserData.RegisterType(type, InteropAccessMode.Default, null);
				table["test"] = UserData.CreateStatic(type);
			}
			this.stringTable = new Table(interpreter);
			table["stringtable"] = this.stringTable;
			foreach (TextAsset textAsset in this.stringTables)
			{
				if (!(textAsset.text == ""))
				{
					JSONObject jsonobject = new JSONObject(textAsset.text, -2, false, false);
					if (jsonobject == null || jsonobject.type != JSONObject.Type.OBJECT)
					{
						Debug.LogError("String table JSON format is not correct " + textAsset.name);
					}
					else
					{
						foreach (string text in jsonobject.keys)
						{
							if (text == "")
							{
								Debug.LogError("String table JSON format is not correct " + textAsset.name);
							}
							else
							{
								Table table2 = new Table(interpreter);
								this.stringTable[text] = table2;
								JSONObject field = jsonobject.GetField(text);
								if (field.type != JSONObject.Type.OBJECT)
								{
									Debug.LogError("String table JSON format is not correct " + textAsset.name);
								}
								else
								{
									foreach (string text2 in field.keys)
									{
										string str = field.GetField(text2).str;
										table2[text2] = str;
									}
								}
							}
						}
					}
				}
			}
			this.stringSubstituter = new StringSubstituter(5);
			this.conversationManager = new ConversationManager();
			this.conversationManager.PopulateCharacterCache();
			if (this.fungusModule == FungusModuleOptions.UseGlobalVariables)
			{
				foreach (TablePair tablePair in table.Pairs)
				{
					if (interpreter.Globals.Keys.Contains(tablePair.Key))
					{
						Debug.LogError("Lua globals already contains a variable " + tablePair.Key);
					}
					else
					{
						interpreter.Globals[tablePair.Key] = tablePair.Value;
					}
				}
				interpreter.Globals["fungus"] = DynValue.Nil;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x000531D0 File Offset: 0x000513D0
		// (set) Token: 0x060079C8 RID: 31176 RVA: 0x000531D8 File Offset: 0x000513D8
		public virtual string ActiveLanguage
		{
			get
			{
				return this.activeLanguage;
			}
			set
			{
				this.activeLanguage = value;
			}
		}

		// Token: 0x060079C9 RID: 31177 RVA: 0x002B94B8 File Offset: 0x002B76B8
		public virtual string GetString(string key)
		{
			if (this.stringTable != null)
			{
				DynValue dynValue = this.stringTable.Get(key);
				if (dynValue.Type == DataType.Table)
				{
					DynValue dynValue2 = dynValue.Table.Get(this.activeLanguage);
					if (dynValue2.Type == DataType.String)
					{
						return dynValue2.String;
					}
				}
			}
			return "";
		}

		// Token: 0x060079CA RID: 31178 RVA: 0x002B950C File Offset: 0x002B770C
		[MoonSharpHidden]
		public virtual bool SubstituteStrings(StringBuilder input)
		{
			if (this.luaEnvironment == null)
			{
				this.luaEnvironment = base.GetComponent<LuaEnvironment>();
				if (this.luaEnvironment != null)
				{
					this.luaEnvironment.InitEnvironment();
				}
			}
			if (this.luaEnvironment == null)
			{
				Debug.LogError("No Lua Environment found");
				return false;
			}
			if (this.luaEnvironment.Interpreter == null)
			{
				Debug.LogError("No Lua interpreter found");
				return false;
			}
			input.Replace("\t", "");
			Script interpreter = this.luaEnvironment.Interpreter;
			Regex regex = new Regex("\\{\\$.*?\\}");
			bool result = false;
			foreach (object obj in regex.Matches(input.ToString()))
			{
				Match match = (Match)obj;
				string key = match.Value.Substring(2, match.Value.Length - 3);
				if (this.stringTable != null)
				{
					DynValue dynValue = this.stringTable.Get(key);
					if (dynValue.Type == DataType.Table)
					{
						DynValue dynValue2 = dynValue.Table.Get(this.activeLanguage);
						if (dynValue2.Type == DataType.String)
						{
							input.Replace(match.Value, dynValue2.String);
							result = true;
							continue;
						}
						continue;
					}
				}
				DynValue dynValue3 = interpreter.Globals.Get(key);
				if (dynValue3.Type != DataType.Nil)
				{
					input.Replace(match.Value, dynValue3.ToPrintString());
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060079CB RID: 31179 RVA: 0x000531E1 File Offset: 0x000513E1
		public virtual string Substitute(string input)
		{
			return this.stringSubstituter.SubstituteStrings(input);
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x000531EF File Offset: 0x000513EF
		public virtual GameObject Find(string name)
		{
			return GameObject.Find(name);
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x000531F7 File Offset: 0x000513F7
		public virtual GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x000531FF File Offset: 0x000513FF
		public virtual GameObject[] FindGameObjectsWithTag(string tag)
		{
			return GameObject.FindGameObjectsWithTag(tag);
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x00053207 File Offset: 0x00051407
		public virtual GameObject Instantiate(GameObject go)
		{
			return Object.Instantiate<GameObject>(go);
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x0005320F File Offset: 0x0005140F
		public virtual void Destroy(GameObject go)
		{
			Object.Destroy(go);
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x002B969C File Offset: 0x002B789C
		public virtual GameObject Spawn(string resourceName)
		{
			GameObject gameObject = Resources.Load<GameObject>(resourceName);
			if (gameObject != null)
			{
				GameObject gameObject2 = this.Instantiate(gameObject);
				gameObject2.name = resourceName.Replace("Prefabs/", "");
				return gameObject2;
			}
			return null;
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x00053217 File Offset: 0x00051417
		public virtual IEnumerator DoConversation(string conv)
		{
			return this.conversationManager.DoConversation(conv);
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x00053225 File Offset: 0x00051425
		public virtual void SetSayDialog(SayDialog sayDialog)
		{
			SayDialog.ActiveSayDialog = sayDialog;
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x0005322D File Offset: 0x0005142D
		public virtual SayDialog GetSayDialog()
		{
			return SayDialog.GetSayDialog();
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x00053234 File Offset: 0x00051434
		public virtual void SetMenuDialog(MenuDialog menuDialog)
		{
			MenuDialog.ActiveMenuDialog = menuDialog;
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x0005323C File Offset: 0x0005143C
		public virtual MenuDialog GetMenuDialog()
		{
			return MenuDialog.GetMenuDialog();
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x002B96D8 File Offset: 0x002B78D8
		public override void Initialize()
		{
			this.luaEnvironment = base.GetComponent<LuaEnvironment>();
			if (this.luaEnvironment == null)
			{
				Debug.LogError("No Lua Environment found");
				return;
			}
			if (this.luaEnvironment.Interpreter == null)
			{
				Debug.LogError("No Lua interpreter found");
				return;
			}
			this.InitTypes();
			this.InitFungusModule();
			this.InitBindings();
		}

		// Token: 0x060079D8 RID: 31192 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override string PreprocessScript(string input)
		{
			return input;
		}

		// Token: 0x04006963 RID: 26979
		[Tooltip("Controls if the fungus utilities are accessed from globals (e.g. say) or via a fungus variable (e.g. fungus.say). You can also choose to disable loading the fungus module if it's not required by your script.")]
		[SerializeField]
		protected FungusModuleOptions fungusModule;

		// Token: 0x04006964 RID: 26980
		[Tooltip("The currently selected language in the string table. Affects variable substitution.")]
		[SerializeField]
		protected string activeLanguage = "en";

		// Token: 0x04006965 RID: 26981
		[HideInInspector]
		[Tooltip("List of JSON text files which contain localized strings. These strings are added to the 'stringTable' table in the Lua environment at startup.")]
		[SerializeField]
		protected List<TextAsset> stringTables = new List<TextAsset>();

		// Token: 0x04006966 RID: 26982
		[HideInInspector]
		[Tooltip("JSON text files listing the c# types that can be accessed from Lua.")]
		[SerializeField]
		protected List<TextAsset> registerTypes = new List<TextAsset>();

		// Token: 0x04006967 RID: 26983
		protected bool initialised;

		// Token: 0x04006968 RID: 26984
		protected Table stringTable;

		// Token: 0x0400696A RID: 26986
		protected StringSubstituter stringSubstituter;

		// Token: 0x0400696B RID: 26987
		protected ConversationManager conversationManager;
	}
}
