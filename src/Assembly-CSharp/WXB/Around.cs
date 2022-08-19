using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000681 RID: 1665
	public class Around
	{
		// Token: 0x060034D0 RID: 13520 RVA: 0x0016F11D File Offset: 0x0016D31D
		public void Add(Rect rect)
		{
			this.m_Rects.Add(rect);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x0016F12B File Offset: 0x0016D32B
		public void Clear()
		{
			this.m_Rects.Clear();
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x0016F138 File Offset: 0x0016D338
		public bool isContain(Rect rect, out float ox)
		{
			if (this.m_Rects.Count == 0)
			{
				ox = 0f;
				return true;
			}
			return this.isContain(rect.x, rect.y, rect.width, rect.height, out ox);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x0016F174 File Offset: 0x0016D374
		public bool isContain(float x, float y, float w, float h, out float ox)
		{
			if (this.m_Rects.Count == 0)
			{
				ox = 0f;
				return true;
			}
			Rect rect;
			rect..ctor(x, y, w, h);
			for (int i = 0; i < this.m_Rects.Count; i++)
			{
				if (this.m_Rects[i].Overlaps(rect))
				{
					ox = this.m_Rects[i].xMax + 5f;
					return false;
				}
			}
			ox = 0f;
			return true;
		}

		// Token: 0x04002EC1 RID: 11969
		private List<Rect> m_Rects = new List<Rect>();
	}
}
