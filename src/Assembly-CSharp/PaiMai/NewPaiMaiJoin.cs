using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A57 RID: 2647
	public class NewPaiMaiJoin : MonoBehaviour, IESCClose
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x00030E38 File Offset: 0x0002F038
		// (set) Token: 0x0600444A RID: 17482 RVA: 0x00030E40 File Offset: 0x0002F040
		public bool CanClick { get; private set; }

		// Token: 0x0600444B RID: 17483 RVA: 0x00030E49 File Offset: 0x0002F049
		private void Awake()
		{
			NewPaiMaiJoin.Inst = this;
			this.CanClick = true;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x001D2D88 File Offset: 0x001D0F88
		public void Init(int paiId, int npcId)
		{
			this.InitPlayerData();
			this.Npc.Init(npcId);
			this.PaiMaiId = paiId;
			this.NpcId = npcId;
			this.PutMax = PaiMaiBiao.DataDict[this.PaiMaiId].jimainum;
			this.RefreshShop();
			this.InitSlotList();
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = new Vector2(0.821f, 0.821f);
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			PanelMamager.CanOpenOrClose = false;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00030E63 File Offset: 0x0002F063
		private void InitPlayerData()
		{
			this.PlayerName.SetText(Tools.GetPlayerName());
			this.PlayerTitle.SetText(Tools.GetPlayerTitle());
			this.PlayerBag.Init(this.NpcId, true);
			this.PlayerBag.UpdateMoney();
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x001D2E3C File Offset: 0x001D103C
		private void RefreshShop()
		{
			this.BaseShopList = new List<BaseItem>();
			Avatar player = Tools.instance.getPlayer();
			if (player.StreamData.PaiMaiDataMag.PaiMaiDict.Count == 0)
			{
				player.StreamData.PaiMaiDataMag.AuToUpDate();
			}
			else if (player.StreamData.PaiMaiDataMag.PaiMaiDict[this.PaiMaiId].IsJoined)
			{
				player.StreamData.PaiMaiDataMag.UpdateById(this.PaiMaiId);
			}
			foreach (int num in player.StreamData.PaiMaiDataMag.PaiMaiDict[this.PaiMaiId].ShopList)
			{
				PaiMaiSlot component = this.ShopCell.Inst(this.ShopPanel).GetComponent<PaiMaiSlot>();
				component.SetSlotData(BaseItem.Create(num, 1, Tools.getUUID(), Tools.CreateItemSeid(num)));
				component.gameObject.SetActive(true);
				this.BaseShopList.Add(component.Item.Clone());
			}
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x001D2F6C File Offset: 0x001D116C
		public void PutItem(PaiMaiSlot dragSlot, PaiMaiSlot toSlot = null)
		{
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
				return;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0寄卖", PopTipIconType.叹号);
				return;
			}
			PaiMaiSlot nullPaiMaiSlot = this.GetNullPaiMaiSlot(dragSlot.Item);
			if (nullPaiMaiSlot == null)
			{
				UIPopTip.Inst.Pop("寄卖物品已达上限", PopTipIconType.叹号);
				return;
			}
			if (nullPaiMaiSlot.IsNull())
			{
				if (toSlot != null)
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
			if (Input.GetKey(304) || Input.GetKey(303))
			{
				setCount = 5;
				if (dragSlot.Item.Count < 5)
				{
					setCount = dragSlot.Item.Count;
				}
			}
			if (Input.GetKey(306) || Input.GetKey(305))
			{
				setCount = dragSlot.Item.Count;
			}
			UnityAction unityAction = delegate()
			{
				int num;
				if (setCount > 0)
				{
					num = setCount;
				}
				else
				{
					num = this.Select.CurNum;
				}
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
				this.PlayerBag.RemoveTempItem(dragSlot.Item.Uid, num);
				dragSlot.Item.Count -= num;
				if (dragSlot.Item.Count <= 0)
				{
					dragSlot.SetNull();
					return;
				}
				dragSlot.UpdateUI();
			};
			if (setCount > 0)
			{
				unityAction.Invoke();
				return;
			}
			this.Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, unityAction, null);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x001D310C File Offset: 0x001D130C
		public void BackItem(PaiMaiSlot dragSlot, PaiMaiSlot toSlot = null)
		{
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
				return;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0寄卖", PopTipIconType.叹号);
				return;
			}
			SlotBase nullBagSlot = this.PlayerBag.GetNullBagSlot(dragSlot.Item.Uid);
			if (nullBagSlot != null && ((!nullBagSlot.IsNull() && nullBagSlot.Item.Id == dragSlot.Item.Id) || toSlot == null))
			{
				toSlot = (PaiMaiSlot)nullBagSlot;
			}
			int setCount = 0;
			if (Input.GetKey(304) || Input.GetKey(303))
			{
				setCount = 5;
				if (dragSlot.Item.Count < 5)
				{
					setCount = dragSlot.Item.Count;
				}
			}
			if (Input.GetKey(306) || Input.GetKey(305))
			{
				setCount = dragSlot.Item.Count;
			}
			UnityAction unityAction = delegate()
			{
				int num;
				if (setCount > 0)
				{
					num = setCount;
				}
				else
				{
					num = this.Select.CurNum;
				}
				this.PlayerBag.AddTempItem(dragSlot.Item, num);
				if (toSlot != null)
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
				this.PlayerBag.UpdateItem(false);
			};
			if (setCount > 0)
			{
				unityAction.Invoke();
				return;
			}
			this.Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, unityAction, null);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x001D32A8 File Offset: 0x001D14A8
		public PaiMaiSlot GetNullPaiMaiSlot(BaseItem baseItem)
		{
			PaiMaiSlot paiMaiSlot = null;
			foreach (PaiMaiSlot paiMaiSlot2 in this.PaiMaiSlotList)
			{
				if (paiMaiSlot2.IsNull())
				{
					if (paiMaiSlot == null)
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

		// Token: 0x06004452 RID: 17490 RVA: 0x001D3330 File Offset: 0x001D1530
		private void InitSlotList()
		{
			this.PaiMaiSlotList = new List<PaiMaiSlot>();
			if (this.PutMax == 0)
			{
				this.SlotBg.sprite = this.SlotSprites[0];
				this.SlotBg.SetNativeSize();
				PaiMaiSlot component = this.PaiMaiSlotCell.Inst(this.PaiMaiSlotParent).GetComponent<PaiMaiSlot>();
				component.SetNull();
				this.PaiMaiSlotList.Add(component);
				return;
			}
			this.SlotBg.sprite = this.SlotSprites[this.PutMax - 1];
			this.SlotBg.SetNativeSize();
			for (int i = 0; i < this.PutMax; i++)
			{
				PaiMaiSlot component2 = this.PaiMaiSlotCell.Inst(this.PaiMaiSlotParent).GetComponent<PaiMaiSlot>();
				component2.SetNull();
				this.PaiMaiSlotList.Add(component2);
			}
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x00030EA2 File Offset: 0x0002F0A2
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			ESCCloseManager.Inst.UnRegisterClose(this);
			NewPaiMaiJoin.Inst = null;
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00030EC1 File Offset: 0x0002F0C1
		public void CanCel()
		{
			if (this.CanClick)
			{
				this.Close();
			}
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x001D3400 File Offset: 0x001D1600
		public void Join()
		{
			if (!this.IsCanJoin())
			{
				return;
			}
			if (!this.CanClick)
			{
				return;
			}
			this.CanClick = false;
			this.Say.SayWord("入场费" + PaiMaiBiao.DataDict[this.PaiMaiId].RuChangFei + "灵石，我收下啦。", null, 1f);
			PaiMaiShopData paiMaiShopData = new PaiMaiShopData
			{
				id = this.PaiMaiId,
				ShopList = new List<PaiMaiShop>()
			};
			foreach (PaiMaiSlot paiMaiSlot in this.PaiMaiSlotList)
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
			foreach (BaseItem baseItem in this.BaseShopList)
			{
				PaiMaiShop paiMaiShop2 = new PaiMaiShop
				{
					ShopId = baseItem.Id,
					Count = 1,
					Price = baseItem.GetPrice(),
					Seid = baseItem.Seid
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
			DisposableExtensions.AddTo<IDisposable>(ObservableExtensions.Subscribe<long>(Observable.Timer(TimeSpan.FromSeconds(1.0)), delegate(long _)
			{
				Tools.instance.getPlayer().StreamData.PaiMaiDataMag.PaiMaiDict[this.PaiMaiId].IsJoined = true;
				Tools.instance.loadOtherScenes("PaiMai");
				this.Close();
			}), this);
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x000111B3 File Offset: 0x0000F3B3
		private void Close()
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x00030ED1 File Offset: 0x0002F0D1
		public bool TryEscClose()
		{
			this.CanCel();
			return true;
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x001D3660 File Offset: 0x001D1860
		private bool IsCanJoin()
		{
			PaiMaiBiao paiMaiBiao = PaiMaiBiao.DataDict[this.PaiMaiId];
			PaiMaiData paiMaiData = Tools.instance.getPlayer().StreamData.PaiMaiDataMag.PaiMaiDict[this.PaiMaiId];
			int num = (int)Tools.instance.getPlayer().money;
			int ruChangFei = paiMaiBiao.RuChangFei;
			int price = paiMaiBiao.Price;
			if (num < ruChangFei)
			{
				this.Say.SayWord(string.Format("我们这入场需要{0}灵石。", ruChangFei), null, 1f);
				return false;
			}
			if (this.PaiMaiSlotList.Count > 0)
			{
				int num2 = 0;
				foreach (PaiMaiSlot paiMaiSlot in this.PaiMaiSlotList)
				{
					if (!paiMaiSlot.IsNull())
					{
						num2 += paiMaiSlot.Item.GetPrice() * paiMaiSlot.Item.Count;
					}
				}
				if (num2 > 0 && num2 < price)
				{
					this.Say.SayWord("物品的总价值，没有达到我们拍卖行的最低价值需求", null, 1f);
					return false;
				}
			}
			if (paiMaiBiao.IsBuShuaXin == 1)
			{
				return true;
			}
			int value = paiMaiData.NextUpdateTime.Year - DateTime.Parse(paiMaiBiao.StarTime).Year;
			DateTime t = DateTime.Parse(paiMaiBiao.StarTime).AddYears(value);
			DateTime t2 = DateTime.Parse(paiMaiBiao.EndTime).AddYears(value);
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			if (nowTime >= t && nowTime <= t2)
			{
				return true;
			}
			this.Say.SayWord(string.Format("下一届拍卖会时间：{0}年{1}月{2}日至{3}年{4}月{5}日", new object[]
			{
				t.Year,
				t.Month,
				t.Day,
				t2.Year,
				t2.Month,
				t2.Day
			}), null, 1f);
			return false;
		}

		// Token: 0x04003C57 RID: 15447
		[SerializeField]
		private Text PlayerName;

		// Token: 0x04003C58 RID: 15448
		[SerializeField]
		private Text PlayerTitle;

		// Token: 0x04003C59 RID: 15449
		public PaiMaiBag PlayerBag;

		// Token: 0x04003C5A RID: 15450
		public BagItemSelect Select;

		// Token: 0x04003C5B RID: 15451
		[SerializeField]
		private NpcUI Npc;

		// Token: 0x04003C5C RID: 15452
		[SerializeField]
		private Image SlotBg;

		// Token: 0x04003C5D RID: 15453
		[SerializeField]
		private GameObject ShopCell;

		// Token: 0x04003C5E RID: 15454
		[SerializeField]
		private Transform ShopPanel;

		// Token: 0x04003C5F RID: 15455
		[SerializeField]
		private List<Sprite> SlotSprites;

		// Token: 0x04003C60 RID: 15456
		[SerializeField]
		private GameObject PaiMaiSlotCell;

		// Token: 0x04003C61 RID: 15457
		[SerializeField]
		private List<PaiMaiSlot> PaiMaiSlotList;

		// Token: 0x04003C62 RID: 15458
		[SerializeField]
		private Transform PaiMaiSlotParent;

		// Token: 0x04003C63 RID: 15459
		[SerializeField]
		private PaiMaiSay Say;

		// Token: 0x04003C64 RID: 15460
		[SerializeField]
		private List<BaseItem> BaseShopList;

		// Token: 0x04003C66 RID: 15462
		public int NpcId;

		// Token: 0x04003C67 RID: 15463
		public int PaiMaiId;

		// Token: 0x04003C68 RID: 15464
		public int PutMax;

		// Token: 0x04003C69 RID: 15465
		public static NewPaiMaiJoin Inst;
	}
}
