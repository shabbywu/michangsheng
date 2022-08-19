using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000656 RID: 1622
	public class LootContainerOpener : MonoBehaviour
	{
		// Token: 0x0600339D RID: 13213 RVA: 0x0016A5D6 File Offset: 0x001687D6
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.OpenLootContainer.SetTryer(new Attempt<LootObject>.GenericTryerDelegate(this.Try_OpenLootContainer));
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x0016A5F3 File Offset: 0x001687F3
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

		// Token: 0x0600339F RID: 13215 RVA: 0x0016A62E File Offset: 0x0016882E
		private void OnChanged_InventoryState()
		{
			if (this.m_CurLootObject && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurLootObject = null;
			}
		}

		// Token: 0x04002DE5 RID: 11749
		[SerializeField]
		private ItemContainer m_ItemContainer;

		// Token: 0x04002DE6 RID: 11750
		private LootObject m_CurLootObject;
	}
}
