using System.Collections.Generic;
using UnityEngine;

public class MainUIPlayerInfo : MonoBehaviour
{
	public static MainUIPlayerInfo inst;

	public MainUIInfoCell zizhi;

	public MainUIInfoCell wuxin;

	public MainUIInfoCell shenshi;

	public MainUIInfoCell dunsu;

	public MainUIInfoCell lingshi;

	public MainUIInfoCell qixue;

	public MainUIInfoCell shouyuan;

	public MainUIInfoCell lingen;

	public MainUIInfoCell xinjing;

	public PlayerSetRandomFace playerFace;

	public int linggenNum;

	public List<int> lingGenList;

	public int canSelectLinGenNum = 5;

	public int sex = 1;

	public string playerName = "";

	public string firstName = "";

	public string lastName = "";

	private void Awake()
	{
		inst = this;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData["1"];
		zizhi.baseNum = jSONObject["ziZhi"].I;
		zizhi.curNum = jSONObject["ziZhi"].I;
		zizhi.UpdateNum(zizhi.curNum.ToString());
		zizhi.desc = jsonData.instance.TianFuDescJsonData["1"]["Desc"].Str;
		zizhi.desc = zizhi.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		wuxin.baseNum = jSONObject["wuXin"].I;
		wuxin.curNum = jSONObject["wuXin"].I;
		wuxin.UpdateNum(wuxin.curNum.ToString());
		wuxin.desc = jsonData.instance.TianFuDescJsonData["3"]["Desc"].Str;
		wuxin.desc = wuxin.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		shenshi.baseNum = jSONObject["shengShi"].I;
		shenshi.curNum = jSONObject["shengShi"].I;
		shenshi.UpdateNum(shenshi.curNum.ToString());
		shenshi.desc = jsonData.instance.TianFuDescJsonData["5"]["Desc"].Str;
		shenshi.desc = shenshi.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		dunsu.baseNum = jSONObject["dunSu"].I;
		dunsu.curNum = jSONObject["dunSu"].I;
		dunsu.UpdateNum(dunsu.curNum.ToString());
		dunsu.desc = jsonData.instance.TianFuDescJsonData["4"]["Desc"].Str;
		dunsu.desc = dunsu.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		lingshi.baseNum = jSONObject["MoneyType"].I;
		lingshi.curNum = jSONObject["MoneyType"].I;
		lingshi.UpdateNum(lingshi.curNum.ToString());
		lingshi.desc = jsonData.instance.TianFuDescJsonData["7"]["Desc"].Str;
		lingshi.desc = lingshi.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		qixue.baseNum = jSONObject["HP"].I;
		qixue.curNum = jSONObject["HP"].I;
		qixue.UpdateNum(qixue.curNum.ToString());
		qixue.desc = jsonData.instance.TianFuDescJsonData["8"]["Desc"].Str;
		qixue.desc = qixue.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		shouyuan.baseNum = jSONObject["shouYuan"].I;
		shouyuan.curNum = jSONObject["shouYuan"].I;
		shouyuan.UpdateNum(shouyuan.curNum.ToString());
		shouyuan.desc = jsonData.instance.TianFuDescJsonData["9"]["Desc"].Str;
		shouyuan.desc = shouyuan.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		lingen.baseNum = 5;
		lingen.curNum = 5;
		lingen.UpdateNum(jsonData.instance.LinGenZiZhiJsonData[lingen.curNum.ToString()]["Title"].Str, isLinGen: true);
		lingen.desc = jsonData.instance.TianFuDescJsonData["2"]["Desc"].Str;
		lingen.desc = lingen.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
		xinjing.baseNum = 0;
		xinjing.curNum = 0;
		xinjing.UpdateNum(jsonData.instance.XinJinJsonData[GetXinJinLevel(xinjing.curNum).ToString()]["Text"].Str + $"（{xinjing.curNum}）");
		xinjing.desc = jsonData.instance.TianFuDescJsonData["6"]["Desc"].Str;
		xinjing.desc = xinjing.desc.Replace("[24a5d6]", "<color=#24a5d6>").Replace("[-]", "</color>");
	}

	public void UpdataBase()
	{
		GetZiZhi();
		GetWuXin();
		GetShenShi();
		GetDunSu();
		GetLinShi();
		GetHp();
		GetShouYuan();
		GetLinGenNum();
		GetXinJing();
	}

