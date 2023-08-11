using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace script.Steam.Utils;

public class FileUtils
{
	public static void OpenFile(UnityAction<string> action)
	{
		OpenDialogFile openDialogFile = new OpenDialogFile();
		openDialogFile.structSize = Marshal.SizeOf(openDialogFile);
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
			if (8388608 < length)
			{
				UIPopTip.Inst.Pop("图片尺寸最大为8M");
				return;
			}
			action?.Invoke(openDialogFile.file);
			Debug.Log((object)("filePath" + openDialogFile.file));
		}
	}

	public static void OpenDirectory(UnityAction<string> action)
	{
		FolderPicker folderPicker = new FolderPicker();
		folderPicker.Title = "Mod选择文件夹";
		bool? flag = folderPicker.ShowDialog(IntPtr.Zero);
		if (flag.HasValue && flag.Value)
		{
			string resultPath = folderPicker.ResultPath;
			action?.Invoke(resultPath);
			Debug.Log((object)("路径" + resultPath));
		}
	}
}
