using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CA7 RID: 3239
	public class TailCallData
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06005AB8 RID: 23224 RVA: 0x00258EEB File Offset: 0x002570EB
		// (set) Token: 0x06005AB9 RID: 23225 RVA: 0x00258EF3 File Offset: 0x002570F3
		public DynValue Function { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06005ABA RID: 23226 RVA: 0x00258EFC File Offset: 0x002570FC
		// (set) Token: 0x06005ABB RID: 23227 RVA: 0x00258F04 File Offset: 0x00257104
		public DynValue[] Args { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06005ABC RID: 23228 RVA: 0x00258F0D File Offset: 0x0025710D
		// (set) Token: 0x06005ABD RID: 23229 RVA: 0x00258F15 File Offset: 0x00257115
		public CallbackFunction Continuation { get; set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06005ABE RID: 23230 RVA: 0x00258F1E File Offset: 0x0025711E
		// (set) Token: 0x06005ABF RID: 23231 RVA: 0x00258F26 File Offset: 0x00257126
		public CallbackFunction ErrorHandler { get; set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x00258F2F File Offset: 0x0025712F
		// (set) Token: 0x06005AC1 RID: 23233 RVA: 0x00258F37 File Offset: 0x00257137
		public DynValue ErrorHandlerBeforeUnwind { get; set; }
	}
}
