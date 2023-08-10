using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class UIInvFilterBtn : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public enum BtnState
	{
		Normal,
		Active,
		HoverNoActive
	}

	private UIInventory inventory;

	private Button button;

	public Image BGImage;

	public Image SanJiao;

	public Text ShowText;

	public string DeafaultName;

	public Color TextNormalColor;

	public Color TextActiveColor;

	public Sprite NormalSprite;

	public Sprite ActiveSprite;

	public bool IsSelected;

	private BtnState state;

	public int BtnData;

	public bool IsNew;

	public BtnState State
	{
		get
		{
			return state;
		}
		set
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			if (state == value)
			{
				return;
			}
			state = value;
			switch (state)
			{
			case BtnState.Normal:
				BGImage.sprite = NormalSprite;
				((Graphic)ShowText).color = TextNormalColor;
				if ((Object)(object)SanJiao != (Object)null)
				{
					ShortcutExtensions.DOLocalRotate(((Component)SanJiao).transform, Vector3.zero, 0.3f, (RotateMode)0);
				}
				break;
			case BtnState.Active:
				IsSelected = true;
				BGImage.sprite = ActiveSprite;
				((Graphic)ShowText).color = TextActiveColor;
				if ((Object)(object)SanJiao != (Object)null)
				{
					ShortcutExtensions.DOLocalRotate(((Component)SanJiao).transform, new Vector3(0f, 0f, 180f), 0.3f, (RotateMode)0);
				}
				break;
			case BtnState.HoverNoActive:
				BGImage.sprite = ActiveSprite;
				((Graphic)ShowText).color = TextActiveColor;
				break;
			}
		}
	}

	public void ClearListener()
	{
		IsNew = true;
		if ((Object)(object)button == (Object)null)
		{
			button = ((Component)this).GetComponent<Button>();
		}
		((UnityEventBase)button.onClick).RemoveAllListeners();
	}

	public void AddClickEvent(UnityAction call)
	{
		if ((Object)(object)button == (Object)null)
		{
			button = ((Component)this).GetComponent<Button>();
		}
		((UnityEvent)button.onClick).AddListener(call);
	}

	private void Awake()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (!IsNew)
		{
			inventory = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<UIInventory>();
			button = ((Component)this).GetComponent<Button>();
			((UnityEvent)button.onClick).AddListener((UnityAction)delegate
			{
				inventory.UITmpValue = BtnData;
				inventory.OnFilterBtnClick(this);
			});
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (State == BtnState.Normal)
		{
			State = BtnState.HoverNoActive;
		}
		if (BtnData < 0)
		{
			MusicMag.instance.PlayEffectMusic(3);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (State == BtnState.HoverNoActive)
		{
			State = BtnState.Normal;
		}
	}
}
