using System;
using UnityEngine;

// Token: 0x0200067B RID: 1659
public class CheckGroundUp : MonoBehaviour
{
	// Token: 0x0600297D RID: 10621 RVA: 0x00020394 File Offset: 0x0001E594
	private void Awake()
	{
		this.player = base.transform.parent.GetComponent<MonkeyController2D>();
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x0014289C File Offset: 0x00140A9C
	private void OnTriggerEnter2D(Collider2D col)
	{
		if ((this.player.state == MonkeyController2D.State.jumped || this.player.state == MonkeyController2D.State.climbUp || this.player.state == MonkeyController2D.State.wasted) && col.tag != "Finish")
		{
			float y;
			if (col.transform.childCount > 0)
			{
				y = col.transform.Find("TriggerPositionDown").position.y;
			}
			else
			{
				y = col.transform.position.y;
			}
			if (!this.player.isSliding)
			{
				if (this.player.transform.position.y < y)
				{
					base.transform.parent.GetComponent<Collider2D>().isTrigger = true;
					return;
				}
				if (this.player.transform.position.y >= y)
				{
					if (!this.player.triggerCheckDownTrigger)
					{
						if (!this.player.triggerCheckDownBehind)
						{
							base.transform.parent.GetComponent<Collider2D>().isTrigger = true;
							return;
						}
					}
					else
					{
						bool triggerCheckDownBehind = this.player.triggerCheckDownBehind;
					}
				}
			}
		}
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x000203AC File Offset: 0x0001E5AC
	private void OnTriggerExit2D(Collider2D col)
	{
		if (this.player.GetComponent<Collider2D>().isTrigger && !this.player.triggerCheckDownTrigger)
		{
			this.player.GetComponent<Collider2D>().isTrigger = false;
		}
	}

	// Token: 0x0400232C RID: 9004
	private MonkeyController2D player;

	// Token: 0x0400232D RID: 9005
	public bool gornji;

	// Token: 0x0400232E RID: 9006
	public bool donji;
}
