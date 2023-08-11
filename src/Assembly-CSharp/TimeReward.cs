using System;
using System.Globalization;
using UnityEngine;

public class TimeReward : MonoBehaviour
{
	public static float VremeBrojaca;

	private float VremeZaOduzimanje;

	private int Minuti;

	private int Sekunde;

	private int Sati;

	private GameObject Kovceg;

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

	public static bool PokupiTimeNagradu;

	private void Start()
	{
		Kovceg = GameObject.Find("Kovceg");
		if (PlayerPrefs.HasKey("VremeBrojaca"))
		{
			VremeBrojaca = PlayerPrefs.GetFloat("VremeBrojaca");
			VremeZaOduzimanje = RacunanjeVremena.UkupnoSekundi;
			if (VremeZaOduzimanje > 10798f)
			{
				VremeBrojaca = 0f;
			}
			else
			{
				VremeBrojaca -= VremeZaOduzimanje;
			}
			if (VremeBrojaca < 1f)
			{
				PokupiTimeNagradu = true;
				Kovceg.GetComponent<Collider>().enabled = true;
				((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = LanguageManager.Collect;
				((Component)Kovceg.transform.Find("Novcici")).gameObject.SetActive(true);
				Kovceg.GetComponent<Animator>().Play("Kovceg Collect Animation");
			}
			else
			{
				((Component)Kovceg.transform.Find("Text/Coin Number")).gameObject.SetActive(false);
				((Component)Kovceg.transform.Find("Text/Coin")).gameObject.SetActive(false);
				((Component)Kovceg.transform.Find("Novcici")).gameObject.SetActive(false);
				Kovceg.GetComponent<Collider>().enabled = false;
			}
		}
		else
		{
			VremeBrojaca = 10800f;
			((Component)Kovceg.transform.Find("Text/Coin Number")).gameObject.SetActive(false);
			((Component)Kovceg.transform.Find("Text/Coin")).gameObject.SetActive(false);
			((Component)Kovceg.transform.Find("Novcici")).gameObject.SetActive(false);
			Kovceg.GetComponent<Collider>().enabled = false;
		}
	}

	private void Update()
	{
		if (!PokupiTimeNagradu)
		{
			Odbrojavanje();
		}
	}

	private void Odbrojavanje()
	{
		VremeBrojaca -= Time.deltaTime;
		Minuti = (int)VremeBrojaca / 60;
		Sekunde = (int)VremeBrojaca % 60;
		Sati = Minuti / 60;
		Minuti -= 60 * Sati;
		if (Sekunde < 1 && Minuti < 1 && Sati < 1)
		{
			Kovceg.GetComponent<Animator>().Play("Kovceg Collect Animation");
			Kovceg.GetComponent<Collider>().enabled = true;
			((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = LanguageManager.Collect;
			((Component)Kovceg.transform.Find("Text/Coin Number")).gameObject.SetActive(true);
			((Component)Kovceg.transform.Find("Text/Coin")).gameObject.SetActive(true);
			((Component)Kovceg.transform.Find("Novcici")).gameObject.SetActive(true);
			PokupiTimeNagradu = true;
		}
		else if (Sati > 0)
		{
			if (Sekunde >= 0 && Sekunde <= 9)
			{
				if (Minuti < 10)
				{
					((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "0" + Sati + ":0" + Minuti + ":0" + Sekunde;
				}
				else
				{
					((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "0" + Sati + ":" + Minuti + ":0" + Sekunde;
				}
			}
			else if (Minuti < 10)
			{
				((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "0" + Sati + ":0" + Minuti + ":" + Sekunde;
			}
			else
			{
				((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "0" + Sati + ":" + Minuti + ":" + Sekunde;
			}
		}
		else if (Sekunde >= 0 && Sekunde <= 9)
		{
			if (Minuti < 10)
			{
				((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "00:0" + Minuti + ":0" + Sekunde;
			}
			else
			{
				((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "00:" + Minuti + ":0" + Sekunde;
			}
		}
		else if (Minuti < 10)
		{
			((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "00:0" + Minuti + ":" + Sekunde;
		}
		else
		{
			((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = "00:" + Minuti + ":" + Sekunde;
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PlayerPrefs.SetFloat("VremeBrojaca", VremeBrojaca);
			PlayerPrefs.Save();
			return;
		}
		VremeBrojaca = PlayerPrefs.GetFloat("VremeBrojaca");
		VremeZaOduzimanje = RacunanjeVremena.UkupnoSekundi;
		if (VremeZaOduzimanje > 10798f)
		{
			VremeBrojaca = 0f;
			PokupiTimeNagradu = true;
			Kovceg.GetComponent<Collider>().enabled = true;
			((Component)Kovceg.transform.Find("Text/Coin Number")).gameObject.SetActive(true);
			((Component)Kovceg.transform.Find("Text/Collect")).GetComponent<TextMesh>().text = LanguageManager.Collect;
			((Component)Kovceg.transform.Find("Novcici")).gameObject.SetActive(true);
			Kovceg.GetComponent<Animator>().Play("Kovceg Collect Animation");
		}
		else
		{
			VremeBrojaca -= VremeZaOduzimanje;
		}
	}

	public void PokupiNagradu()
	{
		if (PokupiTimeNagradu)
		{
			VremeBrojaca = 10800f;
			PokupiTimeNagradu = false;
			StagesParser.currentMoney += 100;
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
			((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.5f);
			((Component)Kovceg.transform.Find("Text/Coin Number")).gameObject.SetActive(false);
			((Component)Kovceg.transform.Find("Text/Coin")).gameObject.SetActive(false);
			StagesParser.ServerUpdate = 1;
		}
	}

	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
	}

	private void DelayZaOdbrojavanje()
	{
		((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(100, GameObject.Find("CoinsReward/Coins Number").GetComponent<TextMesh>(), hasOutline: true));
		((MonoBehaviour)this).Invoke("SkloniCoinsReward", 1.2f);
	}

	private void OnApplicationQuit()
	{
		PlayerPrefs.SetFloat("VremeBrojaca", VremeBrojaca);
		PlayerPrefs.Save();
	}
}
