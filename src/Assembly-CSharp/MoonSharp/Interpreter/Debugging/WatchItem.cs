using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D6A RID: 3434
	public class WatchItem
	{
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06006125 RID: 24869 RVA: 0x002730BB File Offset: 0x002712BB
		// (set) Token: 0x06006126 RID: 24870 RVA: 0x002730C3 File Offset: 0x002712C3
		public int Address { get; set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x002730CC File Offset: 0x002712CC
		// (set) Token: 0x06006128 RID: 24872 RVA: 0x002730D4 File Offset: 0x002712D4
		public int BasePtr { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x002730DD File Offset: 0x002712DD
		// (set) Token: 0x0600612A RID: 24874 RVA: 0x002730E5 File Offset: 0x002712E5
		public int RetAddress { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x002730EE File Offset: 0x002712EE
		// (set) Token: 0x0600612C RID: 24876 RVA: 0x002730F6 File Offset: 0x002712F6
		public string Name { get; set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x002730FF File Offset: 0x002712FF
		// (set) Token: 0x0600612E RID: 24878 RVA: 0x00273107 File Offset: 0x00271307
		public DynValue Value { get; set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x00273110 File Offset: 0x00271310
		// (set) Token: 0x06006130 RID: 24880 RVA: 0x00273118 File Offset: 0x00271318
		public SymbolRef LValue { get; set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x00273121 File Offset: 0x00271321
		// (set) Token: 0x06006132 RID: 24882 RVA: 0x00273129 File Offset: 0x00271329
		public bool IsError { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x00273132 File Offset: 0x00271332
		// (set) Token: 0x06006134 RID: 24884 RVA: 0x0027313A File Offset: 0x0027133A
		public SourceRef Location { get; set; }

		// Token: 0x06006135 RID: 24885 RVA: 0x00273144 File Offset: 0x00271344
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
			{
				this.Address,
				this.BasePtr,
				this.RetAddress,
				this.Name ?? "(null)",
				(this.Value != null) ? this.Value.ToString() : "(null)",
				(this.LValue != null) ? this.LValue.ToString() : "(null)"
			});
		}
	}
}
