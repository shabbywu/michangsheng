using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBiGuanXiuLianPanel : TabPanelBase
{
	public GameObject EventItemPrefab;

	public RectTransform EventSVContent;

	public Text GongFaName;

	public Text GongFaSpeed;

	public Text ZiZhiName;

	public Text ZiZhiSpeed;

	public Text DiMaiName;

	public Text DiMaiSpeed;

	public Text XinJingName;

	public Text XinJingSpeed;

	public Text XinJingChange;

	public Text JingYuanTimeText;

	public Text JingYuanDescText;

	public Text TotalSpeed;

	public Slider TimeSlider;

	public Text YearText;

	public Text MonthText;

	private int biGuanTime;

	private static int[] ziZhiNum = new int[6] { 0, 16, 31, 46, 61, 85 };

	private static string[] ziZhiStr = new string[6] { "资质愚钝", "资质平平", "机敏聪慧", "慧心灵性", "旷世奇才", "天纵之姿" };

	private int BiGuanTime
	{
		get
		{
			return biGuanTime;
		}
		set
		{
			biGuanTime = value;
			YearText.text = $"{biGuanTime / 12}";
			MonthText.text = $"{biGuanTime % 12}";
		}
	}

	public override void OnPanelShow()
	{
		base.OnPanelShow();
		RefreshUI();
	}

	public void RefreshUI()
	{
		RefreshEventUI();
		RefreshSpeedUI();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	public void RefreshEventUI()
	{
		((Transform)(object)EventSVContent).DestoryAllChild();
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (JSONObject item in player.taskMag._TaskData["Task"].list)
		{
			try
			{
				JSONObject jSONObject = jsonData.instance.TaskJsonData[item["id"].I.ToString()];
				if (jSONObject["Type"].I != 0)
				{
					continue;
				}
				DateTime starTime = DateTime.Parse(jSONObject["StarTime"].str);
				DateTime endTime = DateTime.Parse(jSONObject["EndTime"].str);
				int i = jSONObject["circulation"].I;
				DateTime nowTime = player.worldTimeMag.getNowTime();
				string taskNextTime = TaskCell.getTaskNextTime(i, nowTime, starTime, endTime);
				if (!(taskNextTime == "") && !TaskUIManager.checkIsGuoShi(item))
				{
					GameObject val = Object.Instantiate<GameObject>(EventItemPrefab, (Transform)(object)EventSVContent);
					((Component)val.transform.GetChild(0)).GetComponent<Text>().text = jSONObject["Name"].Str;
					((Component)val.transform.GetChild(1)).GetComponent<Text>().text = taskNextTime;
					if (num % 2 == 0)
					{
						((Behaviour)val.GetComponent<Image>()).enabled = false;
					}
					num++;
				}
			}
			catch (Exception arg)
			{
				Debug.LogError((object)$"闭关修炼UI刷新任务UI时出现异常:\njson:{item}\n异常:{arg}");
			}
		}
	}

	public void RefreshSpeedUI()
	{
		Avatar player = Tools.instance.getPlayer();
		int staticID = player.getStaticID();
		JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[staticID.ToString()];
		if (staticID == 0)
		{
			GongFaName.text = "无";
			GongFaSpeed.text = "0/月";
		}
		else
		{
			GongFaName.text = "《" + Tools.instance.getStaticSkillName(staticID) + "》";
			GongFaSpeed.text = string.Format("{0}", jSONObject["Skill_Speed"].I);
		}
		if (SceneEx.NowSceneName == "S101")
		{
			DongFuData dongFuData = new DongFuData(DongFuManager.NowDongFuID);
			dongFuData.Load();
			int xiuliansudu = DFLingYanLevel.DataDict[dongFuData.LingYanLevel].xiuliansudu;
			int xiuliansudu2 = DFZhenYanLevel.DataDict[dongFuData.JuLingZhenLevel].xiuliansudu;
			DiMaiName.text = dongFuData.DongFuName;
			DiMaiSpeed.text = $"{xiuliansudu + xiuliansudu2}%";
		}
		else
		{
			DiMaiName.text = jsonData.instance.BiguanJsonData[UIBiGuanPanel.Inst.BiGuanType.ToString()]["Text"].Str;
			DiMaiSpeed.text = string.Format("{0}%", jsonData.instance.BiguanJsonData[UIBiGuanPanel.Inst.BiGuanType.ToString()]["speed"].I);
		}
		XinJingName.text = jsonData.instance.XinJinJsonData[player.GetXinJingLevel().ToString()]["Text"].Str;
		int num = jsonData.instance.XinJinGuanLianJsonData[player.getXinJinGuanlianType().ToString()]["speed"].I - 100;
		if (num >= 0)
		{
			XinJingChange.text = "提升";
			XinJingSpeed.text = $"{num}%";
		}
		else
		{
			XinJingChange.text = "降低";
			XinJingSpeed.text = $"{-num}%";
		}
		ZiZhiName.text = GetZiZhiStr(player.ZiZhi);
		ZiZhiSpeed.text = string.Format("{0}", (int)player.AddZiZhiSpeed(jSONObject["Skill_Speed"].I));
		float timeExpSpeed = player.getTimeExpSpeed();
		Debug.Log((object)$"基础修炼速度:{timeExpSpeed}");
		float biguanSpeed = GetBiguanSpeed(log: true, UIBiGuanPanel.Inst.BiGuanType);
		int num2 = (int)(timeExpSpeed + biguanSpeed);
		Debug.Log((object)$"总速度:{num2}");
		TotalSpeed.text = num2.ToString();
		TimeSlider.maxValue = GetBiGuanMaxTime();
		if (TimeSlider.maxValue == 0f)
		{
			TimeSlider.minValue = 0f;
			TimeSlider.value = 0f;
			BiGuanTime = 0;
		}
		else
		{
			TimeSlider.minValue = 1f;
			TimeSlider.value = 1f;
			BiGuanTime = 1;
		}
		((UnityEventBase)TimeSlider.onValueChanged).RemoveAllListeners();
		((UnityEvent<float>)(object)TimeSlider.onValueChanged).AddListener((UnityAction<float>)OnTimeSliderValueChanged);
		if (player.ShuangXiuData.HasField("JingYuan"))
		{
			JSONObject jSONObject2 = player.ShuangXiuData["JingYuan"];
			ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[jSONObject2["Skill"].I];
			int num3 = jSONObject2["Count"].I / ShuangXiuLianHuaSuDu.DataDict[jSONObject2["PinJie"].I].speed;
			if (jSONObject2["Count"].I % ShuangXiuLianHuaSuDu.DataDict[jSONObject2["PinJie"].I].speed != 0)
			{
				num3++;
			}
			JingYuanTimeText.text = $"闭关{num3 / 12}年{num3 % 12}月后";
			JingYuanDescText.text = string.Format("可将精元凝练为{0}{1}", jSONObject2["Reward"].I, UINPCShuangXiuAnim.ningliantypes[shuangXiuMiShu.ningliantype - 1]);
		}
		else
		{
			JingYuanTimeText.text = "空";
			JingYuanDescText.text = "";
		}
	}

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
				int xiuliansudu = DFLingYanLevel.DataDict[dongFuData.LingYanLevel].xiuliansudu;
				int xiuliansudu2 = DFZhenYanLevel.DataDict[dongFuData.JuLingZhenLevel].xiuliansudu;
				num2 = xiuliansudu + xiuliansudu2;
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
				Debug.Log((object)($"闭关修炼速度:心境速度{num} 地脉速度(使用的场景{text}){num2} 功法速度{n} 资质速度{num3} 金丹加成{jieDanSkillAddExp} 天赋加成{num5}" + $"\n闭关速度结算:((({n}*{num2}*{num})+{num3})*{jieDanSkillAddExp})*(1+{num5})={num4}"));
			}
			return num4;
		}
		return 0f;
	}

	public int GetBiGuanMaxTime()
	{
		Avatar player = Tools.instance.getPlayer();
		string screenName = Tools.getScreenName();
		DateTime residueTime = player.zulinContorl.getResidueTime(screenName);
		int num = (int)((float)((ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n - player.exp) / (GetBiguanSpeed(log: false, UIBiGuanPanel.Inst.BiGuanType) + player.getTimeExpSpeed())) + 1;
		int timeSum = ZulinContorl.GetTimeSum(residueTime);
		if (player.level == 15 && (int)player.exp >= LevelUpDataJsonData.DataDict[15].MaxExp)
		{
			return Mathf.Min(1200, timeSum);
		}
		return Mathf.Min(Mathf.Min(240, timeSum), num);
	}

	private static string GetZiZhiStr(int zizhi)
	{
		int i;
		for (i = 0; i < ziZhiNum.Length && zizhi >= ziZhiNum[i]; i++)
		{
		}
		return ziZhiStr[i - 1];
	}

	public void OnStartBiGuanClick()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		if (BiGuanTime != 0)
		{
			Tools.instance.playFader("正在闭关苦修...", (UnityAction)delegate
			{
				RefreshUI();
			});
			((MonoBehaviour)this).Invoke("OkEvent", 0.5f);
		}
	}

	private void OkEvent()
	{
		Avatar player = PlayerEx.Player;
		int num = BiGuanTime / 12;
		int num2 = BiGuanTime % 12;
		player.AddTime(0, num2, num);
		if (player.getStaticID() != 0)
		{
			player.addEXP((int)((float)(num * 12 + num2) * GetBiguanSpeed(log: false, UIBiGuanPanel.Inst.BiGuanType)));
		}
		CalcShuangXiu(BiGuanTime);
		player.HP = player.HP_Max;
		try
		{
			player.wuDaoMag.autoAddBiGuanLingGuang(num2 + num * 12);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void CalcShuangXiu(int biGuanTime)
	{
		Avatar player = PlayerEx.Player;
		if (!player.ShuangXiuData.HasField("JingYuan"))
		{
			return;
		}
		JSONObject jSONObject = player.ShuangXiuData["JingYuan"];
		int i = jSONObject["Count"].I;
		ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[jSONObject["Skill"].I];
		int i2 = jSONObject["PinJie"].I;
		int i3 = jSONObject["Reward"].I;
		i -= biGuanTime * ShuangXiuLianHuaSuDu.DataDict[i2].speed;
		if (i <= 0)
		{
			if (shuangXiuMiShu.ningliantype == 1)
			{
				player.addEXP(i3);
			}
			else if (shuangXiuMiShu.ningliantype == 2)
			{
				player.xinjin += i3;
			}
			else if (shuangXiuMiShu.ningliantype == 3)
			{
				player.addShenShi(i3);
			}
			else if (shuangXiuMiShu.ningliantype == 4)
			{
				player._HP_Max += i3;
			}
			player.ShuangXiuData.RemoveField("JingYuan");
		}
		else
		{
			player.ShuangXiuData["JingYuan"].SetField("Count", i);
		}
	}

	public void OnLeftTimeBtnClick()
	{
		Slider timeSlider = TimeSlider;
		float value = timeSlider.value;
		timeSlider.value = value - 1f;
	}

	public void OnRightTimeBtnClick()
	{
		Slider timeSlider = TimeSlider;
		float value = timeSlider.value;
		timeSlider.value = value + 1f;
	}

	public void OnTimeSliderValueChanged(float value)
	{
		BiGuanTime = (int)value;
	}
}
