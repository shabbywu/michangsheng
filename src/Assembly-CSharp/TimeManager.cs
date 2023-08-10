using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public static DateTime currentDate;

	private WWW www;

	private void Awake()
	{
		((Object)((Component)this).transform).name = "TimeManager";
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		GetCurrentTime();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			((MonoBehaviour)this).StopCoroutine("CountTime");
			if (currentDate.Year != 42)
			{
				PlayerPrefs.SetInt("minutes", currentDate.Minute);
				PlayerPrefs.SetInt("hours", currentDate.Hour);
				PlayerPrefs.SetInt("year", currentDate.Year);
				PlayerPrefs.SetInt("day", currentDate.Day);
				PlayerPrefs.SetInt("month", currentDate.Month);
				PlayerPrefs.Save();
			}
		}
		else
		{
			GetCurrentTime();
		}
	}

	private void GetCurrentTime()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		string text = "http://tapsong.net/content/3DGeoQuiz/index.php";
		www = new WWW(text);
		((MonoBehaviour)this).StartCoroutine("Wait5");
		((MonoBehaviour)this).StartCoroutine("WaitWWW");
	}

	private IEnumerator WaitWWW()
	{
		yield return www;
		if (string.IsNullOrEmpty(www.error) && www.isDone)
		{
			((MonoBehaviour)this).StopCoroutine("Wait5");
			string[] array = www.text.Split(new char[1] { '/' });
			int day = int.Parse(array[0]);
			int month = int.Parse(array[1]);
			int year = int.Parse(array[2]);
			int hour = int.Parse(array[3]);
			int minute = int.Parse(array[4]);
			currentDate = new DateTime(year, month, day, hour, minute, 0);
			((MonoBehaviour)this).StartCoroutine("CountTime");
			CheckForDailyReward();
			Debug.Log((object)"wwwOk");
		}
		else
		{
			Debug.Log((object)"wwwError");
			((MonoBehaviour)this).StopCoroutine("Wait5");
			currentDate = new DateTime(42, 1, 1, 1, 1, 1);
		}
	}

	private IEnumerator Wait5()
	{
		yield return (object)new WaitForSeconds(5f);
		if (string.IsNullOrEmpty(www.error) && www.isDone)
		{
			((MonoBehaviour)this).StopCoroutine("WaitWWW");
			string[] array = www.text.Split(new char[1] { '/' });
			int day = int.Parse(array[0]);
			int month = int.Parse(array[1]);
			int year = int.Parse(array[2]);
			int hour = int.Parse(array[3]);
			int minute = int.Parse(array[4]);
			currentDate = new DateTime(year, month, day, hour, minute, 0);
			((MonoBehaviour)this).StartCoroutine("CountTime");
			CheckForDailyReward();
			Debug.Log((object)"5ok");
		}
		else
		{
			Debug.Log((object)"5Error");
			((MonoBehaviour)this).StopCoroutine("WaitWWW");
			currentDate = new DateTime(42, 1, 1, 1, 1, 1);
		}
	}

	private IEnumerator CountTime()
	{
		while (true)
		{
			currentDate.AddSeconds(1.0);
			yield return (object)new WaitForSeconds(1f);
			yield return null;
		}
	}

	private void CheckForDailyReward()
	{
		if (currentDate.Year == 42)
		{
			return;
		}
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
				Mathf.Abs((float)currentDate.Subtract(value).TotalHours);
				_ = 24f;
			}
		}
		else
		{
			PlayerPrefs.SetInt("scheduledNotification", 1);
			PlayerPrefs.SetInt("minutessNotification", currentDate.Minute);
			PlayerPrefs.SetInt("hourssNotification", currentDate.Hour);
			PlayerPrefs.SetInt("yearsNotification", currentDate.Year);
			PlayerPrefs.SetInt("daysNotification", currentDate.Day);
			PlayerPrefs.SetInt("monthsNotification", currentDate.Month);
			PlayerPrefs.Save();
		}
	}
}
