using UnityEngine;

public class MapFollow : MonoBehaviour
{
	public Transform target;

	private void Start()
	{
	}

	private void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)target != (Object)null)
		{
			((Component)this).transform.position = new Vector3(target.position.x, ((Component)this).transform.position.y, target.position.z);
		}
	}
}
