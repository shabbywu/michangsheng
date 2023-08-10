using System.IO;

namespace ICSharpCode.SharpZipLib.Core;

public static class PathUtils
{
	public static string DropPathRoot(string path)
	{
		string text = path;
		if (!string.IsNullOrEmpty(path))
		{
			if (path[0] == '\\' || path[0] == '/')
			{
				if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
				{
					int i = 2;
					int num = 2;
					for (; i <= path.Length; i++)
					{
						if ((path[i] == '\\' || path[i] == '/') && --num <= 0)
						{
							break;
						}
					}
					i++;
					text = ((i >= path.Length) ? "" : path.Substring(i));
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
