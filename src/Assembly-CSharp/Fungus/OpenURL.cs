using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001257 RID: 4695
	[CommandInfo("Scripting", "Open URL", "Opens the specified URL in the browser.", 0)]
	public class OpenURL : Command
	{
		// Token: 0x0600720C RID: 29196 RVA: 0x0004D965 File Offset: 0x0004BB65
		public override void OnEnter()
		{
			Application.OpenURL(this.url.Value);
			this.Continue();
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x0004D97D File Offset: 0x0004BB7D
		public override string GetSummary()
		{
			return this.url.Value;
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x0004D98A File Offset: 0x0004BB8A
		public override bool HasReference(Variable variable)
		{
			return this.url.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006465 RID: 25701
		[Tooltip("URL to open in the browser")]
		[SerializeField]
		protected StringData url;
	}
}
