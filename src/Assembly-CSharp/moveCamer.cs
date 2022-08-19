using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200047F RID: 1151
public class moveCamer : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x060023EF RID: 9199 RVA: 0x000F5770 File Offset: 0x000F3970
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

	// Token: 0x060023F0 RID: 9200 RVA: 0x000F582C File Offset: 0x000F3A2C
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

	// Token: 0x060023F1 RID: 9201 RVA: 0x00004095 File Offset: 0x00002295
	public void OnBeginDrag(PointerEventData data)
	{
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x000F58F8 File Offset: 0x000F3AF8
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

	// Token: 0x060023F3 RID: 9203 RVA: 0x00004095 File Offset: 0x00002295
	public void OnEndDrag(PointerEventData data)
	{
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x000F59F4 File Offset: 0x000F3BF4
	public void OnPointerDown(PointerEventData eventData)
	{
		base.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x000F5A15 File Offset: 0x000F3C15
	public void OnPointerUp(PointerEventData eventData)
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x04001CBE RID: 7358
	private SmoothFollow sf;

	// Token: 0x04001CBF RID: 7359
	private Image itemimage;

	// Token: 0x04001CC0 RID: 7360
	private int touchfingerid = -1;

	// Token: 0x04001CC1 RID: 7361
	private Rect imageRect;
}
