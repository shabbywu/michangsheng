using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004BB RID: 1211
public class UIDuiHuanShop : MonoBehaviour, IESCClose
{
	// Token: 0x06002002 RID: 8194 RVA: 0x0001A4E9 File Offset: 0x000186E9
	private void Awake()
	{
		UIDuiHuanShop.Inst = this;
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x0001A4F1 File Offset: 0x000186F1
	public void Show(int shopID)
	{
		this.ShopID = shopID;
		this.ScaleObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x00111DF8 File Offset: 0x0010FFF8
	public void RefreshUI()
	{
		string nowSceneName = SceneEx.NowSceneName;
		List<jiaoHuanShopGoods> list = jiaoHuanShopGoods.DataList.FindAll((jiaoHuanShopGoods good) => good.ShopID == this.ShopID);
		if (list.Count == 0)
		{
			Debug.LogError(string.Format("UIDuiHuanShop刷新UI异常，兑换商店{0}没有商品信息", this.ShopID));
			return;
		}
		int num = 0;
		PlayerEx.Player.getLevelType();
		ItemDatebase component = jsonData.instance.GetComponent<ItemDatebase>();
		this.ShopRT.DestoryAllChild();
		using (List<jiaoHuanShopGoods>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				UIDuiHuanShop.<>c__DisplayClass9_0 CS$<>8__locals1 = new UIDuiHuanShop.<>c__DisplayClass9_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.good = enumerator.Current;
				if (num == 0)
				{
					num = CS$<>8__locals1.good.EXGoodsID;
				}
				_ItemJsonData item = _ItemJsonData.DataDict[CS$<>8__locals1.good.GoodsID];
				UIMenPaiShopItem component2 = Object.Instantiate<GameObject>(this.UIMenPaiShopItemPrefab, this.ShopRT).GetComponent<UIMenPaiShopItem>();
				int price = item.price;
				if (CS$<>8__locals1.good.Money > 0)
				{
					price = CS$<>8__locals1.good.Money;
				}
				component2.PriceText.text = price.ToString();
				component2.PriceIcon.sprite = component.items[CS$<>8__locals1.good.EXGoodsID].itemIconSprite;
				component2.IconShow.SetItem(CS$<>8__locals1.good.GoodsID);
				component2.IconShow.Count = 1;
				UIIconShow iconShow = component2.IconShow;
				iconShow.OnClick = (UnityAction<PointerEventData>)Delegate.Combine(iconShow.OnClick, new UnityAction<PointerEventData>(delegate(PointerEventData p)
				{
					int playerHas = 0;
					if (CS$<>8__locals1.good.EXGoodsID == 10035)
					{
						playerHas = (int)PlayerEx.Player.money;
					}
					else
					{
						playerHas = PlayerEx.Player.getItemNum(CS$<>8__locals1.good.EXGoodsID);
					}
					int num2 = playerHas / price;
					num2 = Mathf.Min(num2, item.maxNum);
					if (num2 == 0)
					{
						UIPopTip.Inst.Pop(_ItemJsonData.DataDict[CS$<>8__locals1.good.EXGoodsID].name + "不足", PopTipIconType.叹号);
						return;
					}
					if (num2 == 1)
					{
						USelectBox.Show("是否兑换" + item.name + " x1", delegate
						{
							if (playerHas >= price)
							{
								if (CS$<>8__locals1.good.EXGoodsID == 10035)
								{
									PlayerEx.Player.AddMoney(-price);
								}
								else
								{
									PlayerEx.Player.removeItem(CS$<>8__locals1.good.EXGoodsID, price);
								}
								PlayerEx.Player.addItem(CS$<>8__locals1.good.GoodsID, 1, Tools.CreateItemSeid(CS$<>8__locals1.good.GoodsID), false);
								CS$<>8__locals1.<>4__this.RefreshUI();
								UIPopTip.Inst.Pop(string.Format("兑换了{0}x{1}", _ItemJsonData.DataDict[CS$<>8__locals1.good.GoodsID].name, 1), PopTipIconType.包裹);
								return;
							}
							UIPopTip.Inst.Pop(_ItemJsonData.DataDict[CS$<>8__locals1.good.EXGoodsID].name + "不足", PopTipIconType.叹号);
						}, null);
						return;
					}
					USelectNum.Show("兑换数量 x{num}", 1, num2, delegate(int num)
					{
						if (playerHas >= num * price)
						{
							if (CS$<>8__locals1.good.EXGoodsID == 10035)
							{
								PlayerEx.Player.AddMoney(-(num * price));
							}
							else
							{
								PlayerEx.Player.removeItem(CS$<>8__locals1.good.EXGoodsID, num * price);
							}
							PlayerEx.Player.addItem(CS$<>8__locals1.good.GoodsID, num, Tools.CreateItemSeid(CS$<>8__locals1.good.GoodsID), false);
							CS$<>8__locals1.<>4__this.RefreshUI();
							UIPopTip.Inst.Pop(string.Format("兑换了{0}x{1}", _ItemJsonData.DataDict[CS$<>8__locals1.good.GoodsID].name, num), PopTipIconType.包裹);
							return;
						}
						UIPopTip.Inst.Pop(_ItemJsonData.DataDict[CS$<>8__locals1.good.EXGoodsID].name + "不足", PopTipIconType.叹号);
					}, null);
				}));
			}
		}
		this.MoneyIcon.sprite = component.items[num].itemIconSprite;
		if (num == 10035)
		{
			this.MoneyText.text = PlayerEx.Player.money.ToString();
			return;
		}
		this.MoneyText.text = PlayerEx.Player.getItemNum(num).ToString();
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x0001A511 File Offset: 0x00018711
	public void Close()
	{
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x0001A52A File Offset: 0x0001872A
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001B75 RID: 7029
	public static UIDuiHuanShop Inst;

	// Token: 0x04001B76 RID: 7030
	public GameObject UIMenPaiShopItemPrefab;

	// Token: 0x04001B77 RID: 7031
	public GameObject ScaleObj;

	// Token: 0x04001B78 RID: 7032
	public RectTransform ShopRT;

	// Token: 0x04001B79 RID: 7033
	public Image MoneyIcon;

	// Token: 0x04001B7A RID: 7034
	public Text MoneyText;

	// Token: 0x04001B7B RID: 7035
	private int ShopID;
}
