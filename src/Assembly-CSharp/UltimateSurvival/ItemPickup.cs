using System;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008DA RID: 2266
	public class ItemPickup : InteractableObject
	{
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x0002A581 File Offset: 0x00028781
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x0002A589 File Offset: 0x00028789
		public SavableItem ItemToAdd { get; set; }

		// Token: 0x06003A49 RID: 14921 RVA: 0x001A7C94 File Offset: 0x001A5E94
		public override void OnInteract(PlayerEventHandler player)
		{
			if (this.m_PickupMethod == ItemPickup.PickupMethod.WalkOver || !this.ItemToAdd)
			{
				return;
			}
			if (MonoSingleton<GUIController>.Instance.GetContainer("Inventory").TryAddItem(this.ItemToAdd))
			{
				if (this.m_OnDestroySound)
				{
					GameController.Audio.Play2D(this.m_OnDestroySound, this.m_OnDestroyVolume);
				}
				MonoSingleton<MessageDisplayer>.Instance.PushMessage(string.Format("Picked up <color=yellow>{0}</color> x {1}", this.ItemToAdd.Name, this.ItemToAdd.CurrentInStack), default(Color), 16);
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x001A7D3C File Offset: 0x001A5F3C
		private void Awake()
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			ItemData data;
			if (database && database.FindItemByName(this.m_DefaultItem, out data))
			{
				this.ItemToAdd = new SavableItem(data, this.m_DefaultAmount, null);
			}
		}

		// Token: 0x04003457 RID: 13399
		[SerializeField]
		private ItemPickup.PickupMethod m_PickupMethod;

		// Token: 0x04003458 RID: 13400
		[SerializeField]
		private string m_DefaultItem;

		// Token: 0x04003459 RID: 13401
		[SerializeField]
		private int m_DefaultAmount = 1;

		// Token: 0x0400345A RID: 13402
		[SerializeField]
		private AudioClip m_OnDestroySound;

		// Token: 0x0400345B RID: 13403
		[SerializeField]
		private float m_OnDestroyVolume = 0.5f;

		// Token: 0x020008DB RID: 2267
		public enum PickupMethod
		{
			// Token: 0x0400345D RID: 13405
			WalkOver,
			// Token: 0x0400345E RID: 13406
			OnInteract
		}
	}
}
