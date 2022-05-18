using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001075 RID: 4213
	public class TailCallData
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060065A7 RID: 26023 RVA: 0x00045FF7 File Offset: 0x000441F7
		// (set) Token: 0x060065A8 RID: 26024 RVA: 0x00045FFF File Offset: 0x000441FF
		public DynValue Function { get; set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x00046008 File Offset: 0x00044208
		// (set) Token: 0x060065AA RID: 26026 RVA: 0x00046010 File Offset: 0x00044210
		public DynValue[] Args { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x00046019 File Offset: 0x00044219
		// (set) Token: 0x060065AC RID: 26028 RVA: 0x00046021 File Offset: 0x00044221
		public CallbackFunction Continuation { get; set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060065AD RID: 26029 RVA: 0x0004602A File Offset: 0x0004422A
		// (set) Token: 0x060065AE RID: 26030 RVA: 0x00046032 File Offset: 0x00044232
		public CallbackFunction ErrorHandler { get; set; }

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060065AF RID: 26031 RVA: 0x0004603B File Offset: 0x0004423B
		// (set) Token: 0x060065B0 RID: 26032 RVA: 0x00046043 File Offset: 0x00044243
		public DynValue ErrorHandlerBeforeUnwind { get; set; }
	}
}
