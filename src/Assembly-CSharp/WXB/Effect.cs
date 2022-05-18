using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x0200099D RID: 2461
	public static class Effect
	{
		// Token: 0x06003ED5 RID: 16085 RVA: 0x0002D394 File Offset: 0x0002B594
		private static void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			Effect.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x001B81D0 File Offset: 0x001B63D0
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

		// Token: 0x06003ED7 RID: 16087 RVA: 0x001B8278 File Offset: 0x001B6478
		public static void Shadow(VertexHelper vh, int start, Color effectColor, Vector2 effectDistance)
		{
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			Effect.ApplyShadow(list, effectColor, start, list.Count, effectDistance.x, effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x001B82C4 File Offset: 0x001B64C4
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
