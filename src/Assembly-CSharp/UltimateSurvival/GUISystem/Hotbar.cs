using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000654 RID: 1620
	[RequireComponent(typeof(ItemContainer))]
	public class Hotbar : GUIBehaviour
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x00169B77 File Offset: 0x00167D77
		public Slot SelectedSlot
		{
			get
			{
				return this.m_SelectedSlot;
			}
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x00169B80 File Offset: 0x00167D80
		private void Awake()
		{
			this.m_HotbarContainer = base.GetComponent<ItemContainer>();
			this.m_HotbarContainer.Slot_Refreshed.AddListener(new Action<Slot>(this.On_Slot_Refreshed));
			this.m_HotbarContainer.Slot_PointerUp += this.On_Slot_PointerUp;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x00169BCC File Offset: 0x00167DCC
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

		// Token: 0x06003387 RID: 13191 RVA: 0x00169BDC File Offset: 0x00167DDC
		private bool Try_DestroyEquippedItem()
		{
			SavableItem item = base.Player.EquippedItem.Get();
			if (item && this.m_HotbarContainer.HasItem(item))
			{
				base.Player.ChangeEquippedItem.Try(null, true);
			}
			return this.m_HotbarContainer.TryRemoveItem(item);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x00169C30 File Offset: 0x00167E30
		private void On_Slot_PointerUp(PointerEventData data, Slot displayer)
		{
			if (displayer != this.m_SelectedSlot && data.pointerCurrentRaycast.gameObject == displayer.gameObject)
			{
				this.TrySelectSlot(this.m_HotbarSlots.IndexOf(displayer));
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x00169C7B File Offset: 0x00167E7B
		private void On_Slot_Refreshed(Slot slot)
		{
			if (slot == this.m_SelectedSlot)
			{
				this.TrySelectSlot(this.m_HotbarSlots.IndexOf(this.m_SelectedSlot));
			}
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00169CA4 File Offset: 0x00167EA4
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

		// Token: 0x0600338B RID: 13195 RVA: 0x00169DAC File Offset: 0x00167FAC
		public bool IsWeapon(ItemData item)
		{
			return item.Category == "Gun" || item.Category == "Melee Weapon" || item.Category == "Throwable";
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x00169DE4 File Offset: 0x00167FE4
		public bool IsConsume(ItemData item)
		{
			return item.Category == "Consumable";
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00169DF8 File Offset: 0x00167FF8
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

		// Token: 0x04002DC6 RID: 11718
		[SerializeField]
		[Range(0f, 100f)]
		private int m_FirstSelected;

		// Token: 0x04002DC7 RID: 11719
		[Header("Navigation")]
		[SerializeField]
		private bool m_EnableScrolling = true;

		// Token: 0x04002DC8 RID: 11720
		[SerializeField]
		[HideSwitch("m_EnableScrolling", true, 20f)]
		[Clamp(0f, 10f)]
		private float m_ScrollThreeshold = 0.3f;

		// Token: 0x04002DC9 RID: 11721
		[SerializeField]
		private bool m_SelectByDigits = true;

		// Token: 0x04002DCA RID: 11722
		[Header("Selection Graphics")]
		[SerializeField]
		private float m_SelectedSlotScale = 1f;

		// Token: 0x04002DCB RID: 11723
		[SerializeField]
		private Color m_FrameColor = Color.cyan;

		// Token: 0x04002DCC RID: 11724
		[SerializeField]
		private Sprite m_FrameSprite;

		// Token: 0x04002DCD RID: 11725
		private ItemContainer m_HotbarContainer;

		// Token: 0x04002DCE RID: 11726
		private List<Slot> m_HotbarSlots;

		// Token: 0x04002DCF RID: 11727
		private Slot m_SelectedSlot;

		// Token: 0x04002DD0 RID: 11728
		private int m_LastIndex;

		// Token: 0x04002DD1 RID: 11729
		private float m_CurScrollValue;

		// Token: 0x04002DD2 RID: 11730
		private Image m_Frame;
	}
}
