using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB;

public class DrawLineStruct : DrawStruct
{
	public struct Line
	{
		public Vector2 leftPos;

		public float width;

		public float height;

		public Vector2 uv;

		public Color color;

		public int dynSpeed;

		public TextNode node;

		public void Render(VertexHelper vh, ref float curwidth)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			int currentIndexCount = vh.currentIndexCount;
			Color val = color;
			if (!(val.a <= 0.01f))
			{
				if (curwidth >= width)
				{
					Tools.AddLine(vh, leftPos, uv, width, height, val);
				}
				else
				{
					Tools.AddLine(vh, leftPos, uv, curwidth, height, val);
				}
				switch (node.effectType)
				{
				case EffectType.Outline:
					val = node.effectColor;
					Effect.Outline(vh, currentIndexCount, val, node.effectDistance);
					break;
				case EffectType.Shadow:
					val = node.effectColor;
					Effect.Shadow(vh, currentIndexCount, val, node.effectDistance);
					break;
				}
				curwidth -= width;
			}
		}
	}

	public List<Line> lines = new List<Line>();

	public void Render(float width, VertexHelper vh)
	{
		for (int i = 0; i < lines.Count; i++)
		{
			lines[i].Render(vh, ref width);
			if (width <= 0f)
			{
				break;
			}
		}
	}
}
