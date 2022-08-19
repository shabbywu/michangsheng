using System;
using UnityEngine;

// Token: 0x02000493 RID: 1171
public class BarrelExplode : MonoBehaviour
{
	// Token: 0x06002504 RID: 9476 RVA: 0x0010162B File Offset: 0x000FF82B
	private void Start()
	{
		this.anim = base.transform.GetChild(0).GetComponent<Animator>();
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x00101644 File Offset: 0x000FF844
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag.Equals("Monkey"))
		{
			this.anim.Play("BarrelBoom");
			this.eksplozija1.Play();
			this.eksplozija2.Play();
			this.eksplozija3.Play();
			this.eksplozija4.Play();
			this.razbijenoBure = true;
			if (!base.name.Contains("TNT"))
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Bure_Eksplozija();
				}
				base.GetComponent<Collider2D>().enabled = false;
				Manage.barrelsSmashed++;
				MissionManager.Instance.BarrelEvent(Manage.barrelsSmashed);
				Manage.points += 20;
				Manage.pointsText.text = Manage.points.ToString();
				Manage.pointsEffects.RefreshTextOutline(false, true, true);
				int num = Random.Range(5, 11);
				this.coinsReward.transform.Find("+3Coins").GetComponent<TextMesh>().text = (this.coinsReward.transform.Find("+3Coins/+3CoinsShadow").GetComponent<TextMesh>().text = "+" + num);
				this.coinsReward.Play("FadeOutCoins");
				Manage.coinsCollected += num;
				MissionManager.Instance.CoinEvent(Manage.coinsCollected);
				Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
				Manage.Instance.coinsCollectedText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				return;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_TNTBure_Eksplozija();
			}
		}
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x001017E4 File Offset: 0x000FF9E4
	public void ObnoviBure()
	{
		if (this.razbijenoBure)
		{
			base.GetComponent<Collider2D>().enabled = true;
			this.anim.SetTrigger("ObnoviBure");
			this.razbijenoBure = false;
			base.transform.GetChild(0).Find("Barrel").gameObject.SetActive(true);
			base.transform.GetChild(0).Find("BarrelBrokenBottom").gameObject.SetActive(false);
		}
	}

	// Token: 0x04001DD1 RID: 7633
	private Animator anim;

	// Token: 0x04001DD2 RID: 7634
	public ParticleSystem eksplozija1;

	// Token: 0x04001DD3 RID: 7635
	public ParticleSystem eksplozija2;

	// Token: 0x04001DD4 RID: 7636
	public ParticleSystem eksplozija3;

	// Token: 0x04001DD5 RID: 7637
	public ParticleSystem eksplozija4;

	// Token: 0x04001DD6 RID: 7638
	private bool razbijenoBure;

	// Token: 0x04001DD7 RID: 7639
	public Animator coinsReward;
}
