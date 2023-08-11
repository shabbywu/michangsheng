using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip;

public class PathTransformer : INameTransform
{
	public string TransformDirectory(string name)
	{
		name = TransformFile(name);
		if (name.Length > 0)
		{
			if (!name.EndsWith("/", StringComparison.Ordinal))
			{
				name += "/";
			}
			return name;
		}
		throw new ZipException("Cannot have an empty directory name");
	}

	public string TransformFile(string name)
	{
		if (name != null)
		{
			name = name.Replace("\\", "/");
			name = PathUtils.DropPathRoot(name);
			name = name.Trim(new char[1] { '/' });
			for (int num = name.IndexOf("//", StringComparison.Ordinal); num >= 0; num = name.IndexOf("//", StringComparison.Ordinal))
			{
				name = name.Remove(num, 1);
			}
		}
		else
		{
			name = string.Empty;
		}
		return name;
	}
}
