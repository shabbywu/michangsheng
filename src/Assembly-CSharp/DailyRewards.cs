using System;
using System.Globalization;
using UnityEngine;

// Token: 0x02000685 RID: 1669
public class DailyRewards : MonoBehaviour
{
	// Token: 0x060029B5 RID: 10677 RVA: 0x00143294 File Offset: 0x00141494
	private void Start()
	{
		DateTime now = DateTime.Now;
		this.danUlaska = now.Day.ToString();
		this.mesecUlaska = now.Month.ToString();
		this.godinaUlaska = now.Year.ToString();
		Debug.Log(string.Concat(new object[]
		{
			"Trenutno vreme je: ",
			now,
			" danUlaska: ",
			this.danUlaska,
			" mesecUlaska: ",
			this.mesecUlaska
		}));
		if (PlayerPrefs.HasKey("LevelReward"))
		{
			DailyRewards.LevelReward = PlayerPrefs.GetInt("LevelReward");
			Debug.Log("Ppokretanje. Lewel je: " + DailyRewards.LevelReward);
		}
		else
		{
			DailyRewards.LevelReward = 0;
			Debug.Log("Prvo pokretanje. Lewel je: " + DailyRewards.LevelReward);
		}
		if (!PlayerPrefs.HasKey("VremeQuit"))
		{
			Debug.Log("PrvoPokretanje");
			DailyRewards.LevelReward = 0;
			PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
			PlayerPrefs.Save();
			return;
		}
		this.lastPlayDate = PlayerPrefs.GetString("VremeQuit");
		this.VremeIzlaska = DateTime.Parse(this.lastPlayDate);
		this.danIzlaska = this.VremeIzlaska.Day.ToString();
		this.mesecIzlaska = this.VremeIzlaska.Month.ToString();
		this.godinaIzlaska = this.VremeIzlaska.Year.ToString();
		Debug.Log(string.Concat(new string[]
		{
			"Vreme izlaska je: ",
			this.lastPlayDate,
			" danIzlaska: ",
			this.danIzlaska,
			" mesecIzlaska: ",
			this.mesecIzlaska
		}));
		Debug.Log("Razlika je " + (int.Parse(this.danUlaska) - int.Parse(this.danIzlaska)));
		if (int.Parse(this.godinaUlaska) - int.Parse(this.godinaIzlaska) < 1)
		{
			Debug.Log("Ista godina");
			if (int.Parse(this.mesecUlaska) - int.Parse(this.mesecIzlaska) == 0)
			{
				Debug.Log("Isti mesec: " + (int.Parse(this.mesecUlaska) - int.Parse(this.mesecIzlaska)));
				if (int.Parse(this.danUlaska) - int.Parse(this.danIzlaska) > 1)
				{
					Debug.Log("Resetuj rewards, isti mesec je samo sto je proslo vise od 2 dana");
					DailyRewards.LevelReward = 1;
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
					PlayerPrefs.Save();
					this.PrikaziDailyReward(DailyRewards.LevelReward);
					return;
				}
				if (int.Parse(this.danUlaska) - int.Parse(this.danIzlaska) <= 0)
				{
					Debug.Log("Nije novi dan jos uvek");
					return;
				}
				if (DailyRewards.LevelReward < 6)
				{
					Debug.Log("Stari LevelReward je :" + DailyRewards.LevelReward);
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					DailyRewards.LevelReward++;
					Debug.Log("Dobio si nagradu! Novi dan je. Dan: " + DailyRewards.LevelReward);
					PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
					PlayerPrefs.Save();
					Debug.Log("Novi LevelReward je :" + DailyRewards.LevelReward);
					this.PrikaziDailyReward(DailyRewards.LevelReward);
					return;
				}
				DailyRewards.LevelReward = 1;
				Debug.Log("Proslo 6 dana. Redovan bio ali resetuj :" + DailyRewards.LevelReward);
				GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
				PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
				PlayerPrefs.Save();
				this.PrikaziDailyReward(DailyRewards.LevelReward);
				return;
			}
			else
			{
				Debug.Log("Nije isti mesec");
				if (int.Parse(this.danUlaska) != 1)
				{
					Debug.Log("Resetuj Rewards, posto je novi mesec i razlika je vise od 2 dana");
					DailyRewards.LevelReward = 1;
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
					PlayerPrefs.Save();
					this.PrikaziDailyReward(DailyRewards.LevelReward);
					return;
				}
				if (int.Parse(this.mesecIzlaska) == 1 || int.Parse(this.mesecIzlaska) == 3 || int.Parse(this.mesecIzlaska) == 5 || int.Parse(this.mesecIzlaska) == 7 || int.Parse(this.mesecIzlaska) == 8 || int.Parse(this.mesecIzlaska) == 10 || int.Parse(this.mesecIzlaska) == 12)
				{
					Debug.Log("Prethodni Mesec ima 31 dan");
					if (int.Parse(this.danIzlaska) != 31)
					{
						Debug.Log("Prelazak iz meseca u mesec. Resetuj Rewards, proslo vise od 2 dana");
						DailyRewards.LevelReward = 1;
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
						PlayerPrefs.Save();
						this.PrikaziDailyReward(DailyRewards.LevelReward);
						return;
					}
					if (DailyRewards.LevelReward < 6)
					{
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						DailyRewards.LevelReward++;
						Debug.Log("Dobio si nagradu! Prelazak iz meseca u mesec:" + DailyRewards.LevelReward);
						PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
						PlayerPrefs.Save();
						this.PrikaziDailyReward(DailyRewards.LevelReward);
						return;
					}
					DailyRewards.LevelReward = 1;
					Debug.Log("Dobio si nagradu! Prelazak iz meseca u mesec i proslo 6 dana:" + DailyRewards.LevelReward);
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
					PlayerPrefs.Save();
					this.PrikaziDailyReward(DailyRewards.LevelReward);
					return;
				}
				else if (int.Parse(this.mesecIzlaska) == 4 || int.Parse(this.mesecIzlaska) == 6 || int.Parse(this.mesecIzlaska) == 9 || int.Parse(this.mesecIzlaska) == 11)
				{
					Debug.Log("Prethodni Mesec ima 30 dan");
					if (int.Parse(this.danIzlaska) != 30)
					{
						Debug.Log("Prelazak iz meseca u mesec. Resetuj Rewards, proslo vise od 2 dana");
						DailyRewards.LevelReward = 1;
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
						PlayerPrefs.Save();
						this.PrikaziDailyReward(DailyRewards.LevelReward);
						return;
					}
					Debug.Log("Dobio si nagradu! Prelazak iz meseca u mesec.");
					if (DailyRewards.LevelReward < 6)
					{
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						DailyRewards.LevelReward++;
						PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
						PlayerPrefs.Save();
						this.PrikaziDailyReward(DailyRewards.LevelReward);
						return;
					}
					DailyRewards.LevelReward = 1;
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
					PlayerPrefs.Save();
					this.PrikaziDailyReward(DailyRewards.LevelReward);
					return;
				}
				else
				{
					Debug.Log("Mesec je Februar");
					if (int.Parse(this.danIzlaska) <= 27)
					{
						Debug.Log("Prelazak iz meseca u mesec. Resetuj Rewards, proslo vise od 2 dana");
						DailyRewards.LevelReward = 1;
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
						PlayerPrefs.Save();
						this.PrikaziDailyReward(DailyRewards.LevelReward);
						return;
					}
					Debug.Log("Dobio si nagradu! Prelazak iz Februara u Mart.");
					if (DailyRewards.LevelReward < 6)
					{
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						DailyRewards.LevelReward++;
						PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
						PlayerPrefs.Save();
						this.PrikaziDailyReward(DailyRewards.LevelReward);
						return;
					}
					DailyRewards.LevelReward = 1;
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
					PlayerPrefs.Save();
					this.PrikaziDailyReward(DailyRewards.LevelReward);
					return;
				}
			}
		}
		else
		{
			Debug.Log("Nije ista godina");
			if (int.Parse(this.danIzlaska) != 31 || int.Parse(this.danUlaska) != 1)
			{
				Debug.Log("Resetuj Daily Rewards, nije bio redovan zbog Nove godine");
				DailyRewards.LevelReward = 1;
				GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
				PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
				PlayerPrefs.Save();
				this.PrikaziDailyReward(DailyRewards.LevelReward);
				return;
			}
			Debug.Log("Prvi dan u Novoj Godini, i izasao iz app 31. decembra, dobija Daily Rewards");
			if (DailyRewards.LevelReward < 6)
			{
				GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
				DailyRewards.LevelReward++;
				PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
				PlayerPrefs.Save();
				this.PrikaziDailyReward(DailyRewards.LevelReward);
				return;
			}
			DailyRewards.LevelReward = 1;
			GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
			PlayerPrefs.SetInt("LevelReward", DailyRewards.LevelReward);
			PlayerPrefs.Save();
			this.PrikaziDailyReward(DailyRewards.LevelReward);
			return;
		}
	}

