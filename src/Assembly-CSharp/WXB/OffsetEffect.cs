using UnityEngine;

namespace WXB;

public class OffsetEffect : IEffect
{
	private Vector2 offset = Vector2.zero;

	public float xMin = -5f;

	public float yMin = -5f;

	public float xMax = 5f;

	public float yMax = 5f;

	public float speed = 2f;

	private Tweener tweener;

	private Draw current;

	public void UpdateEffect(Draw draw, float deltaTime)
	{
		if (tweener == null)
		{
			tweener = new Tweener();
			tweener.method = Tweener.Method.EaseInOut;
			tweener.style = Tweener.Style.PingPong;
			tweener.duration = 1f;
			tweener.OnUpdate = UpdateOffset;
		}
		current = draw;
		tweener.Update(deltaTime);
		current = null;
	}

	private void UpdateOffset(float val, bool isFin)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		offset = Vector2.Lerp(new Vector2(xMin, yMin), new Vector2(xMax, yMax), val);
		Tools.UpdateRect(current.rectTransform, offset);
	}

	public void Release()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (tweener != null)
		{
			tweener.method = Tweener.Method.EaseInOut;
			tweener.style = Tweener.Style.PingPong;
			tweener.duration = 1f;
		}
		current = null;
		xMin = -5f;
		yMin = -5f;
		xMax = 5f;
		yMax = 5f;
		speed = 2f;
		offset = Vector2.zero;
	}
}
