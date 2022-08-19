using System;
using System.Globalization;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class TimeReward : MonoBehaviour
{
	// Token: 0x06002959 RID: 10585 RVA: 0x0013C420 File Offset: 0x0013A620
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

	// Token: 0x0600295A RID: 10586 RVA: 0x0013C5FB File Offset: 0x0013A7FB
	private void Update()
	{
		if (!TimeReward.PokupiTimeNagradu)
		{
			this.Odbrojavanje();
		}
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x0013C60C File Offset: 0x0013A80C
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

	// Token: 0x0600295C RID: 10588 RVA: 0x0013CA70 File Offset: 0x0013AC70
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

	// Token: 0x0600295D RID: 10589 RVA: 0x0013CB6C File Offset: 0x0013AD6C
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

	// Token: 0x0600295E RID: 10590 RVA: 0x0010621E File Offset: 0x0010441E
	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x0013CC0A File Offset: 0x0013AE0A
	private void DelayZaOdbrojavanje()
	{
		base.StartCoroutine(StagesParser.Instance.moneyCounter(100, GameObject.Find("CoinsReward/Coins Number").GetComponent<TextMesh>(), true));
		base.Invoke("SkloniCoinsReward", 1.2f);
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x0013CC3F File Offset: 0x0013AE3F
	private void OnApplicationQuit()
	{
		PlayerPrefs.SetFloat("VremeBrojaca", TimeReward.VremeBrojaca);
		PlayerPrefs.Save();
	}

	// Token: 0x0400258E RID: 9614
	public static float VremeBrojaca;

	// Token: 0x0400258F RID: 9615
	private float VremeZaOduzimanje;

	// Token: 0x04002590 RID: 9616
	private int Minuti;

	// Token: 0x04002591 RID: 9617
	private int Sekunde;

	// Token: 0x04002592 RID: 9618
	private int Sati;

	// Token: 0x04002593 RID: 9619
	private GameObject Kovceg;

	// Token: 0x04002594 RID: 9620
	private DateTimeFormatInfo format;

	// Token: 0x04002595 RID: 9621
	private DateTime VremePokretanjaDateTime;

	// Token: 0x04002596 RID: 9622
	private DateTime VremeIzlaska;

	// Token: 0x04002597 RID: 9623
	private string VremePokretanja;

	// Token: 0x04002598 RID: 9624
	private string lastPlayDate;

	// Token: 0x04002599 RID: 9625
	private string VremeQuitString;

	// Token: 0x0400259A RID: 9626
	private string Vreme;

	// Token: 0x0400259B RID: 9627
	private string danUlaska;

	// Token: 0x0400259C RID: 9628
	private string mesecUlaska;

	// Token: 0x0400259D RID: 9629
	private string godinaUlaska;

	// Token: 0x0400259E RID: 9630
	private string danIzlaska;

	// Token: 0x0400259F RID: 9631
	private string mesecIzlaska;

	// Token: 0x040025A0 RID: 9632
	private string godinaIzlaska;

	// Token: 0x040025A1 RID: 9633
	public static bool PokupiTimeNagradu;
}
