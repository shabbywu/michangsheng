using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000832 RID: 2098
	public static class PathUtils
	{
		// Token: 0x060036F2 RID: 14066 RVA: 0x0019C8AC File Offset: 0x0019AAAC
		public static string DropPathRoot(string path)
		{
			string text = path;
			if (!string.IsNullOrEmpty(path))
			{
				if (path[0] == '\\' || path[0] == '/')
				{
					if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
					{
						int num = 2;
						int num2 = 2;
						while (num <= path.Length && ((path[num] != '\\' && path[num] != '/') || --num2 > 0))
						{
							num++;
						}
						num++;
						if (num < path.Length)
						{
							text = path.Substring(num);
						}
						else
						{
							text = "";
						}
					}
				}
				else if (path.Length > 1 && path[1] == ':')
				{
					int count = 2;
					if (path.Length > 2 && (path[2] == '\\' || path[2] == '/'))
					{
						count = 3;
					}
					text = text.Remove(0, count);
				}
			}
			return text;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x0019C994 File Offset: 0x0019AB94
		public static string GetTempFileName(string original)
		{
			string tempPath = Path.GetTempPath();
			string text;
			do
			{
				text = ((original == null) ? Path.Combine(tempPath, Path.GetRandomFileName()) : (original + "." + Path.GetRandomFileName()));
			}
			while (File.Exists(text));
			return text;
		}
	}
}
