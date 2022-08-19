using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E3 RID: 739
public class LianDanPageManager : MonoBehaviour
{
	// Token: 0x060019AF RID: 6575 RVA: 0x000B766C File Offset: 0x000B586C
	private void Awake()
	{
		this.RemoveAllBtn.onClick.AddListener(new UnityAction(this.RemoveAll));
		LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
		LianDanSystemManager.inst.putDanLuCallBack();
		LianDanSystemManager.inst.startLianDanBtn.onClick.AddListener(new UnityAction(this.clickLianDan));
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x000B76CC File Offset: 0x000B58CC
	public void clickLianDan()
	{
		int i = jsonData.instance.ItemJsonData[this.putLianDanCellList[5].Item.itemID.ToString()]["quality"].I;
		int num = 0;
		int num2 = LianDanSystemManager.inst.lianDanResultManager.MaxCaoYao[i - 1];
		for (int j = 0; j < this.putLianDanCellList.Count - 1; j++)
		{
			if (this.putLianDanCellList[j].Item.itemID != -1)
			{
				num += this.putLianDanCellList[j].Item.itemNum;
			}
		}
		if (num > num2)
		{
			UIPopTip.Inst.Pop(string.Format("该品阶丹炉最大药材数{0}个", num2), PopTipIconType.叹号);
			return;
		}
		this.selectLianDanSum();
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000B77A0 File Offset: 0x000B59A0
	public void selectLianDanSum()
	{
		int num = 10000000;
		Tools.instance.getPlayer();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < this.putLianDanCellList.Count - 1; i++)
		{
			if (this.putLianDanCellList[i].Item.itemID != -1)
			{
				int itemID = this.putLianDanCellList[i].Item.itemID;
				ITEM_INFO item_INFO = Tools.instance.getPlayer().itemList.values.Find((ITEM_INFO item) => item.itemId == itemID);
				if (item_INFO != null)
				{
					if (dictionary.ContainsKey(itemID))
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int itemID2 = itemID;
						dictionary2[itemID2] += this.putLianDanCellList[i].Item.itemNum;
					}
					else
					{
						dictionary[itemID] = this.putLianDanCellList[i].Item.itemNum;
					}
					int num2 = (int)(item_INFO.itemCount / (uint)dictionary[itemID]);
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
		}
		if (num > 0 && num < 10000000)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), LianDanSystemManager.inst.sumSelectTransform);
			SumSelectManager sumSelectManager = gameObject.GetComponent<SumSelectManager>();
			sumSelectManager.showSelect("炼制", -1, (float)num, delegate
			{
				this.LianDanSum = (int)sumSelectManager.itemSum;
				if (this.LianDanSum <= 0)
				{
					return;
				}
				LianDanSystemManager.inst.lianDanResultManager.lianDanJieSuan();
			}, null, SumSelectManager.SpecialType.炼丹);
		}
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x000B7940 File Offset: 0x000B5B40
	public void RemoveAll()
	{
		for (int i = 25; i < 30; i++)
		{
			LianDanSystemManager.inst.inventory.inventory[i] = new item();
			LianDanSystemManager.inst.inventory.inventory[i].itemNum = 0;
			this.putLianDanCellList[i - 25].updateItem();
		}
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x000B79A4 File Offset: 0x000B5BA4
	public void updateDanLu()
	{
		item item = LianDanSystemManager.inst.inventory.inventory[30];
		if (item.itemID != -1)
		{
			int i = jsonData.instance.ItemJsonData[item.itemID.ToString()]["quality"].I;
			this.DanLuImage.sprite = (Sprite)LianDanSystemManager.inst.DanLuSprite[i];
			this.curNaiJiu.text = item.Seid["NaiJiu"].I.ToString();
		}
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x000B7A40 File Offset: 0x000B5C40
	public string getLianDanName()
	{
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		for (int i = 0; i < list.Count; i++)
		{
			int num = 0;
			for (int j = 0; j < 5; j++)
			{
				int num2 = 0;
				int num3 = 0;
				if (this.putLianDanCellList[j].Item.itemID > 0)
				{
					num2 = this.putLianDanCellList[j].Item.itemID;
					num3 = this.putLianDanCellList[j].Item.itemNum;
				}
				if (list[i]["Type"][j].I == num2 && list[i]["Num"][j].I == num3)
				{
					num++;
				}
			}
			if (num == 5)
			{
				return Tools.Code64(Tools.setColorByID(jsonData.instance.ItemJsonData[list[i]["ID"].I.ToString()]["name"].str, list[i]["ID"].I));
			}
		}
		return Tools.setColorByID("???", 1);
	}

	// Token: 0x040014D3 RID: 5331
	public List<PutLianDanCell> putLianDanCellList;

	// Token: 0x040014D4 RID: 5332
	public bool CanClick = true;

	// Token: 0x040014D5 RID: 5333
	[SerializeField]
	private Button RemoveAllBtn;

	// Token: 0x040014D6 RID: 5334
	[HideInInspector]
	public int LianDanSum;

	// Token: 0x040014D7 RID: 5335
	[SerializeField]
	private Text curNaiJiu;

	// Token: 0x040014D8 RID: 5336
	[SerializeField]
	private Image DanLuImage;
}
