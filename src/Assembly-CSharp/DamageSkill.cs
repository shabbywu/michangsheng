using UnityEngine;

public class DamageSkill : MonoBehaviour
{
	public int Force;

	public string TagDamage;

	public int Damage;

	public float Radius;

	private void Start()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Collider[] array = Physics.OverlapSphere(((Component)this).transform.position, Radius);
		foreach (Collider val in array)
		{
			if (Object.op_Implicit((Object)(object)val))
			{
				if (((Component)val).tag == TagDamage && Object.op_Implicit((Object)(object)((Component)val).gameObject.GetComponent<CharacterStatus>()))
				{
					((Component)val).gameObject.GetComponent<CharacterStatus>().ApplayDamage(Damage, Vector3.zero);
				}
				if (Object.op_Implicit((Object)(object)((Component)val).GetComponent<Rigidbody>()))
				{
					((Component)val).GetComponent<Rigidbody>().AddExplosionForce((float)Force, ((Component)this).transform.position, Radius, 3f);
				}
			}
		}
	}
}
