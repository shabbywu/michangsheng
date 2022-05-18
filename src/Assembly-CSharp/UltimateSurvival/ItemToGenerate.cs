using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008DE RID: 2270
	[Serializable]
	public class ItemToGenerate
	{
		// Token: 0x06003A52 RID: 14930 RVA: 0x001A7E34 File Offset: 0x001A6034
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

		// Token: 0x06003A53 RID: 14931 RVA: 0x001A7EAC File Offset: 0x001A60AC
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

		// Token: 0x04003467 RID: 13415
		[SerializeField]
		private bool m_Random;

		// Token: 0x04003468 RID: 13416
		[SerializeField]
		private string m_CustomName;

		// Token: 0x04003469 RID: 13417
		[SerializeField]
		[Clamp(1f, 9999999f)]
		private int m_StackSize = 1;
	}
}
