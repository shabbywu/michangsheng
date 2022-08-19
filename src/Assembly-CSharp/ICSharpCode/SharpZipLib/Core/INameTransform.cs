using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000582 RID: 1410
	public interface INameTransform
	{
		// Token: 0x06002E58 RID: 11864
		string TransformFile(string name);

		// Token: 0x06002E59 RID: 11865
		string TransformDirectory(string name);
	}
}
