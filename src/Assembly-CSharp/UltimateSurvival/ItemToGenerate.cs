using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000601 RID: 1537
	[Serializable]
	public class ItemToGenerate
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x0015E9BC File Offset: 0x0015CBBC
		public bool TryGenerate(out SavableItem runtimeItem)
		{
			runtimeItem = null;
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			ItemData itemData;
			if (this.m_Random)
			{
				if (database.FindItemById(Random.Range(0, database.GetItemCount() - 1), out itemData))
				{
					runtimeItem = new SavableItem(itemData, (int)((float)itemData.StackSize * 0.1f) + 1, null);
					return true;
				}
			}
			else if (database.FindItemByName(this.m_CustomName, out itemData))
			{
				runtimeItem = new SavableItem(itemData, this.m_StackSize, null);
				return true;
			}
			return false;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x0015EA34 File Offset: 0x0015CC34
		public ItemData GenerateItemData()
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			ItemData result = null;
			if (this.m_Random)
			{
				database.FindItemById(Random.Range(0, database.GetItemCount() - 1), out result);
			}
			else
			{
				database.FindItemByName(this.m_CustomName, out result);
			}
			return result;
		}

		// Token: 0x04002B6E RID: 11118
		[SerializeField]
		private bool m_Random;

		// Token: 0x04002B6F RID: 11119
		[SerializeField]
		private string m_CustomName;

		// Token: 0x04002B70 RID: 11120
		[SerializeField]
		[Clamp(1f, 9999999f)]
		private int m_StackSize = 1;
	}
}
