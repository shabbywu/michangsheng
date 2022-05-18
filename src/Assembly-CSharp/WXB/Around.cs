using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200098E RID: 2446
	public class Around
	{
		// Token: 0x06003E80 RID: 16000 RVA: 0x0002D070 File Offset: 0x0002B270
		public void Add(Rect rect)
		{
			this.m_Rects.Add(rect);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0002D07E File Offset: 0x0002B27E
		public void Clear()
		{
			this.m_Rects.Clear();
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0002D08B File Offset: 0x0002B28B
		public bool isContain(Rect rect, out float ox)
		{
			if (this.m_Rects.Count == 0)
			{
				ox = 0f;
				return true;
			}
			return this.isContain(rect.x, rect.y, rect.width, rect.height, out ox);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x001B74AC File Offset: 0x001B56AC
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

		// Token: 0x04003867 RID: 14439
		private List<Rect> m_Rects = new List<Rect>();
	}
}
