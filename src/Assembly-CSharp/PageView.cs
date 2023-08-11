using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
{
	private ScrollRect rect;

	private float targethorizontal;

	private bool isDrag;

	private List<float> posList = new List<float>();

	private int currentPageIndex = -1;

	public Action<int> OnPageChanged;

	private bool stopMove = true;

	public float smooting = 4f;

	public float sensitivity;

	private float startTime;

	private float startDragHorizontal;

	private void Awake()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		rect = ((Component)((Component)this).transform).GetComponent<ScrollRect>();
		Rect val = rect.content.rect;
		float width = ((Rect)(ref val)).width;
		val = ((Component)this).GetComponent<RectTransform>().rect;
		float num = width - ((Rect)(ref val)).width;
		posList.Add(0f);
		for (int i = 1; i < ((Component)rect.content).transform.childCount - 1; i++)
		{
			List<float> list = posList;
			val = ((Component)this).GetComponent<RectTransform>().rect;
			list.Add(((Rect)(ref val)).width * (float)i / num);
		}
		posList.Add(1f);
	}

	private void Update()
	{
		if (!isDrag && !stopMove)
		{
			startTime += Time.deltaTime;
			float num = startTime * smooting;
			rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, num);
			if (num >= 1f)
			{
				stopMove = true;
			}
		}
	}

	public void pageTo(int index)
	{
		if (index >= 0 && index < posList.Count)
		{
			rect.horizontalNormalizedPosition = posList[index];
			SetPageIndex(index);
		}
		else
		{
			Debug.LogWarning((object)"页码不存在");
		}
	}

	private void SetPageIndex(int index)
	{
		if (currentPageIndex != index)
		{
			currentPageIndex = index;
			if (OnPageChanged != null)
			{
				OnPageChanged(index);
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isDrag = true;
		startDragHorizontal = rect.horizontalNormalizedPosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		float horizontalNormalizedPosition = rect.horizontalNormalizedPosition;
		horizontalNormalizedPosition += (horizontalNormalizedPosition - startDragHorizontal) * sensitivity;
		horizontalNormalizedPosition = ((horizontalNormalizedPosition < 1f) ? horizontalNormalizedPosition : 1f);
		horizontalNormalizedPosition = ((horizontalNormalizedPosition > 0f) ? horizontalNormalizedPosition : 0f);
		int num = 0;
		float num2 = Mathf.Abs(posList[num] - horizontalNormalizedPosition);
		for (int i = 1; i < posList.Count; i++)
		{
			float num3 = Mathf.Abs(posList[i] - horizontalNormalizedPosition);
			if (num3 < num2)
			{
				num = i;
				num2 = num3;
			}
		}
		SetPageIndex(num);
		targethorizontal = posList[num];
		isDrag = false;
		startTime = 0f;
		stopMove = false;
	}
}
