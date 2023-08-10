using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using UnityEngine;

namespace Fungus;

public class LuaScriptLoader : ScriptLoaderBase
{
	protected IEnumerable<TextAsset> luaScripts;

	protected override string ResolveModuleName(string modname, string[] paths)
	{
		return modname;
	}

	public LuaScriptLoader(IEnumerable<TextAsset> luaScripts)
	{
		this.luaScripts = luaScripts;
	}

	public override object LoadFile(string file, Table globalContext)
	{
		foreach (TextAsset luaScript in luaScripts)
		{
			if (string.Compare(((Object)luaScript).name, file, ignoreCase: true) == 0)
			{
				return luaScript.text;
			}
		}
		return "";
	}

	public override bool ScriptFileExists(string name)
	{
		foreach (TextAsset luaScript in luaScripts)
		{
			if (string.Compare(((Object)luaScript).name, name, ignoreCase: true) == 0)
			{
				return true;
			}
		}
		return false;
	}
}
