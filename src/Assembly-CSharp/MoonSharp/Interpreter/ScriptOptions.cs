using System;
using System.IO;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CC5 RID: 3269
	public class ScriptOptions
	{
		// Token: 0x06005BAB RID: 23467 RVA: 0x000027FC File Offset: 0x000009FC
		internal ScriptOptions()
		{
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x0025B1B8 File Offset: 0x002593B8
		internal ScriptOptions(ScriptOptions defaults)
		{
			this.DebugInput = defaults.DebugInput;
			this.DebugPrint = defaults.DebugPrint;
			this.UseLuaErrorLocations = defaults.UseLuaErrorLocations;
			this.Stdin = defaults.Stdin;
			this.Stdout = defaults.Stdout;
			this.Stderr = defaults.Stderr;
			this.TailCallOptimizationThreshold = defaults.TailCallOptimizationThreshold;
			this.ScriptLoader = defaults.ScriptLoader;
			this.CheckThreadAccess = defaults.CheckThreadAccess;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x0025B237 File Offset: 0x00259437
		// (set) Token: 0x06005BAE RID: 23470 RVA: 0x0025B23F File Offset: 0x0025943F
		public IScriptLoader ScriptLoader { get; set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x0025B248 File Offset: 0x00259448
		// (set) Token: 0x06005BB0 RID: 23472 RVA: 0x0025B250 File Offset: 0x00259450
		public Action<string> DebugPrint { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x0025B259 File Offset: 0x00259459
		// (set) Token: 0x06005BB2 RID: 23474 RVA: 0x0025B261 File Offset: 0x00259461
		public Func<string, string> DebugInput { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06005BB3 RID: 23475 RVA: 0x0025B26A File Offset: 0x0025946A
		// (set) Token: 0x06005BB4 RID: 23476 RVA: 0x0025B272 File Offset: 0x00259472
		public bool UseLuaErrorLocations { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06005BB5 RID: 23477 RVA: 0x0025B27B File Offset: 0x0025947B
		// (set) Token: 0x06005BB6 RID: 23478 RVA: 0x0025B283 File Offset: 0x00259483
		public ColonOperatorBehaviour ColonOperatorClrCallbackBehaviour { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x0025B28C File Offset: 0x0025948C
		// (set) Token: 0x06005BB8 RID: 23480 RVA: 0x0025B294 File Offset: 0x00259494
		public Stream Stdin { get; set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06005BB9 RID: 23481 RVA: 0x0025B29D File Offset: 0x0025949D
		// (set) Token: 0x06005BBA RID: 23482 RVA: 0x0025B2A5 File Offset: 0x002594A5
		public Stream Stdout { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06005BBB RID: 23483 RVA: 0x0025B2AE File Offset: 0x002594AE
		// (set) Token: 0x06005BBC RID: 23484 RVA: 0x0025B2B6 File Offset: 0x002594B6
		public Stream Stderr { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06005BBD RID: 23485 RVA: 0x0025B2BF File Offset: 0x002594BF
		// (set) Token: 0x06005BBE RID: 23486 RVA: 0x0025B2C7 File Offset: 0x002594C7
		public int TailCallOptimizationThreshold { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x0025B2D0 File Offset: 0x002594D0
		// (set) Token: 0x06005BC0 RID: 23488 RVA: 0x0025B2D8 File Offset: 0x002594D8
		public bool CheckThreadAccess { get; set; }
	}
}
