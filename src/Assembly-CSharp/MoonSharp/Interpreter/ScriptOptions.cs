using System;
using System.IO;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200109B RID: 4251
	public class ScriptOptions
	{
		// Token: 0x060066B4 RID: 26292 RVA: 0x0000403D File Offset: 0x0000223D
		internal ScriptOptions()
		{
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x00284CE8 File Offset: 0x00282EE8
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

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x00046CAB File Offset: 0x00044EAB
		// (set) Token: 0x060066B7 RID: 26295 RVA: 0x00046CB3 File Offset: 0x00044EB3
		public IScriptLoader ScriptLoader { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060066B8 RID: 26296 RVA: 0x00046CBC File Offset: 0x00044EBC
		// (set) Token: 0x060066B9 RID: 26297 RVA: 0x00046CC4 File Offset: 0x00044EC4
		public Action<string> DebugPrint { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x00046CCD File Offset: 0x00044ECD
		// (set) Token: 0x060066BB RID: 26299 RVA: 0x00046CD5 File Offset: 0x00044ED5
		public Func<string, string> DebugInput { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x00046CDE File Offset: 0x00044EDE
		// (set) Token: 0x060066BD RID: 26301 RVA: 0x00046CE6 File Offset: 0x00044EE6
		public bool UseLuaErrorLocations { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060066BE RID: 26302 RVA: 0x00046CEF File Offset: 0x00044EEF
		// (set) Token: 0x060066BF RID: 26303 RVA: 0x00046CF7 File Offset: 0x00044EF7
		public ColonOperatorBehaviour ColonOperatorClrCallbackBehaviour { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060066C0 RID: 26304 RVA: 0x00046D00 File Offset: 0x00044F00
		// (set) Token: 0x060066C1 RID: 26305 RVA: 0x00046D08 File Offset: 0x00044F08
		public Stream Stdin { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060066C2 RID: 26306 RVA: 0x00046D11 File Offset: 0x00044F11
		// (set) Token: 0x060066C3 RID: 26307 RVA: 0x00046D19 File Offset: 0x00044F19
		public Stream Stdout { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060066C4 RID: 26308 RVA: 0x00046D22 File Offset: 0x00044F22
		// (set) Token: 0x060066C5 RID: 26309 RVA: 0x00046D2A File Offset: 0x00044F2A
		public Stream Stderr { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060066C6 RID: 26310 RVA: 0x00046D33 File Offset: 0x00044F33
		// (set) Token: 0x060066C7 RID: 26311 RVA: 0x00046D3B File Offset: 0x00044F3B
		public int TailCallOptimizationThreshold { get; set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x00046D44 File Offset: 0x00044F44
		// (set) Token: 0x060066C9 RID: 26313 RVA: 0x00046D4C File Offset: 0x00044F4C
		public bool CheckThreadAccess { get; set; }
	}
}
