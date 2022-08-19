using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class setChuanWenDisable : MonoBehaviour
{
	// Token: 0x060021E6 RID: 8678 RVA: 0x000E9E34 File Offset: 0x000E8034
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

	// Token: 0x060021E7 RID: 8679 RVA: 0x000E9E80 File Offset: 0x000E8080
	public void setenabel()
	{
		base.GetComponent<UIButtonColor>().enabled = false;
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}
}
