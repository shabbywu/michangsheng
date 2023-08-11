using UnityEngine;

public class AttachCamera : MonoBehaviour
{
	private Transform myTransform;

	public Transform target;

	public Vector3 offset = new Vector3(0f, 5f, -5f);

	private void Start()
	{
		myTransform = ((Component)this).transform;
	}

	private void FixedUpdate()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)target != (Object)null)
		{
			myTransform.position = target.position + offset;
			myTransform.LookAt(target.position, Vector3.up);
		}
	}
}
