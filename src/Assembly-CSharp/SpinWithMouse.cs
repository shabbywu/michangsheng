using UnityEngine;

[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	public Transform target;

	public float speed = 1f;

	private Transform mTrans;

	private void Start()
	{
		mTrans = ((Component)this).transform;
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if ((Object)(object)target != (Object)null)
		{
			target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * speed, 0f) * target.localRotation;
		}
		else
		{
			mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * speed, 0f) * mTrans.localRotation;
		}
	}
}
