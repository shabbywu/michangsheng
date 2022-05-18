using System;
using UnityEngine;

// Token: 0x020005D6 RID: 1494
public class setChuanWenDisable : MonoBehaviour
{
	// Token: 0x060025A0 RID: 9632 RVA: 0x0012B290 File Offset: 0x00129490
	private void Start()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			base.GetComponent<UIToggle>().enabled = false;
			base.GetComponent<UIButtonColor>().SetState(UIButtonColor.State.Disabled, true);
			base.GetComponent<UIPlayAnimation>().enabled = false;
			base.GetComponent<BoxCollider>().enabled = false;
		}
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x0001E27F File Offset: 0x0001C47F
	public void setenabel()
	{
		base.GetComponent<UIButtonColor>().enabled = false;
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}
}
