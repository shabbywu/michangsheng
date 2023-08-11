using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LianDanPageManager : MonoBehaviour
{
	public List<PutLianDanCell> putLianDanCellList;

	public bool CanClick = true;

	[SerializeField]
	private Button RemoveAllBtn;

	[HideInInspector]
	public int LianDanSum;

	[SerializeField]
	private Text curNaiJiu;

	[SerializeField]
	private Image DanLuImage;

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		((UnityEvent)RemoveAllBtn.onClick).AddListener(new UnityAction(RemoveAll));
		LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
		LianDanSystemManager.inst.putDanLuCallBack();
		((UnityEvent)LianDanSystemManager.inst.startLianDanBtn.onClick).AddListener(new UnityAction(clickLianDan));
	}

	public void clickLianDan()
	{
		int i = jsonData.instance.ItemJsonData[putLianDanCellList[5].Item.itemID.ToString()]["quality"].I;
		int num = 0;
		int num2 = LianDanSystemManager.inst.lianDanResultManager.MaxCaoYao[i - 1];
		for (int j = 0; j < putLianDanCellList.Count - 1; j++)
		{
			if (putLianDanCellList[j].Item.itemID != -1)
			{
				num += putLianDanCellList[j].Item.itemNum;
			}
		}
		if (num > num2)
		{
			UIPopTip.Inst.Pop($"该品阶丹炉最大药材数{num2}个");
		}
		else
		{
			selectLianDanSum();
		}
	}

	public void selectLianDanSum()
	{
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		int num = 10000000;
		Tools.instance.getPlayer();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < putLianDanCellList.Count - 1; i++)
		{
			if (putLianDanCellList[i].Item.itemID == -1)
			{
				continue;
			}
			int itemID = putLianDanCellList[i].Item.itemID;
			ITEM_INFO iTEM_INFO = Tools.instance.getPlayer().itemList.values.Find((ITEM_INFO item) => item.itemId == itemID);
			if (iTEM_INFO != null)
			{
				if (dictionary.ContainsKey(itemID))
				{
					dictionary[itemID] += putLianDanCellList[i].Item.itemNum;
				}
				else
				{
					dictionary[itemID] = putLianDanCellList[i].Item.itemNum;
				}
				int num2 = (int)iTEM_INFO.itemCount / dictionary[itemID];
				if (num2 < num)
				{
					num = num2;
				}
			}
		}
		if (num <= 0 || num >= 10000000)
		{
			return;
		}
		GameObject val = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), LianDanSystemManager.inst.sumSelectTransform);
		SumSelectManager sumSelectManager = val.GetComponent<SumSelectManager>();
		sumSelectManager.showSelect("炼制", -1, num, (UnityAction)delegate
		{
			LianDanSum = (int)sumSelectManager.itemSum;
			if (LianDanSum > 0)
			{
				LianDanSystemManager.inst.lianDanResultManager.lianDanJieSuan();
			}
		}, null, SumSelectManager.SpecialType.炼丹);
	}

	public void RemoveAll()
	{
		for (int i = 25; i < 30; i++)
		{
			LianDanSystemManager.inst.inventory.inventory[i] = new item();
			LianDanSystemManager.inst.inventory.inventory[i].itemNum = 0;
			putLianDanCellList[i - 25].updateItem();
		}
	}

	public void updateDanLu()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		item item = LianDanSystemManager.inst.inventory.inventory[30];
		if (item.itemID != -1)
		{
			int i = jsonData.instance.ItemJsonData[item.itemID.ToString()]["quality"].I;
			DanLuImage.sprite = (Sprite)LianDanSystemManager.inst.DanLuSprite[i];
			curNaiJiu.text = item.Seid["NaiJiu"].I.ToString();
		}
	}

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
				if (putLianDanCellList[j].Item.itemID > 0)
				{
					num2 = putLianDanCellList[j].Item.itemID;
					num3 = putLianDanCellList[j].Item.itemNum;
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
}
