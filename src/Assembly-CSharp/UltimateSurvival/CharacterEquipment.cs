using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008FB RID: 2299
	public class CharacterEquipment : MonoBehaviour
	{
		// Token: 0x06003AE6 RID: 15078 RVA: 0x0002AC2C File Offset: 0x00028E2C
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.EquipmentChanged.AddListener(new Action<ItemHolder>(this.On_EquipmentChanged));
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x001AA908 File Offset: 0x001A8B08
		private void On_EquipmentChanged(ItemHolder holder)
		{
			foreach (CharacterEquipment.EquipmentPiece equipmentPiece in this.m_EquipmentPieces)
			{
				if (holder.HasItem && equipmentPiece.ItemName == holder.CurrentItem.ItemData.Name)
				{
					equipmentPiece.CorrespondingHolder = (holder.HasItem ? holder : null);
					equipmentPiece.Object.SetActive(holder.HasItem);
					if (holder.HasItem && this.m_Underwear && holder.CurrentItem.HasProperty("Disable Underwear"))
					{
						this.m_Underwear.SetActive(false);
					}
				}
				else if (equipmentPiece.CorrespondingHolder == holder)
				{
					equipmentPiece.CorrespondingHolder = null;
					equipmentPiece.Object.SetActive(false);
				}
			}
			this.HandleUnderwear(holder);
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x001AA9D4 File Offset: 0x001A8BD4
		private void HandleUnderwear(ItemHolder holder)
		{
			if (!this.m_Underwear || this.m_Underwear.activeSelf)
			{
				return;
			}
			bool flag = true;
			foreach (CharacterEquipment.EquipmentPiece equipmentPiece in this.m_EquipmentPieces)
			{
				if (equipmentPiece.CorrespondingHolder && equipmentPiece.CorrespondingHolder.HasItem && equipmentPiece.CorrespondingHolder.CurrentItem.HasProperty("Disable Underwear"))
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.m_Underwear.SetActive(true);
			}
		}

		// Token: 0x04003531 RID: 13617
		[SerializeField]
		private CharacterEquipment.EquipmentPiece[] m_EquipmentPieces;

		// Token: 0x04003532 RID: 13618
		[SerializeField]
		private GameObject m_Underwear;

		// Token: 0x020008FC RID: 2300
		[Serializable]
		public class EquipmentPiece
		{
			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x06003AEA RID: 15082 RVA: 0x0002AC49 File Offset: 0x00028E49
			// (set) Token: 0x06003AEB RID: 15083 RVA: 0x0002AC51 File Offset: 0x00028E51
			public ItemHolder CorrespondingHolder { get; set; }

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06003AEC RID: 15084 RVA: 0x0002AC5A File Offset: 0x00028E5A
			public string ItemName
			{
				get
				{
					return this.m_ItemName;
				}
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06003AED RID: 15085 RVA: 0x0002AC62 File Offset: 0x00028E62
			public GameObject Object
			{
				get
				{
					return this.m_Object;
				}
			}

			// Token: 0x04003534 RID: 13620
			[SerializeField]
			private string m_ItemName;

			// Token: 0x04003535 RID: 13621
			[SerializeField]
			private GameObject m_Object;
		}
	}
}
