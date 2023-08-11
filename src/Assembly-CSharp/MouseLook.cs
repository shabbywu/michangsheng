using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	public enum RotationAxes
	{
		MouseXAndY,
		MouseX,
		MouseY
	}

	public RotationAxes axes;

	public float sensitivityX = 15f;

	public float sensitivityY = 15f;

	public float minimumX = -360f;

	public float maximumX = 360f;

	public float minimumY = -60f;

	public float maximumY = 60f;

	private float rotationY;

	private void Update()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetMouseButton(1))
		{
			if (axes == RotationAxes.MouseXAndY)
			{
				float num = ((Component)this).transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
				((Component)this).transform.localEulerAngles = new Vector3(0f - rotationY, num, 0f);
			}
			else if (axes == RotationAxes.MouseX)
			{
				((Component)this).transform.Rotate(0f, Input.GetAxis("Mouse X") * sensitivityX, 0f);
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
				((Component)this).transform.localEulerAngles = new Vector3(0f - rotationY, ((Component)this).transform.localEulerAngles.y, 0f);
			}
		}
	}

	private void Start()
	{
		if (Object.op_Implicit((Object)(object)((Component)this).GetComponent<Rigidbody>()))
		{
			((Component)this).GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
}
