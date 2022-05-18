using System;
using System.Collections;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.VsCodeDebugger;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200139D RID: 5021
	public class LuaEnvironment : MonoBehaviour
	{
		// Token: 0x0600798F RID: 31119 RVA: 0x0005301A File Offset: 0x0005121A
		protected virtual void Start()
		{
			this.InitEnvironment();
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x00053022 File Offset: 0x00051222
		protected virtual void OnDestroy()
		{
			if (LuaEnvironment.DebugServer != null)
			{
				LuaEnvironment.DebugServer.Detach(this.interpreter);
			}
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x002B8878 File Offset: 0x002B6A78
		protected virtual void InitLuaScriptFiles()
		{
			object[] array = Resources.LoadAll("Lua", typeof(TextAsset));
			object[] source = array;
			this.interpreter.Options.ScriptLoader = new LuaScriptLoader(source.OfType<TextAsset>());
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x0005303B File Offset: 0x0005123B
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

		// Token: 0x06007993 RID: 31123 RVA: 0x002B88B8 File Offset: 0x002B6AB8
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

		// Token: 0x06007994 RID: 31124 RVA: 0x00053058 File Offset: 0x00051258
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

		// Token: 0x06007995 RID: 31125 RVA: 0x002B8900 File Offset: 0x002B6B00
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x0005306E File Offset: 0x0005126E
		// (set) Token: 0x06007997 RID: 31127 RVA: 0x00053075 File Offset: 0x00051275
		public static MoonSharpVsCodeDebugServer DebugServer { get; private set; }

		// Token: 0x06007998 RID: 31128 RVA: 0x002B8988 File Offset: 0x002B6B88
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

		// Token: 0x06007999 RID: 31129 RVA: 0x002B89D0 File Offset: 0x002B6BD0
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

		// Token: 0x0600799A RID: 31130 RVA: 0x0005307D File Offset: 0x0005127D
		public virtual Task RunUnityCoroutine(IEnumerator coroutine)
		{
			if (coroutine == null)
			{
				return null;
			}
			return new Task(this.RunUnityCoroutineImpl(coroutine), true);
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x002B8A60 File Offset: 0x002B6C60
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

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600799C RID: 31132 RVA: 0x00053091 File Offset: 0x00051291
		public virtual Script Interpreter
		{
			get
			{
				return this.interpreter;
			}
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x002B8ADC File Offset: 0x002B6CDC
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

		// Token: 0x0600799E RID: 31134 RVA: 0x002B8B5C File Offset: 0x002B6D5C
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

		// Token: 0x0600799F RID: 31135 RVA: 0x002B8BC4 File Offset: 0x002B6DC4
		public virtual void DoLuaString(string luaString, string friendlyName, bool runAsCoroutine, Action<DynValue> onComplete = null)
		{
			Closure fn = this.LoadLuaFunction(luaString, friendlyName);
			this.RunLuaFunction(fn, runAsCoroutine, onComplete);
		}

		// Token: 0x04006943 RID: 26947
		[Tooltip("Start a Lua debug server on scene start.")]
		[SerializeField]
		protected bool startDebugServer = true;

		// Token: 0x04006944 RID: 26948
		[Tooltip("Port to use for the Lua debug server.")]
		[SerializeField]
		protected int debugServerPort = 41912;

		// Token: 0x04006945 RID: 26949
		protected Script interpreter;

		// Token: 0x04006946 RID: 26950
		protected bool initialised;
	}
}
