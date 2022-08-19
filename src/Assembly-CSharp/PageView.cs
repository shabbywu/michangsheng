using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003F5 RID: 1013
public class PageView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
{
	// Token: 0x060020B2 RID: 8370 RVA: 0x000E63C4 File Offset: 0x000E45C4
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

	// Token: 0x060020B3 RID: 8371 RVA: 0x000E6474 File Offset: 0x000E4674
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

	// Token: 0x060020B4 RID: 8372 RVA: 0x000E64E2 File Offset: 0x000E46E2
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

	// Token: 0x060020B5 RID: 8373 RVA: 0x000E651F File Offset: 0x000E471F
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

	// Token: 0x060020B6 RID: 8374 RVA: 0x000E6545 File Offset: 0x000E4745
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.isDrag = true;
		this.startDragHorizontal = this.rect.horizontalNormalizedPosition;
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x000E6560 File Offset: 0x000E4760
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

	// Token: 0x04001A96 RID: 6806
	private ScrollRect rect;

	// Token: 0x04001A97 RID: 6807
	private float targethorizontal;

	// Token: 0x04001A98 RID: 6808
	private bool isDrag;

	// Token: 0x04001A99 RID: 6809
	private List<float> posList = new List<float>();

	// Token: 0x04001A9A RID: 6810
	private int currentPageIndex = -1;

	// Token: 0x04001A9B RID: 6811
	public Action<int> OnPageChanged;

	// Token: 0x04001A9C RID: 6812
	private bool stopMove = true;

	// Token: 0x04001A9D RID: 6813
	public float smooting = 4f;

	// Token: 0x04001A9E RID: 6814
	public float sensitivity;

	// Token: 0x04001A9F RID: 6815
	private float startTime;

	// Token: 0x04001AA0 RID: 6816
	private float startDragHorizontal;
}
