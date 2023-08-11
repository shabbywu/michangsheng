using UnityEngine;

namespace WXB;

public class SpriteNode : RectNode
{
	public Sprite sprite;

	protected override void OnRectRender(RenderCache cache, Line line, Rect rect)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		cache.cacheSprite(line, this, sprite, rect);
	}
}
