using UnityEngine;

namespace Fungus.Examples;

public class SimpleForceMovement : MonoBehaviour
{
	public Rigidbody rb;

	public Transform getForwardFrom;

	public float forceScale;

	private void FixedUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = getForwardFrom.forward;
		val.y = 0f;
		((Vector3)(ref val)).Normalize();
		Vector3 val2 = getForwardFrom.right;
		val2.y = 0f;
		((Vector3)(ref val2)).Normalize();
		val *= Input.GetAxis("Vertical");
		val2 *= Input.GetAxis("Horizontal");
		Vector3 val3 = val + val2;
		if (((Vector3)(ref val3)).magnitude > 1f)
		{
			val3 = ((Vector3)(ref val3)).normalized;
		}
		rb.AddForce(val3 * forceScale);
	}
}
