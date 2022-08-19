using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class DamageHitActive : MonoBehaviour
{
	// Token: 0x06000B88 RID: 2952 RVA: 0x0004667C File Offset: 0x0004487C
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

	// Token: 0x040007BD RID: 1981
	public GameObject explosiveObject;

	// Token: 0x040007BE RID: 1982
	public string TagDamage;
}
