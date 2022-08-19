using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004FC RID: 1276
public class TimeManager : MonoBehaviour
{
	// Token: 0x0600294D RID: 10573 RVA: 0x0013BD1E File Offset: 0x00139F1E
	private void Awake()
	{
		base.transform.name = "TimeManager";
		Object.DontDestroyOnLoad(base.gameObject);
		this.GetCurrentTime();
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x0013BD44 File Offset: 0x00139F44
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

	// Token: 0x0600294F RID: 10575 RVA: 0x0013BDE0 File Offset: 0x00139FE0
	private void GetCurrentTime()
	{
		string text = "http://tapsong.net/content/3DGeoQuiz/index.php";
		this.www = new WWW(text);
		base.StartCoroutine("Wait5");
		base.StartCoroutine("WaitWWW");
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x0013BE17 File Offset: 0x0013A017
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

	// Token: 0x06002951 RID: 10577 RVA: 0x0013BE26 File Offset: 0x0013A026
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

	// Token: 0x06002952 RID: 10578 RVA: 0x0013BE35 File Offset: 0x0013A035
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

	// Token: 0x06002953 RID: 10579 RVA: 0x0013BE40 File Offset: 0x0013A040
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

	// Token: 0x0400257D RID: 9597
	public static DateTime currentDate;

	// Token: 0x0400257E RID: 9598
	private WWW www;
}
