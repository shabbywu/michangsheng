using System;
using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000956 RID: 2390
	public class ItemInspector : GUIBehaviour
	{
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x0002C025 File Offset: 0x0002A225
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x0002C02D File Offset: 0x0002A22D
		public Slot InspectedSlot { get; private set; }

		// Token: 0x06003D14 RID: 15636 RVA: 0x001B30F4 File Offset: 0x001B12F4
		private void Awake()
		{
			this.m_DismantleButton.onClick.AddListener(new UnityAction(this.On_DismantleClicked));
			this.m_DropButton.onClick.AddListener(new UnityAction(this.On_DropClicked));
			this.m_ConsumeButton.onClick.AddListener(new UnityAction(this.On_ConsumeClicked));
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x001B3158 File Offset: 0x001B1358
		private void Start()
		{
			this.m_InspectableContainers = base.Controller.Containers;
			ItemContainer[] inspectableContainers = this.m_InspectableContainers;
			for (int i = 0; i < inspectableContainers.Length; i++)
			{
				inspectableContainers[i].Slot_PointerUp += new Action<PointerEventData, Slot>(this.On_Slot_PointerUp);
			}
			this.m_InventoryContainer = base.Controller.GetContainer("Inventory");
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x001B31B8 File Offset: 0x001B13B8
		private void On_Slot_PointerUp(BaseEventData data, Slot slot)
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed && slot.HasItem && EventSystem.current.currentSelectedGameObject == slot.gameObject)
			{
				if (this.m_Window)
				{
					this.m_Window.Open();
				}
				this.InspectedSlot = slot;
				this.ShowInfo(slot.CurrentItem);
				this.InspectedSlot.E_Deselect += delegate(BaseEventData d, Slot sd)
				{
					base.StartCoroutine(this.C_WaitAndSelect());
				};
				this.InspectedSlot.ItemHolder.Updated.AddListener(new Action<ItemHolder>(this.On_InspectedHolderUpdated));
				return;
			}
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_WaitAndSelect());
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x001B326C File Offset: 0x001B146C
		private void ShowInfo(SavableItem item)
		{
			this.m_ItemName.text = ((item.ItemData.DisplayName == string.Empty) ? item.ItemData.Name : item.ItemData.DisplayName);
			if (item.ItemData.Descriptions.Length != 0)
			{
				this.m_MainDescription.text = item.GetDescription(0);
			}
			else
			{
				this.m_MainDescription.text = "";
			}
			if (item.ItemData.Descriptions.Length > 1)
			{
				this.m_SecondaryDescription.text = item.GetDescription(1);
			}
			else
			{
				this.m_SecondaryDescription.text = "";
			}
			this.m_Icon.sprite = item.ItemData.Icon;
			if (item.HasProperty("Durability"))
			{
				if (!this.m_DurabilityBar.Active)
				{
					this.m_DurabilityBar.SetActive(true);
				}
				this.m_DurabilityBar.SetFillAmount(item.GetPropertyValue("Durability").Float.Ratio);
			}
			else if (this.m_DurabilityBar.Active)
			{
				this.m_DurabilityBar.SetActive(false);
			}
			ItemProperty.Value value;
			if (item.FindPropertyValue("Magazine", out value))
			{
				ItemProperty.IntRange intRange = value.IntRange;
				this.m_Magazine.text = "库存: " + intRange.ToString();
			}
			else
			{
				this.m_Magazine.text = "";
			}
			this.m_ConsumeButton.gameObject.SetActive(item.HasProperty("Can Consume"));
			this.m_DismantleButton.gameObject.SetActive(item.HasProperty("Can Dismantle"));
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x001B3414 File Offset: 0x001B1614
		private void On_InspectedHolderUpdated(ItemHolder holder)
		{
			if (!holder.HasItem)
			{
				this.m_Window.Close(false);
				try
				{
					this.InspectedSlot.ItemHolder.Updated.RemoveListener(new Action<ItemHolder>(this.On_InspectedHolderUpdated));
					this.InspectedSlot = null;
				}
				catch
				{
				}
			}
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x001B3474 File Offset: 0x001B1674
		private void On_DismantleClicked()
		{
			SavableItem currentItem = this.InspectedSlot.CurrentItem;
			this.InspectedSlot.ItemHolder.SetItem(null);
			RequiredItem[] requiredItems = currentItem.ItemData.Recipe.RequiredItems;
			for (int i = 0; i < requiredItems.Length; i++)
			{
				int amount = Mathf.RoundToInt((float)requiredItems[i].Amount * currentItem.GetPropertyValue("Durability").Float.Ratio * 0.6f) + 1;
				this.m_InventoryContainer.TryAddItem(requiredItems[i].Name, amount);
			}
			MonoSingleton<MessageDisplayer>.Instance.PushMessage(string.Format("<color=yellow>{0}</color> has been dismantled", currentItem.Name), default(Color), 16);
			this.m_DismantleAudio.Play2D(ItemSelectionMethod.RandomlyButExcludeLast);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x001B3538 File Offset: 0x001B1738
		private void On_DropClicked()
		{
			SavableItem savableItem = base.Controller.Player.EquippedItem.Get();
			SavableItem currentItem = this.InspectedSlot.CurrentItem;
			if (savableItem == currentItem)
			{
				base.Controller.Player.ChangeEquippedItem.Try(null, true);
			}
			Tooltip component = base.gameObject.GetComponent<Tooltip>();
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar != null)
			{
				avatar.dropRequest(component.item.itemUUID);
			}
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x001B35B4 File Offset: 0x001B17B4
		private void On_ConsumeClicked()
		{
			SavableItem currentItem = this.InspectedSlot.CurrentItem;
			if (currentItem.HasProperty("Can Consume"))
			{
				if (currentItem.HasProperty("Consume Sound") && Time.time - this.m_LastConsumeTime > 2f)
				{
					GameController.Audio.Play2D(currentItem.GetPropertyValue("Consume Sound").Sound, this.m_ConsumeVolume);
				}
				Tooltip component = base.gameObject.GetComponent<Tooltip>();
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				if (avatar != null)
				{
					avatar.useItemRequest(component.item.itemUUID);
				}
				this.m_LastConsumeTime = Time.time;
			}
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x0002C036 File Offset: 0x0002A236
		private IEnumerator C_WaitAndSelect()
		{
			yield return null;
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			Slot slot = null;
			if (currentSelectedGameObject)
			{
				slot = currentSelectedGameObject.GetComponent<Slot>();
			}
			if (!currentSelectedGameObject || !slot || !slot.HasItem)
			{
				if (this.m_Window)
				{
					this.m_Window.Close(false);
				}
				this.InspectedSlot = null;
			}
			yield break;
		}

		// Token: 0x04003752 RID: 14162
		[Header("Setup")]
		[SerializeField]
		private Camera m_GUICamera;

		// Token: 0x04003753 RID: 14163
		[SerializeField]
		private Window m_Window;

		// Token: 0x04003754 RID: 14164
		[Header("Item Info")]
		[SerializeField]
		private Text m_ItemName;

		// Token: 0x04003755 RID: 14165
		[SerializeField]
		private Text m_MainDescription;

		// Token: 0x04003756 RID: 14166
		[SerializeField]
		private Text m_SecondaryDescription;

		// Token: 0x04003757 RID: 14167
		[SerializeField]
		private Image m_Icon;

		// Token: 0x04003758 RID: 14168
		[SerializeField]
		private DurabilityBar m_DurabilityBar;

		// Token: 0x04003759 RID: 14169
		[SerializeField]
		private Text m_Magazine;

		// Token: 0x0400375A RID: 14170
		[Header("Actions")]
		[SerializeField]
		private Button m_DropButton;

		// Token: 0x0400375B RID: 14171
		[SerializeField]
		private Button m_ConsumeButton;

		// Token: 0x0400375C RID: 14172
		[SerializeField]
		private Button m_DismantleButton;

		// Token: 0x0400375D RID: 14173
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_ItemDropAudio;

		// Token: 0x0400375E RID: 14174
		[SerializeField]
		private SoundPlayer m_DismantleAudio;

		// Token: 0x0400375F RID: 14175
		[SerializeField]
		private float m_ConsumeVolume = 0.6f;

		// Token: 0x04003760 RID: 14176
		private ItemContainer[] m_InspectableContainers;

		// Token: 0x04003761 RID: 14177
		private ItemContainer m_InventoryContainer;

		// Token: 0x04003762 RID: 14178
		private float m_LastConsumeTime;
	}
}
