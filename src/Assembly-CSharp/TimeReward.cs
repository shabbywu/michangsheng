using System;
using System.Globalization;
using UnityEngine;

// Token: 0x0200078F RID: 1935
public class TimeReward : MonoBehaviour
{
	// Token: 0x0600315E RID: 12638 RVA: 0x0018995C File Offset: 0x00187B5C
	private void Start()
	{
		this.Kovceg = GameObject.Find("Kovceg");
		if (!PlayerPrefs.HasKey("VremeBrojaca"))
		{
			TimeReward.VremeBrojaca = 10800f;
			this.Kovceg.transform.Find("Text/Coin Number").gameObject.SetActive(false);
			this.Kovceg.transform.Find("Text/Coin").gameObject.SetActive(false);
			this.Kovceg.transform.Find("Novcici").gameObject.SetActive(false);
			this.Kovceg.GetComponent<Collider>().enabled = false;
			return;
		}
		TimeReward.VremeBrojaca = PlayerPrefs.GetFloat("VremeBrojaca");
		this.VremeZaOduzimanje = (float)RacunanjeVremena.UkupnoSekundi;
		if (this.VremeZaOduzimanje > 10798f)
		{
			TimeReward.VremeBrojaca = 0f;
		}
		else
		{
			TimeReward.VremeBrojaca -= this.VremeZaOduzimanje;
		}
		if (TimeReward.VremeBrojaca < 1f)
		{
			TimeReward.PokupiTimeNagradu = true;
			this.Kovceg.GetComponent<Collider>().enabled = true;
			this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = LanguageManager.Collect;
			this.Kovceg.transform.Find("Novcici").gameObject.SetActive(true);
			this.Kovceg.GetComponent<Animator>().Play("Kovceg Collect Animation");
			return;
		}
		this.Kovceg.transform.Find("Text/Coin Number").gameObject.SetActive(false);
		this.Kovceg.transform.Find("Text/Coin").gameObject.SetActive(false);
		this.Kovceg.transform.Find("Novcici").gameObject.SetActive(false);
		this.Kovceg.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x000241F1 File Offset: 0x000223F1
	private void Update()
	{
		if (!TimeReward.PokupiTimeNagradu)
		{
			this.Odbrojavanje();
		}
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x00189B38 File Offset: 0x00187D38
	private void Odbrojavanje()
	{
		TimeReward.VremeBrojaca -= Time.deltaTime;
		this.Minuti = (int)TimeReward.VremeBrojaca / 60;
		this.Sekunde = (int)TimeReward.VremeBrojaca % 60;
		this.Sati = this.Minuti / 60;
		this.Minuti -= 60 * this.Sati;
		if (this.Sekunde < 1 && this.Minuti < 1 && this.Sati < 1)
		{
			this.Kovceg.GetComponent<Animator>().Play("Kovceg Collect Animation");
			this.Kovceg.GetComponent<Collider>().enabled = true;
			this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = LanguageManager.Collect;
			this.Kovceg.transform.Find("Text/Coin Number").gameObject.SetActive(true);
			this.Kovceg.transform.Find("Text/Coin").gameObject.SetActive(true);
			this.Kovceg.transform.Find("Novcici").gameObject.SetActive(true);
			TimeReward.PokupiTimeNagradu = true;
			return;
		}
		if (this.Sati > 0)
		{
			if (this.Sekunde >= 0 && this.Sekunde <= 9)
			{
				if (this.Minuti < 10)
				{
					this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = string.Concat(new string[]
					{
						"0",
						this.Sati.ToString(),
						":0",
						this.Minuti.ToString(),
						":0",
						this.Sekunde.ToString()
					});
					return;
				}
				this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = string.Concat(new string[]
				{
					"0",
					this.Sati.ToString(),
					":",
					this.Minuti.ToString(),
					":0",
					this.Sekunde.ToString()
				});
				return;
			}
			else
			{
				if (this.Minuti < 10)
				{
					this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = string.Concat(new string[]
					{
						"0",
						this.Sati.ToString(),
						":0",
						this.Minuti.ToString(),
						":",
						this.Sekunde.ToString()
					});
					return;
				}
				this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = string.Concat(new string[]
				{
					"0",
					this.Sati.ToString(),
					":",
					this.Minuti.ToString(),
					":",
					this.Sekunde.ToString()
				});
				return;
			}
		}
		else if (this.Sekunde >= 0 && this.Sekunde <= 9)
		{
			if (this.Minuti < 10)
			{
				this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = "00:0" + this.Minuti.ToString() + ":0" + this.Sekunde.ToString();
				return;
			}
			this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = "00:" + this.Minuti.ToString() + ":0" + this.Sekunde.ToString();
			return;
		}
		else
		{
			if (this.Minuti < 10)
			{
				this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = "00:0" + this.Minuti.ToString() + ":" + this.Sekunde.ToString();
				return;
			}
			this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = "00:" + this.Minuti.ToString() + ":" + this.Sekunde.ToString();
			return;
		}
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x00189F9C File Offset: 0x0018819C
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PlayerPrefs.SetFloat("VremeBrojaca", TimeReward.VremeBrojaca);
			PlayerPrefs.Save();
			return;
		}
		TimeReward.VremeBrojaca = PlayerPrefs.GetFloat("VremeBrojaca");
		this.VremeZaOduzimanje = (float)RacunanjeVremena.UkupnoSekundi;
		if (this.VremeZaOduzimanje > 10798f)
		{
			TimeReward.VremeBrojaca = 0f;
			TimeReward.PokupiTimeNagradu = true;
			this.Kovceg.GetComponent<Collider>().enabled = true;
			this.Kovceg.transform.Find("Text/Coin Number").gameObject.SetActive(true);
			this.Kovceg.transform.Find("Text/Collect").GetComponent<TextMesh>().text = LanguageManager.Collect;
			this.Kovceg.transform.Find("Novcici").gameObject.SetActive(true);
			this.Kovceg.GetComponent<Animator>().Play("Kovceg Collect Animation");
			return;
		}
		TimeReward.VremeBrojaca -= this.VremeZaOduzimanje;
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x0018A098 File Offset: 0x00188298
	public void PokupiNagradu()
	{
		if (TimeReward.PokupiTimeNagradu)
		{
			TimeReward.VremeBrojaca = 10800f;
			TimeReward.PokupiTimeNagradu = false;
			StagesParser.currentMoney += 100;
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
			base.Invoke("DelayZaOdbrojavanje", 1.5f);
			this.Kovceg.transform.Find("Text/Coin Number").gameObject.SetActive(false);
			this.Kovceg.transform.Find("Text/Coin").gameObject.SetActive(false);
			StagesParser.ServerUpdate = 1;
		}
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x00020B8C File Offset: 0x0001ED8C
	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
	}

	// Token: 0x06003164 RID: 12644 RVA: 0x00024200 File Offset: 0x00022400
	private void DelayZaOdbrojavanje()
	{
		base.StartCoroutine(StagesParser.Instance.moneyCounter(100, GameObject.Find("CoinsReward/Coins Number").GetComponent<TextMesh>(), true));
		base.Invoke("SkloniCoinsReward", 1.2f);
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x00024235 File Offset: 0x00022435
	private void OnApplicationQuit()
	{
		PlayerPrefs.SetFloat("VremeBrojaca", TimeReward.VremeBrojaca);
		PlayerPrefs.Save();
	}

	// Token: 0x04002D76 RID: 11638
	public static float VremeBrojaca;

	// Token: 0x04002D77 RID: 11639
	private float VremeZaOduzimanje;

	// Token: 0x04002D78 RID: 11640
	private int Minuti;

	// Token: 0x04002D79 RID: 11641
	private int Sekunde;

	// Token: 0x04002D7A RID: 11642
	private int Sati;

	// Token: 0x04002D7B RID: 11643
	private GameObject Kovceg;

	// Token: 0x04002D7C RID: 11644
	private DateTimeFormatInfo format;

	// Token: 0x04002D7D RID: 11645
	private DateTime VremePokretanjaDateTime;

	// Token: 0x04002D7E RID: 11646
	private DateTime VremeIzlaska;

	// Token: 0x04002D7F RID: 11647
	private string VremePokretanja;

	// Token: 0x04002D80 RID: 11648
	private string lastPlayDate;

	// Token: 0x04002D81 RID: 11649
	private string VremeQuitString;

	// Token: 0x04002D82 RID: 11650
	private string Vreme;

	// Token: 0x04002D83 RID: 11651
	private string danUlaska;

	// Token: 0x04002D84 RID: 11652
	private string mesecUlaska;

	// Token: 0x04002D85 RID: 11653
	private string godinaUlaska;

	// Token: 0x04002D86 RID: 11654
	private string danIzlaska;

	// Token: 0x04002D87 RID: 11655
	private string mesecIzlaska;

	// Token: 0x04002D88 RID: 11656
	private string godinaIzlaska;

	// Token: 0x04002D89 RID: 11657
	public static bool PokupiTimeNagradu;
}
