using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

// Token: 0x020003C4 RID: 964
public class FolderPicker
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000DD2C9 File Offset: 0x000DB4C9
	// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000DD2D1 File Offset: 0x000DB4D1
	public virtual string ResultPath { get; protected set; }

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000DD2DA File Offset: 0x000DB4DA
	// (set) Token: 0x06001F64 RID: 8036 RVA: 0x000DD2E2 File Offset: 0x000DB4E2
	public virtual string ResultName { get; protected set; }

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000DD2EB File Offset: 0x000DB4EB
	// (set) Token: 0x06001F66 RID: 8038 RVA: 0x000DD2F3 File Offset: 0x000DB4F3
	public virtual string InputPath { get; set; }

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000DD2FC File Offset: 0x000DB4FC
	// (set) Token: 0x06001F68 RID: 8040 RVA: 0x000DD304 File Offset: 0x000DB504
	public virtual bool ForceFileSystem { get; set; }

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06001F69 RID: 8041 RVA: 0x000DD30D File Offset: 0x000DB50D
	// (set) Token: 0x06001F6A RID: 8042 RVA: 0x000DD315 File Offset: 0x000DB515
	public virtual string Title { get; set; }

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000DD31E File Offset: 0x000DB51E
	// (set) Token: 0x06001F6C RID: 8044 RVA: 0x000DD326 File Offset: 0x000DB526
	public virtual string OkButtonLabel { get; set; }

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000DD32F File Offset: 0x000DB52F
	// (set) Token: 0x06001F6E RID: 8046 RVA: 0x000DD337 File Offset: 0x000DB537
	public virtual string FileNameLabel { get; set; }

	// Token: 0x06001F6F RID: 8047 RVA: 0x000DD340 File Offset: 0x000DB540
	protected virtual int SetOptions(int options)
	{
		if (this.ForceFileSystem)
		{
			options |= 64;
		}
		return options;
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x000DD354 File Offset: 0x000DB554
	public virtual bool? ShowDialog(IntPtr owner, bool throwOnError = false)
	{
		FolderPicker.IFileOpenDialog fileOpenDialog = (FolderPicker.IFileOpenDialog)new FolderPicker.FileOpenDialog();
		if (!string.IsNullOrEmpty(this.InputPath))
		{
			FolderPicker.IShellItem folder;
			if (FolderPicker.CheckHr(FolderPicker.SHCreateItemFromParsingName(this.InputPath, null, typeof(FolderPicker.IShellItem).GUID, out folder), throwOnError) != 0)
			{
				return null;
			}
			fileOpenDialog.SetFolder(folder);
		}
		FolderPicker.FOS options = FolderPicker.FOS.FOS_PICKFOLDERS;
		options = (FolderPicker.FOS)this.SetOptions((int)options);
		fileOpenDialog.SetOptions(options);
		if (this.Title != null)
		{
			fileOpenDialog.SetTitle(this.Title);
		}
		if (this.OkButtonLabel != null)
		{
			fileOpenDialog.SetOkButtonLabel(this.OkButtonLabel);
		}
		if (this.FileNameLabel != null)
		{
			fileOpenDialog.SetFileName(this.FileNameLabel);
		}
		if (owner == IntPtr.Zero)
		{
			owner = Process.GetCurrentProcess().MainWindowHandle;
			if (owner == IntPtr.Zero)
			{
				owner = FolderPicker.GetDesktopWindow();
			}
		}
		int num = fileOpenDialog.Show(owner);
		if (num == -2147023673)
		{
			return null;
		}
		if (FolderPicker.CheckHr(num, throwOnError) != 0)
		{
			return null;
		}
		FolderPicker.IShellItem shellItem;
		if (FolderPicker.CheckHr(fileOpenDialog.GetResult(out shellItem), throwOnError) != 0)
		{
			return null;
		}
		string text;
		if (FolderPicker.CheckHr(shellItem.GetDisplayName((FolderPicker.SIGDN)2147647488U, out text), throwOnError) != 0)
		{
			return null;
		}
		this.ResultPath = text;
		if (FolderPicker.CheckHr(shellItem.GetDisplayName((FolderPicker.SIGDN)2147794944U, out text), false) == 0)
		{
			this.ResultName = text;
		}
		return new bool?(true);
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x000DD4C6 File Offset: 0x000DB6C6
	private static int CheckHr(int hr, bool throwOnError)
	{
		if (hr != 0 && throwOnError)
		{
			Marshal.ThrowExceptionForHR(hr);
		}
		return hr;
	}

	// Token: 0x06001F72 RID: 8050
	[DllImport("shell32")]
	private static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IBindCtx pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out FolderPicker.IShellItem ppv);

	// Token: 0x06001F73 RID: 8051
	[DllImport("user32")]
	private static extern IntPtr GetDesktopWindow();

	// Token: 0x04001974 RID: 6516
	private const int ERROR_CANCELLED = -2147023673;

	// Token: 0x02001371 RID: 4977
	[Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
	[ComImport]
	private class FileOpenDialog
	{
		// Token: 0x06007BF5 RID: 31733
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern FileOpenDialog();
	}

	// Token: 0x02001372 RID: 4978
	[Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	private interface IFileOpenDialog
	{
		// Token: 0x06007BF6 RID: 31734
		[PreserveSig]
		int Show(IntPtr parent);

		// Token: 0x06007BF7 RID: 31735
		[PreserveSig]
		int SetFileTypes();

		// Token: 0x06007BF8 RID: 31736
		[PreserveSig]
		int SetFileTypeIndex(int iFileType);

		// Token: 0x06007BF9 RID: 31737
		[PreserveSig]
		int GetFileTypeIndex(out int piFileType);

		// Token: 0x06007BFA RID: 31738
		[PreserveSig]
		int Advise();

		// Token: 0x06007BFB RID: 31739
		[PreserveSig]
		int Unadvise();

		// Token: 0x06007BFC RID: 31740
		[PreserveSig]
		int SetOptions(FolderPicker.FOS fos);

		// Token: 0x06007BFD RID: 31741
		[PreserveSig]
		int GetOptions(out FolderPicker.FOS pfos);

		// Token: 0x06007BFE RID: 31742
		[PreserveSig]
		int SetDefaultFolder(FolderPicker.IShellItem psi);

		// Token: 0x06007BFF RID: 31743
		[PreserveSig]
		int SetFolder(FolderPicker.IShellItem psi);

		// Token: 0x06007C00 RID: 31744
		[PreserveSig]
		int GetFolder(out FolderPicker.IShellItem ppsi);

		// Token: 0x06007C01 RID: 31745
		[PreserveSig]
		int GetCurrentSelection(out FolderPicker.IShellItem ppsi);

		// Token: 0x06007C02 RID: 31746
		[PreserveSig]
		int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

		// Token: 0x06007C03 RID: 31747
		[PreserveSig]
		int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		// Token: 0x06007C04 RID: 31748
		[PreserveSig]
		int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

		// Token: 0x06007C05 RID: 31749
		[PreserveSig]
		int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

		// Token: 0x06007C06 RID: 31750
		[PreserveSig]
		int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

		// Token: 0x06007C07 RID: 31751
		[PreserveSig]
		int GetResult(out FolderPicker.IShellItem ppsi);

		// Token: 0x06007C08 RID: 31752
		[PreserveSig]
		int AddPlace(FolderPicker.IShellItem psi, int alignment);

		// Token: 0x06007C09 RID: 31753
		[PreserveSig]
		int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

		// Token: 0x06007C0A RID: 31754
		[PreserveSig]
		int Close(int hr);

		// Token: 0x06007C0B RID: 31755
		[PreserveSig]
		int SetClientGuid();

		// Token: 0x06007C0C RID: 31756
		[PreserveSig]
		int ClearClientData();

		// Token: 0x06007C0D RID: 31757
		[PreserveSig]
		int SetFilter([MarshalAs(UnmanagedType.IUnknown)] object pFilter);

		// Token: 0x06007C0E RID: 31758
		[PreserveSig]
		int GetResults([MarshalAs(UnmanagedType.IUnknown)] out object ppenum);

		// Token: 0x06007C0F RID: 31759
		[PreserveSig]
		int GetSelectedItems([MarshalAs(UnmanagedType.IUnknown)] out object ppsai);
	}

	// Token: 0x02001373 RID: 4979
	[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	private interface IShellItem
	{
		// Token: 0x06007C10 RID: 31760
		[PreserveSig]
		int BindToHandler();

		// Token: 0x06007C11 RID: 31761
		[PreserveSig]
		int GetParent();

		// Token: 0x06007C12 RID: 31762
		[PreserveSig]
		int GetDisplayName(FolderPicker.SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

		// Token: 0x06007C13 RID: 31763
		[PreserveSig]
		int GetAttributes();

		// Token: 0x06007C14 RID: 31764
		[PreserveSig]
		int Compare();
	}

	// Token: 0x02001374 RID: 4980
	private enum SIGDN : uint
	{
		// Token: 0x0400686C RID: 26732
		SIGDN_DESKTOPABSOLUTEEDITING = 2147794944U,
		// Token: 0x0400686D RID: 26733
		SIGDN_DESKTOPABSOLUTEPARSING = 2147647488U,
		// Token: 0x0400686E RID: 26734
		SIGDN_FILESYSPATH = 2147844096U,
		// Token: 0x0400686F RID: 26735
		SIGDN_NORMALDISPLAY = 0U,
		// Token: 0x04006870 RID: 26736
		SIGDN_PARENTRELATIVE = 2148007937U,
		// Token: 0x04006871 RID: 26737
		SIGDN_PARENTRELATIVEEDITING = 2147684353U,
		// Token: 0x04006872 RID: 26738
		SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553U,
		// Token: 0x04006873 RID: 26739
		SIGDN_PARENTRELATIVEPARSING = 2147581953U,
		// Token: 0x04006874 RID: 26740
		SIGDN_URL = 2147909632U
	}

	// Token: 0x02001375 RID: 4981
	[Flags]
	private enum FOS
	{
		// Token: 0x04006876 RID: 26742
		FOS_OVERWRITEPROMPT = 2,
		// Token: 0x04006877 RID: 26743
		FOS_STRICTFILETYPES = 4,
		// Token: 0x04006878 RID: 26744
		FOS_NOCHANGEDIR = 8,
		// Token: 0x04006879 RID: 26745
		FOS_PICKFOLDERS = 32,
		// Token: 0x0400687A RID: 26746
		FOS_FORCEFILESYSTEM = 64,
		// Token: 0x0400687B RID: 26747
		FOS_ALLNONSTORAGEITEMS = 128,
		// Token: 0x0400687C RID: 26748
		FOS_NOVALIDATE = 256,
		// Token: 0x0400687D RID: 26749
		FOS_ALLOWMULTISELECT = 512,
		// Token: 0x0400687E RID: 26750
		FOS_PATHMUSTEXIST = 2048,
		// Token: 0x0400687F RID: 26751
		FOS_FILEMUSTEXIST = 4096,
		// Token: 0x04006880 RID: 26752
		FOS_CREATEPROMPT = 8192,
		// Token: 0x04006881 RID: 26753
		FOS_SHAREAWARE = 16384,
		// Token: 0x04006882 RID: 26754
		FOS_NOREADONLYRETURN = 32768,
		// Token: 0x04006883 RID: 26755
		FOS_NOTESTFILECREATE = 65536,
		// Token: 0x04006884 RID: 26756
		FOS_HIDEMRUPLACES = 131072,
		// Token: 0x04006885 RID: 26757
		FOS_HIDEPINNEDPLACES = 262144,
		// Token: 0x04006886 RID: 26758
		FOS_NODEREFERENCELINKS = 1048576,
		// Token: 0x04006887 RID: 26759
		FOS_OKBUTTONNEEDSINTERACTION = 2097152,
		// Token: 0x04006888 RID: 26760
		FOS_DONTADDTORECENT = 33554432,
		// Token: 0x04006889 RID: 26761
		FOS_FORCESHOWHIDDEN = 268435456,
		// Token: 0x0400688A RID: 26762
		FOS_DEFAULTNOMINIMODE = 536870912,
		// Token: 0x0400688B RID: 26763
		FOS_FORCEPREVIEWPANEON = 1073741824,
		// Token: 0x0400688C RID: 26764
		FOS_SUPPORTSTREAMABLEITEMS = -2147483648
	}
}
