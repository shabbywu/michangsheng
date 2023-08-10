using UltimateSurvival.Building;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class BuildingPiece : MonoBehaviour
{
	[SerializeField]
	private string m_PieceName;

	[SerializeField]
	private Vector2 m_DesiredOffset;

	[SerializeField]
	private UltimateSurvival.Building.BuildingPiece m_BuildableObject;

	private Image m_Image;

	private Color m_DefaultColor;

	public string PieceName => m_PieceName;

	public Sprite Icon => m_Image.sprite;

	public Vector2 DesiredOffset => m_DesiredOffset;

	public UltimateSurvival.Building.BuildingPiece BuildableObject => m_BuildableObject;

	public void SetCustomColor(Color color)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)m_Image).color = color;
	}

	public void SetDefaultColor()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)m_Image).color = m_DefaultColor;
	}

	private void Awake()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		m_Image = ((Component)this).GetComponent<Image>();
		m_DefaultColor = ((Graphic)m_Image).color;
	}
}
