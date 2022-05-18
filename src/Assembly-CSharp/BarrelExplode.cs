using System;
using UnityEngine;

// Token: 0x0200066C RID: 1644
public class BarrelExplode : MonoBehaviour
{
	// Token: 0x0600291A RID: 10522 RVA: 0x0001FF5C File Offset: 0x0001E15C
	private void Start()
	{
		this.anim = base.transform.GetChild(0).GetComponent<Animator>();
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x00141244 File Offset: 0x0013F444
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

	// Token: 0x0600291C RID: 10524 RVA: 0x001413E4 File Offset: 0x0013F5E4
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

	// Token: 0x040022DF RID: 8927
	private Animator anim;

	// Token: 0x040022E0 RID: 8928
	public ParticleSystem eksplozija1;

	// Token: 0x040022E1 RID: 8929
	public ParticleSystem eksplozija2;

	// Token: 0x040022E2 RID: 8930
	public ParticleSystem eksplozija3;

	// Token: 0x040022E3 RID: 8931
	public ParticleSystem eksplozija4;

	// Token: 0x040022E4 RID: 8932
	private bool razbijenoBure;

	// Token: 0x040022E5 RID: 8933
	public Animator coinsReward;
}
