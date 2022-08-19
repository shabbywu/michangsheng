using System;
using System.Runtime.InteropServices;

namespace script.Steam.Utils
{
	// Token: 0x020009DC RID: 2524
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class OpenDialogFile
	{
		// Token: 0x0400477D RID: 18301
		public int structSize;

		// Token: 0x0400477E RID: 18302
		public IntPtr dlgOwner = IntPtr.Zero;

		// Token: 0x0400477F RID: 18303
		public IntPtr instance = IntPtr.Zero;

		// Token: 0x04004780 RID: 18304
		public string filter;

		// Token: 0x04004781 RID: 18305
		public string customFilter;

		// Token: 0x04004782 RID: 18306
		public int maxCustFilter;

		// Token: 0x04004783 RID: 18307
		public int filterIndex;

		// Token: 0x04004784 RID: 18308
		public string file;

		// Token: 0x04004785 RID: 18309
		public int maxFile;

		// Token: 0x04004786 RID: 18310
		public string fileTitle;

		// Token: 0x04004787 RID: 18311
		public int maxFileTitle;

		// Token: 0x04004788 RID: 18312
		public string initialDir;

		// Token: 0x04004789 RID: 18313
		public string title;

		// Token: 0x0400478A RID: 18314
		public int flags;

		// Token: 0x0400478B RID: 18315
		public short fileOffset;

		// Token: 0x0400478C RID: 18316
		public short fileExtension;

		// Token: 0x0400478D RID: 18317
		public string defExt;

		// Token: 0x0400478E RID: 18318
		public IntPtr custData = IntPtr.Zero;

		// Token: 0x0400478F RID: 18319
		public IntPtr hook = IntPtr.Zero;

		// Token: 0x04004790 RID: 18320
		public string templateName;

		// Token: 0x04004791 RID: 18321
		public IntPtr reservedPtr = IntPtr.Zero;

		// Token: 0x04004792 RID: 18322
		public int reservedInt;

		// Token: 0x04004793 RID: 18323
		public int flagsEx;
	}
}
