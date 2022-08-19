using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x0200099D RID: 2461
	[Serializable]
	public abstract class BaseItem : IItem
	{
		// Token: 0x06004494 RID: 17556 RVA: 0x001D33A4 File Offset: 0x001D15A4
		public virtual bool IsEqual(BaseItem baseItem)
		{
			return baseItem != null && this.Id == baseItem.Id && (this.Seid == null || baseItem.Seid == null || !(this.Seid.ToString() != baseItem.Seid.ToString()));
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x001D33F6 File Offset: 0x001D15F6
		public void HandleSeid()
		{
			this._seid = this.Seid.ToString().ToCN();
			Debug.Log(this._seid);
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x001D3419 File Offset: 0x001D1619
		public void LoadSeid()
		{
			Debug.Log(this._seid);
			this.Seid = new JSONObject(this._seid, -2, false, false);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x001D343B File Offset: 0x001D163B
		public virtual void SetItem(int id, int count, JSONObject seid)
		{
			if (seid != null)
			{
				this._seid = seid.Print(false);
			}
			this.SetItem(id, count);
			if (seid == null || seid.Count < 1)
			{
				seid = Tools.CreateItemSeid(id);
			}
			this.Seid = seid;
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x001D3470 File Offset: 0x001D1670
		public virtual void SetItem(int id, int count)
		{
			if (!_ItemJsonData.DataDict.ContainsKey(id))
			{
				Debug.LogError(string.Format("BaseItem.SetItem出现异常，没有id为{0}的物品，请检查配表", id));
				return;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
			this.Id = id;
			this.Quality = itemJsonData.quality;
			this.PinJie = itemJsonData.typePinJie;
			this.Type = itemJsonData.type;
			this.ItemType = BaseItem.GetItemType(this.Type);
			this.BasePrice = itemJsonData.price;
			this.Count = count;
			this.Name = itemJsonData.name;
			this.Desc1 = itemJsonData.desc;
			this.Desc2 = itemJsonData.desc2;
			this.CanSale = (itemJsonData.CanSale == 0);
			this.MaxNum = itemJsonData.maxNum;
			this.CanPutSlotType = CanSlotType.全部物品;
			for (int i = 0; i < itemJsonData.wuDao.Count; i += 2)
			{
				this.WuDaoDict.Add(itemJsonData.wuDao[i], itemJsonData.wuDao[i + 1]);
			}
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x001D357C File Offset: 0x001D177C
		public virtual List<int> GetCiZhui()
		{
			return new List<int>(_ItemJsonData.DataDict[this.Id].Affix);
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x001D3598 File Offset: 0x001D1798
		public virtual string GetDesc1()
		{
			int num = GlobalValue.Get(753, string.Format("Bag.BaseItem.GetDesc1 Id:{0}", this.Id));
			return this.Desc1.Replace("{STVar=753}", num.ToString());
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x001D35DC File Offset: 0x001D17DC
		public virtual string GetDesc2()
		{
			return this.Desc2;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void Use()
		{
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x001D35E4 File Offset: 0x001D17E4
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
			case 7:
			case 11:
			case 16:
				result = ItemType.其他;
				break;
			case 8:
				result = ItemType.材料;
				break;
			}
			return result;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x001D3656 File Offset: 0x001D1856
		public virtual int GetImgQuality()
		{
			return this.Quality;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x001D3656 File Offset: 0x001D1856
		public int GetBaseQuality()
		{
			return this.Quality;
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x001D3660 File Offset: 0x001D1860
		public virtual Sprite GetIconSprite()
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.Id];
			Sprite sprite;
			if (itemJsonData.ItemIcon == 0)
			{
				sprite = ResManager.inst.LoadSprite("Item Icon/" + this.Id);
			}
			else
			{
				sprite = ResManager.inst.LoadSprite("Item Icon/" + itemJsonData.ItemIcon);
			}
			if (sprite == null)
			{
				sprite = ResManager.inst.LoadSprite("Item Icon/1");
			}
			return sprite;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x001D36E4 File Offset: 0x001D18E4
		public virtual Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x001D3710 File Offset: 0x001D1910
		public virtual Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x001D373A File Offset: 0x001D193A
		public virtual string GetName()
		{
			return this.Name;
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x001D3742 File Offset: 0x001D1942
		public virtual int GetPlayerPrice()
		{
			return (int)((float)this.GetPrice() * 0.5f);
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x001D3752 File Offset: 0x001D1952
		public virtual int GetPrice()
		{
			return this.BasePrice;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x001D375A File Offset: 0x001D195A
		public virtual JiaoBiaoType GetJiaoBiaoType()
		{
			if (this.Seid != null && this.Seid.HasField("isPaiMai"))
			{
				return JiaoBiaoType.拍;
			}
			return JiaoBiaoType.无;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x001D3779 File Offset: 0x001D1979
		public virtual string GetQualityName()
		{
			return this.Quality.ToCNNumber() + "品";
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x001D3790 File Offset: 0x001D1990
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
				Debug.LogError(string.Format("不存在物品Id:{0}", id));
				Debug.LogError(ex);
				baseItem = new OtherItem();
				baseItem.SetItem(10000, 1, null);
				PlayerEx.AddErrorItemID(id);
				BaseItem baseItem2 = baseItem;
				baseItem2.Desc1 += string.Format("错误的物品ID:{0}", id);
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
			case 7:
			case 11:
			case 16:
				baseItem = new OtherItem();
				baseItem.SetItem(id, count, seid);
				baseItem.Uid = uuid;
				break;
			case 8:
				baseItem = new CaiLiaoItem();
				baseItem.SetItem(id, count, seid);
				baseItem.Uid = uuid;
				break;
			}
			return baseItem;
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x001D3908 File Offset: 0x001D1B08
		public int GetJiaoYiPrice(int npcid, bool isPlayer = false, bool zongjia = false)
		{
			npcid = NPCEx.NPCIDToNew(npcid);
			int num = this.BasePrice;
			int jiaCheng = this.GetJiaCheng(npcid);
			float num2 = 1f + (float)jiaCheng / 100f;
			if (this.Seid != null && this.Seid.HasField("Money"))
			{
				num = this.Seid["Money"].I;
			}
			float num3 = (float)num;
			if (this.Seid != null && this.Seid.HasField("NaiJiu"))
			{
				num3 = (float)num * this.getItemNaiJiuPrice();
			}
			if (isPlayer)
			{
				num3 = num3 * 0.5f * num2;
			}
			else
			{
				float num4 = (float)jsonData.instance.getSellPercent(npcid, this.Id) / 100f;
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
				num5 *= this.Count;
			}
			if (!isPlayer && this.Seid != null && this.Seid.HasField("isPaiMai"))
			{
				num5 *= 2;
			}
			return num5;
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x001D3A50 File Offset: 0x001D1C50
		private int GetNPCZhuangTaiJiaCheng(int npcid)
		{
			bool flag;
			bool flag2;
			this.CalcNPCZhuangTai(npcid, out flag, out flag2);
			if (flag)
			{
				return 120;
			}
			return 0;
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x001D3A70 File Offset: 0x001D1C70
		protected float getItemNaiJiuPrice()
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.Id];
			float result;
			if (itemJsonData.type == 14 || itemJsonData.type == 9)
			{
				float num = 100f;
				if (itemJsonData.type == 14)
				{
					num = (float)jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()]["Naijiu"];
				}
				result = this.Seid["NaiJiu"].n / num;
			}
			else
			{
				result = 1f;
			}
			return result;
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x001D3B04 File Offset: 0x001D1D04
		private int GetJiaCheng(int npcid)
		{
			int num = 0;
			if (npcid > 0)
			{
				num += jsonData.instance.GetMonstarInterestingItem(npcid, this.Id, this.Seid);
				num += this.GetNPCZhuangTaiJiaCheng(npcid);
			}
			num += SceneEx.ItemNowSceneJiaCheng(this.Id);
			JSONObject jsonobject = npcid.NPCJson();
			int num2 = -1;
			if (jsonobject != null && jsonobject.HasField("ActionId"))
			{
				num2 = jsonobject["ActionId"].I;
			}
			if (num2 == 51 || num2 == 52 || num2 == 53)
			{
				num -= 5;
			}
			return num;
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x001D3B88 File Offset: 0x001D1D88
		private void CalcNPCZhuangTai(int npcid, out bool isJiXu, out bool isLaJi)
		{
			isJiXu = false;
			isLaJi = false;
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.Id];
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
			JSONObject jsonobject = npcid.NPCJson();
			if (!jsonobject.HasField("Status"))
			{
				return;
			}
			int i = jsonobject["Status"]["StatusId"].I;
			int i2 = jsonobject["Level"].I;
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

		// Token: 0x060044AE RID: 17582 RVA: 0x001D3CDC File Offset: 0x001D1EDC
		public BaseItem Clone()
		{
			if (this.Seid != null)
			{
				return BaseItem.Create(this.Id, this.Count, this.Uid, this.Seid.Copy());
			}
			return BaseItem.Create(this.Id, this.Count, this.Uid, this.Seid);
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x001D3D31 File Offset: 0x001D1F31
		[OnDeserialized]
		private void AfterLoad(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(this._seid))
			{
				this.Seid = JSONObject.Create(this._seid, -2, false, false);
			}
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x001D3D55 File Offset: 0x001D1F55
		[OnSerializing]
		private void BeforeSave(StreamingContext context)
		{
			if (this.Seid != null)
			{
				this._seid = this.Seid.Print(false);
			}
		}

		// Token: 0x0400464B RID: 17995
		public int Id;

		// Token: 0x0400464C RID: 17996
		protected int PinJie;

		// Token: 0x0400464D RID: 17997
		protected int Quality;

		// Token: 0x0400464E RID: 17998
		public int Type;

		// Token: 0x0400464F RID: 17999
		public int BasePrice;

		// Token: 0x04004650 RID: 18000
		protected string Name;

		// Token: 0x04004651 RID: 18001
		public ItemType ItemType;

		// Token: 0x04004652 RID: 18002
		public CanSlotType CanPutSlotType;

		// Token: 0x04004653 RID: 18003
		public string Desc1;

		// Token: 0x04004654 RID: 18004
		public string Desc2;

		// Token: 0x04004655 RID: 18005
		public bool CanSale;

		// Token: 0x04004656 RID: 18006
		public int Count;

		// Token: 0x04004657 RID: 18007
		public int MaxNum;

		// Token: 0x04004658 RID: 18008
		public string Uid;

		// Token: 0x04004659 RID: 18009
		public Dictionary<int, int> WuDaoDict = new Dictionary<int, int>();

		// Token: 0x0400465A RID: 18010
		[NonSerialized]
		public JSONObject Seid;

		// Token: 0x0400465B RID: 18011
		private string _seid = "";
	}
}
