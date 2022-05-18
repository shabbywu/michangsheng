using System;
using UnityEngine;

// Token: 0x02000677 RID: 1655
public class CardSprite : MonoBehaviour
{
	// Token: 0x06002958 RID: 10584 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x0600295A RID: 10586 RVA: 0x00020205 File Offset: 0x0001E405
	// (set) Token: 0x06002959 RID: 10585 RVA: 0x000201EA File Offset: 0x0001E3EA
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

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x0600295C RID: 10588 RVA: 0x00020216 File Offset: 0x0001E416
	// (set) Token: 0x0600295B RID: 10587 RVA: 0x0002020D File Offset: 0x0001E40D
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

	// Token: 0x0600295D RID: 10589 RVA: 0x001426D4 File Offset: 0x001408D4
	private void SetSprite()
	{
		if (this.card.Attribution == CharacterType.Player || this.card.Attribution == CharacterType.Desk)
		{
			this.sprite.name = this.card.GetCardName;
			return;
		}
		this.sprite.name = "SmallCardBack1";
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x0002021E File Offset: 0x0001E41E
	public void Destroy()
	{
		this.card.isSprite = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x00142724 File Offset: 0x00140924
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

	// Token: 0x06002960 RID: 10592 RVA: 0x00020237 File Offset: 0x0001E437
	public void UIOnHover()
	{
		this.onHover = true;
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x00020240 File Offset: 0x0001E440
	public void UIOnHoverOut()
	{
		this.onHover = false;
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x00020249 File Offset: 0x0001E449
	private void OnDragOver(GameObject obj)
	{
		if (obj != base.gameObject)
		{
			this.UIOnClick();
		}
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000042DD File Offset: 0x000024DD
	public void UIOnClick()
	{
	}

	// Token: 0x0400231F RID: 8991
	private Card card;

	// Token: 0x04002320 RID: 8992
	public SpriteRenderer sprite;

	// Token: 0x04002321 RID: 8993
	public bool isSelected;

	// Token: 0x04002322 RID: 8994
	public bool onHover;

	// Token: 0x04002323 RID: 8995
	private GameObject last;

	// Token: 0x04002324 RID: 8996
	public bool isUsed;

	// Token: 0x04002325 RID: 8997
	public bool firstDraw = true;
}
