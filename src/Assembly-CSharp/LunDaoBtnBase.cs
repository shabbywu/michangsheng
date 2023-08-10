using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LunDaoBtnBase : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	private List<UnityAction> MouseDownCallBack = new List<UnityAction>();

	private List<UnityAction> MouseUpCallBack = new List<UnityAction>();

	private List<UnityAction> MouseEnterCallBack = new List<UnityAction>();

	private List<UnityAction> MouseExitCallBack = new List<UnityAction>();

	public bool Disable;

	protected void AddMouseUpCallBack(UnityAction call)
	{
		MouseUpCallBack.Add(call);
	}

	protected void AddMouseDownCallBack(UnityAction call)
	{
		MouseDownCallBack.Add(call);
	}

	public void ClearClickCallBack()
	{
		MouseUpCallBack = new List<UnityAction>();
		MouseDownCallBack = new List<UnityAction>();
	}

	protected void AddMouseEnterCallBack(UnityAction call)
	{
		MouseEnterCallBack.Add(call);
	}

	public void ClearMouseEnterCallBack()
	{
		MouseEnterCallBack = new List<UnityAction>();
	}

	protected void AddMouseExitCallBack(UnityAction call)
	{
		MouseExitCallBack.Add(call);
	}

	public void ClearMouseExitCallBack()
	{
		MouseExitCallBack = new List<UnityAction>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Disable || MouseEnterCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction item in MouseEnterCallBack)
		{
			item.Invoke();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (Disable || MouseExitCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction item in MouseExitCallBack)
		{
			item.Invoke();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (Disable || MouseDownCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction item in MouseDownCallBack)
		{
			item.Invoke();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (Disable || MouseUpCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction item in MouseUpCallBack)
		{
			item.Invoke();
		}
	}

	public void ClearAll()
	{
		MouseDownCallBack = new List<UnityAction>();
		MouseUpCallBack = new List<UnityAction>();
		MouseEnterCallBack = new List<UnityAction>();
		MouseExitCallBack = new List<UnityAction>();
	}
}
