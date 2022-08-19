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
	// Token: 0x02000CC3 RID: 3267
	public class Script : IScriptPrivateResource
	{
		// Token: 0x06005B74 RID: 23412 RVA: 0x0025A670 File Offset: 0x00258870
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

		// Token: 0x06005B75 RID: 23413 RVA: 0x0025A6DA File Offset: 0x002588DA
		public Script() : this(CoreModules.Preset_Default)
		{
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x0025A6E8 File Offset: 0x002588E8
		public Script(CoreModules coreModules)
		{
			this.Options = new ScriptOptions(Script.DefaultOptions);
			this.PerformanceStats = new PerformanceStatistics();
			this.Registry = new Table(this);
			this.m_ByteCode = new ByteCode(this);
			this.m_MainProcessor = new Processor(this, this.m_GlobalTable, this.m_ByteCode);
			this.m_GlobalTable = new Table(this).RegisterCoreModules(coreModules);
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06005B77 RID: 23415 RVA: 0x0025A76F File Offset: 0x0025896F
		// (set) Token: 0x06005B78 RID: 23416 RVA: 0x0025A776 File Offset: 0x00258976
		public static ScriptOptions DefaultOptions { get; private set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06005B79 RID: 23417 RVA: 0x0025A77E File Offset: 0x0025897E
		// (set) Token: 0x06005B7A RID: 23418 RVA: 0x0025A786 File Offset: 0x00258986
		public ScriptOptions Options { get; private set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06005B7B RID: 23419 RVA: 0x0025A78F File Offset: 0x0025898F
		// (set) Token: 0x06005B7C RID: 23420 RVA: 0x0025A796 File Offset: 0x00258996
		public static ScriptGlobalOptions GlobalOptions { get; private set; } = new ScriptGlobalOptions();

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06005B7D RID: 23421 RVA: 0x0025A79E File Offset: 0x0025899E
		// (set) Token: 0x06005B7E RID: 23422 RVA: 0x0025A7A6 File Offset: 0x002589A6
		public PerformanceStatistics PerformanceStats { get; private set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06005B7F RID: 23423 RVA: 0x0025A7AF File Offset: 0x002589AF
		public Table Globals
		{
			get
			{
				return this.m_GlobalTable;
			}
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x0025A7B8 File Offset: 0x002589B8
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

		// Token: 0x06005B81 RID: 23425 RVA: 0x0025A84C File Offset: 0x00258A4C
		private void SignalByteCodeChange()
		{
			if (this.m_Debugger != null)
			{
				this.m_Debugger.SetByteCode((from s in this.m_ByteCode.Code
				select s.ToString()).ToArray<string>());
			}
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0025A8A0 File Offset: 0x00258AA0
		private void SignalSourceCodeChange(SourceCode source)
		{
			if (this.m_Debugger != null)
			{
				this.m_Debugger.SetSourceCode(source);
			}
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0025A8B8 File Offset: 0x00258AB8
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

		// Token: 0x06005B84 RID: 23428 RVA: 0x0025A9A4 File Offset: 0x00258BA4
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

		// Token: 0x06005B85 RID: 23429 RVA: 0x0025AAC8 File Offset: 0x00258CC8
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

		// Token: 0x06005B86 RID: 23430 RVA: 0x0025AB44 File Offset: 0x00258D44
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

		// Token: 0x06005B87 RID: 23431 RVA: 0x0025AC4C File Offset: 0x00258E4C
		public DynValue DoString(string code, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadString(code, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x0025AC6C File Offset: 0x00258E6C
		public DynValue DoStream(Stream stream, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadStream(stream, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x0025AC8C File Offset: 0x00258E8C
		public DynValue DoFile(string filename, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadFile(filename, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0025ACAA File Offset: 0x00258EAA
		public static DynValue RunFile(string filename)
		{
			return new Script().DoFile(filename, null, null);
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x0025ACB9 File Offset: 0x00258EB9
		public static DynValue RunString(string code)
		{
			return new Script().DoString(code, null, null);
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x0025ACC8 File Offset: 0x00258EC8
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

		// Token: 0x06005B8D RID: 23437 RVA: 0x0025AD8A File Offset: 0x00258F8A
		public DynValue Call(DynValue function)
		{
			return this.Call(function, new DynValue[0]);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0025AD9C File Offset: 0x00258F9C
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

		// Token: 0x06005B8F RID: 23439 RVA: 0x0025AE54 File Offset: 0x00259054
		public DynValue Call(DynValue function, params object[] args)
		{
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(this, args[i]);
			}
			return this.Call(function, array);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0025AE8C File Offset: 0x0025908C
		public DynValue Call(object function)
		{
			return this.Call(DynValue.FromObject(this, function));
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0025AE9B File Offset: 0x0025909B
		public DynValue Call(object function, params object[] args)
		{
			return this.Call(DynValue.FromObject(this, function), args);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0025AEAC File Offset: 0x002590AC
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

		// Token: 0x06005B93 RID: 23443 RVA: 0x0025AF00 File Offset: 0x00259100
		public DynValue CreateCoroutine(object function)
		{
			return this.CreateCoroutine(DynValue.FromObject(this, function));
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06005B94 RID: 23444 RVA: 0x0025AF0F File Offset: 0x0025910F
		// (set) Token: 0x06005B95 RID: 23445 RVA: 0x0025AF1C File Offset: 0x0025911C
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

		// Token: 0x06005B96 RID: 23446 RVA: 0x0025AF2C File Offset: 0x0025912C
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

		// Token: 0x06005B97 RID: 23447 RVA: 0x0025AFA0 File Offset: 0x002591A0
		public SourceCode GetSourceCode(int sourceCodeID)
		{
			return this.m_Sources[sourceCodeID];
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06005B98 RID: 23448 RVA: 0x0025AFAE File Offset: 0x002591AE
		public int SourceCodeCount
		{
			get
			{
				return this.m_Sources.Count;
			}
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x0025AFBC File Offset: 0x002591BC
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

		// Token: 0x06005B9A RID: 23450 RVA: 0x0025B010 File Offset: 0x00259210
		public Table GetTypeMetatable(DataType type)
		{
			if (type >= DataType.Nil && type < (DataType)this.m_TypeMetatables.Length)
			{
				return this.m_TypeMetatables[(int)type];
			}
			return null;
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x0025B038 File Offset: 0x00259238
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

		// Token: 0x06005B9C RID: 23452 RVA: 0x0025B083 File Offset: 0x00259283
		public static void WarmUp()
		{
			new Script(CoreModules.Basic).LoadString("return 1;", null, null);
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x0025B09C File Offset: 0x0025929C
		public DynamicExpression CreateDynamicExpression(string code)
		{
			DynamicExprExpression expr = Loader_Fast.LoadDynamicExpr(this, new SourceCode("__dynamic", code, -1, this));
			return new DynamicExpression(this, code, expr);
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x0025B0C5 File Offset: 0x002592C5
		public DynamicExpression CreateConstantDynamicExpression(string code, DynValue constant)
		{
			this.CheckScriptOwnership(constant);
			return new DynamicExpression(this, code, constant);
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x0025B0D6 File Offset: 0x002592D6
		internal ScriptExecutionContext CreateDynamicExecutionContext(CallbackFunction func = null)
		{
			return new ScriptExecutionContext(this.m_MainProcessor, func, null, true);
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06005BA0 RID: 23456 RVA: 0x0025B0E6 File Offset: 0x002592E6
		// (set) Token: 0x06005BA1 RID: 23457 RVA: 0x0025B0EE File Offset: 0x002592EE
		public Table Registry { get; private set; }

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0025B0F8 File Offset: 0x002592F8
		public static string GetBanner(string subproduct = null)
		{
			subproduct = ((subproduct != null) ? (subproduct + " ") : "");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("MoonSharp {0}{1} [{2}]", subproduct, "2.0.0.0", Script.GlobalOptions.Platform.GetPlatformName()));
			stringBuilder.AppendLine("Copyright (C) 2014-2016 Marco Mastropaolo");
			stringBuilder.AppendLine("http://www.moonsharp.org");
			return stringBuilder.ToString();
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06005BA3 RID: 23459 RVA: 0x00183D24 File Offset: 0x00181F24
		Script IScriptPrivateResource.OwnerScript
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040052E2 RID: 21218
		public const string VERSION = "2.0.0.0";

		// Token: 0x040052E3 RID: 21219
		public const string LUA_VERSION = "5.2";

		// Token: 0x040052E4 RID: 21220
		private Processor m_MainProcessor;

		// Token: 0x040052E5 RID: 21221
		private ByteCode m_ByteCode;

		// Token: 0x040052E6 RID: 21222
		private List<SourceCode> m_Sources = new List<SourceCode>();

		// Token: 0x040052E7 RID: 21223
		private Table m_GlobalTable;

		// Token: 0x040052E8 RID: 21224
		private IDebugger m_Debugger;

		// Token: 0x040052E9 RID: 21225
		private Table[] m_TypeMetatables = new Table[6];
	}
}
