using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D22 RID: 3362
	[Serializable]
	public abstract class BaseItem : IItem
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x000398D6 File Offset: 0x00037AD6
		public void HandleSeid()
		{
			this._seid = this.Seid.ToString().ToCN();
			Debug.Log(this._seid);
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x000398F9 File Offset: 0x00037AF9
		public void LoadSeid()
		{
			Debug.Log(this._seid);
			this.Seid = new JSONObject(this._seid, -2, false, false);
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0003991B File Offset: 0x00037B1B
		public virtual void SetItem(int id, int count, JSONObject seid)
		{
			this.SetItem(id, count);
			if (seid == null || seid.Count < 1)
			{
				seid = Tools.CreateItemSeid(id);
			}
			this.Seid = seid;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x00218050 File Offset: 0x00216250
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

		// Token: 0x06004FF8 RID: 20472 RVA: 0x00039940 File Offset: 0x00037B40
		public virtual List<int> GetCiZhui()
		{
			return new List<int>(_ItemJsonData.DataDict[this.Id].Affix);
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0021815C File Offset: 0x0021635C
		public virtual string GetDesc1()
		{
			int num = GlobalValue.Get(753, string.Format("Bag.BaseItem.GetDesc1 Id:{0}", this.Id));
			return _ItemJsonData.DataDict[this.Id].desc.Replace("{STVar=753}", num.ToString());
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0003995C File Offset: 0x00037B5C
		public virtual string GetDesc2()
		{
			return _ItemJsonData.DataDict[this.Id].desc2;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void Use()
		{
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x002181B0 File Offset: 0x002163B0
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

		// Token: 0x06004FFD RID: 20477 RVA: 0x00039973 File Offset: 0x00037B73
		public virtual int GetImgQuality()
		{
			return this.Quality;
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x00218224 File Offset: 0x00216424
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

		// Token: 0x06004FFF RID: 20479 RVA: 0x002182A8 File Offset: 0x002164A8
		public virtual Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x002182D4 File Offset: 0x002164D4
		public virtual Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0003997B File Offset: 0x00037B7B
		public virtual string GetName()
		{
			return this.Name;
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00039983 File Offset: 0x00037B83
		public virtual int GetPlayerPrice()
		{
			return (int)((float)this.GetPrice() * 0.5f);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x00039993 File Offset: 0x00037B93
		public virtual int GetPrice()
		{
			return this.BasePrice;
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0003999B File Offset: 0x00037B9B
		public virtual JiaoBiaoType GetJiaoBiaoType()
		{
			if (this.Seid != null && this.Seid.HasField("isPaiMai"))
			{
				return JiaoBiaoType.拍;
			}
			return JiaoBiaoType.无;
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x000399BA File Offset: 0x00037BBA
		public virtual string GetQualityName()
		{
			return this.Quality.ToCNNumber() + "品";
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00218300 File Offset: 0x00216500
		public static BaseItem Create(int id, int count, string uuid, JSONObject seid)
		{
			int num = 0;
			try
			{
				num = _ItemJsonData.DataDict[id].type;
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("不存在物品Id:{0}", id));
				Debug.LogError(ex);
			}
			BaseItem baseItem = null;
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

		// Token: 0x06005007 RID: 20487 RVA: 0x00218430 File Offset: 0x00216630
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

		// Token: 0x06005008 RID: 20488 RVA: 0x00218578 File Offset: 0x00216778
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

		// Token: 0x06005009 RID: 20489 RVA: 0x00218598 File Offset: 0x00216798
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

		// Token: 0x0600500A RID: 20490 RVA: 0x0021862C File Offset: 0x0021682C
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

		// Token: 0x0600500B RID: 20491 RVA: 0x002186B0 File Offset: 0x002168B0
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

		// Token: 0x0600500C RID: 20492 RVA: 0x000399D1 File Offset: 0x00037BD1
		public BaseItem Clone()
		{
			return BaseItem.Create(this.Id, this.Count, this.Uid, this.Seid);
		}

		// Token: 0x04005149 RID: 20809
		public int Id;

		// Token: 0x0400514A RID: 20810
		protected int PinJie;

		// Token: 0x0400514B RID: 20811
		protected int Quality;

		// Token: 0x0400514C RID: 20812
		public int Type;

		// Token: 0x0400514D RID: 20813
		public int BasePrice;

		// Token: 0x0400514E RID: 20814
		protected string Name;

		// Token: 0x0400514F RID: 20815
		public ItemType ItemType;

		// Token: 0x04005150 RID: 20816
		public CanSlotType CanPutSlotType;

		// Token: 0x04005151 RID: 20817
		public string Desc1;

		// Token: 0x04005152 RID: 20818
		public string Desc2;

		// Token: 0x04005153 RID: 20819
		public bool CanSale;

		// Token: 0x04005154 RID: 20820
		public int Count;

		// Token: 0x04005155 RID: 20821
		public int MaxNum;

		// Token: 0x04005156 RID: 20822
		public string Uid;

		// Token: 0x04005157 RID: 20823
		public Dictionary<int, int> WuDaoDict = new Dictionary<int, int>();

		// Token: 0x04005158 RID: 20824
		[NonSerialized]
		public JSONObject Seid = new JSONObject();

		// Token: 0x04005159 RID: 20825
		private string _seid = "";
	}
}
