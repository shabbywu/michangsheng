using UnityEngine;

public class CardSprite : MonoBehaviour
{
	private Card card;

	public SpriteRenderer sprite;

	public bool isSelected;

	public bool onHover;

	private GameObject last;

	public bool isUsed;

	public bool firstDraw = true;

	public Card Poker
	{
		get
		{
			return card;
		}
		set
		{
			card = value;
			card.isSprite = true;
			SetSprite();
		}
	}

	public bool Select
	{
		get
		{
			return isSelected;
		}
		set
		{
			isSelected = value;
		}
	}

	private void Start()
	{
	}

	private void SetSprite()
	{
		if (card.Attribution == CharacterType.Player || card.Attribution == CharacterType.Desk)
		{
			((Object)sprite).name = card.GetCardName;
		}
		else
		{
			((Object)sprite).name = "SmallCardBack1";
		}
	}

	public void Destroy()
	{
		card.isSprite = false;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void GoToPosition(GameObject parent, int index, int maxIndex = 100, int chaiBase = 0)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		((Renderer)sprite).sortingOrder = index;
		((Component)this).GetComponent<UITexture>().depth = index;
		if (card == null)
		{
			return;
		}
		UIWidget component = ((Component)parent.transform.Find("CardsStartPoint")).GetComponent<UIWidget>();
		float num = 50f;
		Rect textureRect = sprite.sprite.textureRect;
		if (((Rect)(ref textureRect)).width / 2f * (float)(maxIndex - 1) < component.localSize.x)
		{
			float num2 = (1 + maxIndex) / 2 - (index + 1);
			textureRect = sprite.sprite.textureRect;
			num = num2 * (((Rect)(ref textureRect)).width / 2f);
		}
		else
		{
			float num3 = (1 + maxIndex) / 2 - (index + 1) + chaiBase;
			float x = component.localSize.x;
			textureRect = sprite.sprite.textureRect;
			num = num3 * ((x - ((Rect)(ref textureRect)).width / 2f) / (float)maxIndex);
			if (chaiBase > 0)
			{
				float num4 = num;
				float num5 = chaiBase;
				textureRect = sprite.sprite.textureRect;
				num = num4 - num5 * (((Rect)(ref textureRect)).width / 2f);
			}
		}
		((Component)this).transform.localPosition = ((Component)component).transform.localPosition - Vector3.right * num + new Vector3(0f, 0f, (float)(-index));
		if (isSelected)
		{
			Transform transform = ((Component)this).transform;
			transform.localPosition += Vector3.up * 15f;
		}
	}

	public void UIOnHover()
	{
		onHover = true;
	}

	public void UIOnHoverOut()
	{
		onHover = false;
	}

	private void Update()
	{
	}

	private void OnDragOver(GameObject obj)
	{
		if ((Object)(object)obj != (Object)(object)((Component)this).gameObject)
		{
			UIOnClick();
		}
	}

	public void UIOnClick()
	{
	}
}
