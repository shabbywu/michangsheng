using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200082B RID: 2091
	public interface INameTransform
	{
		// Token: 0x060036CE RID: 14030
		string TransformFile(string name);

		// Token: 0x060036CF RID: 14031
		string TransformDirectory(string name);
	}
}
