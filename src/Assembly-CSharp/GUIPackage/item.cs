using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

[Serializable]
public class item
{
	public enum ItemType
	{
		Weapon = 0,
		Clothing = 1,
		Ring = 2,
		Potion = 3,
		Task = 4,
		Casque = 5,
		Shoes = 6,
		Trousers = 7,
		LinZhou = 14
	}

	public enum ItemSeid
	{
		Seid21 = 21
	}

	private string _itemName;

	public string UUID = "";

	public int itemID = -1;

	public string itemNameCN;

	public string itemDesc;

	private Texture2D _itemIcon;

	private Sprite _itemIconSprite;

	private Texture2D _itemPingZhi;

	private Sprite _itemPingZhiSprite;

	public Sprite _itemPingZhiUP;

	private Sprite _newitemPingZhiSprite;

	public Sprite _newitemPingZhiUP;

	public int ColorIndex;

	public int itemNum;

	public int itemMaxNum;

	public ItemType itemType;

	public int itemtype;

	public int itemPrice;

	public JSONObject Seid = new JSONObject(JSONObject.Type.OBJECT);

	public int ExGoodsID = -1;

	public Texture2D ExItemIcon;

	public int StuTime;

	private int _quality;

	public List<int> seid = new List<int>();

	private bool initedImage;

	private UnityAction inventoryNext;

	public string itemName
	{
		get
		{
			if (Seid != null && Seid.HasField("Name"))
			{
				return Seid["Name"].str;
			}
			return _itemName;
		}
		set
		{
			_itemName = value;
		}
	}

	public Texture2D itemIcon
	{
		get
		{
			InitImage();
			if (Seid != null && Seid.HasField("ItemIcon"))
			{
				return ResManager.inst.LoadTexture2D(Seid["ItemIcon"].str);
			}
			return _itemIcon;
		}
		set
		{
			_itemIcon = value;
		}
	}

	public Sprite itemIconSprite
	{
		get
		{
			InitImage();
			if (Seid != null && Seid.HasField("ItemIcon"))
			{
				return ResManager.inst.LoadSprite(Seid["ItemIcon"].str);
			}
			return _itemIconSprite;
		}
		set
		{
			_itemIconSprite = value;
		}
	}

