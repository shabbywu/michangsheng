using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001060 RID: 4192
	public class Coroutine : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x00045452 File Offset: 0x00043652
		// (set) Token: 0x060064C6 RID: 25798 RVA: 0x0004545A File Offset: 0x0004365A
		public Coroutine.CoroutineType Type { get; private set; }

		// Token: 0x060064C7 RID: 25799 RVA: 0x00045463 File Offset: 0x00043663
		internal Coroutine(CallbackFunction function)
		{
			this.Type = Coroutine.CoroutineType.ClrCallback;
			this.m_ClrCallback = function;
			this.OwnerScript = null;
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x00045480 File Offset: 0x00043680
		internal Coroutine(Processor proc)
		{
			this.Type = Coroutine.CoroutineType.Coroutine;
			this.m_Processor = proc;
			this.m_Processor.AssociatedCoroutine = this;
			this.OwnerScript = proc.GetScript();
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x000454AE File Offset: 0x000436AE
		internal void MarkClrCallbackAsDead()
		{
			if (this.Type != Coroutine.CoroutineType.ClrCallback)
			{
				throw new InvalidOperationException("State must be CoroutineType.ClrCallback");
			}
			this.Type = Coroutine.CoroutineType.ClrCallbackDead;
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x000454CB File Offset: 0x000436CB
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

		// Token: 0x060064CB RID: 25803 RVA: 0x000454DB File Offset: 0x000436DB
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

		// Token: 0x060064CC RID: 25804 RVA: 0x000454EB File Offset: 0x000436EB
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

		// Token: 0x060064CD RID: 25805 RVA: 0x000454FB File Offset: 0x000436FB
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

		// Token: 0x060064CE RID: 25806 RVA: 0x0004550A File Offset: 0x0004370A
		public DynValue Resume(params DynValue[] args)
		{
			this.CheckScriptOwnership(args);
			if (this.Type == Coroutine.CoroutineType.Coroutine)
			{
				return this.m_Processor.Coroutine_Resume(args);
			}
			throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x00281EA0 File Offset: 0x002800A0
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

		// Token: 0x060064D0 RID: 25808 RVA: 0x00045532 File Offset: 0x00043732
		public DynValue Resume()
		{
			return this.Resume(new DynValue[0]);
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x00045540 File Offset: 0x00043740
		public DynValue Resume(ScriptExecutionContext context)
		{
			return this.Resume(context, new DynValue[0]);
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x00281EF4 File Offset: 0x002800F4
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

		// Token: 0x060064D3 RID: 25811 RVA: 0x00281F44 File Offset: 0x00280144
		public DynValue Resume(ScriptExecutionContext context, params object[] args)
		{
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(context.GetScript(), args[i]);
			}
			return this.Resume(context, array);
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060064D4 RID: 25812 RVA: 0x0004554F File Offset: 0x0004374F
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

		// Token: 0x060064D5 RID: 25813 RVA: 0x00045572 File Offset: 0x00043772
		public WatchItem[] GetStackTrace(int skip, SourceRef entrySourceRef = null)
		{
			if (this.State != CoroutineState.Running)
			{
				entrySourceRef = this.m_Processor.GetCoroutineSuspendedLocation();
			}
			return this.m_Processor.Debugger_GetCallStack(entrySourceRef).Skip(skip).ToArray<WatchItem>();
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x000455A1 File Offset: 0x000437A1
		// (set) Token: 0x060064D7 RID: 25815 RVA: 0x000455A9 File Offset: 0x000437A9
		public Script OwnerScript { get; private set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x000455B2 File Offset: 0x000437B2
		// (set) Token: 0x060064D9 RID: 25817 RVA: 0x000455BF File Offset: 0x000437BF
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

		// Token: 0x04005E0C RID: 24076
		private CallbackFunction m_ClrCallback;

		// Token: 0x04005E0D RID: 24077
		private Processor m_Processor;

		// Token: 0x02001061 RID: 4193
		public enum CoroutineType
		{
			// Token: 0x04005E10 RID: 24080
			Coroutine,
			// Token: 0x04005E11 RID: 24081
			ClrCallback,
			// Token: 0x04005E12 RID: 24082
			ClrCallbackDead
		}
	}
}
