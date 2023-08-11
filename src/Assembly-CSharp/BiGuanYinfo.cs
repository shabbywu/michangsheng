using System;
using KBEngine;
using UnityEngine;

public class BiGuanYinfo : MonoBehaviour
{
	public UILabel label1;

	public UILabel label2;

	public UILabel label3;

	public UILabel label4;

	public UIBiGuan biguan;

	public UILabel Temp;

	public UIGrid grid;

	private void Start()
	{
	}

	public void setEventShiJian()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		foreach (Transform item in ((Component)grid).transform)
		{
			Object.Destroy((Object)(object)((Component)item).gameObject);
		}
		Avatar player = Tools.instance.getPlayer();
		foreach (JSONObject item2 in player.taskMag._TaskData["Task"].list)
		{
			JSONObject jSONObject = jsonData.instance.TaskJsonData[item2["id"].I.ToString()];
			if ((int)jSONObject["Type"].n == 0)
			{
				DateTime starTime = DateTime.Parse(jSONObject["StarTime"].str);
				DateTime endTime = DateTime.Parse(jSONObject["EndTime"].str);
				int circulation = (int)jSONObject["circulation"].n;
				DateTime nowTime = player.worldTimeMag.getNowTime();
				string taskNextTime = getTaskNextTime(circulation, nowTime, starTime, endTime);
				if (!(taskNextTime == ""))
				{
					UILabel uILabel = Object.Instantiate<UILabel>(Temp);
					((Component)uILabel).transform.SetParent(((Component)grid).transform);
					((Component)uILabel).transform.localScale = Vector3.one;
					((Component)uILabel).gameObject.SetActive(true);
					uILabel.text = Tools.instance.Code64ToString(jSONObject["Name"].str);
					((Component)((Component)uILabel).transform.Find("Event")).GetComponent<UILabel>().text = taskNextTime;
				}
			}
		}
		grid.repositionNow = true;
	}

	public static string getTaskNextTime(int circulation, DateTime now, DateTime StarTime, DateTime EndTime)
	{
		string result = "";
		if (circulation == 0 || now < StarTime)
		{
			if (now >= StarTime && now < EndTime)
			{
				result = Tools.getStr("chuanwen5");
			}
			else if (now < StarTime)
			{
				TimeSpan timeSpan = StarTime - now;
				DateTime dateTime = new DateTime(1, 1, 1).AddDays(timeSpan.Days);
				result = "剩余:{X}".Replace("{X}", dateTime.Year - 1 + "年" + (dateTime.Month - 1) + "月" + (dateTime.Day - 1) + "日");
			}
		}
		else
		{
			if (now > StarTime)
			{
				now = new DateTime(now.Year - circulation * ((now.Year - StarTime.Year) / circulation), now.Month, (now.Month == 2 && now.Day == 29) ? 28 : now.Day);
			}
			DateTime dateTime2 = new DateTime(StarTime.Year + circulation, StarTime.Month, StarTime.Day);
			if (StarTime <= now && now <= EndTime)
			{
				result = Tools.getStr("chuanwen5");
			}
			else
			{
				TimeSpan timeSpan2 = dateTime2 - now;
				if (StarTime > now && StarTime.Year == now.Year)
				{
					timeSpan2 = StarTime - now;
				}
				DateTime dateTime3 = new DateTime(1, 1, 1).AddDays(timeSpan2.Days);
				result = "剩余:{X}".Replace("{X}", dateTime3.Year - 1 + "年" + (dateTime3.Month - 1) + "月" + (dateTime3.Day - 1) + "日");
			}
		}
		return result;
	}

	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		int staticID = player.getStaticID();
		if (staticID == 0)
		{
			label1.text = "[FF0000]你尚未装备主修功法修炼速度为0";
			return;
		}
		JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)];
		label1.text = "[E58E5D]功法：[-][000000]当前主修功法为《" + Tools.instance.getStaticSkillName(staticID) + "》修炼速度加" + jSONObject["Skill_Speed"].n + "/月";
		label2.text = "[E58E5D]洞府：[-][000000]" + Tools.instance.Code64ToString(jsonData.instance.BiguanJsonData[string.Concat(biguan.biguanType)]["Text"].str);
		int i = jsonData.instance.XinJinGuanLianJsonData[string.Concat(player.getXinJinGuanlianType())]["speed"].I;
		if (i == 100)
		{
			label3.text = "[E58E5D]心境：[-][000000]你当前的心境符合境界需求，闭关时能获得100%收益";
		}
		else if (i < 100)
		{
			label3.text = $"[E58E5D]心境：[-][000000]你当前的心境低于境界需求，闭关仅能获得{i}%收益";
		}
		else
		{
			label3.text = $"[E58E5D]心境：[-][000000]你当前的心境高于境界需求，闭关时能获得{i}%收益";
		}
		label4.text = "[E58E5D]资质：[-][000000]资质增加的修炼速度为" + player.AddZiZhiSpeed(jSONObject["Skill_Speed"].n) + "/月";
	}
}
