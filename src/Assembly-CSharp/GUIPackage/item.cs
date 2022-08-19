using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage
{
	// Token: 0x02000A4E RID: 2638
	[Serializable]
	public class item
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600487C RID: 18556 RVA: 0x001E9C24 File Offset: 0x001E7E24
		// (set) Token: 0x0600487D RID: 18557 RVA: 0x001E9C5C File Offset: 0x001E7E5C
		public string itemName
		{
			get
			{
				if (this.Seid != null && this.Seid.HasField("Name"))
				{
					return this.Seid["Name"].str;
				}
				return this._itemName;
			}
			set
			{
				this._itemName = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x001E9C68 File Offset: 0x001E7E68
		// (set) Token: 0x0600487F RID: 18559 RVA: 0x001E9CBB File Offset: 0x001E7EBB
		public Texture2D itemIcon
		{
			get
			{
				this.InitImage();
				if (this.Seid != null && this.Seid.HasField("ItemIcon"))
				{
					return ResManager.inst.LoadTexture2D(this.Seid["ItemIcon"].str);
				}
				return this._itemIcon;
			}
			set
			{
				this._itemIcon = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x001E9CC4 File Offset: 0x001E7EC4
		// (set) Token: 0x06004881 RID: 18561 RVA: 0x001E9D17 File Offset: 0x001E7F17
		public Sprite itemIconSprite
		{
			get
			{
				this.InitImage();
				if (this.Seid != null && this.Seid.HasField("ItemIcon"))
				{
					return ResManager.inst.LoadSprite(this.Seid["ItemIcon"].str);
				}
				return this._itemIconSprite;
			}
			set
			{
				this._itemIconSprite = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x001E9D20 File Offset: 0x001E7F20
		// (set) Token: 0x06004883 RID: 18563 RVA: 0x001E9D86 File Offset: 0x001E7F86
		public Texture2D itemPingZhi
		{
			get
			{
				this.InitImage();
				if (this.Seid != null && this.Seid.HasField("quality"))
				{
					int i = this.Seid["quality"].I;
					return ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + (i + 1));
				}
				return this._itemPingZhi;
			}
			set
			{
				this._itemPingZhi = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06004884 RID: 18564 RVA: 0x001E9D90 File Offset: 0x001E7F90
		// (set) Token: 0x06004885 RID: 18565 RVA: 0x001E9DF0 File Offset: 0x001E7FF0
		public Sprite itemPingZhiSprite
		{
			get
			{
				if (this.Seid != null && this.Seid.HasField("quality"))
				{
					int i = this.Seid["quality"].I;
					return ResManager.inst.LoadSprite("Ui Icon/tab/item" + (i + 1));
				}
				return this._itemPingZhiSprite;
			}
			set
			{
				this._itemPingZhiSprite = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x001E9DFC File Offset: 0x001E7FFC
		// (set) Token: 0x06004887 RID: 18567 RVA: 0x001E9E62 File Offset: 0x001E8062
		public Sprite itemPingZhiUP
		{
			get
			{
				this.InitImage();
				if (this.Seid != null && this.Seid.HasField("quality"))
				{
					int i = this.Seid["quality"].I;
					return ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + (i + 1));
				}
				return this._itemPingZhiUP;
			}
			set
			{
				this._itemPingZhiUP = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x001E9E6C File Offset: 0x001E806C
		// (set) Token: 0x06004889 RID: 18569 RVA: 0x001E9ED2 File Offset: 0x001E80D2
		public Sprite newitemPingZhiSprite
		{
			get
			{
				this.InitImage();
				if (this.Seid != null && this.Seid.HasField("quality"))
				{
					int i = this.Seid["quality"].I;
					return ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + (i + 1));
				}
				return this._newitemPingZhiSprite;
			}
			set
			{
				this._newitemPingZhiSprite = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x001E9EDC File Offset: 0x001E80DC
		// (set) Token: 0x0600488B RID: 18571 RVA: 0x001E9F49 File Offset: 0x001E8149
		public Sprite newitemPingZhiUP
		{
			get
			{
				this.InitImage();
				if (this.Seid != null && this.Seid.HasField("quality"))
				{
					int i = this.Seid["quality"].I;
					this.ColorIndex = i;
					return ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + (i + 1));
				}
				return this._newitemPingZhiUP;
			}
			set
			{
				this._newitemPingZhiUP = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600488C RID: 18572 RVA: 0x001E9F52 File Offset: 0x001E8152
		// (set) Token: 0x0600488D RID: 18573 RVA: 0x001E9F8A File Offset: 0x001E818A
		public int quality
		{
			get
			{
				if (this.Seid != null && this.Seid.HasField("quality"))
				{
					return this.Seid["quality"].I;
				}
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x001E9F94 File Offset: 0x001E8194
		public item()
		{
			this.itemID = -1;
			this.UUID = "";
			this.ExGoodsID = -1;
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x001E9FF0 File Offset: 0x001E81F0
		public item(string name, int id, string nameCN, string desc, int max_num, item.ItemType type, int price)
		{
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x001EA028 File Offset: 0x001E8228
		public item(int id)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
			this.itemID = id;
			this.itemNameCN = itemJsonData.name;
			this.itemDesc = itemJsonData.desc;
			this.itemName = this.itemNameCN;
			this.itemNum = 1;
			this.itemMaxNum = itemJsonData.maxNum;
			this.itemType = (item.ItemType)itemJsonData.type;
			this.itemtype = itemJsonData.type;
			this.itemPrice = itemJsonData.price;
			this.StuTime = itemJsonData.StuTime;
			this.quality = itemJsonData.quality;
			if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
			{
				this.ColorIndex = this.quality;
			}
			else if (itemJsonData.type == 3 || itemJsonData.type == 4)
			{
				this.ColorIndex = this.quality * 2 - 1;
			}
			else
			{
				this.ColorIndex = this.quality - 1;
			}
			foreach (int item in itemJsonData.seid)
			{
				this.seid.Add(item);
			}
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x001EA198 File Offset: 0x001E8398
		public void InitImage()
		{
			if (!this.initedImage)
			{
				this.initedImage = true;
				if (!_ItemJsonData.DataDict.ContainsKey(this.itemID))
				{
					return;
				}
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.itemID];
				if (itemJsonData.ItemIcon == 0)
				{
					this.itemIcon = ResManager.inst.LoadTexture2D("Item Icon/" + this.itemID);
					this.itemIconSprite = ResManager.inst.LoadSprite("Item Icon/" + this.itemID);
				}
				else
				{
					this.itemIcon = ResManager.inst.LoadTexture2D("Item Icon/" + itemJsonData.ItemIcon);
					this.itemIconSprite = ResManager.inst.LoadSprite("Item Icon/" + itemJsonData.ItemIcon);
				}
				if (this.itemIcon == null)
				{
					this.itemIcon = ResManager.inst.LoadTexture2D("Item Icon/1");
					this.itemIconSprite = ResManager.inst.LoadSprite("Item Icon/1");
				}
				if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
				{
					this.itemPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + (this.quality + 1));
					this.itemPingZhiUP = ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + (this.quality + 1));
					this._newitemPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + (this.quality + 1));
					this._newitemPingZhiUP = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + (this.quality + 1));
					return;
				}
				if (itemJsonData.type == 3 || itemJsonData.type == 4)
				{
					this.itemPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + this.quality * 2);
					this.itemPingZhiUP = ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + this.quality * 2);
					this._newitemPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + this.quality * 2);
					this._newitemPingZhiUP = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + this.quality * 2);
					return;
				}
				this.itemPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + this.quality);
				this.itemPingZhiUP = ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + this.quality);
				this._newitemPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + this.quality);
				this._newitemPingZhiUP = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + this.quality);
			}
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x001EA4B4 File Offset: 0x001E86B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("itemID = {0}", this.itemID));
			stringBuilder.AppendLine("itemNameCN = " + this.itemNameCN);
			stringBuilder.AppendLine("itemDesc = " + this.itemDesc);
			stringBuilder.AppendLine("itemName = " + this.itemName);
			stringBuilder.AppendLine(string.Format("itemNum = {0}", this.itemNum));
			stringBuilder.AppendLine(string.Format("itemMaxNum = {0}", this.itemMaxNum));
			stringBuilder.AppendLine(string.Format("itemType = {0}", this.itemType));
			stringBuilder.AppendLine(string.Format("itemtype = {0}", this.itemtype));
			stringBuilder.AppendLine(string.Format("itemPrice = {0}", this.itemPrice));
			stringBuilder.AppendLine(string.Format("StuTime = {0}", this.StuTime));
			stringBuilder.AppendLine(string.Format("quality = {0}", this.quality));
			List<int> list = new List<int>();
			if (this.Seid != null && this.Seid.HasField("ItemFlag"))
			{
				list = this.Seid["ItemFlag"].ToList();
			}
			else if (_ItemJsonData.DataDict[this.itemID].ItemFlag.Count > 0)
			{
				list = _ItemJsonData.DataDict[this.itemID].ItemFlag;
			}
			if (list.Count > 0)
			{
				stringBuilder.Append("ItemFlag = ");
				foreach (int number in list)
				{
					stringBuilder.Append(number.ToItemFlagName() + " ");
				}
				stringBuilder.Append("\n");
			}
			stringBuilder.AppendLine("Seid = " + this.Seid.ToString().ToCN());
			return stringBuilder.ToString();
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x001EA6F0 File Offset: 0x001E88F0
		public int GetItemPrice()
		{
			int i = this.itemPrice;
			if (this.Seid != null && this.Seid.HasField("Money"))
			{
				i = this.Seid["Money"].I;
			}
			return (int)((float)i * 0.5f);
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x001EA740 File Offset: 0x001E8940
		public int GetItemOriPrice()
		{
			int i = this.itemPrice;
			if (this.Seid != null && this.Seid.HasField("Money"))
			{
				i = this.Seid["Money"].I;
			}
			float num = 1f;
			if (this.itemtype == 9)
			{
				if (this.Seid.HasField("NaiJiu"))
				{
					num = this.Seid["NaiJiu"].f / 100f;
				}
				else
				{
					Debug.LogError(string.Format("物品ID:{0},物品名称：{1},不存在耐久度", this.itemID, this.itemName));
				}
			}
			else if (this.itemtype == 14)
			{
				if (this.Seid.HasField("NaiJiu"))
				{
					num = this.Seid["NaiJiu"].f / (float)jsonData.instance.LingZhouPinJie[this.quality.ToString()]["Naijiu"];
				}
				else
				{
					Debug.LogError(string.Format("物品ID:{0},物品名称：{1},不存在耐久度", this.itemID, this.itemName));
				}
			}
			return (int)((float)i * num);
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x001EA878 File Offset: 0x001E8A78
		public void CalcNPCZhuangTai(int npcid, out bool isJiXu, out bool isLaJi)
		{
			isJiXu = false;
			isLaJi = false;
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.itemID];
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

		// Token: 0x06004896 RID: 18582 RVA: 0x001EA9CC File Offset: 0x001E8BCC
		public int GetNPCZhuangTaiJiaCheng(int npcid)
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

		// Token: 0x06004897 RID: 18583 RVA: 0x001EA9EC File Offset: 0x001E8BEC
		public int GetJiaCheng(int npcid)
		{
			int num = 0;
			if (npcid > 0)
			{
				num += jsonData.instance.GetMonstarInterestingItem(npcid, this.itemID, this.Seid);
				num += this.GetNPCZhuangTaiJiaCheng(npcid);
			}
			num += SceneEx.ItemNowSceneJiaCheng(this.itemID);
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

		// Token: 0x06004898 RID: 18584 RVA: 0x001EAA70 File Offset: 0x001E8C70
		public int GetJiaoYiPrice(int npcid, bool isPlayer = false, bool zongjia = false)
		{
			npcid = NPCEx.NPCIDToNew(npcid);
			int i = this.itemPrice;
			int jiaCheng = this.GetJiaCheng(npcid);
			float num = 1f + (float)jiaCheng / 100f;
			if (this.Seid != null && this.Seid.HasField("Money"))
			{
				i = this.Seid["Money"].I;
			}
			float num2 = (float)i;
			if (this.Seid != null && this.Seid.HasField("NaiJiu"))
			{
				num2 = (float)i * ItemCellEX.getItemNaiJiuPrice(this);
			}
			if (isPlayer)
			{
				num2 = num2 * 0.5f * num;
			}
			else
			{
				float num3 = (float)jsonData.instance.getSellPercent(npcid, this.itemID) / 100f;
				if (jsonData.instance.AvatarJsonData[npcid.ToString()]["gudingjiage"].I == 1)
				{
					num = 1f;
					if ((double)num3 < 1.5)
					{
						num = 1.5f;
					}
				}
				num2 = num2 * num * num3;
			}
			int num4 = (int)num2;
			if (num2 % 1f > 0.9f)
			{
				num4++;
			}
			if (zongjia)
			{
				num4 *= this.itemNum;
			}
			return num4;
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x001EAB94 File Offset: 0x001E8D94
		public item Clone()
		{
			return base.MemberwiseClone() as item;
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x001EABA4 File Offset: 0x001E8DA4
		public void Copy(item A, item B)
		{
			PropertyInfo[] properties = A.GetType().GetProperties();
			PropertyInfo[] properties2 = B.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties2[i].CanWrite)
				{
					properties2[i].SetValue(this, properties[i].GetValue(A, null));
				}
			}
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x001EABF4 File Offset: 0x001E8DF4
		public bool IsWuDaoCanStudy(List<int> wudaoTypeList, List<int> wudaoLvList)
		{
			Avatar player = Tools.instance.getPlayer();
			int num = 0;
			foreach (int wuDaoType in wudaoTypeList)
			{
				if (player.wuDaoMag.getWuDaoLevelByType(wuDaoType) < wudaoLvList[num])
				{
					string str = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[wuDaoType.ToString()]["name"].str);
					Tools.Code64(jsonData.instance.WuDaoJinJieJson[wudaoLvList[num].ToString()]["Text"].str);
					string msg = str + "之道感悟不足";
					UIPopTip.Inst.Pop(msg, PopTipIconType.叹号);
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x001EACE8 File Offset: 0x001E8EE8
		public static JSONObject getGongFaBookItem(int STSKillID)
		{
			foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.ItemJsonData)
			{
				if (keyValuePair.Value["type"].I == 4)
				{
					float num = 0f;
					if (float.TryParse(keyValuePair.Value["desc"].str, out num) && (int)num == STSKillID)
					{
						return keyValuePair.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x001EAD88 File Offset: 0x001E8F88
		public static string StudyTiaoJian(List<int> wudaoTypeList, List<int> wudaoLvList)
		{
			Tools.instance.getPlayer();
			int num = 0;
			string text = "";
			bool flag = true;
			foreach (int num2 in wudaoTypeList)
			{
				string text2 = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[num2.ToString()]["name"].str);
				string text3 = Tools.Code64(jsonData.instance.WuDaoJinJieJson[wudaoLvList[num].ToString()]["Text"].str);
				if (wudaoLvList[num] >= 1)
				{
					text = string.Concat(new string[]
					{
						text,
						"对",
						text2,
						"之道的感悟达到",
						text3,
						";"
					});
					flag = false;
				}
				num++;
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			if (flag)
			{
				text += "无";
			}
			text += "。";
			return text;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x001EAEC4 File Offset: 0x001E90C4
		public static string StudyTiSheng(List<int> wudaoTypeList, string startString = "领悟后能够提升对")
		{
			Tools.instance.getPlayer();
			int num = 0;
			string text = startString;
			foreach (int num2 in wudaoTypeList)
			{
				string str = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[num2.ToString()]["name"].str);
				text = text + str + "之道、";
				num++;
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			text += "的感悟。";
			return text;
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x001EAF80 File Offset: 0x001E9180
		public static void GetWuDaoType(int itemID, List<int> wudaoTypeList, List<int> wudaoLvList)
		{
			int num = 0;
			foreach (int item in _ItemJsonData.DataDict[itemID].wuDao)
			{
				if (num % 2 == 0)
				{
					wudaoTypeList.Add(item);
				}
				else
				{
					wudaoLvList.Add(item);
				}
				num++;
			}
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x001EAFF4 File Offset: 0x001E91F4
		public static int GetItemCanUseNum(int ItemID)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[ItemID];
			int num = itemJsonData.CanUse;
			int type = itemJsonData.type;
			int staticSkillAddSum = PlayerEx.Player.getStaticSkillAddSum(15);
			if (type == 5 && staticSkillAddSum != 0)
			{
				num *= 2;
			}
			return num;
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x001EB030 File Offset: 0x001E9230
		public void gongneng(UnityAction Next = null, bool isTuPo = false)
		{
			Debug.Log("您使用了" + this.itemNameCN);
			Avatar player = PlayerEx.Player;
			if (!_ItemJsonData.DataDict.ContainsKey(this.itemID))
			{
				Debug.LogError(string.Format("物品表没有ID为{0}的物品", this.itemID));
				return;
			}
			int type = _ItemJsonData.DataDict[this.itemID].type;
			int itemCanUseNum = item.GetItemCanUseNum(this.itemID);
			if (type == 3 || type == 4 || type == 10)
			{
				if (type == 3 || type == 4)
				{
					List<int> wudaoTypeList = new List<int>();
					List<int> wudaoLvList = new List<int>();
					item.GetWuDaoType(this.itemID, wudaoTypeList, wudaoLvList);
					if (!this.IsWuDaoCanStudy(wudaoTypeList, wudaoLvList))
					{
						return;
					}
				}
				int num = this.itemID;
				if (num > jsonData.QingJiaoItemIDSegment)
				{
					num -= jsonData.QingJiaoItemIDSegment;
				}
				using (List<int>.Enumerator enumerator = _ItemJsonData.DataDict[num].seid.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						if (num2 == 1)
						{
							int id = ItemsSeidJsonData1.DataDict[num].value1;
							if (PlayerEx.Player.hasSkillList.Find((SkillItem s) => s.itemId == id) != null)
							{
								UIPopTip.Inst.Pop("你已经学习过该技能", PopTipIconType.叹号);
								return;
							}
						}
						else
						{
							if (num2 == 2)
							{
								int id = ItemsSeidJsonData2.DataDict[num].value1;
								if (player.hasStaticSkillList.Find((SkillItem s) => s.itemId == id) != null)
								{
									UIPopTip.Inst.Pop("你已经学习过该功法", PopTipIconType.叹号);
									return;
								}
								using (List<StaticSkillJsonData>.Enumerator enumerator2 = StaticSkillJsonData.DataList.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										StaticSkillJsonData staticSkillJsonData = enumerator2.Current;
										if (staticSkillJsonData.Skill_ID == id)
										{
											if (staticSkillJsonData.seid.Contains(9) && player.dunSu < 18)
											{
												UIPopTip.Inst.Pop("遁速大于18方可学习", PopTipIconType.叹号);
												return;
											}
											break;
										}
									}
									continue;
								}
							}
							if (num2 == 13)
							{
								int value = ItemsSeidJsonData13.DataDict[num].value1;
								if (player.ISStudyDanFan(value))
								{
									UIPopTip.Inst.Pop("你已经阅读过该丹方", PopTipIconType.叹号);
									return;
								}
								LianDanDanFangBiao lianDanDanFangBiao = LianDanDanFangBiao.DataDict[value];
								int value2 = lianDanDanFangBiao.value2;
								if (value2 > 0)
								{
									player.AddYaoCaiShuXin(value2, 2);
								}
								int value3 = lianDanDanFangBiao.value3;
								if (value3 > 0)
								{
									player.AddYaoCaiShuXin(value3, 2);
								}
								int value4 = lianDanDanFangBiao.value4;
								if (value4 > 0)
								{
									player.AddYaoCaiShuXin(value4, 3);
								}
								int value5 = lianDanDanFangBiao.value5;
								if (value5 > 0)
								{
									player.AddYaoCaiShuXin(value5, 3);
								}
								int value6 = lianDanDanFangBiao.value1;
								if (value6 > 0)
								{
									player.AddYaoCaiShuXin(value6, 1);
								}
							}
						}
					}
					goto IL_3DA;
				}
			}
			if ((type == 5 || type == 13) && itemCanUseNum > 0)
			{
				if (jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["seid"].ToList().Contains(35))
				{
					UIPopTip.Inst.Pop("仅能在装扮前服用", PopTipIconType.叹号);
					return;
				}
				if (TpUIMag.inst == null && jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["seid"].ToList().Contains(31))
				{
					UIPopTip.Inst.Pop("需要在突破前服用", PopTipIconType.叹号);
					return;
				}
				if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(this.itemID)) >= itemCanUseNum)
				{
					string msg = "已到最大耐药性，无法服用";
					if (type == 13)
					{
						msg = "已经领悟过该大道书";
					}
					UIPopTip.Inst.Pop(msg, PopTipIconType.叹号);
					return;
				}
			}
			IL_3DA:
			if (isTuPo)
			{
				if (!Tools.canClickFlag)
				{
					return;
				}
				Tools.instance.playFader("正在翻阅秘籍...", null);
			}
			this.inventoryNext = Next;
			if (Tools.instance.isEquip(this.itemID))
			{
				if (this.inventoryNext != null)
				{
					this.inventoryNext.Invoke();
				}
				return;
			}
			foreach (JSONObject jsonobject in jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["seid"].list)
			{
				if (!this.CanNextSeid(jsonobject.I))
				{
					break;
				}
				this.realizeSeid(jsonobject.I);
			}
			JSONObject jsonobject2 = jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["seid"];
			if (jsonobject2.HasItem(1) || jsonobject2.HasItem(2) || type == 13)
			{
				int addday = Tools.CalcLingWuTime(this.itemID);
				player.AddTime(addday, 0, 0);
			}
			if (type == 3)
			{
				item.AddWuDao(this.getItemJson()["StuTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["linwu"].n, Tools.JsonListToList(jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["wuDao"]), 2);
			}
			if (type == 4)
			{
				item.AddWuDao(this.getItemJson()["StuTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["gongfa"].n, Tools.JsonListToList(jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["wuDao"]), 2);
			}
			if (type == 13)
			{
				item.AddWuDao(this.getItemJson()["StuTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["kanshu"].n, Tools.JsonListToList(jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["wuDao"]), 2);
			}
			if (jsonData.instance.ItemJsonData[string.Concat(this.itemID)]["vagueType"].n == 2f)
			{
				return;
			}
			if (this.inventoryNext != null)
			{
				this.inventoryNext.Invoke();
			}
			if (type == 5)
			{
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				int num3 = (int)jsonData.instance.ItemJsonData[this.itemID.ToString()]["DanDu"].n;
				num3 -= avatar.getStaticSkillAddSum(14);
				if (num3 < 0)
				{
					num3 = 0;
				}
				avatar.AddDandu(num3);
			}
			if (type == 5 || type == 13)
			{
				this.AddNaiYaoXin();
			}
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x001EB784 File Offset: 0x001E9984
		public void AddNaiYaoXin()
		{
			if (!Tools.instance.getPlayer().NaiYaoXin.HasField(string.Concat(this.itemID)))
			{
				Tools.instance.getPlayer().NaiYaoXin.AddField(string.Concat(this.itemID), 0);
			}
			int num = (int)Tools.instance.getPlayer().NaiYaoXin[string.Concat(this.itemID)].n;
			Tools.instance.getPlayer().NaiYaoXin.SetField(string.Concat(this.itemID), num + 1);
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x001EB830 File Offset: 0x001E9A30
		public static void AddWuDao(int timeday, float xishu, List<int> xishuList, int listXi = 2)
		{
			Avatar player = Tools.instance.getPlayer();
			int num = (int)(xishu * (float)timeday);
			int num2 = xishuList.Count / listXi;
			if (num2 > 0)
			{
				int num3 = 0;
				foreach (int wuDaoType in xishuList)
				{
					if (num3 % listXi == 0)
					{
						player.wuDaoMag.addWuDaoEx(wuDaoType, num / num2);
					}
					num3++;
				}
			}
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x001EB8B4 File Offset: 0x001E9AB4
		public static void AddWuDao(int num, List<int> xishuList, int listXi = 2)
		{
			Avatar player = Tools.instance.getPlayer();
			int num2 = xishuList.Count / listXi;
			if (num2 > 0)
			{
				int num3 = 0;
				foreach (int wuDaoType in xishuList)
				{
					if (num3 % listXi == 0)
					{
						player.wuDaoMag.addWuDaoEx(wuDaoType, num / num2);
					}
					num3++;
				}
			}
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x001EB934 File Offset: 0x001E9B34
		public static int getAddWuDaoEx(int timeday, float xishu, List<int> xishuList, int listXi = 2)
		{
			Tools.instance.getPlayer();
			int num = (int)(xishu * (float)timeday);
			int num2 = xishuList.Count / listXi;
			if (num2 > 0)
			{
				using (List<int>.Enumerator enumerator = xishuList.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						int num3 = enumerator.Current;
						return num / num2;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x001EB9A4 File Offset: 0x001E9BA4
		public void realizeSeid(int seid)
		{
			int i = 0;
			while (i < 500)
			{
				if (i == seid)
				{
					MethodInfo method = base.GetType().GetMethod("realizeSeid" + seid);
					if (method != null)
					{
						method.Invoke(this, new object[]
						{
							seid
						});
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x001EBA04 File Offset: 0x001E9C04
		public bool CanNextSeid(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			return seid != 16 || player.TianFuID.HasField(string.Concat(17));
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x001EBA40 File Offset: 0x001E9C40
		public JSONObject getSeidJson(int seid)
		{
			int num = this.itemID;
			if (num > jsonData.QingJiaoItemIDSegment)
			{
				num -= jsonData.QingJiaoItemIDSegment;
			}
			return jsonData.instance.ItemsSeidJsonData[seid][num.ToString()];
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x001EBA7C File Offset: 0x001E9C7C
		public JSONObject getItemJson()
		{
			return jsonData.instance.ItemJsonData[this.itemID.ToString()];
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x001EBA98 File Offset: 0x001E9C98
		public void realizeSeid1(int seid)
		{
			((Avatar)KBEngineApp.app.player()).addHasSkillList((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x001EBAC8 File Offset: 0x001E9CC8
		public void realizeSeid2(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			avatar.addHasStaticSkillList((int)this.getSeidJson(seid)["value1"].n, 1);
			new StaticSkill(Tools.instance.getStaticSkillKeyByID((int)this.getSeidJson(seid)["value1"].n), 0, 5).Puting(avatar, avatar, 3);
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x001EBB33 File Offset: 0x001E9D33
		public void realizeSeid3(int seid)
		{
			((Avatar)KBEngineApp.app.player()).AllMapAddHP((int)this.getSeidJson(seid)["value1"].n, DeathType.身死道消);
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x001EBB64 File Offset: 0x001E9D64
		public void realizeSeid4(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			UIPopTip.Inst.Pop("你的修为提升了" + this.getSeidJson(seid)["value1"].I, PopTipIconType.上箭头);
			avatar.addEXP(this.getSeidJson(seid)["value1"].I);
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x001EBBCB File Offset: 0x001E9DCB
		public void realizeSeid5(int seid)
		{
			((Avatar)KBEngineApp.app.player())._shengShi += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x001EBC00 File Offset: 0x001E9E00
		public void realizeSeid6(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			avatar._HP_Max += (int)this.getSeidJson(seid)["value1"].n;
			avatar.HP += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x001EBC63 File Offset: 0x001E9E63
		public void realizeSeid7(int seid)
		{
			((Avatar)KBEngineApp.app.player()).shouYuan += (uint)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x001EBC97 File Offset: 0x001E9E97
		public void realizeSeid8(int seid)
		{
			((Avatar)KBEngineApp.app.player())._xinjin += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x001EBCCB File Offset: 0x001E9ECB
		public void realizeSeid9(int seid)
		{
			((Avatar)KBEngineApp.app.player()).addZiZhi((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x001EBCF8 File Offset: 0x001E9EF8
		public void realizeSeid10(int seid)
		{
			((Avatar)KBEngineApp.app.player()).addWuXin((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x001EBD25 File Offset: 0x001E9F25
		public void realizeSeid11(int seid)
		{
			((Avatar)KBEngineApp.app.player())._dunSu += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x001EBD5C File Offset: 0x001E9F5C
		public void realizeSeid12(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = 0;
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				if (avatar.StreamData.DanYaoBuFFDict.ContainsKey(jsonobject.I))
				{
					Dictionary<int, int> danYaoBuFFDict = avatar.StreamData.DanYaoBuFFDict;
					int i = jsonobject.I;
					danYaoBuFFDict[i] += this.getSeidJson(seid)["value2"][num].I;
				}
				else
				{
					avatar.StreamData.DanYaoBuFFDict.Add(jsonobject.I, this.getSeidJson(seid)["value2"][num].I);
				}
				num++;
			}
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x001EBE64 File Offset: 0x001EA064
		public void realizeSeid13(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			JSONObject jsonobject = jsonData.instance.LianDanDanFangBiao[((int)this.getSeidJson(seid)["value1"].n).ToString()];
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 1; i <= 5; i++)
			{
				list.Add((int)jsonobject["value" + i].n);
				list2.Add((int)jsonobject["num" + i].n);
			}
			avatar.addDanFang(jsonobject["ItemID"].I, list, list2);
			LianDanMag.AddWuDaoLianDan(jsonobject["ItemID"].I, 1);
			UIPopTip.Inst.Pop("学会了" + _ItemJsonData.DataDict[jsonobject["ItemID"].I].name + "炼制配方", PopTipIconType.包裹);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x001EBF7A File Offset: 0x001EA17A
		public void realizeSeid14(int seid)
		{
			((Avatar)KBEngineApp.app.player()).statiReduceDandu((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x001EBFA8 File Offset: 0x001EA1A8
		public void realizeSeid15(int seid)
		{
			List<int> lingGeng = ((Avatar)KBEngineApp.app.player()).LingGeng;
			int index = (int)this.getSeidJson(seid)["value1"].n;
			lingGeng[index] += (int)this.getSeidJson(seid)["value2"].n;
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x001EC008 File Offset: 0x001EA208
		public void realizeSeid17(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				JSONObject jsonobject2 = jsonData.instance.LianDanDanFangBiao[jsonobject.I.ToString()];
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				for (int i = 1; i <= 5; i++)
				{
					list.Add((int)jsonobject2["value" + i].n);
					list2.Add((int)jsonobject2["num" + i].n);
				}
				avatar.addDanFang(jsonobject2["ItemID"].I, list, list2);
				UIPopTip.Inst.Pop("学会了" + _ItemJsonData.DataDict[jsonobject2["ItemID"].I].name + "炼制配方", PopTipIconType.包裹);
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001EC154 File Offset: 0x001EA354
		public void realizeSeid18(int seid)
		{
			Singleton.ints.TuJIanPlan.open();
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x001EC168 File Offset: 0x001EA368
		public void realizeSeid19(int seid)
		{
			Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(this.getSeidJson(seid)["value1"].I, this.getSeidJson(seid)["value2"].I);
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x001EC1B8 File Offset: 0x001EA3B8
		public void realizeSeid20(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			JSONObject seidJson = this.getSeidJson(seid);
			int num = 0;
			foreach (JSONObject jsonobject in seidJson["value1"].list)
			{
				player.addItem(seidJson["value1"][num].I, seidJson["value2"][num].I, Tools.CreateItemSeid(seidJson["value1"][num].I), true);
				num++;
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x001EC278 File Offset: 0x001EA478
		public void realizeSeid22(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			JSONObject seidJson = this.getSeidJson(seid);
			if (!player.nomelTaskMag.IsNTaskStart(seidJson["value1"].I))
			{
				player.nomelTaskMag.StartNTask(seidJson["value1"].I, 1);
			}
			UIPopTip.Inst.Pop("获得一条新的传闻", PopTipIconType.任务进度);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x001EC2E4 File Offset: 0x001EA4E4
		public void realizeSeid23(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			int value = ItemsSeidJsonData23.DataDict[this.itemID].value1;
			foreach (JToken jtoken in jsonData.instance.EndlessSeaHaiYuData[value.ToString()]["shuxing"])
			{
				int index = (int)player.EndlessSea["AllIaLand"][(int)jtoken - 1];
				EndlessSeaMag.AddSeeIsland(EndlessSeaMag.GetRealIndex((int)jtoken, index));
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.SeaStaticIsland)
			{
				if ((int)keyValuePair.Value["SeaID"] == value)
				{
					EndlessSeaMag.AddSeeIsland((int)keyValuePair.Value["IsLandIndex"]);
				}
			}
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x001EC414 File Offset: 0x001EA614
		public void realizeSeid24(int seid)
		{
			Singleton.ints.ShowSeaMapUI();
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x001EC420 File Offset: 0x001EA620
		public void realizeSeid25(int seid)
		{
			Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(this.getSeidJson(seid)["value1"].I, this.getSeidJson(seid)["value2"].I);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x001EC46D File Offset: 0x001EA66D
		public void realizeSeid26(int seid)
		{
			Tools.instance.getPlayer()._WuDaoDian += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x001EC49C File Offset: 0x001EA69C
		public void realizeSeid27(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			player.ItemBuffList[seid.ToString()] = new JObject();
			JObject jobject = (JObject)player.ItemBuffList[seid.ToString()];
			jobject["AIType"] = this.getSeidJson(seid)["value1"].I;
			jobject["StartTime"] = player.worldTimeMag.nowTime;
			jobject["ContinueTime"] = this.getSeidJson(seid)["value2"].I;
			jobject["icon"] = this.getSeidJson(seid)["value3"].str;
			jobject["start"] = true;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x001EC580 File Offset: 0x001EA780
		public void realizeSeid28(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				player.YaoCaiChanDi.Add(jsonobject.I);
			}
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x001EC5F8 File Offset: 0x001EA7F8
		public void realizeSeid29(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			if ((int)player.level < this.getSeidJson(seid)["value1"].I)
			{
				player.AddDandu(this.getSeidJson(seid)["value2"].I);
			}
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x001EC64C File Offset: 0x001EA84C
		public void realizeSeid30(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			avatar.addHasSkillList((int)this.getSeidJson(seid)["value1"].n);
			avatar.addHasStaticSkillList((int)this.getSeidJson(seid)["value2"].n, 1);
			new StaticSkill(Tools.instance.getStaticSkillKeyByID((int)this.getSeidJson(seid)["value2"].n), 0, 5).Puting(avatar, avatar, 3);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x001EC6D4 File Offset: 0x001EA8D4
		public void realizeSeid32(int seid)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			PlayerEx.StudyShuangXiuSkill(i);
			UIPopTip.Inst.Pop("学会了" + ShuangXiuMiShu.DataDict[i].name, PopTipIconType.包裹);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x001EC724 File Offset: 0x001EA924
		public void realizeSeid33(int seid)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			PlayerEx.AddSeaTanSuoDu(i, i2);
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x001EC764 File Offset: 0x001EA964
		public void realizeSeid34(int seid)
		{
			List<int> list = this.getSeidJson(seid)["value1"].ToList();
			Avatar player = Tools.instance.getPlayer();
			foreach (int caoYaoId in list)
			{
				player.UnLockCaoYaoData(caoYaoId);
			}
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x001EC7D4 File Offset: 0x001EA9D4
		public void realizeSeid35(int seid)
		{
			Tools.instance.getPlayer().IsCanSetFace = true;
		}

		// Token: 0x040048FB RID: 18683
		private string _itemName;

		// Token: 0x040048FC RID: 18684
		public string UUID = "";

		// Token: 0x040048FD RID: 18685
		public int itemID = -1;

		// Token: 0x040048FE RID: 18686
		public string itemNameCN;

		// Token: 0x040048FF RID: 18687
		public string itemDesc;

		// Token: 0x04004900 RID: 18688
		private Texture2D _itemIcon;

		// Token: 0x04004901 RID: 18689
		private Sprite _itemIconSprite;

		// Token: 0x04004902 RID: 18690
		private Texture2D _itemPingZhi;

		// Token: 0x04004903 RID: 18691
		private Sprite _itemPingZhiSprite;

		// Token: 0x04004904 RID: 18692
		public Sprite _itemPingZhiUP;

		// Token: 0x04004905 RID: 18693
		private Sprite _newitemPingZhiSprite;

		// Token: 0x04004906 RID: 18694
		public Sprite _newitemPingZhiUP;

		// Token: 0x04004907 RID: 18695
		public int ColorIndex;

		// Token: 0x04004908 RID: 18696
		public int itemNum;

		// Token: 0x04004909 RID: 18697
		public int itemMaxNum;

		// Token: 0x0400490A RID: 18698
		public item.ItemType itemType;

		// Token: 0x0400490B RID: 18699
		public int itemtype;

		// Token: 0x0400490C RID: 18700
		public int itemPrice;

		// Token: 0x0400490D RID: 18701
		public JSONObject Seid = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x0400490E RID: 18702
		public int ExGoodsID = -1;

		// Token: 0x0400490F RID: 18703
		public Texture2D ExItemIcon;

		// Token: 0x04004910 RID: 18704
		public int StuTime;

		// Token: 0x04004911 RID: 18705
		private int _quality;

		// Token: 0x04004912 RID: 18706
		public List<int> seid = new List<int>();

		// Token: 0x04004913 RID: 18707
		private bool initedImage;

		// Token: 0x04004914 RID: 18708
		private UnityAction inventoryNext;

		// Token: 0x0200157C RID: 5500
		public enum ItemType
		{
			// Token: 0x04006F8A RID: 28554
			Weapon,
			// Token: 0x04006F8B RID: 28555
			Clothing,
			// Token: 0x04006F8C RID: 28556
			Ring,
			// Token: 0x04006F8D RID: 28557
			Potion,
			// Token: 0x04006F8E RID: 28558
			Task,
			// Token: 0x04006F8F RID: 28559
			Casque,
			// Token: 0x04006F90 RID: 28560
			Shoes,
			// Token: 0x04006F91 RID: 28561
			Trousers,
			// Token: 0x04006F92 RID: 28562
			LinZhou = 14
		}

		// Token: 0x0200157D RID: 5501
		public enum ItemSeid
		{
			// Token: 0x04006F94 RID: 28564
			Seid21 = 21
		}
	}
}
