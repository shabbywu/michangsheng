using System;
using UnityEngine;

// Token: 0x020007A6 RID: 1958
public class VerticalScroll : MonoBehaviour
{
	// Token: 0x060031D3 RID: 12755 RVA: 0x0018C2C4 File Offset: 0x0018A4C4
	private void Start()
	{
		this.items = base.transform.Find("Items");
		this.upLimitY = this.upLimit.position.y;
		this.downLimitY = this.downLimit.position.y;
	}

	// Token: 0x060031D4 RID: 12756 RVA: 0x0018C314 File Offset: 0x0018A514
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

	// Token: 0x060031D5 RID: 12757 RVA: 0x0018C5B0 File Offset: 0x0018A7B0
	private string RaycastFunction(Vector3 obj)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(obj), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return string.Empty;
	}

	// Token: 0x04002E07 RID: 11783
	public Transform upLimit;

	// Token: 0x04002E08 RID: 11784
	public Transform downLimit;

	// Token: 0x04002E09 RID: 11785
	private Transform items;

	// Token: 0x04002E0A RID: 11786
	private float upLimitY;

	// Token: 0x04002E0B RID: 11787
	private float downLimitY;

	// Token: 0x04002E0C RID: 11788
	private bool pomeraj;

	// Token: 0x04002E0D RID: 11789
	private float startY;

	// Token: 0x04002E0E RID: 11790
	private float endY;

	// Token: 0x04002E0F RID: 11791
	public bool canScroll = true;

	// Token: 0x04002E10 RID: 11792
	private string clickedItem;

	// Token: 0x04002E11 RID: 11793
	private string releasedItem;

	// Token: 0x04002E12 RID: 11794
	private float offsetY;

	// Token: 0x04002E13 RID: 11795
	private bool bounce;

	// Token: 0x04002E14 RID: 11796
	private bool moved;

	// Token: 0x04002E15 RID: 11797
	private bool released;
}
