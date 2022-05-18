using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000954 RID: 2388
	[RequireComponent(typeof(ItemContainer))]
	public class Hotbar : GUIBehaviour
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x0002BF46 File Offset: 0x0002A146
		public Slot SelectedSlot
		{
			get
			{
				return this.m_SelectedSlot;
			}
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x001B2C18 File Offset: 0x001B0E18
		private void Awake()
		{
			this.m_HotbarContainer = base.GetComponent<ItemContainer>();
			this.m_HotbarContainer.Slot_Refreshed.AddListener(new Action<Slot>(this.On_Slot_Refreshed));
			this.m_HotbarContainer.Slot_PointerUp += this.On_Slot_PointerUp;
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x0002BF4E File Offset: 0x0002A14E
		private IEnumerator Start()
		{
			base.Player.DestroyEquippedItem.SetTryer(new TryerDelegate(this.Try_DestroyEquippedItem));
			base.Player.Sleep.AddStopListener(delegate
			{
				this.TrySelectSlot(0);
			});
			this.m_HotbarSlots = this.m_HotbarContainer.Slots;
			this.m_Frame = GUIUtils.CreateImageUnder("Frame", base.GetComponent<RectTransform>(), Vector2.zero, Vector2.zero);
			this.m_Frame.sprite = this.m_FrameSprite;
			this.m_Frame.color = this.m_FrameColor;
			this.m_Frame.enabled = false;
			yield return null;
			this.TrySelectSlot(this.m_FirstSelected);
			yield break;
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x001B2C64 File Offset: 0x001B0E64
		private bool Try_DestroyEquippedItem()
		{
			SavableItem item = base.Player.EquippedItem.Get();
			if (item && this.m_HotbarContainer.HasItem(item))
			{
				base.Player.ChangeEquippedItem.Try(null, true);
			}
			return this.m_HotbarContainer.TryRemoveItem(item);
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x001B2CB8 File Offset: 0x001B0EB8
		private void On_Slot_PointerUp(PointerEventData data, Slot displayer)
		{
			if (displayer != this.m_SelectedSlot && data.pointerCurrentRaycast.gameObject == displayer.gameObject)
			{
				this.TrySelectSlot(this.m_HotbarSlots.IndexOf(displayer));
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x0002BF5D File Offset: 0x0002A15D
		private void On_Slot_Refreshed(Slot slot)
		{
			if (slot == this.m_SelectedSlot)
			{
				this.TrySelectSlot(this.m_HotbarSlots.IndexOf(this.m_SelectedSlot));
			}
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x001B2D04 File Offset: 0x001B0F04
		private void Update()
		{
			int num;
			if (this.m_SelectByDigits && !base.Player.SelectBuildable.Active && Input.anyKeyDown && int.TryParse(Input.inputString, out num))
			{
				this.TrySelectSlot(num - 1);
			}
			if (this.m_EnableScrolling && !base.Player.SelectBuildable.Active && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				float num2 = base.Player.ScrollValue.Get();
				this.m_CurScrollValue = Mathf.Clamp(this.m_CurScrollValue + num2, -this.m_ScrollThreeshold, this.m_ScrollThreeshold);
				if (Mathf.Abs(this.m_CurScrollValue - this.m_ScrollThreeshold * Mathf.Sign(num2)) < Mathf.Epsilon)
				{
					this.m_CurScrollValue = 0f;
					this.m_LastIndex = (int)Mathf.Repeat((float)(this.m_LastIndex + ((num2 >= 0f) ? 1 : -1)), (float)this.m_HotbarSlots.Count);
					this.TrySelectSlot(this.m_LastIndex);
				}
			}
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0002BF84 File Offset: 0x0002A184
		public bool IsWeapon(ItemData item)
		{
			return item.Category == "Gun" || item.Category == "Melee Weapon" || item.Category == "Throwable";
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x0002BFBC File Offset: 0x0002A1BC
		public bool IsConsume(ItemData item)
		{
			return item.Category == "Consumable";
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x001B2E0C File Offset: 0x001B100C
		private void TrySelectSlot(int index)
		{
			index = Mathf.Clamp(index, 0, this.m_HotbarSlots.Count - 1);
			Slot slot = this.m_HotbarSlots[index];
			base.Player.ChangeEquippedItem.Try(slot.CurrentItem, false);
			if (this.m_SelectedSlot)
			{
				this.m_SelectedSlot.SetScale(Vector3.one, 0.2f);
			}
			if (slot && slot.HasItem && this.IsWeapon(slot.ItemHolder.CurrentItem.ItemData))
			{
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				if (avatar != null)
				{
					avatar.equipItemRequest(slot.ItemHolder.uuid);
				}
			}
			if (slot && !slot.HasItem && this.m_SelectedSlot && this.m_SelectedSlot.HasItem && this.IsWeapon(this.m_SelectedSlot.ItemHolder.CurrentItem.ItemData))
			{
				Avatar avatar2 = (Avatar)KBEngineApp.app.player();
				if (avatar2 != null)
				{
					avatar2.UnEquipItemRequest(this.m_SelectedSlot.ItemHolder.uuid);
				}
			}
			if (slot && slot.HasItem && this.IsConsume(slot.ItemHolder.CurrentItem.ItemData))
			{
				UI_MainUI.inst.btn_use.gameObject.SetActive(true);
			}
			else
			{
				UI_MainUI.inst.btn_use.gameObject.SetActive(false);
			}
			this.m_SelectedSlot = slot;
			this.m_SelectedSlot.SetScale(Vector3.one * this.m_SelectedSlotScale, 0.1f);
			this.m_Frame.enabled = true;
			RectTransform component = this.m_Frame.GetComponent<RectTransform>();
			component.SetParent(this.m_SelectedSlot.transform);
			component.localPosition = Vector2.zero;
			component.sizeDelta = this.m_SelectedSlot.GetComponent<RectTransform>().sizeDelta;
			component.localScale = Vector3.one;
		}

		// Token: 0x04003741 RID: 14145
		[SerializeField]
		[Range(0f, 100f)]
		private int m_FirstSelected;

		// Token: 0x04003742 RID: 14146
		[Header("Navigation")]
		[SerializeField]
		private bool m_EnableScrolling = true;

		// Token: 0x04003743 RID: 14147
		[SerializeField]
		[HideSwitch("m_EnableScrolling", true, 20f)]
		[Clamp(0f, 10f)]
		private float m_ScrollThreeshold = 0.3f;

		// Token: 0x04003744 RID: 14148
		[SerializeField]
		private bool m_SelectByDigits = true;

		// Token: 0x04003745 RID: 14149
		[Header("Selection Graphics")]
		[SerializeField]
		private float m_SelectedSlotScale = 1f;

		// Token: 0x04003746 RID: 14150
		[SerializeField]
		private Color m_FrameColor = Color.cyan;

		// Token: 0x04003747 RID: 14151
		[SerializeField]
		private Sprite m_FrameSprite;

		// Token: 0x04003748 RID: 14152
		private ItemContainer m_HotbarContainer;

		// Token: 0x04003749 RID: 14153
		private List<Slot> m_HotbarSlots;

		// Token: 0x0400374A RID: 14154
		private Slot m_SelectedSlot;

		// Token: 0x0400374B RID: 14155
		private int m_LastIndex;

		// Token: 0x0400374C RID: 14156
		private float m_CurScrollValue;

		// Token: 0x0400374D RID: 14157
		private Image m_Frame;
	}
}
