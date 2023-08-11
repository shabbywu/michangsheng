using System;
using System.Globalization;
using UnityEngine;

public class RacunanjeVremena : MonoBehaviour
{
	private DateTime VremeQuitDateTime;

	private DateTime VremeResumeDateTime;

	private string VremeQuitString;

	private string VremeResumeString;

	public static string Vreme;

	private int ProveriVreme;

	private string sati;

	private string minuti;

	private string sekunde;

	private string UkupnoSek;

	private int satiInt;

	private int minutiInt;

	private int sekundeInt;

	public static int UkupnoSekundi;

	private DateTimeFormatInfo format;

	private void Awake()
	{
		format = CultureInfo.CurrentCulture.DateTimeFormat;
		if (PlayerPrefs.HasKey("VremeQuit"))
		{
			ProveriVreme = PlayerPrefs.GetInt("ProveriVreme");
			VremeQuitString = PlayerPrefs.GetString("VremeQuit");
			VremeQuitDateTime = DateTime.Parse(VremeQuitString);
			VremeResumeString = DateTime.Now.ToString(format.FullDateTimePattern);
			VremeResumeDateTime = DateTime.Parse(VremeResumeString);
			Vreme = VremeResumeDateTime.Subtract(VremeQuitDateTime).ToString();
			string[] array = Vreme.Split(new char[1] { ':' });
			string[] array2 = array[0].Split(new char[1] { '.' });
			int num = array2.Length;
			Debug.Log((object)("duzina:" + num));
			if (num == 2)
			{
				int num2 = int.Parse(array2[0]) * 24 + int.Parse(array2[num - 1]);
				if (num2 < 0)
				{
					num2 = Mathf.Abs(num2);
				}
				sati = num2.ToString();
				Debug.Log((object)("UkupnoSek sati posle konverzije iz dana:" + sati));
			}
			else
			{
				sati = array2[num - 1];
			}
			minuti = array[1];
			sekunde = array[2];
			satiInt = int.Parse(sati);
			minutiInt = int.Parse(minuti);
			sekundeInt = int.Parse(sekunde);
			UkupnoSekundi = sekundeInt + minutiInt * 60 + satiInt * 3600;
			UkupnoSek = UkupnoSekundi.ToString();
			Debug.Log((object)("Proslo je ukupno: " + UkupnoSek));
		}
		else
		{
			string text = DateTime.Now.ToString(format.FullDateTimePattern);
			PlayerPrefs.SetString("VremeQuit", text);
			PlayerPrefs.SetInt("ProveriVreme", 0);
			PlayerPrefs.Save();
			UkupnoSekundi = 0;
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			VremeQuitString = DateTime.Now.ToString(format.FullDateTimePattern);
			PlayerPrefs.SetString("VremeQuit", VremeQuitString);
			PlayerPrefs.SetInt("ProveriVreme", 1);
			PlayerPrefs.Save();
			return;
		}
		ProveriVreme = PlayerPrefs.GetInt("ProveriVreme");
		if (ProveriVreme == 1)
		{
			format = CultureInfo.CurrentCulture.DateTimeFormat;
			VremeResumeString = DateTime.Now.ToString(format.FullDateTimePattern);
			VremeResumeDateTime = DateTime.Parse(VremeResumeString);
			VremeQuitString = PlayerPrefs.GetString("VremeQuit");
			VremeQuitDateTime = DateTime.Parse(VremeQuitString);
			TimeSpan timeSpan = VremeResumeDateTime.Subtract(VremeQuitDateTime);
			Debug.Log((object)("Duration: " + timeSpan));
			Vreme = timeSpan.ToString();
			string[] array = Vreme.Split(new char[1] { ':' });
			string[] array2 = array[0].Split(new char[1] { '.' });
			int num = array2.Length;
			Debug.Log((object)("duzina:" + num));
			if (num == 2)
			{
				int num2 = int.Parse(array2[0]) * 24 + int.Parse(array2[num - 1]);
				if (num2 < 0)
				{
					num2 = Mathf.Abs(num2);
				}
				sati = num2.ToString();
				Debug.Log((object)("UkupnoSek sati posle konverzije iz dana:" + sati));
			}
			else
			{
				sati = array2[num - 1];
			}
			minuti = array[1];
			sekunde = array[2];
			satiInt = int.Parse(sati);
			minutiInt = int.Parse(minuti);
			sekundeInt = int.Parse(sekunde);
			UkupnoSekundi = sekundeInt + minutiInt * 60 + satiInt * 3600;
			UkupnoSek = UkupnoSekundi.ToString();
			Debug.Log((object)("Sati :" + sati + " Minuti: " + minuti + " Sekunde: " + sekunde));
		}
		else
		{
			Debug.Log((object)"Proveri vreme je 0");
		}
	}

	private void OnApplicationQuit()
	{
		VremeQuitString = DateTime.Now.ToString(format.FullDateTimePattern);
		PlayerPrefs.SetString("VremeQuit", VremeQuitString);
		PlayerPrefs.SetInt("ProveriVreme", 1);
		PlayerPrefs.Save();
	}
}
