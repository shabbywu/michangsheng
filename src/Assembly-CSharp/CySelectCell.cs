using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002A4 RID: 676
public class CySelectCell : MonoBehaviour
{
	// Token: 0x06001818 RID: 6168 RVA: 0x000A8786 File Offset: 0x000A6986
	public void Init(string name, UnityAction action)
	{
		this.btn.mouseUpEvent.AddListener(action);
		this.selectName.text = name;
		base.gameObject.SetActive(true);
	}

	// Token: 0x04001324 RID: 4900
	public FpBtn btn;

	// Token: 0x04001325 RID: 4901
	public Text selectName;
}
