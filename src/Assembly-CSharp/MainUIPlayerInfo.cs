using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200048F RID: 1167
public class MainUIPlayerInfo : MonoBehaviour
{
	// Token: 0x06001F1D RID: 7965 RVA: 0x0010BB54 File Offset: 0x00109D54
	private void Awake()
	{
		MainUIPlayerInfo.inst = this;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData["1"];
		this.zizhi.baseNum = jsonobject["ziZhi"].I;
		this.zizhi.curNum = jsonobject["ziZhi"].I;
		this.zizhi.UpdateNum(this.zizhi.curNum.ToString(), false);
		this.zizhi.desc = jsonData.instance.TianFuDescJsonData["1"]["Desc"].Str;
		this.zizhi.desc = this.zizhi.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.wuxin.baseNum = jsonobject["wuXin"].I;
		this.wuxin.curNum = jsonobject["wuXin"].I;
		this.wuxin.UpdateNum(this.wuxin.curNum.ToString(), false);
		this.wuxin.desc = jsonData.instance.TianFuDescJsonData["3"]["Desc"].Str;
		this.wuxin.desc = this.wuxin.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.shenshi.baseNum = jsonobject["shengShi"].I;
		this.shenshi.curNum = jsonobject["shengShi"].I;
		this.shenshi.UpdateNum(this.shenshi.curNum.ToString(), false);
		this.shenshi.desc = jsonData.instance.TianFuDescJsonData["5"]["Desc"].Str;
		this.shenshi.desc = this.shenshi.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.dunsu.baseNum = jsonobject["dunSu"].I;
		this.dunsu.curNum = jsonobject["dunSu"].I;
		this.dunsu.UpdateNum(this.dunsu.curNum.ToString(), false);
		this.dunsu.desc = jsonData.instance.TianFuDescJsonData["4"]["Desc"].Str;
		this.dunsu.desc = this.dunsu.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.lingshi.baseNum = jsonobject["MoneyType"].I;
		this.lingshi.curNum = jsonobject["MoneyType"].I;
		this.lingshi.UpdateNum(this.lingshi.curNum.ToString(), false);
		this.lingshi.desc = jsonData.instance.TianFuDescJsonData["7"]["Desc"].Str;
		this.lingshi.desc = this.lingshi.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.qixue.baseNum = jsonobject["HP"].I;
		this.qixue.curNum = jsonobject["HP"].I;
		this.qixue.UpdateNum(this.qixue.curNum.ToString(), false);
		this.qixue.desc = jsonData.instance.TianFuDescJsonData["8"]["Desc"].Str;
		this.qixue.desc = this.qixue.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.shouyuan.baseNum = jsonobject["shouYuan"].I;
		this.shouyuan.curNum = jsonobject["shouYuan"].I;
		this.shouyuan.UpdateNum(this.shouyuan.curNum.ToString(), false);
		this.shouyuan.desc = jsonData.instance.TianFuDescJsonData["9"]["Desc"].Str;
		this.shouyuan.desc = this.shouyuan.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.lingen.baseNum = 5;
		this.lingen.curNum = 5;
		this.lingen.UpdateNum(jsonData.instance.LinGenZiZhiJsonData[this.lingen.curNum.ToString()]["Title"].Str, true);
		this.lingen.desc = jsonData.instance.TianFuDescJsonData["2"]["Desc"].Str;
		this.lingen.desc = this.lingen.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		this.xinjing.baseNum = 0;
		this.xinjing.curNum = 0;
		this.xinjing.UpdateNum(jsonData.instance.XinJinJsonData[this.GetXinJinLevel(this.xinjing.curNum).ToString()]["Text"].Str + string.Format("（{0}）", this.xinjing.curNum), false);
		this.xinjing.desc = jsonData.instance.TianFuDescJsonData["6"]["Desc"].Str;
		this.xinjing.desc = this.xinjing.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x0010C1F8 File Offset: 0x0010A3F8
	public void UpdataBase()
	{
		this.GetZiZhi();
		this.GetWuXin();
		this.GetShenShi();
		this.GetDunSu();
		this.GetLinShi();
		this.GetHp();
		this.GetShouYuan();
		this.GetLinGenNum();
		this.GetXinJing();
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x0010C244 File Offset: 0x0010A444
	public int GetZiZhi()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.zizhi.curNum = this.zizhi.baseNum;
		if (hasSelectSeidList.ContainsKey(1))
		{
			foreach (int num in hasSelectSeidList[1])
			{
				this.zizhi.curNum += jsonData.instance.CrateAvatarSeidJsonData[1][num.ToString()]["value1"].I;
			}
		}
		this.zizhi.UpdateNum(this.zizhi.curNum.ToString(), false);
		return this.zizhi.curNum;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0010C328 File Offset: 0x0010A528
	public int GetWuXin()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.wuxin.curNum = this.wuxin.baseNum;
		if (hasSelectSeidList.ContainsKey(2))
		{
			foreach (int num in hasSelectSeidList[2])
			{
				this.wuxin.curNum += jsonData.instance.CrateAvatarSeidJsonData[2][num.ToString()]["value1"].I;
			}
		}
		this.wuxin.UpdateNum(this.wuxin.curNum.ToString(), false);
		return this.wuxin.curNum;
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x0010C40C File Offset: 0x0010A60C
	public int GetShenShi()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.shenshi.curNum = this.shenshi.baseNum;
		if (hasSelectSeidList.ContainsKey(6))
		{
			foreach (int num in hasSelectSeidList[6])
			{
				this.shenshi.curNum += jsonData.instance.CrateAvatarSeidJsonData[6][num.ToString()]["value1"].I;
			}
		}
		this.shenshi.UpdateNum(this.shenshi.curNum.ToString(), false);
		return this.shenshi.curNum;
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x0010C4F0 File Offset: 0x0010A6F0
	public int GetDunSu()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.dunsu.curNum = this.dunsu.baseNum;
		if (hasSelectSeidList.ContainsKey(7))
		{
			foreach (int num in hasSelectSeidList[7])
			{
				this.dunsu.curNum += jsonData.instance.CrateAvatarSeidJsonData[7][num.ToString()]["value1"].I;
			}
		}
		this.dunsu.UpdateNum(this.dunsu.curNum.ToString(), false);
		return this.dunsu.curNum;
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x0010C5D4 File Offset: 0x0010A7D4
	public int GetLinShi()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.lingshi.curNum = this.lingshi.baseNum;
		if (hasSelectSeidList.ContainsKey(14))
		{
			foreach (int num in hasSelectSeidList[14])
			{
				this.lingshi.curNum += jsonData.instance.CrateAvatarSeidJsonData[14][num.ToString()]["value1"].I;
			}
		}
		this.lingshi.UpdateNum(this.lingshi.curNum.ToString(), false);
		return this.lingshi.curNum;
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x0010C6B8 File Offset: 0x0010A8B8
	public int GetHp()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.qixue.curNum = this.qixue.baseNum;
		if (hasSelectSeidList.ContainsKey(8))
		{
			foreach (int num in hasSelectSeidList[8])
			{
				this.qixue.curNum += jsonData.instance.CrateAvatarSeidJsonData[8][num.ToString()]["value1"].I;
			}
		}
		this.qixue.UpdateNum(this.qixue.curNum.ToString(), false);
		return this.qixue.curNum;
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x0010C79C File Offset: 0x0010A99C
	public int GetShouYuan()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.shouyuan.curNum = this.shouyuan.baseNum;
		if (hasSelectSeidList.ContainsKey(5))
		{
			foreach (int num in hasSelectSeidList[5])
			{
				this.shouyuan.curNum += jsonData.instance.CrateAvatarSeidJsonData[5][num.ToString()]["value1"].I;
			}
		}
		this.shouyuan.UpdateNum(this.shouyuan.curNum.ToString(), false);
		return this.shouyuan.curNum;
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x0010C880 File Offset: 0x0010AA80
	public int GetLinGenNum()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		if (hasSelectSeidList.ContainsKey(3))
		{
			foreach (int num in hasSelectSeidList[3])
			{
				this.lingen.curNum = jsonData.instance.CrateAvatarSeidJsonData[3][num.ToString()]["value1"].I;
			}
		}
		this.linggenNum = this.lingen.curNum;
		this.lingen.UpdateNum(jsonData.instance.LinGenZiZhiJsonData[this.lingen.curNum.ToString()]["Title"].Str, true);
		return this.lingen.curNum;
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x0010C974 File Offset: 0x0010AB74
	public int GetXinJing()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		this.xinjing.curNum = this.xinjing.baseNum;
		if (hasSelectSeidList.ContainsKey(4))
		{
			foreach (int num in hasSelectSeidList[4])
			{
				this.xinjing.curNum += jsonData.instance.CrateAvatarSeidJsonData[4][num.ToString()]["value1"].I;
			}
		}
		this.xinjing.UpdateNum(jsonData.instance.XinJinJsonData[this.GetXinJinLevel(this.xinjing.curNum).ToString()]["Text"].Str + string.Format("（{0}）", this.xinjing.curNum), false);
		return this.xinjing.curNum;
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x0010CA9C File Offset: 0x0010AC9C
	public List<int> GetLingGen()
	{
		List<int> list = new List<int>();
		foreach (int item in this.lingGenList)
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x0010CAF8 File Offset: 0x0010ACF8
	private int GetXinJinLevel(int curNum)
	{
		foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
		{
			if (jsonobject["Max"].I > curNum)
			{
				return (int)jsonobject["id"].n;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}

	// Token: 0x04001A95 RID: 6805
	public static MainUIPlayerInfo inst;

	// Token: 0x04001A96 RID: 6806
	public MainUIInfoCell zizhi;

	// Token: 0x04001A97 RID: 6807
	public MainUIInfoCell wuxin;

	// Token: 0x04001A98 RID: 6808
	public MainUIInfoCell shenshi;

	// Token: 0x04001A99 RID: 6809
	public MainUIInfoCell dunsu;

	// Token: 0x04001A9A RID: 6810
	public MainUIInfoCell lingshi;

	// Token: 0x04001A9B RID: 6811
	public MainUIInfoCell qixue;

	// Token: 0x04001A9C RID: 6812
	public MainUIInfoCell shouyuan;

	// Token: 0x04001A9D RID: 6813
	public MainUIInfoCell lingen;

	// Token: 0x04001A9E RID: 6814
	public MainUIInfoCell xinjing;

	// Token: 0x04001A9F RID: 6815
	public PlayerSetRandomFace playerFace;

	// Token: 0x04001AA0 RID: 6816
	public int linggenNum;

	// Token: 0x04001AA1 RID: 6817
	public List<int> lingGenList;

	// Token: 0x04001AA2 RID: 6818
	public int canSelectLinGenNum = 5;

	// Token: 0x04001AA3 RID: 6819
	public int sex = 1;

	// Token: 0x04001AA4 RID: 6820
	public string playerName = "";

	// Token: 0x04001AA5 RID: 6821
	public string firstName = "";

	// Token: 0x04001AA6 RID: 6822
	public string lastName = "";
}
