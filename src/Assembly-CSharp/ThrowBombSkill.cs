using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class ThrowBombSkill : MonoBehaviour
{
	// Token: 0x06000B92 RID: 2962 RVA: 0x00046B5C File Offset: 0x00044D5C
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().AddForce((base.transform.forward + Vector3.up) * this.Force);
			base.GetComponent<Rigidbody>().AddTorque((base.transform.forward + Vector3.up) * this.Force);
		}
		base.StartCoroutine(this.countdown());
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00046BD9 File Offset: 0x00044DD9
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

	// Token: 0x040007D6 RID: 2006
	public float Dulation = 3f;

	// Token: 0x040007D7 RID: 2007
	public GameObject DamageSkill;

	// Token: 0x040007D8 RID: 2008
	public float Force = 300f;
}
