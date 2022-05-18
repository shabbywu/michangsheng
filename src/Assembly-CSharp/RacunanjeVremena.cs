using System;
using System.Globalization;
using UnityEngine;

// Token: 0x0200078E RID: 1934
public class RacunanjeVremena : MonoBehaviour
{
	// Token: 0x0600315A RID: 12634 RVA: 0x0018948C File Offset: 0x0018768C
	private void Awake()
	{
		this.format = CultureInfo.CurrentCulture.DateTimeFormat;
		if (PlayerPrefs.HasKey("VremeQuit"))
		{
			this.ProveriVreme = PlayerPrefs.GetInt("ProveriVreme");
			this.VremeQuitString = PlayerPrefs.GetString("VremeQuit");
			this.VremeQuitDateTime = DateTime.Parse(this.VremeQuitString);
			this.VremeResumeString = DateTime.Now.ToString(this.format.FullDateTimePattern);
			this.VremeResumeDateTime = DateTime.Parse(this.VremeResumeString);
			RacunanjeVremena.Vreme = this.VremeResumeDateTime.Subtract(this.VremeQuitDateTime).ToString();
			string[] array = RacunanjeVremena.Vreme.Split(new char[]
			{
				':'
			});
			string[] array2 = array[0].Split(new char[]
			{
				'.'
			});
			int num = array2.Length;
			Debug.Log("duzina:" + num);
			if (num == 2)
			{
				int num2 = int.Parse(array2[0]) * 24 + int.Parse(array2[num - 1]);
				if (num2 < 0)
				{
					num2 = Mathf.Abs(num2);
				}
				this.sati = num2.ToString();
				Debug.Log("UkupnoSek sati posle konverzije iz dana:" + this.sati);
			}
			else
			{
				this.sati = array2[num - 1];
			}
			this.minuti = array[1];
			this.sekunde = array[2];
			this.satiInt = int.Parse(this.sati);
			this.minutiInt = int.Parse(this.minuti);
			this.sekundeInt = int.Parse(this.sekunde);
			RacunanjeVremena.UkupnoSekundi = this.sekundeInt + this.minutiInt * 60 + this.satiInt * 3600;
			this.UkupnoSek = RacunanjeVremena.UkupnoSekundi.ToString();
			Debug.Log("Proslo je ukupno: " + this.UkupnoSek);
			return;
		}
		string text = DateTime.Now.ToString(this.format.FullDateTimePattern);
		PlayerPrefs.SetString("VremeQuit", text);
		PlayerPrefs.SetInt("ProveriVreme", 0);
		PlayerPrefs.Save();
		RacunanjeVremena.UkupnoSekundi = 0;
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x001896A4 File Offset: 0x001878A4
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			this.VremeQuitString = DateTime.Now.ToString(this.format.FullDateTimePattern);
			PlayerPrefs.SetString("VremeQuit", this.VremeQuitString);
			PlayerPrefs.SetInt("ProveriVreme", 1);
			PlayerPrefs.Save();
			return;
		}
		this.ProveriVreme = PlayerPrefs.GetInt("ProveriVreme");
		if (this.ProveriVreme == 1)
		{
			this.format = CultureInfo.CurrentCulture.DateTimeFormat;
			this.VremeResumeString = DateTime.Now.ToString(this.format.FullDateTimePattern);
			this.VremeResumeDateTime = DateTime.Parse(this.VremeResumeString);
			this.VremeQuitString = PlayerPrefs.GetString("VremeQuit");
			this.VremeQuitDateTime = DateTime.Parse(this.VremeQuitString);
			TimeSpan timeSpan = this.VremeResumeDateTime.Subtract(this.VremeQuitDateTime);
			Debug.Log("Duration: " + timeSpan);
			RacunanjeVremena.Vreme = timeSpan.ToString();
			string[] array = RacunanjeVremena.Vreme.Split(new char[]
			{
				':'
			});
			string[] array2 = array[0].Split(new char[]
			{
				'.'
			});
			int num = array2.Length;
			Debug.Log("duzina:" + num);
			if (num == 2)
			{
				int num2 = int.Parse(array2[0]) * 24 + int.Parse(array2[num - 1]);
				if (num2 < 0)
				{
					num2 = Mathf.Abs(num2);
				}
				this.sati = num2.ToString();
				Debug.Log("UkupnoSek sati posle konverzije iz dana:" + this.sati);
			}
			else
			{
				this.sati = array2[num - 1];
			}
			this.minuti = array[1];
			this.sekunde = array[2];
			this.satiInt = int.Parse(this.sati);
			this.minutiInt = int.Parse(this.minuti);
			this.sekundeInt = int.Parse(this.sekunde);
			RacunanjeVremena.UkupnoSekundi = this.sekundeInt + this.minutiInt * 60 + this.satiInt * 3600;
			this.UkupnoSek = RacunanjeVremena.UkupnoSekundi.ToString();
			Debug.Log(string.Concat(new string[]
			{
				"Sati :",
				this.sati,
				" Minuti: ",
				this.minuti,
				" Sekunde: ",
				this.sekunde
			}));
			return;
		}
		Debug.Log("Proveri vreme je 0");
	}

	// Token: 0x0600315C RID: 12636 RVA: 0x00189910 File Offset: 0x00187B10
	private void OnApplicationQuit()
	{
		this.VremeQuitString = DateTime.Now.ToString(this.format.FullDateTimePattern);
		PlayerPrefs.SetString("VremeQuit", this.VremeQuitString);
		PlayerPrefs.SetInt("ProveriVreme", 1);
		PlayerPrefs.Save();
	}

	// Token: 0x04002D67 RID: 11623
	private DateTime VremeQuitDateTime;

	// Token: 0x04002D68 RID: 11624
	private DateTime VremeResumeDateTime;

	// Token: 0x04002D69 RID: 11625
	private string VremeQuitString;

	// Token: 0x04002D6A RID: 11626
	private string VremeResumeString;

	// Token: 0x04002D6B RID: 11627
	public static string Vreme;

	// Token: 0x04002D6C RID: 11628
	private int ProveriVreme;

	// Token: 0x04002D6D RID: 11629
	private string sati;

	// Token: 0x04002D6E RID: 11630
	private string minuti;

	// Token: 0x04002D6F RID: 11631
	private string sekunde;

	// Token: 0x04002D70 RID: 11632
	private string UkupnoSek;

	// Token: 0x04002D71 RID: 11633
	private int satiInt;

	// Token: 0x04002D72 RID: 11634
	private int minutiInt;

	// Token: 0x04002D73 RID: 11635
	private int sekundeInt;

	// Token: 0x04002D74 RID: 11636
	public static int UkupnoSekundi;

	// Token: 0x04002D75 RID: 11637
	private DateTimeFormatInfo format;
}
