using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

public class FolderPicker
{
	[ComImport]
	[Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
	private class FileOpenDialog
	{
	}

	[ComImport]
	[Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	private interface IFileOpenDialog
	{
		[PreserveSig]
		int Show(IntPtr parent);

		[PreserveSig]
		int SetFileTypes();

		[PreserveSig]
		int SetFileTypeIndex(int iFileType);

		[PreserveSig]
		int GetFileTypeIndex(out int piFileType);

		[PreserveSig]
		int Advise();

		[PreserveSig]
		int Unadvise();

		[PreserveSig]
		int SetOptions(FOS fos);

		[PreserveSig]
		int GetOptions(out FOS pfos);

		[PreserveSig]
		int SetDefaultFolder(IShellItem psi);

		[PreserveSig]
		int SetFolder(IShellItem psi);

		[PreserveSig]
		int GetFolder(out IShellItem ppsi);

		[PreserveSig]
		int GetCurrentSelection(out IShellItem ppsi);

		[PreserveSig]
		int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

		[PreserveSig]
		int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		[PreserveSig]
		int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

		[PreserveSig]
		int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

		[PreserveSig]
		int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

		[PreserveSig]
		int GetResult(out IShellItem ppsi);

		[PreserveSig]
		int AddPlace(IShellItem psi, int alignment);

		[PreserveSig]
		int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

		[PreserveSig]
		int Close(int hr);

		[PreserveSig]
		int SetClientGuid();

		[PreserveSig]
		int ClearClientData();

		[PreserveSig]
		int SetFilter([MarshalAs(UnmanagedType.IUnknown)] object pFilter);

		[PreserveSig]
		int GetResults([MarshalAs(UnmanagedType.IUnknown)] out object ppenum);

		[PreserveSig]
		int GetSelectedItems([MarshalAs(UnmanagedType.IUnknown)] out object ppsai);
	}

	[ComImport]
	[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	private interface IShellItem
	{
		[PreserveSig]
		int BindToHandler();

		[PreserveSig]
		int GetParent();

		[PreserveSig]
		int GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

		[PreserveSig]
		int GetAttributes();

		[PreserveSig]
		int Compare();
	}

	private enum SIGDN : uint
	{
		SIGDN_DESKTOPABSOLUTEEDITING = 2147794944u,
		SIGDN_DESKTOPABSOLUTEPARSING = 2147647488u,
		SIGDN_FILESYSPATH = 2147844096u,
		SIGDN_NORMALDISPLAY = 0u,
		SIGDN_PARENTRELATIVE = 2148007937u,
		SIGDN_PARENTRELATIVEEDITING = 2147684353u,
		SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553u,
		SIGDN_PARENTRELATIVEPARSING = 2147581953u,
		SIGDN_URL = 2147909632u
	}

	[Flags]
	private enum FOS
	{
		FOS_OVERWRITEPROMPT = 2,
		FOS_STRICTFILETYPES = 4,
		FOS_NOCHANGEDIR = 8,
		FOS_PICKFOLDERS = 0x20,
		FOS_FORCEFILESYSTEM = 0x40,
		FOS_ALLNONSTORAGEITEMS = 0x80,
		FOS_NOVALIDATE = 0x100,
		FOS_ALLOWMULTISELECT = 0x200,
		FOS_PATHMUSTEXIST = 0x800,
		FOS_FILEMUSTEXIST = 0x1000,
		FOS_CREATEPROMPT = 0x2000,
		FOS_SHAREAWARE = 0x4000,
		FOS_NOREADONLYRETURN = 0x8000,
		FOS_NOTESTFILECREATE = 0x10000,
		FOS_HIDEMRUPLACES = 0x20000,
		FOS_HIDEPINNEDPLACES = 0x40000,
		FOS_NODEREFERENCELINKS = 0x100000,
		FOS_OKBUTTONNEEDSINTERACTION = 0x200000,
		FOS_DONTADDTORECENT = 0x2000000,
		FOS_FORCESHOWHIDDEN = 0x10000000,
		FOS_DEFAULTNOMINIMODE = 0x20000000,
		FOS_FORCEPREVIEWPANEON = 0x40000000,
		FOS_SUPPORTSTREAMABLEITEMS = int.MinValue
	}

	private const int ERROR_CANCELLED = -2147023673;

	public virtual string ResultPath { get; protected set; }

	public virtual string ResultName { get; protected set; }

	public virtual string InputPath { get; set; }

	public virtual bool ForceFileSystem { get; set; }

	public virtual string Title { get; set; }

	public virtual string OkButtonLabel { get; set; }

	public virtual string FileNameLabel { get; set; }

	protected virtual int SetOptions(int options)
	{
		if (ForceFileSystem)
		{
			options |= 0x40;
		}
		return options;
	}

	public virtual bool? ShowDialog(IntPtr owner, bool throwOnError = false)
	{
		IFileOpenDialog fileOpenDialog = (IFileOpenDialog)new FileOpenDialog();
		if (!string.IsNullOrEmpty(InputPath))
		{
			if (CheckHr(SHCreateItemFromParsingName(InputPath, null, typeof(IShellItem).GUID, out var ppv), throwOnError) != 0)
			{
				return null;
			}
			fileOpenDialog.SetFolder(ppv);
		}
		FOS options = FOS.FOS_PICKFOLDERS;
		options = (FOS)SetOptions((int)options);
		fileOpenDialog.SetOptions(options);
		if (Title != null)
		{
			fileOpenDialog.SetTitle(Title);
		}
		if (OkButtonLabel != null)
		{
			fileOpenDialog.SetOkButtonLabel(OkButtonLabel);
		}
		if (FileNameLabel != null)
		{
			fileOpenDialog.SetFileName(FileNameLabel);
		}
		if (owner == IntPtr.Zero)
		{
			owner = Process.GetCurrentProcess().MainWindowHandle;
			if (owner == IntPtr.Zero)
			{
				owner = GetDesktopWindow();
			}
		}
		int num = fileOpenDialog.Show(owner);
		if (num == -2147023673)
		{
			return null;
		}
		if (CheckHr(num, throwOnError) != 0)
		{
			return null;
		}
		if (CheckHr(fileOpenDialog.GetResult(out var ppsi), throwOnError) != 0)
		{
			return null;
		}
		if (CheckHr(ppsi.GetDisplayName(SIGDN.SIGDN_DESKTOPABSOLUTEPARSING, out var ppszName), throwOnError) != 0)
		{
			return null;
		}
		ResultPath = ppszName;
		if (CheckHr(ppsi.GetDisplayName(SIGDN.SIGDN_DESKTOPABSOLUTEEDITING, out ppszName), throwOnError: false) == 0)
		{
			ResultName = ppszName;
		}
		return true;
	}

	private static int CheckHr(int hr, bool throwOnError)
	{
		if (hr != 0 && throwOnError)
		{
			Marshal.ThrowExceptionForHR(hr);
		}
		return hr;
	}

	[DllImport("shell32")]
	private static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IBindCtx pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IShellItem ppv);

	[DllImport("user32")]
	private static extern IntPtr GetDesktopWindow();
}
