using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class changAnButton : MonoBehaviour
{
	// Token: 0x060021C4 RID: 8644 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x000E99BC File Offset: 0x000E7BBC
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

	// Token: 0x060021C6 RID: 8646 RVA: 0x000E9A49 File Offset: 0x000E7C49
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

	// Token: 0x04001B3C RID: 6972
	private bool flagSwitch;

	// Token: 0x04001B3D RID: 6973
	private float lastTime;

	// Token: 0x04001B3E RID: 6974
	private float AllTime;
}
