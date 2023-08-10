using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace CaiJi;

public class CaiJiImpSlot : MonoBehaviour
{
	public bool IsNull = true;

	[SerializeField]
	private UIIconShow Item;

	[SerializeField]
	private FpBtn Btn;

	private MessageData data = new MessageData(0);

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		Btn.mouseUpEvent.AddListener(new UnityAction(CancelSelectItem));
	}

	public void PutItem(int itemId)
	{
		Item.SetItem(itemId);
		Item.CanDrag = false;
		((Component)Item).gameObject.SetActive(true);
		IsNull = false;
		PlayerShowEffect();
	}

	public void CancelSelectItem()
	{
		IsNull = true;
		Item.CloseTooltip();
		PlayerHideEffect();
		data.valueInt = Item.tmpItem.itemID;
		MessageMag.Instance.Send("CaiJi_Item_Cancel", data);
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
				((Component)Item).gameObject.SetActive(false);
			});
		});
	}
}
