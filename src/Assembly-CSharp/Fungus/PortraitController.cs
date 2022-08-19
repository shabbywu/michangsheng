using System;
using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E7F RID: 3711
	public class PortraitController : MonoBehaviour
	{
		// Token: 0x060068F2 RID: 26866 RVA: 0x0028EBB2 File Offset: 0x0028CDB2
		protected virtual void Awake()
		{
			this.stage = base.GetComponentInParent<Stage>();
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x0028EBC0 File Offset: 0x0028CDC0
		protected virtual void FinishCommand(PortraitOptions options)
		{
			if (options.onComplete == null)
			{
				base.StartCoroutine(this.WaitUntilFinished(options.fadeDuration, null));
				return;
			}
			if (!options.waitUntilFinished)
			{
				options.onComplete();
				return;
			}
			base.StartCoroutine(this.WaitUntilFinished(options.fadeDuration, options.onComplete));
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x0028EC18 File Offset: 0x0028CE18
		protected virtual PortraitOptions CleanPortraitOptions(PortraitOptions options)
		{
			if (options.useDefaultSettings)
			{
				options.fadeDuration = this.stage.FadeDuration;
				options.moveDuration = this.stage.MoveDuration;
				options.shiftOffset = this.stage.ShiftOffset;
			}
			if (options.character.State.portrait == null)
			{
				options.character.State.portrait = options.character.ProfileSprite;
			}
			if (options.portrait == null)
			{
				options.portrait = options.character.State.portrait;
			}
			if (options.character.State.position == null)
			{
				options.character.State.position = this.stage.DefaultPosition.rectTransform;
			}
			if (options.toPosition == null)
			{
				options.toPosition = options.character.State.position;
			}
			if (options.replacedCharacter != null && options.replacedCharacter.State.position == null)
			{
				options.replacedCharacter.State.position = this.stage.DefaultPosition.rectTransform;
			}
			if (options.display == DisplayType.Replace)
			{
				options.toPosition = options.replacedCharacter.State.position;
			}
			if (options.fromPosition == null)
			{
				options.fromPosition = options.character.State.position;
			}
			if (!options.move)
			{
				options.fromPosition = options.toPosition;
			}
			if (options.display == DisplayType.Hide)
			{
				options.fromPosition = options.character.State.position;
			}
			if (options.character.State.facing == FacingDirection.None)
			{
				options.character.State.facing = options.character.PortraitsFace;
			}
			if (options.facing == FacingDirection.None)
			{
				options.facing = options.character.State.facing;
			}
			if (options.character.State.portraitImage == null)
			{
				this.CreatePortraitObject(options.character, options.fadeDuration);
			}
			return options;
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x0028EE48 File Offset: 0x0028D048
		protected virtual void CreatePortraitObject(Character character, float fadeDuration)
		{
			GameObject gameObject = new GameObject(character.name, new Type[]
			{
				typeof(RectTransform),
				typeof(CanvasRenderer),
				typeof(Image)
			});
			gameObject.transform.SetParent(this.stage.PortraitCanvas.transform, true);
			Image component = gameObject.GetComponent<Image>();
			component.preserveAspect = true;
			component.sprite = character.ProfileSprite;
			component.color = new Color(1f, 1f, 1f, 0f);
			float time = (fadeDuration > 0f) ? fadeDuration : float.Epsilon;
			LeanTween.alpha(component.transform as RectTransform, 1f, time).setEase(this.stage.FadeEaseType).setRecursive(false);
			character.State.portraitImage = component;
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x0028EF2B File Offset: 0x0028D12B
		protected virtual IEnumerator WaitUntilFinished(float duration, Action onComplete = null)
		{
			this.waitTimer = duration;
			while (this.waitTimer > 0f)
			{
				this.waitTimer -= Time.deltaTime;
				yield return null;
			}
			yield return new WaitForEndOfFrame();
			if (onComplete != null)
			{
				onComplete();
			}
			yield break;
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x0028EF48 File Offset: 0x0028D148
		protected virtual void SetupPortrait(PortraitOptions options)
		{
			PortraitController.SetRectTransform(options.character.State.portraitImage.rectTransform, options.fromPosition);
			if (options.character.State.facing != options.character.PortraitsFace)
			{
				options.character.State.portraitImage.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				options.character.State.portraitImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
			}
			if (options.facing != options.character.PortraitsFace)
			{
				options.character.State.portraitImage.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
				return;
			}
			options.character.State.portraitImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x0028F060 File Offset: 0x0028D260
		protected virtual void DoMoveTween(Character character, RectTransform fromPosition, RectTransform toPosition, float moveDuration, bool waitUntilFinished)
		{
			this.DoMoveTween(new PortraitOptions(true)
			{
				character = character,
				fromPosition = fromPosition,
				toPosition = toPosition,
				moveDuration = moveDuration,
				waitUntilFinished = waitUntilFinished
			});
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x0028F0A0 File Offset: 0x0028D2A0
		protected virtual void DoMoveTween(PortraitOptions options)
		{
			this.CleanPortraitOptions(options);
			float time = (options.moveDuration > 0f) ? options.moveDuration : float.Epsilon;
			LeanTween.move(options.character.State.portraitImage.gameObject, options.toPosition.position, time).setEase(this.stage.FadeEaseType);
			if (options.waitUntilFinished)
			{
				this.waitTimer = time;
			}
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x0028F118 File Offset: 0x0028D318
		public static void SetRectTransform(RectTransform oldRectTransform, RectTransform newRectTransform)
		{
			oldRectTransform.eulerAngles = newRectTransform.eulerAngles;
			oldRectTransform.position = newRectTransform.position;
			oldRectTransform.rotation = newRectTransform.rotation;
			oldRectTransform.anchoredPosition = newRectTransform.anchoredPosition;
			oldRectTransform.sizeDelta = newRectTransform.sizeDelta;
			oldRectTransform.anchorMax = newRectTransform.anchorMax;
			oldRectTransform.anchorMin = newRectTransform.anchorMin;
			oldRectTransform.pivot = newRectTransform.pivot;
			oldRectTransform.localScale = newRectTransform.localScale;
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x0028F194 File Offset: 0x0028D394
		public virtual void RunPortraitCommand(PortraitOptions options, Action onComplete)
		{
			this.waitTimer = 0f;
			if (options.character == null)
			{
				onComplete();
				return;
			}
			if (options.display == DisplayType.Replace && options.replacedCharacter == null)
			{
				onComplete();
				return;
			}
			if (options.display == DisplayType.Hide && !options.character.State.onScreen)
			{
				onComplete();
				return;
			}
			options = this.CleanPortraitOptions(options);
			options.onComplete = onComplete;
			switch (options.display)
			{
			case DisplayType.Show:
				this.Show(options);
				return;
			case DisplayType.Hide:
				this.Hide(options);
				return;
			case DisplayType.Replace:
				this.Show(options);
				this.Hide(options.replacedCharacter, options.replacedCharacter.State.position.name);
				return;
			case DisplayType.MoveToFront:
				this.MoveToFront(options);
				return;
			default:
				return;
			}
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x0028F274 File Offset: 0x0028D474
		public virtual void MoveToFront(Character character)
		{
			this.MoveToFront(this.CleanPortraitOptions(new PortraitOptions(true)
			{
				character = character
			}));
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x0028F29C File Offset: 0x0028D49C
		public virtual void MoveToFront(PortraitOptions options)
		{
			options.character.State.portraitImage.transform.SetSiblingIndex(options.character.State.portraitImage.transform.parent.childCount);
			options.character.State.display = DisplayType.MoveToFront;
			this.FinishCommand(options);
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x0028F2FC File Offset: 0x0028D4FC
		public virtual void Show(Character character, string position)
		{
			PortraitOptions portraitOptions = new PortraitOptions(true);
			portraitOptions.character = character;
			portraitOptions.fromPosition = (portraitOptions.toPosition = this.stage.GetPosition(position));
			this.Show(portraitOptions);
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x0028F33C File Offset: 0x0028D53C
		public virtual void Show(Character character, string portrait, string fromPosition, string toPosition)
		{
			this.Show(new PortraitOptions(true)
			{
				character = character,
				portrait = character.GetPortrait(portrait),
				fromPosition = this.stage.GetPosition(fromPosition),
				toPosition = this.stage.GetPosition(toPosition),
				move = true
			});
		}

		// Token: 0x06006900 RID: 26880 RVA: 0x0028F397 File Offset: 0x0028D597
		public virtual void Show(Table optionsTable)
		{
			this.Show(PortraitUtil.ConvertTableToPortraitOptions(optionsTable, this.stage));
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x0028F3AC File Offset: 0x0028D5AC
		public virtual void Show(PortraitOptions options)
		{
			options = this.CleanPortraitOptions(options);
			if (options.shiftIntoPlace)
			{
				options.fromPosition = Object.Instantiate<RectTransform>(options.toPosition);
				if (options.offset == PositionOffset.OffsetLeft)
				{
					options.fromPosition.anchoredPosition = new Vector2(options.fromPosition.anchoredPosition.x - Mathf.Abs(options.shiftOffset.x), options.fromPosition.anchoredPosition.y - Mathf.Abs(options.shiftOffset.y));
				}
				else if (options.offset == PositionOffset.OffsetRight)
				{
					options.fromPosition.anchoredPosition = new Vector2(options.fromPosition.anchoredPosition.x + Mathf.Abs(options.shiftOffset.x), options.fromPosition.anchoredPosition.y + Mathf.Abs(options.shiftOffset.y));
				}
				else
				{
					options.fromPosition.anchoredPosition = new Vector2(options.fromPosition.anchoredPosition.x, options.fromPosition.anchoredPosition.y);
				}
			}
			this.SetupPortrait(options);
			float time = (options.fadeDuration > 0f) ? options.fadeDuration : float.Epsilon;
			if (options.character.State.portraitImage != null && options.character.State.portraitImage.sprite != null)
			{
				GameObject tempGO = Object.Instantiate<GameObject>(options.character.State.portraitImage.gameObject);
				tempGO.transform.SetParent(options.character.State.portraitImage.transform, false);
				tempGO.transform.localPosition = Vector3.zero;
				tempGO.transform.localScale = options.character.State.position.localScale;
				Image component = tempGO.GetComponent<Image>();
				component.sprite = options.character.State.portraitImage.sprite;
				component.preserveAspect = true;
				component.color = options.character.State.portraitImage.color;
				LeanTween.alpha(component.rectTransform, 0f, time).setEase(this.stage.FadeEaseType).setOnComplete(delegate()
				{
					Object.Destroy(tempGO);
				}).setRecursive(false);
			}
			if (options.character.State.portraitImage.sprite != options.portrait || options.character.State.portraitImage.color.a < 1f)
			{
				options.character.State.portraitImage.sprite = options.portrait;
				options.character.State.portraitImage.color = new Color(1f, 1f, 1f, 0f);
				LeanTween.alpha(options.character.State.portraitImage.rectTransform, 1f, time).setEase(this.stage.FadeEaseType).setRecursive(false);
			}
			this.DoMoveTween(options);
			this.FinishCommand(options);
			if (!this.stage.CharactersOnStage.Contains(options.character))
			{
				this.stage.CharactersOnStage.Add(options.character);
			}
			options.character.State.onScreen = true;
			options.character.State.display = DisplayType.Show;
			options.character.State.portrait = options.portrait;
			options.character.State.facing = options.facing;
			options.character.State.position = options.toPosition;
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x0028F794 File Offset: 0x0028D994
		public virtual void ShowPortrait(Character character, string portrait)
		{
			PortraitOptions portraitOptions = new PortraitOptions(true);
			portraitOptions.character = character;
			portraitOptions.portrait = character.GetPortrait(portrait);
			if (character.State.position == null)
			{
				portraitOptions.toPosition = (portraitOptions.fromPosition = this.stage.GetPosition("middle"));
			}
			else
			{
				portraitOptions.fromPosition = (portraitOptions.toPosition = character.State.position);
			}
			this.Show(portraitOptions);
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x0028F814 File Offset: 0x0028DA14
		public virtual void Hide(Character character)
		{
			this.Hide(new PortraitOptions(true)
			{
				character = character
			});
		}

		// Token: 0x06006904 RID: 26884 RVA: 0x0028F838 File Offset: 0x0028DA38
		public virtual void Hide(Character character, string toPosition)
		{
			this.Hide(new PortraitOptions(true)
			{
				character = character,
				toPosition = this.stage.GetPosition(toPosition),
				move = true
			});
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x0028F873 File Offset: 0x0028DA73
		public virtual void Hide(Table optionsTable)
		{
			this.Hide(PortraitUtil.ConvertTableToPortraitOptions(optionsTable, this.stage));
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x0028F888 File Offset: 0x0028DA88
		public virtual void Hide(PortraitOptions options)
		{
			this.CleanPortraitOptions(options);
			if (options.character.State.display == DisplayType.None)
			{
				return;
			}
			this.SetupPortrait(options);
			float time = (options.fadeDuration > 0f) ? options.fadeDuration : float.Epsilon;
			LeanTween.alpha(options.character.State.portraitImage.rectTransform, 0f, time).setEase(this.stage.FadeEaseType).setRecursive(false);
			this.DoMoveTween(options);
			this.stage.CharactersOnStage.Remove(options.character);
			options.character.State.onScreen = false;
			options.character.State.portrait = options.portrait;
			options.character.State.facing = options.facing;
			options.character.State.position = options.toPosition;
			options.character.State.display = DisplayType.Hide;
			this.FinishCommand(options);
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x0028F994 File Offset: 0x0028DB94
		public virtual void SetDimmed(Character character, bool dimmedState)
		{
			if (character.State.dimmed == dimmedState)
			{
				return;
			}
			character.State.dimmed = dimmedState;
			Color to = dimmedState ? this.stage.DimColor : Color.white;
			float time = (this.stage.FadeDuration > 0f) ? this.stage.FadeDuration : float.Epsilon;
			LeanTween.color(character.State.portraitImage.rectTransform, to, time).setEase(this.stage.FadeEaseType).setRecursive(false);
		}

		// Token: 0x0400591C RID: 22812
		protected float waitTimer;

		// Token: 0x0400591D RID: 22813
		protected Stage stage;
	}
}
