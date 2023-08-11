using System;
using System.Collections;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.VsCodeDebugger;
using UnityEngine;

namespace Fungus;

public class LuaEnvironment : MonoBehaviour
{
	[Tooltip("Start a Lua debug server on scene start.")]
	[SerializeField]
	protected bool startDebugServer = true;

	[Tooltip("Port to use for the Lua debug server.")]
	[SerializeField]
	protected int debugServerPort = 41912;

	protected Script interpreter;

	protected bool initialised;

	public static MoonSharpVsCodeDebugServer DebugServer { get; private set; }

	public virtual Script Interpreter => interpreter;

	protected virtual void Start()
	{
		InitEnvironment();
	}

	protected virtual void OnDestroy()
	{
		if (DebugServer != null)
		{
			DebugServer.Detach(interpreter);
		}
	}

	protected virtual void InitLuaScriptFiles()
	{
		object[] array = Resources.LoadAll("Lua", typeof(TextAsset));
		object[] source = array;
		interpreter.Options.ScriptLoader = new LuaScriptLoader(source.OfType<TextAsset>());
	}

	protected virtual IEnumerator RunLuaCoroutine(Closure closure, Action<DynValue> onComplete = null)
	{
		DynValue co = interpreter.CreateCoroutine(closure);
		DynValue returnValue = null;
		while (co.Coroutine.State != CoroutineState.Dead)
		{
			try
			{
				returnValue = co.Coroutine.Resume();
			}
			catch (InterpreterException ex)
			{
				LogException(ex.DecoratedMessage, GetSourceCode());
			}
			yield return null;
		}
		onComplete?.Invoke(returnValue);
	}

	protected virtual string GetSourceCode()
	{
		string result = "";
		if (interpreter.SourceCodeCount > 0)
		{
			SourceCode sourceCode = interpreter.GetSourceCode(interpreter.SourceCodeCount - 1);
			if (sourceCode != null)
			{
				result = sourceCode.Code;
			}
		}
		return result;
	}

	protected virtual IEnumerator RunUnityCoroutineImpl(IEnumerator coroutine)
	{
		if (coroutine == null)
		{
			Debug.LogWarning((object)"Coroutine must not be null");
		}
		else
		{
			yield return ((MonoBehaviour)this).StartCoroutine(coroutine);
		}
	}

	protected static void LogException(string decoratedMessage, string debugInfo)
	{
		string text = decoratedMessage + "\n";
		char[] separator = new char[2] { '\r', '\n' };
		string[] array = debugInfo.Split(separator, StringSplitOptions.None);
		int num = 1;
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			text = text + num + ": " + text2 + "\n";
			num++;
		}
		Debug.LogError((object)text);
	}

	public static LuaEnvironment GetLua()
	{
		LuaEnvironment luaEnvironment = Object.FindObjectOfType<LuaEnvironment>();
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			GameObject val = Resources.Load<GameObject>("Prefabs/LuaEnvironment");
			if ((Object)(object)val != (Object)null)
			{
				GameObject obj = Object.Instantiate<GameObject>(val);
				((Object)obj).name = "LuaEnvironment";
				luaEnvironment = obj.GetComponent<LuaEnvironment>();
			}
		}
		return luaEnvironment;
	}

	public static void RegisterType(string typeName, bool extensionType = false)
	{
		Type type = null;
		try
		{
			type = Type.GetType(typeName);
		}
		catch
		{
		}
		if (type == null)
		{
			Debug.LogWarning((object)("Type not found: " + typeName));
		}
		else
		{
			if (type == typeof(object) || UserData.IsTypeRegistered(type))
			{
				return;
			}
			try
			{
				if (extensionType)
				{
					UserData.RegisterExtensionType(type);
				}
				else
				{
					UserData.RegisterType(type);
				}
			}
			catch (ArgumentException ex)
			{
				Debug.LogWarning((object)ex.Message);
			}
		}
	}

	public virtual Task RunUnityCoroutine(IEnumerator coroutine)
	{
		if (coroutine == null)
		{
			return null;
		}
		return new Task(RunUnityCoroutineImpl(coroutine));
	}

	public virtual void InitEnvironment()
	{
		if (!initialised)
		{
			Script.DefaultOptions.DebugPrint = delegate(string s)
			{
				Debug.Log((object)s);
			};
			interpreter = new Script(CoreModules.Preset_Complete);
			InitLuaScriptFiles();
			LuaEnvironmentInitializer[] components = ((Component)this).GetComponents<LuaEnvironmentInitializer>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].Initialize();
			}
			initialised = true;
		}
	}

	public virtual Closure LoadLuaFunction(string luaString, string friendlyName)
	{
		InitEnvironment();
		LuaEnvironmentInitializer component = ((Component)this).GetComponent<LuaEnvironmentInitializer>();
		string code = ((!((Object)(object)component != (Object)null)) ? luaString : component.PreprocessScript(luaString));
		DynValue dynValue = null;
		try
		{
			dynValue = interpreter.LoadString(code, null, friendlyName);
		}
		catch (InterpreterException ex)
		{
			LogException(ex.DecoratedMessage, luaString);
		}
		if (dynValue == null || dynValue.Type != DataType.Function)
		{
			Debug.LogError((object)"Failed to create Lua function from Lua string");
			return null;
		}
		return dynValue.Function;
	}

	public virtual void RunLuaFunction(Closure fn, bool runAsCoroutine, Action<DynValue> onComplete = null)
	{
		if (fn == null)
		{
			onComplete?.Invoke(null);
			return;
		}
		if (runAsCoroutine)
		{
			((MonoBehaviour)this).StartCoroutine(RunLuaCoroutine(fn, onComplete));
			return;
		}
		DynValue obj = null;
		try
		{
			obj = fn.Call();
		}
		catch (InterpreterException ex)
		{
			LogException(ex.DecoratedMessage, GetSourceCode());
		}
		onComplete?.Invoke(obj);
	}

	public virtual void DoLuaString(string luaString, string friendlyName, bool runAsCoroutine, Action<DynValue> onComplete = null)
	{
		Closure fn = LoadLuaFunction(luaString, friendlyName);
		RunLuaFunction(fn, runAsCoroutine, onComplete);
	}
}
