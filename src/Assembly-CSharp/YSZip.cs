using System;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;

// Token: 0x020001D8 RID: 472
public static class YSZip
{
	// Token: 0x0600141A RID: 5146 RVA: 0x00082328 File Offset: 0x00080528
	public static bool ZipFile(string srcFiles, string strZip)
	{
		ZipOutputStream zipOutputStream = null;
		bool result;
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
			char c = srcFiles[length - 1];
			if (srcFiles[srcFiles.Length - 1] != Path.DirectorySeparatorChar)
			{
				srcFiles += Path.DirectorySeparatorChar.ToString();
			}
			zipOutputStream = new ZipOutputStream(File.Create(strZip));
			zipOutputStream.SetLevel(6);
			YSZip.zip(srcFiles, zipOutputStream, srcFiles);
			zipOutputStream.Finish();
			zipOutputStream.Close();
			stopwatch.Stop();
			Console.WriteLine(string.Format("压缩{0}到{1}完毕，耗时{2}ms", srcFiles, strZip, stopwatch.ElapsedMilliseconds));
			result = true;
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
		return result;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0008241C File Offset: 0x0008061C
	private static void zip(string srcFiles, ZipOutputStream outstream, string staticFile)
	{
		if (srcFiles[srcFiles.Length - 1] != Path.DirectorySeparatorChar)
		{
			srcFiles += Path.DirectorySeparatorChar.ToString();
		}
		Crc32 crc = new Crc32();
		foreach (string text in Directory.GetFileSystemEntries(srcFiles))
		{
			if (Directory.Exists(text))
			{
				YSZip.zip(text, outstream, staticFile);
			}
			else
			{
				FileStream fileStream = File.OpenRead(text);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				ZipEntry zipEntry = new ZipEntry(text.Substring(staticFile.LastIndexOf("\\") + 1));
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
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x00082524 File Offset: 0x00080724
	public static bool UnZipFile(string zipFilePath, string targetFolderPath)
	{
		if (!File.Exists(zipFilePath))
		{
			Console.WriteLine("找不到压缩包 '" + zipFilePath + "'");
			return false;
		}
		try
		{
			using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath)))
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ZipEntry nextEntry;
				while ((nextEntry = zipInputStream.GetNextEntry()) != null)
				{
					string directoryName = Path.GetDirectoryName(nextEntry.Name);
					string fileName = Path.GetFileName(nextEntry.Name);
					if (directoryName.Length > 0)
					{
						Directory.CreateDirectory(targetFolderPath + "/" + directoryName);
					}
					if (fileName != string.Empty)
					{
						using (FileStream fileStream = File.Create(targetFolderPath + "/" + nextEntry.Name))
						{
							byte[] array = new byte[2048];
							for (;;)
							{
								int num = zipInputStream.Read(array, 0, array.Length);
								if (num <= 0)
								{
									break;
								}
								fileStream.Write(array, 0, num);
							}
						}
					}
				}
				stopwatch.Stop();
				Console.WriteLine(string.Format("解压{0}到{1}完毕，耗时{2}ms", zipFilePath, targetFolderPath, stopwatch.ElapsedMilliseconds));
				return true;
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return false;
	}
}
