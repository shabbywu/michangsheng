using System;
using KBEngine;

// Token: 0x020003BB RID: 955
public class SiXuData
{
	// Token: 0x06001A76 RID: 6774 RVA: 0x000E9AB0 File Offset: 0x000E7CB0
	public SiXuData(JSONObject json)
	{
		this.Init(json);
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000E9B08 File Offset: 0x000E7D08
	public void Init(JSONObject json)
	{
		this.info = json;
		this.ShuoMing = this.info["desc"].Str;
		Avatar player = Tools.instance.getPlayer();
		DateTime endTime = DateTime.Parse(this.info["startTime"].str).AddDays((double)this.info["guoqiTime"].I);
		DateTime shengYuShiJian = Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime);
		this.ShengYuTimeFull = Tools.TimeToShengYuTime(shengYuShiJian, "");
		if (shengYuShiJian.Year > 1)
		{
			this.ShengYuTime = string.Format("剩{0}年", shengYuShiJian.Year - 1);
		}
		else if (shengYuShiJian.Month > 1)
		{
			this.ShengYuTime = string.Format("剩{0}月", shengYuShiJian.Month - 1);
		}
		else
		{
			this.ShengYuTime = string.Format("剩{0}日", shengYuShiJian.Day);
		}
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow((double)wuXin, 3.0) - 0.0042f * (float)Math.Pow((double)wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		int day = (int)(json["studyTime"].n / num);
		this.XiaoHao = string.Format("{0}年{1}月{2}日", Tools.DayToYear(day), Tools.DayToMonth(day), Tools.DayToDay(day));
		this.WuDaoType = json["type"].I;
		if (this.WuDaoType > 10)
		{
			this.wuDaoFilter = this.WuDaoType - 10;
		}
		else
		{
			this.wuDaoFilter = this.WuDaoType;
		}
		this.WuDaoTypeStr = jsonData.instance.WuDaoAllTypeJson[this.WuDaoType.ToString()]["name"].Str;
		this.PinJie = json["quality"].I;
		string text = this.pinJieColors[this.PinJie - 1];
		this.PinJieStr = string.Concat(new string[]
		{
			"<color=#",
			text,
			">",
			this.PinJie.ToCNNumber(),
			"品</color>"
		});
		this.XiaoGuo = string.Concat(new object[]
		{
			"对",
			this.WuDaoTypeStr,
			"之道的感悟提升<color=#",
			text,
			">",
			(float)json["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + json["quality"].I].n,
			"</color>点"
		});
	}

	// Token: 0x040015DD RID: 5597
	public JSONObject info;

	// Token: 0x040015DE RID: 5598
	public string XiaoGuo;

	// Token: 0x040015DF RID: 5599
	public string ShengYuTimeFull;

	// Token: 0x040015E0 RID: 5600
	public string ShengYuTime;

	// Token: 0x040015E1 RID: 5601
	public string XiaoHao;

	// Token: 0x040015E2 RID: 5602
	public string ShuoMing;

	// Token: 0x040015E3 RID: 5603
	public int PinJie;

	// Token: 0x040015E4 RID: 5604
	public string PinJieStr;

	// Token: 0x040015E5 RID: 5605
	public int wuDaoFilter;

	// Token: 0x040015E6 RID: 5606
	public int WuDaoType;

	// Token: 0x040015E7 RID: 5607
	public string WuDaoTypeStr;

	// Token: 0x040015E8 RID: 5608
	private string[] pinJieColors = new string[]
	{
		"79796E",
		"4A7914",
		"097689",
		"A63FAF",
		"CB741C",
		"CD271E"
	};
}
