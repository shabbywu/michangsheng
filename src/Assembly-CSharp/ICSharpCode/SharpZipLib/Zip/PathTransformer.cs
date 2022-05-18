using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007F0 RID: 2032
	public class PathTransformer : INameTransform
	{
		// Token: 0x06003441 RID: 13377 RVA: 0x000261DA File Offset: 0x000243DA
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
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

		// Token: 0x06003442 RID: 13378 RVA: 0x00193578 File Offset: 0x00191778
		public string TransformFile(string name)
		{
			if (name != null)
			{
				name = name.Replace("\\", "/");
				name = PathUtils.DropPathRoot(name);
				name = name.Trim(new char[]
				{
					'/'
				});
				for (int i = name.IndexOf("//", StringComparison.Ordinal); i >= 0; i = name.IndexOf("//", StringComparison.Ordinal))
				{
					name = name.Remove(i, 1);
				}
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}
	}
}
