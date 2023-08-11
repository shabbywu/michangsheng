using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai;

public class NewPaiMaiJoin : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Text PlayerName;

	[SerializeField]
	private Text PlayerTitle;

	public PaiMaiBag PlayerBag;

	public BagItemSelect Select;

	[SerializeField]
	private NpcUI Npc;

	[SerializeField]
	private Image SlotBg;

	[SerializeField]
	private GameObject ShopCell;

	[SerializeField]
	private Transform ShopPanel;

	[SerializeField]
	private List<Sprite> SlotSprites;

	[SerializeField]
	private GameObject PaiMaiSlotCell;

	[SerializeField]
	private List<PaiMaiSlot> PaiMaiSlotList;

	[SerializeField]
	private Transform PaiMaiSlotParent;

	[SerializeField]
	private PaiMaiSay Say;

	[SerializeField]
	private List<BaseItem> BaseShopList;

	public int NpcId;

	public int PaiMaiId;

	public int PutMax;

	public static NewPaiMaiJoin Inst;

	public bool CanClick { get; private set; }

	private void Awake()
	{
		Inst = this;
		CanClick = true;
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void Init(int paiId, int npcId)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		InitPlayerData();
		Npc.Init(npcId);
		PaiMaiId = paiId;
		NpcId = npcId;
		PutMax = PaiMaiBiao.DataDict[PaiMaiId].jimainum;
		RefreshShop();
		InitSlotList();
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector2.op_Implicit(new Vector2(0.821f, 0.821f));
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		PanelMamager.CanOpenOrClose = false;
	}

	private void InitPlayerData()
	{
		PlayerName.SetText(Tools.GetPlayerName());
		PlayerTitle.SetText(Tools.GetPlayerTitle());
		PlayerBag.Init(NpcId, isPlayer: true);
		PlayerBag.UpdateMoney();
	}

	private void RefreshShop()
	{
		BaseShopList = new List<BaseItem>();
		Avatar player = Tools.instance.getPlayer();
		if (player.StreamData.PaiMaiDataMag.PaiMaiDict.Count == 0)
		{
			player.StreamData.PaiMaiDataMag.AuToUpDate();
		}
		else if (player.StreamData.PaiMaiDataMag.PaiMaiDict[PaiMaiId].IsJoined)
		{
			player.StreamData.PaiMaiDataMag.UpdateById(PaiMaiId);
		}
		foreach (int shop in player.StreamData.PaiMaiDataMag.PaiMaiDict[PaiMaiId].ShopList)
		{
			PaiMaiSlot component = ShopCell.Inst(ShopPanel).GetComponent<PaiMaiSlot>();
			component.SetSlotData(BaseItem.Create(shop, 1, Tools.getUUID(), Tools.CreateItemSeid(shop)));
			((Component)component).gameObject.SetActive(true);
			BaseShopList.Add(component.Item.Clone());
		}
	}

	public void PutItem(PaiMaiSlot dragSlot, PaiMaiSlot toSlot = null)
	{
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
			return;
		}
		if (dragSlot.Item.Count < 1)
		{
			UIPopTip.Inst.Pop("此物品数量小于0寄卖");
			return;
		}
		PaiMaiSlot nullPaiMaiSlot = GetNullPaiMaiSlot(dragSlot.Item);
		if ((Object)(object)nullPaiMaiSlot == (Object)null)
		{
			UIPopTip.Inst.Pop("寄卖物品已达上限");
			return;
		}
		if (nullPaiMaiSlot.IsNull())
		{
			if ((Object)(object)toSlot != (Object)null)
			{
				if (!toSlot.IsNull())
				{
					toSlot = nullPaiMaiSlot;
				}
			}
			else
			{
				toSlot = nullPaiMaiSlot;
			}
		}
		else
		{
			toSlot = nullPaiMaiSlot;
		}
		int setCount = 0;
		if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
		{
			setCount = 5;
			if (dragSlot.Item.Count < 5)
			{
				setCount = dragSlot.Item.Count;
			}
		}
		if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
		{
			setCount = dragSlot.Item.Count;
		}
		UnityAction val = (UnityAction)delegate
		{
			int num = 0;
			num = ((setCount <= 0) ? Select.CurNum : setCount);
			if (toSlot.IsNull())
			{
				toSlot.SetSlotData(dragSlot.Item.Clone());
				toSlot.Item.Count = num;
			}
			else
			{
				toSlot.Item.Count += num;
			}
			toSlot.UpdateUI();
			PlayerBag.RemoveTempItem(dragSlot.Item.Uid, num);
			dragSlot.Item.Count -= num;
			if (dragSlot.Item.Count <= 0)
			{
				dragSlot.SetNull();
			}
			else
			{
				dragSlot.UpdateUI();
			}
		};
		if (setCount > 0)
		{
			val.Invoke();
		}
		else
		{
			Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, val);
		}
	}

	public void BackItem(PaiMaiSlot dragSlot, PaiMaiSlot toSlot = null)
	{
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
			return;
		}
		if (dragSlot.Item.Count < 1)
		{
			UIPopTip.Inst.Pop("此物品数量小于0寄卖");
			return;
		}
		SlotBase nullBagSlot = PlayerBag.GetNullBagSlot(dragSlot.Item.Uid);
		if ((Object)(object)nullBagSlot != (Object)null && ((!nullBagSlot.IsNull() && nullBagSlot.Item.Id == dragSlot.Item.Id) || (Object)(object)toSlot == (Object)null))
		{
			toSlot = (PaiMaiSlot)nullBagSlot;
		}
		int setCount = 0;
		if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
		{
			setCount = 5;
			if (dragSlot.Item.Count < 5)
			{
				setCount = dragSlot.Item.Count;
			}
		}
		if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
		{
			setCount = dragSlot.Item.Count;
		}
		UnityAction val = (UnityAction)delegate
		{
			int num = 0;
			num = ((setCount <= 0) ? Select.CurNum : setCount);
			PlayerBag.AddTempItem(dragSlot.Item, num);
			if ((Object)(object)toSlot != (Object)null)
			{
				if (toSlot.IsNull())
				{
					toSlot.SetSlotData(dragSlot.Item.Clone());
					toSlot.Item.Count = num;
				}
				else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
				{
					toSlot.Item.Count += num;
				}
				toSlot.UpdateUI();
			}
			dragSlot.Item.Count -= num;
			if (dragSlot.Item.Count <= 0)
			{
				dragSlot.SetNull();
			}
			else
			{
				dragSlot.UpdateUI();
			}
			PlayerBag.UpdateItem();
		};
		if (setCount > 0)
		{
			val.Invoke();
		}
		else
		{
			Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, val);
		}
	}

	public PaiMaiSlot GetNullPaiMaiSlot(BaseItem baseItem)
	{
		PaiMaiSlot paiMaiSlot = null;
		foreach (PaiMaiSlot paiMaiSlot2 in PaiMaiSlotList)
		{
			if (paiMaiSlot2.IsNull())
			{
				if ((Object)(object)paiMaiSlot == (Object)null)
				{
					paiMaiSlot = paiMaiSlot2;
				}
			}
			else if (paiMaiSlot2.Item.Id == baseItem.Id && baseItem.MaxNum > 1)
			{
				return paiMaiSlot2;
			}
		}
		return paiMaiSlot;
	}

	private void InitSlotList()
	{
		PaiMaiSlotList = new List<PaiMaiSlot>();
		if (PutMax == 0)
		{
			SlotBg.sprite = SlotSprites[0];
			((Graphic)SlotBg).SetNativeSize();
			PaiMaiSlot component = PaiMaiSlotCell.Inst(PaiMaiSlotParent).GetComponent<PaiMaiSlot>();
			component.SetNull();
			PaiMaiSlotList.Add(component);
			return;
		}
		SlotBg.sprite = SlotSprites[PutMax - 1];
		((Graphic)SlotBg).SetNativeSize();
		for (int i = 0; i < PutMax; i++)
		{
			PaiMaiSlot component2 = PaiMaiSlotCell.Inst(PaiMaiSlotParent).GetComponent<PaiMaiSlot>();
			component2.SetNull();
			PaiMaiSlotList.Add(component2);
		}
	}

	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		PanelMamager.CanOpenOrClose = true;
		ESCCloseManager.Inst.UnRegisterClose(this);
		Inst = null;
	}

	public void CanCel()
	{
		if (CanClick)
		{
			Close();
		}
	}

	public void Join()
	{
		if (!IsCanJoin() || !CanClick)
		{
			return;
		}
		CanClick = false;
		Say.SayWord("入场费" + PaiMaiBiao.DataDict[PaiMaiId].RuChangFei + "灵石，我收下啦。");
		PaiMaiShopData paiMaiShopData = new PaiMaiShopData
		{
			id = PaiMaiId,
			ShopList = new List<PaiMaiShop>()
		};
		foreach (PaiMaiSlot paiMaiSlot in PaiMaiSlotList)
		{
			if (!paiMaiSlot.IsNull())
			{
				BaseItem item = paiMaiSlot.Item;
				PaiMaiShop paiMaiShop = new PaiMaiShop
				{
					ShopId = item.Id,
					Count = item.Count,
					Price = item.GetPrice(),
					Seid = item.Seid.Copy(),
					IsPlayer = true
				};
				if (paiMaiShop.Seid == null || paiMaiShop.Seid.Count == 0)
				{
					paiMaiShop.Seid = Tools.CreateItemSeid(paiMaiShop.ShopId);
				}
				paiMaiShop.Init();
				paiMaiShopData.ShopList.Add(paiMaiShop);
				Tools.instance.RemoveItem(item.Uid, item.Count);
			}
		}
		foreach (BaseItem baseShop in BaseShopList)
		{
			PaiMaiShop paiMaiShop2 = new PaiMaiShop
			{
				ShopId = baseShop.Id,
				Count = 1,
				Price = baseShop.GetPrice(),
				Seid = baseShop.Seid
			};
			if (paiMaiShop2.Seid == null || paiMaiShop2.Seid.Count == 0)
			{
				paiMaiShop2.Seid = Tools.CreateItemSeid(paiMaiShop2.ShopId);
			}
			paiMaiShop2.Init();
			paiMaiShopData.ShopList.Add(paiMaiShop2);
		}
		paiMaiShopData.ShopList.Sort();
		BindData.Bind("PaiMaiData", paiMaiShopData);
		DisposableExtensions.AddTo<IDisposable>(ObservableExtensions.Subscribe<long>(Observable.Timer(TimeSpan.FromSeconds(1.0)), (Action<long>)delegate
		{
			Tools.instance.getPlayer().StreamData.PaiMaiDataMag.PaiMaiDict[PaiMaiId].IsJoined = true;
			Tools.instance.loadOtherScenes("PaiMai");
			Close();
		}), (Component)(object)this);
	}

	private void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public bool TryEscClose()
	{
		CanCel();
		return true;
	}

	private bool IsCanJoin()
	{
		PaiMaiBiao paiMaiBiao = PaiMaiBiao.DataDict[PaiMaiId];
		PaiMaiData paiMaiData = Tools.instance.getPlayer().StreamData.PaiMaiDataMag.PaiMaiDict[PaiMaiId];
		int num = (int)Tools.instance.getPlayer().money;
		int ruChangFei = paiMaiBiao.RuChangFei;
		int price = paiMaiBiao.Price;
		if (num < ruChangFei)
		{
			Say.SayWord($"我们这入场需要{ruChangFei}灵石。");
			return false;
		}
		if (PaiMaiSlotList.Count > 0)
		{
			int num2 = 0;
			foreach (PaiMaiSlot paiMaiSlot in PaiMaiSlotList)
			{
				if (!paiMaiSlot.IsNull())
				{
					num2 += paiMaiSlot.Item.GetPrice() * paiMaiSlot.Item.Count;
				}
			}
			if (num2 > 0 && num2 < price)
			{
				Say.SayWord("物品的总价值，没有达到我们拍卖行的最低价值需求");
				return false;
			}
		}
		if (paiMaiBiao.IsBuShuaXin == 1)
		{
			return true;
		}
		int value = paiMaiData.NextUpdateTime.Year - DateTime.Parse(paiMaiBiao.StarTime).Year;
		DateTime dateTime = DateTime.Parse(paiMaiBiao.StarTime).AddYears(value);
		DateTime dateTime2 = DateTime.Parse(paiMaiBiao.EndTime).AddYears(value);
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		if (nowTime >= dateTime && nowTime <= dateTime2)
		{
			return true;
		}
		Say.SayWord($"下一届拍卖会时间：{dateTime.Year}年{dateTime.Month}月{dateTime.Day}日至{dateTime2.Year}年{dateTime2.Month}月{dateTime2.Day}日");
		return false;
	}
}
