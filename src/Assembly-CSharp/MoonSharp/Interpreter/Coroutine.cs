using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C99 RID: 3225
	public class Coroutine : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06005A03 RID: 23043 RVA: 0x0025714F File Offset: 0x0025534F
		// (set) Token: 0x06005A04 RID: 23044 RVA: 0x00257157 File Offset: 0x00255357
		public Coroutine.CoroutineType Type { get; private set; }

		// Token: 0x06005A05 RID: 23045 RVA: 0x00257160 File Offset: 0x00255360
		internal Coroutine(CallbackFunction function)
		{
			this.Type = Coroutine.CoroutineType.ClrCallback;
			this.m_ClrCallback = function;
			this.OwnerScript = null;
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x0025717D File Offset: 0x0025537D
		internal Coroutine(Processor proc)
		{
			this.Type = Coroutine.CoroutineType.Coroutine;
			this.m_Processor = proc;
			this.m_Processor.AssociatedCoroutine = this;
			this.OwnerScript = proc.GetScript();
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x002571AB File Offset: 0x002553AB
		internal void MarkClrCallbackAsDead()
		{
			if (this.Type != Coroutine.CoroutineType.ClrCallback)
			{
				throw new InvalidOperationException("State must be CoroutineType.ClrCallback");
			}
			this.Type = Coroutine.CoroutineType.ClrCallbackDead;
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x002571C8 File Offset: 0x002553C8
		public IEnumerable<DynValue> AsTypedEnumerable()
		{
			if (this.Type != Coroutine.CoroutineType.Coroutine)
			{
				throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
			}
			while (this.State == CoroutineState.NotStarted || this.State == CoroutineState.Suspended || this.State == CoroutineState.ForceSuspended)
			{
				yield return this.Resume();
			}
			yield break;
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x002571D8 File Offset: 0x002553D8
		public IEnumerable<object> AsEnumerable()
		{
			foreach (DynValue dynValue in this.AsTypedEnumerable())
			{
				yield return dynValue.ToScalar().ToObject();
			}
			IEnumerator<DynValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x002571E8 File Offset: 0x002553E8
		public IEnumerable<T> AsEnumerable<T>()
		{
			foreach (DynValue dynValue in this.AsTypedEnumerable())
			{
				yield return dynValue.ToScalar().ToObject<T>();
			}
			IEnumerator<DynValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x002571F8 File Offset: 0x002553F8
		public IEnumerator AsUnityCoroutine()
		{
			foreach (DynValue dynValue in this.AsTypedEnumerable())
			{
				yield return null;
			}
			IEnumerator<DynValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x00257207 File Offset: 0x00255407
		public DynValue Resume(params DynValue[] args)
		{
			this.CheckScriptOwnership(args);
			if (this.Type == Coroutine.CoroutineType.Coroutine)
			{
				return this.m_Processor.Coroutine_Resume(args);
			}
			throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x00257230 File Offset: 0x00255430
		public DynValue Resume(ScriptExecutionContext context, params DynValue[] args)
		{
			this.CheckScriptOwnership(context);
			this.CheckScriptOwnership(args);
			if (this.Type == Coroutine.CoroutineType.Coroutine)
			{
				return this.m_Processor.Coroutine_Resume(args);
			}
			if (this.Type == Coroutine.CoroutineType.ClrCallback)
			{
				DynValue result = this.m_ClrCallback.Invoke(context, args, false);
				this.MarkClrCallbackAsDead();
				return result;
			}
			throw ScriptRuntimeException.CannotResumeNotSuspended(CoroutineState.Dead);
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x00257284 File Offset: 0x00255484
		public DynValue Resume()
		{
			return this.Resume(new DynValue[0]);
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x00257292 File Offset: 0x00255492
		public DynValue Resume(ScriptExecutionContext context)
		{
			return this.Resume(context, new DynValue[0]);
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x002572A4 File Offset: 0x002554A4
		public DynValue Resume(params object[] args)
		{
			if (this.Type != Coroutine.CoroutineType.Coroutine)
			{
				throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
			}
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(this.OwnerScript, args[i]);
			}
			return this.Resume(array);
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x002572F4 File Offset: 0x002554F4
		public DynValue Resume(ScriptExecutionContext context, params object[] args)
		{
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(context.GetScript(), args[i]);
			}
			return this.Resume(context, array);
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x00257331 File Offset: 0x00255531
		public CoroutineState State
		{
			get
			{
				if (this.Type == Coroutine.CoroutineType.ClrCallback)
				{
					return CoroutineState.NotStarted;
				}
				if (this.Type == Coroutine.CoroutineType.ClrCallbackDead)
				{
					return CoroutineState.Dead;
				}
				return this.m_Processor.State;
			}
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x00257354 File Offset: 0x00255554
		public WatchItem[] GetStackTrace(int skip, SourceRef entrySourceRef = null)
		{
			if (this.State != CoroutineState.Running)
			{
				entrySourceRef = this.m_Processor.GetCoroutineSuspendedLocation();
			}
			return this.m_Processor.Debugger_GetCallStack(entrySourceRef).Skip(skip).ToArray<WatchItem>();
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x00257383 File Offset: 0x00255583
		// (set) Token: 0x06005A15 RID: 23061 RVA: 0x0025738B File Offset: 0x0025558B
		public Script OwnerScript { get; private set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x00257394 File Offset: 0x00255594
		// (set) Token: 0x06005A17 RID: 23063 RVA: 0x002573A1 File Offset: 0x002555A1
		public long AutoYieldCounter
		{
			get
			{
				return this.m_Processor.AutoYieldCounter;
			}
			set
			{
				this.m_Processor.AutoYieldCounter = value;
			}
		}

		// Token: 0x04005259 RID: 21081
		private CallbackFunction m_ClrCallback;

		// Token: 0x0400525A RID: 21082
		private Processor m_Processor;

		// Token: 0x0200163C RID: 5692
		public enum CoroutineType
		{
			// Token: 0x04007211 RID: 29201
			Coroutine,
			// Token: 0x04007212 RID: 29202
			ClrCallback,
			// Token: 0x04007213 RID: 29203
			ClrCallbackDead
		}
	}
}
