using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetBtnTextColor : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Color hoverColor;

	public Color hoverColorStart;

	private Text label;

	private void Start()
	{
		label = ((Component)this).GetComponentInChildren<Text>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)label).color = hoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)label).color = hoverColorStart;
	}
}
