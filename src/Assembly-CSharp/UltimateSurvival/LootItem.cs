using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008E1 RID: 2273
	[Serializable]
	public class LootItem
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x0002A74C File Offset: 0x0002894C
		public string ItemName
		{
			get
			{
				return this.m_ItemName;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x0002A754 File Offset: 0x00028954
		public float SpawnChance
		{
			get
			{
				return this.m_SpawnChance;
			}
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x001A8024 File Offset: 0x001A6224
		public void AddToInventory(out int added, float amountFactor)
		{
			added = 0;
			int num = Mathf.CeilToInt((float)Random.Range(this.m_MinAmount, this.m_MaxAmount) * amountFactor);
			if (num > 0)
			{
				MonoSingleton<InventoryController>.Instance.AddItemToCollection(this.m_ItemName, num, "Inventory", out added);
			}
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x001A806C File Offset: 0x001A626C
		public GameObject CreatePickup(Vector3 position, Quaternion rotation)
		{
			ItemData itemData;
			if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(this.m_ItemName, out itemData) && itemData.WorldObject)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(itemData.WorldObject, position, rotation);
				ItemPickup component = gameObject.GetComponent<ItemPickup>();
				if (component)
				{
					component.ItemToAdd.CurrentInStack = Random.Range(this.m_MinAmount, this.m_MaxAmount);
				}
				return gameObject;
			}
			return null;
		}

		// Token: 0x04003477 RID: 13431
		[SerializeField]
		private string m_ItemName;

		// Token: 0x04003478 RID: 13432
		[SerializeField]
		[Range(0f, 100f)]
		private float m_SpawnChance;

		// Token: 0x04003479 RID: 13433
		[SerializeField]
		private int m_MinAmount;

		// Token: 0x0400347A RID: 13434
		[SerializeField]
		private int m_MaxAmount;
	}
}
