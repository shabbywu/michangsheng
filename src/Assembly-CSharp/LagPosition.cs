using UnityEngine;

[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
	public int updateOrder;

	public Vector3 speed = new Vector3(10f, 10f, 10f);

	public bool ignoreTimeScale;

	private Transform mTrans;

	private Vector3 mRelative;

	private Vector3 mAbsolute;

	private void OnEnable()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		mTrans = ((Component)this).transform;
		mAbsolute = mTrans.position;
		mRelative = mTrans.localPosition;
	}

	private void Update()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		Transform parent = mTrans.parent;
		if ((Object)(object)parent != (Object)null)
		{
			float num = (ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
			Vector3 val = parent.position + parent.rotation * mRelative;
			mAbsolute.x = Mathf.Lerp(mAbsolute.x, val.x, Mathf.Clamp01(num * speed.x));
			mAbsolute.y = Mathf.Lerp(mAbsolute.y, val.y, Mathf.Clamp01(num * speed.y));
			mAbsolute.z = Mathf.Lerp(mAbsolute.z, val.z, Mathf.Clamp01(num * speed.z));
			mTrans.position = mAbsolute;
		}
	}
}
