using System;
using System.Runtime.InteropServices;

namespace script.Steam.Utils;

public class DllOpenFileDialog
{
	[DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
	public static extern bool GetOpenFileName([In][Out] OpenDialogFile ofn);

	[DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
	public static extern bool GetSaveFileName([In][Out] OpenDialogFile ofn);

	[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
	public static extern IntPtr SHBrowseForFolder([In][Out] OpenDialogDir ofn);

	[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
	public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In][Out] char[] fileName);
}
