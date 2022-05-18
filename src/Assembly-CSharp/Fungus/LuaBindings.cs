using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200139C RID: 5020
	[ExecuteInEditMode]
	public class LuaBindings : LuaBindingsBase
	{
		// Token: 0x0600798B RID: 31115 RVA: 0x00052FC0 File Offset: 0x000511C0
		protected virtual void Update()
		{
			if (this.boundObjects.Count == 0)
			{
				this.boundObjects.Add(null);
			}
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x002B86A8 File Offset: 0x002B68A8
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

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600798D RID: 31117 RVA: 0x00052FDB File Offset: 0x000511DB
		public override List<BoundObject> BoundObjects
		{
			get
			{
				return this.boundObjects;
			}
		}

		// Token: 0x0400693C RID: 26940
		[Tooltip("Add bindings to every Lua Environment in the scene. If false, only add bindings to a specific Lua Environment.")]
		[SerializeField]
		protected bool allEnvironments = true;

		// Token: 0x0400693D RID: 26941
		[Tooltip("The specific LuaEnvironment to register the bindings in.")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x0400693E RID: 26942
		[Tooltip("Name of global table variable to store bindings in. If left blank then each binding will be added as a global variable.")]
		[SerializeField]
		protected string tableName = "";

		// Token: 0x0400693F RID: 26943
		[Tooltip("Register all CLR types used by the bound objects so that they can be accessed from Lua. If you don't use this option you will need to register these types yourself.")]
		[SerializeField]
		protected bool registerTypes = true;

		// Token: 0x04006940 RID: 26944
		[HideInInspector]
		[SerializeField]
		protected List<string> boundTypes = new List<string>();

		// Token: 0x04006941 RID: 26945
		[Tooltip("The list of Unity objects to be bound to make them accessible in Lua script.")]
		[SerializeField]
		protected List<BoundObject> boundObjects = new List<BoundObject>();

		// Token: 0x04006942 RID: 26946
		[Tooltip("Show inherited public members.")]
		[SerializeField]
		protected bool showInherited;
	}
}
