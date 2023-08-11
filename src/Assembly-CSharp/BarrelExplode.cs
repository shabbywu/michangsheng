using UnityEngine;

public class BarrelExplode : MonoBehaviour
{
	private Animator anim;

	public ParticleSystem eksplozija1;

	public ParticleSystem eksplozija2;

	public ParticleSystem eksplozija3;

	public ParticleSystem eksplozija4;

	private bool razbijenoBure;

	public Animator coinsReward;

	private void Start()
	{
		anim = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!((Component)col).tag.Equals("Monkey"))
		{
			return;
		}
		anim.Play("BarrelBoom");
		eksplozija1.Play();
		eksplozija2.Play();
		eksplozija3.Play();
		eksplozija4.Play();
		razbijenoBure = true;
		if (!((Object)this).name.Contains("TNT"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Bure_Eksplozija();
			}
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
			Manage.barrelsSmashed++;
			MissionManager.Instance.BarrelEvent(Manage.barrelsSmashed);
			Manage.points += 20;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			int num = Random.Range(5, 11);
			TextMesh component = ((Component)((Component)coinsReward).transform.Find("+3Coins")).GetComponent<TextMesh>();
			string text2 = (((Component)((Component)coinsReward).transform.Find("+3Coins/+3CoinsShadow")).GetComponent<TextMesh>().text = "+" + num);
			component.text = text2;
			coinsReward.Play("FadeOutCoins");
			Manage.coinsCollected += num;
			MissionManager.Instance.CoinEvent(Manage.coinsCollected);
			Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
			((Component)Manage.Instance.coinsCollectedText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		else if (PlaySounds.soundOn)
		{
			PlaySounds.Play_TNTBure_Eksplozija();
		}
	}

	public void ObnoviBure()
	{
		if (razbijenoBure)
		{
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = true;
			anim.SetTrigger("ObnoviBure");
			razbijenoBure = false;
			((Component)((Component)this).transform.GetChild(0).Find("Barrel")).gameObject.SetActive(true);
			((Component)((Component)this).transform.GetChild(0).Find("BarrelBrokenBottom")).gameObject.SetActive(false);
		}
	}
}
