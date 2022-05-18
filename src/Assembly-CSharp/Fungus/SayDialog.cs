using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012F2 RID: 4850
	public class SayDialog : MonoBehaviour
	{
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600762F RID: 30255 RVA: 0x000507E2 File Offset: 0x0004E9E2
		// (set) Token: 0x06007630 RID: 30256 RVA: 0x000507EF File Offset: 0x0004E9EF
		public virtual string NameText
		{
			get
			{
				return this.nameTextAdapter.Text;
			}
			set
			{
				this.nameTextAdapter.Text = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06007631 RID: 30257 RVA: 0x000507FD File Offset: 0x0004E9FD
		// (set) Token: 0x06007632 RID: 30258 RVA: 0x0005080A File Offset: 0x0004EA0A
		public virtual string StoryText
		{
			get
			{
				return this.storyTextAdapter.Text;
			}
			set
			{
				this.storyTextAdapter.Text = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06007633 RID: 30259 RVA: 0x00050818 File Offset: 0x0004EA18
		public virtual RectTransform StoryTextRectTrans
		{
			get
			{
				if (!(this.storyText != null))
				{
					return this.storyTextGO.GetComponent<RectTransform>();
				}
				return this.storyText.rectTransform;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06007634 RID: 30260 RVA: 0x0005083F File Offset: 0x0004EA3F
		public virtual Image CharacterImage
		{
			get
			{
				return this.characterImage;
			}
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x002B26F8 File Offset: 0x002B08F8
		protected virtual void Awake()
		{
			if (!SayDialog.activeSayDialogs.Contains(this))
			{
				SayDialog.activeSayDialogs.Add(this);
			}
			this.nameTextAdapter.InitFromGameObject((this.nameText != null) ? this.nameText.gameObject : this.nameTextGO, false);
			this.storyTextAdapter.InitFromGameObject((this.storyText != null) ? this.storyText.gameObject : this.storyTextGO, false);
			if (this.currentCharacterImage == null)
			{
				this.SetCharacterImage(null, 0);
			}
			CanClickManager.Inst.SayDialogCache.Add(this);
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x00050847 File Offset: 0x0004EA47
		protected virtual void OnDestroy()
		{
			SayDialog.activeSayDialogs.Remove(this);
		}

		// Token: 0x06007637 RID: 30263 RVA: 0x002B27A0 File Offset: 0x002B09A0
		protected virtual Writer GetWriter()
		{
			if (this.writer != null)
			{
				return this.writer;
			}
			this.writer = base.GetComponent<Writer>();
			if (this.writer == null)
			{
				this.writer = base.gameObject.AddComponent<Writer>();
			}
			return this.writer;
		}

		// Token: 0x06007638 RID: 30264 RVA: 0x002B27F4 File Offset: 0x002B09F4
		protected virtual CanvasGroup GetCanvasGroup()
		{
			if (this.canvasGroup != null)
			{
				return this.canvasGroup;
			}
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			if (this.canvasGroup == null)
			{
				this.canvasGroup = base.gameObject.AddComponent<CanvasGroup>();
			}
			return this.canvasGroup;
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x002B2848 File Offset: 0x002B0A48
		protected virtual WriterAudio GetWriterAudio()
		{
			if (this.writerAudio != null)
			{
				return this.writerAudio;
			}
			this.writerAudio = base.GetComponent<WriterAudio>();
			if (this.writerAudio == null)
			{
				this.writerAudio = base.gameObject.AddComponent<WriterAudio>();
			}
			return this.writerAudio;
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x002B289C File Offset: 0x002B0A9C
		protected virtual void Start()
		{
			this.GetCanvasGroup().alpha = 0f;
			if (base.GetComponent<GraphicRaycaster>() == null)
			{
				base.gameObject.AddComponent<GraphicRaycaster>();
			}
			if (this.NameText == "")
			{
				this.SetCharacterName("", Color.black);
			}
		}

		// Token: 0x0600763B RID: 30267 RVA: 0x002B28F8 File Offset: 0x002B0AF8
		protected virtual void LateUpdate()
		{
			this.UpdateAlpha();
			if (this.continueButton != null)
			{
				this.continueButton.gameObject.SetActive(this.GetWriter().IsWaitingForInput);
			}
			if (PanelMamager.inst != null)
			{
				if (base.gameObject.activeSelf)
				{
					PanelMamager.inst.canOpenPanel = false;
					return;
				}
				PanelMamager.inst.canOpenPanel = true;
			}
		}

		// Token: 0x0600763C RID: 30268 RVA: 0x002B2968 File Offset: 0x002B0B68
		protected virtual void UpdateAlpha()
		{
			if (this.GetWriter().IsWriting)
			{
				this.targetAlpha = 1f;
				this.fadeCoolDownTimer = 0.1f;
			}
			else if (this.fadeWhenDone && Mathf.Approximately(this.fadeCoolDownTimer, 0f))
			{
				this.targetAlpha = 0f;
			}
			else
			{
				this.fadeCoolDownTimer = Mathf.Max(0f, this.fadeCoolDownTimer - Time.deltaTime);
			}
			CanvasGroup canvasGroup = this.GetCanvasGroup();
			if (this.fadeDuration <= 0f)
			{
				canvasGroup.alpha = this.targetAlpha;
				return;
			}
			float num = 1f / this.fadeDuration * Time.deltaTime;
			float num2 = Mathf.MoveTowards(canvasGroup.alpha, this.targetAlpha, num);
			canvasGroup.alpha = num2;
			if (num2 <= 0f)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x00050855 File Offset: 0x0004EA55
		protected virtual void ClearStoryText()
		{
			this.StoryText = "";
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600763E RID: 30270 RVA: 0x00050862 File Offset: 0x0004EA62
		public Character SpeakingCharacter
		{
			get
			{
				return SayDialog.speakingCharacter;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600763F RID: 30271 RVA: 0x00050869 File Offset: 0x0004EA69
		// (set) Token: 0x06007640 RID: 30272 RVA: 0x00050870 File Offset: 0x0004EA70
		public static SayDialog ActiveSayDialog { get; set; }

		// Token: 0x06007641 RID: 30273 RVA: 0x002B2A40 File Offset: 0x002B0C40
		public static SayDialog GetSayDialog()
		{
			if (SayDialog.ActiveSayDialog == null)
			{
				SayDialog sayDialog = null;
				if (SayDialog.activeSayDialogs.Count > 0)
				{
					sayDialog = SayDialog.activeSayDialogs[0];
				}
				if (sayDialog != null)
				{
					SayDialog.ActiveSayDialog = sayDialog;
				}
				if (SayDialog.ActiveSayDialog == null)
				{
					GameObject gameObject = Resources.Load<GameObject>("Prefabs/SayDialog");
					if (gameObject != null)
					{
						GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
						gameObject2.SetActive(false);
						gameObject2.name = "SayDialog";
						SayDialog.ActiveSayDialog = gameObject2.GetComponent<SayDialog>();
						Object.DontDestroyOnLoad(gameObject2);
					}
				}
			}
			return SayDialog.ActiveSayDialog;
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x002B2AD4 File Offset: 0x002B0CD4
		public static void StopPortraitTweens()
		{
			List<Character> activeCharacters = Character.ActiveCharacters;
			for (int i = 0; i < activeCharacters.Count; i++)
			{
				Character character = activeCharacters[i];
				if (character.State.portraitImage != null && LeanTween.isTweening(character.State.portraitImage.gameObject))
				{
					LeanTween.cancel(character.State.portraitImage.gameObject, true);
					PortraitController.SetRectTransform(character.State.portraitImage.rectTransform, character.State.position);
					if (character.State.dimmed)
					{
						character.State.portraitImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
					}
					else
					{
						character.State.portraitImage.color = Color.white;
					}
				}
			}
		}

		// Token: 0x06007643 RID: 30275 RVA: 0x000500DC File Offset: 0x0004E2DC
		public virtual void SetActive(bool state)
		{
			base.gameObject.SetActive(state);
		}

		// Token: 0x06007644 RID: 30276 RVA: 0x002B2BBC File Offset: 0x002B0DBC
		public virtual void SetCharacter(Character character, int characterID = 0)
		{
			this.nameText.color = new Color(1f, 0.9843137f, 0.87058824f);
			if (character == null)
			{
				if (this.characterImage != null)
				{
					this.characterImage.gameObject.SetActive(false);
				}
				if (this.NameText != null)
				{
					JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(characterID);
					if (jsonData.instance != null && jsonData.instance.AvatarRandomJsonData.HasField(string.Concat(characterID)))
					{
						if (characterID == 1)
						{
							this.NameText = Tools.instance.getPlayer().name + "：";
						}
						else
						{
							this.NameText = ((wuJiangBangDing == null) ? Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(characterID)]["Name"].str) : Tools.Code64(wuJiangBangDing["Name"].str)) + "：";
						}
						this.chengHao.text = ((wuJiangBangDing == null) ? Tools.getMonstarTitle(characterID) : Tools.Code64(wuJiangBangDing["Title"].str));
					}
					else
					{
						this.NameText = "";
					}
				}
				SayDialog.speakingCharacter = null;
				return;
			}
			Character character2 = SayDialog.speakingCharacter;
			SayDialog.speakingCharacter = character;
			this.chengHao.text = "";
			List<Stage> activeStages = Stage.ActiveStages;
			for (int i = 0; i < activeStages.Count; i++)
			{
				Stage stage = activeStages[i];
				if (stage.DimPortraits)
				{
					List<Character> charactersOnStage = stage.CharactersOnStage;
					for (int j = 0; j < charactersOnStage.Count; j++)
					{
						Character character3 = charactersOnStage[j];
						if (character2 != SayDialog.speakingCharacter)
						{
							if (character3 != null && !character3.Equals(SayDialog.speakingCharacter))
							{
								stage.SetDimmed(character3, true);
							}
							else
							{
								stage.SetDimmed(character3, false);
							}
						}
					}
				}
			}
			string objectName = character.NameText;
			if (objectName == "")
			{
				objectName = character.GetObjectName();
			}
			this.SetCharacterName(objectName, character.NameColor);
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x002B2DF4 File Offset: 0x002B0FF4
		public virtual void SetCharacterImage(Sprite image, int characterID = 0)
		{
			if (this.characterImage == null)
			{
				return;
			}
			if (!(image != null) && characterID == 0)
			{
				this.FaceNolmeImage.gameObject.SetActive(false);
				this.FaceImage.SetActive(false);
				this.characterImage.gameObject.SetActive(false);
				this.chengHao.gameObject.SetActive(false);
				this.panle.sprite = this.bgKuang[1];
				return;
			}
			this.characterImage.sprite = image;
			this.characterImage.gameObject.SetActive(true);
			this.currentCharacterImage = image;
			this.panle.sprite = this.bgKuang[0];
			this.chengHao.gameObject.SetActive(true);
			if (image == null)
			{
				this.characterImage.gameObject.SetActive(false);
				this.FaceImage.SetActive(true);
				JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(characterID);
				this.FaceImage.GetComponent<PlayerSetRandomFace>().randomAvatar((wuJiangBangDing == null) ? characterID : ((int)wuJiangBangDing["Image"].n));
				return;
			}
			this.FaceImage.SetActive(false);
		}

		// Token: 0x06007646 RID: 30278 RVA: 0x002B2F28 File Offset: 0x002B1128
		public virtual void SetCharacterName(string name, Color color)
		{
			if (this.NameText != null)
			{
				string text = this.stringSubstituter.SubstituteStrings(name);
				this.NameText = text;
				this.nameTextAdapter.SetTextColor(color);
			}
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x002B2F60 File Offset: 0x002B1160
		public virtual void Say(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
		{
			base.StartCoroutine(this.DoSay(text, clearPrevious, waitForInput, fadeWhenDone, stopVoiceover, waitForVO, voiceOverClip, onComplete));
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x002B2F88 File Offset: 0x002B1188
		public virtual IEnumerator DoSay(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
		{
			Writer writer = this.GetWriter();
			if (writer.IsWriting || writer.IsWaitingForInput)
			{
				writer.Stop();
				while (writer.IsWriting || writer.IsWaitingForInput)
				{
					yield return null;
				}
			}
			if (this.closeOtherDialogs)
			{
				for (int i = 0; i < SayDialog.activeSayDialogs.Count; i++)
				{
					SayDialog sayDialog = SayDialog.activeSayDialogs[i];
					if (sayDialog.gameObject != base.gameObject)
					{
						sayDialog.SetActive(false);
					}
				}
			}
			base.gameObject.SetActive(true);
			this.fadeWhenDone = fadeWhenDone;
			AudioClip audioClip = null;
			if (voiceOverClip != null)
			{
				this.GetWriterAudio().OnVoiceover(voiceOverClip);
			}
			else if (SayDialog.speakingCharacter != null)
			{
				audioClip = SayDialog.speakingCharacter.SoundEffect;
			}
			writer.AttachedWriterAudio = this.writerAudio;
			yield return base.StartCoroutine(writer.Write(text, clearPrevious, waitForInput, stopVoiceover, waitForVO, audioClip, onComplete));
			yield break;
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06007649 RID: 30281 RVA: 0x00050878 File Offset: 0x0004EA78
		// (set) Token: 0x0600764A RID: 30282 RVA: 0x00050880 File Offset: 0x0004EA80
		public virtual bool FadeWhenDone
		{
			get
			{
				return this.fadeWhenDone;
			}
			set
			{
				this.fadeWhenDone = value;
			}
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x00050889 File Offset: 0x0004EA89
		public virtual void Stop()
		{
			this.fadeWhenDone = true;
			this.GetWriter().Stop();
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x0005089D File Offset: 0x0004EA9D
		public virtual void Clear()
		{
			this.ClearStoryText();
			base.StopAllCoroutines();
		}

		// Token: 0x04006707 RID: 26375
		[Tooltip("Duration to fade dialogue in/out")]
		[SerializeField]
		protected float fadeDuration = 0.25f;

		// Token: 0x04006708 RID: 26376
		[Tooltip("The continue button UI object")]
		[SerializeField]
		protected Button continueButton;

		// Token: 0x04006709 RID: 26377
		[Tooltip("The canvas UI object")]
		[SerializeField]
		protected Canvas dialogCanvas;

		// Token: 0x0400670A RID: 26378
		[Tooltip("The name text UI object")]
		[SerializeField]
		protected Text nameText;

		// Token: 0x0400670B RID: 26379
		[Tooltip("TextAdapter will search for appropriate output on this GameObject if nameText is null")]
		[SerializeField]
		protected GameObject nameTextGO;

		// Token: 0x0400670C RID: 26380
		protected TextAdapter nameTextAdapter = new TextAdapter();

		// Token: 0x0400670D RID: 26381
		[Tooltip("The story text UI object")]
		[SerializeField]
		protected Text storyText;

		// Token: 0x0400670E RID: 26382
		[Tooltip("TextAdapter will search for appropriate output on this GameObject if storyText is null")]
		[SerializeField]
		protected GameObject storyTextGO;

		// Token: 0x0400670F RID: 26383
		protected TextAdapter storyTextAdapter = new TextAdapter();

		// Token: 0x04006710 RID: 26384
		public List<Sprite> bgKuang = new List<Sprite>();

		// Token: 0x04006711 RID: 26385
		public Image panle;

		// Token: 0x04006712 RID: 26386
		[Tooltip("The character UI object")]
		[SerializeField]
		protected Image characterImage;

		// Token: 0x04006713 RID: 26387
		[Tooltip("The Face0Image UI object")]
		[SerializeField]
		protected GameObject FaceImage;

		// Token: 0x04006714 RID: 26388
		[Tooltip("The Face0Image UI object")]
		[SerializeField]
		protected GameObject FaceNolmeImage;

		// Token: 0x04006715 RID: 26389
		[Tooltip("The character UI object")]
		[SerializeField]
		protected Text chengHao;

		// Token: 0x04006716 RID: 26390
		[Tooltip("Adjust width of story text when Character Image is displayed (to avoid overlapping)")]
		[SerializeField]
		protected bool fitTextWithImage = true;

		// Token: 0x04006717 RID: 26391
		[Tooltip("Close any other open Say Dialogs when this one is active")]
		[SerializeField]
		protected bool closeOtherDialogs;

		// Token: 0x04006718 RID: 26392
		protected float startStoryTextHight;

		// Token: 0x04006719 RID: 26393
		protected float startStoryTextWidth;

		// Token: 0x0400671A RID: 26394
		protected float startStoryTextInset;

		// Token: 0x0400671B RID: 26395
		protected WriterAudio writerAudio;

		// Token: 0x0400671C RID: 26396
		protected Writer writer;

		// Token: 0x0400671D RID: 26397
		protected CanvasGroup canvasGroup;

		// Token: 0x0400671E RID: 26398
		protected bool fadeWhenDone = true;

		// Token: 0x0400671F RID: 26399
		protected float targetAlpha;

		// Token: 0x04006720 RID: 26400
		protected float fadeCoolDownTimer;

		// Token: 0x04006721 RID: 26401
		protected Sprite currentCharacterImage;

		// Token: 0x04006722 RID: 26402
		protected static Character speakingCharacter;

		// Token: 0x04006723 RID: 26403
		protected StringSubstituter stringSubstituter = new StringSubstituter(5);

		// Token: 0x04006724 RID: 26404
		protected static List<SayDialog> activeSayDialogs = new List<SayDialog>();
	}
}
