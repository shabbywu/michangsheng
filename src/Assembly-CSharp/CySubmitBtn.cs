using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003D7 RID: 983
public class CySubmitBtn : MonoBehaviour
{
	// Token: 0x06001AEF RID: 6895 RVA: 0x000EE9EC File Offset: 0x000ECBEC
	public void Init(Sprite sprite, string name, UnityAction unityAction)
	{
		this.bg.sprite = sprite;
		this.text.text = name;
		if (unityAction == null)
		{
			this.btnCell.Disable = true;
		}
		else
		{
			this.btnCell.mouseUp.AddListener(unityAction);
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x0400167F RID: 5759
	public Image bg;

	// Token: 0x04001680 RID: 5760
	public Text text;

	// Token: 0x04001681 RID: 5761
	public BtnCell btnCell;
}
