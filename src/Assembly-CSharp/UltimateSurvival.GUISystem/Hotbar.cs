using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

[RequireComponent(typeof(ItemContainer))]
public class Hotbar : GUIBehaviour
{
	[SerializeField]
	[Range(0f, 100f)]
	private int m_FirstSelected;

	[Header("Navigation")]
	[SerializeField]
	private bool m_EnableScrolling = true;

	[SerializeField]
	[HideSwitch("m_EnableScrolling", true, 20f)]
	[Clamp(0f, 10f)]
	private float m_ScrollThreeshold = 0.3f;

	[SerializeField]
	private bool m_SelectByDigits = true;

	[Header("Selection Graphics")]
	[SerializeField]
	private float m_SelectedSlotScale = 1f;

	[SerializeField]
	private Color m_FrameColor = Color.cyan;

	[SerializeField]
	private Sprite m_FrameSprite;

	private ItemContainer m_HotbarContainer;

	private List<Slot> m_HotbarSlots;

	private Slot m_SelectedSlot;

	private int m_LastIndex;

	private float m_CurScrollValue;

	private Image m_Frame;

	public Slot SelectedSlot => m_SelectedSlot;

	private void Awake()
	{
		m_HotbarContainer = ((Component)this).GetComponent<ItemContainer>();
		m_HotbarContainer.Slot_Refreshed.AddListener(On_Slot_Refreshed);
		m_HotbarContainer.Slot_PointerUp += On_Slot_PointerUp;
	}

	private IEnumerator Start()
	{
		base.Player.DestroyEquippedItem.SetTryer(Try_DestroyEquippedItem);
		base.Player.Sleep.AddStopListener(delegate
		{
			TrySelectSlot(0);
		});
		m_HotbarSlots = m_HotbarContainer.Slots;
		m_Frame = GUIUtils.CreateImageUnder("Frame", ((Component)this).GetComponent<RectTransform>(), Vector2.zero, Vector2.zero);
		m_Frame.sprite = m_FrameSprite;
		((Graphic)m_Frame).color = m_FrameColor;
		((Behaviour)m_Frame).enabled = false;
		yield return null;
		TrySelectSlot(m_FirstSelected);
	}

	private bool Try_DestroyEquippedItem()
	{
		SavableItem savableItem = base.Player.EquippedItem.Get();
		if ((bool)savableItem && m_HotbarContainer.HasItem(savableItem))
		{
			base.Player.ChangeEquippedItem.Try(null, arg2: true);
		}
		return m_HotbarContainer.TryRemoveItem(savableItem);
	}

	private void On_Slot_PointerUp(PointerEventData data, Slot displayer)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)displayer != (Object)(object)m_SelectedSlot)
		{
			RaycastResult pointerCurrentRaycast = data.pointerCurrentRaycast;
			if ((Object)(object)((RaycastResult)(ref pointerCurrentRaycast)).gameObject == (Object)(object)((Component)displayer).gameObject)
			{
				TrySelectSlot(m_HotbarSlots.IndexOf(displayer));
			}
		}
	}

	private void On_Slot_Refreshed(Slot slot)
	{
		if ((Object)(object)slot == (Object)(object)m_SelectedSlot)
		{
			TrySelectSlot(m_HotbarSlots.IndexOf(m_SelectedSlot));
		}
	}

	private void Update()
	{
		if (m_SelectByDigits && !base.Player.SelectBuildable.Active && Input.anyKeyDown && int.TryParse(Input.inputString, out var result))
		{
			TrySelectSlot(result - 1);
		}
		if (m_EnableScrolling && !base.Player.SelectBuildable.Active && MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			float num = base.Player.ScrollValue.Get();
			m_CurScrollValue = Mathf.Clamp(m_CurScrollValue + num, 0f - m_ScrollThreeshold, m_ScrollThreeshold);
			if (Mathf.Abs(m_CurScrollValue - m_ScrollThreeshold * Mathf.Sign(num)) < Mathf.Epsilon)
			{
				m_CurScrollValue = 0f;
				m_LastIndex = (int)Mathf.Repeat((float)(m_LastIndex + ((num >= 0f) ? 1 : (-1))), (float)m_HotbarSlots.Count);
				TrySelectSlot(m_LastIndex);
			}
		}
	}

	public bool IsWeapon(ItemData item)
	{
		if (!(item.Category == "Gun") && !(item.Category == "Melee Weapon"))
		{
			return item.Category == "Throwable";
		}
		return true;
	}

	public bool IsConsume(ItemData item)
	{
		return item.Category == "Consumable";
	}

	private void TrySelectSlot(int index)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		index = Mathf.Clamp(index, 0, m_HotbarSlots.Count - 1);
		Slot slot = m_HotbarSlots[index];
		base.Player.ChangeEquippedItem.Try(slot.CurrentItem, arg2: false);
		if (Object.op_Implicit((Object)(object)m_SelectedSlot))
		{
			m_SelectedSlot.SetScale(Vector3.one, 0.2f);
		}
		if (Object.op_Implicit((Object)(object)slot) && slot.HasItem && IsWeapon(slot.ItemHolder.CurrentItem.ItemData))
		{
			((Avatar)KBEngineApp.app.player())?.equipItemRequest(slot.ItemHolder.uuid);
		}
		if (Object.op_Implicit((Object)(object)slot) && !slot.HasItem && Object.op_Implicit((Object)(object)m_SelectedSlot) && m_SelectedSlot.HasItem && IsWeapon(m_SelectedSlot.ItemHolder.CurrentItem.ItemData))
		{
			((Avatar)KBEngineApp.app.player())?.UnEquipItemRequest(m_SelectedSlot.ItemHolder.uuid);
		}
		if (Object.op_Implicit((Object)(object)slot) && slot.HasItem && IsConsume(slot.ItemHolder.CurrentItem.ItemData))
		{
			((Component)UI_MainUI.inst.btn_use).gameObject.SetActive(true);
		}
		else
		{
			((Component)UI_MainUI.inst.btn_use).gameObject.SetActive(false);
		}
		m_SelectedSlot = slot;
		m_SelectedSlot.SetScale(Vector3.one * m_SelectedSlotScale, 0.1f);
		((Behaviour)m_Frame).enabled = true;
		RectTransform component = ((Component)m_Frame).GetComponent<RectTransform>();
		((Transform)component).SetParent(((Component)m_SelectedSlot).transform);
		((Transform)component).localPosition = Vector2.op_Implicit(Vector2.zero);
		component.sizeDelta = ((Component)m_SelectedSlot).GetComponent<RectTransform>().sizeDelta;
		((Transform)component).localScale = Vector3.one;
	}
}
