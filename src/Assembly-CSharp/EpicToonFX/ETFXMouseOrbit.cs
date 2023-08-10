using UnityEngine;

namespace EpicToonFX;

public class ETFXMouseOrbit : MonoBehaviour
{
	public Transform target;

	public float distance = 5f;

	public float xSpeed = 120f;

	public float ySpeed = 120f;

	public float yMinLimit = -20f;

	public float yMaxLimit = 80f;

	public float distanceMin = 0.5f;

	public float distanceMax = 15f;

	public float smoothTime = 2f;

	private float rotationYAxis;

	private float rotationXAxis;

	private float velocityX;

	private float velocityY;

	private void Start()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Vector3 eulerAngles = ((Component)this).transform.eulerAngles;
		rotationYAxis = eulerAngles.y;
		rotationXAxis = eulerAngles.x;
		if (Object.op_Implicit((Object)(object)((Component)this).GetComponent<Rigidbody>()))
		{
			((Component)this).GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	private void LateUpdate()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)target))
		{
			if (Input.GetMouseButton(1))
			{
				velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
				velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
			}
			rotationYAxis += velocityX;
			rotationXAxis -= velocityY;
			rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
			Quaternion val = Quaternion.Euler(rotationXAxis, rotationYAxis, 0f);
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5f, distanceMin, distanceMax);
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Linecast(target.position, ((Component)this).transform.position, ref val2))
			{
				distance -= ((RaycastHit)(ref val2)).distance;
			}
			Vector3 val3 = default(Vector3);
			((Vector3)(ref val3))._002Ector(0f, 0f, 0f - distance);
			Vector3 position = val * val3 + target.position;
			((Component)this).transform.rotation = val;
			((Component)this).transform.position = position;
			velocityX = Mathf.Lerp(velocityX, 0f, Time.deltaTime * smoothTime);
			velocityY = Mathf.Lerp(velocityY, 0f, Time.deltaTime * smoothTime);
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
