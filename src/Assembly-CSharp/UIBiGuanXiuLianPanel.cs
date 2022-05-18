using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003B9 RID: 953
public class UIBiGuanXiuLianPanel : TabPanelBase
{
	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00016813 File Offset: 0x00014A13
	// (set) Token: 0x06001A62 RID: 6754 RVA: 0x000E8D80 File Offset: 0x000E6F80
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

	// Token: 0x06001A63 RID: 6755 RVA: 0x0001681B File Offset: 0x00014A1B
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x00016829 File Offset: 0x00014A29
	public void RefreshUI()
	{
		this.RefreshEventUI();
		this.RefreshSpeedUI();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x000E8DDC File Offset: 0x000E6FDC
	public void RefreshEventUI()
	{
		this.EventSVContent.DestoryAllChild();
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (JSONObject jsonobject in player.taskMag._TaskData["Task"].list)
		{
			JSONObject jsonobject2 = jsonData.instance.TaskJsonData[((int)jsonobject["id"].n).ToString()];
			if ((int)jsonobject2["Type"].n == 0)
			{
				DateTime starTime = DateTime.Parse(jsonobject2["StarTime"].str);
				DateTime endTime = DateTime.Parse(jsonobject2["EndTime"].str);
				int i = jsonobject2["circulation"].I;
				DateTime nowTime = player.worldTimeMag.getNowTime();
				string taskNextTime = TaskCell.getTaskNextTime(i, nowTime, starTime, endTime);
				if (!(taskNextTime == "") && !TaskUIManager.checkIsGuoShi(jsonobject))
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

	// Token: 0x06001A66 RID: 6758 RVA: 0x000E8F84 File Offset: 0x000E7184
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

	// Token: 0x06001A67 RID: 6759 RVA: 0x000E9480 File Offset: 0x000E7680
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

	// Token: 0x06001A68 RID: 6760 RVA: 0x000E96BC File Offset: 0x000E78BC
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

	// Token: 0x06001A69 RID: 6761 RVA: 0x000E9784 File Offset: 0x000E7984
	private static string GetZiZhiStr(int zizhi)
	{
		int num = 0;
		while (num < UIBiGuanXiuLianPanel.ziZhiNum.Length && zizhi >= UIBiGuanXiuLianPanel.ziZhiNum[num])
		{
			num++;
		}
		return UIBiGuanXiuLianPanel.ziZhiStr[num - 1];
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x00016841 File Offset: 0x00014A41
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

	// Token: 0x06001A6B RID: 6763 RVA: 0x000E97B8 File Offset: 0x000E79B8
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

	// Token: 0x06001A6C RID: 6764 RVA: 0x000E9858 File Offset: 0x000E7A58
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

	// Token: 0x06001A6D RID: 6765 RVA: 0x000E9980 File Offset: 0x000E7B80
	public void OnLeftTimeBtnClick()
	{
		Slider timeSlider = this.TimeSlider;
		float value = timeSlider.value;
		timeSlider.value = value - 1f;
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x000E99A8 File Offset: 0x000E7BA8
	public void OnRightTimeBtnClick()
	{
		Slider timeSlider = this.TimeSlider;
		float value = timeSlider.value;
		timeSlider.value = value + 1f;
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x00016877 File Offset: 0x00014A77
	public void OnTimeSliderValueChanged(float value)
	{
		this.BiGuanTime = (int)value;
	}

	// Token: 0x040015C4 RID: 5572
	public GameObject EventItemPrefab;

	// Token: 0x040015C5 RID: 5573
	public RectTransform EventSVContent;

	// Token: 0x040015C6 RID: 5574
	public Text GongFaName;

	// Token: 0x040015C7 RID: 5575
	public Text GongFaSpeed;

	// Token: 0x040015C8 RID: 5576
	public Text ZiZhiName;

	// Token: 0x040015C9 RID: 5577
	public Text ZiZhiSpeed;

	// Token: 0x040015CA RID: 5578
	public Text DiMaiName;

	// Token: 0x040015CB RID: 5579
	public Text DiMaiSpeed;

	// Token: 0x040015CC RID: 5580
	public Text XinJingName;

	// Token: 0x040015CD RID: 5581
	public Text XinJingSpeed;

	// Token: 0x040015CE RID: 5582
	public Text XinJingChange;

	// Token: 0x040015CF RID: 5583
	public Text JingYuanTimeText;

	// Token: 0x040015D0 RID: 5584
	public Text JingYuanDescText;

	// Token: 0x040015D1 RID: 5585
	public Text TotalSpeed;

	// Token: 0x040015D2 RID: 5586
	public Slider TimeSlider;

	// Token: 0x040015D3 RID: 5587
	public Text YearText;

	// Token: 0x040015D4 RID: 5588
	public Text MonthText;

	// Token: 0x040015D5 RID: 5589
	private int biGuanTime;

	// Token: 0x040015D6 RID: 5590
	private static int[] ziZhiNum = new int[]
	{
		0,
		16,
		31,
		46,
		61,
		85
	};

	// Token: 0x040015D7 RID: 5591
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
