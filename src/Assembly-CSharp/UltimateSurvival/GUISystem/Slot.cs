using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000933 RID: 2355
	public class Slot : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06003C1C RID: 15388 RVA: 0x001AFF00 File Offset: 0x001AE100
		// (remove) Token: 0x06003C1D RID: 15389 RVA: 0x001AFF38 File Offset: 0x001AE138
		public event Slot.BaseAction E_Select;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06003C1E RID: 15390 RVA: 0x001AFF70 File Offset: 0x001AE170
		// (remove) Token: 0x06003C1F RID: 15391 RVA: 0x001AFFA8 File Offset: 0x001AE1A8
		public event Slot.BaseAction E_Deselect;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06003C20 RID: 15392 RVA: 0x001AFFE0 File Offset: 0x001AE1E0
		// (remove) Token: 0x06003C21 RID: 15393 RVA: 0x001B0018 File Offset: 0x001AE218
		public event Slot.PointerAction PointerDown;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06003C22 RID: 15394 RVA: 0x001B0050 File Offset: 0x001AE250
		// (remove) Token: 0x06003C23 RID: 15395 RVA: 0x001B0088 File Offset: 0x001AE288
		public event Slot.PointerAction PointerUp;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06003C24 RID: 15396 RVA: 0x001B00C0 File Offset: 0x001AE2C0
		// (remove) Token: 0x06003C25 RID: 15397 RVA: 0x001B00F8 File Offset: 0x001AE2F8
		public event Slot.PointerAction BeginDrag;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06003C26 RID: 15398 RVA: 0x001B0130 File Offset: 0x001AE330
		// (remove) Token: 0x06003C27 RID: 15399 RVA: 0x001B0168 File Offset: 0x001AE368
		public event Slot.PointerAction Drag;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06003C28 RID: 15400 RVA: 0x001B01A0 File Offset: 0x001AE3A0
		// (remove) Token: 0x06003C29 RID: 15401 RVA: 0x001B01D8 File Offset: 0x001AE3D8
		public event Slot.PointerAction EndDrag;

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x0002B6CB File Offset: 0x000298CB
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x0002B6D3 File Offset: 0x000298D3
		public ItemHolder ItemHolder { get; private set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x0002B6DC File Offset: 0x000298DC
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x0002B6E4 File Offset: 0x000298E4
		public ItemContainer Parent { get; private set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x0002B6ED File Offset: 0x000298ED
		public bool HasItem
		{
			get
			{
				return this.ItemHolder && this.ItemHolder.HasItem;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x0002B709 File Offset: 0x00029909
		public SavableItem CurrentItem
		{
			get
			{
				if (!this.ItemHolder)
				{
					return null;
				}
				return this.ItemHolder.CurrentItem;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x0002B725 File Offset: 0x00029925
		// (set) Token: 0x06003C31 RID: 15409 RVA: 0x0002B72D File Offset: 0x0002992D
		public ReorderableStringList RequiredCategories
		{
			get
			{
				return this.m_RequiredCategories;
			}
			set
			{
				this.m_RequiredCategories = value;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x0002B736 File Offset: 0x00029936
		// (set) Token: 0x06003C33 RID: 15411 RVA: 0x0002B73E File Offset: 0x0002993E
		public ReorderableStringList RequiredProperties
		{
			get
			{
				return this.m_RequiredProperties;
			}
			set
			{
				this.m_RequiredProperties = value;
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x001B0210 File Offset: 0x001AE410
		private void Start()
		{
			GameObject gameObject = GameObject.Find("8-Item Inspector");
			if (gameObject != null)
			{
				this.tooltip = gameObject.GetComponent<Tooltip>();
			}
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x001B0240 File Offset: 0x001AE440
		public bool AllowsItem(SavableItem item)
		{
			bool flag = false;
			bool flag2 = true;
			if (this.m_RequiredProperties.Count > 0)
			{
				for (int i = 0; i < this.m_RequiredProperties.Count; i++)
				{
					if (!item.HasProperty(this.m_RequiredProperties[i]))
					{
						flag2 = false;
						break;
					}
				}
			}
			if (this.m_RequiredCategories.Count > 0)
			{
				for (int j = 0; j < this.m_RequiredCategories.Count; j++)
				{
					if (this.m_RequiredCategories[j] == item.ItemData.Category)
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				flag = true;
			}
			return flag && flag2;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x001B02DC File Offset: 0x001AE4DC
		public void LinkWithHolder(ItemHolder holder)
		{
			if (this.ItemHolder)
			{
				this.ItemHolder.Updated.RemoveListener(new Action<ItemHolder>(this.On_ItemHolder_Updated));
			}
			this.ItemHolder = holder;
			this.Refresh();
			this.ItemHolder.Updated.AddListener(new Action<ItemHolder>(this.On_ItemHolder_Updated));
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x001B033C File Offset: 0x001AE53C
		public void Refresh()
		{
			this.m_ItemIcon.enabled = this.HasItem;
			this.m_StackDisplayer.enabled = (this.HasItem && this.CurrentItem.CurrentInStack > 1);
			this.m_DurabilityBar.SetActive(this.HasItem && this.CurrentItem.HasProperty("Durability"));
			if (this.m_ItemIcon.enabled)
			{
				this.m_ItemIcon.sprite = this.CurrentItem.ItemData.Icon;
			}
			if (this.m_StackDisplayer.enabled)
			{
				this.m_StackDisplayer.text = string.Format("x{0}", this.CurrentItem.CurrentInStack);
			}
			if (this.m_DurabilityBar.Active)
			{
				this.m_DurabilityBar.SetFillAmount(this.CurrentItem.GetPropertyValue("Durability").Float.Ratio);
			}
			this.Refreshed.Send(this);
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x001B0440 File Offset: 0x001AE640
		public RectTransform GetDragTemplate(SavableItem forItem, float alpha)
		{
			Slot slot = Object.Instantiate<Slot>(this);
			Transform transform = slot.transform.FindDeepChild("Frame");
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
			slot.enabled = false;
			slot.image.enabled = false;
			slot.m_ItemIcon.enabled = true;
			slot.m_StackDisplayer.enabled = (forItem.CurrentInStack > 1);
			slot.m_StackDisplayer.text = string.Format("x{0}", forItem.CurrentInStack);
			slot.m_DurabilityBar.SetActive(forItem.HasProperty("Durability"));
			slot.gameObject.AddComponent<CanvasGroup>().alpha = alpha;
			return slot.GetComponent<RectTransform>();
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x0002B747 File Offset: 0x00029947
		public void SetScale(Vector3 localScale, float duration)
		{
			if (base.IsDestroyed())
			{
				return;
			}
			if (this.m_ScaleSetter != null)
			{
				base.StopCoroutine(this.m_ScaleSetter);
			}
			this.m_ScaleSetter = base.StartCoroutine(this.C_SetScale(localScale, duration));
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x0002B77A File Offset: 0x0002997A
		public override void OnSelect(BaseEventData data)
		{
			base.OnSelect(data);
			if (this.E_Select != null)
			{
				this.E_Select(data, this);
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x001B04F8 File Offset: 0x001AE6F8
		public override void OnDeselect(BaseEventData data)
		{
			if (this.HasItem && !MonoSingleton<InventoryController>.Instance.IsClosed && MonoSingleton<GUIController>.Instance.MouseOverSelectionKeeper())
			{
				base.StartCoroutine(this.C_WaitAndSelect(1));
				return;
			}
			if (this.E_Deselect != null)
			{
				this.E_Deselect(data, this);
			}
			base.OnDeselect(data);
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x001B0550 File Offset: 0x001AE750
		public override void OnPointerDown(PointerEventData data)
		{
			base.OnPointerDown(data);
			if (this.PointerDown != null)
			{
				this.PointerDown(data, this);
			}
			if (this.tooltip != null)
			{
				this.tooltip.tooltipType = TooltipType.Inventory;
				Transform transform = base.transform;
				while (transform.parent)
				{
					if (transform.tag == "MainInventory")
					{
						this.tooltip.tooltipType = TooltipType.Inventory;
						break;
					}
					if (transform.tag == "EquipmentSystem")
					{
						this.tooltip.tooltipType = TooltipType.Equipment;
						break;
					}
					transform = transform.parent;
				}
				this.item = base.GetComponent<ItemOnObject>().item;
				this.tooltip.item = this.item;
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x0002B798 File Offset: 0x00029998
		public override void OnPointerUp(PointerEventData data)
		{
			base.OnPointerUp(data);
			if (this.PointerUp != null)
			{
				this.PointerUp(data, this);
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0002B7B6 File Offset: 0x000299B6
		public void OnBeginDrag(PointerEventData data)
		{
			if (this.BeginDrag != null)
			{
				this.BeginDrag(data, this);
			}
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0002B7CD File Offset: 0x000299CD
		public void OnDrag(PointerEventData data)
		{
			if (this.Drag != null)
			{
				this.Drag(data, this);
			}
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x0002B7E4 File Offset: 0x000299E4
		public void exchengeSlotItem(Slot chengeSlot)
		{
			ItemOnObject component = base.gameObject.GetComponent<ItemOnObject>();
			component.item = component.gameObject.GetComponent<ItemOnObject>().item;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x001B0618 File Offset: 0x001AE818
		public void OnEndDrag(PointerEventData data)
		{
			if (this.EndDrag != null)
			{
				this.EndDrag(data, this);
			}
			if (data.pointerCurrentRaycast.gameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(null);
				this.DoStateTransition(this.HasItem ? 0 : 3, true);
			}
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x0002B806 File Offset: 0x00029A06
		protected override void Awake()
		{
			base.Awake();
			if (!Application.isPlaying)
			{
				return;
			}
			this.Parent = base.GetComponentInParent<ItemContainer>();
			this.m_ItemIcon.enabled = false;
			this.m_StackDisplayer.enabled = false;
			this.m_DurabilityBar.SetActive(false);
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x001B0674 File Offset: 0x001AE874
		private void On_ItemHolder_Updated(ItemHolder holder)
		{
			this.Refresh();
			ItemOnObject component = base.GetComponent<ItemOnObject>();
			component.item = World.inventoryItemList.getItemByID(holder.itemID);
			component.item.itemUUID = holder.uuid;
			component.item.itemIndex = holder.index;
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x0002B846 File Offset: 0x00029A46
		private IEnumerator C_SetScale(Vector3 localScale, float duration)
		{
			float finishTime = Time.time + duration;
			float scaleChangeSpeed = (localScale - base.transform.localScale).magnitude / duration;
			while (Time.time < finishTime)
			{
				base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, localScale, scaleChangeSpeed * Time.deltaTime);
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x0002B863 File Offset: 0x00029A63
		private IEnumerator C_WaitAndSelect(int waitFrameCount)
		{
			int num;
			for (int i = 0; i < waitFrameCount; i = num + 1)
			{
				yield return null;
				num = i;
			}
			this.Select();
			yield break;
		}

		// Token: 0x0400367D RID: 13949
		public Tooltip tooltip;

		// Token: 0x0400367E RID: 13950
		public Item item;

		// Token: 0x0400367F RID: 13951
		public Message<Slot> Refreshed = new Message<Slot>();

		// Token: 0x04003689 RID: 13961
		[Header("Setup")]
		[SerializeField]
		private Image m_ItemIcon;

		// Token: 0x0400368A RID: 13962
		[SerializeField]
		private Text m_StackDisplayer;

		// Token: 0x0400368B RID: 13963
		[SerializeField]
		private DurabilityBar m_DurabilityBar;

		// Token: 0x0400368C RID: 13964
		[Header("Required Stuff")]
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredCategories;

		// Token: 0x0400368D RID: 13965
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredProperties;

		// Token: 0x0400368E RID: 13966
		private Coroutine m_ScaleSetter;

		// Token: 0x02000934 RID: 2356
		// (Invoke) Token: 0x06003C48 RID: 15432
		public delegate void BaseAction(BaseEventData data, Slot wrapper);

		// Token: 0x02000935 RID: 2357
		// (Invoke) Token: 0x06003C4C RID: 15436
		public delegate void PointerAction(PointerEventData data, Slot wrapper);
	}
}
