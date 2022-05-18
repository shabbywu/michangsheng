using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000437 RID: 1079
public class LianDanPageManager : MonoBehaviour
{
	// Token: 0x06001CC8 RID: 7368 RVA: 0x000FD270 File Offset: 0x000FB470
	private void Awake()
	{
		this.RemoveAllBtn.onClick.AddListener(new UnityAction(this.RemoveAll));
		LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
		LianDanSystemManager.inst.putDanLuCallBack();
		LianDanSystemManager.inst.startLianDanBtn.onClick.AddListener(new UnityAction(this.clickLianDan));
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x000FD2D0 File Offset: 0x000FB4D0
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

	// Token: 0x06001CCA RID: 7370 RVA: 0x000FD3A4 File Offset: 0x000FB5A4
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

	// Token: 0x06001CCB RID: 7371 RVA: 0x000FD544 File Offset: 0x000FB744
	public void RemoveAll()
	{
		for (int i = 25; i < 30; i++)
		{
			LianDanSystemManager.inst.inventory.inventory[i] = new item();
			LianDanSystemManager.inst.inventory.inventory[i].itemNum = 0;
			this.putLianDanCellList[i - 25].updateItem();
		}
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x000FD5A8 File Offset: 0x000FB7A8
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

	// Token: 0x06001CCD RID: 7373 RVA: 0x000FD644 File Offset: 0x000FB844
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

	// Token: 0x040018BE RID: 6334
	public List<PutLianDanCell> putLianDanCellList;

	// Token: 0x040018BF RID: 6335
	public bool CanClick = true;

	// Token: 0x040018C0 RID: 6336
	[SerializeField]
	private Button RemoveAllBtn;

	// Token: 0x040018C1 RID: 6337
	[HideInInspector]
	public int LianDanSum;

	// Token: 0x040018C2 RID: 6338
	[SerializeField]
	private Text curNaiJiu;

	// Token: 0x040018C3 RID: 6339
	[SerializeField]
	private Image DanLuImage;
}
