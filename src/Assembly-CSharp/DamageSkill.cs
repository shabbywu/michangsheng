using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class DamageSkill : MonoBehaviour
{
	// Token: 0x06000C79 RID: 3193 RVA: 0x00098138 File Offset: 0x00096338
	private void Start()
	{
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.Radius))
		{
			if (collider)
			{
				if (collider.tag == this.TagDamage && collider.gameObject.GetComponent<CharacterStatus>())
				{
					collider.gameObject.GetComponent<CharacterStatus>().ApplayDamage(this.Damage, Vector3.zero);
				}
				if (collider.GetComponent<Rigidbody>())
				{
					collider.GetComponent<Rigidbody>().AddExplosionForce((float)this.Force, base.transform.position, this.Radius, 3f);
				}
			}
		}
	}

	// Token: 0x0400099D RID: 2461
	public int Force;

	// Token: 0x0400099E RID: 2462
	public string TagDamage;

	// Token: 0x0400099F RID: 2463
	public int Damage;

	// Token: 0x040009A0 RID: 2464
	public float Radius;
}
