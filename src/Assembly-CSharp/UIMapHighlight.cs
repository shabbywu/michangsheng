using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMapHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public int ID;

	private Image image;

	private void Awake()
	{
		image = ((Component)this).GetComponent<Image>();
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		UIMapPanel.Inst.OnMouseEnterHighlightBlock(this);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		UIMapPanel.Inst.OnMouseExitHighlightBlock(this);
	}

	public void SetLight(bool light)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (light)
		{
			((Graphic)image).color = Color.white;
		}
		else
		{
			((Graphic)image).color = Color.clear;
		}
	}
}
