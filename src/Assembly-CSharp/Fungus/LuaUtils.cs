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
	// Token: 0x02000F00 RID: 3840
	public class LuaUtils : LuaEnvironmentInitializer, ISubstitutionHandler
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06006C0C RID: 27660 RVA: 0x00297C17 File Offset: 0x00295E17
		// (set) Token: 0x06006C0D RID: 27661 RVA: 0x00297C1F File Offset: 0x00295E1F
		protected LuaEnvironment luaEnvironment { get; set; }

		// Token: 0x06006C0E RID: 27662 RVA: 0x00297C28 File Offset: 0x00295E28
		protected virtual void OnEnable()
		{
			StringSubstituter.RegisterHandler(this);
		}

		// Token: 0x06006C0F RID: 27663 RVA: 0x00297C30 File Offset: 0x00295E30
		protected virtual void OnDisable()
		{
			StringSubstituter.UnregisterHandler(this);
		}

		// Token: 0x06006C10 RID: 27664 RVA: 0x00297C38 File Offset: 0x00295E38
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

		// Token: 0x06006C11 RID: 27665 RVA: 0x00297E1C File Offset: 0x0029601C
		protected virtual void InitBindings()
		{
			LuaBindingsBase[] array = Object.FindObjectsOfType<LuaBindingsBase>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AddBindings(this.luaEnvironment);
			}
		}

		// Token: 0x06006C12 RID: 27666 RVA: 0x00297E4C File Offset: 0x0029604C
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

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06006C13 RID: 27667 RVA: 0x00298224 File Offset: 0x00296424
		// (set) Token: 0x06006C14 RID: 27668 RVA: 0x0029822C File Offset: 0x0029642C
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

		// Token: 0x06006C15 RID: 27669 RVA: 0x00298238 File Offset: 0x00296438
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

		// Token: 0x06006C16 RID: 27670 RVA: 0x0029828C File Offset: 0x0029648C
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

		// Token: 0x06006C17 RID: 27671 RVA: 0x0029841C File Offset: 0x0029661C
		public virtual string Substitute(string input)
		{
			return this.stringSubstituter.SubstituteStrings(input);
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x0029842A File Offset: 0x0029662A
		public virtual GameObject Find(string name)
		{
			return GameObject.Find(name);
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x00298432 File Offset: 0x00296632
		public virtual GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x0029843A File Offset: 0x0029663A
		public virtual GameObject[] FindGameObjectsWithTag(string tag)
		{
			return GameObject.FindGameObjectsWithTag(tag);
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x00298442 File Offset: 0x00296642
		public virtual GameObject Instantiate(GameObject go)
		{
			return Object.Instantiate<GameObject>(go);
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x0029844A File Offset: 0x0029664A
		public virtual void Destroy(GameObject go)
		{
			Object.Destroy(go);
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x00298454 File Offset: 0x00296654
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

		// Token: 0x06006C1E RID: 27678 RVA: 0x00298490 File Offset: 0x00296690
		public virtual IEnumerator DoConversation(string conv)
		{
			return this.conversationManager.DoConversation(conv);
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x0029849E File Offset: 0x0029669E
		public virtual void SetSayDialog(SayDialog sayDialog)
		{
			SayDialog.ActiveSayDialog = sayDialog;
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x002984A6 File Offset: 0x002966A6
		public virtual SayDialog GetSayDialog()
		{
			return SayDialog.GetSayDialog();
		}

		// Token: 0x06006C21 RID: 27681 RVA: 0x002984AD File Offset: 0x002966AD
		public virtual void SetMenuDialog(MenuDialog menuDialog)
		{
			MenuDialog.ActiveMenuDialog = menuDialog;
		}

		// Token: 0x06006C22 RID: 27682 RVA: 0x002984B5 File Offset: 0x002966B5
		public virtual MenuDialog GetMenuDialog()
		{
			return MenuDialog.GetMenuDialog();
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x002984BC File Offset: 0x002966BC
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

		// Token: 0x06006C24 RID: 27684 RVA: 0x001086F1 File Offset: 0x001068F1
		public override string PreprocessScript(string input)
		{
			return input;
		}

		// Token: 0x04005AEA RID: 23274
		[Tooltip("Controls if the fungus utilities are accessed from globals (e.g. say) or via a fungus variable (e.g. fungus.say). You can also choose to disable loading the fungus module if it's not required by your script.")]
		[SerializeField]
		protected FungusModuleOptions fungusModule;

		// Token: 0x04005AEB RID: 23275
		[Tooltip("The currently selected language in the string table. Affects variable substitution.")]
		[SerializeField]
		protected string activeLanguage = "en";

		// Token: 0x04005AEC RID: 23276
		[HideInInspector]
		[Tooltip("List of JSON text files which contain localized strings. These strings are added to the 'stringTable' table in the Lua environment at startup.")]
		[SerializeField]
		protected List<TextAsset> stringTables = new List<TextAsset>();

		// Token: 0x04005AED RID: 23277
		[HideInInspector]
		[Tooltip("JSON text files listing the c# types that can be accessed from Lua.")]
		[SerializeField]
		protected List<TextAsset> registerTypes = new List<TextAsset>();

		// Token: 0x04005AEE RID: 23278
		protected bool initialised;

		// Token: 0x04005AEF RID: 23279
		protected Table stringTable;

		// Token: 0x04005AF1 RID: 23281
		protected StringSubstituter stringSubstituter;

		// Token: 0x04005AF2 RID: 23282
		protected ConversationManager conversationManager;
	}
}
