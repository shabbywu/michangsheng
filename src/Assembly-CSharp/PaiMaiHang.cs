using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class PaiMaiHang : ExchangePlan
{
	// Token: 0x06001421 RID: 5153 RVA: 0x00012B44 File Offset: 0x00010D44
	private void Awake()
	{
		base.init();
		PaiMaiHang.inst = this;
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x000B7FC0 File Offset: 0x000B61C0
	public bool IsCanJoin()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jsonobject = jsonData.instance.PaiMaiBiao[this.PaiMaiHangID.ToString()];
		DateTime.Parse(jsonobject["StarTime"].str);
		DateTime.Parse(jsonobject["EndTime"].str);
		int num = (int)jsonobject["circulation"].n;
		return PaiMaiHang.IsCanJoin(player.worldTimeMag.getNowTime().Year / num, this.PaiMaiHangID, "paiMai");
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000B8054 File Offset: 0x000B6254
	public static bool IsCanJoin(int DijiJIe, int paimaihangID, string JsonName)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.CanJiaPaiMai.HasField(JsonName + paimaihangID.ToString()))
		{
			using (List<JSONObject>.Enumerator enumerator = player.CanJiaPaiMai[JsonName + paimaihangID.ToString()].list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((int)enumerator.Current.n == DijiJIe)
					{
						return false;
					}
				}
			}
			return true;
		}
		return true;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000B80EC File Offset: 0x000B62EC
	public void JiMai()
	{
		JSONObject jsonobject = jsonData.instance.PaiMaiBiao[this.PaiMaiHangID.ToString()];
		DateTime startTime = DateTime.Parse(jsonobject["StarTime"].str);
		DateTime endTime = DateTime.Parse(jsonobject["EndTime"].str);
		int num = (int)jsonobject["circulation"].n;
		Avatar player = Tools.instance.getPlayer();
		DateTime nowTime = player.worldTimeMag.getNowTime();
		if (!this.IsCanJoin())
		{
			int num2 = (num != 1) ? (startTime.Year - num) : startTime.Year;
			if (num != 1 && nowTime.Year % num != 0)
			{
				num2 = startTime.Year - num;
			}
			int num3 = (num != 1) ? (endTime.Year - num) : endTime.Year;
			if (num != 1 && nowTime.Year % num != 0)
			{
				num3 = endTime.Year - num;
			}
			int num4 = num2 + nowTime.Year - ((num == 1) ? 1 : (nowTime.Year % num)) + num;
			base.MonstarterSay(string.Concat(new object[]
			{
				"下一届拍卖会时间",
				num4,
				"年",
				startTime.Month,
				"月",
				startTime.Day,
				"日至",
				num3 + nowTime.Year - ((num == 1) ? 1 : (nowTime.Year % num)) + num,
				"年",
				endTime.Month,
				"月",
				endTime.Day,
				"日"
			}));
			return;
		}
		if (!Tools.instance.IsInTime(player.worldTimeMag.getNowTime(), startTime, endTime, num))
		{
			int num5 = (nowTime.Month < startTime.Month) ? (startTime.Year - num) : startTime.Year;
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
			int num6 = (nowTime.Month < startTime.Month) ? (endTime.Year - num) : endTime.Year;
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
			base.MonstarterSay(string.Concat(new object[]
			{
				"下一届拍卖会时间",
				num7,
				"年",
				startTime.Month,
				"月",
				startTime.Day,
				"日至",
				num8,
				"年",
				endTime.Month,
				"月",
				endTime.Day,
				"日"
			}));
			return;
		}
		if (player.money >= (ulong)jsonobject["RuChangFei"].n)
		{
			base.MonstarterSay("入场费" + (ulong)jsonobject["RuChangFei"].n + "灵石，我收下啦。");
			int num9 = 0;
			foreach (item item in this.inventoryPlayer.inventory)
			{
				if (num9 >= 24 && item.itemID > 0)
				{
					JSONObject jsonobject2 = jsonData.instance.ItemJsonData[item.itemID.ToString()];
					int num10 = (int)jsonobject["Price"].n;
					if (item.Seid != null && item.Seid.HasField("Money"))
					{
						num10 = item.Seid["Money"].I;
					}
					if (num10 * item.itemNum < (int)jsonobject["Price"].n)
					{
						base.MonstarterSay(Tools.Code64(jsonobject2["name"].str) + "这件物品的总价值，没有达到我们拍卖行的最低价值需求");
						return;
					}
				}
				num9++;
			}
			int num11 = 0;
			foreach (item item2 in this.inventoryPlayer.inventory)
			{
				if (num11 >= 24 && item2.itemID > 0)
				{
					jsonData.instance.MonstarAddItem(this.MonstarID, item2.UUID, item2.itemID, item2.itemNum, item2.Seid, 1);
					Tools.instance.getPlayer().removeItem(item2.UUID, item2.itemNum);
				}
				num11++;
			}
			Tools.instance.getPlayer().nowPaiMaiCompereAvatarID = this.MonstarID;
			Tools.instance.getPlayer().nowPaiMaiID = this.PaiMaiHangID;
			Avatar player2 = Tools.instance.getPlayer();
			if (!player2.CanJiaPaiMai.HasField("paiMai" + this.PaiMaiHangID.ToString()))
			{
				player2.CanJiaPaiMai.AddField("paiMai" + this.PaiMaiHangID.ToString(), new JSONObject(JSONObject.Type.ARRAY));
			}
			player2.CanJiaPaiMai["paiMai" + this.PaiMaiHangID.ToString()].Add(player.worldTimeMag.getNowTime().Year / num);
			player.money -= (ulong)jsonobject["RuChangFei"].n;
			Tools.startPaiMai();
			return;
		}
		base.MonstarterSay("我们这入场需要" + (ulong)jsonobject["RuChangFei"].n + "灵石。");
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000B87D4 File Offset: 0x000B69D4
	public override void initPlan()
	{
		List<JSONObject> monsatrBackpack = jsonData.instance.GetMonsatrBackpack(this.MonstarID);
		JSONObject jsonobject = jsonData.instance.PaiMaiBiao[this.PaiMaiHangID.ToString()];
		int num = (int)jsonobject["circulation"].n;
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		if (monsatrBackpack.Count <= (int)jsonobject["ItemNum"].n)
		{
			jsonData.instance.setMonstarDeath(this.MonstarID, true);
		}
		Avatar player = Tools.instance.getPlayer();
		if (!PaiMaiHang.IsCanJoin(nowTime.Year / num, this.PaiMaiHangID, "paiMai"))
		{
			if (PaiMaiHang.IsCanJoin(nowTime.Year / num + 1, this.PaiMaiHangID, "paiMaiRandom"))
			{
				if (!player.CanJiaPaiMai.HasField("paiMaiRandom" + this.PaiMaiHangID.ToString()))
				{
					player.CanJiaPaiMai.AddField("paiMaiRandom" + this.PaiMaiHangID.ToString(), new JSONObject(JSONObject.Type.ARRAY));
				}
				player.CanJiaPaiMai["paiMaiRandom" + this.PaiMaiHangID.ToString()].Add(player.worldTimeMag.getNowTime().Year / num + 1);
				jsonData.instance.setMonstarDeath(this.MonstarID, true);
			}
		}
		else if (PaiMaiHang.IsCanJoin(nowTime.Year / num, this.PaiMaiHangID, "paiMaiRandom"))
		{
			if (!player.CanJiaPaiMai.HasField("paiMaiRandom" + this.PaiMaiHangID.ToString()))
			{
				player.CanJiaPaiMai.AddField("paiMaiRandom" + this.PaiMaiHangID.ToString(), new JSONObject(JSONObject.Type.ARRAY));
			}
			player.CanJiaPaiMai["paiMaiRandom" + this.PaiMaiHangID.ToString()].Add(player.worldTimeMag.getNowTime().Year / num);
			jsonData.instance.setMonstarDeath(this.MonstarID, true);
		}
		if (monsatrBackpack.Find((JSONObject aa) => aa.HasField("paiMaiPlayer") && (int)aa["paiMaiPlayer"].n > 1) == null)
		{
			this.setPaiMaiGoods(this.MonstarID, (int)jsonobject["ItemNum"].n);
		}
		this.inventoryPlayer.LoadInventory();
		this.inventoryMonstar.PaiMaiMonstarLoad(this.MonstarID);
		this.playerText.text = Tools.getMonstarTitle(1);
		this.MonstarText.text = Tools.getMonstarTitle(this.ShowPaiMaiAvatar);
		this.MonstarFace.randomAvatar(this.ShowPaiMaiAvatar);
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000111B3 File Offset: 0x0000F3B3
	public override void close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000B8A8C File Offset: 0x000B6C8C
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

	// Token: 0x06001428 RID: 5160 RVA: 0x000B8B34 File Offset: 0x000B6D34
	public static JSONObject getTimePaiMaiJson(int _PaiMaiID)
	{
		List<JSONObject> staticNumJsonobj = Tools.GetStaticNumJsonobj(jsonData.instance.PaiMaiCanYuAvatar, "PaiMaiID", _PaiMaiID);
		JSONObject jsonobject = jsonData.instance.PaiMaiBiao[_PaiMaiID.ToString()];
		int circulation = (int)jsonobject["circulation"].n;
		JSONObject jsonobject2 = staticNumJsonobj.Find(delegate(JSONObject aa)
		{
			if (aa["StarTime"].str == "" || aa["EndTime"].str == "")
			{
				return false;
			}
			DateTime startTime = DateTime.Parse(aa["StarTime"].str);
			DateTime endTime = DateTime.Parse(aa["EndTime"].str);
			return Tools.instance.IsInTime(Tools.instance.getPlayer().worldTimeMag.getNowTime(), startTime, endTime, 0) && !PaiMaiHang.IsCanJoin(startTime.Year / circulation, _PaiMaiID, "paiMai");
		});
		if (jsonobject2 != null)
		{
			return jsonobject2;
		}
		return staticNumJsonobj.Find((JSONObject aa) => aa["EndTime"].str == "" && aa["StarTime"].str == "");
	}

	// Token: 0x04000FA6 RID: 4006
	public int PaiMaiHangID;

	// Token: 0x04000FA7 RID: 4007
	public int ShowPaiMaiAvatar;

	// Token: 0x04000FA8 RID: 4008
	public static PaiMaiHang inst;
}
