using System;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;

public static class YSZip
{
	public static bool ZipFile(string srcFiles, string strZip)
	{
		ZipOutputStream zipOutputStream = null;
		try
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			FileInfo fileInfo = new FileInfo(strZip);
			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
			}
			int length = srcFiles.Length;
			_ = srcFiles[length - 1];
			if (srcFiles[srcFiles.Length - 1] != Path.DirectorySeparatorChar)
			{
				string text = srcFiles;
				char directorySeparatorChar = Path.DirectorySeparatorChar;
				srcFiles = text + directorySeparatorChar;
			}
			zipOutputStream = new ZipOutputStream(File.Create(strZip));
			zipOutputStream.SetLevel(6);
			zip(srcFiles, zipOutputStream, srcFiles);
			zipOutputStream.Finish();
			zipOutputStream.Close();
			stopwatch.Stop();
			Console.WriteLine($"压缩{srcFiles}到{strZip}完毕，耗时{stopwatch.ElapsedMilliseconds}ms");
			return true;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			if (zipOutputStream != null)
			{
				zipOutputStream.Finish();
				zipOutputStream.Close();
			}
		}
	}

	private static void zip(string srcFiles, ZipOutputStream outstream, string staticFile)
	{
		if (srcFiles[srcFiles.Length - 1] != Path.DirectorySeparatorChar)
		{
			string text = srcFiles;
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			srcFiles = text + directorySeparatorChar;
		}
		Crc32 crc = new Crc32();
		string[] fileSystemEntries = Directory.GetFileSystemEntries(srcFiles);
		foreach (string text2 in fileSystemEntries)
		{
			if (Directory.Exists(text2))
			{
				zip(text2, outstream, staticFile);
				continue;
			}
			FileStream fileStream = File.OpenRead(text2);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			ZipEntry zipEntry = new ZipEntry(text2.Substring(staticFile.LastIndexOf("\\") + 1));
			zipEntry.DateTime = DateTime.Now;
			zipEntry.Size = fileStream.Length;
			fileStream.Close();
			crc.Reset();
			crc.Update(array);
			zipEntry.Crc = crc.Value;
			outstream.PutNextEntry(zipEntry);
			outstream.Write(array, 0, array.Length);
		}
	}

	public static bool UnZipFile(string zipFilePath, string targetFolderPath)
	{
		if (!File.Exists(zipFilePath))
		{
			Console.WriteLine("找不到压缩包 '" + zipFilePath + "'");
			return false;
		}
		try
		{
			using ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath));
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				string directoryName = Path.GetDirectoryName(nextEntry.Name);
				string? fileName = Path.GetFileName(nextEntry.Name);
				if (directoryName.Length > 0)
				{
					Directory.CreateDirectory(targetFolderPath + "/" + directoryName);
				}
				if (!(fileName != string.Empty))
				{
					continue;
				}
				using FileStream fileStream = File.Create(targetFolderPath + "/" + nextEntry.Name);
				int num = 2048;
				byte[] array = new byte[2048];
				while (true)
				{
					num = zipInputStream.Read(array, 0, array.Length);
					if (num > 0)
					{
						fileStream.Write(array, 0, num);
						continue;
					}
					break;
				}
			}
			stopwatch.Stop();
			Console.WriteLine($"解压{zipFilePath}到{targetFolderPath}完毕，耗时{stopwatch.ElapsedMilliseconds}ms");
			return true;
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return false;
	}
}
