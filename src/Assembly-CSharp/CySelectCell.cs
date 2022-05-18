using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003DE RID: 990
public class CySelectCell : MonoBehaviour
{
	// Token: 0x06001B0B RID: 6923 RVA: 0x00016E29 File Offset: 0x00015029
	public void Init(string name, UnityAction action)
	{
		this.btn.mouseUp.AddListener(action);
		this.selectName.text = name;
		base.gameObject.SetActive(true);
	}

	// Token: 0x040016C2 RID: 5826
	public BtnCell btn;

	// Token: 0x040016C3 RID: 5827
	public Text selectName;
}
