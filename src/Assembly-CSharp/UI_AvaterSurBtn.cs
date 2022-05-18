using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005B0 RID: 1456
public class UI_AvaterSurBtn : MonoBehaviour
{
	// Token: 0x060024BC RID: 9404 RVA: 0x0001D87C File Offset: 0x0001BA7C
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.ChoiceAvaterSurface));
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x0001D89A File Offset: 0x0001BA9A
	public void ChoiceAvaterSurface()
	{
		Event.fireOut("ChoiceAvaterSurface", new object[]
		{
			this.AvaterSurface
		});
	}

	// Token: 0x04001F86 RID: 8070
	public int AvaterType = 1;

	// Token: 0x04001F87 RID: 8071
	public int AvaterSurface = 1;
}
