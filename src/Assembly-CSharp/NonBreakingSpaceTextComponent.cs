using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000354 RID: 852
[RequireComponent(typeof(Text))]
public class NonBreakingSpaceTextComponent : MonoBehaviour
{
	// Token: 0x06001CE1 RID: 7393 RVA: 0x000CE240 File Offset: 0x000CC440
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.text.RegisterDirtyVerticesCallback(new UnityAction(this.SetTextSpace));
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x000CE265 File Offset: 0x000CC465
	private void SetTextSpace()
	{
		if (this.text.text.Contains(" "))
		{
			this.text.text = this.text.text.Replace(" ", NonBreakingSpaceTextComponent.NoBreakingSpace);
		}
	}

	// Token: 0x0400176E RID: 5998
	public static readonly string NoBreakingSpace = "\u00a0";

	// Token: 0x0400176F RID: 5999
	private Text text;
}
