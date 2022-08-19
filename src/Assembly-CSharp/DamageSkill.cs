using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class DamageSkill : MonoBehaviour
{
	// Token: 0x06000B8A RID: 2954 RVA: 0x000466E4 File Offset: 0x000448E4
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

	// Token: 0x040007BF RID: 1983
	public int Force;

	// Token: 0x040007C0 RID: 1984
	public string TagDamage;

	// Token: 0x040007C1 RID: 1985
	public int Damage;

	// Token: 0x040007C2 RID: 1986
	public float Radius;
}
