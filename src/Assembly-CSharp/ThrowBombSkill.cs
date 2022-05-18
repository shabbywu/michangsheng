using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class ThrowBombSkill : MonoBehaviour
{
	// Token: 0x06000C81 RID: 3201 RVA: 0x00098564 File Offset: 0x00096764
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().AddForce((base.transform.forward + Vector3.up) * this.Force);
			base.GetComponent<Rigidbody>().AddTorque((base.transform.forward + Vector3.up) * this.Force);
		}
		base.StartCoroutine(this.countdown());
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0000E689 File Offset: 0x0000C889
	private IEnumerator countdown()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.Dulation);
			Object.Instantiate<GameObject>(this.DamageSkill, base.transform.position, Quaternion.identity);
			Object.Destroy(base.gameObject);
		}
		yield break;
	}

	// Token: 0x040009B4 RID: 2484
	public float Dulation = 3f;

	// Token: 0x040009B5 RID: 2485
	public GameObject DamageSkill;

	// Token: 0x040009B6 RID: 2486
	public float Force = 300f;
}
