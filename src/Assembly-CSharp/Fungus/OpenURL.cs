using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E0A RID: 3594
	[CommandInfo("Scripting", "Open URL", "Opens the specified URL in the browser.", 0)]
	public class OpenURL : Command
	{
		// Token: 0x0600657E RID: 25982 RVA: 0x0028355B File Offset: 0x0028175B
		public override void OnEnter()
		{
			Application.OpenURL(this.url.Value);
			this.Continue();
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x00283573 File Offset: 0x00281773
		public override string GetSummary()
		{
			return this.url.Value;
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x00283580 File Offset: 0x00281780
		public override bool HasReference(Variable variable)
		{
			return this.url.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04005730 RID: 22320
		[Tooltip("URL to open in the browser")]
		[SerializeField]
		protected StringData url;
	}
}
