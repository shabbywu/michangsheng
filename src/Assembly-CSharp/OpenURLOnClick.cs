using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x0600045C RID: 1116 RVA: 0x00017FF4 File Offset: 0x000161F4
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.lastWorldPosition);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
