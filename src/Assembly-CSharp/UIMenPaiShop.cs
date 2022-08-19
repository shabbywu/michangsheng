using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000348 RID: 840
public class UIMenPaiShop : MonoBehaviour, IESCClose
{
	// Token: 0x06001CB7 RID: 7351 RVA: 0x000CD746 File Offset: 0x000CB946
	private void Awake()
	{
		UIMenPaiShop.Inst = this;
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x000CD74E File Offset: 0x000CB94E
	public void Show()
	{
		this.ScaleObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x000CD768 File Offset: 0x000CB968
	public void RefreshUI()
	{
		UIMenPaiShop.<>c__DisplayClass10_0 CS$<>8__locals1 = new UIMenPaiShop.<>c__DisplayClass10_0();
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.sceneName = SceneEx.NowSceneName;
		List<NomelShopJsonData> list = NomelShopJsonData.DataList.FindAll((NomelShopJsonData d) => string.Format("S{0}", d.threeScene) == CS$<>8__locals1.sceneName);
		if (list.Count == 0)
		{
			Debug.LogError("UIMenPaiShop刷新UI异常，此场景没有商品信息");
			return;
		}
		this.ShopTitle.text = list[0].Title;
		int num = 0;
		int levelType = PlayerEx.Player.getLevelType();
		ItemDatebase component = jsonData.instance.GetComponent<ItemDatebase>();
		for (int i = 0; i < 3; i++)
		{
			UIMenPaiShop.<>c__DisplayClass10_1 CS$<>8__locals2 = new UIMenPaiShop.<>c__DisplayClass10_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.shop = list[i];
			this.ShopName[i].text = CS$<>8__locals2.shop.ChildTitle;
			List<jiaoHuanShopGoods> list2 = jiaoHuanShopGoods.DataList.FindAll((jiaoHuanShopGoods d) => d.ShopID == CS$<>8__locals2.shop.ExShopID);
			list2.Sort();
			this.ShopRT[i].DestoryAllChild();
			using (List<jiaoHuanShopGoods>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UIMenPaiShop.<>c__DisplayClass10_2 CS$<>8__locals3 = new UIMenPaiShop.<>c__DisplayClass10_2();
					CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
					CS$<>8__locals3.good = enumerator.Current;
					if (num == 0)
					{
						num = CS$<>8__locals3.good.EXGoodsID;
					}
					_ItemJsonData item = _ItemJsonData.DataDict[CS$<>8__locals3.good.GoodsID];
					if (CS$<>8__locals3.CS$<>8__locals2.shop.SType != 1 || levelType >= item.quality || (item.type != 3 && item.type != 4))
					{
						UIMenPaiShopItem component2 = Object.Instantiate<GameObject>(this.UIMenPaiShopItemPrefab, this.ShopRT[i]).GetComponent<UIMenPaiShopItem>();
						int price = item.price / CS$<>8__locals3.good.percent;
						if (item.price % CS$<>8__locals3.good.percent > 0)
						{
							int price2 = price;
							price = price2 + 1;
						}
						component2.PriceText.text = price.ToString();
						component2.PriceIcon.sprite = component.items[CS$<>8__locals3.good.EXGoodsID].itemIconSprite;
						component2.IconShow.SetItem(CS$<>8__locals3.good.GoodsID);
						component2.IconShow.Count = 1;
						UIIconShow iconShow = component2.IconShow;
						UnityAction <>9__3;
						UnityAction<int> <>9__4;
						iconShow.OnClick = (UnityAction<PointerEventData>)Delegate.Combine(iconShow.OnClick, new UnityAction<PointerEventData>(delegate(PointerEventData p)
						{
							int num2 = PlayerEx.Player.getItemNum(CS$<>8__locals3.good.EXGoodsID) / price;
							num2 = Mathf.Min(num2, item.maxNum);
							if (num2 == 0)
							{
								UIPopTip.Inst.Pop(_ItemJsonData.DataDict[CS$<>8__locals3.good.EXGoodsID].name + "不足", PopTipIconType.叹号);
								return;
							}
							if (num2 == 1)
							{
								string text = "是否兑换" + item.name + " x1";
								UnityAction onOK;
								if ((onOK = <>9__3) == null)
								{
									onOK = (<>9__3 = delegate()
									{
										if (PlayerEx.Player.getItemNum(CS$<>8__locals3.good.EXGoodsID) >= price)
										{
											PlayerEx.Player.removeItem(CS$<>8__locals3.good.EXGoodsID, price);
											PlayerEx.Player.addItem(CS$<>8__locals3.good.GoodsID, 1, Tools.CreateItemSeid(CS$<>8__locals3.good.GoodsID), false);
											CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.RefreshUI();
											UIPopTip.Inst.Pop("兑换了" + _ItemJsonData.DataDict[CS$<>8__locals3.good.GoodsID].name + "x1", PopTipIconType.包裹);
											return;
										}
										UIPopTip.Inst.Pop(_ItemJsonData.DataDict[CS$<>8__locals3.good.EXGoodsID].name + "不足", PopTipIconType.叹号);
									});
								}
								USelectBox.Show(text, onOK, null);
								return;
							}
							string desc = "兑换数量 x{num}";
							int minNum = 1;
							int maxNum = num2;
							UnityAction<int> ok;
							if ((ok = <>9__4) == null)
							{
								ok = (<>9__4 = delegate(int num)
								{
									if (PlayerEx.Player.getItemNum(CS$<>8__locals3.good.EXGoodsID) >= num * price)
									{
										PlayerEx.Player.removeItem(CS$<>8__locals3.good.EXGoodsID, num * price);
										PlayerEx.Player.addItem(CS$<>8__locals3.good.GoodsID, num, Tools.CreateItemSeid(CS$<>8__locals3.good.GoodsID), false);
										CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.RefreshUI();
										UIPopTip.Inst.Pop(string.Format("兑换了{0}x{1}", _ItemJsonData.DataDict[CS$<>8__locals3.good.GoodsID].name, num), PopTipIconType.包裹);
										return;
									}
									UIPopTip.Inst.Pop(_ItemJsonData.DataDict[CS$<>8__locals3.good.EXGoodsID].name + "不足", PopTipIconType.叹号);
								});
							}
							USelectNum.Show(desc, minNum, maxNum, ok, null);
						}));
					}
				}
			}
		}
		this.MoneyIcon.sprite = component.items[num].itemIconSprite;
		this.MoneyText.text = PlayerEx.Player.getItemNum(num).ToString();
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x000CDAA4 File Offset: 0x000CBCA4
	public void Close()
	{
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x000CDABD File Offset: 0x000CBCBD
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x0400173B RID: 5947
	public static UIMenPaiShop Inst;

	// Token: 0x0400173C RID: 5948
	public GameObject UIMenPaiShopItemPrefab;

	// Token: 0x0400173D RID: 5949
	public GameObject ScaleObj;

	// Token: 0x0400173E RID: 5950
	public Text ShopTitle;

	// Token: 0x0400173F RID: 5951
	public List<Text> ShopName;

	// Token: 0x04001740 RID: 5952
	public List<RectTransform> ShopRT;

	// Token: 0x04001741 RID: 5953
	public Image MoneyIcon;

	// Token: 0x04001742 RID: 5954
	public Text MoneyText;
}
