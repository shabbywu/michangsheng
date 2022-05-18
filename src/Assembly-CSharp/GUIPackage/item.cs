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
	// Token: 0x02000D56 RID: 3414
	[Serializable]
	public class item
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06005141 RID: 20801 RVA: 0x0003A81E File Offset: 0x00038A1E
		// (set) Token: 0x06005142 RID: 20802 RVA: 0x0003A856 File Offset: 0x00038A56
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

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06005143 RID: 20803 RVA: 0x0021D978 File Offset: 0x0021BB78
		// (set) Token: 0x06005144 RID: 20804 RVA: 0x0003A85F File Offset: 0x00038A5F
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

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x0021D9CC File Offset: 0x0021BBCC
		// (set) Token: 0x06005146 RID: 20806 RVA: 0x0003A868 File Offset: 0x00038A68
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

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x0021DA20 File Offset: 0x0021BC20
		// (set) Token: 0x06005148 RID: 20808 RVA: 0x0003A871 File Offset: 0x00038A71
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x0021DA88 File Offset: 0x0021BC88
		// (set) Token: 0x0600514A RID: 20810 RVA: 0x0003A87A File Offset: 0x00038A7A
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

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600514B RID: 20811 RVA: 0x0021DAE8 File Offset: 0x0021BCE8
		// (set) Token: 0x0600514C RID: 20812 RVA: 0x0003A883 File Offset: 0x00038A83
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

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600514D RID: 20813 RVA: 0x0021DB50 File Offset: 0x0021BD50
		// (set) Token: 0x0600514E RID: 20814 RVA: 0x0003A88C File Offset: 0x00038A8C
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

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x0021DBB8 File Offset: 0x0021BDB8
		// (set) Token: 0x06005150 RID: 20816 RVA: 0x0003A895 File Offset: 0x00038A95
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06005151 RID: 20817 RVA: 0x0003A89E File Offset: 0x00038A9E
		// (set) Token: 0x06005152 RID: 20818 RVA: 0x0003A8D6 File Offset: 0x00038AD6
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

		// Token: 0x06005153 RID: 20819 RVA: 0x0021DC28 File Offset: 0x0021BE28
		public item()
		{
			this.itemID = -1;
			this.UUID = "";
			this.ExGoodsID = -1;
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0003A8DF File Offset: 0x00038ADF
		public item(string name, int id, string nameCN, string desc, int max_num, item.ItemType type, int price)
		{
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0021DC84 File Offset: 0x0021BE84
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

		// Token: 0x06005156 RID: 20822 RVA: 0x0021DDF4 File Offset: 0x0021BFF4
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

		// Token: 0x06005157 RID: 20823 RVA: 0x0021E110 File Offset: 0x0021C310
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

		// Token: 0x06005158 RID: 20824 RVA: 0x0021E34C File Offset: 0x0021C54C
		public int GetItemPrice()
		{
			int i = this.itemPrice;
			if (this.Seid != null && this.Seid.HasField("Money"))
			{
				i = this.Seid["Money"].I;
			}
			return (int)((float)i * 0.5f);
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0021E39C File Offset: 0x0021C59C
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

		// Token: 0x0600515A RID: 20826 RVA: 0x0021E4D4 File Offset: 0x0021C6D4
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

		// Token: 0x0600515B RID: 20827 RVA: 0x0021E628 File Offset: 0x0021C828
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

		// Token: 0x0600515C RID: 20828 RVA: 0x0021E648 File Offset: 0x0021C848
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

		// Token: 0x0600515D RID: 20829 RVA: 0x0021E6CC File Offset: 0x0021C8CC
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

		// Token: 0x0600515E RID: 20830 RVA: 0x0003A917 File Offset: 0x00038B17
		public item Clone()
		{
			return base.MemberwiseClone() as item;
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0021E7F0 File Offset: 0x0021C9F0
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

		// Token: 0x06005160 RID: 20832 RVA: 0x0021E840 File Offset: 0x0021CA40
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

		// Token: 0x06005161 RID: 20833 RVA: 0x000C6B9C File Offset: 0x000C4D9C
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

		// Token: 0x06005162 RID: 20834 RVA: 0x0021E934 File Offset: 0x0021CB34
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

		// Token: 0x06005163 RID: 20835 RVA: 0x0021EA70 File Offset: 0x0021CC70
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

		// Token: 0x06005164 RID: 20836 RVA: 0x0021EB2C File Offset: 0x0021CD2C
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

		// Token: 0x06005165 RID: 20837 RVA: 0x0021EBA0 File Offset: 0x0021CDA0
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

		// Token: 0x06005166 RID: 20838 RVA: 0x0021EBDC File Offset: 0x0021CDDC
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
				if (num > 100000)
				{
					num -= 100000;
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
					goto IL_392;
				}
			}
			if ((type == 5 || type == 13) && itemCanUseNum > 0)
			{
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
			IL_392:
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

		// Token: 0x06005167 RID: 20839 RVA: 0x0021F2E8 File Offset: 0x0021D4E8
		public void AddNaiYaoXin()
		{
			if (!Tools.instance.getPlayer().NaiYaoXin.HasField(string.Concat(this.itemID)))
			{
				Tools.instance.getPlayer().NaiYaoXin.AddField(string.Concat(this.itemID), 0);
			}
			int num = (int)Tools.instance.getPlayer().NaiYaoXin[string.Concat(this.itemID)].n;
			Tools.instance.getPlayer().NaiYaoXin.SetField(string.Concat(this.itemID), num + 1);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0021F394 File Offset: 0x0021D594
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

		// Token: 0x06005169 RID: 20841 RVA: 0x0021F418 File Offset: 0x0021D618
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

		// Token: 0x0600516A RID: 20842 RVA: 0x0021F498 File Offset: 0x0021D698
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

		// Token: 0x0600516B RID: 20843 RVA: 0x0021F508 File Offset: 0x0021D708
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

		// Token: 0x0600516C RID: 20844 RVA: 0x0021F568 File Offset: 0x0021D768
		public bool CanNextSeid(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			return seid != 16 || player.TianFuID.HasField(string.Concat(17));
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0021F5A4 File Offset: 0x0021D7A4
		public JSONObject getSeidJson(int seid)
		{
			int num = this.itemID;
			if (num > 100000)
			{
				num -= 100000;
			}
			return jsonData.instance.ItemsSeidJsonData[seid][num.ToString()];
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0003A924 File Offset: 0x00038B24
		public JSONObject getItemJson()
		{
			return jsonData.instance.ItemJsonData[this.itemID.ToString()];
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0003A940 File Offset: 0x00038B40
		public void realizeSeid1(int seid)
		{
			((Avatar)KBEngineApp.app.player()).addHasSkillList((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0021F5E0 File Offset: 0x0021D7E0
		public void realizeSeid2(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			avatar.addHasStaticSkillList((int)this.getSeidJson(seid)["value1"].n, 1);
			new StaticSkill(Tools.instance.getStaticSkillKeyByID((int)this.getSeidJson(seid)["value1"].n), 0, 5).Puting(avatar, avatar, 3);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0003A96D File Offset: 0x00038B6D
		public void realizeSeid3(int seid)
		{
			((Avatar)KBEngineApp.app.player()).AllMapAddHP((int)this.getSeidJson(seid)["value1"].n, DeathType.身死道消);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0021F64C File Offset: 0x0021D84C
		public void realizeSeid4(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			UIPopTip.Inst.Pop("你的修为提升了" + this.getSeidJson(seid)["value1"].I, PopTipIconType.上箭头);
			avatar.addEXP(this.getSeidJson(seid)["value1"].I);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0003A99B File Offset: 0x00038B9B
		public void realizeSeid5(int seid)
		{
			((Avatar)KBEngineApp.app.player())._shengShi += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0021F6B4 File Offset: 0x0021D8B4
		public void realizeSeid6(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			avatar._HP_Max += (int)this.getSeidJson(seid)["value1"].n;
			avatar.HP += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x0003A9CE File Offset: 0x00038BCE
		public void realizeSeid7(int seid)
		{
			((Avatar)KBEngineApp.app.player()).shouYuan += (uint)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0003AA02 File Offset: 0x00038C02
		public void realizeSeid8(int seid)
		{
			((Avatar)KBEngineApp.app.player())._xinjin += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x0003AA36 File Offset: 0x00038C36
		public void realizeSeid9(int seid)
		{
			((Avatar)KBEngineApp.app.player()).addZiZhi((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0003AA63 File Offset: 0x00038C63
		public void realizeSeid10(int seid)
		{
			((Avatar)KBEngineApp.app.player()).addWuXin((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x0003AA90 File Offset: 0x00038C90
		public void realizeSeid11(int seid)
		{
			((Avatar)KBEngineApp.app.player())._dunSu += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x0021F718 File Offset: 0x0021D918
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

		// Token: 0x0600517B RID: 20859 RVA: 0x0021F820 File Offset: 0x0021DA20
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
			avatar.addDanFang((int)jsonobject["ItemID"].n, list, list2);
			LianDanMag.AddWuDaoLianDan((int)jsonobject["ItemID"].n, 1);
			UIPopTip.Inst.Pop("学会了" + _ItemJsonData.DataDict[jsonobject["ItemID"].I].name + "炼制配方", PopTipIconType.包裹);
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0003AAC4 File Offset: 0x00038CC4
		public void realizeSeid14(int seid)
		{
			((Avatar)KBEngineApp.app.player()).statiReduceDandu((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x0021F938 File Offset: 0x0021DB38
		public void realizeSeid15(int seid)
		{
			List<int> lingGeng = ((Avatar)KBEngineApp.app.player()).LingGeng;
			int index = (int)this.getSeidJson(seid)["value1"].n;
			lingGeng[index] += (int)this.getSeidJson(seid)["value2"].n;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x0021F998 File Offset: 0x0021DB98
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
				avatar.addDanFang((int)jsonobject2["ItemID"].n, list, list2);
				UIPopTip.Inst.Pop("学会了" + _ItemJsonData.DataDict[jsonobject2["ItemID"].I].name + "炼制配方", PopTipIconType.包裹);
			}
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0003AAF1 File Offset: 0x00038CF1
		public void realizeSeid18(int seid)
		{
			Singleton.ints.TuJIanPlan.open();
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x0021FAE4 File Offset: 0x0021DCE4
		public void realizeSeid19(int seid)
		{
			Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(this.getSeidJson(seid)["value1"].I, this.getSeidJson(seid)["value2"].I);
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x0021FB34 File Offset: 0x0021DD34
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

		// Token: 0x06005182 RID: 20866 RVA: 0x0021FBF4 File Offset: 0x0021DDF4
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

		// Token: 0x06005183 RID: 20867 RVA: 0x0021FC60 File Offset: 0x0021DE60
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

		// Token: 0x06005184 RID: 20868 RVA: 0x0003AB02 File Offset: 0x00038D02
		public void realizeSeid24(int seid)
		{
			Singleton.ints.ShowSeaMapUI();
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x0021FAE4 File Offset: 0x0021DCE4
		public void realizeSeid25(int seid)
		{
			Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(this.getSeidJson(seid)["value1"].I, this.getSeidJson(seid)["value2"].I);
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x0003AB0E File Offset: 0x00038D0E
		public void realizeSeid26(int seid)
		{
			Tools.instance.getPlayer()._WuDaoDian += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0021FD90 File Offset: 0x0021DF90
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

		// Token: 0x06005188 RID: 20872 RVA: 0x0021FE74 File Offset: 0x0021E074
		public void realizeSeid28(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				player.YaoCaiChanDi.Add(jsonobject.I);
			}
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x0021FEEC File Offset: 0x0021E0EC
		public void realizeSeid29(int seid)
		{
			Avatar player = Tools.instance.getPlayer();
			if ((int)player.level < this.getSeidJson(seid)["value1"].I)
			{
				player.AddDandu(this.getSeidJson(seid)["value2"].I);
			}
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0021FF40 File Offset: 0x0021E140
		public void realizeSeid30(int seid)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			avatar.addHasSkillList((int)this.getSeidJson(seid)["value1"].n);
			avatar.addHasStaticSkillList((int)this.getSeidJson(seid)["value2"].n, 1);
			new StaticSkill(Tools.instance.getStaticSkillKeyByID((int)this.getSeidJson(seid)["value2"].n), 0, 5).Puting(avatar, avatar, 3);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0021FFC8 File Offset: 0x0021E1C8
		public void realizeSeid32(int seid)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			PlayerEx.StudyShuangXiuSkill(i);
			UIPopTip.Inst.Pop("学会了" + ShuangXiuMiShu.DataDict[i].name, PopTipIconType.包裹);
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00220018 File Offset: 0x0021E218
		public void realizeSeid33(int seid)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			PlayerEx.AddSeaTanSuoDu(i, i2);
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00220058 File Offset: 0x0021E258
		public void realizeSeid34(int seid)
		{
			List<int> list = this.getSeidJson(seid)["value1"].ToList();
			Avatar player = Tools.instance.getPlayer();
			foreach (int caoYaoId in list)
			{
				player.UnLockCaoYaoData(caoYaoId);
			}
		}

		// Token: 0x0400523F RID: 21055
		private string _itemName;

		// Token: 0x04005240 RID: 21056
		public string UUID = "";

		// Token: 0x04005241 RID: 21057
		public int itemID = -1;

		// Token: 0x04005242 RID: 21058
		public string itemNameCN;

		// Token: 0x04005243 RID: 21059
		public string itemDesc;

		// Token: 0x04005244 RID: 21060
		private Texture2D _itemIcon;

		// Token: 0x04005245 RID: 21061
		private Sprite _itemIconSprite;

		// Token: 0x04005246 RID: 21062
		private Texture2D _itemPingZhi;

		// Token: 0x04005247 RID: 21063
		private Sprite _itemPingZhiSprite;

		// Token: 0x04005248 RID: 21064
		public Sprite _itemPingZhiUP;

		// Token: 0x04005249 RID: 21065
		private Sprite _newitemPingZhiSprite;

		// Token: 0x0400524A RID: 21066
		public Sprite _newitemPingZhiUP;

		// Token: 0x0400524B RID: 21067
		public int ColorIndex;

		// Token: 0x0400524C RID: 21068
		public int itemNum;

		// Token: 0x0400524D RID: 21069
		public int itemMaxNum;

		// Token: 0x0400524E RID: 21070
		public item.ItemType itemType;

		// Token: 0x0400524F RID: 21071
		public int itemtype;

		// Token: 0x04005250 RID: 21072
		public int itemPrice;

		// Token: 0x04005251 RID: 21073
		public JSONObject Seid = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005252 RID: 21074
		public int ExGoodsID = -1;

		// Token: 0x04005253 RID: 21075
		public Texture2D ExItemIcon;

		// Token: 0x04005254 RID: 21076
		public int StuTime;

		// Token: 0x04005255 RID: 21077
		private int _quality;

		// Token: 0x04005256 RID: 21078
		public List<int> seid = new List<int>();

		// Token: 0x04005257 RID: 21079
		private bool initedImage;

		// Token: 0x04005258 RID: 21080
		private UnityAction inventoryNext;

		// Token: 0x02000D57 RID: 3415
		public enum ItemType
		{
			// Token: 0x0400525A RID: 21082
			Weapon,
			// Token: 0x0400525B RID: 21083
			Clothing,
			// Token: 0x0400525C RID: 21084
			Ring,
			// Token: 0x0400525D RID: 21085
			Potion,
			// Token: 0x0400525E RID: 21086
			Task,
			// Token: 0x0400525F RID: 21087
			Casque,
			// Token: 0x04005260 RID: 21088
			Shoes,
			// Token: 0x04005261 RID: 21089
			Trousers,
			// Token: 0x04005262 RID: 21090
			LinZhou = 14
		}

		// Token: 0x02000D58 RID: 3416
		public enum ItemSeid
		{
			// Token: 0x04005264 RID: 21092
			Seid21 = 21
		}
	}
}
