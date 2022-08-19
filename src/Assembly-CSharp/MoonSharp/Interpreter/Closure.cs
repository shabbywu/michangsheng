using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C98 RID: 3224
	public class Closure : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060059F0 RID: 23024 RVA: 0x0025700C File Offset: 0x0025520C
		// (set) Token: 0x060059F1 RID: 23025 RVA: 0x00257014 File Offset: 0x00255214
		public int EntryPointByteCodeLocation { get; private set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x0025701D File Offset: 0x0025521D
		// (set) Token: 0x060059F3 RID: 23027 RVA: 0x00257025 File Offset: 0x00255225
		public Script OwnerScript { get; private set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x0025702E File Offset: 0x0025522E
		// (set) Token: 0x060059F5 RID: 23029 RVA: 0x00257036 File Offset: 0x00255236
		internal ClosureContext ClosureContext { get; private set; }

		// Token: 0x060059F6 RID: 23030 RVA: 0x0025703F File Offset: 0x0025523F
		internal Closure(Script script, int idx, SymbolRef[] symbols, IEnumerable<DynValue> resolvedLocals)
		{
			this.OwnerScript = script;
			this.EntryPointByteCodeLocation = idx;
			if (symbols.Length != 0)
			{
				this.ClosureContext = new ClosureContext(symbols, resolvedLocals);
				return;
			}
			this.ClosureContext = Closure.emptyClosure;
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x00257073 File Offset: 0x00255273
		public DynValue Call()
		{
			return this.OwnerScript.Call(this);
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00257081 File Offset: 0x00255281
		public DynValue Call(params object[] args)
		{
			return this.OwnerScript.Call(this, args);
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00257090 File Offset: 0x00255290
		public DynValue Call(params DynValue[] args)
		{
			return this.OwnerScript.Call(this, args);
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x002570AC File Offset: 0x002552AC
		public ScriptFunctionDelegate GetDelegate()
		{
			return (object[] args) => this.Call(args).ToObject();
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x002570BA File Offset: 0x002552BA
		public ScriptFunctionDelegate<T> GetDelegate<T>()
		{
			return (object[] args) => this.Call(args).ToObject<T>();
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x002570C8 File Offset: 0x002552C8
		public int GetUpvaluesCount()
		{
			return this.ClosureContext.Count;
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x002570D5 File Offset: 0x002552D5
		public string GetUpvalueName(int idx)
		{
			return this.ClosureContext.Symbols[idx];
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x002570E4 File Offset: 0x002552E4
		public DynValue GetUpvalue(int idx)
		{
			return this.ClosureContext[idx];
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x002570F4 File Offset: 0x002552F4
		public Closure.UpvaluesType GetUpvaluesType()
		{
			int upvaluesCount = this.GetUpvaluesCount();
			if (upvaluesCount == 0)
			{
				return Closure.UpvaluesType.None;
			}
			if (upvaluesCount == 1 && this.GetUpvalueName(0) == "_ENV")
			{
				return Closure.UpvaluesType.Environment;
			}
			return Closure.UpvaluesType.Closure;
		}

		// Token: 0x04005256 RID: 21078
		private static ClosureContext emptyClosure = new ClosureContext();

		// Token: 0x0200163B RID: 5691
		public enum UpvaluesType
		{
			// Token: 0x0400720D RID: 29197
			None,
			// Token: 0x0400720E RID: 29198
			Environment,
			// Token: 0x0400720F RID: 29199
			Closure
		}
	}
}
