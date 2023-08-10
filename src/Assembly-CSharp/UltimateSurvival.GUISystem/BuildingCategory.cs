using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class BuildingCategory : MonoBehaviour
{
	[SerializeField]
	private string m_CategoryName;

	[SerializeField]
	private Vector2 m_DesiredOffset;

	[SerializeField]
	[Range(0.5f, 2f)]
	private float m_SelectionScale = 1.1f;

	[Header("Layout")]
	[SerializeField]
	private float m_Distance = 211.7f;

	[SerializeField]
	[Range(-90f, 90f)]
	private float m_Offset;

	[SerializeField]
	[Range(-90f, 90f)]
	private float m_Spacing;

	private BuildingPiece[] m_Pieces;

	private bool m_ShowPieces;

	private BuildingPiece m_HighlightedPiece;

	private float m_ClosestPieceAngle;

	private int m_CurrentIndex;

	public string CategoryName => m_CategoryName;

	public Vector2 DesiredOffset => m_DesiredOffset;

	public bool ShowPieces
	{
		get
		{
			return m_ShowPieces;
		}
		set
		{
			BuildingPiece[] pieces = m_Pieces;
			for (int i = 0; i < pieces.Length; i++)
			{
				((Component)pieces[i]).gameObject.SetActive(value);
			}
			m_ShowPieces = value;
		}
	}

	public float Distance => m_Distance;

	public float Offset => m_Offset;

	public float Spacing => m_Spacing;

	public BuildingPiece SelectFirst()
	{
		if (m_Pieces.Length != 0)
		{
			Select(0);
			return m_HighlightedPiece;
		}
		return null;
	}

	public BuildingPiece SelectNext()
	{
		Select(m_CurrentIndex + 1);
		return m_HighlightedPiece;
	}

	public BuildingPiece SelectPrevious()
	{
		Select(m_CurrentIndex - 1);
		return m_HighlightedPiece;
	}

	private void Select(int index)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		m_CurrentIndex = (int)Mathf.Repeat((float)index, (float)m_Pieces.Length);
		m_HighlightedPiece = m_Pieces[m_CurrentIndex];
		for (int i = 0; i < m_Pieces.Length; i++)
		{
			if ((Object)(object)m_Pieces[i] == (Object)(object)m_HighlightedPiece)
			{
				((Component)m_Pieces[i]).transform.localScale = m_SelectionScale * Vector3.one;
				m_Pieces[i].SetCustomColor(new Color(0f, 1f, 0f, 0.85f));
			}
			else
			{
				((Component)m_Pieces[i]).transform.localScale = Vector3.one;
				m_Pieces[i].SetDefaultColor();
			}
		}
	}

	private void Awake()
	{
		m_Pieces = ((Component)this).GetComponentsInChildren<BuildingPiece>();
	}
}
