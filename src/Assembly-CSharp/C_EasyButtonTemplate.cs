using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class C_EasyButtonTemplate : MonoBehaviour
{
	// Token: 0x06000C3D RID: 3133 RVA: 0x00049E66 File Offset: 0x00048066
	private void OnEnable()
	{
		EasyButton.On_ButtonDown += this.On_ButtonDown;
		EasyButton.On_ButtonPress += this.On_ButtonPress;
		EasyButton.On_ButtonUp += this.On_ButtonUp;
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00049E9B File Offset: 0x0004809B
	private void OnDisable()
	{
		EasyButton.On_ButtonDown -= this.On_ButtonDown;
		EasyButton.On_ButtonPress -= this.On_ButtonPress;
		EasyButton.On_ButtonUp -= this.On_ButtonUp;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00049E9B File Offset: 0x0004809B
	private void OnDestroy()
	{
		EasyButton.On_ButtonDown -= this.On_ButtonDown;
		EasyButton.On_ButtonPress -= this.On_ButtonPress;
		EasyButton.On_ButtonUp -= this.On_ButtonUp;
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00004095 File Offset: 0x00002295
	private void On_ButtonDown(string buttonName)
	{
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00004095 File Offset: 0x00002295
	private void On_ButtonPress(string buttonName)
	{
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00004095 File Offset: 0x00002295
	private void On_ButtonUp(string buttonName)
	{
	}
}
