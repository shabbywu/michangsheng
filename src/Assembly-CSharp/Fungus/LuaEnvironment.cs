using System;
using System.Collections;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.VsCodeDebugger;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EFB RID: 3835
	public class LuaEnvironment : MonoBehaviour
	{
		// Token: 0x06006BEA RID: 27626 RVA: 0x002975A3 File Offset: 0x002957A3
		protected virtual void Start()
		{
			this.InitEnvironment();
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x002975AB File Offset: 0x002957AB
		protected virtual void OnDestroy()
		{
			if (LuaEnvironment.DebugServer != null)
			{
				LuaEnvironment.DebugServer.Detach(this.interpreter);
			}
		}

		// Token: 0x06006BEC RID: 27628 RVA: 0x002975C4 File Offset: 0x002957C4
		protected virtual void InitLuaScriptFiles()
		{
			object[] array = Resources.LoadAll("Lua", typeof(TextAsset));
			object[] source = array;
			this.interpreter.Options.ScriptLoader = new LuaScriptLoader(source.OfType<TextAsset>());
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x00297603 File Offset: 0x00295803
		protected virtual IEnumerator RunLuaCoroutine(Closure closure, Action<DynValue> onComplete = null)
		{
			DynValue co = this.interpreter.CreateCoroutine(closure);
			DynValue returnValue = null;
			while (co.Coroutine.State != CoroutineState.Dead)
			{
				try
				{
					returnValue = co.Coroutine.Resume();
				}
				catch (InterpreterException ex)
				{
					LuaEnvironment.LogException(ex.DecoratedMessage, this.GetSourceCode());
				}
				yield return null;
			}
			if (onComplete != null)
			{
				onComplete(returnValue);
			}
			yield break;
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x00297620 File Offset: 0x00295820
		protected virtual string GetSourceCode()
		{
			string result = "";
			if (this.interpreter.SourceCodeCount > 0)
			{
				SourceCode sourceCode = this.interpreter.GetSourceCode(this.interpreter.SourceCodeCount - 1);
				if (sourceCode != null)
				{
					result = sourceCode.Code;
				}
			}
			return result;
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x00297665 File Offset: 0x00295865
		protected virtual IEnumerator RunUnityCoroutineImpl(IEnumerator coroutine)
		{
			if (coroutine == null)
			{
				Debug.LogWarning("Coroutine must not be null");
				yield break;
			}
			yield return base.StartCoroutine(coroutine);
			yield break;
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x0029767C File Offset: 0x0029587C
		protected static void LogException(string decoratedMessage, string debugInfo)
		{
			string text = decoratedMessage + "\n";
			char[] separator = new char[]
			{
				'\r',
				'\n'
			};
			string[] array = debugInfo.Split(separator, StringSplitOptions.None);
			int num = 1;
			foreach (string text2 in array)
			{
				text = string.Concat(new string[]
				{
					text,
					num.ToString(),
					": ",
					text2,
					"\n"
				});
				num++;
			}
			Debug.LogError(text);
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06006BF1 RID: 27633 RVA: 0x00297702 File Offset: 0x00295902
		// (set) Token: 0x06006BF2 RID: 27634 RVA: 0x00297709 File Offset: 0x00295909
		public static MoonSharpVsCodeDebugServer DebugServer { get; private set; }

		// Token: 0x06006BF3 RID: 27635 RVA: 0x00297714 File Offset: 0x00295914
		public static LuaEnvironment GetLua()
		{
			LuaEnvironment luaEnvironment = Object.FindObjectOfType<LuaEnvironment>();
			if (luaEnvironment == null)
			{
				GameObject gameObject = Resources.Load<GameObject>("Prefabs/LuaEnvironment");
				if (gameObject != null)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
					gameObject2.name = "LuaEnvironment";
					luaEnvironment = gameObject2.GetComponent<LuaEnvironment>();
				}
			}
			return luaEnvironment;
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x0029775C File Offset: 0x0029595C
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
				Debug.LogWarning("Type not found: " + typeName);
				return;
			}
			if (type == typeof(object))
			{
				return;
			}
			if (!UserData.IsTypeRegistered(type))
			{
				try
				{
					if (extensionType)
					{
						UserData.RegisterExtensionType(type, InteropAccessMode.Default);
					}
					else
					{
						UserData.RegisterType(type, InteropAccessMode.Default, null);
					}
				}
				catch (ArgumentException ex)
				{
					Debug.LogWarning(ex.Message);
				}
			}
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x002977EC File Offset: 0x002959EC
		public virtual Task RunUnityCoroutine(IEnumerator coroutine)
		{
			if (coroutine == null)
			{
				return null;
			}
			return new Task(this.RunUnityCoroutineImpl(coroutine), true);
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x00297800 File Offset: 0x00295A00
		public virtual void InitEnvironment()
		{
			if (this.initialised)
			{
				return;
			}
			Script.DefaultOptions.DebugPrint = delegate(string s)
			{
				Debug.Log(s);
			};
			this.interpreter = new Script(CoreModules.Preset_Complete);
			this.InitLuaScriptFiles();
			LuaEnvironmentInitializer[] components = base.GetComponents<LuaEnvironmentInitializer>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].Initialize();
			}
			this.initialised = true;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06006BF7 RID: 27639 RVA: 0x00297879 File Offset: 0x00295A79
		public virtual Script Interpreter
		{
			get
			{
				return this.interpreter;
			}
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x00297884 File Offset: 0x00295A84
		public virtual Closure LoadLuaFunction(string luaString, string friendlyName)
		{
			this.InitEnvironment();
			LuaEnvironmentInitializer component = base.GetComponent<LuaEnvironmentInitializer>();
			string code;
			if (component != null)
			{
				code = component.PreprocessScript(luaString);
			}
			else
			{
				code = luaString;
			}
			DynValue dynValue = null;
			try
			{
				dynValue = this.interpreter.LoadString(code, null, friendlyName);
			}
			catch (InterpreterException ex)
			{
				LuaEnvironment.LogException(ex.DecoratedMessage, luaString);
			}
			if (dynValue == null || dynValue.Type != DataType.Function)
			{
				Debug.LogError("Failed to create Lua function from Lua string");
				return null;
			}
			return dynValue.Function;
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x00297904 File Offset: 0x00295B04
		public virtual void RunLuaFunction(Closure fn, bool runAsCoroutine, Action<DynValue> onComplete = null)
		{
			if (fn == null)
			{
				if (onComplete != null)
				{
					onComplete(null);
				}
				return;
			}
			if (runAsCoroutine)
			{
				base.StartCoroutine(this.RunLuaCoroutine(fn, onComplete));
				return;
			}
			DynValue obj = null;
			try
			{
				obj = fn.Call();
			}
			catch (InterpreterException ex)
			{
				LuaEnvironment.LogException(ex.DecoratedMessage, this.GetSourceCode());
			}
			if (onComplete != null)
			{
				onComplete(obj);
			}
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x0029796C File Offset: 0x00295B6C
		public virtual void DoLuaString(string luaString, string friendlyName, bool runAsCoroutine, Action<DynValue> onComplete = null)
		{
			Closure fn = this.LoadLuaFunction(luaString, friendlyName);
			this.RunLuaFunction(fn, runAsCoroutine, onComplete);
		}

		// Token: 0x04005AD7 RID: 23255
		[Tooltip("Start a Lua debug server on scene start.")]
		[SerializeField]
		protected bool startDebugServer = true;

		// Token: 0x04005AD8 RID: 23256
		[Tooltip("Port to use for the Lua debug server.")]
		[SerializeField]
		protected int debugServerPort = 41912;

		// Token: 0x04005AD9 RID: 23257
		protected Script interpreter;

		// Token: 0x04005ADA RID: 23258
		protected bool initialised;
	}
}
