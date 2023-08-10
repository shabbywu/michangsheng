using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class GUIController : MonoSingleton<GUIController>
{
	[Header("Setup")]
	[SerializeField]
	private Canvas m_Canvas;

	[SerializeField]
	private Camera m_GUICamera;

	[SerializeField]
	private Font m_Font;

	[SerializeField]
	[Reorderable]
	[Tooltip("If the player clicks while on those rects, the current selection will not be lost.")]
	private ReorderableRectTransformList m_SelectionBlockers;

	[Header("Audio")]
	[SerializeField]
	private AudioClip m_InventoryOpenClip;

	[SerializeField]
	private AudioClip m_InventoryCloseClip;

	public PlayerEventHandler Player { get; private set; }

	public Canvas Canvas => m_Canvas;

	public ItemContainer[] Containers { get; private set; }

	public Font Font => m_Font;

	public ItemContainer GetContainer(string name)
	{
		for (int i = 0; i < Containers.Length; i++)
		{
			if (Containers[i].Name == name)
			{
				return Containers[i];
			}
		}
		Debug.LogWarning((object)("No container with the name " + name + " found!"));
		return null;
	}

	public bool MouseOverSelectionKeeper()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_SelectionBlockers.Count; i++)
		{
			if (((Component)m_SelectionBlockers[i]).gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(m_SelectionBlockers[i], Vector2.op_Implicit(Input.mousePosition), m_GUICamera))
			{
				return true;
			}
		}
		return false;
	}

	public void ApplyForAllCollections()
	{
		ItemContainer[] componentsInChildren = ((Component)this).GetComponentsInChildren<ItemContainer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].ApplyAll();
		}
	}

	private void Awake()
	{
		Containers = ((Component)this).GetComponentsInChildren<ItemContainer>(true);
		Player = GameController.LocalPlayer;
	}

	private void Start()
	{
		MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnChanged_InventoryState);
	}

	private void OnChanged_InventoryState()
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			GameController.Audio.Play2D(m_InventoryOpenClip, 0.6f);
		}
		else
		{
			GameController.Audio.Play2D(m_InventoryCloseClip, 0.6f);
		}
	}
}
