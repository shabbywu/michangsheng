using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDuiHuanShop : MonoBehaviour, IESCClose
{
	public static UIDuiHuanShop Inst;

	public GameObject UIMenPaiShopItemPrefab;

	public GameObject ScaleObj;

	public RectTransform ShopRT;

	public Image MoneyIcon;

	public Text MoneyText;

	private int ShopID;

	private void Awake()
	{
		Inst = this;
	}

	public void Show(int shopID)
	{
		ShopID = shopID;
		ScaleObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void RefreshUI()
	{
		_ = SceneEx.NowSceneName;
		List<jiaoHuanShopGoods> list = jiaoHuanShopGoods.DataList.FindAll((jiaoHuanShopGoods good) => good.ShopID == ShopID);
		if (list.Count == 0)
		{
			Debug.LogError((object)$"UIDuiHuanShop刷新UI异常，兑换商店{ShopID}没有商品信息");
			return;
		}
		int num2 = 0;
		PlayerEx.Player.getLevelType();
		ItemDatebase component = ((Component)jsonData.instance).GetComponent<ItemDatebase>();
		((Transform)(object)ShopRT).DestoryAllChild();
		foreach (jiaoHuanShopGoods good2 in list)
		{
			if (num2 == 0)
			{
				num2 = good2.EXGoodsID;
			}
			_ItemJsonData item = _ItemJsonData.DataDict[good2.GoodsID];
			UIMenPaiShopItem component2 = Object.Instantiate<GameObject>(UIMenPaiShopItemPrefab, (Transform)(object)ShopRT).GetComponent<UIMenPaiShopItem>();
			int price = item.price;
			if (good2.Money > 0)
			{
				price = good2.Money;
			}
			component2.PriceText.text = price.ToString();
			component2.PriceIcon.sprite = component.items[good2.EXGoodsID].itemIconSprite;
			component2.IconShow.SetItem(good2.GoodsID);
			component2.IconShow.Count = 1;
			UIIconShow iconShow = component2.IconShow;
			iconShow.OnClick = (UnityAction<PointerEventData>)(object)Delegate.Combine((Delegate?)(object)iconShow.OnClick, (Delegate?)(object)(UnityAction<PointerEventData>)delegate
			{
				//IL_00db: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e6: Expected O, but got Unknown
				int playerHas = 0;
				if (good2.EXGoodsID == 10035)
				{
					playerHas = (int)PlayerEx.Player.money;
				}
				else
				{
					playerHas = PlayerEx.Player.getItemNum(good2.EXGoodsID);
				}
				int num3 = playerHas / price;
				num3 = Mathf.Min(num3, item.maxNum);
				switch (num3)
				{
				case 0:
					UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good2.EXGoodsID].name + "不足");
					break;
				case 1:
					USelectBox.Show("是否兑换" + item.name + " x1", (UnityAction)delegate
					{
						if (playerHas >= price)
						{
							if (good2.EXGoodsID == 10035)
							{
								PlayerEx.Player.AddMoney(-price);
							}
							else
							{
								PlayerEx.Player.removeItem(good2.EXGoodsID, price);
							}
							PlayerEx.Player.addItem(good2.GoodsID, 1, Tools.CreateItemSeid(good2.GoodsID));
							RefreshUI();
							UIPopTip.Inst.Pop($"兑换了{_ItemJsonData.DataDict[good2.GoodsID].name}x{1}", PopTipIconType.包裹);
						}
						else
						{
							UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good2.EXGoodsID].name + "不足");
						}
					});
					break;
				default:
					USelectNum.Show("兑换数量 x{num}", 1, num3, delegate(int num)
					{
						if (playerHas >= num * price)
						{
							if (good2.EXGoodsID == 10035)
							{
								PlayerEx.Player.AddMoney(-(num * price));
							}
							else
							{
								PlayerEx.Player.removeItem(good2.EXGoodsID, num * price);
							}
							PlayerEx.Player.addItem(good2.GoodsID, num, Tools.CreateItemSeid(good2.GoodsID));
							RefreshUI();
							UIPopTip.Inst.Pop($"兑换了{_ItemJsonData.DataDict[good2.GoodsID].name}x{num}", PopTipIconType.包裹);
						}
						else
						{
							UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good2.EXGoodsID].name + "不足");
						}
					});
					break;
				}
			});
		}
		MoneyIcon.sprite = component.items[num2].itemIconSprite;
		if (num2 == 10035)
		{
			MoneyText.text = PlayerEx.Player.money.ToString();
		}
		else
		{
			MoneyText.text = PlayerEx.Player.getItemNum(num2).ToString();
		}
	}

	public void Close()
	{
		ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
