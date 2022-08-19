using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200063D RID: 1597
	public class Slot : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060032D4 RID: 13012 RVA: 0x00166C7C File Offset: 0x00164E7C
		// (remove) Token: 0x060032D5 RID: 13013 RVA: 0x00166CB4 File Offset: 0x00164EB4
		public event Slot.BaseAction E_Select;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060032D6 RID: 13014 RVA: 0x00166CEC File Offset: 0x00164EEC
		// (remove) Token: 0x060032D7 RID: 13015 RVA: 0x00166D24 File Offset: 0x00164F24
		public event Slot.BaseAction E_Deselect;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060032D8 RID: 13016 RVA: 0x00166D5C File Offset: 0x00164F5C
		// (remove) Token: 0x060032D9 RID: 13017 RVA: 0x00166D94 File Offset: 0x00164F94
		public event Slot.PointerAction PointerDown;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060032DA RID: 13018 RVA: 0x00166DCC File Offset: 0x00164FCC
		// (remove) Token: 0x060032DB RID: 13019 RVA: 0x00166E04 File Offset: 0x00165004
		public event Slot.PointerAction PointerUp;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060032DC RID: 13020 RVA: 0x00166E3C File Offset: 0x0016503C
		// (remove) Token: 0x060032DD RID: 13021 RVA: 0x00166E74 File Offset: 0x00165074
		public event Slot.PointerAction BeginDrag;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060032DE RID: 13022 RVA: 0x00166EAC File Offset: 0x001650AC
		// (remove) Token: 0x060032DF RID: 13023 RVA: 0x00166EE4 File Offset: 0x001650E4
		public event Slot.PointerAction Drag;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060032E0 RID: 13024 RVA: 0x00166F1C File Offset: 0x0016511C
		// (remove) Token: 0x060032E1 RID: 13025 RVA: 0x00166F54 File Offset: 0x00165154
		public event Slot.PointerAction EndDrag;

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x00166F89 File Offset: 0x00165189
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x00166F91 File Offset: 0x00165191
		public ItemHolder ItemHolder { get; private set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x00166F9A File Offset: 0x0016519A
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x00166FA2 File Offset: 0x001651A2
		public ItemContainer Parent { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x00166FAB File Offset: 0x001651AB
		public bool HasItem
		{
			get
			{
				return this.ItemHolder && this.ItemHolder.HasItem;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x00166FC7 File Offset: 0x001651C7
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x00166FE3 File Offset: 0x001651E3
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x00166FEB File Offset: 0x001651EB
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x00166FF4 File Offset: 0x001651F4
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x00166FFC File Offset: 0x001651FC
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

		// Token: 0x060032EC RID: 13036 RVA: 0x00167008 File Offset: 0x00165208
		private void Start()
		{
			GameObject gameObject = GameObject.Find("8-Item Inspector");
			if (gameObject != null)
			{
				this.tooltip = gameObject.GetComponent<Tooltip>();
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x00167038 File Offset: 0x00165238
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

		// Token: 0x060032EE RID: 13038 RVA: 0x001670D4 File Offset: 0x001652D4
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

		// Token: 0x060032EF RID: 13039 RVA: 0x00167134 File Offset: 0x00165334
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

		// Token: 0x060032F0 RID: 13040 RVA: 0x00167238 File Offset: 0x00165438
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

		// Token: 0x060032F1 RID: 13041 RVA: 0x001672EF File Offset: 0x001654EF
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

		// Token: 0x060032F2 RID: 13042 RVA: 0x00167322 File Offset: 0x00165522
		public override void OnSelect(BaseEventData data)
		{
			base.OnSelect(data);
			if (this.E_Select != null)
			{
				this.E_Select(data, this);
			}
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x00167340 File Offset: 0x00165540
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

		// Token: 0x060032F4 RID: 13044 RVA: 0x00167398 File Offset: 0x00165598
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

		// Token: 0x060032F5 RID: 13045 RVA: 0x0016745D File Offset: 0x0016565D
		public override void OnPointerUp(PointerEventData data)
		{
			base.OnPointerUp(data);
			if (this.PointerUp != null)
			{
				this.PointerUp(data, this);
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0016747B File Offset: 0x0016567B
		public void OnBeginDrag(PointerEventData data)
		{
			if (this.BeginDrag != null)
			{
				this.BeginDrag(data, this);
			}
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x00167492 File Offset: 0x00165692
		public void OnDrag(PointerEventData data)
		{
			if (this.Drag != null)
			{
				this.Drag(data, this);
			}
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x001674A9 File Offset: 0x001656A9
		public void exchengeSlotItem(Slot chengeSlot)
		{
			ItemOnObject component = base.gameObject.GetComponent<ItemOnObject>();
			component.item = component.gameObject.GetComponent<ItemOnObject>().item;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x001674CC File Offset: 0x001656CC
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

		// Token: 0x060032FA RID: 13050 RVA: 0x00167527 File Offset: 0x00165727
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

		// Token: 0x060032FB RID: 13051 RVA: 0x00167568 File Offset: 0x00165768
		private void On_ItemHolder_Updated(ItemHolder holder)
		{
			this.Refresh();
			ItemOnObject component = base.GetComponent<ItemOnObject>();
			component.item = World.inventoryItemList.getItemByID(holder.itemID);
			component.item.itemUUID = holder.uuid;
			component.item.itemIndex = holder.index;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x001675B8 File Offset: 0x001657B8
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

		// Token: 0x060032FD RID: 13053 RVA: 0x001675D5 File Offset: 0x001657D5
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

		// Token: 0x04002D20 RID: 11552
		public Tooltip tooltip;

		// Token: 0x04002D21 RID: 11553
		public Item item;

		// Token: 0x04002D22 RID: 11554
		public Message<Slot> Refreshed = new Message<Slot>();

		// Token: 0x04002D2C RID: 11564
		[Header("Setup")]
		[SerializeField]
		private Image m_ItemIcon;

		// Token: 0x04002D2D RID: 11565
		[SerializeField]
		private Text m_StackDisplayer;

		// Token: 0x04002D2E RID: 11566
		[SerializeField]
		private DurabilityBar m_DurabilityBar;

		// Token: 0x04002D2F RID: 11567
		[Header("Required Stuff")]
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredCategories;

		// Token: 0x04002D30 RID: 11568
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredProperties;

		// Token: 0x04002D31 RID: 11569
		private Coroutine m_ScaleSetter;

		// Token: 0x020014E0 RID: 5344
		// (Invoke) Token: 0x06008225 RID: 33317
		public delegate void BaseAction(BaseEventData data, Slot wrapper);

		// Token: 0x020014E1 RID: 5345
		// (Invoke) Token: 0x06008229 RID: 33321
		public delegate void PointerAction(PointerEventData data, Slot wrapper);
	}
}
