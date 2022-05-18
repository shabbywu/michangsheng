using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class CharacterStatus : MonoBehaviour
{
	// Token: 0x06000C1F RID: 3103 RVA: 0x0009619C File Offset: 0x0009439C
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

	// Token: 0x06000C20 RID: 3104 RVA: 0x00096224 File Offset: 0x00094424
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

	// Token: 0x06000C21 RID: 3105 RVA: 0x0000E27C File Offset: 0x0000C47C
	public void AddParticle(Vector3 pos)
	{
		if (this.ParticleObject)
		{
			Object.Destroy(Object.Instantiate<GameObject>(this.ParticleObject, pos, base.transform.rotation), 1f);
		}
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x000962D8 File Offset: 0x000944D8
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

	// Token: 0x06000C23 RID: 3107 RVA: 0x0009634C File Offset: 0x0009454C
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

	// Token: 0x04000928 RID: 2344
	public GameObject DeadbodyModel;

	// Token: 0x04000929 RID: 2345
	public GameObject ParticleObject;

	// Token: 0x0400092A RID: 2346
	public string Name = "";

	// Token: 0x0400092B RID: 2347
	public int HP = 10;

	// Token: 0x0400092C RID: 2348
	public int SP = 10;

	// Token: 0x0400092D RID: 2349
	public int SPmax = 10;

	// Token: 0x0400092E RID: 2350
	public int HPmax = 10;

	// Token: 0x0400092F RID: 2351
	public int Damage = 1;

	// Token: 0x04000930 RID: 2352
	public int Defend = 1;

	// Token: 0x04000931 RID: 2353
	public int HPregen = 1;

	// Token: 0x04000932 RID: 2354
	public int SPregen = 1;

	// Token: 0x04000933 RID: 2355
	public AudioClip[] SoundHit;

	// Token: 0x04000934 RID: 2356
	private Vector3 velocityDamage;

	// Token: 0x04000935 RID: 2357
	private float lastRegen;
}
