using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class LootObject : InteractableObject, IInventoryTrigger
{
	[SerializeField]
	[Range(1f, 50f)]
	protected int m_Capacity = 8;

	[SerializeField]
	protected ItemToGenerate[] m_InitialItems;

	[Header("Box Opening")]
	[SerializeField]
	private Transform m_Cover;

	[SerializeField]
	private float m_OpenSpeed = 6f;

	[SerializeField]
	private float m_ClosedRotation;

	[SerializeField]
	private float m_OpenRotation = 60f;

	private float m_CurrentRotation;

	public List<ItemHolder> ItemHolders { get; private set; }

	public override void OnInteract(PlayerEventHandler player)
	{
		if (((Behaviour)this).enabled && MonoSingleton<InventoryController>.Instance.OpenLootContainer.Try(this))
		{
			On_InventoryOpened();
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnChanged_InventoryController_State);
		}
	}

	private void Start()
	{
		ItemHolders = new List<ItemHolder>();
		for (int i = 0; i < m_Capacity; i++)
		{
			ItemHolders.Add(new ItemHolder());
			if (i < m_InitialItems.Length && m_InitialItems[i].TryGenerate(out var runtimeItem))
			{
				ItemHolders[i].SetItem(runtimeItem);
			}
		}
	}

	private void OnChanged_InventoryController_State()
	{
		if (MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			On_InventoryClosed();
		}
	}

	private void On_InventoryOpened()
	{
		if ((Object)(object)m_Cover != (Object)null)
		{
			((MonoBehaviour)this).StopAllCoroutines();
			((MonoBehaviour)this).StartCoroutine(C_OpenCover(open: true));
		}
	}

	private void On_InventoryClosed()
	{
		if ((Object)(object)m_Cover != (Object)null)
		{
			((MonoBehaviour)this).StopAllCoroutines();
			((MonoBehaviour)this).StartCoroutine(C_OpenCover(open: false));
		}
	}

	private IEnumerator C_OpenCover(bool open)
	{
		float targetRotation = (open ? m_OpenRotation : m_ClosedRotation);
		while (Mathf.Abs(targetRotation - m_CurrentRotation) > 0.1f)
		{
			m_CurrentRotation = Mathf.Lerp(m_CurrentRotation, targetRotation, Time.deltaTime * m_OpenSpeed);
			((Component)m_Cover).transform.localRotation = Quaternion.Euler(m_CurrentRotation, 0f, 0f);
			yield return null;
		}
	}
}
