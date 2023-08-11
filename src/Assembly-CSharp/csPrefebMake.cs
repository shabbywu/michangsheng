using UnityEngine;

public class csPrefebMake : MonoBehaviour
{
	public Transform MakePrefeb;

	private Transform _MakePrefeb;

	public float DeadTime;

	private void Start()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		_MakePrefeb = Object.Instantiate<Transform>(MakePrefeb, ((Component)this).transform.position, Quaternion.identity);
		((Component)_MakePrefeb).transform.parent = ((Component)this).transform;
	}

	private void Update()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (!(DeadTime <= 0f))
		{
			if (Object.op_Implicit((Object)(object)_MakePrefeb))
			{
				Object.Destroy((Object)(object)((Component)_MakePrefeb).gameObject, DeadTime);
			}
			else if (!Object.op_Implicit((Object)(object)_MakePrefeb))
			{
				_MakePrefeb = Object.Instantiate<Transform>(MakePrefeb, ((Component)this).transform.position, Quaternion.identity);
				((Component)_MakePrefeb).transform.parent = ((Component)this).transform;
			}
		}
	}
}
