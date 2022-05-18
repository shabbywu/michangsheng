using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000640 RID: 1600
public class moveCamer : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x060027B3 RID: 10163 RVA: 0x001355F4 File Offset: 0x001337F4
	private void Start()
	{
		this.sf = Camera.main.GetComponent<SmoothFollow>();
		this.itemimage = base.gameObject.GetComponent<Image>();
		Vector3 position = this.itemimage.rectTransform.position;
		Rect rect = this.itemimage.rectTransform.rect;
		int num = Screen.width / 711;
		int num2 = Screen.height / 400;
		Vector2 size = rect.size;
		float num3 = size.x * (float)num;
		float num4 = size.y * (float)num2;
		this.imageRect = new Rect(new Vector2(position.x - num3 / 2f, position.y - num4 / 2f), new Vector2(num3, num4));
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x001356B0 File Offset: 0x001338B0
	private void Update()
	{
		if (Input.touchCount == 1)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == null)
				{
					Vector2 position = touch.position;
					if (this.imageRect.Contains(position) && this.touchfingerid == -1)
					{
						this.touchfingerid = touch.fingerId;
					}
				}
				if (touch.phase == 1 && this.touchfingerid != -1 && touch.fingerId == this.touchfingerid)
				{
					float x = touch.deltaPosition.x;
					this.sf.rotateCamerX(x);
				}
				if (touch.phase == 3 && touch.fingerId == this.touchfingerid)
				{
					this.touchfingerid = -1;
				}
			}
		}
	}

	// Token: 0x060027B5 RID: 10165 RVA: 0x000042DD File Offset: 0x000024DD
	public void OnBeginDrag(PointerEventData data)
	{
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x0013577C File Offset: 0x0013397C
	public void OnDrag(PointerEventData data)
	{
		if (data.pressPosition.x > 0f)
		{
			this.sf.transform.position = new Vector3(this.sf.transform.position.x + data.pressPosition.x, this.sf.transform.position.y, this.sf.transform.position.x);
		}
		if (data.pressPosition.y > 0f)
		{
			this.sf.transform.position = new Vector3(this.sf.transform.position.x, this.sf.transform.position.y + data.pressPosition.y, this.sf.transform.position.x);
		}
		this.sf.FollowUpdate();
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x000042DD File Offset: 0x000024DD
	public void OnEndDrag(PointerEventData data)
	{
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
	public void OnPointerDown(PointerEventData eventData)
	{
		base.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x0001F611 File Offset: 0x0001D811
	public void OnPointerUp(PointerEventData eventData)
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x040021A1 RID: 8609
	private SmoothFollow sf;

	// Token: 0x040021A2 RID: 8610
	private Image itemimage;

	// Token: 0x040021A3 RID: 8611
	private int touchfingerid = -1;

	// Token: 0x040021A4 RID: 8612
	private Rect imageRect;
}
