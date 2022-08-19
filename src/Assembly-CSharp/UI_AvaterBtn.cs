using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003FF RID: 1023
public class UI_AvaterBtn : MonoBehaviour
{
	// Token: 0x06002107 RID: 8455 RVA: 0x000E789F File Offset: 0x000E5A9F
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.ChoiceAvater));
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x000E78BD File Offset: 0x000E5ABD
	public void ChoiceAvater()
	{
		Event.fireOut("ChoiceAvater", new object[]
		{
			this.AvaterType
		});
	}

	// Token: 0x04001AC8 RID: 6856
	public int AvaterType = 1;

	// Token: 0x04001AC9 RID: 6857
	public int AvaterSurface = 1;
}
