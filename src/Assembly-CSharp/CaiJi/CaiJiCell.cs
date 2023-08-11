using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace CaiJi;

public class CaiJiCell : MonoBehaviour
{
	[SerializeField]
	private FpBtn Btn;

	public UIIconShow Item;

	private bool CanClick = true;

	private Sequence StopQuence;

	private MessageData data = new MessageData(0);

	public bool IsSelected;

	public CaiJiCell Init(int itemId)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		Item.SetItem(itemId);
		data.valueInt = itemId;
		((Component)this).gameObject.SetActive(true);
		IsSelected = false;
		Btn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (CaiJiUIMag.inst.IsMax)
			{
				if (CanClick)
				{
					CanClick = false;
					PlayerSharkeEffect();
				}
			}
			else
			{
				PlayerHideEffect();
				MessageMag.Instance.Send("CaiJi_Item_Select", data);
				IsSelected = true;
				UToolTip.CloseOldTooltip();
			}
		});
		return this;
	}

	private void PlayerShowEffect()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(((Component)Item).gameObject.transform, Vector2.op_Implicit(new Vector2(0.94f, 0.94f)), 0.05f), (TweenCallback)delegate
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(((Component)Item).gameObject.transform, Vector2.op_Implicit(new Vector2(1.05f, 1.05f)), 0.05f), (TweenCallback)delegate
			{
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				//IL_001f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0035: Unknown result type (might be due to invalid IL or missing references)
				//IL_003f: Expected O, but got Unknown
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(((Component)Item).gameObject.transform, Vector2.op_Implicit(new Vector2(0.98f, 0.98f)), 0.05f), (TweenCallback)delegate
				{
					ShortcutExtensions.DOScale(((Component)Item).gameObject.transform, 1f, 0.05f);
				});
			});
		});
	}

	private void PlayerHideEffect()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(((Component)Item).gameObject.transform, Vector2.op_Implicit(new Vector2(0.95f, 0.95f)), 0.05f), (TweenCallback)delegate
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(((Component)Item).gameObject.transform, Vector2.op_Implicit(Vector2.zero), 0f), (TweenCallback)delegate
			{
				((Component)this).gameObject.SetActive(false);
			});
		});
	}

	private void PlayerSharkeEffect()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(((Component)Item).gameObject.transform, -10f, 0.005f, false), (Ease)2), (TweenCallback)delegate
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(((Component)Item).gameObject.transform, 9f, 0.1f, false), (Ease)2), (TweenCallback)delegate
			{
				//IL_002d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0037: Expected O, but got Unknown
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(((Component)Item).gameObject.transform, -2f, 0.05f, false), (Ease)2), (TweenCallback)delegate
				{
					//IL_002d: Unknown result type (might be due to invalid IL or missing references)
					//IL_0037: Expected O, but got Unknown
					TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(((Component)Item).gameObject.transform, 0f, 0.025f, false), (Ease)2), (TweenCallback)delegate
					{
						CanClick = true;
					});
				});
			});
		});
	}

	public void Show()
	{
		((Component)this).gameObject.SetActive(true);
		PlayerShowEffect();
		IsSelected = false;
	}
}
