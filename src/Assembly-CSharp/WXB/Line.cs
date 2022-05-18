using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009A5 RID: 2469
	public class Line
	{
		// Token: 0x06003EED RID: 16109 RVA: 0x0002D44B File Offset: 0x0002B64B
		public Line(Vector2 s)
		{
			this.size = s;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x0002D45A File Offset: 0x0002B65A
		// (set) Token: 0x06003EEF RID: 16111 RVA: 0x0002D467 File Offset: 0x0002B667
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

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x0002D475 File Offset: 0x0002B675
		// (set) Token: 0x06003EF1 RID: 16113 RVA: 0x0002D482 File Offset: 0x0002B682
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

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x0002D490 File Offset: 0x0002B690
		public Vector2 s
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0002D498 File Offset: 0x0002B698
		public void Clear()
		{
			this.size = Vector2.zero;
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x0002D4A5 File Offset: 0x0002B6A5
		// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x0002D4AD File Offset: 0x0002B6AD
		public float minY { get; set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x0002D4B6 File Offset: 0x0002B6B6
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x0002D4BE File Offset: 0x0002B6BE
		public float maxY { get; set; }

		// Token: 0x06003EF8 RID: 16120 RVA: 0x001B8818 File Offset: 0x001B6A18
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

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x0002D4C7 File Offset: 0x0002B6C7
		public float fontHeight
		{
			get
			{
				return this.maxY - this.minY;
			}
		}

		// Token: 0x040038A8 RID: 14504
		private Vector2 size;
	}
}
