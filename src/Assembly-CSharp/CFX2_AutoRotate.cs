using UnityEngine;

public class CFX2_AutoRotate : MonoBehaviour
{
	public Vector3 speed = new Vector3(0f, 40f, 0f);

	private void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.Rotate(speed * Time.deltaTime);
	}
}
