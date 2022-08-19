using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class MainUIPlayerInfo : MonoBehaviour
{
	// Token: 0x06001BDB RID: 7131 RVA: 0x000C6A00 File Offset: 0x000C4C00
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

	// Token: 0x06001BDC RID: 7132 RVA: 0x000C70A4 File Offset: 0x000C52A4
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

	// Token: 0x06001BDD RID: 7133 RVA: 0x000C70F0 File Offset: 0x000C52F0
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

	// Token: 0x06001BDE RID: 7134 RVA: 0x000C71D4 File Offset: 0x000C53D4
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

	// Token: 0x06001BDF RID: 7135 RVA: 0x000C72B8 File Offset: 0x000C54B8
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

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000C739C File Offset: 0x000C559C
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

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000C7480 File Offset: 0x000C5680
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

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000C7564 File Offset: 0x000C5764
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

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000C7648 File Offset: 0x000C5848
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

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000C772C File Offset: 0x000C592C
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

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000C7820 File Offset: 0x000C5A20
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

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000C7948 File Offset: 0x000C5B48
	public List<int> GetLingGen()
	{
		List<int> list = new List<int>();
		foreach (int item in this.lingGenList)
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000C79A4 File Offset: 0x000C5BA4
	private int GetXinJinLevel(int curNum)
	{
		foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
		{
			if (jsonobject["Max"].I > curNum)
			{
				return jsonobject["id"].I;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}

	// Token: 0x04001670 RID: 5744
	public static MainUIPlayerInfo inst;

	// Token: 0x04001671 RID: 5745
	public MainUIInfoCell zizhi;

	// Token: 0x04001672 RID: 5746
	public MainUIInfoCell wuxin;

	// Token: 0x04001673 RID: 5747
	public MainUIInfoCell shenshi;

	// Token: 0x04001674 RID: 5748
	public MainUIInfoCell dunsu;

	// Token: 0x04001675 RID: 5749
	public MainUIInfoCell lingshi;

	// Token: 0x04001676 RID: 5750
	public MainUIInfoCell qixue;

	// Token: 0x04001677 RID: 5751
	public MainUIInfoCell shouyuan;

	// Token: 0x04001678 RID: 5752
	public MainUIInfoCell lingen;

	// Token: 0x04001679 RID: 5753
	public MainUIInfoCell xinjing;

	// Token: 0x0400167A RID: 5754
	public PlayerSetRandomFace playerFace;

	// Token: 0x0400167B RID: 5755
	public int linggenNum;

	// Token: 0x0400167C RID: 5756
	public List<int> lingGenList;

	// Token: 0x0400167D RID: 5757
	public int canSelectLinGenNum = 5;

	// Token: 0x0400167E RID: 5758
	public int sex = 1;

	// Token: 0x0400167F RID: 5759
	public string playerName = "";

	// Token: 0x04001680 RID: 5760
	public string firstName = "";

	// Token: 0x04001681 RID: 5761
	public string lastName = "";
}
