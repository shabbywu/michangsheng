using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public abstract class RectNode : NodeBase
{
	public float width;

	public float height;

	public override float getHeight()
	{
		return height;
	}

	public override float getWidth()
	{
		return width;
	}

	public override void Release()
	{
		base.Release();
		width = 0f;
		height = 0f;
	}

	protected abstract void OnRectRender(RenderCache cache, Line line, Rect rect);

	public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		float num = getWidth();
		float num2 = getHeight();
		if (x + num > maxWidth)
		{
			x = 0f;
			yline++;
		}
		float num3 = NodeBase.AlignedFormatting(owner, formatting, maxWidth, lines[(int)yline].x);
		float num4 = offsetY;
		for (int i = 0; i < yline; i++)
		{
			num4 += lines[i].y;
		}
		Rect rect = default(Rect);
		((Rect)(ref rect))._002Ector(x + offsetX + num3, num4, num, num2);
		float ox = 0f;
		while (!owner.around.isContain(rect, out ox))
		{
			((Rect)(ref rect)).x = ox;
			x = ox - num3 - offsetX;
			if (x + num > maxWidth)
			{
				x = 0f;
				yline++;
				num4 += lines[(int)yline].y;
				((Rect)(ref rect))._002Ector(x + offsetX + num3, num4, num, num2);
			}
		}
		OnRectRender(cache, lines[(int)yline], rect);
		x += num;
		if (d_bNewLine)
		{
			x = 0f;
			yline++;
		}
	}
}
