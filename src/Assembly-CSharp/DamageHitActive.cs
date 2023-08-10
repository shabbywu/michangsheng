using UnityEngine;

public class DamageHitActive : MonoBehaviour
{
	public GameObject explosiveObject;

	public string TagDamage;

	private void OnTriggerEnter(Collider other)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)other).tag == TagDamage)
		{
			if (Object.op_Implicit((Object)(object)explosiveObject))
			{
				Object.Instantiate<GameObject>(explosiveObject, ((Component)this).transform.position, ((Component)this).transform.rotation);
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}
}
