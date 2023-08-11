using System;
using KBEngine;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival.Building;

public class PlayerBuilder : MonoBehaviour
{
	[SerializeField]
	private BuildingHelpers m_BuildingHelpers;

	[SerializeField]
	[Tooltip("How fast the player can rotate buildables.")]
	private float m_RotationSpeed = 120f;

	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	[Tooltip("A shake that will be played when the player places an object.")]
	private GenericShake m_PlaceShake;

	private float m_NextTimeCanPlayAudio;

	private PlayerEventHandler m_Player;

	private ItemContainer m_InventoryContainer;

	private BuildingPiece m_SelectedPiece;

	private void Start()
	{
		m_Player = GameController.LocalPlayer;
		m_BuildingHelpers.Initialize(((Component)this).transform, m_Player, m_AudioSource);
		m_Player.PlaceObject.SetTryer(Try_Place);
		m_Player.PlaceObject.AddListener(m_BuildingHelpers.PlacePiece);
		m_Player.RotateObject.SetTryer(Try_RotateObject);
		m_Player.EquippedItem.AddChangeListener(OnChanged_EquippedItem);
		m_Player.SelectedBuildable.AddChangeListener(OnChanged_SelectedBuildable);
		m_InventoryContainer = MonoSingleton<GUIController>.Instance.GetContainer("Inventory");
	}

	private void OnChanged_EquippedItem()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		SavableItem savableItem = m_Player.EquippedItem.Get();
		if (savableItem != null && savableItem.HasProperty("Is Placeable"))
		{
			GameObject val = Object.Instantiate<GameObject>(savableItem.ItemData.WorldObject);
			val.transform.position = Vector3.one * 10000f;
			m_Player.SelectedBuildable.Set(val.GetComponent<BuildingPiece>());
		}
		if (savableItem != null && savableItem.HasProperty("Allows Building"))
		{
			m_Player.SelectedBuildable.Set(m_SelectedPiece);
		}
		UpdatePreview();
	}

	private void OnChanged_SelectedBuildable()
	{
		if (m_Player.EquippedItem.Get() != null && m_Player.EquippedItem.Get().HasProperty("Allows Building"))
		{
			m_SelectedPiece = m_Player.SelectedBuildable.Get();
		}
		UpdatePreview();
	}

	private void UpdatePreview()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (m_BuildingHelpers.PreviewExists())
		{
			m_BuildingHelpers.ClearPreview();
		}
		BuildingPiece buildingPiece = m_Player.SelectedBuildable.Get();
		if ((Object)(object)buildingPiece != (Object)null && m_Player.EquippedItem.Get() != null && (m_Player.EquippedItem.Get().HasProperty("Allows Building") || m_Player.EquippedItem.Get().HasProperty("Is Placeable")))
		{
			m_BuildingHelpers.SpawnPreview(((Component)buildingPiece).gameObject);
			m_BuildingHelpers.PreviewColor = m_BuildingHelpers.CurrentPreviewPiece.Renderers[0].material.color;
		}
		else
		{
			m_BuildingHelpers.ClearPreview();
		}
	}

	private bool Try_RotateObject(float rotationSign)
	{
		if (m_Player.ViewLocked.Is(value: false) && m_BuildingHelpers.PreviewExists())
		{
			m_BuildingHelpers.RotationOffset += m_RotationSpeed * rotationSign;
			return true;
		}
		return false;
	}

	private bool Try_Place()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		if (!m_BuildingHelpers.PreviewExists() || !m_BuildingHelpers.PlacementAllowed)
		{
			return false;
		}
		if (m_Player.EquippedItem.Get().HasProperty("Is Placeable"))
		{
			MonoSingleton<InventoryController>.Instance.Database.FindItemByName(m_BuildingHelpers.CurrentPreviewPiece.Name, out var _);
			ulong itemUUID = ((Component)((Component)MonoSingleton<GUIController>.Instance.GetContainer("Hotbar")).gameObject.GetComponent<UltimateSurvival.GUISystem.Hotbar>().SelectedSlot).gameObject.GetComponent<ItemOnObject>().item.itemUUID;
			double num = (double)m_BuildingHelpers.CurrentPreview.transform.eulerAngles.x / 360.0 * (Math.PI * 2.0);
			double num2 = (double)m_BuildingHelpers.CurrentPreview.transform.eulerAngles.z / 360.0 * (Math.PI * 2.0);
			double num3 = (double)m_BuildingHelpers.CurrentPreview.transform.eulerAngles.y / 360.0 * (Math.PI * 2.0);
			if (num - Math.PI > 0.0)
			{
				num -= Math.PI * 2.0;
			}
			if (num2 - Math.PI > 0.0)
			{
				num2 -= Math.PI * 2.0;
			}
			if (num3 - Math.PI > 0.0)
			{
				num3 -= Math.PI * 2.0;
			}
			((Avatar)KBEngineApp.app.player()).createBuild(itemUUID, m_BuildingHelpers.CurrentPreview.transform.position, new Vector3((float)num, (float)num2, (float)num3));
			return false;
		}
		if (HasRequiredItems())
		{
			m_BuildingHelpers.CurrentPreviewPiece.BuildAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, m_AudioSource);
			m_PlaceShake.Shake(1f);
			for (int i = 0; i < m_BuildingHelpers.CurrentPreviewPiece.RequiredItems.Length; i++)
			{
				RequiredItem requiredItem = m_BuildingHelpers.CurrentPreviewPiece.RequiredItems[i];
				m_InventoryContainer.RemoveItems(requiredItem.Name, requiredItem.Amount);
			}
			return true;
		}
		string text = "You don't have all the required materials: \n";
		for (int j = 0; j < m_BuildingHelpers.CurrentPreviewPiece.RequiredItems.Length; j++)
		{
			RequiredItem requiredItem2 = m_BuildingHelpers.CurrentPreviewPiece.RequiredItems[j];
			text += $"<color=yellow>{requiredItem2.Name}</color> x {requiredItem2.Amount}, ";
		}
		MonoSingleton<MessageDisplayer>.Instance.PushMessage(text, default(Color), 48);
		return false;
	}

	private bool HasRequiredItems()
	{
		for (int i = 0; i < m_BuildingHelpers.CurrentPreviewPiece.RequiredItems.Length; i++)
		{
			RequiredItem requiredItem = m_BuildingHelpers.CurrentPreviewPiece.RequiredItems[i];
			if (m_InventoryContainer.GetItemCount(requiredItem.Name) < requiredItem.Amount)
			{
				return false;
			}
		}
		return true;
	}

	private void Update()
	{
		m_BuildingHelpers.HasSocket = false;
		if (m_BuildingHelpers.PreviewExists())
		{
			m_BuildingHelpers.LookForSnaps();
		}
		if (m_BuildingHelpers.PreviewExists())
		{
			m_BuildingHelpers.ManagePreview();
		}
	}

	public bool CanPlace()
	{
		return m_BuildingHelpers.PlacementAllowed;
	}
}
