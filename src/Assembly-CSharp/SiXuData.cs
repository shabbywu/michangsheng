using System;
using KBEngine;

// Token: 0x0200028D RID: 653
public class SiXuData
{
	// Token: 0x06001799 RID: 6041 RVA: 0x000A26B8 File Offset: 0x000A08B8
	public SiXuData(JSONObject json)
	{
		this.Init(json);
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000A2710 File Offset: 0x000A0910
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

	// Token: 0x0400125A RID: 4698
	public JSONObject info;

	// Token: 0x0400125B RID: 4699
	public string XiaoGuo;

	// Token: 0x0400125C RID: 4700
	public string ShengYuTimeFull;

	// Token: 0x0400125D RID: 4701
	public string ShengYuTime;

	// Token: 0x0400125E RID: 4702
	public string XiaoHao;

	// Token: 0x0400125F RID: 4703
	public string ShuoMing;

	// Token: 0x04001260 RID: 4704
	public int PinJie;

	// Token: 0x04001261 RID: 4705
	public string PinJieStr;

	// Token: 0x04001262 RID: 4706
	public int wuDaoFilter;

	// Token: 0x04001263 RID: 4707
	public int WuDaoType;

	// Token: 0x04001264 RID: 4708
	public string WuDaoTypeStr;

	// Token: 0x04001265 RID: 4709
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
