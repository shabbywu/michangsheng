using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public class RectSpriteNode : NodeBase
{
	public Rect rect;

	public Sprite sprite;

	public Color color;

	public override float getHeight()
	{
		return ((Rect)(ref rect)).height;
	}

	public override float getWidth()
	{
		return ((Rect)(ref rect)).width;
	}

	public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		cache.cacheSprite(null, this, sprite, rect);
	}

	public override void fill(ref Vector2 currentpos, List<Line> Lines, float maxWidth, float pixelsPerUnit)
	{
	}
}
