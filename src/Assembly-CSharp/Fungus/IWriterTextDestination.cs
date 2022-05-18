using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200133F RID: 4927
	public interface IWriterTextDestination
	{
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060077A1 RID: 30625
		// (set) Token: 0x060077A2 RID: 30626
		string Text { get; set; }

		// Token: 0x060077A3 RID: 30627
		void ForceRichText();

		// Token: 0x060077A4 RID: 30628
		void SetTextColor(Color textColor);

		// Token: 0x060077A5 RID: 30629
		void SetTextAlpha(float textAlpha);

		// Token: 0x060077A6 RID: 30630
		bool HasTextObject();

		// Token: 0x060077A7 RID: 30631
		bool SupportsRichText();
	}
}
