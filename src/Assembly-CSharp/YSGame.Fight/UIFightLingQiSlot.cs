using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightLingQiSlot : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
	public static bool IgnoreEffect;

	public GameObject LingQiCountObj;

	public Text LingQiCountText;

	public Image LingQiImage;

	public GameObject HighlightObj;

	private bool nowShowCount = true;

	[SerializeField]
	private LingQiType lingQiType = LingQiType.Count;

	protected float lingQiTweenTime = 1f;

	private int lingQiCount;

	private bool isLock;

	public LingQiType LingQiType
	{
		get
		{
			return lingQiType;
		}
		set
		{
			lingQiType = value;
			if (lingQiType != LingQiType.Count)
			{
				SetLingQiSprite(0);
			}
			else
			{
				LingQiImage.sprite = null;
			}
		}
	}

	public int LingQiCount
	{
		get
		{
			return lingQiCount;
		}
		set
		{
			if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			int num = value;
			if (num < 0)
			{
				if (this is UIFightLingQiPlayerSlot)
				{
					Debug.LogError((object)$"{LingQiType}被设置为{num}");
				}
				num = 0;
			}
			int change = num - lingQiCount;
			lingQiCount = num;
			OnLingQiCountChanged(change);
		}
	}

	public int SpriteIndex
	{
		get
		{
			if (LingQiType == LingQiType.Count)
			{
				return -1;
			}
			if (LingQiCount > 0 && LingQiCount <= 10)
			{
				return 0;
			}
			if (LingQiCount > 10 && LingQiCount <= 30)
			{
				return 1;
			}
			if (LingQiCount > 30)
			{
				return 2;
			}
			return -1;
		}
	}

	public bool IsLock
	{
		get
		{
			return isLock;
		}
		set
		{
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			isLock = value;
			if (LingQiType != LingQiType.Count)
			{
				if (isLock)
				{
					SetLingQiSprite(1);
				}
				else
				{
					SetLingQiSprite(0);
				}
			}
			else
			{
				LingQiImage.sprite = null;
				((Graphic)LingQiImage).color = new Color(1f, 1f, 1f, 0f);
			}
		}
	}

	public void SetLingQiSprite(int state)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int spriteIndex = SpriteIndex;
		if (spriteIndex < 0)
		{
			LingQiImage.sprite = null;
			((Graphic)LingQiImage).color = new Color(1f, 1f, 1f, 0f);
			return;
		}
		((Graphic)LingQiImage).color = Color.white;
		Sprite sprite = null;
		switch (spriteIndex)
		{
		case 0:
			switch (state)
			{
			case 0:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Normal;
				break;
			case 1:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Lock;
				break;
			case 2:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Press;
				break;
			case 3:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Highlight;
				break;
			}
			break;
		case 1:
			switch (state)
			{
			case 0:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Normal2;
				break;
			case 1:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Lock2;
				break;
			case 2:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Press2;
				break;
			case 3:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Highlight2;
				break;
			}
			break;
		case 2:
			switch (state)
			{
			case 0:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Normal3;
				break;
			case 1:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Lock3;
				break;
			case 2:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Press3;
				break;
			case 3:
				sprite = UIFightPanel.Inst.LingQiImageDatas[(int)LingQiType].Highlight3;
				break;
			}
			break;
		}
		LingQiImage.sprite = sprite;
	}

	protected virtual void OnLingQiCountChanged(int change)
	{
		if (change != 0)
		{
			if (change > 0)
			{
				PlayAddLingQiAnim(change);
			}
			else
			{
				PlayRemoveLingQiAnim(Mathf.Abs(change));
			}
		}
		if (IsLock)
		{
			SetLingQiSprite(1);
		}
		else
		{
			SetLingQiSprite(0);
		}
	}

	protected virtual void PlayAddLingQiAnim(int count)
	{
	}

	protected virtual void PlayRemoveLingQiAnim(int count)
	{
	}

	public bool CanInteractive()
	{
		if (LingQiType != LingQiType.Count && !IsLock)
		{
			return true;
		}
		return false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (CanInteractive())
		{
			SetLingQiSprite(3);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (CanInteractive())
		{
			SetLingQiSprite(0);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (CanInteractive())
		{
			SetLingQiSprite(0);
			bool mouseButtonUp = Input.GetMouseButtonUp(0);
			bool mouseButtonUp2 = Input.GetMouseButtonUp(1);
			if (mouseButtonUp || mouseButtonUp2)
			{
				OnClick();
			}
			if (mouseButtonUp)
			{
				OnLeftClick();
			}
			if (mouseButtonUp2)
			{
				OnRightClick();
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (CanInteractive())
		{
			SetLingQiSprite(2);
		}
	}

	protected virtual void OnClick()
	{
	}

	protected virtual void OnLeftClick()
	{
	}

	protected virtual void OnRightClick()
	{
	}

	public virtual void SetNull()
	{
		LingQiType = LingQiType.Count;
		lingQiCount = 0;
		IsLock = false;
		LingQiCountText.text = "";
		SetLingQiCountShow(show: false);
		HighlightObj.SetActive(false);
	}

	public void SetLingQiCountShow(bool show)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (nowShowCount != show)
		{
			nowShowCount = show;
			if (nowShowCount)
			{
				LingQiCountObj.transform.localScale = Vector3.zero;
				ShortcutExtensions.DOScale(LingQiCountObj.transform, Vector3.one, 0.1f);
			}
			else
			{
				LingQiCountObj.transform.localScale = Vector3.one;
				ShortcutExtensions.DOScale(LingQiCountObj.transform, Vector3.zero, 0.1f);
			}
		}
	}

	public void PlayLingQiSound()
	{
		UIFightPanel.Inst.NeedPlayLingQiSound = true;
	}
}
