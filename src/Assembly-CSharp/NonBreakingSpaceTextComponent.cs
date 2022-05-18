using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004D1 RID: 1233
[RequireComponent(typeof(Text))]
public class NonBreakingSpaceTextComponent : MonoBehaviour
{
	// Token: 0x0600204B RID: 8267 RVA: 0x0001A7FB File Offset: 0x000189FB
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.text.RegisterDirtyVerticesCallback(new UnityAction(this.SetTextSpace));
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x0001A820 File Offset: 0x00018A20
	private void SetTextSpace()
	{
		if (this.text.text.Contains(" "))
		{
			this.text.text = this.text.text.Replace(" ", NonBreakingSpaceTextComponent.NoBreakingSpace);
		}
	}

	// Token: 0x04001BC6 RID: 7110
	public static readonly string NoBreakingSpace = "\u00a0";

	// Token: 0x04001BC7 RID: 7111
	private Text text;
}
