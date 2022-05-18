using System;

namespace Fungus
{
	// Token: 0x0200133D RID: 4925
	internal interface IUpdateable
	{
		// Token: 0x06007799 RID: 30617
		void UpdateToVersion(int oldVersion, int newVersion);
	}
}
