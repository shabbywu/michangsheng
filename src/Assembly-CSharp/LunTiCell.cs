using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200031B RID: 795
public class LunTiCell : MonoBehaviour
{
	// Token: 0x06001B93 RID: 7059 RVA: 0x000C4397 File Offset: 0x000C2597
	public void InitLunTiCell(Sprite selectSprite, Sprite unselectSprite, int lunTiId, string lunTiName, UnityAction<int> selectAction, UnityAction<int> unSelectAction)
	{
		this.selectImage.sprite = selectSprite;
		this.unSelectImage.sprite = unselectSprite;
		this.lunTiId = lunTiId;
		this.lunTiName.text = lunTiName;
		this.selectAction = selectAction;
		this.unSelectAction = unSelectAction;
	}

	// Token: 0x06001B94 RID: 7060 RVA: 0x000C43D8 File Offset: 0x000C25D8
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

	// Token: 0x04001611 RID: 5649
	private bool state;

	// Token: 0x04001612 RID: 5650
	[SerializeField]
	private Image selectImage;

	// Token: 0x04001613 RID: 5651
	[SerializeField]
	private Image unSelectImage;

	// Token: 0x04001614 RID: 5652
	[SerializeField]
	private Text lunTiName;

	// Token: 0x04001615 RID: 5653
	public int lunTiId;

	// Token: 0x04001616 RID: 5654
	private UnityAction<int> selectAction;

	// Token: 0x04001617 RID: 5655
	private UnityAction<int> unSelectAction;
}
