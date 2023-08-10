using System.Collections;
using UnityEngine;

public class ThrowBombSkill : MonoBehaviour
{
	public float Dulation = 3f;

	public GameObject DamageSkill;

	public float Force = 300f;

	private void Start()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)((Component)this).GetComponent<Rigidbody>()))
		{
			((Component)this).GetComponent<Rigidbody>().AddForce((((Component)this).transform.forward + Vector3.up) * Force);
			((Component)this).GetComponent<Rigidbody>().AddTorque((((Component)this).transform.forward + Vector3.up) * Force);
		}
		((MonoBehaviour)this).StartCoroutine(countdown());
	}

	private IEnumerator countdown()
	{
		while (true)
		{
			yield return (object)new WaitForSeconds(Dulation);
			Object.Instantiate<GameObject>(DamageSkill, ((Component)this).transform.position, Quaternion.identity);
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}
}
