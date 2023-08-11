using UnityEngine;

[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	public int updateOrder;

	public Camera uiCamera;

	public float yawAmount = 20f;

	private Transform mTrans;

	private void OnDisable()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		mTrans.localRotation = Quaternion.identity;
	}

	private void OnEnable()
	{
		if ((Object)(object)uiCamera == (Object)null)
		{
			uiCamera = NGUITools.FindCameraForLayer(((Component)this).gameObject.layer);
		}
		mTrans = ((Component)this).transform;
	}

	private void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)uiCamera != (Object)null)
		{
			Vector3 val = uiCamera.WorldToViewportPoint(mTrans.position);
			mTrans.localRotation = Quaternion.Euler(0f, (val.x * 2f - 1f) * yawAmount, 0f);
		}
	}
}
