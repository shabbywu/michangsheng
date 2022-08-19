using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000589 RID: 1417
	public static class PathUtils
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x00151C1C File Offset: 0x0014FE1C
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

		// Token: 0x06002E7D RID: 11901 RVA: 0x00151D04 File Offset: 0x0014FF04
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
