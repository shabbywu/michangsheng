using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009BD RID: 2493
	public class HyperlinkNode : TextNode
	{
		// Token: 0x06003F7B RID: 16251 RVA: 0x0002D9F2 File Offset: 0x0002BBF2
		public override void onMouseEnter()
		{
			this.isEnter = true;
			this.owner.SetRenderDirty();
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x0002DA06 File Offset: 0x0002BC06
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

		// Token: 0x06003F7D RID: 16253 RVA: 0x0002DA1D File Offset: 0x0002BC1D
		public override void onMouseLeave()
		{
			this.isEnter = false;
			this.owner.SetRenderDirty();
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool IsHyText()
		{
			return true;
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0002DA31 File Offset: 0x0002BC31
		public override void Release()
		{
			base.Release();
			this.isEnter = false;
		}

		// Token: 0x040038E3 RID: 14563
		private bool isEnter;

		// Token: 0x040038E4 RID: 14564
		public Color hoveColor = Color.red;

		// Token: 0x040038E5 RID: 14565
		public string d_link;
	}
}
