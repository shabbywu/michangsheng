using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai;

[Serializable]
public class PaiMaiAvatar
{
	public enum StateType
	{
		放弃 = 1,
		略感兴趣,
		跃跃欲试,
		势在必得,
		所有状态
	}

	public int NpcId;

	public string Name;

	public string Title;

	public int Money;

	public int Level;

	public int ItemLevel;

	public int MaxPrice;

	public int ShenShi;

	public StateType State;

	public List<int> NeedItemTagList;

	public List<int> SuitableEquipIdList;

	public bool IsPlayer;

	public AvatarUI UiCtr;

	public bool CanSelect;

	public PaiMaiAvatar(int id)
	{
		NpcId = id;
		Name = jsonData.instance.AvatarRandomJsonData[NpcId.ToString()]["Name"].Str;
		Title = jsonData.instance.AvatarJsonData[id.ToString()]["Title"].Str;
		ShenShi = jsonData.instance.AvatarJsonData[id.ToString()]["shengShi"].I;
		MaxPrice = 0;
		NeedItemTagList = new List<int>();
		SuitableEquipIdList = new List<int>();
		if (NpcId < 20000)
		{
			Money = PaiMaiOldAvatar.DataDict[NpcId].LingShi;
		}
		else
		{
			Money = jsonData.instance.AvatarBackpackJsonData[NpcId.ToString()]["money"].I;
			JSONObject jSONObject = NpcId.NPCJson();
			if (jSONObject.HasField("Status"))
			{
				int i = jSONObject["Status"]["StatusId"].I;
				if (i == 6)
				{
					NeedItemTagList.Add(620);
				}
				if (i == 2)
				{
					if (Level == 3)
					{
						NeedItemTagList.Add(610);
					}
					if (Level == 6)
					{
						NeedItemTagList.Add(611);
					}
					if (Level == 9)
					{
						NeedItemTagList.Add(612);
					}
					if (Level == 12)
					{
						NeedItemTagList.Add(3);
					}
					if (Level == 15)
					{
						NeedItemTagList.Add(614);
					}
				}
			}
			List<int> collection = jsonData.instance.AvatarJsonData[NpcId.ToString()]["equipWeaponPianHao"].ToList();
			List<int> collection2 = jsonData.instance.AvatarJsonData[NpcId.ToString()]["equipClothingPianHao"].ToList();
			List<int> collection3 = jsonData.instance.AvatarJsonData[NpcId.ToString()]["equipRingPianHao"].ToList();
			SuitableEquipIdList.AddRange(collection);
			SuitableEquipIdList.AddRange(collection2);
			SuitableEquipIdList.AddRange(collection3);
		}
		Level = jsonData.instance.AvatarJsonData[NpcId.ToString()]["Level"].I;
		ItemLevel = (Level - 1) / 3 + 1;
	}

	public PaiMaiAvatar(string name)
	{
		Name = name;
		IsPlayer = true;
	}

	public void ThinKCurShop()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (SingletonMono<PaiMaiUiMag>.Instance.CurShop == null)
		{
			Debug.LogError((object)"当前商品为空,放弃思考");
			UiCtr.SetState(StateType.放弃);
			return;
		}
		if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice > Money)
		{
			UiCtr.SetState(StateType.放弃);
			return;
		}
		if (IsNeedItem())
		{
			num += PaiMaiPanDing.DataDict[2].JiaWei;
			num2 += PaiMaiPanDing.DataDict[2].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[2].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[2].LueGanXingQu;
		}
		else if (IsLikeItem())
		{
			num += PaiMaiPanDing.DataDict[1].JiaWei;
			num2 += PaiMaiPanDing.DataDict[1].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[1].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[1].LueGanXingQu;
		}
		else if (IsSuitableEquip())
		{
			num += PaiMaiPanDing.DataDict[4].JiaWei;
			num2 += PaiMaiPanDing.DataDict[4].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[4].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[4].LueGanXingQu;
		}
		else
		{
			num += PaiMaiPanDing.DataDict[3].JiaWei;
			num2 += PaiMaiPanDing.DataDict[3].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[3].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[3].LueGanXingQu;
		}
		if (ItemLevel > SingletonMono<PaiMaiUiMag>.Instance.CurShop.Quality)
		{
			num += PaiMaiPanDing.DataDict[11].JiaWei;
			num2 += PaiMaiPanDing.DataDict[11].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[11].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[11].LueGanXingQu;
		}
		else if (ItemLevel == SingletonMono<PaiMaiUiMag>.Instance.CurShop.Quality)
		{
			num += PaiMaiPanDing.DataDict[12].JiaWei;
			num2 += PaiMaiPanDing.DataDict[12].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[12].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[12].LueGanXingQu;
		}
		else
		{
			num += PaiMaiPanDing.DataDict[13].JiaWei;
			num2 += PaiMaiPanDing.DataDict[13].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[13].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[13].LueGanXingQu;
		}
		if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level >= 0)
		{
			num += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].JiaWei;
			num2 += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].ShiZaiBiDe;
			num3 += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].YueYueYuShi;
			num4 += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].LueGanXingQu;
		}
		int randomInt = Tools.instance.GetRandomInt(0, 100);
		int randomInt2 = Tools.instance.GetRandomInt(0, 100);
		int randomInt3 = Tools.instance.GetRandomInt(0, 100);
		if (randomInt <= num2)
		{
			UiCtr.SetState(StateType.势在必得);
		}
		else if (randomInt2 <= num3)
		{
			UiCtr.SetState(StateType.跃跃欲试);
		}
		else if (randomInt3 <= num4)
		{
			UiCtr.SetState(StateType.略感兴趣);
		}
		else
		{
			UiCtr.SetState(StateType.放弃);
		}
		if (State != StateType.放弃)
		{
			num += PaiMaiPanDing.DataDict[(int)(100 + State)].JiaWei;
			MaxPrice = num * SingletonMono<PaiMaiUiMag>.Instance.CurShop.Price / 100;
			if (MaxPrice > Money)
			{
				MaxPrice = Money;
			}
			else if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice > MaxPrice)
			{
				UiCtr.SetState(StateType.放弃);
			}
			Debug.Log((object)$"npcId:{NpcId},商品原价：{SingletonMono<PaiMaiUiMag>.Instance.CurShop.Price},加成百分比{num},心理价位：{MaxPrice}");
		}
	}

	private bool IsLikeItem()
	{
		if (jsonData.instance.GetMonstarInterestingItem(NpcId, SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId) > 0)
		{
			return true;
		}
		return false;
	}

	private bool IsNeedItem()
	{
		foreach (int tag in SingletonMono<PaiMaiUiMag>.Instance.CurShop.TagList)
		{
			if (NeedItemTagList.Contains(tag))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsSuitableEquip()
	{
		foreach (int tag in SingletonMono<PaiMaiUiMag>.Instance.CurShop.TagList)
		{
			if (SuitableEquipIdList.Contains(tag))
			{
				return true;
			}
		}
		return false;
	}

	public void Select()
	{
		SingletonMono<PaiMaiUiMag>.Instance.SelectAvatarCallBack(this);
	}

	public void AddMaxMoney(float precent)
	{
		int num = (int)((float)MaxPrice * precent);
		if (num == 0)
		{
			num = ((precent >= 0f) ? 1 : (-1));
		}
		MaxPrice += num;
		if (MaxPrice > Money)
		{
			MaxPrice = Money;
		}
		if (MaxPrice < 0)
		{
			MaxPrice = 0;
		}
	}
}
