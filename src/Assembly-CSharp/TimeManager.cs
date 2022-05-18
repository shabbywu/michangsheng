using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200078A RID: 1930
public class TimeManager : MonoBehaviour
{
	// Token: 0x06003140 RID: 12608 RVA: 0x00024163 File Offset: 0x00022363
	private void Awake()
	{
		base.transform.name = "TimeManager";
		Object.DontDestroyOnLoad(base.gameObject);
		this.GetCurrentTime();
	}

	// Token: 0x06003141 RID: 12609 RVA: 0x00188FD8 File Offset: 0x001871D8
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			base.StopCoroutine("CountTime");
			if (TimeManager.currentDate.Year != 42)
			{
				PlayerPrefs.SetInt("minutes", TimeManager.currentDate.Minute);
				PlayerPrefs.SetInt("hours", TimeManager.currentDate.Hour);
				PlayerPrefs.SetInt("year", TimeManager.currentDate.Year);
				PlayerPrefs.SetInt("day", TimeManager.currentDate.Day);
				PlayerPrefs.SetInt("month", TimeManager.currentDate.Month);
				PlayerPrefs.Save();
				return;
			}
		}
		else
		{
			this.GetCurrentTime();
		}
	}

	// Token: 0x06003142 RID: 12610 RVA: 0x00189074 File Offset: 0x00187274
	private void GetCurrentTime()
	{
		string text = "http://tapsong.net/content/3DGeoQuiz/index.php";
		this.www = new WWW(text);
		base.StartCoroutine("Wait5");
		base.StartCoroutine("WaitWWW");
	}

	// Token: 0x06003143 RID: 12611 RVA: 0x00024186 File Offset: 0x00022386
	private IEnumerator WaitWWW()
	{
		yield return this.www;
		if (string.IsNullOrEmpty(this.www.error) && this.www.isDone)
		{
			base.StopCoroutine("Wait5");
			string[] array = this.www.text.Split(new char[]
			{
				'/'
			});
			int day = int.Parse(array[0]);
			int month = int.Parse(array[1]);
			int year = int.Parse(array[2]);
			int hour = int.Parse(array[3]);
			int minute = int.Parse(array[4]);
			TimeManager.currentDate = new DateTime(year, month, day, hour, minute, 0);
			base.StartCoroutine("CountTime");
			this.CheckForDailyReward();
			Debug.Log("wwwOk");
		}
		else
		{
			Debug.Log("wwwError");
			base.StopCoroutine("Wait5");
			TimeManager.currentDate = new DateTime(42, 1, 1, 1, 1, 1);
		}
		yield break;
	}

	// Token: 0x06003144 RID: 12612 RVA: 0x00024195 File Offset: 0x00022395
	private IEnumerator Wait5()
	{
		yield return new WaitForSeconds(5f);
		if (string.IsNullOrEmpty(this.www.error) && this.www.isDone)
		{
			base.StopCoroutine("WaitWWW");
			string[] array = this.www.text.Split(new char[]
			{
				'/'
			});
			int day = int.Parse(array[0]);
			int month = int.Parse(array[1]);
			int year = int.Parse(array[2]);
			int hour = int.Parse(array[3]);
			int minute = int.Parse(array[4]);
			TimeManager.currentDate = new DateTime(year, month, day, hour, minute, 0);
			base.StartCoroutine("CountTime");
			this.CheckForDailyReward();
			Debug.Log("5ok");
		}
		else
		{
			Debug.Log("5Error");
			base.StopCoroutine("WaitWWW");
			TimeManager.currentDate = new DateTime(42, 1, 1, 1, 1, 1);
		}
		yield break;
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x000241A4 File Offset: 0x000223A4
	private IEnumerator CountTime()
	{
		for (;;)
		{
			TimeManager.currentDate.AddSeconds(1.0);
			yield return new WaitForSeconds(1f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06003146 RID: 12614 RVA: 0x001890AC File Offset: 0x001872AC
	private void CheckForDailyReward()
	{
		if (TimeManager.currentDate.Year != 42)
		{
			if (PlayerPrefs.HasKey("scheduledNotification"))
			{
				int @int = PlayerPrefs.GetInt("minutesNotification");
				int int2 = PlayerPrefs.GetInt("hoursNotification");
				int int3 = PlayerPrefs.GetInt("yearNotification");
				int int4 = PlayerPrefs.GetInt("dayNotification");
				int int5 = PlayerPrefs.GetInt("monthNotification");
				if (int3 != 0)
				{
					DateTime value = new DateTime(int3, int5, int4, int2, @int, 0);
					Mathf.Abs((float)TimeManager.currentDate.Subtract(value).TotalHours);
					return;
				}
			}
			else
			{
				PlayerPrefs.SetInt("scheduledNotification", 1);
				PlayerPrefs.SetInt("minutessNotification", TimeManager.currentDate.Minute);
				PlayerPrefs.SetInt("hourssNotification", TimeManager.currentDate.Hour);
				PlayerPrefs.SetInt("yearsNotification", TimeManager.currentDate.Year);
				PlayerPrefs.SetInt("daysNotification", TimeManager.currentDate.Day);
				PlayerPrefs.SetInt("monthsNotification", TimeManager.currentDate.Month);
				PlayerPrefs.Save();
			}
		}
	}

	// Token: 0x04002D5D RID: 11613
	public static DateTime currentDate;

	// Token: 0x04002D5E RID: 11614
	private WWW www;
}
