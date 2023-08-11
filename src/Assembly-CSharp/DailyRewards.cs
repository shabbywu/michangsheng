using System;
using System.Globalization;
using UnityEngine;

public class DailyRewards : MonoBehaviour
{
	public static int[] DailyRewardAmount = new int[6] { 100, 200, 300, 400, 500, 600 };

	private int OneDayTime = 86400;

	public static int LevelReward;

	private DateTimeFormatInfo format;

	private DateTime VremePokretanjaDateTime;

	private DateTime VremeIzlaska;

	private string VremePokretanja;

	private string lastPlayDate;

	private string VremeQuitString;

	private string Vreme;

	private string danUlaska;

	private string mesecUlaska;

	private string godinaUlaska;

	private string danIzlaska;

	private string mesecIzlaska;

	private string godinaIzlaska;

	private void Start()
	{
		DateTime now = DateTime.Now;
		danUlaska = now.Day.ToString();
		mesecUlaska = now.Month.ToString();
		godinaUlaska = now.Year.ToString();
		Debug.Log((object)string.Concat("Trenutno vreme je: ", now, " danUlaska: ", danUlaska, " mesecUlaska: ", mesecUlaska));
		if (PlayerPrefs.HasKey("LevelReward"))
		{
			LevelReward = PlayerPrefs.GetInt("LevelReward");
			Debug.Log((object)("Ppokretanje. Lewel je: " + LevelReward));
		}
		else
		{
			LevelReward = 0;
			Debug.Log((object)("Prvo pokretanje. Lewel je: " + LevelReward));
		}
		if (PlayerPrefs.HasKey("VremeQuit"))
		{
			lastPlayDate = PlayerPrefs.GetString("VremeQuit");
			VremeIzlaska = DateTime.Parse(lastPlayDate);
			danIzlaska = VremeIzlaska.Day.ToString();
			mesecIzlaska = VremeIzlaska.Month.ToString();
			godinaIzlaska = VremeIzlaska.Year.ToString();
			Debug.Log((object)("Vreme izlaska je: " + lastPlayDate + " danIzlaska: " + danIzlaska + " mesecIzlaska: " + mesecIzlaska));
			Debug.Log((object)("Razlika je " + (int.Parse(danUlaska) - int.Parse(danIzlaska))));
			if (int.Parse(godinaUlaska) - int.Parse(godinaIzlaska) < 1)
			{
				Debug.Log((object)"Ista godina");
				if (int.Parse(mesecUlaska) - int.Parse(mesecIzlaska) == 0)
				{
					Debug.Log((object)("Isti mesec: " + (int.Parse(mesecUlaska) - int.Parse(mesecIzlaska))));
					if (int.Parse(danUlaska) - int.Parse(danIzlaska) > 1)
					{
						Debug.Log((object)"Resetuj rewards, isti mesec je samo sto je proslo vise od 2 dana");
						LevelReward = 1;
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						PlayerPrefs.SetInt("LevelReward", LevelReward);
						PlayerPrefs.Save();
						PrikaziDailyReward(LevelReward);
					}
					else if (int.Parse(danUlaska) - int.Parse(danIzlaska) > 0)
					{
						if (LevelReward < 6)
						{
							Debug.Log((object)("Stari LevelReward je :" + LevelReward));
							GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
							LevelReward++;
							Debug.Log((object)("Dobio si nagradu! Novi dan je. Dan: " + LevelReward));
							PlayerPrefs.SetInt("LevelReward", LevelReward);
							PlayerPrefs.Save();
							Debug.Log((object)("Novi LevelReward je :" + LevelReward));
							PrikaziDailyReward(LevelReward);
						}
						else
						{
							LevelReward = 1;
							Debug.Log((object)("Proslo 6 dana. Redovan bio ali resetuj :" + LevelReward));
							GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
							PlayerPrefs.SetInt("LevelReward", LevelReward);
							PlayerPrefs.Save();
							PrikaziDailyReward(LevelReward);
						}
					}
					else
					{
						Debug.Log((object)"Nije novi dan jos uvek");
					}
					return;
				}
				Debug.Log((object)"Nije isti mesec");
				if (int.Parse(danUlaska) == 1)
				{
					if (int.Parse(mesecIzlaska) == 1 || int.Parse(mesecIzlaska) == 3 || int.Parse(mesecIzlaska) == 5 || int.Parse(mesecIzlaska) == 7 || int.Parse(mesecIzlaska) == 8 || int.Parse(mesecIzlaska) == 10 || int.Parse(mesecIzlaska) == 12)
					{
						Debug.Log((object)"Prethodni Mesec ima 31 dan");
						if (int.Parse(danIzlaska) == 31)
						{
							if (LevelReward < 6)
							{
								GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
								LevelReward++;
								Debug.Log((object)("Dobio si nagradu! Prelazak iz meseca u mesec:" + LevelReward));
								PlayerPrefs.SetInt("LevelReward", LevelReward);
								PlayerPrefs.Save();
								PrikaziDailyReward(LevelReward);
							}
							else
							{
								LevelReward = 1;
								Debug.Log((object)("Dobio si nagradu! Prelazak iz meseca u mesec i proslo 6 dana:" + LevelReward));
								GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
								PlayerPrefs.SetInt("LevelReward", LevelReward);
								PlayerPrefs.Save();
								PrikaziDailyReward(LevelReward);
							}
						}
						else
						{
							Debug.Log((object)"Prelazak iz meseca u mesec. Resetuj Rewards, proslo vise od 2 dana");
							LevelReward = 1;
							GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
							PlayerPrefs.SetInt("LevelReward", LevelReward);
							PlayerPrefs.Save();
							PrikaziDailyReward(LevelReward);
						}
						return;
					}
					if (int.Parse(mesecIzlaska) == 4 || int.Parse(mesecIzlaska) == 6 || int.Parse(mesecIzlaska) == 9 || int.Parse(mesecIzlaska) == 11)
					{
						Debug.Log((object)"Prethodni Mesec ima 30 dan");
						if (int.Parse(danIzlaska) == 30)
						{
							Debug.Log((object)"Dobio si nagradu! Prelazak iz meseca u mesec.");
							if (LevelReward < 6)
							{
								GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
								LevelReward++;
								PlayerPrefs.SetInt("LevelReward", LevelReward);
								PlayerPrefs.Save();
								PrikaziDailyReward(LevelReward);
							}
							else
							{
								LevelReward = 1;
								GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
								PlayerPrefs.SetInt("LevelReward", LevelReward);
								PlayerPrefs.Save();
								PrikaziDailyReward(LevelReward);
							}
						}
						else
						{
							Debug.Log((object)"Prelazak iz meseca u mesec. Resetuj Rewards, proslo vise od 2 dana");
							LevelReward = 1;
							GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
							PlayerPrefs.SetInt("LevelReward", LevelReward);
							PlayerPrefs.Save();
							PrikaziDailyReward(LevelReward);
						}
						return;
					}
					Debug.Log((object)"Mesec je Februar");
					if (int.Parse(danIzlaska) > 27)
					{
						Debug.Log((object)"Dobio si nagradu! Prelazak iz Februara u Mart.");
						if (LevelReward < 6)
						{
							GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
							LevelReward++;
							PlayerPrefs.SetInt("LevelReward", LevelReward);
							PlayerPrefs.Save();
							PrikaziDailyReward(LevelReward);
						}
						else
						{
							LevelReward = 1;
							GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
							PlayerPrefs.SetInt("LevelReward", LevelReward);
							PlayerPrefs.Save();
							PrikaziDailyReward(LevelReward);
						}
					}
					else
					{
						Debug.Log((object)"Prelazak iz meseca u mesec. Resetuj Rewards, proslo vise od 2 dana");
						LevelReward = 1;
						GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
						PlayerPrefs.SetInt("LevelReward", LevelReward);
						PlayerPrefs.Save();
						PrikaziDailyReward(LevelReward);
					}
				}
				else
				{
					Debug.Log((object)"Resetuj Rewards, posto je novi mesec i razlika je vise od 2 dana");
					LevelReward = 1;
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", LevelReward);
					PlayerPrefs.Save();
					PrikaziDailyReward(LevelReward);
				}
				return;
			}
			Debug.Log((object)"Nije ista godina");
			if (int.Parse(danIzlaska) == 31 && int.Parse(danUlaska) == 1)
			{
				Debug.Log((object)"Prvi dan u Novoj Godini, i izasao iz app 31. decembra, dobija Daily Rewards");
				if (LevelReward < 6)
				{
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					LevelReward++;
					PlayerPrefs.SetInt("LevelReward", LevelReward);
					PlayerPrefs.Save();
					PrikaziDailyReward(LevelReward);
				}
				else
				{
					LevelReward = 1;
					GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
					PlayerPrefs.SetInt("LevelReward", LevelReward);
					PlayerPrefs.Save();
					PrikaziDailyReward(LevelReward);
				}
			}
			else
			{
				Debug.Log((object)"Resetuj Daily Rewards, nije bio redovan zbog Nove godine");
				LevelReward = 1;
				GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyDolazak");
				PlayerPrefs.SetInt("LevelReward", LevelReward);
				PlayerPrefs.Save();
				PrikaziDailyReward(LevelReward);
			}
		}
		else
		{
			Debug.Log((object)"PrvoPokretanje");
			LevelReward = 0;
			PlayerPrefs.SetInt("LevelReward", LevelReward);
			PlayerPrefs.Save();
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			VremeQuitString = DateTime.Now.ToString();
			PlayerPrefs.SetString("VremeQuit", VremeQuitString);
			PlayerPrefs.Save();
		}
		else
		{
			Debug.Log((object)"Usao nazad u aplikaciju iz Pause");
		}
	}

	private void PrikaziDailyReward(int TrenutniDan)
	{
		Debug.Log((object)("Trenutni dan nagrade je: " + TrenutniDan));
		GameObject.Find("SpotLight").GetComponent<Animation>().Play("DailyReward" + TrenutniDan);
		GameObject val = ((TrenutniDan >= 6) ? GameObject.Find("Day 6 - Magic Box") : GameObject.Find("Day " + TrenutniDan));
		((Component)val.transform.GetChild(0)).GetComponent<Animator>().Play("CollectDailyRewardTab");
		((Component)val.transform.GetChild(0).Find("DailyRewardParticlesIdle")).GetComponent<ParticleSystem>().Play();
	}

	private void OnApplicationQuit()
	{
		VremeQuitString = DateTime.Now.ToString();
		PlayerPrefs.SetString("VremeQuit", VremeQuitString);
		PlayerPrefs.Save();
	}
}