	public int GetZiZhi()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		zizhi.curNum = zizhi.baseNum;
		if (hasSelectSeidList.ContainsKey(1))
		{
			foreach (int item in hasSelectSeidList[1])
			{
				zizhi.curNum += jsonData.instance.CrateAvatarSeidJsonData[1][item.ToString()]["value1"].I;
			}
		}
		zizhi.UpdateNum(zizhi.curNum.ToString());
		return zizhi.curNum;
	}

	public int GetWuXin()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		wuxin.curNum = wuxin.baseNum;
		if (hasSelectSeidList.ContainsKey(2))
		{
			foreach (int item in hasSelectSeidList[2])
			{
				wuxin.curNum += jsonData.instance.CrateAvatarSeidJsonData[2][item.ToString()]["value1"].I;
			}
		}
		wuxin.UpdateNum(wuxin.curNum.ToString());
		return wuxin.curNum;
	}

	public int GetShenShi()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		shenshi.curNum = shenshi.baseNum;
		if (hasSelectSeidList.ContainsKey(6))
		{
			foreach (int item in hasSelectSeidList[6])
			{
				shenshi.curNum += jsonData.instance.CrateAvatarSeidJsonData[6][item.ToString()]["value1"].I;
			}
		}
		shenshi.UpdateNum(shenshi.curNum.ToString());
		return shenshi.curNum;
	}

	public int GetDunSu()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		dunsu.curNum = dunsu.baseNum;
		if (hasSelectSeidList.ContainsKey(7))
		{
			foreach (int item in hasSelectSeidList[7])
			{
				dunsu.curNum += jsonData.instance.CrateAvatarSeidJsonData[7][item.ToString()]["value1"].I;
			}
		}
		dunsu.UpdateNum(dunsu.curNum.ToString());
		return dunsu.curNum;
	}

	public int GetLinShi()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		lingshi.curNum = lingshi.baseNum;
		if (hasSelectSeidList.ContainsKey(14))
		{
			foreach (int item in hasSelectSeidList[14])
			{
				lingshi.curNum += jsonData.instance.CrateAvatarSeidJsonData[14][item.ToString()]["value1"].I;
			}
		}
		lingshi.UpdateNum(lingshi.curNum.ToString());
		return lingshi.curNum;
	}

	public int GetHp()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		qixue.curNum = qixue.baseNum;
		if (hasSelectSeidList.ContainsKey(8))
		{
			foreach (int item in hasSelectSeidList[8])
			{
				qixue.curNum += jsonData.instance.CrateAvatarSeidJsonData[8][item.ToString()]["value1"].I;
			}
		}
		qixue.UpdateNum(qixue.curNum.ToString());
		return qixue.curNum;
	}

	public int GetShouYuan()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		shouyuan.curNum = shouyuan.baseNum;
		if (hasSelectSeidList.ContainsKey(5))
		{
			foreach (int item in hasSelectSeidList[5])
			{
				shouyuan.curNum += jsonData.instance.CrateAvatarSeidJsonData[5][item.ToString()]["value1"].I;
			}
		}
		shouyuan.UpdateNum(shouyuan.curNum.ToString());
		return shouyuan.curNum;
	}

	public int GetLinGenNum()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		if (hasSelectSeidList.ContainsKey(3))
		{
			foreach (int item in hasSelectSeidList[3])
			{
				lingen.curNum = jsonData.instance.CrateAvatarSeidJsonData[3][item.ToString()]["value1"].I;
			}
		}
		linggenNum = lingen.curNum;
		lingen.UpdateNum(jsonData.instance.LinGenZiZhiJsonData[lingen.curNum.ToString()]["Title"].Str, isLinGen: true);
		return lingen.curNum;
	}

	public int GetXinJing()
	{
		Dictionary<int, List<int>> hasSelectSeidList = MainUIMag.inst.createAvatarPanel.setTianFu.hasSelectSeidList;
		xinjing.curNum = xinjing.baseNum;
		if (hasSelectSeidList.ContainsKey(4))
		{
			foreach (int item in hasSelectSeidList[4])
			{
				xinjing.curNum += jsonData.instance.CrateAvatarSeidJsonData[4][item.ToString()]["value1"].I;
			}
		}
		xinjing.UpdateNum(jsonData.instance.XinJinJsonData[GetXinJinLevel(xinjing.curNum).ToString()]["Text"].Str + $"（{xinjing.curNum}）");
		return xinjing.curNum;
	}

	public List<int> GetLingGen()
	{
		List<int> list = new List<int>();
		foreach (int lingGen in lingGenList)
		{
			list.Add(lingGen);
		}
		return list;
	}

	private int GetXinJinLevel(int curNum)
	{
		foreach (JSONObject item in jsonData.instance.XinJinJsonData.list)
		{
			if (item["Max"].I > curNum)
			{
				return item["id"].I;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}
}
