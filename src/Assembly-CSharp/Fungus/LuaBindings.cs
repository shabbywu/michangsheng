using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

[ExecuteInEditMode]
public class LuaBindings : LuaBindingsBase
{
	[Tooltip("Add bindings to every Lua Environment in the scene. If false, only add bindings to a specific Lua Environment.")]
	[SerializeField]
	protected bool allEnvironments = true;

	[Tooltip("The specific LuaEnvironment to register the bindings in.")]
	[SerializeField]
	protected LuaEnvironment luaEnvironment;

	[Tooltip("Name of global table variable to store bindings in. If left blank then each binding will be added as a global variable.")]
	[SerializeField]
	protected string tableName = "";

	[Tooltip("Register all CLR types used by the bound objects so that they can be accessed from Lua. If you don't use this option you will need to register these types yourself.")]
	[SerializeField]
	protected bool registerTypes = true;

	[HideInInspector]
	[SerializeField]
	protected List<string> boundTypes = new List<string>();

	[Tooltip("The list of Unity objects to be bound to make them accessible in Lua script.")]
	[SerializeField]
	protected List<BoundObject> boundObjects = new List<BoundObject>();

	[Tooltip("Show inherited public members.")]
	[SerializeField]
	protected bool showInherited;

	public override List<BoundObject> BoundObjects => boundObjects;

	protected virtual void Update()
	{
		if (boundObjects.Count == 0)
		{
			boundObjects.Add(null);
		}
	}

	public override void AddBindings(LuaEnvironment luaEnv)
	{
		if (!allEnvironments && (Object)(object)luaEnvironment != (Object)null && !((object)luaEnvironment).Equals((object?)luaEnv))
		{
			return;
		}
		Table globals = luaEnv.Interpreter.Globals;
		Table table = null;
		if (tableName == "")
		{
			table = globals;
		}
		else
		{
			DynValue dynValue = globals.Get(tableName);
			if (dynValue.Type == DataType.Table)
			{
				table = dynValue.Table;
			}
			else
			{
				table = new Table(globals.OwnerScript);
				globals[tableName] = table;
			}
		}
		if (table == null)
		{
			Debug.LogError((object)"Bindings table must not be null");
		}
		if (registerTypes)
		{
			foreach (string boundType in boundTypes)
			{
				LuaEnvironment.RegisterType(boundType);
			}
		}
		for (int i = 0; i < boundObjects.Count; i++)
		{
			string key = boundObjects[i].key;
			if (key == null || key == "")
			{
				continue;
			}
			if (table.Get(key).Type != 0)
			{
				Debug.LogWarning((object)("An item already exists with the same key as binding '" + key + "'. This binding will be ignored."));
				continue;
			}
			Object obj = boundObjects[i].obj;
			GameObject val = (GameObject)(object)((obj is GameObject) ? obj : null);
			if ((Object)(object)val != (Object)null)
			{
				Component component = boundObjects[i].component;
				if ((Object)(object)component == (Object)null)
				{
					table[key] = val;
				}
				else
				{
					table[key] = component;
				}
			}
			else
			{
				table[key] = boundObjects[i].obj;
			}
		}
	}
}
