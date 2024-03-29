using System.Collections.Generic;
using UnityEngine;

namespace WXB;

internal class LineNode : NodeBase
{
	public Font font;

	public FontStyle fs;

	public int fontSize;

	private float height;

	public override float getHeight()
	{
		return height;
	}

	public override float getWidth()
	{
		return 0f;
	}

	public override void fill(ref Vector2 currentpos, List<Line> lines, float maxWidth, float pixelsPerUnit)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		height = (float)FontCache.GetLineHeight(font, (int)((float)fontSize * pixelsPerUnit), fs) / pixelsPerUnit;
		lines.Add(new Line(new Vector2(0f, height)));
	}

	public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
	{
		yline++;
		x = offsetX;
	}

	public override void Release()
	{
		base.Release();
		height = 0f;
	}
}
