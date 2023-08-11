using KBEngine;
using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class CharacterEquipmentHandler : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_EquipAudio;

	private void Start()
	{
		Slot[] componentsInChildren = ((Component)this).GetComponentsInChildren<Slot>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].ItemHolder.Updated.AddListener(On_ItemHolder_Updated);
		}
	}

	private void On_ItemHolder_Updated(ItemHolder holder)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		MonoSingleton<InventoryController>.Instance.EquipmentChanged.Send(holder);
		if (holder.HasItem)
		{
			EquipmentSystem component = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
			if (!((Object)(object)component != (Object)null))
			{
				return;
			}
			Item itemByID = World.inventoryItemList.getItemByID(holder.itemID);
			for (int i = 0; i < component.slotsInTotal; i++)
			{
				if (component.itemTypeOfSlots[i].Equals(itemByID.itemType))
				{
					((Avatar)KBEngineApp.app.player())?.equipItemRequest(holder.uuid);
					break;
				}
			}
		}
		else if (((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().getFirstEmptyItemIndex() >= 0)
		{
			((Avatar)KBEngineApp.app.player())?.UnEquipItemRequest(holder.uuid);
		}
	}
}