	// Token: 0x060029B6 RID: 10678 RVA: 0x00143BB4 File Offset: 0x00141DB4
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			this.VremeQuitString = DateTime.Now.ToString();
			PlayerPrefs.SetString("VremeQuit", this.VremeQuitString);
			PlayerPrefs.Save();
			return;
		}
		Debug.Log("Usao nazad u aplikaciju iz Pause");
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x00143BF8 File Offset: 0x00141DF8
	private void PrikaziDailyReward(int TrenutniDan)
	{
		Debug.Log("Trenutni dan nagrade je: " + TrenutniDan);
		GameObject.Find("SpotLight").GetComponent<Animation>().Play("DailyReward" + TrenutniDan);
		GameObject gameObject;
		if (TrenutniDan < 6)
		{
			gameObject = GameObject.Find("Day " + TrenutniDan.ToString());
		}
		else
		{
			gameObject = GameObject.Find("Day 6 - Magic Box");
		}
		gameObject.transform.GetChild(0).GetComponent<Animator>().Play("CollectDailyRewardTab");
		gameObject.transform.GetChild(0).Find("DailyRewardParticlesIdle").GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x00143CA4 File Offset: 0x00141EA4
	private void OnApplicationQuit()
	{
		this.VremeQuitString = DateTime.Now.ToString();
		PlayerPrefs.SetString("VremeQuit", this.VremeQuitString);
		PlayerPrefs.Save();
	}

	// Token: 0x04002359 RID: 9049
	public static int[] DailyRewardAmount = new int[]
	{
		100,
		200,
		300,
		400,
		500,
		600
	};

	// Token: 0x0400235A RID: 9050
	private int OneDayTime = 86400;

	// Token: 0x0400235B RID: 9051
	public static int LevelReward;

	// Token: 0x0400235C RID: 9052
	private DateTimeFormatInfo format;

	// Token: 0x0400235D RID: 9053
	private DateTime VremePokretanjaDateTime;

	// Token: 0x0400235E RID: 9054
	private DateTime VremeIzlaska;

	// Token: 0x0400235F RID: 9055
	private string VremePokretanja;

	// Token: 0x04002360 RID: 9056
	private string lastPlayDate;

	// Token: 0x04002361 RID: 9057
	private string VremeQuitString;

	// Token: 0x04002362 RID: 9058
	private string Vreme;

	// Token: 0x04002363 RID: 9059
	private string danUlaska;

	// Token: 0x04002364 RID: 9060
	private string mesecUlaska;

	// Token: 0x04002365 RID: 9061
	private string godinaUlaska;

	// Token: 0x04002366 RID: 9062
	private string danIzlaska;

	// Token: 0x04002367 RID: 9063
	private string mesecIzlaska;

	// Token: 0x04002368 RID: 9064
	private string godinaIzlaska;
}
