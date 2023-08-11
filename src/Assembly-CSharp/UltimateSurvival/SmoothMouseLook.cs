using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
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

	private float rotationX;

	private float rotationY;

	private List<float> rotArrayX = new List<float>();

	private float rotAverageX;

	private List<float> rotArrayY = new List<float>();

	private float rotAverageY;

	public float frameCounter = 20f;

	private Quaternion originalRotation;

	private void Update()
	{
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		if (axes == RotationAxes.MouseXAndY)
		{
			rotAverageY = 0f;
			rotAverageX = 0f;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotArrayY.Add(rotationY);
			rotArrayX.Add(rotationX);
			if ((float)rotArrayY.Count >= frameCounter)
			{
				rotArrayY.RemoveAt(0);
			}
			if ((float)rotArrayX.Count >= frameCounter)
			{
				rotArrayX.RemoveAt(0);
			}
			for (int i = 0; i < rotArrayY.Count; i++)
			{
				rotAverageY += rotArrayY[i];
			}
			for (int j = 0; j < rotArrayX.Count; j++)
			{
				rotAverageX += rotArrayX[j];
			}
			rotAverageY /= rotArrayY.Count;
			rotAverageX /= rotArrayX.Count;
			rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
			Quaternion val = Quaternion.AngleAxis(rotAverageY, Vector3.left);
			Quaternion val2 = Quaternion.AngleAxis(rotAverageX, Vector3.up);
			((Component)this).transform.localRotation = originalRotation * val2 * val;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotAverageX = 0f;
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotArrayX.Add(rotationX);
			if ((float)rotArrayX.Count >= frameCounter)
			{
				rotArrayX.RemoveAt(0);
			}
			for (int k = 0; k < rotArrayX.Count; k++)
			{
				rotAverageX += rotArrayX[k];
			}
			rotAverageX /= rotArrayX.Count;
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
			Quaternion val3 = Quaternion.AngleAxis(rotAverageX, Vector3.up);
			((Component)this).transform.localRotation = originalRotation * val3;
		}
		else
		{
			rotAverageY = 0f;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotArrayY.Add(rotationY);
			if ((float)rotArrayY.Count >= frameCounter)
			{
				rotArrayY.RemoveAt(0);
			}
			for (int l = 0; l < rotArrayY.Count; l++)
			{
				rotAverageY += rotArrayY[l];
			}
			rotAverageY /= rotArrayY.Count;
			rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
			Quaternion val4 = Quaternion.AngleAxis(rotAverageY, Vector3.left);
			((Component)this).transform.localRotation = originalRotation * val4;
		}
	}

	private void Start()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Rigidbody component = ((Component)this).GetComponent<Rigidbody>();
		if (Object.op_Implicit((Object)(object)component))
		{
			component.freezeRotation = true;
		}
		originalRotation = ((Component)this).transform.localRotation;
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		angle %= 360f;
		if (angle >= -360f && angle <= 360f)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
		}
		return Mathf.Clamp(angle, min, max);
	}
}
