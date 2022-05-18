using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001081 RID: 4225
	public class ScriptExecutionContext : IScriptPrivateResource
	{
		// Token: 0x06006626 RID: 26150 RVA: 0x000467A4 File Offset: 0x000449A4
		internal ScriptExecutionContext(Processor p, CallbackFunction callBackFunction, SourceRef sourceRef, bool isDynamic = false)
		{
			this.IsDynamicExecution = isDynamic;
			this.m_Processor = p;
			this.m_Callback = callBackFunction;
			this.CallingLocation = sourceRef;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06006627 RID: 26151 RVA: 0x000467C9 File Offset: 0x000449C9
		// (set) Token: 0x06006628 RID: 26152 RVA: 0x000467D1 File Offset: 0x000449D1
		public bool IsDynamicExecution { get; private set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06006629 RID: 26153 RVA: 0x000467DA File Offset: 0x000449DA
		// (set) Token: 0x0600662A RID: 26154 RVA: 0x000467E2 File Offset: 0x000449E2
		public SourceRef CallingLocation { get; private set; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x000467EB File Offset: 0x000449EB
		// (set) Token: 0x0600662C RID: 26156 RVA: 0x00046802 File Offset: 0x00044A02
		public object AdditionalData
		{
			get
			{
				if (this.m_Callback == null)
				{
					return null;
				}
				return this.m_Callback.AdditionalData;
			}
			set
			{
				if (this.m_Callback == null)
				{
					throw new InvalidOperationException("Cannot set additional data on a context which has no callback");
				}
				this.m_Callback.AdditionalData = value;
			}
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x00046823 File Offset: 0x00044A23
		public Table GetMetatable(DynValue value)
		{
			return this.m_Processor.GetMetatable(value);
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x00046831 File Offset: 0x00044A31
		public DynValue GetMetamethod(DynValue value, string metamethod)
		{
			return this.m_Processor.GetMetamethod(value, metamethod);
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x002839D0 File Offset: 0x00281BD0
		public DynValue GetMetamethodTailCall(DynValue value, string metamethod, params DynValue[] args)
		{
			DynValue metamethod2 = this.GetMetamethod(value, metamethod);
			if (metamethod2 == null)
			{
				return null;
			}
			return DynValue.NewTailCallReq(metamethod2, args);
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x00046840 File Offset: 0x00044A40
		public DynValue GetBinaryMetamethod(DynValue op1, DynValue op2, string eventName)
		{
			return this.m_Processor.GetBinaryMetamethod(op1, op2, eventName);
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x00046850 File Offset: 0x00044A50
		public Script GetScript()
		{
			return this.m_Processor.GetScript();
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x0004685D File Offset: 0x00044A5D
		public Coroutine GetCallingCoroutine()
		{
			return this.m_Processor.AssociatedCoroutine;
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x002839F4 File Offset: 0x00281BF4
		public DynValue EmulateClassicCall(CallbackArguments args, string functionName, Func<LuaState, int> callback)
		{
			LuaState luaState = new LuaState(this, args, functionName);
			int retvals = callback(luaState);
			return luaState.GetReturnValue(retvals);
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x00283A1C File Offset: 0x00281C1C
		public DynValue Call(DynValue func, params DynValue[] args)
		{
			if (func.Type == DataType.Function)
			{
				return this.GetScript().Call(func, args);
			}
			if (func.Type == DataType.ClrFunction)
			{
				for (;;)
				{
					DynValue dynValue = func.Callback.Invoke(this, args, false);
					if (dynValue.Type == DataType.YieldRequest)
					{
						break;
					}
					if (dynValue.Type != DataType.TailCallRequest)
					{
						return dynValue;
					}
					TailCallData tailCallData = dynValue.TailCallData;
					if (tailCallData.Continuation != null || tailCallData.ErrorHandler != null)
					{
						goto IL_61;
					}
					args = tailCallData.Args;
					func = tailCallData.Function;
				}
				throw ScriptRuntimeException.CannotYield();
				IL_61:
				throw new ScriptRuntimeException("the function passed cannot be called directly. wrap in a script function instead.");
			}
			int i = 10;
			while (i > 0)
			{
				DynValue metamethod = this.GetMetamethod(func, "__call");
				if (metamethod == null && metamethod.IsNil())
				{
					throw ScriptRuntimeException.AttemptToCallNonFunc(func.Type, null);
				}
				func = metamethod;
				if (func.Type == DataType.Function || func.Type == DataType.ClrFunction)
				{
					return this.Call(func, args);
				}
			}
			throw ScriptRuntimeException.LoopInCall();
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x0004686A File Offset: 0x00044A6A
		public DynValue EvaluateSymbol(SymbolRef symref)
		{
			if (symref == null)
			{
				return DynValue.Nil;
			}
			return this.m_Processor.GetGenericSymbol(symref);
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x00046881 File Offset: 0x00044A81
		public DynValue EvaluateSymbolByName(string symbol)
		{
			return this.EvaluateSymbol(this.FindSymbolByName(symbol));
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x00046890 File Offset: 0x00044A90
		public SymbolRef FindSymbolByName(string symbol)
		{
			return this.m_Processor.FindSymbolByName(symbol);
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x00283AFC File Offset: 0x00281CFC
		public Table CurrentGlobalEnv
		{
			get
			{
				DynValue dynValue = this.EvaluateSymbolByName("_ENV");
				if (dynValue == null || dynValue.Type != DataType.Table)
				{
					return null;
				}
				return dynValue.Table;
			}
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x0004689E File Offset: 0x00044A9E
		public void PerformMessageDecorationBeforeUnwind(DynValue messageHandler, ScriptRuntimeException exception)
		{
			if (messageHandler != null)
			{
				exception.DecoratedMessage = this.m_Processor.PerformMessageDecorationBeforeUnwind(messageHandler, exception.Message, this.CallingLocation);
				return;
			}
			exception.DecoratedMessage = exception.Message;
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600663A RID: 26170 RVA: 0x000468CE File Offset: 0x00044ACE
		public Script OwnerScript
		{
			get
			{
				return this.GetScript();
			}
		}

		// Token: 0x04005E86 RID: 24198
		private Processor m_Processor;

		// Token: 0x04005E87 RID: 24199
		private CallbackFunction m_Callback;
	}
}
