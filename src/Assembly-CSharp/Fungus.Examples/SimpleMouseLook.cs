using UnityEngine;

namespace Fungus.Examples;

public class SimpleMouseLook : MonoBehaviour
{
	public float xsen = 1f;

	public float ysen = 1f;

	public float maxPitch = 60f;

	public Transform target;

	private float pitch;

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		Quaternion localRotation = target.localRotation;
		Vector3 eulerAngles = ((Quaternion)(ref localRotation)).eulerAngles;
		((Vector3)(ref eulerAngles))._002Ector(pitch - Input.GetAxis("Mouse Y"), eulerAngles.y + Input.GetAxis("Mouse X"), 0f);
		eulerAngles.z = 0f;
		eulerAngles.x = Mathf.Clamp(eulerAngles.x, 0f - maxPitch, maxPitch);
		pitch = eulerAngles.x;
		target.localRotation = Quaternion.Euler(eulerAngles);
	}
}
