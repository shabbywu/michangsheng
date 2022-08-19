using System;
using System.Globalization;
using UnityEngine;

// Token: 0x020004FD RID: 1277
public class RacunanjeVremena : MonoBehaviour
{
	// Token: 0x06002955 RID: 10581 RVA: 0x0013BF50 File Offset: 0x0013A150
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

	// Token: 0x06002956 RID: 10582 RVA: 0x0013C168 File Offset: 0x0013A368
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

	// Token: 0x06002957 RID: 10583 RVA: 0x0013C3D4 File Offset: 0x0013A5D4
	private void OnApplicationQuit()
	{
		this.VremeQuitString = DateTime.Now.ToString(this.format.FullDateTimePattern);
		PlayerPrefs.SetString("VremeQuit", this.VremeQuitString);
		PlayerPrefs.SetInt("ProveriVreme", 1);
		PlayerPrefs.Save();
	}

	// Token: 0x0400257F RID: 9599
	private DateTime VremeQuitDateTime;

	// Token: 0x04002580 RID: 9600
	private DateTime VremeResumeDateTime;

	// Token: 0x04002581 RID: 9601
	private string VremeQuitString;

	// Token: 0x04002582 RID: 9602
	private string VremeResumeString;

	// Token: 0x04002583 RID: 9603
	public static string Vreme;

	// Token: 0x04002584 RID: 9604
	private int ProveriVreme;

	// Token: 0x04002585 RID: 9605
	private string sati;

	// Token: 0x04002586 RID: 9606
	private string minuti;

	// Token: 0x04002587 RID: 9607
	private string sekunde;

	// Token: 0x04002588 RID: 9608
	private string UkupnoSek;

	// Token: 0x04002589 RID: 9609
	private int satiInt;

	// Token: 0x0400258A RID: 9610
	private int minutiInt;

	// Token: 0x0400258B RID: 9611
	private int sekundeInt;

	// Token: 0x0400258C RID: 9612
	public static int UkupnoSekundi;

	// Token: 0x0400258D RID: 9613
	private DateTimeFormatInfo format;
}
