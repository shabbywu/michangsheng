using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200105E RID: 4190
	public class Closure : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x00045360 File Offset: 0x00043560
		// (set) Token: 0x060064B3 RID: 25779 RVA: 0x00045368 File Offset: 0x00043568
		public int EntryPointByteCodeLocation { get; private set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060064B4 RID: 25780 RVA: 0x00045371 File Offset: 0x00043571
		// (set) Token: 0x060064B5 RID: 25781 RVA: 0x00045379 File Offset: 0x00043579
		public Script OwnerScript { get; private set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060064B6 RID: 25782 RVA: 0x00045382 File Offset: 0x00043582
		// (set) Token: 0x060064B7 RID: 25783 RVA: 0x0004538A File Offset: 0x0004358A
		internal ClosureContext ClosureContext { get; private set; }

		// Token: 0x060064B8 RID: 25784 RVA: 0x00045393 File Offset: 0x00043593
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

		// Token: 0x060064B9 RID: 25785 RVA: 0x000453C7 File Offset: 0x000435C7
		public DynValue Call()
		{
			return this.OwnerScript.Call(this);
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x000453D5 File Offset: 0x000435D5
		public DynValue Call(params object[] args)
		{
			return this.OwnerScript.Call(this, args);
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x00281E50 File Offset: 0x00280050
		public DynValue Call(params DynValue[] args)
		{
			return this.OwnerScript.Call(this, args);
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x000453E4 File Offset: 0x000435E4
		public ScriptFunctionDelegate GetDelegate()
		{
			return (object[] args) => this.Call(args).ToObject();
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x000453F2 File Offset: 0x000435F2
		public ScriptFunctionDelegate<T> GetDelegate<T>()
		{
			return (object[] args) => this.Call(args).ToObject<T>();
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x00045400 File Offset: 0x00043600
		public int GetUpvaluesCount()
		{
			return this.ClosureContext.Count;
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x0004540D File Offset: 0x0004360D
		public string GetUpvalueName(int idx)
		{
			return this.ClosureContext.Symbols[idx];
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x0004541C File Offset: 0x0004361C
		public DynValue GetUpvalue(int idx)
		{
			return this.ClosureContext[idx];
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x00281E6C File Offset: 0x0028006C
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

		// Token: 0x04005E05 RID: 24069
		private static ClosureContext emptyClosure = new ClosureContext();

		// Token: 0x0200105F RID: 4191
		public enum UpvaluesType
		{
			// Token: 0x04005E08 RID: 24072
			None,
			// Token: 0x04005E09 RID: 24073
			Environment,
			// Token: 0x04005E0A RID: 24074
			Closure
		}
	}
}
