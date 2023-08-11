using System;
using UnityEngine;

namespace UltimateSurvival;

public class CharacterEquipment : MonoBehaviour
{
	[Serializable]
	public class EquipmentPiece
	{
		[SerializeField]
		private string m_ItemName;

		[SerializeField]
		private GameObject m_Object;

		public ItemHolder CorrespondingHolder { get; set; }

		public string ItemName => m_ItemName;

		public GameObject Object => m_Object;
	}

	[SerializeField]
	private EquipmentPiece[] m_EquipmentPieces;

	[SerializeField]
	private GameObject m_Underwear;

	private void Start()
	{
		MonoSingleton<InventoryController>.Instance.EquipmentChanged.AddListener(On_EquipmentChanged);
	}

	private void On_EquipmentChanged(ItemHolder holder)
	{
		EquipmentPiece[] equipmentPieces = m_EquipmentPieces;
		foreach (EquipmentPiece equipmentPiece in equipmentPieces)
		{
			if (holder.HasItem && equipmentPiece.ItemName == holder.CurrentItem.ItemData.Name)
			{
				equipmentPiece.CorrespondingHolder = (holder.HasItem ? holder : null);
				equipmentPiece.Object.SetActive(holder.HasItem);
				if (holder.HasItem && Object.op_Implicit((Object)(object)m_Underwear) && holder.CurrentItem.HasProperty("Disable Underwear"))
				{
					m_Underwear.SetActive(false);
				}
			}
			else if (equipmentPiece.CorrespondingHolder == holder)
			{
				equipmentPiece.CorrespondingHolder = null;
				equipmentPiece.Object.SetActive(false);
			}
		}
		HandleUnderwear(holder);
	}

	private void HandleUnderwear(ItemHolder holder)
	{
		if (!Object.op_Implicit((Object)(object)m_Underwear) || m_Underwear.activeSelf)
		{
			return;
		}
		bool flag = true;
		EquipmentPiece[] equipmentPieces = m_EquipmentPieces;
		foreach (EquipmentPiece equipmentPiece in equipmentPieces)
		{
			if ((bool)equipmentPiece.CorrespondingHolder && equipmentPiece.CorrespondingHolder.HasItem && equipmentPiece.CorrespondingHolder.CurrentItem.HasProperty("Disable Underwear"))
			{
				flag = false;
			}
		}
		if (flag)
		{
			m_Underwear.SetActive(true);
		}
	}
}
