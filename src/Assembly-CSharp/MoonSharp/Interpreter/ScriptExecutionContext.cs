using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB2 RID: 3250
	public class ScriptExecutionContext : IScriptPrivateResource
	{
		// Token: 0x06005B34 RID: 23348 RVA: 0x00259A7A File Offset: 0x00257C7A
		internal ScriptExecutionContext(Processor p, CallbackFunction callBackFunction, SourceRef sourceRef, bool isDynamic = false)
		{
			this.IsDynamicExecution = isDynamic;
			this.m_Processor = p;
			this.m_Callback = callBackFunction;
			this.CallingLocation = sourceRef;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x00259A9F File Offset: 0x00257C9F
		// (set) Token: 0x06005B36 RID: 23350 RVA: 0x00259AA7 File Offset: 0x00257CA7
		public bool IsDynamicExecution { get; private set; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06005B37 RID: 23351 RVA: 0x00259AB0 File Offset: 0x00257CB0
		// (set) Token: 0x06005B38 RID: 23352 RVA: 0x00259AB8 File Offset: 0x00257CB8
		public SourceRef CallingLocation { get; private set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06005B39 RID: 23353 RVA: 0x00259AC1 File Offset: 0x00257CC1
		// (set) Token: 0x06005B3A RID: 23354 RVA: 0x00259AD8 File Offset: 0x00257CD8
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

		// Token: 0x06005B3B RID: 23355 RVA: 0x00259AF9 File Offset: 0x00257CF9
		public Table GetMetatable(DynValue value)
		{
			return this.m_Processor.GetMetatable(value);
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x00259B07 File Offset: 0x00257D07
		public DynValue GetMetamethod(DynValue value, string metamethod)
		{
			return this.m_Processor.GetMetamethod(value, metamethod);
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x00259B18 File Offset: 0x00257D18
		public DynValue GetMetamethodTailCall(DynValue value, string metamethod, params DynValue[] args)
		{
			DynValue metamethod2 = this.GetMetamethod(value, metamethod);
			if (metamethod2 == null)
			{
				return null;
			}
			return DynValue.NewTailCallReq(metamethod2, args);
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x00259B3A File Offset: 0x00257D3A
		public DynValue GetBinaryMetamethod(DynValue op1, DynValue op2, string eventName)
		{
			return this.m_Processor.GetBinaryMetamethod(op1, op2, eventName);
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x00259B4A File Offset: 0x00257D4A
		public Script GetScript()
		{
			return this.m_Processor.GetScript();
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x00259B57 File Offset: 0x00257D57
		public Coroutine GetCallingCoroutine()
		{
			return this.m_Processor.AssociatedCoroutine;
		}

		// Token: 0x06005B41 RID: 23361 RVA: 0x00259B64 File Offset: 0x00257D64
		public DynValue EmulateClassicCall(CallbackArguments args, string functionName, Func<LuaState, int> callback)
		{
			LuaState luaState = new LuaState(this, args, functionName);
			int retvals = callback(luaState);
			return luaState.GetReturnValue(retvals);
		}

		// Token: 0x06005B42 RID: 23362 RVA: 0x00259B8C File Offset: 0x00257D8C
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

		// Token: 0x06005B43 RID: 23363 RVA: 0x00259C6B File Offset: 0x00257E6B
		public DynValue EvaluateSymbol(SymbolRef symref)
		{
			if (symref == null)
			{
				return DynValue.Nil;
			}
			return this.m_Processor.GetGenericSymbol(symref);
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x00259C82 File Offset: 0x00257E82
		public DynValue EvaluateSymbolByName(string symbol)
		{
			return this.EvaluateSymbol(this.FindSymbolByName(symbol));
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x00259C91 File Offset: 0x00257E91
		public SymbolRef FindSymbolByName(string symbol)
		{
			return this.m_Processor.FindSymbolByName(symbol);
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x00259CA0 File Offset: 0x00257EA0
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

		// Token: 0x06005B47 RID: 23367 RVA: 0x00259CCD File Offset: 0x00257ECD
		public void PerformMessageDecorationBeforeUnwind(DynValue messageHandler, ScriptRuntimeException exception)
		{
			if (messageHandler != null)
			{
				exception.DecoratedMessage = this.m_Processor.PerformMessageDecorationBeforeUnwind(messageHandler, exception.Message, this.CallingLocation);
				return;
			}
			exception.DecoratedMessage = exception.Message;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x00259CFD File Offset: 0x00257EFD
		public Script OwnerScript
		{
			get
			{
				return this.GetScript();
			}
		}

		// Token: 0x040052B1 RID: 21169
		private Processor m_Processor;

		// Token: 0x040052B2 RID: 21170
		private CallbackFunction m_Callback;
	}
}
