using System;
using System.Runtime.InteropServices;

namespace script.Steam.Utils
{
	// Token: 0x020009D8 RID: 2520
	public class DllOpenFileDialog
	{
		// Token: 0x060045F4 RID: 17908
		[DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern bool GetOpenFileName([In] [Out] OpenDialogFile ofn);

		// Token: 0x060045F5 RID: 17909
		[DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern bool GetSaveFileName([In] [Out] OpenDialogFile ofn);

		// Token: 0x060045F6 RID: 17910
		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern IntPtr SHBrowseForFolder([In] [Out] OpenDialogDir ofn);

		// Token: 0x060045F7 RID: 17911
		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In] [Out] char[] fileName);
	}
}
