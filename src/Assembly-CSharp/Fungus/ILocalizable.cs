using System;

namespace Fungus
{
	// Token: 0x0200133C RID: 4924
	public interface ILocalizable
	{
		// Token: 0x06007795 RID: 30613
		string GetStandardText();

		// Token: 0x06007796 RID: 30614
		void SetStandardText(string standardText);

		// Token: 0x06007797 RID: 30615
		string GetDescription();

		// Token: 0x06007798 RID: 30616
		string GetStringId();
	}
}
