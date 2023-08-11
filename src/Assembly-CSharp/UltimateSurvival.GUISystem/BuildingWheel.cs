using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class BuildingWheel : GUIBehaviour
{
	[SerializeField]
	private Window m_Window;

	[SerializeField]
	private Camera m_GUICamera;

	[SerializeField]
	private RectTransform m_SelectionHighlight;

	[SerializeField]
	private Text m_CategoryName;

	[SerializeField]
	private Text m_PieceName;

	[SerializeField]
	[Range(0f, 50f)]
	private float m_ScrollThreeshold = 1f;

	[Header("Audio")]
	[SerializeField]
	private SoundPlayer m_RefreshAudio;

	[SerializeField]
	private SoundPlayer m_SelectPieceAudio;

	[Header("Layout")]
	[SerializeField]
	private float m_Distance = 211.7f;

	[SerializeField]
	[Range(-90f, 90f)]
	private float m_Offset;

	[SerializeField]
	[Range(-90f, 90f)]
	private float m_Spacing;

	private BuildingCategory[] m_Categories;

	private BuildingCategory m_SelectedCategory;

	private int m_CategoryIndex;

	private bool m_ChoosingPiece;

	private BuildingPiece m_SelectedPiece;

	private BuildingPiece m_HighlightedPiece;

	private float m_CategoryScrollPos;

	private float m_PieceScrollPos;

	public RectTransform SelectionHighlight => m_SelectionHighlight;

	public float Distance => m_Distance;

	public float Offset => m_Offset;

	public float Spacing => m_Spacing;

	private void Update()
	{
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		if (!m_Window.IsOpen)
		{
			return;
		}
		float num = base.Player.ScrollValue.Get();
		if ((Object)(object)m_SelectedCategory != (Object)null)
		{
			if (m_ChoosingPiece && (Object)(object)m_HighlightedPiece != (Object)null)
			{
				if (!((Behaviour)m_PieceName).enabled)
				{
					((Behaviour)m_PieceName).enabled = true;
				}
				m_PieceName.text = m_HighlightedPiece.PieceName;
			}
			else if (!m_ChoosingPiece)
			{
				if (!((Behaviour)m_PieceName).enabled)
				{
					((Behaviour)m_PieceName).enabled = true;
				}
				m_PieceName.text = (((Object)(object)m_SelectedPiece == (Object)null) ? "" : m_SelectedPiece.PieceName);
			}
			if (Input.GetKeyDown((KeyCode)323))
			{
				if (m_SelectedCategory.CategoryName == "None")
				{
					base.Player.SelectedBuildable.Set(null);
					base.Player.SelectBuildable.TryStop();
					((Behaviour)m_PieceName).enabled = false;
					return;
				}
				if (m_ChoosingPiece)
				{
					m_SelectedPiece = m_HighlightedPiece;
					base.Player.SelectedBuildable.Set(m_SelectedPiece.BuildableObject);
					m_SelectPieceAudio.Play2D();
				}
				m_ChoosingPiece = !m_ChoosingPiece;
			}
			if (m_ChoosingPiece)
			{
				if (!m_SelectedCategory.ShowPieces)
				{
					m_SelectedCategory.ShowPieces = true;
					m_PieceScrollPos = 0f;
					m_HighlightedPiece = m_SelectedCategory.SelectFirst();
					return;
				}
				m_PieceScrollPos += num;
				m_PieceScrollPos = Mathf.Clamp(m_PieceScrollPos, 0f - m_ScrollThreeshold, m_ScrollThreeshold);
				if (Mathf.Abs(m_PieceScrollPos - m_ScrollThreeshold * Mathf.Sign(num)) < Mathf.Epsilon)
				{
					m_PieceScrollPos = 0f;
					if (num > 0f)
					{
						m_HighlightedPiece = m_SelectedCategory.SelectNext();
					}
					else
					{
						m_HighlightedPiece = m_SelectedCategory.SelectPrevious();
					}
				}
				return;
			}
			if (m_SelectedCategory.ShowPieces)
			{
				m_SelectedCategory.ShowPieces = false;
			}
		}
		m_CategoryScrollPos += num;
		m_CategoryScrollPos = Mathf.Clamp(m_CategoryScrollPos, 0f - m_ScrollThreeshold, m_ScrollThreeshold);
		BuildingCategory selectedCategory = m_SelectedCategory;
		if (Mathf.Abs(m_CategoryScrollPos - m_ScrollThreeshold * Mathf.Sign(num)) < Mathf.Epsilon)
		{
			m_CategoryScrollPos = 0f;
			m_CategoryIndex = (int)Mathf.Repeat((float)(m_CategoryIndex + ((num > 0f) ? 1 : (-1))), (float)m_Categories.Length);
			m_SelectedCategory = m_Categories[m_CategoryIndex];
		}
		if ((Object)(object)selectedCategory != (Object)(object)m_SelectedCategory)
		{
			m_Window.Refresh();
			m_RefreshAudio.Play2D();
			m_CategoryName.text = m_SelectedCategory.CategoryName;
		}
		if ((Object)(object)m_SelectedCategory != (Object)null)
		{
			float num2 = Offset + Spacing * (float)m_CategoryIndex;
			((Transform)m_SelectionHighlight).localPosition = Quaternion.Euler(Vector3.back * num2) * Vector3.up * Distance;
			((Transform)m_SelectionHighlight).localRotation = Quaternion.Euler(Vector3.back * num2);
		}
	}

	private void Start()
	{
		m_Categories = ((Component)this).GetComponentsInChildren<BuildingCategory>(false);
		base.Player.SelectBuildable.AddStartTryer(TryStart_SelectBuildable);
		base.Player.SelectBuildable.AddStopTryer(TryStop_SelectBuildable);
		MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnChanged_InventoryState);
		BuildingCategory[] categories = m_Categories;
		for (int i = 0; i < categories.Length; i++)
		{
			categories[i].ShowPieces = false;
		}
		((Behaviour)m_PieceName).enabled = false;
	}

	private void OnChanged_InventoryState()
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			while (base.Player.SelectBuildable.Active)
			{
				base.Player.SelectBuildable.TryStop();
			}
		}
	}

	private bool TryStart_SelectBuildable()
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed || !base.Player.EquippedItem.Get() || !base.Player.EquippedItem.Get().HasProperty("Allows Building"))
		{
			return false;
		}
		m_Window.Open();
		base.Player.ViewLocked.Set(value: true);
		return true;
	}

	private bool TryStop_SelectBuildable()
	{
		if (m_ChoosingPiece)
		{
			m_ChoosingPiece = false;
			return false;
		}
		m_Window.Close();
		base.Player.ViewLocked.Set(value: false);
		return true;
	}
}
