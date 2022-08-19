using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x0200030D RID: 781
public class LunDaoAmrMag
{
	// Token: 0x06001B37 RID: 6967 RVA: 0x000C1F40 File Offset: 0x000C0140
	public LunDaoAmrMag.LunDaoAnimationController PlayStartLunDao()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.LoadAnimation("StartLunDao"), LunDaoManager.inst.transform);
		Animator component = gameObject.GetComponent<Animator>();
		AnimatorUtils component2 = gameObject.GetComponent<AnimatorUtils>();
		this.controller = new LunDaoAmrMag.LunDaoAnimationController(component, component2);
		return this.controller;
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x000C1F87 File Offset: 0x000C0187
	public void AddHeCheng(Transform transform)
	{
		this.heChengQueue.Enqueue(transform);
		this.PlayHeCheng();
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x000C1F9C File Offset: 0x000C019C
	public void AddChuPai(int id)
	{
		Image component = Object.Instantiate<GameObject>(this.LoadAnimation("NpcUseCard"), LunDaoManager.inst.AnimatorPanel).GetComponent<Image>();
		component.sprite = LunDaoManager.inst.cardSprites[id - 1];
		this.useCardQueue.Enqueue(component);
		this.PlayChuPai();
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x000C1FF4 File Offset: 0x000C01F4
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

	// Token: 0x06001B3B RID: 6971 RVA: 0x000C2044 File Offset: 0x000C0244
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

	// Token: 0x06001B3C RID: 6972 RVA: 0x000C210C File Offset: 0x000C030C
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

	// Token: 0x06001B3D RID: 6973 RVA: 0x000C21E4 File Offset: 0x000C03E4
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

	// Token: 0x06001B3E RID: 6974 RVA: 0x000C2457 File Offset: 0x000C0657
	private GameObject LoadAnimation(string name)
	{
		return Resources.Load<GameObject>("NewUI/LunDao/DongHua/Prefab/" + name);
	}

	// Token: 0x040015B0 RID: 5552
	private LunDaoAmrMag.LunDaoAnimationController controller;

	// Token: 0x040015B1 RID: 5553
	private Queue<Transform> heChengQueue = new Queue<Transform>();

	// Token: 0x040015B2 RID: 5554
	private Queue<Image> useCardQueue = new Queue<Image>();

	// Token: 0x040015B3 RID: 5555
	private Queue<List<Image>> completeLunTiQueue = new Queue<List<Image>>();

	// Token: 0x040015B4 RID: 5556
	private bool heChengLock = true;

	// Token: 0x040015B5 RID: 5557
	private bool useCardLock = true;

	// Token: 0x040015B6 RID: 5558
	private bool completeLunTiLock = true;

	// Token: 0x02001333 RID: 4915
	public class LunDaoAnimationController
	{
		// Token: 0x06007B5B RID: 31579 RVA: 0x002C1829 File Offset: 0x002BFA29
		public LunDaoAnimationController(Animator animator, AnimatorUtils animatorUtils)
		{
			this.animator = animator;
			this.animatorUtils = animatorUtils;
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x002C183F File Offset: 0x002BFA3F
		public LunDaoAmrMag.LunDaoAnimationController PlayingAction(UnityAction playingAction)
		{
			this.playingAction = playingAction;
			return this;
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x002C1849 File Offset: 0x002BFA49
		public LunDaoAmrMag.LunDaoAnimationController CompleteAction(UnityAction completeCallBack)
		{
			this.completeCallBack = completeCallBack;
			return this;
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x002C1853 File Offset: 0x002BFA53
		public void Run()
		{
			this.animatorUtils.completeCallBack = this.completeCallBack;
			this.animator.Play("Start");
			this.playingAction.Invoke();
		}

		// Token: 0x040067CF RID: 26575
		private UnityAction playingAction;

		// Token: 0x040067D0 RID: 26576
		private UnityAction completeCallBack;

		// Token: 0x040067D1 RID: 26577
		private AnimatorUtils animatorUtils;

		// Token: 0x040067D2 RID: 26578
		private Animator animator;
	}

	// Token: 0x02001334 RID: 4916
	public enum AnimationName
	{
		// Token: 0x040067D4 RID: 26580
		StartLunDao = 1
	}
}