	public Texture2D itemPingZhi
	{
		get
		{
			InitImage();
			if (Seid != null && Seid.HasField("quality"))
			{
				int i = Seid["quality"].I;
				return ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + (i + 1));
			}
			return _itemPingZhi;
		}
		set
		{
			_itemPingZhi = value;
		}
	}

	public Sprite itemPingZhiSprite
	{
		get
		{
			if (Seid != null && Seid.HasField("quality"))
			{
				int i = Seid["quality"].I;
				return ResManager.inst.LoadSprite("Ui Icon/tab/item" + (i + 1));
			}
			return _itemPingZhiSprite;
		}
		set
		{
			_itemPingZhiSprite = value;
		}
	}

	public Sprite itemPingZhiUP
	{
		get
		{
			InitImage();
			if (Seid != null && Seid.HasField("quality"))
			{
				int i = Seid["quality"].I;
				return ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + (i + 1));
			}
			return _itemPingZhiUP;
		}
		set
		{
			_itemPingZhiUP = value;
		}
	}

	public Sprite newitemPingZhiSprite
	{
		get
		{
			InitImage();
			if (Seid != null && Seid.HasField("quality"))
			{
				int i = Seid["quality"].I;
				return ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + (i + 1));
			}
			return _newitemPingZhiSprite;
		}
		set
		{
			_newitemPingZhiSprite = value;
		}
	}

	public Sprite newitemPingZhiUP
	{
		get
		{
			InitImage();
			if (Seid != null && Seid.HasField("quality"))
			{
				int num = (ColorIndex = Seid["quality"].I);
				return ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + (num + 1));
			}
			return _newitemPingZhiUP;
		}
		set
		{
			_newitemPingZhiUP = value;
		}
	}

	public int quality
	{
		get
		{
			if (Seid != null && Seid.HasField("quality"))
			{
				return Seid["quality"].I;
			}
			return _quality;
		}
		set
		{
			_quality = value;
		}
	}

	public item()
	{
		itemID = -1;
		UUID = "";
		ExGoodsID = -1;
	}

	public item(string name, int id, string nameCN, string desc, int max_num, ItemType type, int price)
	{
	}

	public item(int id)
	{
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
		itemID = id;
		itemNameCN = itemJsonData.name;
		itemDesc = itemJsonData.desc;
		itemName = itemNameCN;
		itemNum = 1;
		itemMaxNum = itemJsonData.maxNum;
		itemType = (ItemType)itemJsonData.type;
		itemtype = itemJsonData.type;
		itemPrice = itemJsonData.price;
		StuTime = itemJsonData.StuTime;
		quality = itemJsonData.quality;
		if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
		{
			ColorIndex = quality;
		}
		else if (itemJsonData.type == 3 || itemJsonData.type == 4)
		{
			ColorIndex = quality * 2 - 1;
		}
		else
		{
			ColorIndex = quality - 1;
		}
		foreach (int item in itemJsonData.seid)
		{
			seid.Add(item);
		}
	}

	public void InitImage()
	{
		if (initedImage)
		{
			return;
		}
		initedImage = true;
		if (_ItemJsonData.DataDict.ContainsKey(itemID))
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
			if (itemJsonData.ItemIcon == 0)
			{
				itemIcon = ResManager.inst.LoadTexture2D("Item Icon/" + itemID);
				itemIconSprite = ResManager.inst.LoadSprite("Item Icon/" + itemID);
			}
			else
			{
				itemIcon = ResManager.inst.LoadTexture2D("Item Icon/" + itemJsonData.ItemIcon);
				itemIconSprite = ResManager.inst.LoadSprite("Item Icon/" + itemJsonData.ItemIcon);
			}
			if ((Object)(object)itemIcon == (Object)null)
			{
				itemIcon = ResManager.inst.LoadTexture2D("Item Icon/1");
				itemIconSprite = ResManager.inst.LoadSprite("Item Icon/1");
			}
			if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
			{
				itemPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + (quality + 1));
				itemPingZhiUP = ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + (quality + 1));
				_newitemPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + (quality + 1));
				_newitemPingZhiUP = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + (quality + 1));
			}
			else if (itemJsonData.type == 3 || itemJsonData.type == 4)
			{
				itemPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + quality * 2);
				itemPingZhiUP = ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + quality * 2);
				_newitemPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + quality * 2);
				_newitemPingZhiUP = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + quality * 2);
			}
			else
			{
				itemPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + quality);
				itemPingZhiUP = ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + quality);
				_newitemPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + quality);
				_newitemPingZhiUP = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + quality);
			}
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"itemID = {itemID}");
		stringBuilder.AppendLine("itemNameCN = " + itemNameCN);
		stringBuilder.AppendLine("itemDesc = " + itemDesc);
		stringBuilder.AppendLine("itemName = " + itemName);
		stringBuilder.AppendLine($"itemNum = {itemNum}");
		stringBuilder.AppendLine($"itemMaxNum = {itemMaxNum}");
		stringBuilder.AppendLine($"itemType = {itemType}");
		stringBuilder.AppendLine($"itemtype = {itemtype}");
		stringBuilder.AppendLine($"itemPrice = {itemPrice}");
		stringBuilder.AppendLine($"StuTime = {StuTime}");
		stringBuilder.AppendLine($"quality = {quality}");
		List<int> list = new List<int>();
		if (Seid != null && Seid.HasField("ItemFlag"))
		{
			list = Seid["ItemFlag"].ToList();
		}
		else if (_ItemJsonData.DataDict[itemID].ItemFlag.Count > 0)
		{
			list = _ItemJsonData.DataDict[itemID].ItemFlag;
		}
		if (list.Count > 0)
		{
			stringBuilder.Append("ItemFlag = ");
			foreach (int item in list)
			{
				stringBuilder.Append(item.ToItemFlagName() + " ");
			}
			stringBuilder.Append("\n");
		}
		stringBuilder.AppendLine("Seid = " + Seid.ToString().ToCN());
		return stringBuilder.ToString();
	}

	public int GetItemPrice()
	{
		int i = itemPrice;
		if (Seid != null && Seid.HasField("Money"))
		{
			i = Seid["Money"].I;
		}
		return (int)((float)i * 0.5f);
	}

	public int GetItemOriPrice()
	{
		int i = itemPrice;
		if (Seid != null && Seid.HasField("Money"))
		{
			i = Seid["Money"].I;
		}
		float num = 1f;
		if (itemtype == 9)
		{
			if (Seid.HasField("NaiJiu"))
			{
				num = Seid["NaiJiu"].f / 100f;
			}
			else
			{
				Debug.LogError((object)$"物品ID:{itemID},物品名称：{itemName},不存在耐久度");
			}
		}
		else if (itemtype == 14)
		{
			if (Seid.HasField("NaiJiu"))
			{
				num = Seid["NaiJiu"].f / (float)jsonData.instance.LingZhouPinJie[quality.ToString()][(object)"Naijiu"];
			}
			else
			{
				Debug.LogError((object)$"物品ID:{itemID},物品名称：{itemName},不存在耐久度");
			}
		}
		return (int)((float)i * num);
	}

	public void CalcNPCZhuangTai(int npcid, out bool isJiXu, out bool isLaJi)
	{
		isJiXu = false;
		isLaJi = false;
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
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

	public int GetNPCZhuangTaiJiaCheng(int npcid)
	{
		CalcNPCZhuangTai(npcid, out var isJiXu, out var _);
		if (isJiXu)
		{
			return 120;
		}
		return 0;
	}

	public int GetJiaCheng(int npcid)
	{
		int num = 0;
		if (npcid > 0)
		{
			num += jsonData.instance.GetMonstarInterestingItem(npcid, itemID, Seid);
			num += GetNPCZhuangTaiJiaCheng(npcid);
		}
		num += SceneEx.ItemNowSceneJiaCheng(itemID);
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

	public int GetJiaoYiPrice(int npcid, bool isPlayer = false, bool zongjia = false)
	{
		npcid = NPCEx.NPCIDToNew(npcid);
		int i = itemPrice;
		int jiaCheng = GetJiaCheng(npcid);
		float num = 1f + (float)jiaCheng / 100f;
		if (Seid != null && Seid.HasField("Money"))
		{
			i = Seid["Money"].I;
		}
		float num2 = i;
		if (Seid != null && Seid.HasField("NaiJiu"))
		{
			num2 = (float)i * ItemCellEX.getItemNaiJiuPrice(this);
		}
		if (isPlayer)
		{
			num2 = num2 * 0.5f * num;
		}
		else
		{
			float num3 = (float)jsonData.instance.getSellPercent(npcid, itemID) / 100f;
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
			num4 *= itemNum;
		}
		return num4;
	}

	public item Clone()
	{
		return MemberwiseClone() as item;
	}

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

	public bool IsWuDaoCanStudy(List<int> wudaoTypeList, List<int> wudaoLvList)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (int wudaoType in wudaoTypeList)
		{
			if (player.wuDaoMag.getWuDaoLevelByType(wudaoType) < wudaoLvList[num])
			{
				string text = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[wudaoType.ToString()]["name"].str);
				Tools.Code64(jsonData.instance.WuDaoJinJieJson[wudaoLvList[num].ToString()]["Text"].str);
				string msg = text + "之道感悟不足";
				UIPopTip.Inst.Pop(msg);
				return false;
			}
			num++;
		}
		return true;
	}

	public static JSONObject getGongFaBookItem(int STSKillID)
	{
		foreach (KeyValuePair<string, JSONObject> itemJsonDatum in jsonData.instance.ItemJsonData)
		{
			if (itemJsonDatum.Value["type"].I == 4)
			{
				float result = 0f;
				if (float.TryParse(itemJsonDatum.Value["desc"].str, out result) && (int)result == STSKillID)
				{
					return itemJsonDatum.Value;
				}
			}
		}
		return null;
	}

	public static string StudyTiaoJian(List<int> wudaoTypeList, List<int> wudaoLvList)
	{
		Tools.instance.getPlayer();
		int num = 0;
		string text = "";
		bool flag = true;
		foreach (int wudaoType in wudaoTypeList)
		{
			string text2 = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[wudaoType.ToString()]["name"].str);
			string text3 = Tools.Code64(jsonData.instance.WuDaoJinJieJson[wudaoLvList[num].ToString()]["Text"].str);
			if (wudaoLvList[num] >= 1)
			{
				text = text + "对" + text2 + "之道的感悟达到" + text3 + ";";
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
		return text + "。";
	}

	public static string StudyTiSheng(List<int> wudaoTypeList, string startString = "领悟后能够提升对")
	{
		Tools.instance.getPlayer();
		int num = 0;
		string text = startString;
		foreach (int wudaoType in wudaoTypeList)
		{
			string text2 = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[wudaoType.ToString()]["name"].str);
			text = text + text2 + "之道、";
			num++;
		}
		if (text.Length > 1)
		{
			text = text.Substring(0, text.Length - 1);
		}
		return text + "的感悟。";
	}

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

	public void gongneng(UnityAction Next = null, bool isTuPo = false)
	{
		Debug.Log((object)("您使用了" + itemNameCN));
		Avatar player = PlayerEx.Player;
		if (!_ItemJsonData.DataDict.ContainsKey(itemID))
		{
			Debug.LogError((object)$"物品表没有ID为{itemID}的物品");
			return;
		}
		int type = _ItemJsonData.DataDict[itemID].type;
		int itemCanUseNum = GetItemCanUseNum(itemID);
		switch (type)
		{
		case 3:
		case 4:
		case 10:
		{
			if (type == 3 || type == 4)
			{
				List<int> wudaoTypeList = new List<int>();
				List<int> wudaoLvList = new List<int>();
				GetWuDaoType(itemID, wudaoTypeList, wudaoLvList);
				if (!IsWuDaoCanStudy(wudaoTypeList, wudaoLvList))
				{
					return;
				}
			}
			int num = itemID;
			if (num > jsonData.QingJiaoItemIDSegment)
			{
				num -= jsonData.QingJiaoItemIDSegment;
			}
			using (List<int>.Enumerator enumerator = _ItemJsonData.DataDict[num].seid.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case 1:
					{
						int id2 = ItemsSeidJsonData1.DataDict[num].value1;
						if (PlayerEx.Player.hasSkillList.Find((SkillItem s) => s.itemId == id2) != null)
						{
							UIPopTip.Inst.Pop("你已经学习过该技能");
							return;
						}
						break;
					}
					case 2:
					{
						int id = ItemsSeidJsonData2.DataDict[num].value1;
						if (player.hasStaticSkillList.Find((SkillItem s) => s.itemId == id) != null)
						{
							UIPopTip.Inst.Pop("你已经学习过该功法");
							return;
						}
						foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
						{
							if (data.Skill_ID == id)
							{
								if (data.seid.Contains(9) && player.dunSu < 18)
								{
									UIPopTip.Inst.Pop("遁速大于18方可学习");
									return;
								}
								break;
							}
						}
						break;
					}
					case 13:
					{
						int value = ItemsSeidJsonData13.DataDict[num].value1;
						if (player.ISStudyDanFan(value))
						{
							UIPopTip.Inst.Pop("你已经阅读过该丹方");
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
						break;
					}
					}
				}
			}
			break;
		}
		case 5:
		case 13:
			if (itemCanUseNum <= 0)
			{
				break;
			}
			if (jsonData.instance.ItemJsonData[string.Concat(itemID)]["seid"].ToList().Contains(35))
			{
				UIPopTip.Inst.Pop("仅能在装扮前服用");
				return;
			}
			if ((Object)(object)TpUIMag.inst == (Object)null && jsonData.instance.ItemJsonData[string.Concat(itemID)]["seid"].ToList().Contains(31))
			{
				UIPopTip.Inst.Pop("需要在突破前服用");
				return;
			}
			if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(itemID)) >= itemCanUseNum)
			{
				string msg = "已到最大耐药性，无法服用";
				if (type == 13)
				{
					msg = "已经领悟过该大道书";
				}
				UIPopTip.Inst.Pop(msg);
				return;
			}
			break;
		}
		if (isTuPo)
		{
			if (!Tools.canClickFlag)
			{
				return;
			}
			Tools.instance.playFader("正在翻阅秘籍...");
		}
		inventoryNext = Next;
		if (Tools.instance.isEquip(itemID))
		{
			if (inventoryNext != null)
			{
				inventoryNext.Invoke();
			}
			return;
		}
		foreach (JSONObject item in jsonData.instance.ItemJsonData[string.Concat(itemID)]["seid"].list)
		{
			if (CanNextSeid(item.I))
			{
				realizeSeid(item.I);
				continue;
			}
			break;
		}
		JSONObject jSONObject = jsonData.instance.ItemJsonData[string.Concat(itemID)]["seid"];
		if (jSONObject.HasItem(1) || jSONObject.HasItem(2) || type == 13)
		{
			int addday = Tools.CalcLingWuTime(itemID);
			player.AddTime(addday);
		}
		if (type == 3)
		{
			AddWuDao(getItemJson()["StuTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["linwu"].n, Tools.JsonListToList(jsonData.instance.ItemJsonData[string.Concat(itemID)]["wuDao"]));
		}
		if (type == 4)
		{
			AddWuDao(getItemJson()["StuTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["gongfa"].n, Tools.JsonListToList(jsonData.instance.ItemJsonData[string.Concat(itemID)]["wuDao"]));
		}
		if (type == 13)
		{
			AddWuDao(getItemJson()["StuTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["kanshu"].n, Tools.JsonListToList(jsonData.instance.ItemJsonData[string.Concat(itemID)]["wuDao"]));
		}
		if (jsonData.instance.ItemJsonData[string.Concat(itemID)]["vagueType"].n == 2f)
		{
			return;
		}
		if (inventoryNext != null)
		{
			inventoryNext.Invoke();
		}
		if (type == 5)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num2 = (int)jsonData.instance.ItemJsonData[itemID.ToString()]["DanDu"].n;
			num2 -= avatar.getStaticSkillAddSum(14);
			if (num2 < 0)
			{
				num2 = 0;
			}
			avatar.AddDandu(num2);
		}
		if (type == 5 || type == 13)
		{
			AddNaiYaoXin();
		}
	}

	public void AddNaiYaoXin()
	{
		if (!Tools.instance.getPlayer().NaiYaoXin.HasField(string.Concat(itemID)))
		{
			Tools.instance.getPlayer().NaiYaoXin.AddField(string.Concat(itemID), 0);
		}
		int num = (int)Tools.instance.getPlayer().NaiYaoXin[string.Concat(itemID)].n;
		Tools.instance.getPlayer().NaiYaoXin.SetField(string.Concat(itemID), num + 1);
	}

	public static void AddWuDao(int timeday, float xishu, List<int> xishuList, int listXi = 2)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = (int)(xishu * (float)timeday);
		int num2 = xishuList.Count / listXi;
		if (num2 <= 0)
		{
			return;
		}
		int num3 = 0;
		foreach (int xishu2 in xishuList)
		{
			if (num3 % listXi == 0)
			{
				player.wuDaoMag.addWuDaoEx(xishu2, num / num2);
			}
			num3++;
		}
	}

	public static void AddWuDao(int num, List<int> xishuList, int listXi = 2)
	{
		Avatar player = Tools.instance.getPlayer();
		int num2 = xishuList.Count / listXi;
		if (num2 <= 0)
		{
			return;
		}
		int num3 = 0;
		foreach (int xishu in xishuList)
		{
			if (num3 % listXi == 0)
			{
				player.wuDaoMag.addWuDaoEx(xishu, num / num2);
			}
			num3++;
		}
	}

	public static int getAddWuDaoEx(int timeday, float xishu, List<int> xishuList, int listXi = 2)
	{
		Tools.instance.getPlayer();
		int num = (int)(xishu * (float)timeday);
		int num2 = xishuList.Count / listXi;
		if (num2 > 0)
		{
			using List<int>.Enumerator enumerator = xishuList.GetEnumerator();
			if (enumerator.MoveNext())
			{
				_ = enumerator.Current;
				return num / num2;
			}
		}
		return 0;
	}

	public void realizeSeid(int seid)
	{
		for (int i = 0; i < 500; i++)
		{
			if (i == seid)
			{
				MethodInfo method = GetType().GetMethod("realizeSeid" + seid);
				if (method != null)
				{
					method.Invoke(this, new object[1] { seid });
				}
				break;
			}
		}
	}

	public bool CanNextSeid(int seid)
	{
		Avatar player = Tools.instance.getPlayer();
		if (seid == 16 && !player.TianFuID.HasField(string.Concat(17)))
		{
			return false;
		}
		return true;
	}

	public JSONObject getSeidJson(int seid)
	{
		int num = itemID;
		if (num > jsonData.QingJiaoItemIDSegment)
		{
			num -= jsonData.QingJiaoItemIDSegment;
		}
		return jsonData.instance.ItemsSeidJsonData[seid][num.ToString()];
	}

	public JSONObject getItemJson()
	{
		return jsonData.instance.ItemJsonData[itemID.ToString()];
	}

	public void realizeSeid1(int seid)
	{
		((Avatar)KBEngineApp.app.player()).addHasSkillList((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid2(int seid)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.addHasStaticSkillList((int)getSeidJson(seid)["value1"].n);
		new StaticSkill(Tools.instance.getStaticSkillKeyByID((int)getSeidJson(seid)["value1"].n), 0, 5).Puting(avatar, avatar, 3);
	}

	public void realizeSeid3(int seid)
	{
		((Avatar)KBEngineApp.app.player()).AllMapAddHP((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid4(int seid)
	{
		Avatar obj = (Avatar)KBEngineApp.app.player();
		UIPopTip.Inst.Pop("你的修为提升了" + getSeidJson(seid)["value1"].I, PopTipIconType.上箭头);
		obj.addEXP(getSeidJson(seid)["value1"].I);
	}

	public void realizeSeid5(int seid)
	{
		((Avatar)KBEngineApp.app.player())._shengShi += getSeidJson(seid)["value1"].I;
	}

	public void realizeSeid6(int seid)
	{
		Avatar obj = (Avatar)KBEngineApp.app.player();
		obj._HP_Max += (int)getSeidJson(seid)["value1"].n;
		obj.HP += (int)getSeidJson(seid)["value1"].n;
	}

	public void realizeSeid7(int seid)
	{
		((Avatar)KBEngineApp.app.player()).shouYuan += (uint)getSeidJson(seid)["value1"].n;
	}

	public void realizeSeid8(int seid)
	{
		((Avatar)KBEngineApp.app.player())._xinjin += (int)getSeidJson(seid)["value1"].n;
	}

	public void realizeSeid9(int seid)
	{
		((Avatar)KBEngineApp.app.player()).addZiZhi((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid10(int seid)
	{
		((Avatar)KBEngineApp.app.player()).addWuXin((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid11(int seid)
	{
		((Avatar)KBEngineApp.app.player())._dunSu += (int)getSeidJson(seid)["value1"].n;
	}

	public void realizeSeid12(int seid)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		int num = 0;
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			if (avatar.StreamData.DanYaoBuFFDict.ContainsKey(item.I))
			{
				avatar.StreamData.DanYaoBuFFDict[item.I] += getSeidJson(seid)["value2"][num].I;
			}
			else
			{
				avatar.StreamData.DanYaoBuFFDict.Add(item.I, getSeidJson(seid)["value2"][num].I);
			}
			num++;
		}
	}

	public void realizeSeid13(int seid)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		JSONObject jSONObject = jsonData.instance.LianDanDanFangBiao[((int)getSeidJson(seid)["value1"].n).ToString()];
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 1; i <= 5; i++)
		{
			list.Add((int)jSONObject["value" + i].n);
			list2.Add((int)jSONObject["num" + i].n);
		}
		avatar.addDanFang(jSONObject["ItemID"].I, list, list2);
		LianDanMag.AddWuDaoLianDan(jSONObject["ItemID"].I, 1);
		UIPopTip.Inst.Pop("学会了" + _ItemJsonData.DataDict[jSONObject["ItemID"].I].name + "炼制配方", PopTipIconType.包裹);
	}

	public void realizeSeid14(int seid)
	{
		((Avatar)KBEngineApp.app.player()).statiReduceDandu((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid15(int seid)
	{
		((Avatar)KBEngineApp.app.player()).LingGeng[(int)getSeidJson(seid)["value1"].n] += (int)getSeidJson(seid)["value2"].n;
	}

	public void realizeSeid17(int seid)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			JSONObject jSONObject = jsonData.instance.LianDanDanFangBiao[item.I.ToString()];
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 1; i <= 5; i++)
			{
				list.Add((int)jSONObject["value" + i].n);
				list2.Add((int)jSONObject["num" + i].n);
			}
			avatar.addDanFang(jSONObject["ItemID"].I, list, list2);
			UIPopTip.Inst.Pop("学会了" + _ItemJsonData.DataDict[jSONObject["ItemID"].I].name + "炼制配方", PopTipIconType.包裹);
		}
	}

	public void realizeSeid18(int seid)
	{
		Singleton.ints.TuJIanPlan.open();
	}

	public void realizeSeid19(int seid)
	{
		Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(getSeidJson(seid)["value1"].I, getSeidJson(seid)["value2"].I);
	}

	public void realizeSeid20(int seid)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject seidJson = getSeidJson(seid);
		int num = 0;
		foreach (JSONObject item in seidJson["value1"].list)
		{
			_ = item;
			player.addItem(seidJson["value1"][num].I, seidJson["value2"][num].I, Tools.CreateItemSeid(seidJson["value1"][num].I), ShowText: true);
			num++;
		}
	}

	public void realizeSeid22(int seid)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject seidJson = getSeidJson(seid);
		if (!player.nomelTaskMag.IsNTaskStart(seidJson["value1"].I))
		{
			player.nomelTaskMag.StartNTask(seidJson["value1"].I);
		}
		UIPopTip.Inst.Pop("获得一条新的传闻", PopTipIconType.任务进度);
	}

	public void realizeSeid23(int seid)
	{
		Avatar player = Tools.instance.getPlayer();
		int value = ItemsSeidJsonData23.DataDict[itemID].value1;
		foreach (JToken item in (IEnumerable<JToken>)jsonData.instance.EndlessSeaHaiYuData[value.ToString()][(object)"shuxing"])
		{
			int index = (int)player.EndlessSea["AllIaLand"][(object)((int)item - 1)];
			EndlessSeaMag.AddSeeIsland(EndlessSeaMag.GetRealIndex((int)item, index));
		}
		foreach (KeyValuePair<string, JToken> item2 in jsonData.instance.SeaStaticIsland)
		{
			if ((int)item2.Value[(object)"SeaID"] == value)
			{
				EndlessSeaMag.AddSeeIsland((int)item2.Value[(object)"IsLandIndex"]);
			}
		}
	}

	public void realizeSeid24(int seid)
	{
		Singleton.ints.ShowSeaMapUI();
	}

	public void realizeSeid25(int seid)
	{
		Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(getSeidJson(seid)["value1"].I, getSeidJson(seid)["value2"].I);
	}

	public void realizeSeid26(int seid)
	{
		Tools.instance.getPlayer()._WuDaoDian += getSeidJson(seid)["value1"].I;
	}

	public void realizeSeid27(int seid)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		player.ItemBuffList[seid.ToString()] = (JToken)new JObject();
		JObject val = (JObject)player.ItemBuffList[seid.ToString()];
		val["AIType"] = JToken.op_Implicit(getSeidJson(seid)["value1"].I);
		val["StartTime"] = JToken.op_Implicit(player.worldTimeMag.nowTime);
		val["ContinueTime"] = JToken.op_Implicit(getSeidJson(seid)["value2"].I);
		val["icon"] = JToken.op_Implicit(getSeidJson(seid)["value3"].str);
		val["start"] = JToken.op_Implicit(true);
	}

	public void realizeSeid28(int seid)
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			player.YaoCaiChanDi.Add(item.I);
		}
	}

	public void realizeSeid29(int seid)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.level < getSeidJson(seid)["value1"].I)
		{
			player.AddDandu(getSeidJson(seid)["value2"].I);
		}
	}

	public void realizeSeid30(int seid)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.addHasSkillList((int)getSeidJson(seid)["value1"].n);
		avatar.addHasStaticSkillList((int)getSeidJson(seid)["value2"].n);
		new StaticSkill(Tools.instance.getStaticSkillKeyByID((int)getSeidJson(seid)["value2"].n), 0, 5).Puting(avatar, avatar, 3);
	}

	public void realizeSeid32(int seid)
	{
		int i = getSeidJson(seid)["value1"].I;
		PlayerEx.StudyShuangXiuSkill(i);
		UIPopTip.Inst.Pop("学会了" + ShuangXiuMiShu.DataDict[i].name, PopTipIconType.包裹);
	}

	public void realizeSeid33(int seid)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		PlayerEx.AddSeaTanSuoDu(i, i2);
	}

	public void realizeSeid34(int seid)
	{
		List<int> list = getSeidJson(seid)["value1"].ToList();
		Avatar player = Tools.instance.getPlayer();
		foreach (int item in list)
		{
			player.UnLockCaoYaoData(item);
		}
	}

	public void realizeSeid35(int seid)
	{
		Tools.instance.getPlayer().IsCanSetFace = true;
	}
}
