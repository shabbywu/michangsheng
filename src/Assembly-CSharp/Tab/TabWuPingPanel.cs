using System;
using System.Collections.Generic;
using Bag;
using GUIPackage;
using KBEngine;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000705 RID: 1797
	[Serializable]
	public class TabWuPingPanel : ITabPanelBase
	{
		// Token: 0x060039A5 RID: 14757 RVA: 0x0018A7D4 File Offset: 0x001889D4
		public TabWuPingPanel(GameObject gameObject)
		{
			this.HasHp = true;
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0018A824 File Offset: 0x00188A24
		private void Init()
		{
			Transform transform = base.Get("EquipList", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				EquipSlot component = transform.GetChild(i).GetComponent<EquipSlot>();
				this.EquipDict.Add(i + 1, component);
			}
			if (Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
			{
				this.EquipDict[3].gameObject.SetActive(true);
				return;
			}
			this.EquipDict[3].gameObject.SetActive(false);
			this.EquipDict.Remove(3);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x0018A8C4 File Offset: 0x00188AC4
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			SingletonMono<TabUIMag>.Instance.TabBag.OpenBag(BagType.背包);
			this.LoadEquipData();
			SingletonMono<TabUIMag>.Instance.ShowBaseData();
			this._go.SetActive(true);
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x0018A914 File Offset: 0x00188B14
		public void LoadEquipData()
		{
			Dictionary<int, BaseItem> curEquipDict = this.FangAnData.GetCurEquipDict();
			foreach (int key in this.EquipDict.Keys)
			{
				if (curEquipDict.ContainsKey(key))
				{
					this.EquipDict[key].SetSlotData(curEquipDict[key]);
				}
				else
				{
					this.EquipDict[key].SetNull();
				}
			}
			this.ResetEquipSeid();
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0018A9AC File Offset: 0x00188BAC
		public void AddEquip(int index, EquipItem equipItem)
		{
			if (this.EquipDict[index].IsNull())
			{
				this.FangAnData.GetCurEquipDict()[index] = equipItem.Clone();
				this.EquipDict[index].SetSlotData(equipItem.Clone());
				DragMag.Inst.DragSlot.SetNull();
				Tools.instance.getPlayer().removeItem(equipItem.Uid);
			}
			else
			{
				DragMag.Inst.DragSlot.SetSlotData(this.EquipDict[index].Item.Clone());
				Tools.instance.getPlayer().AddEquip(this.EquipDict[index].Item.Id, this.EquipDict[index].Item.Uid, this.EquipDict[index].Item.Seid);
				this.FangAnData.GetCurEquipDict()[index] = equipItem;
				this.EquipDict[index].SetSlotData(equipItem);
				Tools.instance.getPlayer().removeItem(equipItem.Uid);
			}
			this.ResetEquipSeid();
			SingletonMono<TabUIMag>.Instance.TabBag.UpDateSlotList();
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x0018AAE8 File Offset: 0x00188CE8
		public void AddEquip(EquipItem equipItem)
		{
			foreach (int num in this.EquipDict.Keys)
			{
				if (equipItem.CanPutSlotType == this.EquipDict[num].AcceptType)
				{
					this.AddEquip(num, equipItem);
					break;
				}
			}
			DragMag.Inst.Clear();
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x0018AB68 File Offset: 0x00188D68
		public void RmoveEquip(EquipSlotType slotType)
		{
			BaseItem item = this.EquipDict[(int)slotType].Item;
			Tools.instance.getPlayer().AddEquip(item.Id, item.Uid, item.Seid);
			SingletonMono<TabUIMag>.Instance.TabBag.UpDateSlotList();
			this.EquipDict[(int)slotType].SetNull();
			this.FangAnData.GetCurEquipDict().Remove((int)slotType);
			this.ResetEquipSeid();
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x0018ABE4 File Offset: 0x00188DE4
		public void ExEquip(EquipSlotType slotType1, EquipSlotType slotType2)
		{
			if (this.EquipDict[(int)slotType2].IsNull())
			{
				this.EquipDict[(int)slotType2].SetSlotData(this.EquipDict[(int)slotType1].Item.Clone());
				this.EquipDict[(int)slotType1].SetNull();
				this.FangAnData.GetCurEquipDict().Remove((int)slotType1);
			}
			else
			{
				BaseItem slotData = this.EquipDict[(int)slotType1].Item.Clone();
				BaseItem slotData2 = this.EquipDict[(int)slotType2].Item.Clone();
				this.EquipDict[(int)slotType1].SetSlotData(slotData2);
				this.EquipDict[(int)slotType2].SetSlotData(slotData);
				this.FangAnData.GetCurEquipDict()[(int)slotType1] = this.EquipDict[(int)slotType1].Item.Clone();
			}
			this.FangAnData.GetCurEquipDict()[(int)slotType2] = this.EquipDict[(int)slotType2].Item.Clone();
			this.ResetEquipSeid();
			Tools.instance.ResetEquipSeid();
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x0018AD04 File Offset: 0x00188F04
		public void ResetEquipSeid()
		{
			Avatar player = Tools.instance.getPlayer();
			player.EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();
			Dictionary<int, BaseItem> curEquipDict = this.FangAnData.GetCurEquipDict();
			foreach (int key in curEquipDict.Keys)
			{
				if (curEquipDict[key].Seid == null && curEquipDict[key].Seid.HasField("ItemSeids"))
				{
					using (List<JSONObject>.Enumerator enumerator2 = curEquipDict[key].Seid["ItemSeids"].list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							JSONObject itemAddSeid = enumerator2.Current;
							new Equips(curEquipDict[key].Id, 0, 5)
							{
								ItemAddSeid = itemAddSeid
							}.Puting(player, player, 2);
						}
						continue;
					}
				}
				new Equips(curEquipDict[key].Id, 0, 5).Puting(player, player, 2);
			}
			player.nowConfigEquipItem = this.FangAnData.CurEquipIndex;
			player.equipItemList.values = player.StreamData.FangAnData.CurEquipDictToOldList();
		}

		// Token: 0x040031BF RID: 12735
		private bool _isInit;

		// Token: 0x040031C0 RID: 12736
		public Dictionary<int, EquipSlot> EquipDict = new Dictionary<int, EquipSlot>();

		// Token: 0x040031C1 RID: 12737
		private FangAnData FangAnData = Tools.instance.getPlayer().StreamData.FangAnData;
	}
}
