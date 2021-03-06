using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005ED RID: 1517
public class BiGuanYinfo : MonoBehaviour
{
	// Token: 0x06002612 RID: 9746 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x0012D424 File Offset: 0x0012B624
	public void setEventShiJian()
	{
		foreach (object obj in this.grid.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		Avatar player = Tools.instance.getPlayer();
		foreach (JSONObject jsonobject in player.taskMag._TaskData["Task"].list)
		{
			JSONObject jsonobject2 = jsonData.instance.TaskJsonData[((int)jsonobject["id"].n).ToString()];
			if ((int)jsonobject2["Type"].n == 0)
			{
				DateTime starTime = DateTime.Parse(jsonobject2["StarTime"].str);
				DateTime endTime = DateTime.Parse(jsonobject2["EndTime"].str);
				int circulation = (int)jsonobject2["circulation"].n;
				DateTime nowTime = player.worldTimeMag.getNowTime();
				string taskNextTime = BiGuanYinfo.getTaskNextTime(circulation, nowTime, starTime, endTime);
				if (!(taskNextTime == ""))
				{
					UILabel uilabel = Object.Instantiate<UILabel>(this.Temp);
					uilabel.transform.SetParent(this.grid.transform);
					uilabel.transform.localScale = Vector3.one;
					uilabel.gameObject.SetActive(true);
					uilabel.text = Tools.instance.Code64ToString(jsonobject2["Name"].str);
					uilabel.transform.Find("Event").GetComponent<UILabel>().text = taskNextTime;
				}
			}
		}
		this.grid.repositionNow = true;
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x0012D630 File Offset: 0x0012B830
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
				DateTime dateTime = new DateTime(1, 1, 1).AddDays((double)timeSpan.Days);
				result = "剩余:{X}".Replace("{X}", string.Concat(new object[]
				{
					dateTime.Year - 1,
					"年",
					dateTime.Month - 1,
					"月",
					dateTime.Day - 1,
					"日"
				}));
			}
		}
		else
		{
			if (now > StarTime)
			{
				now = new DateTime(now.Year - circulation * ((now.Year - StarTime.Year) / circulation), now.Month, (now.Month == 2 && now.Day == 29) ? 28 : now.Day);
			}
			DateTime d = new DateTime(StarTime.Year + circulation, StarTime.Month, StarTime.Day);
			if (StarTime <= now && now <= EndTime)
			{
				result = Tools.getStr("chuanwen5");
			}
			else
			{
				TimeSpan timeSpan2 = d - now;
				if (StarTime > now && StarTime.Year == now.Year)
				{
					timeSpan2 = StarTime - now;
				}
				DateTime dateTime2 = new DateTime(1, 1, 1).AddDays((double)timeSpan2.Days);
				result = "剩余:{X}".Replace("{X}", string.Concat(new object[]
				{
					dateTime2.Year - 1,
					"年",
					dateTime2.Month - 1,
					"月",
					dateTime2.Day - 1,
					"日"
				}));
			}
		}
		return result;
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x0012D84C File Offset: 0x0012BA4C
	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		int staticID = player.getStaticID();
		if (staticID == 0)
		{
			this.label1.text = "[FF0000]你尚未装备主修功法修炼速度为0";
			return;
		}
		JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)];
		this.label1.text = string.Concat(new string[]
		{
			"[E58E5D]功法：[-][000000]当前主修功法为《",
			Tools.instance.getStaticSkillName(staticID, false),
			"》修炼速度加",
			jsonobject["Skill_Speed"].n.ToString(),
			"/月"
		});
		this.label2.text = "[E58E5D]洞府：[-][000000]" + Tools.instance.Code64ToString(jsonData.instance.BiguanJsonData[string.Concat(this.biguan.biguanType)]["Text"].str);
		int i = jsonData.instance.XinJinGuanLianJsonData[string.Concat(player.getXinJinGuanlianType())]["speed"].I;
		if (i == 100)
		{
			this.label3.text = "[E58E5D]心境：[-][000000]你当前的心境符合境界需求，闭关时能获得100%收益";
		}
		else if (i < 100)
		{
			this.label3.text = string.Format("[E58E5D]心境：[-][000000]你当前的心境低于境界需求，闭关仅能获得{0}%收益", i);
		}
		else
		{
			this.label3.text = string.Format("[E58E5D]心境：[-][000000]你当前的心境高于境界需求，闭关时能获得{0}%收益", i);
		}
		this.label4.text = "[E58E5D]资质：[-][000000]资质增加的修炼速度为" + player.AddZiZhiSpeed(jsonobject["Skill_Speed"].n) + "/月";
	}

	// Token: 0x04002092 RID: 8338
	public UILabel label1;

	// Token: 0x04002093 RID: 8339
	public UILabel label2;

	// Token: 0x04002094 RID: 8340
	public UILabel label3;

	// Token: 0x04002095 RID: 8341
	public UILabel label4;

	// Token: 0x04002096 RID: 8342
	public UIBiGuan biguan;

	// Token: 0x04002097 RID: 8343
	public UILabel Temp;

	// Token: 0x04002098 RID: 8344
	public UIGrid grid;
}
