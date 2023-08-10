using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag.Filter;

public class LianDanFilter : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public bool IsSelect;

	public int Value;

	public GameObject Select;

	public GameObject UnSelect;

	public Text Content1;

	public Text Content2;

	public UnityAction<LianDanFilter> SelectCall;

	public void Init(int value, string content, UnityAction<LianDanFilter> action, float x, float y)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Value = value;
		((Object)((Component)this).gameObject).name = content;
		((Component)this).transform.localPosition = Vector2.op_Implicit(new Vector2(x, y));
		Content1.SetText(content);
		Content2.SetText(content);
		SelectCall = action;
		((Component)this).gameObject.SetActive(true);
	}

	public void Click()
	{
		if (!IsSelect)
		{
			SelectCall?.Invoke(this);
		}
	}

	public void SetState(bool selected)
	{
		IsSelect = selected;
		Select.SetActive(selected);
		UnSelect.SetActive(!selected);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Click();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!IsSelect)
		{
			Select.SetActive(true);
			UnSelect.SetActive(false);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!IsSelect)
		{
			Select.SetActive(false);
			UnSelect.SetActive(true);
		}
	}
}
