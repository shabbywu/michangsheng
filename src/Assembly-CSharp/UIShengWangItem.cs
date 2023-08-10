using UnityEngine;
using UnityEngine.EventSystems;

public class UIShengWangItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public GameObject ActiveImage;

	public GameObject UnActiveImage;

	public GameObject NameImage;

	public GameObject NameText;

	public string TeQuanDesc;

	public void Set(bool active)
	{
		ActiveImage.SetActive(active);
		UnActiveImage.SetActive(!active);
		NameImage.SetActive(active);
		NameText.SetActive(!active);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!string.IsNullOrWhiteSpace(TeQuanDesc))
		{
			UToolTip.Show(TeQuanDesc, 500f, 100f);
			UToolTip.BindObj = ((Component)this).gameObject;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UToolTip.Close();
	}
}
