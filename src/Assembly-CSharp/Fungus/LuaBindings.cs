using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EFA RID: 3834
	[ExecuteInEditMode]
	public class LuaBindings : LuaBindingsBase
	{
		// Token: 0x06006BE6 RID: 27622 RVA: 0x00297378 File Offset: 0x00295578
		protected virtual void Update()
		{
			if (this.boundObjects.Count == 0)
			{
				this.boundObjects.Add(null);
			}
		}

		// Token: 0x06006BE7 RID: 27623 RVA: 0x00297394 File Offset: 0x00295594
		public override void AddBindings(LuaEnvironment luaEnv)
		{
			if (!this.allEnvironments && this.luaEnvironment != null && !this.luaEnvironment.Equals(luaEnv))
			{
				return;
			}
			Table globals = luaEnv.Interpreter.Globals;
			Table table = null;
			if (this.tableName == "")
			{
				table = globals;
			}
			else
			{
				DynValue dynValue = globals.Get(this.tableName);
				if (dynValue.Type == DataType.Table)
				{
					table = dynValue.Table;
				}
				else
				{
					table = new Table(globals.OwnerScript);
					globals[this.tableName] = table;
				}
			}
			if (table == null)
			{
				Debug.LogError("Bindings table must not be null");
			}
			if (this.registerTypes)
			{
				foreach (string typeName in this.boundTypes)
				{
					LuaEnvironment.RegisterType(typeName, false);
				}
			}
			for (int i = 0; i < this.boundObjects.Count; i++)
			{
				string key = this.boundObjects[i].key;
				if (key != null && !(key == ""))
				{
					if (table.Get(key).Type != DataType.Nil)
					{
						Debug.LogWarning("An item already exists with the same key as binding '" + key + "'. This binding will be ignored.");
					}
					else
					{
						GameObject gameObject = this.boundObjects[i].obj as GameObject;
						if (gameObject != null)
						{
							Component component = this.boundObjects[i].component;
							if (component == null)
							{
								table[key] = gameObject;
							}
							else
							{
								table[key] = component;
							}
						}
						else
						{
							table[key] = this.boundObjects[i].obj;
						}
					}
				}
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x00297564 File Offset: 0x00295764
		public override List<BoundObject> BoundObjects
		{
			get
			{
				return this.boundObjects;
			}
		}

		// Token: 0x04005AD0 RID: 23248
		[Tooltip("Add bindings to every Lua Environment in the scene. If false, only add bindings to a specific Lua Environment.")]
		[SerializeField]
		protected bool allEnvironments = true;

		// Token: 0x04005AD1 RID: 23249
		[Tooltip("The specific LuaEnvironment to register the bindings in.")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x04005AD2 RID: 23250
		[Tooltip("Name of global table variable to store bindings in. If left blank then each binding will be added as a global variable.")]
		[SerializeField]
		protected string tableName = "";

		// Token: 0x04005AD3 RID: 23251
		[Tooltip("Register all CLR types used by the bound objects so that they can be accessed from Lua. If you don't use this option you will need to register these types yourself.")]
		[SerializeField]
		protected bool registerTypes = true;

		// Token: 0x04005AD4 RID: 23252
		[HideInInspector]
		[SerializeField]
		protected List<string> boundTypes = new List<string>();

		// Token: 0x04005AD5 RID: 23253
		[Tooltip("The list of Unity objects to be bound to make them accessible in Lua script.")]
		[SerializeField]
		protected List<BoundObject> boundObjects = new List<BoundObject>();

		// Token: 0x04005AD6 RID: 23254
		[Tooltip("Show inherited public members.")]
		[SerializeField]
		protected bool showInherited;
	}
}
