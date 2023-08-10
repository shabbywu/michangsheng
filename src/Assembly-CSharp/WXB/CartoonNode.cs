using UnityEngine;

namespace WXB;

public class CartoonNode : RectNode
{
	public Cartoon cartoon;

	public override float getWidth()
	{
		return (int)(width + cartoon.space);
	}

	protected override void OnRectRender(RenderCache cache, Line line, Rect rect)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		float space = cartoon.space;
		((Rect)(ref rect)).x = ((Rect)(ref rect)).x + space / 2f;
		((Rect)(ref rect)).width = ((Rect)(ref rect)).width - space;
		cache.cacheCartoon(line, this, cartoon, rect);
	}
}
