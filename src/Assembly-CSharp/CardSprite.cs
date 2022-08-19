using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class CardSprite : MonoBehaviour
{
	// Token: 0x06002534 RID: 9524 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06002536 RID: 9526 RVA: 0x00102AAE File Offset: 0x00100CAE
	// (set) Token: 0x06002535 RID: 9525 RVA: 0x00102A93 File Offset: 0x00100C93
	public Card Poker
	{
		get
		{
			return this.card;
		}
		set
		{
			this.card = value;
			this.card.isSprite = true;
			this.SetSprite();
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06002538 RID: 9528 RVA: 0x00102ABF File Offset: 0x00100CBF
	// (set) Token: 0x06002537 RID: 9527 RVA: 0x00102AB6 File Offset: 0x00100CB6
	public bool Select
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x00102AC8 File Offset: 0x00100CC8
	private void SetSprite()
	{
		if (this.card.Attribution == CharacterType.Player || this.card.Attribution == CharacterType.Desk)
		{
			this.sprite.name = this.card.GetCardName;
			return;
		}
		this.sprite.name = "SmallCardBack1";
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x00102B18 File Offset: 0x00100D18
	public void Destroy()
	{
		this.card.isSprite = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600253B RID: 9531 RVA: 0x00102B34 File Offset: 0x00100D34
	public void GoToPosition(GameObject parent, int index, int maxIndex = 100, int chaiBase = 0)
	{
		this.sprite.sortingOrder = index;
		base.GetComponent<UITexture>().depth = index;
		if (this.card == null)
		{
			return;
		}
		UIWidget component = parent.transform.Find("CardsStartPoint").GetComponent<UIWidget>();
		float num;
		if (this.sprite.sprite.textureRect.width / 2f * (float)(maxIndex - 1) < component.localSize.x)
		{
			num = (float)((1 + maxIndex) / 2 - (index + 1)) * (this.sprite.sprite.textureRect.width / 2f);
		}
		else
		{
			num = (float)((1 + maxIndex) / 2 - (index + 1) + chaiBase) * ((component.localSize.x - this.sprite.sprite.textureRect.width / 2f) / (float)maxIndex);
			if (chaiBase > 0)
			{
				num -= (float)chaiBase * (this.sprite.sprite.textureRect.width / 2f);
			}
		}
		base.transform.localPosition = component.transform.localPosition - Vector3.right * num + new Vector3(0f, 0f, (float)(-(float)index));
		if (this.isSelected)
		{
			base.transform.localPosition += Vector3.up * 15f;
		}
	}

	// Token: 0x0600253C RID: 9532 RVA: 0x00102CAA File Offset: 0x00100EAA
	public void UIOnHover()
	{
		this.onHover = true;
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x00102CB3 File Offset: 0x00100EB3
	public void UIOnHoverOut()
	{
		this.onHover = false;
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x00102CBC File Offset: 0x00100EBC
	private void OnDragOver(GameObject obj)
	{
		if (obj != base.gameObject)
		{
			this.UIOnClick();
		}
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x00004095 File Offset: 0x00002295
	public void UIOnClick()
	{
	}

	// Token: 0x04001E08 RID: 7688
	private Card card;

	// Token: 0x04001E09 RID: 7689
	public SpriteRenderer sprite;

	// Token: 0x04001E0A RID: 7690
	public bool isSelected;

	// Token: 0x04001E0B RID: 7691
	public bool onHover;

	// Token: 0x04001E0C RID: 7692
	private GameObject last;

	// Token: 0x04001E0D RID: 7693
	public bool isUsed;

	// Token: 0x04001E0E RID: 7694
	public bool firstDraw = true;
}
