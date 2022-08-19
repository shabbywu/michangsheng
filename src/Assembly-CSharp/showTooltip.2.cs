using System;
using UnityEngine;

// Token: 0x02000426 RID: 1062
public class showTooltip : MonoBehaviour
{
	// Token: 0x17000286 RID: 646
	// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000E9ED0 File Offset: 0x000E80D0
	public static showTooltip Instence
	{
		get
		{
			showTooltip.instence == null;
			return showTooltip.instence;
		}
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x000E9EE3 File Offset: 0x000E80E3
	private void Start()
	{
		showTooltip.instence = this;
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x000E9EEB File Offset: 0x000E80EB
	private void OnDestroy()
	{
		showTooltip.instence = null;
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x000E9EF4 File Offset: 0x000E80F4
	private void Update()
	{
		if (this.ISshowTooltip)
		{
			Vector3 vector;
			vector..ctor(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			if (Input.mousePosition.x > (float)(Screen.width / 2))
			{
				vector.x -= (float)base.GetComponentInChildren<UISprite>().width;
			}
			if (Input.mousePosition.y < (float)(Screen.height / 2))
			{
				vector.y += (float)base.GetComponentInChildren<UISprite>().height;
			}
			base.transform.position = UICamera.currentCamera.ScreenToWorldPoint(vector);
			return;
		}
		base.transform.position = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x04001B5D RID: 7005
	private static showTooltip instence;

	// Token: 0x04001B5E RID: 7006
	public bool ISshowTooltip;
}
