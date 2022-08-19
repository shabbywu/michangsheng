using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200054D RID: 1357
	public class PathTransformer : INameTransform
	{
		// Token: 0x06002BE6 RID: 11238 RVA: 0x0014771B File Offset: 0x0014591B
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

		// Token: 0x06002BE7 RID: 11239 RVA: 0x00147758 File Offset: 0x00145958
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
