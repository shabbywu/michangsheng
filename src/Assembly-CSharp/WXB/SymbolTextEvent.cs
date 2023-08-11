using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WXB;

[RequireComponent(typeof(SymbolText))]
public class SymbolTextEvent : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[Serializable]
	public class OnClickEvent : UnityEvent<NodeBase>
	{
	}

	private SymbolText d_symbolText;

	private RenderCache.BaseData d_baseData;

	public OnClickEvent OnClick = new OnClickEvent();

	private bool isEnter;

	private RenderCache.BaseData d_down_basedata;

	private Vector2 localPosition;

	private void OnEnable()
	{
		if ((Object)(object)d_symbolText == (Object)null)
		{
			d_symbolText = ((Component)this).GetComponent<SymbolText>();
		}
	}

	private void OnDisable()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		isEnter = false;
		d_baseData = null;
		d_down_basedata = null;
		localPosition = Vector2.zero;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isEnter = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isEnter = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		d_down_basedata = d_baseData;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (d_down_basedata == d_baseData && d_down_basedata != null)
		{
			((UnityEvent<NodeBase>)OnClick).Invoke(d_down_basedata.node);
		}
	}

	private void Update()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (isEnter)
		{
			if ((Object)(object)((Graphic)d_symbolText).canvas != (Object)null)
			{
				Tools.ScreenPointToWorldPointInRectangle(((Graphic)d_symbolText).rectTransform, Vector2.op_Implicit(Input.mousePosition), ((Graphic)d_symbolText).canvas.worldCamera, out localPosition);
			}
			RenderCache.BaseData baseData = d_symbolText.renderCache.Get(localPosition);
			if (d_baseData != baseData)
			{
				if (d_baseData != null)
				{
					d_baseData.OnMouseLevel();
				}
				d_baseData = baseData;
				if (d_baseData != null)
				{
					d_baseData.OnMouseEnter();
				}
			}
		}
		else if (d_baseData != null)
		{
			d_baseData.OnMouseLevel();
			d_baseData = null;
		}
	}
}
