using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x0200046A RID: 1130
public class LunDaoAmrMag
{
	// Token: 0x06001E5D RID: 7773 RVA: 0x0010763C File Offset: 0x0010583C
	public LunDaoAmrMag.LunDaoAnimationController PlayStartLunDao()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.LoadAnimation("StartLunDao"), LunDaoManager.inst.transform);
		Animator component = gameObject.GetComponent<Animator>();
		AnimatorUtils component2 = gameObject.GetComponent<AnimatorUtils>();
		this.controller = new LunDaoAmrMag.LunDaoAnimationController(component, component2);
		return this.controller;
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x000192D6 File Offset: 0x000174D6
	public void AddHeCheng(Transform transform)
	{
		this.heChengQueue.Enqueue(transform);
		this.PlayHeCheng();
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x00107684 File Offset: 0x00105884
	public void AddChuPai(int id)
	{
		Image component = Object.Instantiate<GameObject>(this.LoadAnimation("NpcUseCard"), LunDaoManager.inst.AnimatorPanel).GetComponent<Image>();
		component.sprite = LunDaoManager.inst.cardSprites[id - 1];
		this.useCardQueue.Enqueue(component);
		this.PlayChuPai();
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x001076DC File Offset: 0x001058DC
	public void AddCompleteLunTi(Image bg, Image img)
	{
		if (img != null && bg != null)
		{
			this.completeLunTiQueue.Enqueue(new List<Image>
			{
				bg,
				img
			});
			this.PlayCompleteLunTi();
			return;
		}
		img.gameObject.SetActive(true);
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x0010772C File Offset: 0x0010592C
	private void PlayCompleteLunTi()
	{
		if (this.completeLunTiLock && this.completeLunTiQueue.Count > 0)
		{
			this.completeLunTiLock = false;
			List<Image> list = this.completeLunTiQueue.Dequeue();
			Image image = list[0];
			Image image2 = list[1];
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(image, new Color(1f, 1f, 1f, 1f), 0.5f));
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(image2, new Color(1f, 1f, 1f, 1f), 0.5f));
			TweenExtensions.Play<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(image2.transform, Vector3.one, 0.5f), delegate()
			{
				this.completeLunTiLock = true;
				this.PlayCompleteLunTi();
			}));
		}
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x001077F4 File Offset: 0x001059F4
	private void PlayChuPai()
	{
		if (this.useCardLock && this.useCardQueue.Count > 0)
		{
			this.useCardLock = false;
			Image image = this.useCardQueue.Dequeue();
			image.gameObject.SetActive(true);
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(image, new Color(1f, 1f, 1f, 1f), 0.4f), delegate()
			{
				TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(image, new Color(1f, 1f, 1f, 0f), 0.1f));
			}));
			TweenExtensions.Play<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(image.transform, 37f, 0.5f, false), delegate()
			{
				this.useCardLock = true;
				Object.Destroy(image.gameObject);
				this.PlayChuPai();
			}), 20));
		}
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x001078CC File Offset: 0x00105ACC
	private void PlayHeCheng()
	{
		if (this.heChengLock && this.heChengQueue.Count > 0)
		{
			this.heChengLock = false;
			MusicMag.instance.PlayEffectMusic(17, 1f);
			Transform transform = this.heChengQueue.Dequeue();
			GameObject gameObject = Object.Instantiate<GameObject>(this.LoadAnimation("HeCheng"), LunDaoManager.inst.AnimatorPanel);
			gameObject.transform.position = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z);
			gameObject.SetActive(false);
			LunDaoQiu component = transform.GetComponent<LunDaoQiu>();
			GameObject gameObject2 = Object.Instantiate<GameObject>(component.lunDaoQiuImage.gameObject, LunDaoManager.inst.AnimatorPanel);
			gameObject2.SetActive(false);
			Image component2 = gameObject2.GetComponent<Image>();
			component2.color = new Color(1f, 1f, 1f, 0f);
			Text componentInChildren = gameObject2.GetComponentInChildren<Text>();
			componentInChildren.text = (int.Parse(componentInChildren.text) - 1).ToString();
			gameObject2.transform.position = new Vector3(transform.position.x + 321f, transform.position.y, 0f);
			componentInChildren.color = new Color(1f, 1f, 1f, 0f);
			Animator component3 = gameObject.GetComponent<Animator>();
			gameObject.GetComponent<AnimatorUtils>().completeCallBack = delegate()
			{
				this.heChengLock = true;
				this.PlayHeCheng();
			};
			gameObject.SetActive(true);
			gameObject2.SetActive(true);
			component3.Play("Play");
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(component2, new Color(1f, 1f, 1f, 1f), 0.2f));
			TweenExtensions.Play<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(componentInChildren, new Color(1f, 1f, 1f, 1f), 0.2f));
			TweenExtensions.Play<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => gameObject2.transform.position, delegate(Vector3 x)
			{
				gameObject2.transform.position = x;
			}, new Vector3(transform.position.x, transform.position.y, 0f), 0.2f), delegate()
			{
				Object.Destroy(gameObject2);
			}));
		}
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x000192EA File Offset: 0x000174EA
	private GameObject LoadAnimation(string name)
	{
		return Resources.Load<GameObject>("NewUI/LunDao/DongHua/Prefab/" + name);
	}

	// Token: 0x040019BD RID: 6589
	private LunDaoAmrMag.LunDaoAnimationController controller;

	// Token: 0x040019BE RID: 6590
	private Queue<Transform> heChengQueue = new Queue<Transform>();

	// Token: 0x040019BF RID: 6591
	private Queue<Image> useCardQueue = new Queue<Image>();

	// Token: 0x040019C0 RID: 6592
	private Queue<List<Image>> completeLunTiQueue = new Queue<List<Image>>();

	// Token: 0x040019C1 RID: 6593
	private bool heChengLock = true;

	// Token: 0x040019C2 RID: 6594
	private bool useCardLock = true;

	// Token: 0x040019C3 RID: 6595
	private bool completeLunTiLock = true;

	// Token: 0x0200046B RID: 1131
	public class LunDaoAnimationController
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x00019358 File Offset: 0x00017558
		public LunDaoAnimationController(Animator animator, AnimatorUtils animatorUtils)
		{
			this.animator = animator;
			this.animatorUtils = animatorUtils;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0001936E File Offset: 0x0001756E
		public LunDaoAmrMag.LunDaoAnimationController PlayingAction(UnityAction playingAction)
		{
			this.playingAction = playingAction;
			return this;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00019378 File Offset: 0x00017578
		public LunDaoAmrMag.LunDaoAnimationController CompleteAction(UnityAction completeCallBack)
		{
			this.completeCallBack = completeCallBack;
			return this;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00019382 File Offset: 0x00017582
		public void Run()
		{
			this.animatorUtils.completeCallBack = this.completeCallBack;
			this.animator.Play("Start");
			this.playingAction.Invoke();
		}

		// Token: 0x040019C4 RID: 6596
		private UnityAction playingAction;

		// Token: 0x040019C5 RID: 6597
		private UnityAction completeCallBack;

		// Token: 0x040019C6 RID: 6598
		private AnimatorUtils animatorUtils;

		// Token: 0x040019C7 RID: 6599
		private Animator animator;
	}

	// Token: 0x0200046C RID: 1132
	public enum AnimationName
	{
		// Token: 0x040019C9 RID: 6601
		StartLunDao = 1
	}
}
