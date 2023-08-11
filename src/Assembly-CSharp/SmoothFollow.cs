using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	public Transform target;

	public float distance = 10f;

	public float height = 5f;

	public float heightDamping = 2f;

	public float rotationDamping = 0.3f;

	public float rotate;

	public void ResetView()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)target))
		{
			float y = target.eulerAngles.y;
			float num = target.position.y + height;
			float y2 = ((Component)this).transform.position.y;
			y2 = Mathf.Lerp(y2, num, heightDamping * Time.deltaTime);
			Quaternion val = Quaternion.Euler(0f, y, 0f);
			((Component)this).transform.position = target.position;
			Transform transform = ((Component)this).transform;
			transform.position -= val * Vector3.forward * distance;
			((Component)this).transform.position = new Vector3(((Component)this).transform.position.x, y2, ((Component)this).transform.position.z);
			((Component)this).transform.LookAt(target);
		}
	}

	public void FollowUpdate()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)target))
		{
			float y = target.eulerAngles.y;
			float num = target.position.y + height;
			float y2 = ((Component)this).transform.eulerAngles.y;
			float y3 = ((Component)this).transform.position.y;
			float num2 = y - y2;
			if (num2 > 180f)
			{
				num2 -= 360f;
			}
			else if (num2 < -180f)
			{
				num2 += 360f;
			}
			if (num2 > 90f)
			{
				num2 = 180f - num2;
			}
			if (num2 < -90f)
			{
				num2 = -180f - num2;
			}
			y2 = Mathf.LerpAngle(y2, y2 + num2, rotationDamping * Time.deltaTime);
			y3 = Mathf.Lerp(y3, num, heightDamping * Time.deltaTime);
			Quaternion val = Quaternion.Euler(0f, y2, 0f);
			((Component)this).transform.position = target.position;
			Transform transform = ((Component)this).transform;
			transform.position -= val * Vector3.forward * distance;
			((Component)this).transform.position = new Vector3(((Component)this).transform.position.x, y3, ((Component)this).transform.position.z);
			((Component)this).transform.LookAt(target);
		}
	}

	public void rotateCamerX(float rotatex)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)target))
		{
			_ = target.eulerAngles;
			float num = target.position.y + height;
			float y = ((Component)this).transform.eulerAngles.y;
			float y2 = ((Component)this).transform.position.y;
			float num2 = rotatex;
			if (num2 > 180f)
			{
				num2 -= 360f;
			}
			else if (num2 < -180f)
			{
				num2 += 360f;
			}
			if (num2 > 90f)
			{
				num2 = 180f - num2;
			}
			if (num2 < -90f)
			{
				num2 = -180f - num2;
			}
			y = Mathf.LerpAngle(y, y + num2, rotationDamping * Time.deltaTime * 30f);
			y2 = Mathf.Lerp(y2, num, heightDamping * Time.deltaTime * 10f);
			Quaternion val = Quaternion.Euler(0f, y, 0f);
			((Component)this).transform.position = target.position;
			Transform transform = ((Component)this).transform;
			transform.position -= val * Vector3.forward * distance;
			((Component)this).transform.position = new Vector3(((Component)this).transform.position.x, y2, ((Component)this).transform.position.z);
			((Component)this).transform.LookAt(target);
		}
	}
}
