using System;
using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012EA RID: 4842
	public class PortraitController : MonoBehaviour
	{
		// Token: 0x060075D9 RID: 30169 RVA: 0x000504E6 File Offset: 0x0004E6E6
		protected virtual void Awake()
		{
			this.stage = base.GetComponentInParent<Stage>();
		}

		// Token: 0x060075DA RID: 30170 RVA: 0x002B1100 File Offset: 0x002AF300
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

		// Token: 0x060075DB RID: 30171 RVA: 0x002B1158 File Offset: 0x002AF358
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

		// Token: 0x060075DC RID: 30172 RVA: 0x002B1388 File Offset: 0x002AF588
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

		// Token: 0x060075DD RID: 30173 RVA: 0x000504F4 File Offset: 0x0004E6F4
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

		// Token: 0x060075DE RID: 30174 RVA: 0x002B146C File Offset: 0x002AF66C
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

		// Token: 0x060075DF RID: 30175 RVA: 0x002B1584 File Offset: 0x002AF784
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

		// Token: 0x060075E0 RID: 30176 RVA: 0x002B15C4 File Offset: 0x002AF7C4
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

		// Token: 0x060075E1 RID: 30177 RVA: 0x002B163C File Offset: 0x002AF83C
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

		// Token: 0x060075E2 RID: 30178 RVA: 0x002B16B8 File Offset: 0x002AF8B8
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

		// Token: 0x060075E3 RID: 30179 RVA: 0x002B1798 File Offset: 0x002AF998
		public virtual void MoveToFront(Character character)
		{
			this.MoveToFront(this.CleanPortraitOptions(new PortraitOptions(true)
			{
				character = character
			}));
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x002B17C0 File Offset: 0x002AF9C0
		public virtual void MoveToFront(PortraitOptions options)
		{
			options.character.State.portraitImage.transform.SetSiblingIndex(options.character.State.portraitImage.transform.parent.childCount);
			options.character.State.display = DisplayType.MoveToFront;
			this.FinishCommand(options);
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x002B1820 File Offset: 0x002AFA20
		public virtual void Show(Character character, string position)
		{
			PortraitOptions portraitOptions = new PortraitOptions(true);
			portraitOptions.character = character;
			portraitOptions.fromPosition = (portraitOptions.toPosition = this.stage.GetPosition(position));
			this.Show(portraitOptions);
		}

		// Token: 0x060075E6 RID: 30182 RVA: 0x002B1860 File Offset: 0x002AFA60
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

		// Token: 0x060075E7 RID: 30183 RVA: 0x00050511 File Offset: 0x0004E711
		public virtual void Show(Table optionsTable)
		{
			this.Show(PortraitUtil.ConvertTableToPortraitOptions(optionsTable, this.stage));
		}

		// Token: 0x060075E8 RID: 30184 RVA: 0x002B18BC File Offset: 0x002AFABC
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

		// Token: 0x060075E9 RID: 30185 RVA: 0x002B1CA4 File Offset: 0x002AFEA4
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

		// Token: 0x060075EA RID: 30186 RVA: 0x002B1D24 File Offset: 0x002AFF24
		public virtual void Hide(Character character)
		{
			this.Hide(new PortraitOptions(true)
			{
				character = character
			});
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x002B1D48 File Offset: 0x002AFF48
		public virtual void Hide(Character character, string toPosition)
		{
			this.Hide(new PortraitOptions(true)
			{
				character = character,
				toPosition = this.stage.GetPosition(toPosition),
				move = true
			});
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x00050525 File Offset: 0x0004E725
		public virtual void Hide(Table optionsTable)
		{
			this.Hide(PortraitUtil.ConvertTableToPortraitOptions(optionsTable, this.stage));
		}

		// Token: 0x060075ED RID: 30189 RVA: 0x002B1D84 File Offset: 0x002AFF84
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

		// Token: 0x060075EE RID: 30190 RVA: 0x002B1E90 File Offset: 0x002B0090
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

		// Token: 0x040066E4 RID: 26340
		protected float waitTimer;

		// Token: 0x040066E5 RID: 26341
		protected Stage stage;
	}
}
