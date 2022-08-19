using System;

namespace Fungus
{
	// Token: 0x02000EB3 RID: 3763
	public interface ILocalizable
	{
		// Token: 0x06006A58 RID: 27224
		string GetStandardText();

		// Token: 0x06006A59 RID: 27225
		void SetStandardText(string standardText);

		// Token: 0x06006A5A RID: 27226
		string GetDescription();

		// Token: 0x06006A5B RID: 27227
		string GetStringId();
	}
}
