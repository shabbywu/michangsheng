using System;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005FF RID: 1535
	public class ItemPickup : InteractableObject
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x0015E75A File Offset: 0x0015C95A
		// (set) Token: 0x06003145 RID: 12613 RVA: 0x0015E762 File Offset: 0x0015C962
		public SavableItem ItemToAdd { get; set; }

		// Token: 0x06003146 RID: 12614 RVA: 0x0015E76C File Offset: 0x0015C96C
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

		// Token: 0x06003147 RID: 12615 RVA: 0x0015E814 File Offset: 0x0015CA14
		private void Awake()
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			ItemData data;
			if (database && database.FindItemByName(this.m_DefaultItem, out data))
			{
				this.ItemToAdd = new SavableItem(data, this.m_DefaultAmount, null);
			}
		}

		// Token: 0x04002B65 RID: 11109
		[SerializeField]
		private ItemPickup.PickupMethod m_PickupMethod;

		// Token: 0x04002B66 RID: 11110
		[SerializeField]
		private string m_DefaultItem;

		// Token: 0x04002B67 RID: 11111
		[SerializeField]
		private int m_DefaultAmount = 1;

		// Token: 0x04002B68 RID: 11112
		[SerializeField]
		private AudioClip m_OnDestroySound;

		// Token: 0x04002B69 RID: 11113
		[SerializeField]
		private float m_OnDestroyVolume = 0.5f;

		// Token: 0x020014C5 RID: 5317
		public enum PickupMethod
		{
			// Token: 0x04006D35 RID: 27957
			WalkOver,
			// Token: 0x04006D36 RID: 27958
			OnInteract
		}
	}
}
