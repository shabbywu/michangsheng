using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

public class LuaStore : LuaBindingsBase
{
	protected Table primeTable;

	protected bool initialized;

	protected static LuaStore instance;

	public virtual Table PrimeTable => primeTable;

	public override List<BoundObject> BoundObjects => null;

	protected virtual void Start()
	{
		Init();
	}

	protected virtual bool Init()
	{
		if (initialized)
		{
			return true;
		}
		if ((Object)(object)instance == (Object)null)
		{
			instance = this;
		}
		else if ((Object)(object)instance != (Object)(object)this)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return false;
		}
		primeTable = DynValue.NewPrimeTable().Table;
		((Component)this).transform.parent = null;
		Object.DontDestroyOnLoad((Object)(object)this);
		initialized = true;
		return true;
	}

	public override void AddBindings(LuaEnvironment luaEnv)
	{
		if (!Init())
		{
			return;
		}
		Table globals = luaEnv.Interpreter.Globals;
		if (globals == null)
		{
			Debug.LogError((object)"Lua globals table is null");
			return;
		}
		Table table = globals.Get("fungus").Table;
		if (table != null)
		{
			table["store"] = primeTable;
		}
		else
		{
			globals["store"] = primeTable;
		}
	}
}
