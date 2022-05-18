using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class DamageHitActive : MonoBehaviour
{
	// Token: 0x06000C77 RID: 3191 RVA: 0x000980D0 File Offset: 0x000962D0
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == this.TagDamage)
		{
			if (this.explosiveObject)
			{
				Object.Instantiate<GameObject>(this.explosiveObject, base.transform.position, base.transform.rotation);
				Object.Destroy(base.gameObject);
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400099B RID: 2459
	public GameObject explosiveObject;

	// Token: 0x0400099C RID: 2460
	public string TagDamage;
}
