using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public class XSpaceNode : NodeBase
{
	public float d_offset;

	public override float getHeight()
	{
		return 0.01f;
	}

	public override float getWidth()
	{
		return d_offset;
	}

	public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
	{
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(x + offsetX, offsetY);
		for (int i = 0; i < yline; i++)
		{
			val.y += lines[i].y;
		}
		float num = NodeBase.AlignedFormatting(owner, formatting, maxWidth, lines[(int)yline].x);
		if (x + d_offset + num > maxWidth)
		{
			yline++;
			x = 0f;
		}
		else
		{
			x += d_offset;
		}
		if (d_bNewLine)
		{
			yline++;
			x = 0f;
		}
	}

	public override void Release()
	{
		base.Release();
		d_offset = 0f;
	}
}
