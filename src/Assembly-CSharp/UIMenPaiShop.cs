using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMenPaiShop : MonoBehaviour, IESCClose
{
	public static UIMenPaiShop Inst;

	public GameObject UIMenPaiShopItemPrefab;

	public GameObject ScaleObj;

	public Text ShopTitle;

	public List<Text> ShopName;

	public List<RectTransform> ShopRT;

	public Image MoneyIcon;

	public Text MoneyText;

	private void Awake()
	{
		Inst = this;
	}

	public void Show()
	{
		ScaleObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void RefreshUI()
	{
		string sceneName = SceneEx.NowSceneName;
		List<NomelShopJsonData> list = NomelShopJsonData.DataList.FindAll((NomelShopJsonData d) => $"S{d.threeScene}" == sceneName);
		if (list.Count == 0)
		{
			Debug.LogError((object)"UIMenPaiShop刷新UI异常，此场景没有商品信息");
			return;
		}
		ShopTitle.text = list[0].Title;
		int num2 = 0;
		int levelType = PlayerEx.Player.getLevelType();
		ItemDatebase component = ((Component)jsonData.instance).GetComponent<ItemDatebase>();
		for (int i = 0; i < 3; i++)
		{
			NomelShopJsonData shop = list[i];
			ShopName[i].text = shop.ChildTitle;
			List<jiaoHuanShopGoods> list2 = jiaoHuanShopGoods.DataList.FindAll((jiaoHuanShopGoods d) => d.ShopID == shop.ExShopID);
			list2.Sort();
			((Transform)(object)ShopRT[i]).DestoryAllChild();
			foreach (jiaoHuanShopGoods good in list2)
			{
				if (num2 == 0)
				{
					num2 = good.EXGoodsID;
				}
				_ItemJsonData item = _ItemJsonData.DataDict[good.GoodsID];
				if (shop.SType == 1 && levelType < item.quality && (item.type == 3 || item.type == 4))
				{
					continue;
				}
				UIMenPaiShopItem component2 = Object.Instantiate<GameObject>(UIMenPaiShopItemPrefab, (Transform)(object)ShopRT[i]).GetComponent<UIMenPaiShopItem>();
				int price = item.price / good.percent;
				if (item.price % good.percent > 0)
				{
					price++;
				}
				component2.PriceText.text = price.ToString();
				component2.PriceIcon.sprite = component.items[good.EXGoodsID].itemIconSprite;
				component2.IconShow.SetItem(good.GoodsID);
				component2.IconShow.Count = 1;
				UIIconShow iconShow = component2.IconShow;
				UnityAction val = default(UnityAction);
				iconShow.OnClick = (UnityAction<PointerEventData>)(object)Delegate.Combine((Delegate?)(object)iconShow.OnClick, (Delegate?)(object)(UnityAction<PointerEventData>)delegate
				{
					//IL_009c: Unknown result type (might be due to invalid IL or missing references)
					//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
					//IL_00a3: Expected O, but got Unknown
					//IL_00a8: Expected O, but got Unknown
					int num3 = PlayerEx.Player.getItemNum(good.EXGoodsID) / price;
					num3 = Mathf.Min(num3, item.maxNum);
					switch (num3)
					{
					case 0:
						UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good.EXGoodsID].name + "不足");
						break;
					case 1:
					{
						string text = "是否兑换" + item.name + " x1";
						UnityAction obj = val;
						if (obj == null)
						{
							UnityAction val2 = delegate
							{
								if (PlayerEx.Player.getItemNum(good.EXGoodsID) >= price)
								{
									PlayerEx.Player.removeItem(good.EXGoodsID, price);
									PlayerEx.Player.addItem(good.GoodsID, 1, Tools.CreateItemSeid(good.GoodsID));
									RefreshUI();
									UIPopTip.Inst.Pop("兑换了" + _ItemJsonData.DataDict[good.GoodsID].name + "x1", PopTipIconType.包裹);
								}
								else
								{
									UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good.EXGoodsID].name + "不足");
								}
							};
							UnityAction val3 = val2;
							val = val2;
							obj = val3;
						}
						USelectBox.Show(text, obj);
						break;
					}
					default:
						USelectNum.Show("兑换数量 x{num}", 1, num3, delegate(int num)
						{
							if (PlayerEx.Player.getItemNum(good.EXGoodsID) >= num * price)
							{
								PlayerEx.Player.removeItem(good.EXGoodsID, num * price);
								PlayerEx.Player.addItem(good.GoodsID, num, Tools.CreateItemSeid(good.GoodsID));
								RefreshUI();
								UIPopTip.Inst.Pop($"兑换了{_ItemJsonData.DataDict[good.GoodsID].name}x{num}", PopTipIconType.包裹);
							}
							else
							{
								UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good.EXGoodsID].name + "不足");
							}
						});
						break;
					}
				});
			}
		}
		MoneyIcon.sprite = component.items[num2].itemIconSprite;
		MoneyText.text = PlayerEx.Player.getItemNum(num2).ToString();
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
