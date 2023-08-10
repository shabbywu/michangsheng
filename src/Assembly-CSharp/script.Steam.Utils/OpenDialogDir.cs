using System;
using System.Runtime.InteropServices;

namespace script.Steam.Utils;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenDialogDir
{
	public IntPtr hwndOwner = IntPtr.Zero;

	public IntPtr pidlRoot = IntPtr.Zero;

	public string pszDisplayName = "123";

	public string lpszTitle;

	public uint ulFlags;

	public IntPtr lpfn = IntPtr.Zero;

	public IntPtr lParam = IntPtr.Zero;

	public int iImage;
}
