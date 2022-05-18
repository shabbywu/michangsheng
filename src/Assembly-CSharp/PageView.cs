using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020005A5 RID: 1445
public class PageView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
{
	// Token: 0x06002464 RID: 9316 RVA: 0x001285D0 File Offset: 0x001267D0
	private void Awake()
	{
		this.rect = base.transform.GetComponent<ScrollRect>();
		float num = this.rect.content.rect.width - base.GetComponent<RectTransform>().rect.width;
		this.posList.Add(0f);
		for (int i = 1; i < this.rect.content.transform.childCount - 1; i++)
		{
			this.posList.Add(base.GetComponent<RectTransform>().rect.width * (float)i / num);
		}
		this.posList.Add(1f);
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x00128680 File Offset: 0x00126880
	private void Update()
	{
		if (!this.isDrag && !this.stopMove)
		{
			this.startTime += Time.deltaTime;
			float num = this.startTime * this.smooting;
			this.rect.horizontalNormalizedPosition = Mathf.Lerp(this.rect.horizontalNormalizedPosition, this.targethorizontal, num);
			if (num >= 1f)
			{
				this.stopMove = true;
			}
		}
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x0001D484 File Offset: 0x0001B684
	public void pageTo(int index)
	{
		if (index >= 0 && index < this.posList.Count)
		{
			this.rect.horizontalNormalizedPosition = this.posList[index];
			this.SetPageIndex(index);
			return;
		}
		Debug.LogWarning("页码不存在");
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x0001D4C1 File Offset: 0x0001B6C1
	private void SetPageIndex(int index)
	{
		if (this.currentPageIndex != index)
		{
			this.currentPageIndex = index;
			if (this.OnPageChanged != null)
			{
				this.OnPageChanged(index);
			}
		}
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x0001D4E7 File Offset: 0x0001B6E7
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.isDrag = true;
		this.startDragHorizontal = this.rect.horizontalNormalizedPosition;
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x001286F0 File Offset: 0x001268F0
	public void OnEndDrag(PointerEventData eventData)
	{
		float num = this.rect.horizontalNormalizedPosition;
		num += (num - this.startDragHorizontal) * this.sensitivity;
		num = ((num < 1f) ? num : 1f);
		num = ((num > 0f) ? num : 0f);
		int num2 = 0;
		float num3 = Mathf.Abs(this.posList[num2] - num);
		for (int i = 1; i < this.posList.Count; i++)
		{
			float num4 = Mathf.Abs(this.posList[i] - num);
			if (num4 < num3)
			{
				num2 = i;
				num3 = num4;
			}
		}
		this.SetPageIndex(num2);
		this.targethorizontal = this.posList[num2];
		this.isDrag = false;
		this.startTime = 0f;
		this.stopMove = false;
	}

	// Token: 0x04001F52 RID: 8018
	private ScrollRect rect;

	// Token: 0x04001F53 RID: 8019
	private float targethorizontal;

	// Token: 0x04001F54 RID: 8020
	private bool isDrag;

	// Token: 0x04001F55 RID: 8021
	private List<float> posList = new List<float>();

	// Token: 0x04001F56 RID: 8022
	private int currentPageIndex = -1;

	// Token: 0x04001F57 RID: 8023
	public Action<int> OnPageChanged;

	// Token: 0x04001F58 RID: 8024
	private bool stopMove = true;

	// Token: 0x04001F59 RID: 8025
	public float smooting = 4f;

	// Token: 0x04001F5A RID: 8026
	public float sensitivity;

	// Token: 0x04001F5B RID: 8027
	private float startTime;

	// Token: 0x04001F5C RID: 8028
	private float startDragHorizontal;
}
