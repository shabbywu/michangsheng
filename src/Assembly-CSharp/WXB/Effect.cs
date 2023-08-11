using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB;

public static class Effect
{
	private static void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ApplyShadowZeroAlloc(verts, color, start, end, x, y);
	}

	private static void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		int num = verts.Count + end - start;
		if (verts.Capacity < num)
		{
			verts.Capacity = num;
		}
		for (int i = start; i < end; i++)
		{
			UIVertex val = verts[i];
			verts.Add(val);
			Vector3 position = val.position;
			position.x += x;
			position.y += y;
			val.position = position;
			Color32 val2 = color;
			val2.a = (byte)(val2.a * verts[i].color.a / 255);
			val.color = val2;
			verts[i] = val;
		}
	}

	public static void Shadow(VertexHelper vh, int start, Color effectColor, Vector2 effectDistance)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		List<UIVertex> list = ListPool<UIVertex>.Get();
		vh.GetUIVertexStream(list);
		ApplyShadow(list, Color32.op_Implicit(effectColor), start, list.Count, effectDistance.x, effectDistance.y);
		vh.Clear();
		vh.AddUIVertexTriangleStream(list);
		ListPool<UIVertex>.Release(list);
	}

	public static void Outline(VertexHelper vh, int start, Color effectColor, Vector2 effectDistance)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		List<UIVertex> list = ListPool<UIVertex>.Get();
		vh.GetUIVertexStream(list);
		int num = (list.Count - start) * 5;
		if (list.Capacity < num)
		{
			list.Capacity = num;
		}
		int count = list.Count;
		ApplyShadowZeroAlloc(list, Color32.op_Implicit(effectColor), start, list.Count, effectDistance.x, effectDistance.y);
		start = count;
		int count2 = list.Count;
		ApplyShadowZeroAlloc(list, Color32.op_Implicit(effectColor), start, list.Count, effectDistance.x, 0f - effectDistance.y);
		start = count2;
		int count3 = list.Count;
		ApplyShadowZeroAlloc(list, Color32.op_Implicit(effectColor), start, list.Count, 0f - effectDistance.x, effectDistance.y);
		start = count3;
		_ = list.Count;
		ApplyShadowZeroAlloc(list, Color32.op_Implicit(effectColor), start, list.Count, 0f - effectDistance.x, 0f - effectDistance.y);
		vh.Clear();
		vh.AddUIVertexTriangleStream(list);
		ListPool<UIVertex>.Release(list);
	}
}
