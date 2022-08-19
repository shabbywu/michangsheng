using System;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public class VerticalScroll : MonoBehaviour
{
	// Token: 0x060029C0 RID: 10688 RVA: 0x0013EF40 File Offset: 0x0013D140
	private void Start()
	{
		this.items = base.transform.Find("Items");
		this.upLimitY = this.upLimit.position.y;
		this.downLimitY = this.downLimit.position.y;
	}

	// Token: 0x060029C1 RID: 10689 RVA: 0x0013EF90 File Offset: 0x0013D190
	private void Update()
	{
		if (this.canScroll)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.clickedItem = this.RaycastFunction(Input.mousePosition);
				this.startY = (this.endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
				Debug.Log("start y: " + this.startY);
			}
			else if (Input.GetMouseButton(0))
			{
				this.moved = true;
				this.endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
				this.offsetY = this.endY - this.startY;
				this.items.position = new Vector3(this.items.position.x, Mathf.MoveTowards(this.items.position.y, this.items.position.y + this.offsetY, 0.5f), this.items.position.z);
				this.startY = this.endY;
			}
			else if (Input.GetMouseButtonUp(0) && this.moved)
			{
				this.moved = false;
				this.released = true;
			}
			if (this.released)
			{
				this.items.Translate(0f, this.offsetY, 0f);
				this.offsetY *= 0.92f;
			}
			if (this.released && this.startY == this.endY)
			{
				if (this.items.position.y < this.downLimitY)
				{
					this.items.position = new Vector3(this.items.position.x, Mathf.MoveTowards(this.items.position.y, this.downLimitY, 1f), this.items.position.z);
					return;
				}
				if (this.items.position.y > this.upLimitY)
				{
					this.items.position = new Vector3(this.items.position.x, Mathf.MoveTowards(this.items.position.y, this.upLimitY, 1f), this.items.position.z);
					return;
				}
				if (this.items.position.y == this.upLimitY || this.items.position.y == this.downLimitY)
				{
					this.released = false;
				}
			}
		}
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x0013F22C File Offset: 0x0013D42C
	private string RaycastFunction(Vector3 obj)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(obj), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return string.Empty;
	}

	// Token: 0x04002617 RID: 9751
	public Transform upLimit;

	// Token: 0x04002618 RID: 9752
	public Transform downLimit;

	// Token: 0x04002619 RID: 9753
	private Transform items;

	// Token: 0x0400261A RID: 9754
	private float upLimitY;

	// Token: 0x0400261B RID: 9755
	private float downLimitY;

	// Token: 0x0400261C RID: 9756
	private bool pomeraj;

	// Token: 0x0400261D RID: 9757
	private float startY;

	// Token: 0x0400261E RID: 9758
	private float endY;

	// Token: 0x0400261F RID: 9759
	public bool canScroll = true;

	// Token: 0x04002620 RID: 9760
	private string clickedItem;

	// Token: 0x04002621 RID: 9761
	private string releasedItem;

	// Token: 0x04002622 RID: 9762
	private float offsetY;

	// Token: 0x04002623 RID: 9763
	private bool bounce;

	// Token: 0x04002624 RID: 9764
	private bool moved;

	// Token: 0x04002625 RID: 9765
	private bool released;
}
