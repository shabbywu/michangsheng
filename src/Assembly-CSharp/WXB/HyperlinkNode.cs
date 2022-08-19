using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A6 RID: 1702
	public class HyperlinkNode : TextNode
	{
		// Token: 0x060035BA RID: 13754 RVA: 0x00171DC4 File Offset: 0x0016FFC4
		public override void onMouseEnter()
		{
			this.isEnter = true;
			this.owner.SetRenderDirty();
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x00171DD8 File Offset: 0x0016FFD8
		public override Color currentColor
		{
			get
			{
				if (!this.isEnter)
				{
					return this.d_color;
				}
				return this.hoveColor;
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x00171DEF File Offset: 0x0016FFEF
		public override void onMouseLeave()
		{
			this.isEnter = false;
			this.owner.SetRenderDirty();
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool IsHyText()
		{
			return true;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x00171E03 File Offset: 0x00170003
		public override void Release()
		{
			base.Release();
			this.isEnter = false;
		}

		// Token: 0x04002F1D RID: 12061
		private bool isEnter;

		// Token: 0x04002F1E RID: 12062
		public Color hoveColor = Color.red;

		// Token: 0x04002F1F RID: 12063
		public string d_link;
	}
}
