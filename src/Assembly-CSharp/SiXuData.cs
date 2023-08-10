using System;
using KBEngine;

public class SiXuData
{
	public JSONObject info;

	public string XiaoGuo;

	public string ShengYuTimeFull;

	public string ShengYuTime;

	public string XiaoHao;

	public string ShuoMing;

	public int PinJie;

	public string PinJieStr;

	public int wuDaoFilter;

	public int WuDaoType;

	public string WuDaoTypeStr;

	private string[] pinJieColors = new string[6] { "79796E", "4A7914", "097689", "A63FAF", "CB741C", "CD271E" };

	public SiXuData(JSONObject json)
	{
		Init(json);
	}

	public void Init(JSONObject json)
	{
		info = json;
		ShuoMing = info["desc"].Str;
		Avatar player = Tools.instance.getPlayer();
		DateTime check = Tools.getShengYuShiJian(endTime: DateTime.Parse(info["startTime"].str).AddDays(info["guoqiTime"].I), nowTime: player.worldTimeMag.getNowTime());
		ShengYuTimeFull = Tools.TimeToShengYuTime(check, "");
		if (check.Year > 1)
		{
			ShengYuTime = $"剩{check.Year - 1}年";
		}
		else if (check.Month > 1)
		{
			ShengYuTime = $"剩{check.Month - 1}月";
		}
		else
		{
			ShengYuTime = $"剩{check.Day}日";
		}
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow(wuXin, 3.0) - 0.0042f * (float)Math.Pow(wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		int day = (int)(json["studyTime"].n / num);
		XiaoHao = $"{Tools.DayToYear(day)}年{Tools.DayToMonth(day)}月{Tools.DayToDay(day)}日";
		WuDaoType = json["type"].I;
		if (WuDaoType > 10)
		{
			wuDaoFilter = WuDaoType - 10;
		}
		else
		{
			wuDaoFilter = WuDaoType;
		}
		WuDaoTypeStr = jsonData.instance.WuDaoAllTypeJson[WuDaoType.ToString()]["name"].Str;
		PinJie = json["quality"].I;
		string text = pinJieColors[PinJie - 1];
		PinJieStr = "<color=#" + text + ">" + PinJie.ToCNNumber() + "品</color>";
		XiaoGuo = "对" + WuDaoTypeStr + "之道的感悟提升<color=#" + text + ">" + (float)json["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + json["quality"].I].n + "</color>点";
	}
}
