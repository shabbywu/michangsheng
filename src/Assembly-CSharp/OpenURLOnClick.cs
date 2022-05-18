using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x060004AA RID: 1194 RVA: 0x0006F408 File Offset: 0x0006D608
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
