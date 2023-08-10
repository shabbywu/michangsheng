using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class Slot : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	public delegate void BaseAction(BaseEventData data, Slot wrapper);

	public delegate void PointerAction(PointerEventData data, Slot wrapper);

	public Tooltip tooltip;

	public Item item;

	public Message<Slot> Refreshed = new Message<Slot>();

	[Header("Setup")]
	[SerializeField]
	private Image m_ItemIcon;

	[SerializeField]
	private Text m_StackDisplayer;

	[SerializeField]
	private DurabilityBar m_DurabilityBar;

	[Header("Required Stuff")]
	[SerializeField]
	[Reorderable]
	private ReorderableStringList m_RequiredCategories;

	[SerializeField]
	[Reorderable]
	private ReorderableStringList m_RequiredProperties;

	private Coroutine m_ScaleSetter;

	public ItemHolder ItemHolder { get; private set; }

	public ItemContainer Parent { get; private set; }

	public bool HasItem
	{
		get
		{
			if ((bool)ItemHolder)
			{
				return ItemHolder.HasItem;
			}
			return false;
		}
	}

	public SavableItem CurrentItem
	{
		get
		{
			if (!ItemHolder)
			{
				return null;
			}
			return ItemHolder.CurrentItem;
		}
	}

	public ReorderableStringList RequiredCategories
	{
		get
		{
			return m_RequiredCategories;
		}
		set
		{
			m_RequiredCategories = value;
		}
	}

	public ReorderableStringList RequiredProperties
	{
		get
		{
			return m_RequiredProperties;
		}
		set
		{
			m_RequiredProperties = value;
		}
	}

	public event BaseAction E_Select;

	public event BaseAction E_Deselect;

	public event PointerAction PointerDown;

	public event PointerAction PointerUp;

	public event PointerAction BeginDrag;

	public event PointerAction Drag;

	public event PointerAction EndDrag;

	private void Start()
	{
		GameObject val = GameObject.Find("8-Item Inspector");
		if ((Object)(object)val != (Object)null)
		{
			tooltip = val.GetComponent<Tooltip>();
		}
	}

	public bool AllowsItem(SavableItem item)
	{
		bool flag = false;
		bool flag2 = true;
		if (m_RequiredProperties.Count > 0)
		{
			for (int i = 0; i < m_RequiredProperties.Count; i++)
			{
				if (!item.HasProperty(m_RequiredProperties[i]))
				{
					flag2 = false;
					break;
				}
			}
		}
		if (m_RequiredCategories.Count > 0)
		{
			for (int j = 0; j < m_RequiredCategories.Count; j++)
			{
				if (m_RequiredCategories[j] == item.ItemData.Category)
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

	public void LinkWithHolder(ItemHolder holder)
	{
		if ((bool)ItemHolder)
		{
			ItemHolder.Updated.RemoveListener(On_ItemHolder_Updated);
		}
		ItemHolder = holder;
		Refresh();
		ItemHolder.Updated.AddListener(On_ItemHolder_Updated);
	}

	public void Refresh()
	{
		((Behaviour)m_ItemIcon).enabled = HasItem;
		((Behaviour)m_StackDisplayer).enabled = HasItem && CurrentItem.CurrentInStack > 1;
		m_DurabilityBar.SetActive(HasItem && CurrentItem.HasProperty("Durability"));
		if (((Behaviour)m_ItemIcon).enabled)
		{
			m_ItemIcon.sprite = CurrentItem.ItemData.Icon;
		}
		if (((Behaviour)m_StackDisplayer).enabled)
		{
			m_StackDisplayer.text = $"x{CurrentItem.CurrentInStack}";
		}
		if (m_DurabilityBar.Active)
		{
			m_DurabilityBar.SetFillAmount(CurrentItem.GetPropertyValue("Durability").Float.Ratio);
		}
		Refreshed.Send(this);
	}

	public RectTransform GetDragTemplate(SavableItem forItem, float alpha)
	{
		Slot slot = Object.Instantiate<Slot>(this);
		Transform val = ((Component)slot).transform.FindDeepChild("Frame");
		if ((Object)(object)val != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)val).gameObject);
		}
		((Behaviour)slot).enabled = false;
		((Behaviour)((Selectable)slot).image).enabled = false;
		((Behaviour)slot.m_ItemIcon).enabled = true;
		((Behaviour)slot.m_StackDisplayer).enabled = forItem.CurrentInStack > 1;
		slot.m_StackDisplayer.text = $"x{forItem.CurrentInStack}";
		slot.m_DurabilityBar.SetActive(forItem.HasProperty("Durability"));
		((Component)slot).gameObject.AddComponent<CanvasGroup>().alpha = alpha;
		return ((Component)slot).GetComponent<RectTransform>();
	}

	public void SetScale(Vector3 localScale, float duration)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (!((UIBehaviour)this).IsDestroyed())
		{
			if (m_ScaleSetter != null)
			{
				((MonoBehaviour)this).StopCoroutine(m_ScaleSetter);
			}
			m_ScaleSetter = ((MonoBehaviour)this).StartCoroutine(C_SetScale(localScale, duration));
		}
	}

	public override void OnSelect(BaseEventData data)
	{
		((Selectable)this).OnSelect(data);
		if (this.E_Select != null)
		{
			this.E_Select(data, this);
		}
	}

	public override void OnDeselect(BaseEventData data)
	{
		if (HasItem && !MonoSingleton<InventoryController>.Instance.IsClosed && MonoSingleton<GUIController>.Instance.MouseOverSelectionKeeper())
		{
			((MonoBehaviour)this).StartCoroutine(C_WaitAndSelect(1));
			return;
		}
		if (this.E_Deselect != null)
		{
			this.E_Deselect(data, this);
		}
		((Selectable)this).OnDeselect(data);
	}

	public override void OnPointerDown(PointerEventData data)
	{
		((Selectable)this).OnPointerDown(data);
		if (this.PointerDown != null)
		{
			this.PointerDown(data, this);
		}
		if (!((Object)(object)tooltip != (Object)null))
		{
			return;
		}
		tooltip.tooltipType = TooltipType.Inventory;
		Transform val = ((Component)this).transform;
		while (Object.op_Implicit((Object)(object)val.parent))
		{
			if (((Component)val).tag == "MainInventory")
			{
				tooltip.tooltipType = TooltipType.Inventory;
				break;
			}
			if (((Component)val).tag == "EquipmentSystem")
			{
				tooltip.tooltipType = TooltipType.Equipment;
				break;
			}
			val = val.parent;
		}
		item = ((Component)this).GetComponent<ItemOnObject>().item;
		tooltip.item = item;
	}

	public override void OnPointerUp(PointerEventData data)
	{
		((Selectable)this).OnPointerUp(data);
		if (this.PointerUp != null)
		{
			this.PointerUp(data, this);
		}
	}

	public void OnBeginDrag(PointerEventData data)
	{
		if (this.BeginDrag != null)
		{
			this.BeginDrag(data, this);
		}
	}

	public void OnDrag(PointerEventData data)
	{
		if (this.Drag != null)
		{
			this.Drag(data, this);
		}
	}

	public void exchengeSlotItem(Slot chengeSlot)
	{
		ItemOnObject component = ((Component)this).gameObject.GetComponent<ItemOnObject>();
		component.item = ((Component)component).gameObject.GetComponent<ItemOnObject>().item;
	}

	public void OnEndDrag(PointerEventData data)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (this.EndDrag != null)
		{
			this.EndDrag(data, this);
		}
		RaycastResult pointerCurrentRaycast = data.pointerCurrentRaycast;
		if ((Object)(object)((RaycastResult)(ref pointerCurrentRaycast)).gameObject != (Object)(object)((Component)this).gameObject)
		{
			EventSystem.current.SetSelectedGameObject((GameObject)null);
			((Selectable)this).DoStateTransition((SelectionState)((!HasItem) ? 3 : 0), true);
		}
	}

	protected override void Awake()
	{
		((Selectable)this).Awake();
		if (Application.isPlaying)
		{
			Parent = ((Component)this).GetComponentInParent<ItemContainer>();
			((Behaviour)m_ItemIcon).enabled = false;
			((Behaviour)m_StackDisplayer).enabled = false;
			m_DurabilityBar.SetActive(active: false);
		}
	}

	private void On_ItemHolder_Updated(ItemHolder holder)
	{
		Refresh();
		ItemOnObject component = ((Component)this).GetComponent<ItemOnObject>();
		component.item = World.inventoryItemList.getItemByID(holder.itemID);
		component.item.itemUUID = holder.uuid;
		component.item.itemIndex = holder.index;
	}

	private IEnumerator C_SetScale(Vector3 localScale, float duration)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		float finishTime = Time.time + duration;
		Vector3 val = localScale - ((Component)this).transform.localScale;
		float scaleChangeSpeed = ((Vector3)(ref val)).magnitude / duration;
		while (Time.time < finishTime)
		{
			((Component)this).transform.localScale = Vector3.MoveTowards(((Component)this).transform.localScale, localScale, scaleChangeSpeed * Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator C_WaitAndSelect(int waitFrameCount)
	{
		for (int i = 0; i < waitFrameCount; i++)
		{
			yield return null;
		}
		((Selectable)this).Select();
	}
}
