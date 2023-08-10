using UnityEngine;

namespace Fungus;

public class Parallax : MonoBehaviour
{
	[SerializeField]
	protected SpriteRenderer backgroundSprite;

	[SerializeField]
	protected Vector2 parallaxScale = new Vector2(0.25f, 0f);

	[SerializeField]
	protected float accelerometerScale = 0.5f;

	protected Vector3 startPosition;

	protected Vector3 acceleration;

	protected Vector3 velocity;

	protected virtual void Start()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		startPosition = ((Component)this).transform.position;
		if ((Object)(object)backgroundSprite == (Object)null)
		{
			backgroundSprite = ((Component)this).gameObject.GetComponentInParent<SpriteRenderer>();
		}
	}

	protected virtual void Update()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)backgroundSprite == (Object)null))
		{
			Vector3 zero = Vector3.zero;
			Bounds bounds = ((Renderer)backgroundSprite).bounds;
			Vector3 center = ((Bounds)(ref bounds)).center;
			Vector3 position = ((Component)Camera.main).transform.position;
			zero = center - position;
			zero.x *= parallaxScale.x;
			zero.y *= parallaxScale.y;
			zero.z = 0f;
			if (SystemInfo.supportsAccelerometer)
			{
				float num = Mathf.Max(parallaxScale.x, parallaxScale.y);
				acceleration = Vector3.SmoothDamp(acceleration, Input.acceleration, ref velocity, 0.1f);
				Vector3 val = Quaternion.Euler(45f, 0f, 0f) * acceleration * num * accelerometerScale;
				zero += val;
			}
			((Component)this).transform.position = startPosition + zero;
		}
	}
}
