using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000439 RID: 1081
public class LingGuangCell : MonoBehaviour
{
	// Token: 0x06002265 RID: 8805 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x000ECB6B File Offset: 0x000EAD6B
	public void init(string desc)
	{
		this.Desc.text = desc;
	}

	// Token: 0x06002267 RID: 8807 RVA: 0x000ECB79 File Offset: 0x000EAD79
	public void Click()
	{
		Event.fireOut("ClickLingGuangCell", new object[]
		{
			this.Info
		});
	}

	// Token: 0x06002268 RID: 8808 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001BD1 RID: 7121
	public JSONObject Info;

	// Token: 0x04001BD2 RID: 7122
	public Text Desc;

	// Token: 0x04001BD3 RID: 7123
	public Text ShengYuTime;
}
