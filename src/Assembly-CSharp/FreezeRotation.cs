using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
	public Transform tr;

	private Transform myTransform;

	private Vector3 shieldPosition;

	private float x;

	private float y;

	private void Start()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		shieldPosition = ((Component)this).transform.position;
	}

	private void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.position = new Vector3(tr.position.x, tr.position.y, ((Component)this).transform.position.z);
	}
}
