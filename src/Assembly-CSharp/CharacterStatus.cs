using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class CharacterStatus : MonoBehaviour
{
	// Token: 0x06000B30 RID: 2864 RVA: 0x00044384 File Offset: 0x00042584
	private void Update()
	{
		if (Time.time - this.lastRegen >= 1f)
		{
			this.lastRegen = Time.time;
			this.HP += this.HPregen;
			this.SP += this.SPregen;
		}
		if (this.HP > this.HPmax)
		{
			this.HP = this.HPmax;
		}
		if (this.SP > this.SPmax)
		{
			this.SP = this.SPmax;
		}
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x0004440C File Offset: 0x0004260C
	public int ApplayDamage(int damage, Vector3 dirdamge)
	{
		if (this.HP < 0)
		{
			return 0;
		}
		if (this.SoundHit.Length != 0)
		{
			int num = Random.Range(0, this.SoundHit.Length);
			if (this.SoundHit[num] != null)
			{
				AudioSource.PlayClipAtPoint(this.SoundHit[num], base.transform.position);
			}
		}
		if (base.gameObject.GetComponent<CharacterSystem>())
		{
			base.gameObject.GetComponent<CharacterSystem>().GotHit(1f);
		}
		int num2 = damage - this.Defend;
		if (num2 < 1)
		{
			num2 = 1;
		}
		this.HP -= num2;
		this.velocityDamage = dirdamge;
		if (this.HP <= 0)
		{
			this.Dead();
		}
		return num2;
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x000444BF File Offset: 0x000426BF
	public void AddParticle(Vector3 pos)
	{
		if (this.ParticleObject)
		{
			Object.Destroy(Object.Instantiate<GameObject>(this.ParticleObject, pos, base.transform.rotation), 1f);
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x000444F0 File Offset: 0x000426F0
	private void Dead()
	{
		if (this.DeadbodyModel)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.DeadbodyModel, base.gameObject.transform.position, base.gameObject.transform.rotation);
			this.CopyTransformsRecurse(base.gameObject.transform, gameObject.transform);
			Object.Destroy(gameObject, 10f);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00044564 File Offset: 0x00042764
	public void CopyTransformsRecurse(Transform src, Transform dst)
	{
		dst.position = src.position;
		dst.rotation = src.rotation;
		dst.localScale = src.localScale;
		foreach (Transform transform in dst.Cast<Transform>())
		{
			Transform transform2 = src.Find(transform.name);
			if (transform.GetComponent<Rigidbody>())
			{
				transform.GetComponent<Rigidbody>().AddForce(this.velocityDamage / 3f);
			}
			if (transform2)
			{
				this.CopyTransformsRecurse(transform2, transform);
			}
		}
	}

	// Token: 0x0400074D RID: 1869
	public GameObject DeadbodyModel;

	// Token: 0x0400074E RID: 1870
	public GameObject ParticleObject;

	// Token: 0x0400074F RID: 1871
	public string Name = "";

	// Token: 0x04000750 RID: 1872
	public int HP = 10;

	// Token: 0x04000751 RID: 1873
	public int SP = 10;

	// Token: 0x04000752 RID: 1874
	public int SPmax = 10;

	// Token: 0x04000753 RID: 1875
	public int HPmax = 10;

	// Token: 0x04000754 RID: 1876
	public int Damage = 1;

	// Token: 0x04000755 RID: 1877
	public int Defend = 1;

	// Token: 0x04000756 RID: 1878
	public int HPregen = 1;

	// Token: 0x04000757 RID: 1879
	public int SPregen = 1;

	// Token: 0x04000758 RID: 1880
	public AudioClip[] SoundHit;

	// Token: 0x04000759 RID: 1881
	private Vector3 velocityDamage;

	// Token: 0x0400075A RID: 1882
	private float lastRegen;
}
