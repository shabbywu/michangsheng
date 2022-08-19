using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000692 RID: 1682
	public class Line
	{
		// Token: 0x06003532 RID: 13618 RVA: 0x00170548 File Offset: 0x0016E748
		public Line(Vector2 s)
		{
			this.size = s;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x00170557 File Offset: 0x0016E757
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x00170564 File Offset: 0x0016E764
		public float x
		{
			get
			{
				return this.size.x;
			}
			set
			{
				this.size.x = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x00170572 File Offset: 0x0016E772
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x0017057F File Offset: 0x0016E77F
		public float y
		{
			get
			{
				return this.size.y;
			}
			set
			{
				this.size.y = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x0017058D File Offset: 0x0016E78D
		public Vector2 s
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x00170595 File Offset: 0x0016E795
		public void Clear()
		{
			this.size = Vector2.zero;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x001705A2 File Offset: 0x0016E7A2
		// (set) Token: 0x0600353A RID: 13626 RVA: 0x001705AA File Offset: 0x0016E7AA
		public float minY { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x001705B3 File Offset: 0x0016E7B3
		// (set) Token: 0x0600353C RID: 13628 RVA: 0x001705BB File Offset: 0x0016E7BB
		public float maxY { get; set; }

		// Token: 0x0600353D RID: 13629 RVA: 0x001705C4 File Offset: 0x0016E7C4
		public override string ToString()
		{
			return string.Format("w:{0} h:{1} minY:{2} maxY:{3} fh:{4}", new object[]
			{
				this.x,
				this.y,
				this.minY,
				this.maxY,
				this.fontHeight
			});
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x00170627 File Offset: 0x0016E827
		public float fontHeight
		{
			get
			{
				return this.maxY - this.minY;
			}
		}

		// Token: 0x04002EF0 RID: 12016
		private Vector2 size;
	}
}
