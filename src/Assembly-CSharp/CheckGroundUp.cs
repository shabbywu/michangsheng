using System;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class CheckGroundUp : MonoBehaviour
{
	// Token: 0x06002559 RID: 9561 RVA: 0x00102E07 File Offset: 0x00101007
	private void Awake()
	{
		this.player = base.transform.parent.GetComponent<MonkeyController2D>();
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x00102E20 File Offset: 0x00101020
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

	// Token: 0x0600255B RID: 9563 RVA: 0x00102F3F File Offset: 0x0010113F
	private void OnTriggerExit2D(Collider2D col)
	{
		if (this.player.GetComponent<Collider2D>().isTrigger && !this.player.triggerCheckDownTrigger)
		{
			this.player.GetComponent<Collider2D>().isTrigger = false;
		}
	}

	// Token: 0x04001E15 RID: 7701
	private MonkeyController2D player;

	// Token: 0x04001E16 RID: 7702
	public bool gornji;

	// Token: 0x04001E17 RID: 7703
	public bool donji;
}
