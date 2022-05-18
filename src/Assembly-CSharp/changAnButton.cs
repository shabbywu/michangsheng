using System;
using UnityEngine;

// Token: 0x020005CD RID: 1485
public class changAnButton : MonoBehaviour
{
	// Token: 0x0600257E RID: 9598 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x0012AFD0 File Offset: 0x001291D0
	private void Update()
	{
		if (this.flagSwitch)
		{
			this.lastTime += Time.deltaTime;
			this.AllTime += Time.deltaTime;
			if (this.AllTime > 0.5f && this.lastTime > 0.1f)
			{
				EventDelegate.Execute(base.GetComponent<UIButton>().onClick);
				this.lastTime = 0f;
			}
		}
		if (UICamera.GetMouse(0).current != base.gameObject)
		{
			this.flagSwitch = false;
		}
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
	protected void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.flagSwitch = true;
			this.lastTime = 0f;
			this.AllTime = 0f;
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.flagSwitch = false;
		}
	}

	// Token: 0x04001FFD RID: 8189
	private bool flagSwitch;

	// Token: 0x04001FFE RID: 8190
	private float lastTime;

	// Token: 0x04001FFF RID: 8191
	private float AllTime;
}
