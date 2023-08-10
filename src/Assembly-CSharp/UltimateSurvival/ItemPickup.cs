using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival;

public class ItemPickup : InteractableObject
{
	public enum PickupMethod
	{
		WalkOver,
		OnInteract
	}

	[SerializeField]
	private PickupMethod m_PickupMethod;

	[SerializeField]
	private string m_DefaultItem;

	[SerializeField]
	private int m_DefaultAmount = 1;

	[SerializeField]
	private AudioClip m_OnDestroySound;

	[SerializeField]
	private float m_OnDestroyVolume = 0.5f;

	public SavableItem ItemToAdd { get; set; }

	public override void OnInteract(PlayerEventHandler player)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (m_PickupMethod == PickupMethod.WalkOver || !ItemToAdd)
		{
			return;
		}
		if (MonoSingleton<GUIController>.Instance.GetContainer("Inventory").TryAddItem(ItemToAdd))
		{
			if (Object.op_Implicit((Object)(object)m_OnDestroySound))
			{
				GameController.Audio.Play2D(m_OnDestroySound, m_OnDestroyVolume);
			}
			MonoSingleton<MessageDisplayer>.Instance.PushMessage($"Picked up <color=yellow>{ItemToAdd.Name}</color> x {ItemToAdd.CurrentInStack}");
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void Awake()
	{
		ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
		if (Object.op_Implicit((Object)(object)database) && database.FindItemByName(m_DefaultItem, out var itemData))
		{
			ItemToAdd = new SavableItem(itemData, m_DefaultAmount);
		}
	}
}
