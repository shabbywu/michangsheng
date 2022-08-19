using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200029E RID: 670
public class CySubmitBtn : MonoBehaviour
{
	// Token: 0x060017FD RID: 6141 RVA: 0x000A775C File Offset: 0x000A595C
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

	// Token: 0x040012E3 RID: 4835
	public Image bg;

	// Token: 0x040012E4 RID: 4836
	public Text text;

	// Token: 0x040012E5 RID: 4837
	public BtnCell btnCell;
}
