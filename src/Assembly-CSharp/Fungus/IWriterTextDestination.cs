using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EB6 RID: 3766
	public interface IWriterTextDestination
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06006A64 RID: 27236
		// (set) Token: 0x06006A65 RID: 27237
		string Text { get; set; }

		// Token: 0x06006A66 RID: 27238
		void ForceRichText();

		// Token: 0x06006A67 RID: 27239
		void SetTextColor(Color textColor);

		// Token: 0x06006A68 RID: 27240
		void SetTextAlpha(float textAlpha);

		// Token: 0x06006A69 RID: 27241
		bool HasTextObject();

		// Token: 0x06006A6A RID: 27242
		bool SupportsRichText();
	}
}
