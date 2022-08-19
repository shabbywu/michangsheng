using System;
using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000655 RID: 1621
	public class ItemInspector : GUIBehaviour
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x0016A031 File Offset: 0x00168231
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x0016A039 File Offset: 0x00168239
		public Slot InspectedSlot { get; private set; }

		// Token: 0x06003392 RID: 13202 RVA: 0x0016A044 File Offset: 0x00168244
		private void Awake()
		{
			this.m_DismantleButton.onClick.AddListener(new UnityAction(this.On_DismantleClicked));
			this.m_DropButton.onClick.AddListener(new UnityAction(this.On_DropClicked));
			this.m_ConsumeButton.onClick.AddListener(new UnityAction(this.On_ConsumeClicked));
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x0016A0A8 File Offset: 0x001682A8
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

		// Token: 0x06003394 RID: 13204 RVA: 0x0016A108 File Offset: 0x00168308
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

		// Token: 0x06003395 RID: 13205 RVA: 0x0016A1BC File Offset: 0x001683BC
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

		// Token: 0x06003396 RID: 13206 RVA: 0x0016A364 File Offset: 0x00168564
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

		// Token: 0x06003397 RID: 13207 RVA: 0x0016A3C4 File Offset: 0x001685C4
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

		// Token: 0x06003398 RID: 13208 RVA: 0x0016A488 File Offset: 0x00168688
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

		// Token: 0x06003399 RID: 13209 RVA: 0x0016A504 File Offset: 0x00168704
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

		// Token: 0x0600339A RID: 13210 RVA: 0x0016A5A5 File Offset: 0x001687A5
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

		// Token: 0x04002DD4 RID: 11732
		[Header("Setup")]
		[SerializeField]
		private Camera m_GUICamera;

		// Token: 0x04002DD5 RID: 11733
		[SerializeField]
		private Window m_Window;

		// Token: 0x04002DD6 RID: 11734
		[Header("Item Info")]
		[SerializeField]
		private Text m_ItemName;

		// Token: 0x04002DD7 RID: 11735
		[SerializeField]
		private Text m_MainDescription;

		// Token: 0x04002DD8 RID: 11736
		[SerializeField]
		private Text m_SecondaryDescription;

		// Token: 0x04002DD9 RID: 11737
		[SerializeField]
		private Image m_Icon;

		// Token: 0x04002DDA RID: 11738
		[SerializeField]
		private DurabilityBar m_DurabilityBar;

		// Token: 0x04002DDB RID: 11739
		[SerializeField]
		private Text m_Magazine;

		// Token: 0x04002DDC RID: 11740
		[Header("Actions")]
		[SerializeField]
		private Button m_DropButton;

		// Token: 0x04002DDD RID: 11741
		[SerializeField]
		private Button m_ConsumeButton;

		// Token: 0x04002DDE RID: 11742
		[SerializeField]
		private Button m_DismantleButton;

		// Token: 0x04002DDF RID: 11743
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_ItemDropAudio;

		// Token: 0x04002DE0 RID: 11744
		[SerializeField]
		private SoundPlayer m_DismantleAudio;

		// Token: 0x04002DE1 RID: 11745
		[SerializeField]
		private float m_ConsumeVolume = 0.6f;

		// Token: 0x04002DE2 RID: 11746
		private ItemContainer[] m_InspectableContainers;

		// Token: 0x04002DE3 RID: 11747
		private ItemContainer m_InventoryContainer;

		// Token: 0x04002DE4 RID: 11748
		private float m_LastConsumeTime;
	}
}
