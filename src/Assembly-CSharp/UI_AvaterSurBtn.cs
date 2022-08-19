using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000400 RID: 1024
public class UI_AvaterSurBtn : MonoBehaviour
{
	// Token: 0x0600210A RID: 8458 RVA: 0x000E78F3 File Offset: 0x000E5AF3
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.ChoiceAvaterSurface));
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x000E7911 File Offset: 0x000E5B11
	public void ChoiceAvaterSurface()
	{
		Event.fireOut("ChoiceAvaterSurface", new object[]
		{
			this.AvaterSurface
		});
	}

	// Token: 0x04001ACA RID: 6858
	public int AvaterType = 1;

	// Token: 0x04001ACB RID: 6859
	public int AvaterSurface = 1;
}
