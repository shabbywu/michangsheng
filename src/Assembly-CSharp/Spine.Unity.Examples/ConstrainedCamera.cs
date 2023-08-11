using UnityEngine;

namespace Spine.Unity.Examples;

public class ConstrainedCamera : MonoBehaviour
{
	public Transform target;

	public Vector3 offset;

	public Vector3 min;

	public Vector3 max;

	public float smoothing = 5f;

	private void LateUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = target.position + offset;
		val.x = Mathf.Clamp(val.x, min.x, max.x);
		val.y = Mathf.Clamp(val.y, min.y, max.y);
		val.z = Mathf.Clamp(val.z, min.z, max.z);
		((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, val, smoothing * Time.deltaTime);
	}
}
