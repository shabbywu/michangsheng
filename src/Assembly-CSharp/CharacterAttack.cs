using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class CharacterAttack : MonoBehaviour
{
	// Token: 0x06000BE0 RID: 3040 RVA: 0x0000DF77 File Offset: 0x0000C177
	public void StartDamage()
	{
		this.listObjHitted.Clear();
		this.Activated = false;
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x0000DF8B File Offset: 0x0000C18B
	private void AddObjHitted(GameObject obj)
	{
		this.listObjHitted.Add(obj);
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0009467C File Offset: 0x0009287C
	public void AddFloatingText(Vector3 pos, string text)
	{
		if (this.FloatingText)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.FloatingText, pos, base.transform.rotation);
			if (gameObject.GetComponent<FloatingText>())
			{
				gameObject.GetComponent<FloatingText>().Text = text;
			}
			Object.Destroy(gameObject, 1f);
		}
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000946D4 File Offset: 0x000928D4
	public void DoDamage()
	{
		this.Activated = true;
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.Radius))
		{
			if (collider && !(collider.gameObject == base.gameObject) && !(collider.gameObject.tag == base.gameObject.tag) && !this.listObjHitted.Contains(collider.gameObject) && Vector3.Dot((collider.transform.position - base.transform.position).normalized, base.transform.forward) >= this.Direction)
			{
				OrbitGameObject component = Camera.main.gameObject.GetComponent<OrbitGameObject>();
				if (component != null && component.Target == collider.gameObject)
				{
					ShakeCamera.Shake(0.5f, 0.5f);
				}
				Vector3 vector = (base.transform.forward + base.transform.up) * (float)this.Force;
				if (collider.gameObject.GetComponent<CharacterStatus>())
				{
					if (this.SoundHit.Length != 0)
					{
						int num = Random.Range(0, this.SoundHit.Length);
						if (this.SoundHit[num] != null)
						{
							AudioSource.PlayClipAtPoint(this.SoundHit[num], base.transform.position);
						}
					}
					int damage = base.gameObject.GetComponent<CharacterStatus>().Damage;
					int damage2 = (int)Random.Range((float)damage / 2f, (float)damage) + 1;
					CharacterStatus component2 = collider.gameObject.GetComponent<CharacterStatus>();
					int num2 = component2.ApplayDamage(damage2, vector);
					this.AddFloatingText(collider.transform.position + Vector3.up, num2.ToString());
					component2.AddParticle(collider.transform.position + Vector3.up);
				}
				if (collider.GetComponent<Rigidbody>())
				{
					collider.GetComponent<Rigidbody>().AddForce(vector);
				}
				this.AddObjHitted(collider.gameObject);
			}
		}
	}

	// Token: 0x040008CB RID: 2251
	public float Direction = 0.5f;

	// Token: 0x040008CC RID: 2252
	public float Radius = 1f;

	// Token: 0x040008CD RID: 2253
	public int Force = 500;

	// Token: 0x040008CE RID: 2254
	public AudioClip[] SoundHit;

	// Token: 0x040008CF RID: 2255
	public bool Activated;

	// Token: 0x040008D0 RID: 2256
	public GameObject FloatingText;

	// Token: 0x040008D1 RID: 2257
	private HashSet<GameObject> listObjHitted = new HashSet<GameObject>();
}
