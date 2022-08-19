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
	// Token: 0x02000709 RID: 1801
	public class NewPaiMaiJoin : MonoBehaviour, IESCClose
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x0018B6A5 File Offset: 0x001898A5
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x0018B6AD File Offset: 0x001898AD
		public bool CanClick { get; private set; }

		// Token: 0x060039C3 RID: 14787 RVA: 0x0018B6B6 File Offset: 0x001898B6
		private void Awake()
		{
			NewPaiMaiJoin.Inst = this;
			this.CanClick = true;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x0018B6D0 File Offset: 0x001898D0
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

		// Token: 0x060039C5 RID: 14789 RVA: 0x0018B784 File Offset: 0x00189984
		private void InitPlayerData()
		{
			this.PlayerName.SetText(Tools.GetPlayerName());
			this.PlayerTitle.SetText(Tools.GetPlayerTitle());
			this.PlayerBag.Init(this.NpcId, true);
			this.PlayerBag.UpdateMoney();
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x0018B7C4 File Offset: 0x001899C4
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

		// Token: 0x060039C7 RID: 14791 RVA: 0x0018B8F4 File Offset: 0x00189AF4
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

		// Token: 0x060039C8 RID: 14792 RVA: 0x0018BA94 File Offset: 0x00189C94
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

		// Token: 0x060039C9 RID: 14793 RVA: 0x0018BC30 File Offset: 0x00189E30
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

		// Token: 0x060039CA RID: 14794 RVA: 0x0018BCB8 File Offset: 0x00189EB8
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

		// Token: 0x060039CB RID: 14795 RVA: 0x0018BD87 File Offset: 0x00189F87
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			ESCCloseManager.Inst.UnRegisterClose(this);
			NewPaiMaiJoin.Inst = null;
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0018BDA6 File Offset: 0x00189FA6
		public void CanCel()
		{
			if (this.CanClick)
			{
				this.Close();
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x0018BDB8 File Offset: 0x00189FB8
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

		// Token: 0x060039CE RID: 14798 RVA: 0x0005C928 File Offset: 0x0005AB28
		private void Close()
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x0018C018 File Offset: 0x0018A218
		public bool TryEscClose()
		{
			this.CanCel();
			return true;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0018C024 File Offset: 0x0018A224
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

		// Token: 0x040031D8 RID: 12760
		[SerializeField]
		private Text PlayerName;

		// Token: 0x040031D9 RID: 12761
		[SerializeField]
		private Text PlayerTitle;

		// Token: 0x040031DA RID: 12762
		public PaiMaiBag PlayerBag;

		// Token: 0x040031DB RID: 12763
		public BagItemSelect Select;

		// Token: 0x040031DC RID: 12764
		[SerializeField]
		private NpcUI Npc;

		// Token: 0x040031DD RID: 12765
		[SerializeField]
		private Image SlotBg;

		// Token: 0x040031DE RID: 12766
		[SerializeField]
		private GameObject ShopCell;

		// Token: 0x040031DF RID: 12767
		[SerializeField]
		private Transform ShopPanel;

		// Token: 0x040031E0 RID: 12768
		[SerializeField]
		private List<Sprite> SlotSprites;

		// Token: 0x040031E1 RID: 12769
		[SerializeField]
		private GameObject PaiMaiSlotCell;

		// Token: 0x040031E2 RID: 12770
		[SerializeField]
		private List<PaiMaiSlot> PaiMaiSlotList;

		// Token: 0x040031E3 RID: 12771
		[SerializeField]
		private Transform PaiMaiSlotParent;

		// Token: 0x040031E4 RID: 12772
		[SerializeField]
		private PaiMaiSay Say;

		// Token: 0x040031E5 RID: 12773
		[SerializeField]
		private List<BaseItem> BaseShopList;

		// Token: 0x040031E7 RID: 12775
		public int NpcId;

		// Token: 0x040031E8 RID: 12776
		public int PaiMaiId;

		// Token: 0x040031E9 RID: 12777
		public int PutMax;

		// Token: 0x040031EA RID: 12778
		public static NewPaiMaiJoin Inst;
	}
}
