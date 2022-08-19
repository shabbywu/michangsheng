using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000641 RID: 1601
	public class CharacterEquipmentHandler : MonoBehaviour
	{
		// Token: 0x0600331E RID: 13086 RVA: 0x00167D4C File Offset: 0x00165F4C
		private void Start()
		{
			Slot[] componentsInChildren = base.GetComponentsInChildren<Slot>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ItemHolder.Updated.AddListener(new Action<ItemHolder>(this.On_ItemHolder_Updated));
			}
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x00167D8C File Offset: 0x00165F8C
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

		// Token: 0x04002D55 RID: 11605
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002D56 RID: 11606
		[SerializeField]
		private SoundPlayer m_EquipAudio;
	}
}
