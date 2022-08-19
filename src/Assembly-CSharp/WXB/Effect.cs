using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x0200068B RID: 1675
	public static class Effect
	{
		// Token: 0x0600351A RID: 13594 RVA: 0x0016FE49 File Offset: 0x0016E049
		private static void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			Effect.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x0016FE58 File Offset: 0x0016E058
		private static void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count + end - start;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x0016FF00 File Offset: 0x0016E100
		public static void Shadow(VertexHelper vh, int start, Color effectColor, Vector2 effectDistance)
		{
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			Effect.ApplyShadow(list, effectColor, start, list.Count, effectDistance.x, effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x0016FF4C File Offset: 0x0016E14C
		public static void Outline(VertexHelper vh, int start, Color effectColor, Vector2 effectDistance)
		{
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			int num = (list.Count - start) * 5;
			if (list.Capacity < num)
			{
				list.Capacity = num;
			}
			int count = list.Count;
			Effect.ApplyShadowZeroAlloc(list, effectColor, start, list.Count, effectDistance.x, effectDistance.y);
			start = count;
			int count2 = list.Count;
			Effect.ApplyShadowZeroAlloc(list, effectColor, start, list.Count, effectDistance.x, -effectDistance.y);
			start = count2;
			int count3 = list.Count;
			Effect.ApplyShadowZeroAlloc(list, effectColor, start, list.Count, -effectDistance.x, effectDistance.y);
			start = count3;
			int count4 = list.Count;
			Effect.ApplyShadowZeroAlloc(list, effectColor, start, list.Count, -effectDistance.x, -effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}
	}
}
