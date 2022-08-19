using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200028B RID: 651
public class UIBiGuanXiuLianPanel : TabPanelBase
{
	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06001784 RID: 6020 RVA: 0x000A18AC File Offset: 0x0009FAAC
	// (set) Token: 0x06001785 RID: 6021 RVA: 0x000A18B4 File Offset: 0x0009FAB4
	private int BiGuanTime
	{
		get
		{
			return this.biGuanTime;
		}
		set
		{
			this.biGuanTime = value;
			this.YearText.text = string.Format("{0}", this.biGuanTime / 12);
			this.MonthText.text = string.Format("{0}", this.biGuanTime % 12);
		}
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x000A190E File Offset: 0x0009FB0E
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000A191C File Offset: 0x0009FB1C
	public void RefreshUI()
	{
		this.RefreshEventUI();
		this.RefreshSpeedUI();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000A1934 File Offset: 0x0009FB34
	public void RefreshEventUI()
	{
		this.EventSVContent.DestoryAllChild();
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (JSONObject jsonobject in player.taskMag._TaskData["Task"].list)
		{
			try
			{
				JSONObject jsonobject2 = jsonData.instance.TaskJsonData[jsonobject["id"].I.ToString()];
				if (jsonobject2["Type"].I == 0)
				{
					DateTime starTime = DateTime.Parse(jsonobject2["StarTime"].str);
					DateTime endTime = DateTime.Parse(jsonobject2["EndTime"].str);
					int i = jsonobject2["circulation"].I;
					DateTime nowTime = player.worldTimeMag.getNowTime();
					string taskNextTime = TaskCell.getTaskNextTime(i, nowTime, starTime, endTime);
					if (!(taskNextTime == ""))
					{
						if (!TaskUIManager.checkIsGuoShi(jsonobject))
						{
							GameObject gameObject = Object.Instantiate<GameObject>(this.EventItemPrefab, this.EventSVContent);
							gameObject.transform.GetChild(0).GetComponent<Text>().text = jsonobject2["Name"].Str;
							gameObject.transform.GetChild(1).GetComponent<Text>().text = taskNextTime;
							if (num % 2 == 0)
							{
								gameObject.GetComponent<Image>().enabled = false;
							}
							num++;
						}
					}
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("闭关修炼UI刷新任务UI时出现异常:\njson:{0}\n异常:{1}", jsonobject, arg));
			}
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x000A1B14 File Offset: 0x0009FD14
	public void RefreshSpeedUI()
	{
		Avatar player = Tools.instance.getPlayer();
		int staticID = player.getStaticID();
		JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[staticID.ToString()];
		if (staticID == 0)
		{
			this.GongFaName.text = "无";
			this.GongFaSpeed.text = "0/月";
		}
		else
		{
			this.GongFaName.text = "《" + Tools.instance.getStaticSkillName(staticID, false) + "》";
			this.GongFaSpeed.text = string.Format("{0}", jsonobject["Skill_Speed"].I);
		}
		if (SceneEx.NowSceneName == "S101")
		{
			DongFuData dongFuData = new DongFuData(DongFuManager.NowDongFuID);
			dongFuData.Load();
			int xiuliansudu = DFLingYanLevel.DataDict[dongFuData.LingYanLevel].xiuliansudu;
			int xiuliansudu2 = DFZhenYanLevel.DataDict[dongFuData.JuLingZhenLevel].xiuliansudu;
			this.DiMaiName.text = dongFuData.DongFuName;
			this.DiMaiSpeed.text = string.Format("{0}%", xiuliansudu + xiuliansudu2);
		}
		else
		{
			this.DiMaiName.text = jsonData.instance.BiguanJsonData[UIBiGuanPanel.Inst.BiGuanType.ToString()]["Text"].Str;
			this.DiMaiSpeed.text = string.Format("{0}%", jsonData.instance.BiguanJsonData[UIBiGuanPanel.Inst.BiGuanType.ToString()]["speed"].I);
		}
		this.XinJingName.text = jsonData.instance.XinJinJsonData[player.GetXinJingLevel().ToString()]["Text"].Str;
		int num = jsonData.instance.XinJinGuanLianJsonData[player.getXinJinGuanlianType().ToString()]["speed"].I - 100;
		if (num >= 0)
		{
			this.XinJingChange.text = "提升";
			this.XinJingSpeed.text = string.Format("{0}%", num);
		}
		else
		{
			this.XinJingChange.text = "降低";
			this.XinJingSpeed.text = string.Format("{0}%", -num);
		}
		this.ZiZhiName.text = UIBiGuanXiuLianPanel.GetZiZhiStr(player.ZiZhi);
		this.ZiZhiSpeed.text = string.Format("{0}", (int)player.AddZiZhiSpeed((float)jsonobject["Skill_Speed"].I));
		float timeExpSpeed = player.getTimeExpSpeed();
		Debug.Log(string.Format("基础修炼速度:{0}", timeExpSpeed));
		float biguanSpeed = UIBiGuanXiuLianPanel.GetBiguanSpeed(true, UIBiGuanPanel.Inst.BiGuanType, "");
		int num2 = (int)(timeExpSpeed + biguanSpeed);
		Debug.Log(string.Format("总速度:{0}", num2));
		this.TotalSpeed.text = num2.ToString();
		this.TimeSlider.maxValue = (float)this.GetBiGuanMaxTime();
		if (this.TimeSlider.maxValue == 0f)
		{
			this.TimeSlider.minValue = 0f;
			this.TimeSlider.value = 0f;
			this.BiGuanTime = 0;
		}
		else
		{
			this.TimeSlider.minValue = 1f;
			this.TimeSlider.value = 1f;
			this.BiGuanTime = 1;
		}
		this.TimeSlider.onValueChanged.RemoveAllListeners();
		this.TimeSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnTimeSliderValueChanged));
		if (player.ShuangXiuData.HasField("JingYuan"))
		{
			JSONObject jsonobject2 = player.ShuangXiuData["JingYuan"];
			ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[jsonobject2["Skill"].I];
			int num3 = jsonobject2["Count"].I / ShuangXiuLianHuaSuDu.DataDict[jsonobject2["PinJie"].I].speed;
			if (jsonobject2["Count"].I % ShuangXiuLianHuaSuDu.DataDict[jsonobject2["PinJie"].I].speed != 0)
			{
				num3++;
			}
			this.JingYuanTimeText.text = string.Format("闭关{0}年{1}月后", num3 / 12, num3 % 12);
			this.JingYuanDescText.text = string.Format("可将精元凝练为{0}{1}", jsonobject2["Reward"].I, UINPCShuangXiuAnim.ningliantypes[shuangXiuMiShu.ningliantype - 1]);
			return;
		}
		this.JingYuanTimeText.text = "空";
		this.JingYuanDescText.text = "";
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x000A2010 File Offset: 0x000A0210
	public static float GetBiguanSpeed(bool log = false, int biGuanType = 1, string sceneName = "")
	{
		Avatar player = PlayerEx.Player;
		int staticID = player.getStaticID();
		if (staticID != 0)
		{
			float num = jsonData.instance.XinJinGuanLianJsonData[player.getXinJinGuanlianType().ToString()]["speed"].n / 100f;
			string text = SceneEx.NowSceneName;
			if (!string.IsNullOrWhiteSpace(sceneName))
			{
				text = sceneName;
			}
			float num2;
			if (text == "S101")
			{
				DongFuData dongFuData = new DongFuData(DongFuManager.NowDongFuID);
				dongFuData.Load();
				float xiuliansudu = (float)DFLingYanLevel.DataDict[dongFuData.LingYanLevel].xiuliansudu;
				int xiuliansudu2 = DFZhenYanLevel.DataDict[dongFuData.JuLingZhenLevel].xiuliansudu;
				num2 = xiuliansudu + (float)xiuliansudu2;
				num2 /= 100f;
			}
			else
			{
				num2 = jsonData.instance.BiguanJsonData[biGuanType.ToString()]["speed"].n / 100f;
			}
			float n = jsonData.instance.StaticSkillJsonData[staticID.ToString()]["Skill_Speed"].n;
			float num3 = player.AddZiZhiSpeed(n);
			float jieDanSkillAddExp = player.getJieDanSkillAddExp();
			float num4 = (n * num2 * num + num3) * jieDanSkillAddExp;
			float num5 = 0f;
			if (player.TianFuID.HasField(string.Concat(12)))
			{
				num5 = player.TianFuID["12"].n / 100f;
				num4 += num4 * num5;
			}
			if (log)
			{
				Debug.Log(string.Format("闭关修炼速度:心境速度{0} 地脉速度(使用的场景{1}){2} 功法速度{3} 资质速度{4} 金丹加成{5} 天赋加成{6}", new object[]
				{
					num,
					text,
					num2,
					n,
					num3,
					jieDanSkillAddExp,
					num5
				}) + string.Format("\n闭关速度结算:((({0}*{1}*{2})+{3})*{4})*(1+{5})={6}", new object[]
				{
					n,
					num2,
					num,
					num3,
					jieDanSkillAddExp,
					num5,
					num4
				}));
			}
			return num4;
		}
		return 0f;
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000A224C File Offset: 0x000A044C
	public int GetBiGuanMaxTime()
	{
		Avatar player = Tools.instance.getPlayer();
		string screenName = Tools.getScreenName();
		DateTime residueTime = player.zulinContorl.getResidueTime(screenName);
		int num = (int)(((ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n - player.exp) / (UIBiGuanXiuLianPanel.GetBiguanSpeed(false, UIBiGuanPanel.Inst.BiGuanType, "") + player.getTimeExpSpeed())) + 1;
		int timeSum = ZulinContorl.GetTimeSum(residueTime);
		if (player.level == 15 && (int)player.exp >= LevelUpDataJsonData.DataDict[15].MaxExp)
		{
			return Mathf.Min(1200, timeSum);
		}
		return Mathf.Min(Mathf.Min(240, timeSum), num);
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000A2314 File Offset: 0x000A0514
	private static string GetZiZhiStr(int zizhi)
	{
		int num = 0;
		while (num < UIBiGuanXiuLianPanel.ziZhiNum.Length && zizhi >= UIBiGuanXiuLianPanel.ziZhiNum[num])
		{
			num++;
		}
		return UIBiGuanXiuLianPanel.ziZhiStr[num - 1];
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000A2346 File Offset: 0x000A0546
	public void OnStartBiGuanClick()
	{
		if (this.BiGuanTime == 0)
		{
			return;
		}
		Tools.instance.playFader("正在闭关苦修...", delegate
		{
			this.RefreshUI();
		});
		base.Invoke("OkEvent", 0.5f);
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000A237C File Offset: 0x000A057C
	private void OkEvent()
	{
		Avatar player = PlayerEx.Player;
		int num = this.BiGuanTime / 12;
		int num2 = this.BiGuanTime % 12;
		player.AddTime(0, num2, num);
		if (player.getStaticID() != 0)
		{
			player.addEXP((int)((float)(num * 12 + num2) * UIBiGuanXiuLianPanel.GetBiguanSpeed(false, UIBiGuanPanel.Inst.BiGuanType, "")));
		}
		UIBiGuanXiuLianPanel.CalcShuangXiu(this.BiGuanTime);
		player.HP = player.HP_Max;
		try
		{
			player.wuDaoMag.autoAddBiGuanLingGuang(num2 + num * 12);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000A241C File Offset: 0x000A061C
	public static void CalcShuangXiu(int biGuanTime)
	{
		Avatar player = PlayerEx.Player;
		if (player.ShuangXiuData.HasField("JingYuan"))
		{
			JSONObject jsonobject = player.ShuangXiuData["JingYuan"];
			int num = jsonobject["Count"].I;
			ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[jsonobject["Skill"].I];
			int i = jsonobject["PinJie"].I;
			int i2 = jsonobject["Reward"].I;
			num -= biGuanTime * ShuangXiuLianHuaSuDu.DataDict[i].speed;
			if (num <= 0)
			{
				if (shuangXiuMiShu.ningliantype == 1)
				{
					player.addEXP(i2);
				}
				else if (shuangXiuMiShu.ningliantype == 2)
				{
					player.xinjin += i2;
				}
				else if (shuangXiuMiShu.ningliantype == 3)
				{
					player.addShenShi(i2);
				}
				else if (shuangXiuMiShu.ningliantype == 4)
				{
					player._HP_Max += i2;
				}
				player.ShuangXiuData.RemoveField("JingYuan");
				return;
			}
			player.ShuangXiuData["JingYuan"].SetField("Count", num);
		}
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000A2544 File Offset: 0x000A0744
	public void OnLeftTimeBtnClick()
	{
		Slider timeSlider = this.TimeSlider;
		float value = timeSlider.value;
		timeSlider.value = value - 1f;
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x000A256C File Offset: 0x000A076C
	public void OnRightTimeBtnClick()
	{
		Slider timeSlider = this.TimeSlider;
		float value = timeSlider.value;
		timeSlider.value = value + 1f;
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000A2592 File Offset: 0x000A0792
	public void OnTimeSliderValueChanged(float value)
	{
		this.BiGuanTime = (int)value;
	}

	// Token: 0x04001241 RID: 4673
	public GameObject EventItemPrefab;

	// Token: 0x04001242 RID: 4674
	public RectTransform EventSVContent;

	// Token: 0x04001243 RID: 4675
	public Text GongFaName;

	// Token: 0x04001244 RID: 4676
	public Text GongFaSpeed;

	// Token: 0x04001245 RID: 4677
	public Text ZiZhiName;

	// Token: 0x04001246 RID: 4678
	public Text ZiZhiSpeed;

	// Token: 0x04001247 RID: 4679
	public Text DiMaiName;

	// Token: 0x04001248 RID: 4680
	public Text DiMaiSpeed;

	// Token: 0x04001249 RID: 4681
	public Text XinJingName;

	// Token: 0x0400124A RID: 4682
	public Text XinJingSpeed;

	// Token: 0x0400124B RID: 4683
	public Text XinJingChange;

	// Token: 0x0400124C RID: 4684
	public Text JingYuanTimeText;

	// Token: 0x0400124D RID: 4685
	public Text JingYuanDescText;

	// Token: 0x0400124E RID: 4686
	public Text TotalSpeed;

	// Token: 0x0400124F RID: 4687
	public Slider TimeSlider;

	// Token: 0x04001250 RID: 4688
	public Text YearText;

	// Token: 0x04001251 RID: 4689
	public Text MonthText;

	// Token: 0x04001252 RID: 4690
	private int biGuanTime;

	// Token: 0x04001253 RID: 4691
	private static int[] ziZhiNum = new int[]
	{
		0,
		16,
		31,
		46,
		61,
		85
	};

	// Token: 0x04001254 RID: 4692
	private static string[] ziZhiStr = new string[]
	{
		"资质愚钝",
		"资质平平",
		"机敏聪慧",
		"慧心灵性",
		"旷世奇才",
		"天纵之姿"
	};
}
