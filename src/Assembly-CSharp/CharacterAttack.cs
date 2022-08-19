using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class CharacterAttack : MonoBehaviour
{
	// Token: 0x06000AFD RID: 2813 RVA: 0x00042878 File Offset: 0x00040A78
	public void StartDamage()
	{
		this.listObjHitted.Clear();
		this.Activated = false;
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0004288C File Offset: 0x00040A8C
	private void AddObjHitted(GameObject obj)
	{
		this.listObjHitted.Add(obj);
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0004289C File Offset: 0x00040A9C
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

	// Token: 0x06000B01 RID: 2817 RVA: 0x000428F4 File Offset: 0x00040AF4
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

	// Token: 0x04000720 RID: 1824
	public float Direction = 0.5f;

	// Token: 0x04000721 RID: 1825
	public float Radius = 1f;

	// Token: 0x04000722 RID: 1826
	public int Force = 500;

	// Token: 0x04000723 RID: 1827
	public AudioClip[] SoundHit;

	// Token: 0x04000724 RID: 1828
	public bool Activated;

	// Token: 0x04000725 RID: 1829
	public GameObject FloatingText;

	// Token: 0x04000726 RID: 1830
	private HashSet<GameObject> listObjHitted = new HashSet<GameObject>();
}
