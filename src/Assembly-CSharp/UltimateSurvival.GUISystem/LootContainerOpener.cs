using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class LootContainerOpener : MonoBehaviour
{
	[SerializeField]
	private ItemContainer m_ItemContainer;

	private LootObject m_CurLootObject;

	private void Start()
	{
		MonoSingleton<InventoryController>.Instance.OpenLootContainer.SetTryer(Try_OpenLootContainer);
	}

	private bool Try_OpenLootContainer(LootObject lootObject)
	{
		if (MonoSingleton<InventoryController>.Instance.IsClosed && MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Loot))
		{
			m_CurLootObject = lootObject;
			m_ItemContainer.Setup(lootObject.ItemHolders);
			return true;
		}
		return false;
	}

	private void OnChanged_InventoryState()
	{
		if (Object.op_Implicit((Object)(object)m_CurLootObject) && MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			m_CurLootObject = null;
		}
	}
}
