using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200047E RID: 1150
public class LunTiCell : MonoBehaviour
{
	// Token: 0x06001EC6 RID: 7878 RVA: 0x0001987C File Offset: 0x00017A7C
	public void InitLunTiCell(Sprite selectSprite, Sprite unselectSprite, int lunTiId, string lunTiName, UnityAction<int> selectAction, UnityAction<int> unSelectAction)
	{
		this.selectImage.sprite = selectSprite;
		this.unSelectImage.sprite = unselectSprite;
		this.lunTiId = lunTiId;
		this.lunTiName.text = lunTiName;
		this.selectAction = selectAction;
		this.unSelectAction = unSelectAction;
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x00109600 File Offset: 0x00107800
	public void MouseUp()
	{
		this.state = !this.state;
		this.selectImage.gameObject.SetActive(this.state);
		this.unSelectImage.gameObject.SetActive(!this.state);
		if (this.state)
		{
			this.selectAction.Invoke(this.lunTiId);
			return;
		}
		this.unSelectAction.Invoke(this.lunTiId);
	}

	// Token: 0x04001A2C RID: 6700
	private bool state;

	// Token: 0x04001A2D RID: 6701
	[SerializeField]
	private Image selectImage;

	// Token: 0x04001A2E RID: 6702
	[SerializeField]
	private Image unSelectImage;

	// Token: 0x04001A2F RID: 6703
	[SerializeField]
	private Text lunTiName;

	// Token: 0x04001A30 RID: 6704
	public int lunTiId;

	// Token: 0x04001A31 RID: 6705
	private UnityAction<int> selectAction;

	// Token: 0x04001A32 RID: 6706
	private UnityAction<int> unSelectAction;
}
