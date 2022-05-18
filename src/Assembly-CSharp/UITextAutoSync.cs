using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004E4 RID: 1252
[RequireComponent(typeof(UITextAutoSync))]
public class UITextAutoSync : MonoBehaviour
{
	// Token: 0x060020A0 RID: 8352 RVA: 0x0001AD4F File Offset: 0x00018F4F
	private void Awake()
	{
		this.me = base.GetComponent<Text>();
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x00113A0C File Offset: 0x00111C0C
	private void Update()
	{
		if (this.Target != null && this.me.text != this.Target.text)
		{
			this.me.text = this.Target.text;
		}
	}

	// Token: 0x04001C1F RID: 7199
	private Text me;

	// Token: 0x04001C20 RID: 7200
	public Text Target;
}
