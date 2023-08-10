using UnityEngine;

[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	public Vector2 degrees = new Vector2(5f, 3f);

	public float range = 1f;

	private Transform mTrans;

	private Quaternion mStart;

	private Vector2 mRot = Vector2.zero;

	private void Start()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		mTrans = ((Component)this).transform;
		mStart = mTrans.localRotation;
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = RealTime.deltaTime;
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (range < 0.1f)
		{
			range = 0.1f;
		}
		float num3 = Mathf.Clamp((mousePosition.x - num) / num / range, -1f, 1f);
		float num4 = Mathf.Clamp((mousePosition.y - num2) / num2 / range, -1f, 1f);
		mRot = Vector2.Lerp(mRot, new Vector2(num3, num4), deltaTime * 5f);
		mTrans.localRotation = mStart * Quaternion.Euler((0f - mRot.y) * degrees.y, mRot.x * degrees.x, 0f);
	}
}
