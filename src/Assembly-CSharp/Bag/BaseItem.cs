using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JSONClass;
using UnityEngine;

namespace Bag;

[Serializable]
public abstract class BaseItem : IItem
{
	public int Id;

	protected int PinJie;

	protected int Quality;

	public int Type;

	public int BasePrice;

	protected string Name;

	public ItemType ItemType;

	public CanSlotType CanPutSlotType;

	public string Desc1;

	public string Desc2;

	public bool CanSale;

	public int Count;

	public int MaxNum;

	public string Uid;

	public Dictionary<int, int> WuDaoDict = new Dictionary<int, int>();

	[NonSerialized]
	public JSONObject Seid;

	private string _seid = "";

	public virtual bool IsEqual(BaseItem baseItem)
	{
		if (baseItem == null)
		{
			return false;
		}
		if (Id != baseItem.Id)
		{
			return false;
		}
		if (Seid != null && baseItem.Seid != null && Seid.ToString() != baseItem.Seid.ToString())
		{
			return false;
		}
		return true;
	}

	public void HandleSeid()
	{
		_seid = Seid.ToString().ToCN();
		Debug.Log((object)_seid);
	}

	public void LoadSeid()
	{
		Debug.Log((object)_seid);
		Seid = new JSONObject(_seid);
	}

	public virtual void SetItem(int id, int count, JSONObject seid)
	{
		if (seid != null)
		{
			_seid = seid.Print();
		}
		SetItem(id, count);
		if (seid == null || seid.Count < 1)
		{
			seid = Tools.CreateItemSeid(id);
		}
		Seid = seid;
	}

