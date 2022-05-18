using System;
using UnityEngine;

// Token: 0x020005DA RID: 1498
public class showTooltip : MonoBehaviour
{
	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x060025AE RID: 9646 RVA: 0x0001E2CF File Offset: 0x0001C4CF
	public static showTooltip Instence
	{
		get
		{
			showTooltip.instence == null;
			return showTooltip.instence;
		}
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
	private void Start()
	{
		showTooltip.instence = this;
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x0001E2EA File Offset: 0x0001C4EA
	private void OnDestroy()
	{
		showTooltip.instence = null;
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x0012B2DC File Offset: 0x001294DC
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

	// Token: 0x04002023 RID: 8227
	private static showTooltip instence;

	// Token: 0x04002024 RID: 8228
	public bool ISshowTooltip;
}
