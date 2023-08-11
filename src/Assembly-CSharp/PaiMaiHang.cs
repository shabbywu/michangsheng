using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class PaiMaiHang : ExchangePlan
{
	public int PaiMaiHangID;

	public int ShowPaiMaiAvatar;

	public static PaiMaiHang inst;

	private void Awake()
	{
		init();
		inst = this;
	}

	public bool IsCanJoin()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jSONObject = jsonData.instance.PaiMaiBiao[PaiMaiHangID.ToString()];
		DateTime.Parse(jSONObject["StarTime"].str);
		DateTime.Parse(jSONObject["EndTime"].str);
		int num = (int)jSONObject["circulation"].n;
		return IsCanJoin(player.worldTimeMag.getNowTime().Year / num, PaiMaiHangID, "paiMai");
	}

	public static bool IsCanJoin(int DijiJIe, int paimaihangID, string JsonName)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.CanJiaPaiMai.HasField(JsonName + paimaihangID))
		{
			foreach (JSONObject item in player.CanJiaPaiMai[JsonName + paimaihangID].list)
			{
				if ((int)item.n == DijiJIe)
				{
					return false;
				}
			}
		}
		return true;
	}

	public void JiMai()
	{
		JSONObject jSONObject = jsonData.instance.PaiMaiBiao[PaiMaiHangID.ToString()];
		DateTime startTime = DateTime.Parse(jSONObject["StarTime"].str);
		DateTime endTime = DateTime.Parse(jSONObject["EndTime"].str);
		int num = (int)jSONObject["circulation"].n;
		Avatar player = Tools.instance.getPlayer();
		DateTime nowTime = player.worldTimeMag.getNowTime();
		if (!IsCanJoin())
		{
			int num2 = ((num != 1) ? (startTime.Year - num) : startTime.Year);
			if (num != 1 && nowTime.Year % num != 0)
			{
				num2 = startTime.Year - num;
			}
			int num3 = ((num != 1) ? (endTime.Year - num) : endTime.Year);
			if (num != 1 && nowTime.Year % num != 0)
			{
				num3 = endTime.Year - num;
			}
			int num4 = num2 + nowTime.Year - ((num == 1) ? 1 : (nowTime.Year % num)) + num;
			MonstarterSay("下一届拍卖会时间" + num4 + "年" + startTime.Month + "月" + startTime.Day + "日至" + (num3 + nowTime.Year - ((num == 1) ? 1 : (nowTime.Year % num)) + num) + "年" + endTime.Month + "月" + endTime.Day + "日");
		}
		else if (!Tools.instance.IsInTime(player.worldTimeMag.getNowTime(), startTime, endTime, num))
		{
			int num5 = ((nowTime.Month < startTime.Month) ? (startTime.Year - num) : startTime.Year);
			if (num != 1 && nowTime.Year % num != 0)
			{
				num5 = startTime.Year - num;
			}
			else if (num != 1 && nowTime.Year % num == 0)
			{
				if (nowTime.Month < startTime.Month)
				{
					num5 = startTime.Year - num - num;
				}
				else if (nowTime.Month >= endTime.Month)
				{
					num5 = startTime.Year - num;
				}
			}
			int num6 = ((nowTime.Month < startTime.Month) ? (endTime.Year - num) : endTime.Year);
			if (num != 1 && nowTime.Year % num != 0)
			{
				num6 = endTime.Year - num;
			}
			else if (num != 1 && nowTime.Year % num == 0)
			{
				if (nowTime.Month < startTime.Month)
				{
					num6 = endTime.Year - num - num;
				}
				else if (nowTime.Month >= endTime.Month)
				{
					num6 = endTime.Year - num;
				}
			}
			int num7 = num5 + nowTime.Year - ((num == 1) ? 1 : (nowTime.Year % num)) + num;
			int num8 = num6 + nowTime.Year - ((num == 1) ? 1 : (nowTime.Year % num)) + num;
			MonstarterSay("下一届拍卖会时间" + num7 + "年" + startTime.Month + "月" + startTime.Day + "日至" + num8 + "年" + endTime.Month + "月" + endTime.Day + "日");
		}
		else if (player.money >= (ulong)jSONObject["RuChangFei"].n)
		{
			MonstarterSay("入场费" + (ulong)jSONObject["RuChangFei"].n + "灵石，我收下啦。");
			int num9 = 0;
			foreach (item item in inventoryPlayer.inventory)
			{
				if (num9 >= 24 && item.itemID > 0)
				{
					JSONObject jSONObject2 = jsonData.instance.ItemJsonData[item.itemID.ToString()];
					int num10 = (int)jSONObject["Price"].n;
					if (item.Seid != null && item.Seid.HasField("Money"))
					{
						num10 = item.Seid["Money"].I;
					}
					if (num10 * item.itemNum < (int)jSONObject["Price"].n)
					{
						MonstarterSay(Tools.Code64(jSONObject2["name"].str) + "这件物品的总价值，没有达到我们拍卖行的最低价值需求");
						return;
					}
				}
				num9++;
			}
			int num11 = 0;
			foreach (item item2 in inventoryPlayer.inventory)
			{
				if (num11 >= 24 && item2.itemID > 0)
				{
					jsonData.instance.MonstarAddItem(MonstarID, item2.UUID, item2.itemID, item2.itemNum, item2.Seid, 1);
					Tools.instance.getPlayer().removeItem(item2.UUID, item2.itemNum);
				}
				num11++;
			}
			Tools.instance.getPlayer().nowPaiMaiCompereAvatarID = MonstarID;
			Tools.instance.getPlayer().nowPaiMaiID = PaiMaiHangID;
			Avatar player2 = Tools.instance.getPlayer();
			if (!player2.CanJiaPaiMai.HasField("paiMai" + PaiMaiHangID))
			{
				player2.CanJiaPaiMai.AddField("paiMai" + PaiMaiHangID, new JSONObject(JSONObject.Type.ARRAY));
			}
			player2.CanJiaPaiMai["paiMai" + PaiMaiHangID].Add(player.worldTimeMag.getNowTime().Year / num);
			player.money -= (ulong)jSONObject["RuChangFei"].n;
			Tools.startPaiMai();
		}
		else
		{
			MonstarterSay("我们这入场需要" + (ulong)jSONObject["RuChangFei"].n + "灵石。");
		}
	}

	public override void initPlan()
	{
		List<JSONObject> monsatrBackpack = jsonData.instance.GetMonsatrBackpack(MonstarID);
		JSONObject jSONObject = jsonData.instance.PaiMaiBiao[PaiMaiHangID.ToString()];
		int num = (int)jSONObject["circulation"].n;
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		if (monsatrBackpack.Count <= (int)jSONObject["ItemNum"].n)
		{
			jsonData.instance.setMonstarDeath(MonstarID);
		}
		Avatar player = Tools.instance.getPlayer();
		if (!IsCanJoin(nowTime.Year / num, PaiMaiHangID, "paiMai"))
		{
			if (IsCanJoin(nowTime.Year / num + 1, PaiMaiHangID, "paiMaiRandom"))
			{
				if (!player.CanJiaPaiMai.HasField("paiMaiRandom" + PaiMaiHangID))
				{
					player.CanJiaPaiMai.AddField("paiMaiRandom" + PaiMaiHangID, new JSONObject(JSONObject.Type.ARRAY));
				}
				player.CanJiaPaiMai["paiMaiRandom" + PaiMaiHangID].Add(player.worldTimeMag.getNowTime().Year / num + 1);
				jsonData.instance.setMonstarDeath(MonstarID);
			}
		}
		else if (IsCanJoin(nowTime.Year / num, PaiMaiHangID, "paiMaiRandom"))
		{
			if (!player.CanJiaPaiMai.HasField("paiMaiRandom" + PaiMaiHangID))
			{
				player.CanJiaPaiMai.AddField("paiMaiRandom" + PaiMaiHangID, new JSONObject(JSONObject.Type.ARRAY));
			}
			player.CanJiaPaiMai["paiMaiRandom" + PaiMaiHangID].Add(player.worldTimeMag.getNowTime().Year / num);
			jsonData.instance.setMonstarDeath(MonstarID);
		}
		if (monsatrBackpack.Find((JSONObject aa) => (aa.HasField("paiMaiPlayer") && (int)aa["paiMaiPlayer"].n > 1) ? true : false) == null)
		{
			setPaiMaiGoods(MonstarID, (int)jSONObject["ItemNum"].n);
		}
		inventoryPlayer.LoadInventory();
		inventoryMonstar.PaiMaiMonstarLoad(MonstarID);
		playerText.text = Tools.getMonstarTitle(1);
		MonstarText.text = Tools.getMonstarTitle(ShowPaiMaiAvatar);
		MonstarFace.randomAvatar(ShowPaiMaiAvatar);
	}

	public override void close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void setPaiMaiGoods(int monstarID, int Time)
	{
		new List<int>();
		List<JSONObject> monsatrBackpack = jsonData.instance.GetMonsatrBackpack(monstarID);
		for (int i = 0; i < Time; i++)
		{
			int index = jsonData.GetRandom() % monsatrBackpack.Count;
			if (monsatrBackpack[index].HasField("paiMaiPlayer") && (int)monsatrBackpack[index]["paiMaiPlayer"].n == 2)
			{
				i--;
			}
			else if (monsatrBackpack[index].HasField("paiMaiPlayer"))
			{
				monsatrBackpack[index].SetField("paiMaiPlayer", 2);
			}
			else
			{
				monsatrBackpack[index].AddField("paiMaiPlayer", 2);
			}
		}
	}

	public static JSONObject getTimePaiMaiJson(int _PaiMaiID)
	{
		List<JSONObject> staticNumJsonobj = Tools.GetStaticNumJsonobj(jsonData.instance.PaiMaiCanYuAvatar, "PaiMaiID", _PaiMaiID);
		JSONObject jSONObject = jsonData.instance.PaiMaiBiao[_PaiMaiID.ToString()];
		int circulation = (int)jSONObject["circulation"].n;
		JSONObject jSONObject2 = staticNumJsonobj.Find(delegate(JSONObject aa)
		{
			if (aa["StarTime"].str == "" || aa["EndTime"].str == "")
			{
				return false;
			}
			DateTime startTime = DateTime.Parse(aa["StarTime"].str);
			DateTime endTime = DateTime.Parse(aa["EndTime"].str);
			return (Tools.instance.IsInTime(Tools.instance.getPlayer().worldTimeMag.getNowTime(), startTime, endTime) && !IsCanJoin(startTime.Year / circulation, _PaiMaiID, "paiMai")) ? true : false;
		});
		if (jSONObject2 != null)
		{
			return jSONObject2;
		}
		return staticNumJsonobj.Find((JSONObject aa) => aa["EndTime"].str == "" && aa["StarTime"].str == "");
	}
}
