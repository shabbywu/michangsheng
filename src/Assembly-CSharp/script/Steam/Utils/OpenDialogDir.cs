using System;
using System.Runtime.InteropServices;

namespace script.Steam.Utils
{
	// Token: 0x020009DB RID: 2523
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class OpenDialogDir
	{
		// Token: 0x04004775 RID: 18293
		public IntPtr hwndOwner = IntPtr.Zero;

		// Token: 0x04004776 RID: 18294
		public IntPtr pidlRoot = IntPtr.Zero;

		// Token: 0x04004777 RID: 18295
		public string pszDisplayName = "123";

		// Token: 0x04004778 RID: 18296
		public string lpszTitle;

		// Token: 0x04004779 RID: 18297
		public uint ulFlags;

		// Token: 0x0400477A RID: 18298
		public IntPtr lpfn = IntPtr.Zero;

		// Token: 0x0400477B RID: 18299
		public IntPtr lParam = IntPtr.Zero;

		// Token: 0x0400477C RID: 18300
		public int iImage;
	}
}
