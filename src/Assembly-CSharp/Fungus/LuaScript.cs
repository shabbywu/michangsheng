using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

public class LuaScript : MonoBehaviour
{
	[Tooltip("The Lua Environment to use when executing Lua script.")]
	[SerializeField]
	protected LuaEnvironment luaEnvironment;

	[Tooltip("Text file containing Lua script to be executed.")]
	[SerializeField]
	protected TextAsset luaFile;

	[Tooltip("A Lua string to execute, appended to the contents of Lua File (if one is specified).")]
	[TextArea(5, 50)]
	[SerializeField]
	protected string luaScript = "";

	[Tooltip("Run the script as a Lua coroutine so execution can be yielded for asynchronous operations.")]
	[SerializeField]
	protected bool runAsCoroutine = true;

	protected string friendlyName = "";

	protected bool initialised;

	protected Closure luaFunction;

	public bool Initialised
	{
		set
		{
			initialised = value;
		}
	}

	protected static string GetPath(Transform current)
	{
		if ((Object)(object)current.parent == (Object)null)
		{
			return ((Object)current).name;
		}
		return GetPath(current.parent) + "." + ((Object)current).name;
	}

	protected virtual void Start()
	{
		InitLuaScript();
	}

	protected virtual void InitLuaScript()
	{
		if (!initialised)
		{
			if ((Object)(object)luaEnvironment == (Object)null)
			{
				luaEnvironment = LuaEnvironment.GetLua();
			}
			if ((Object)(object)luaEnvironment == (Object)null)
			{
				Debug.LogError((object)"No Lua Environment found");
				return;
			}
			luaEnvironment.InitEnvironment();
			friendlyName = GetPath(((Component)this).transform) + ".LuaScript";
			string luaString = GetLuaString();
			luaFunction = luaEnvironment.LoadLuaFunction(luaString, friendlyName);
			initialised = true;
		}
	}

	protected virtual string GetLuaString()
	{
		string text = "";
		if ((Object)(object)luaFile != (Object)null)
		{
			text = luaFile.text;
		}
		if (luaScript.Length > 0)
		{
			text += luaScript;
		}
		return text;
	}

	public virtual void OnExecute()
	{
		InitLuaScript();
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			Debug.LogWarning((object)"No Lua Environment found");
		}
		else
		{
			luaEnvironment.RunLuaFunction(luaFunction, runAsCoroutine);
		}
	}
}
