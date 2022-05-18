using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005AF RID: 1455
public class UI_AvaterBtn : MonoBehaviour
{
	// Token: 0x060024B9 RID: 9401 RVA: 0x0001D828 File Offset: 0x0001BA28
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.ChoiceAvater));
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x0001D846 File Offset: 0x0001BA46
	public void ChoiceAvater()
	{
		Event.fireOut("ChoiceAvater", new object[]
		{
			this.AvaterType
		});
	}

	// Token: 0x04001F84 RID: 8068
	public int AvaterType = 1;

	// Token: 0x04001F85 RID: 8069
	public int AvaterSurface = 1;
}