	public virtual void SetItem(int id, int count)
	{
		if (!_ItemJsonData.DataDict.ContainsKey(id))
		{
			Debug.LogError((object)$"BaseItem.SetItem出现异常，没有id为{id}的物品，请检查配表");
			return;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
		Id = id;
		Quality = itemJsonData.quality;
		PinJie = itemJsonData.typePinJie;
		Type = itemJsonData.type;
		ItemType = GetItemType(Type);
		BasePrice = itemJsonData.price;
		Count = count;
		Name = itemJsonData.name;
		Desc1 = itemJsonData.desc;
		Desc2 = itemJsonData.desc2;
		CanSale = itemJsonData.CanSale == 0;
		MaxNum = itemJsonData.maxNum;
		CanPutSlotType = CanSlotType.全部物品;
		for (int i = 0; i < itemJsonData.wuDao.Count; i += 2)
		{
			WuDaoDict.Add(itemJsonData.wuDao[i], itemJsonData.wuDao[i + 1]);
		}
	}

	public virtual List<int> GetCiZhui()
	{
		return new List<int>(_ItemJsonData.DataDict[Id].Affix);
	}

	public virtual string GetDesc1()
	{
		int num = GlobalValue.Get(753, $"Bag.BaseItem.GetDesc1 Id:{Id}");
		return Desc1.Replace("{STVar=753}", num.ToString());
	}

	public virtual string GetDesc2()
	{
		return Desc2;
	}

	public virtual void Use()
	{
	}

	public static ItemType GetItemType(int type)
	{
		ItemType result = ItemType.其他;
		switch (type)
		{
		case 0:
		case 1:
		case 2:
		case 9:
		case 14:
			result = ItemType.法宝;
			break;
		case 3:
		case 4:
		case 10:
		case 12:
		case 13:
			result = ItemType.秘籍;
			break;
		case 5:
		case 15:
			result = ItemType.丹药;
			break;
		case 6:
			result = ItemType.草药;
			break;
		case 8:
			result = ItemType.材料;
			break;
		case 7:
		case 11:
		case 16:
			result = ItemType.其他;
			break;
		}
		return result;
	}

	public virtual int GetImgQuality()
	{
		return Quality;
	}

	public int GetBaseQuality()
	{
		return Quality;
	}

	public virtual Sprite GetIconSprite()
	{
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[Id];
		Sprite val = null;
		val = ((itemJsonData.ItemIcon != 0) ? ResManager.inst.LoadSprite("Item Icon/" + itemJsonData.ItemIcon) : ResManager.inst.LoadSprite("Item Icon/" + Id));
		if ((Object)(object)val == (Object)null)
		{
			val = ResManager.inst.LoadSprite("Item Icon/1");
		}
		return val;
	}

	public virtual Sprite GetQualitySprite()
	{
		return BagMag.Inst.QualityDict[GetImgQuality().ToString()];
	}

	public virtual Sprite GetQualityUpSprite()
	{
		return BagMag.Inst.QualityUpDict[GetImgQuality().ToString()];
	}

	public virtual string GetName()
	{
		return Name;
	}

	public virtual int GetPlayerPrice()
	{
		return (int)((float)GetPrice() * 0.5f);
	}

	public virtual int GetPrice()
	{
		return BasePrice;
	}

	public virtual JiaoBiaoType GetJiaoBiaoType()
	{
		if (Seid != null && Seid.HasField("isPaiMai"))
		{
			return JiaoBiaoType.拍;
		}
		return JiaoBiaoType.无;
	}

	public virtual string GetQualityName()
	{
		return Quality.ToCNNumber() + "品";
	}

	public static BaseItem Create(int id, int count, string uuid, JSONObject seid)
	{
		BaseItem baseItem = null;
		int num = 0;
		try
		{
			num = _ItemJsonData.DataDict[id].type;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)$"不存在物品Id:{id}");
			Debug.LogError((object)ex);
			baseItem = new OtherItem();
			baseItem.SetItem(10000, 1, null);
			PlayerEx.AddErrorItemID(id);
			baseItem.Desc1 += $"错误的物品ID:{id}";
			baseItem.Uid = uuid;
			return baseItem;
		}
		switch (num)
		{
		case 0:
		case 1:
		case 2:
		case 9:
		case 14:
			baseItem = new EquipItem();
			baseItem.SetItem(id, count, seid);
			baseItem.Uid = uuid;
			break;
		case 3:
		case 4:
		case 10:
		case 12:
		case 13:
			baseItem = new MiJiItem();
			baseItem.SetItem(id, count, seid);
			baseItem.Uid = uuid;
			break;
		case 5:
		case 15:
			baseItem = new DanYaoItem();
			baseItem.SetItem(id, count, seid);
			baseItem.Uid = uuid;
			break;
		case 6:
			baseItem = new CaoYaoItem();
			baseItem.SetItem(id, count, seid);
			baseItem.Uid = uuid;
			break;
		case 8:
			baseItem = new CaiLiaoItem();
			baseItem.SetItem(id, count, seid);
			baseItem.Uid = uuid;
			break;
		case 7:
		case 11:
		case 16:
			baseItem = new OtherItem();
			baseItem.SetItem(id, count, seid);
			baseItem.Uid = uuid;
			break;
		}
		return baseItem;
	}

	public int GetJiaoYiPrice(int npcid, bool isPlayer = false, bool zongjia = false)
	{
		npcid = NPCEx.NPCIDToNew(npcid);
		int num = BasePrice;
		int jiaCheng = GetJiaCheng(npcid);
		float num2 = 1f + (float)jiaCheng / 100f;
		if (Seid != null && Seid.HasField("Money"))
		{
			num = Seid["Money"].I;
		}
		float num3 = num;
		if (Seid != null && Seid.HasField("NaiJiu"))
		{
			num3 = (float)num * getItemNaiJiuPrice();
		}
		if (isPlayer)
		{
			num3 = num3 * 0.5f * num2;
		}
		else
		{
			float num4 = (float)jsonData.instance.getSellPercent(npcid, Id) / 100f;
			if (jsonData.instance.AvatarJsonData[npcid.ToString()]["gudingjiage"].I == 1)
			{
				num2 = 1f;
				if ((double)num4 < 1.5)
				{
					num2 = 1.5f;
				}
			}
			num3 = num3 * num2 * num4;
		}
		int num5 = (int)num3;
		if (num3 % 1f > 0.9f)
		{
			num5++;
		}
		if (zongjia)
		{
			num5 *= Count;
		}
		if (!isPlayer && Seid != null && Seid.HasField("isPaiMai"))
		{
			num5 *= 2;
		}
		return num5;
	}

	private int GetNPCZhuangTaiJiaCheng(int npcid)
	{
		CalcNPCZhuangTai(npcid, out var isJiXu, out var _);
		if (isJiXu)
		{
			return 120;
		}
		return 0;
	}

	protected float getItemNaiJiuPrice()
	{
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[Id];
		float num = 0f;
		if (itemJsonData.type == 14 || itemJsonData.type == 9)
		{
			float num2 = 100f;
			if (itemJsonData.type == 14)
			{
				num2 = (float)jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()][(object)"Naijiu"];
			}
			return Seid["NaiJiu"].n / num2;
		}
		return 1f;
	}

	private int GetJiaCheng(int npcid)
	{
		int num = 0;
		if (npcid > 0)
		{
			num += jsonData.instance.GetMonstarInterestingItem(npcid, Id, Seid);
			num += GetNPCZhuangTaiJiaCheng(npcid);
		}
		num += SceneEx.ItemNowSceneJiaCheng(Id);
		JSONObject jSONObject = npcid.NPCJson();
		int num2 = -1;
		if (jSONObject != null && jSONObject.HasField("ActionId"))
		{
			num2 = jSONObject["ActionId"].I;
		}
		if (num2 == 51 || num2 == 52 || num2 == 53)
		{
			num -= 5;
		}
		return num;
	}

	private void CalcNPCZhuangTai(int npcid, out bool isJiXu, out bool isLaJi)
	{
		isJiXu = false;
		isLaJi = false;
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[Id];
		List<int> list = new List<int>();
		if (itemJsonData.ItemFlag.Count > 0)
		{
			foreach (int item in itemJsonData.ItemFlag)
			{
				list.Add(item);
			}
		}
		if (list.Contains(50))
		{
			isLaJi = true;
		}
		JSONObject jSONObject = npcid.NPCJson();
		if (!jSONObject.HasField("Status"))
		{
			return;
		}
		int i = jSONObject["Status"]["StatusId"].I;
		int i2 = jSONObject["Level"].I;
		if (i == 6 && list.Contains(620))
		{
			isJiXu = true;
		}
		if (i == 2)
		{
			if (i2 == 3 && list.Contains(610))
			{
				isJiXu = true;
			}
			if (i2 == 6 && list.Contains(611))
			{
				isJiXu = true;
			}
			if (i2 == 9 && list.Contains(612))
			{
				isJiXu = true;
			}
			if (i2 == 12 && list.Contains(613))
			{
				isJiXu = true;
			}
			if (i2 == 15 && list.Contains(614))
			{
				isJiXu = true;
			}
		}
	}

	public BaseItem Clone()
	{
		if (Seid != null)
		{
			return Create(Id, Count, Uid, Seid.Copy());
		}
		return Create(Id, Count, Uid, Seid);
	}

	[OnDeserialized]
	private void AfterLoad(StreamingContext context)
	{
		if (!string.IsNullOrEmpty(_seid))
		{
			Seid = JSONObject.Create(_seid);
		}
	}

	[OnSerializing]
	private void BeforeSave(StreamingContext context)
	{
		if (Seid != null)
		{
			_seid = Seid.Print();
		}
	}
}
