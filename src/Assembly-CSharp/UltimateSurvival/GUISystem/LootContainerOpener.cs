using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000958 RID: 2392
	public class LootContainerOpener : MonoBehaviour
	{
		// Token: 0x06003D25 RID: 15653 RVA: 0x0002C07E File Offset: 0x0002A27E
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.OpenLootContainer.SetTryer(new Attempt<LootObject>.GenericTryerDelegate(this.Try_OpenLootContainer));
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x0002C09B File Offset: 0x0002A29B
		private bool Try_OpenLootContainer(LootObject lootObject)
		{
			if (MonoSingleton<InventoryController>.Instance.IsClosed && MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Loot))
			{
				this.m_CurLootObject = lootObject;
				this.m_ItemContainer.Setup(lootObject.ItemHolders);
				return true;
			}
			return false;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x0002C0D6 File Offset: 0x0002A2D6
		private void OnChanged_InventoryState()
		{
			if (this.m_CurLootObject && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurLootObject = null;
			}
		}

		// Token: 0x04003766 RID: 14182
		[SerializeField]
		private ItemContainer m_ItemContainer;

		// Token: 0x04003767 RID: 14183
		private LootObject m_CurLootObject;
	}
}
