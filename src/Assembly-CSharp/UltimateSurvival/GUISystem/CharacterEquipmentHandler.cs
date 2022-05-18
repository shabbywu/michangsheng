using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200093B RID: 2363
	public class CharacterEquipmentHandler : MonoBehaviour
	{
		// Token: 0x06003C7A RID: 15482 RVA: 0x001B0D94 File Offset: 0x001AEF94
		private void Start()
		{
			Slot[] componentsInChildren = base.GetComponentsInChildren<Slot>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ItemHolder.Updated.AddListener(new Action<ItemHolder>(this.On_ItemHolder_Updated));
			}
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x001B0DD4 File Offset: 0x001AEFD4
		private void On_ItemHolder_Updated(ItemHolder holder)
		{
			MonoSingleton<InventoryController>.Instance.EquipmentChanged.Send(holder);
			if (holder.HasItem)
			{
				EquipmentSystem component = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
				if (component != null)
				{
					Item itemByID = World.inventoryItemList.getItemByID(holder.itemID);
					int i = 0;
					while (i < component.slotsInTotal)
					{
						if (component.itemTypeOfSlots[i].Equals(itemByID.itemType))
						{
							Avatar avatar = (Avatar)KBEngineApp.app.player();
							if (avatar != null)
							{
								avatar.equipItemRequest(holder.uuid);
								return;
							}
							return;
						}
						else
						{
							i++;
						}
					}
					return;
				}
			}
			else if (((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().getFirstEmptyItemIndex() >= 0)
			{
				Avatar avatar2 = (Avatar)KBEngineApp.app.player();
				if (avatar2 != null)
				{
					avatar2.UnEquipItemRequest(holder.uuid);
				}
			}
		}

		// Token: 0x040036BE RID: 14014
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x040036BF RID: 14015
		[SerializeField]
		private SoundPlayer m_EquipAudio;
	}
}
