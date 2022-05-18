using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.IO;
using MoonSharp.Interpreter.Platforms;
using MoonSharp.Interpreter.Tree.Expressions;
using MoonSharp.Interpreter.Tree.Fast_Interface;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001098 RID: 4248
	public class Script : IScriptPrivateResource
	{
		// Token: 0x06006678 RID: 26232 RVA: 0x0028433C File Offset: 0x0028253C
		static Script()
		{
			Script.DefaultOptions = new ScriptOptions
			{
				DebugPrint = delegate(string s)
				{
					Script.GlobalOptions.Platform.DefaultPrint(s);
				},
				DebugInput = ((string s) => Script.GlobalOptions.Platform.DefaultInput(s)),
				CheckThreadAccess = true,
				ScriptLoader = PlatformAutoDetector.GetDefaultScriptLoader(),
				TailCallOptimizationThreshold = 65536
			};
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x00046AE6 File Offset: 0x00044CE6
		public Script() : this(CoreModules.Preset_Default)
		{
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x002843A8 File Offset: 0x002825A8
		public Script(CoreModules coreModules)
		{
			this.Options = new ScriptOptions(Script.DefaultOptions);
			this.PerformanceStats = new PerformanceStatistics();
			this.Registry = new Table(this);
			this.m_ByteCode = new ByteCode(this);
			this.m_MainProcessor = new Processor(this, this.m_GlobalTable, this.m_ByteCode);
			this.m_GlobalTable = new Table(this).RegisterCoreModules(coreModules);
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600667B RID: 26235 RVA: 0x00046AF3 File Offset: 0x00044CF3
		// (set) Token: 0x0600667C RID: 26236 RVA: 0x00046AFA File Offset: 0x00044CFA
		public static ScriptOptions DefaultOptions { get; private set; }

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600667D RID: 26237 RVA: 0x00046B02 File Offset: 0x00044D02
		// (set) Token: 0x0600667E RID: 26238 RVA: 0x00046B0A File Offset: 0x00044D0A
		public ScriptOptions Options { get; private set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600667F RID: 26239 RVA: 0x00046B13 File Offset: 0x00044D13
		// (set) Token: 0x06006680 RID: 26240 RVA: 0x00046B1A File Offset: 0x00044D1A
		public static ScriptGlobalOptions GlobalOptions { get; private set; } = new ScriptGlobalOptions();

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06006681 RID: 26241 RVA: 0x00046B22 File Offset: 0x00044D22
		// (set) Token: 0x06006682 RID: 26242 RVA: 0x00046B2A File Offset: 0x00044D2A
		public PerformanceStatistics PerformanceStats { get; private set; }

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06006683 RID: 26243 RVA: 0x00046B33 File Offset: 0x00044D33
		public Table Globals
		{
			get
			{
				return this.m_GlobalTable;
			}
		}

		// Token: 0x06006684 RID: 26244 RVA: 0x00284430 File Offset: 0x00282630
		public DynValue LoadFunction(string code, Table globalTable = null, string funcFriendlyName = null)
		{
			this.CheckScriptOwnership(globalTable);
			SourceCode sourceCode = new SourceCode(string.Format("libfunc_{0}", funcFriendlyName ?? this.m_Sources.Count.ToString()), code, this.m_Sources.Count, this);
			this.m_Sources.Add(sourceCode);
			int address = Loader_Fast.LoadFunction(this, sourceCode, this.m_ByteCode, globalTable != null || this.m_GlobalTable != null);
			this.SignalSourceCodeChange(sourceCode);
			this.SignalByteCodeChange();
			return this.MakeClosure(address, globalTable ?? this.m_GlobalTable);
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x002844C4 File Offset: 0x002826C4
		private void SignalByteCodeChange()
		{
			if (this.m_Debugger != null)
			{
				this.m_Debugger.SetByteCode((from s in this.m_ByteCode.Code
				select s.ToString()).ToArray<string>());
			}
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x00046B3B File Offset: 0x00044D3B
		private void SignalSourceCodeChange(SourceCode source)
		{
			if (this.m_Debugger != null)
			{
				this.m_Debugger.SetSourceCode(source);
			}
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x00284518 File Offset: 0x00282718
		public DynValue LoadString(string code, Table globalTable = null, string codeFriendlyName = null)
		{
			this.CheckScriptOwnership(globalTable);
			if (code.StartsWith("MoonSharp_dump_b64::"))
			{
				code = code.Substring("MoonSharp_dump_b64::".Length);
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(code)))
				{
					return this.LoadStream(memoryStream, globalTable, codeFriendlyName);
				}
			}
			string text = string.Format("{0}", codeFriendlyName ?? ("chunk_" + this.m_Sources.Count.ToString()));
			SourceCode sourceCode = new SourceCode(codeFriendlyName ?? text, code, this.m_Sources.Count, this);
			this.m_Sources.Add(sourceCode);
			int address = Loader_Fast.LoadChunk(this, sourceCode, this.m_ByteCode);
			this.SignalSourceCodeChange(sourceCode);
			this.SignalByteCodeChange();
			return this.MakeClosure(address, globalTable ?? this.m_GlobalTable);
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x00284604 File Offset: 0x00282804
		public DynValue LoadStream(Stream stream, Table globalTable = null, string codeFriendlyName = null)
		{
			this.CheckScriptOwnership(globalTable);
			Stream stream2 = new UndisposableStream(stream);
			if (!Processor.IsDumpStream(stream2))
			{
				using (StreamReader streamReader = new StreamReader(stream2))
				{
					string code = streamReader.ReadToEnd();
					return this.LoadString(code, globalTable, codeFriendlyName);
				}
			}
			string text = string.Format("{0}", codeFriendlyName ?? ("dump_" + this.m_Sources.Count.ToString()));
			SourceCode sourceCode = new SourceCode(codeFriendlyName ?? text, string.Format("-- This script was decoded from a binary dump - dump_{0}", this.m_Sources.Count), this.m_Sources.Count, this);
			this.m_Sources.Add(sourceCode);
			bool flag;
			int address = this.m_MainProcessor.Undump(stream2, this.m_Sources.Count - 1, globalTable ?? this.m_GlobalTable, out flag);
			this.SignalSourceCodeChange(sourceCode);
			this.SignalByteCodeChange();
			if (flag)
			{
				return this.MakeClosure(address, globalTable ?? this.m_GlobalTable);
			}
			return this.MakeClosure(address, null);
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x00284728 File Offset: 0x00282928
		public void Dump(DynValue function, Stream stream)
		{
			this.CheckScriptOwnership(function);
			if (function.Type != DataType.Function)
			{
				throw new ArgumentException("function arg is not a function!");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("stream is readonly!");
			}
			Closure.UpvaluesType upvaluesType = function.Function.GetUpvaluesType();
			if (upvaluesType == Closure.UpvaluesType.Closure)
			{
				throw new ArgumentException("function arg has upvalues other than _ENV");
			}
			UndisposableStream stream2 = new UndisposableStream(stream);
			this.m_MainProcessor.Dump(stream2, function.Function.EntryPointByteCodeLocation, upvaluesType == Closure.UpvaluesType.Environment);
		}

		// Token: 0x0600668A RID: 26250 RVA: 0x002847A4 File Offset: 0x002829A4
		public DynValue LoadFile(string filename, Table globalContext = null, string friendlyFilename = null)
		{
			this.CheckScriptOwnership(globalContext);
			filename = this.Options.ScriptLoader.ResolveFileName(filename, globalContext ?? this.m_GlobalTable);
			object obj = this.Options.ScriptLoader.LoadFile(filename, globalContext ?? this.m_GlobalTable);
			if (obj is string)
			{
				return this.LoadString((string)obj, globalContext, friendlyFilename ?? filename);
			}
			if (obj is byte[])
			{
				using (MemoryStream memoryStream = new MemoryStream((byte[])obj))
				{
					return this.LoadStream(memoryStream, globalContext, friendlyFilename ?? filename);
				}
			}
			if (obj is Stream)
			{
				try
				{
					return this.LoadStream((Stream)obj, globalContext, friendlyFilename ?? filename);
				}
				finally
				{
					((Stream)obj).Dispose();
				}
			}
			if (obj == null)
			{
				throw new InvalidCastException("Unexpected null from IScriptLoader.LoadFile");
			}
			throw new InvalidCastException(string.Format("Unsupported return type from IScriptLoader.LoadFile : {0}", obj.GetType()));
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x002848AC File Offset: 0x00282AAC
		public DynValue DoString(string code, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadString(code, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x002848CC File Offset: 0x00282ACC
		public DynValue DoStream(Stream stream, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadStream(stream, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x002848EC File Offset: 0x00282AEC
		public DynValue DoFile(string filename, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadFile(filename, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x00046B51 File Offset: 0x00044D51
		public static DynValue RunFile(string filename)
		{
			return new Script().DoFile(filename, null, null);
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x00046B60 File Offset: 0x00044D60
		public static DynValue RunString(string code)
		{
			return new Script().DoString(code, null, null);
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x0028490C File Offset: 0x00282B0C
		private DynValue MakeClosure(int address, Table envTable = null)
		{
			this.CheckScriptOwnership(envTable);
			Closure function;
			if (envTable == null)
			{
				Instruction instruction = this.m_MainProcessor.FindMeta(ref address);
				if (instruction != null && instruction.NumVal2 == 0)
				{
					function = new Closure(this, address, new SymbolRef[]
					{
						SymbolRef.Upvalue("_ENV", 0)
					}, new DynValue[]
					{
						instruction.Value
					});
				}
				else
				{
					function = new Closure(this, address, new SymbolRef[0], new DynValue[0]);
				}
			}
			else
			{
				SymbolRef[] symbols = new SymbolRef[]
				{
					new SymbolRef
					{
						i_Env = null,
						i_Index = 0,
						i_Name = "_ENV",
						i_Type = SymbolRefType.DefaultEnv
					}
				};
				DynValue[] resolvedLocals = new DynValue[]
				{
					DynValue.NewTable(envTable)
				};
				function = new Closure(this, address, symbols, resolvedLocals);
			}
			return DynValue.NewClosure(function);
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x00046B6F File Offset: 0x00044D6F
		public DynValue Call(DynValue function)
		{
			return this.Call(function, new DynValue[0]);
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x002849D0 File Offset: 0x00282BD0
		public DynValue Call(DynValue function, params DynValue[] args)
		{
			this.CheckScriptOwnership(function);
			this.CheckScriptOwnership(args);
			if (function.Type != DataType.Function && function.Type != DataType.ClrFunction)
			{
				DynValue metamethod = this.m_MainProcessor.GetMetamethod(function, "__call");
				if (metamethod == null)
				{
					throw new ArgumentException("function is not a function and has no __call metamethod.");
				}
				DynValue[] array = new DynValue[args.Length + 1];
				array[0] = function;
				for (int i = 0; i < args.Length; i++)
				{
					array[i + 1] = args[i];
				}
				function = metamethod;
				args = array;
			}
			else if (function.Type == DataType.ClrFunction)
			{
				return function.Callback.ClrCallback(this.CreateDynamicExecutionContext(function.Callback), new CallbackArguments(args, false));
			}
			return this.m_MainProcessor.Call(function, args);
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x00284A88 File Offset: 0x00282C88
		public DynValue Call(DynValue function, params object[] args)
		{
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(this, args[i]);
			}
			return this.Call(function, array);
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x00046B7E File Offset: 0x00044D7E
		public DynValue Call(object function)
		{
			return this.Call(DynValue.FromObject(this, function));
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x00046B8D File Offset: 0x00044D8D
		public DynValue Call(object function, params object[] args)
		{
			return this.Call(DynValue.FromObject(this, function), args);
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x00284AC0 File Offset: 0x00282CC0
		public DynValue CreateCoroutine(DynValue function)
		{
			this.CheckScriptOwnership(function);
			if (function.Type == DataType.Function)
			{
				return this.m_MainProcessor.Coroutine_Create(function.Function);
			}
			if (function.Type == DataType.ClrFunction)
			{
				return DynValue.NewCoroutine(new Coroutine(function.Callback));
			}
			throw new ArgumentException("function is not of DataType.Function or DataType.ClrFunction");
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x00046B9D File Offset: 0x00044D9D
		public DynValue CreateCoroutine(object function)
		{
			return this.CreateCoroutine(DynValue.FromObject(this, function));
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x00046BAC File Offset: 0x00044DAC
		// (set) Token: 0x06006699 RID: 26265 RVA: 0x00046BB9 File Offset: 0x00044DB9
		public bool DebuggerEnabled
		{
			get
			{
				return this.m_MainProcessor.DebuggerEnabled;
			}
			set
			{
				this.m_MainProcessor.DebuggerEnabled = value;
			}
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x00284B14 File Offset: 0x00282D14
		public void AttachDebugger(IDebugger debugger)
		{
			this.DebuggerEnabled = true;
			this.m_Debugger = debugger;
			this.m_MainProcessor.AttachDebugger(debugger);
			foreach (SourceCode source in this.m_Sources)
			{
				this.SignalSourceCodeChange(source);
			}
			this.SignalByteCodeChange();
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x00046BC7 File Offset: 0x00044DC7
		public SourceCode GetSourceCode(int sourceCodeID)
		{
			return this.m_Sources[sourceCodeID];
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x00046BD5 File Offset: 0x00044DD5
		public int SourceCodeCount
		{
			get
			{
				return this.m_Sources.Count;
			}
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x00284B88 File Offset: 0x00282D88
		public DynValue RequireModule(string modname, Table globalContext = null)
		{
			this.CheckScriptOwnership(globalContext);
			Table globalContext2 = globalContext ?? this.m_GlobalTable;
			string text = this.Options.ScriptLoader.ResolveModuleName(modname, globalContext2);
			if (text == null)
			{
				throw new ScriptRuntimeException("module '{0}' not found", new object[]
				{
					modname
				});
			}
			return this.LoadFile(text, globalContext, text);
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x00284BDC File Offset: 0x00282DDC
		public Table GetTypeMetatable(DataType type)
		{
			if (type >= DataType.Nil && type < (DataType)this.m_TypeMetatables.Length)
			{
				return this.m_TypeMetatables[(int)type];
			}
			return null;
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x00284C04 File Offset: 0x00282E04
		public void SetTypeMetatable(DataType type, Table metatable)
		{
			this.CheckScriptOwnership(metatable);
			int num = (int)type;
			if (num >= 0 && num < this.m_TypeMetatables.Length)
			{
				this.m_TypeMetatables[num] = metatable;
				return;
			}
			throw new ArgumentException("Specified type not supported : " + type.ToString());
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x00046BE2 File Offset: 0x00044DE2
		public static void WarmUp()
		{
			new Script(CoreModules.Basic).LoadString("return 1;", null, null);
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x00284C50 File Offset: 0x00282E50
		public DynamicExpression CreateDynamicExpression(string code)
		{
			DynamicExprExpression expr = Loader_Fast.LoadDynamicExpr(this, new SourceCode("__dynamic", code, -1, this));
			return new DynamicExpression(this, code, expr);
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x00046BF8 File Offset: 0x00044DF8
		public DynamicExpression CreateConstantDynamicExpression(string code, DynValue constant)
		{
			this.CheckScriptOwnership(constant);
			return new DynamicExpression(this, code, constant);
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x00046C09 File Offset: 0x00044E09
		internal ScriptExecutionContext CreateDynamicExecutionContext(CallbackFunction func = null)
		{
			return new ScriptExecutionContext(this.m_MainProcessor, func, null, true);
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x00046C19 File Offset: 0x00044E19
		// (set) Token: 0x060066A5 RID: 26277 RVA: 0x00046C21 File Offset: 0x00044E21
		public Table Registry { get; private set; }

		// Token: 0x060066A6 RID: 26278 RVA: 0x00284C7C File Offset: 0x00282E7C
		public static string GetBanner(string subproduct = null)
		{
			subproduct = ((subproduct != null) ? (subproduct + " ") : "");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("MoonSharp {0}{1} [{2}]", subproduct, "2.0.0.0", Script.GlobalOptions.Platform.GetPlatformName()));
			stringBuilder.AppendLine("Copyright (C) 2014-2016 Marco Mastropaolo");
			stringBuilder.AppendLine("http://www.moonsharp.org");
			return stringBuilder.ToString();
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x0002FB09 File Offset: 0x0002DD09
		Script IScriptPrivateResource.OwnerScript
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04005EC3 RID: 24259
		public const string VERSION = "2.0.0.0";

		// Token: 0x04005EC4 RID: 24260
		public const string LUA_VERSION = "5.2";

		// Token: 0x04005EC5 RID: 24261
		private Processor m_MainProcessor;

		// Token: 0x04005EC6 RID: 24262
		private ByteCode m_ByteCode;

		// Token: 0x04005EC7 RID: 24263
		private List<SourceCode> m_Sources = new List<SourceCode>();

		// Token: 0x04005EC8 RID: 24264
		private Table m_GlobalTable;

		// Token: 0x04005EC9 RID: 24265
		private IDebugger m_Debugger;

		// Token: 0x04005ECA RID: 24266
		private Table[] m_TypeMetatables = new Table[6];
	}
}
