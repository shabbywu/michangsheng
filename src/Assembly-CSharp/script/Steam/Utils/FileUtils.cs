using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace script.Steam.Utils
{
	// Token: 0x020009DA RID: 2522
	public class FileUtils
	{
		// Token: 0x060045FC RID: 17916 RVA: 0x001D9FD0 File Offset: 0x001D81D0
		public static void OpenFile(UnityAction<string> action)
		{
			OpenDialogFile openDialogFile = new OpenDialogFile();
			openDialogFile.structSize = Marshal.SizeOf<OpenDialogFile>(openDialogFile);
			openDialogFile.filter = "图片文件(*.PNG; *.JPG)\0*.PNG; *.JPG\0";
			openDialogFile.file = new string(new char[256]);
			openDialogFile.maxFile = openDialogFile.file.Length;
			openDialogFile.fileTitle = new string(new char[64]);
			openDialogFile.maxFileTitle = openDialogFile.fileTitle.Length;
			openDialogFile.initialDir = Application.streamingAssetsPath.Replace('/', '\\');
			openDialogFile.title = "选择封面图片";
			openDialogFile.flags = 530440;
			if (DllOpenFileDialog.GetSaveFileName(openDialogFile))
			{
				long length = new FileInfo(openDialogFile.file).Length;
				if (8388608L < length)
				{
					UIPopTip.Inst.Pop("图片尺寸最大为8M", PopTipIconType.叹号);
					return;
				}
				if (action != null)
				{
					action.Invoke(openDialogFile.file);
				}
				Debug.Log("filePath" + openDialogFile.file);
			}
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x001DA0C4 File Offset: 0x001D82C4
		public static void OpenDirectory(UnityAction<string> action)
		{
			FolderPicker folderPicker = new FolderPicker();
			folderPicker.Title = "Mod选择文件夹";
			bool? flag = folderPicker.ShowDialog(IntPtr.Zero, false);
			if (flag == null || !flag.Value)
			{
				return;
			}
			string resultPath = folderPicker.ResultPath;
			if (action != null)
			{
				action.Invoke(resultPath);
			}
			Debug.Log("路径" + resultPath);
		}
	}
}
