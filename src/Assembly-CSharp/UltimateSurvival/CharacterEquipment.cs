using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000616 RID: 1558
	public class CharacterEquipment : MonoBehaviour
	{
		// Token: 0x060031BD RID: 12733 RVA: 0x00160F98 File Offset: 0x0015F198
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.EquipmentChanged.AddListener(new Action<ItemHolder>(this.On_EquipmentChanged));
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x00160FB8 File Offset: 0x0015F1B8
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

		// Token: 0x060031BF RID: 12735 RVA: 0x00161084 File Offset: 0x0015F284
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

		// Token: 0x04002C13 RID: 11283
		[SerializeField]
		private CharacterEquipment.EquipmentPiece[] m_EquipmentPieces;

		// Token: 0x04002C14 RID: 11284
		[SerializeField]
		private GameObject m_Underwear;

		// Token: 0x020014CF RID: 5327
		[Serializable]
		public class EquipmentPiece
		{
			// Token: 0x17000B00 RID: 2816
			// (get) Token: 0x06008205 RID: 33285 RVA: 0x002DA52A File Offset: 0x002D872A
			// (set) Token: 0x06008206 RID: 33286 RVA: 0x002DA532 File Offset: 0x002D8732
			public ItemHolder CorrespondingHolder { get; set; }

			// Token: 0x17000B01 RID: 2817
			// (get) Token: 0x06008207 RID: 33287 RVA: 0x002DA53B File Offset: 0x002D873B
			public string ItemName
			{
				get
				{
					return this.m_ItemName;
				}
			}

			// Token: 0x17000B02 RID: 2818
			// (get) Token: 0x06008208 RID: 33288 RVA: 0x002DA543 File Offset: 0x002D8743
			public GameObject Object
			{
				get
				{
					return this.m_Object;
				}
			}

			// Token: 0x04006D61 RID: 28001
			[SerializeField]
			private string m_ItemName;

			// Token: 0x04006D62 RID: 28002
			[SerializeField]
			private GameObject m_Object;
		}
	}
}
