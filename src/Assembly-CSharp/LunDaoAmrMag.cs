using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class LunDaoAmrMag
{
	public class LunDaoAnimationController
	{
		private UnityAction playingAction;

		private UnityAction completeCallBack;

		private AnimatorUtils animatorUtils;

		private Animator animator;

		public LunDaoAnimationController(Animator animator, AnimatorUtils animatorUtils)
		{
			this.animator = animator;
			this.animatorUtils = animatorUtils;
		}

		public LunDaoAnimationController PlayingAction(UnityAction playingAction)
		{
			this.playingAction = playingAction;
			return this;
		}

		public LunDaoAnimationController CompleteAction(UnityAction completeCallBack)
		{
			this.completeCallBack = completeCallBack;
			return this;
		}

		public void Run()
		{
			animatorUtils.completeCallBack = completeCallBack;
			animator.Play("Start");
			playingAction.Invoke();
		}
	}

	public enum AnimationName
	{
		StartLunDao = 1
	}

	private LunDaoAnimationController controller;

	private Queue<Transform> heChengQueue = new Queue<Transform>();

	private Queue<Image> useCardQueue = new Queue<Image>();

	private Queue<List<Image>> completeLunTiQueue = new Queue<List<Image>>();

	private bool heChengLock = true;

	private bool useCardLock = true;

	private bool completeLunTiLock = true;

	public LunDaoAnimationController PlayStartLunDao()
	{
		GameObject obj = Object.Instantiate<GameObject>(LoadAnimation("StartLunDao"), ((Component)LunDaoManager.inst).transform);
		Animator component = obj.GetComponent<Animator>();
		AnimatorUtils component2 = obj.GetComponent<AnimatorUtils>();
		controller = new LunDaoAnimationController(component, component2);
		return controller;
	}

	public void AddHeCheng(Transform transform)
	{
		heChengQueue.Enqueue(transform);
		PlayHeCheng();
	}

	public void AddChuPai(int id)
	{
		Image component = Object.Instantiate<GameObject>(LoadAnimation("NpcUseCard"), LunDaoManager.inst.AnimatorPanel).GetComponent<Image>();
		component.sprite = LunDaoManager.inst.cardSprites[id - 1];
		useCardQueue.Enqueue(component);
		PlayChuPai();
	}

	public void AddCompleteLunTi(Image bg, Image img)
	{
		if ((Object)(object)img != (Object)null && (Object)(object)bg != (Object)null)
		{
			completeLunTiQueue.Enqueue(new List<Image> { bg, img });
			PlayCompleteLunTi();
		}
		else
		{
			((Component)img).gameObject.SetActive(true);
		}
	}

	private void PlayCompleteLunTi()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		if (completeLunTiLock && completeLunTiQueue.Count > 0)
		{
			completeLunTiLock = false;
			List<Image> list = completeLunTiQueue.Dequeue();
			Image val = list[0];
			Image obj = list[1];
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(val, new Color(1f, 1f, 1f, 1f), 0.5f));
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(obj, new Color(1f, 1f, 1f, 1f), 0.5f));
			TweenExtensions.Play<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(((Component)obj).transform, Vector3.one, 0.5f), (TweenCallback)delegate
			{
				completeLunTiLock = true;
				PlayCompleteLunTi();
			}));
		}
	}

	private void PlayChuPai()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		if (useCardLock && useCardQueue.Count > 0)
		{
			useCardLock = false;
			Image image = useCardQueue.Dequeue();
			((Component)image).gameObject.SetActive(true);
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(image, new Color(1f, 1f, 1f, 1f), 0.4f), (TweenCallback)delegate
			{
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(image, new Color(1f, 1f, 1f, 0f), 0.1f));
			}));
			TweenExtensions.Play<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(((Component)image).transform, 37f, 0.5f, false), (TweenCallback)delegate
			{
				useCardLock = true;
				Object.Destroy((Object)(object)((Component)image).gameObject);
				PlayChuPai();
			}), (Ease)20));
		}
	}

	private void PlayHeCheng()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		if (heChengLock && heChengQueue.Count > 0)
		{
			heChengLock = false;
			MusicMag.instance.PlayEffectMusic(17);
			Transform val = heChengQueue.Dequeue();
			GameObject val2 = Object.Instantiate<GameObject>(LoadAnimation("HeCheng"), LunDaoManager.inst.AnimatorPanel);
			val2.transform.position = new Vector3(val.position.x + 10f, val.position.y, val.position.z);
			val2.SetActive(false);
			LunDaoQiu component = ((Component)val).GetComponent<LunDaoQiu>();
			GameObject gameObject2 = Object.Instantiate<GameObject>(((Component)component.lunDaoQiuImage).gameObject, LunDaoManager.inst.AnimatorPanel);
			gameObject2.SetActive(false);
			Image component2 = gameObject2.GetComponent<Image>();
			((Graphic)component2).color = new Color(1f, 1f, 1f, 0f);
			Text componentInChildren = gameObject2.GetComponentInChildren<Text>();
			componentInChildren.text = (int.Parse(componentInChildren.text) - 1).ToString();
			gameObject2.transform.position = new Vector3(val.position.x + 321f, val.position.y, 0f);
			((Graphic)componentInChildren).color = new Color(1f, 1f, 1f, 0f);
			Animator component3 = val2.GetComponent<Animator>();
			val2.GetComponent<AnimatorUtils>().completeCallBack = (UnityAction)delegate
			{
				heChengLock = true;
				PlayHeCheng();
			};
			val2.SetActive(true);
			gameObject2.SetActive(true);
			component3.Play("Play");
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(component2, new Color(1f, 1f, 1f, 1f), 0.2f));
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(componentInChildren, new Color(1f, 1f, 1f, 1f), 0.2f));
			TweenExtensions.Play<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To((DOGetter<Vector3>)(() => gameObject2.transform.position), (DOSetter<Vector3>)delegate(Vector3 x)
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				gameObject2.transform.position = x;
			}, new Vector3(val.position.x, val.position.y, 0f), 0.2f), (TweenCallback)delegate
			{
				Object.Destroy((Object)(object)gameObject2);
			}));
		}
	}

	private GameObject LoadAnimation(string name)
	{
		return Resources.Load<GameObject>("NewUI/LunDao/DongHua/Prefab/" + name);
	}
}
