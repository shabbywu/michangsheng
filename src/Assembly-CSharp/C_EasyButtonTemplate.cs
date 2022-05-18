using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class C_EasyButtonTemplate : MonoBehaviour
{
	// Token: 0x06000D5E RID: 3422 RVA: 0x0000F16F File Offset: 0x0000D36F
	private void OnEnable()
	{
		EasyButton.On_ButtonDown += this.On_ButtonDown;
		EasyButton.On_ButtonPress += this.On_ButtonPress;
		EasyButton.On_ButtonUp += this.On_ButtonUp;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
	private void OnDisable()
	{
		EasyButton.On_ButtonDown -= this.On_ButtonDown;
		EasyButton.On_ButtonPress -= this.On_ButtonPress;
		EasyButton.On_ButtonUp -= this.On_ButtonUp;
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
	private void OnDestroy()
	{
		EasyButton.On_ButtonDown -= this.On_ButtonDown;
		EasyButton.On_ButtonPress -= this.On_ButtonPress;
		EasyButton.On_ButtonUp -= this.On_ButtonUp;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_ButtonDown(string buttonName)
	{
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_ButtonPress(string buttonName)
	{
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_ButtonUp(string buttonName)
	{
	}
}
