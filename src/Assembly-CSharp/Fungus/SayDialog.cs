using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E83 RID: 3715
	public class SayDialog : MonoBehaviour
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600693C RID: 26940 RVA: 0x002903AC File Offset: 0x0028E5AC
		// (set) Token: 0x0600693D RID: 26941 RVA: 0x002903B9 File Offset: 0x0028E5B9
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x002903C7 File Offset: 0x0028E5C7
		// (set) Token: 0x0600693F RID: 26943 RVA: 0x002903D4 File Offset: 0x0028E5D4
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06006940 RID: 26944 RVA: 0x002903E2 File Offset: 0x0028E5E2
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

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06006941 RID: 26945 RVA: 0x00290409 File Offset: 0x0028E609
		public virtual Image CharacterImage
		{
			get
			{
				return this.characterImage;
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x00290414 File Offset: 0x0028E614
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

		// Token: 0x06006943 RID: 26947 RVA: 0x002904B9 File Offset: 0x0028E6B9
		protected virtual void OnDestroy()
		{
			SayDialog.activeSayDialogs.Remove(this);
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x002904C8 File Offset: 0x0028E6C8
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

		// Token: 0x06006945 RID: 26949 RVA: 0x0029051C File Offset: 0x0028E71C
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

		// Token: 0x06006946 RID: 26950 RVA: 0x00290570 File Offset: 0x0028E770
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

		// Token: 0x06006947 RID: 26951 RVA: 0x002905C4 File Offset: 0x0028E7C4
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

		// Token: 0x06006948 RID: 26952 RVA: 0x00290620 File Offset: 0x0028E820
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

		// Token: 0x06006949 RID: 26953 RVA: 0x00290690 File Offset: 0x0028E890
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

		// Token: 0x0600694A RID: 26954 RVA: 0x00290767 File Offset: 0x0028E967
		protected virtual void ClearStoryText()
		{
			this.StoryText = "";
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600694B RID: 26955 RVA: 0x00290774 File Offset: 0x0028E974
		public Character SpeakingCharacter
		{
			get
			{
				return SayDialog.speakingCharacter;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600694C RID: 26956 RVA: 0x0029077B File Offset: 0x0028E97B
		// (set) Token: 0x0600694D RID: 26957 RVA: 0x00290782 File Offset: 0x0028E982
		public static SayDialog ActiveSayDialog { get; set; }

		// Token: 0x0600694E RID: 26958 RVA: 0x0029078C File Offset: 0x0028E98C
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

		// Token: 0x0600694F RID: 26959 RVA: 0x00290820 File Offset: 0x0028EA20
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

		// Token: 0x06006950 RID: 26960 RVA: 0x0028DCB8 File Offset: 0x0028BEB8
		public virtual void SetActive(bool state)
		{
			base.gameObject.SetActive(state);
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x00290908 File Offset: 0x0028EB08
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

		// Token: 0x06006952 RID: 26962 RVA: 0x00290B40 File Offset: 0x0028ED40
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

		// Token: 0x06006953 RID: 26963 RVA: 0x00290C74 File Offset: 0x0028EE74
		public virtual void SetCharacterName(string name, Color color)
		{
			if (this.NameText != null)
			{
				string text = this.stringSubstituter.SubstituteStrings(name);
				this.NameText = text;
				this.nameTextAdapter.SetTextColor(color);
			}
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x00290CAC File Offset: 0x0028EEAC
		public virtual void Say(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
		{
			base.StartCoroutine(this.DoSay(text, clearPrevious, waitForInput, fadeWhenDone, stopVoiceover, waitForVO, voiceOverClip, onComplete));
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x00290CD4 File Offset: 0x0028EED4
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

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06006956 RID: 26966 RVA: 0x00290D2B File Offset: 0x0028EF2B
		// (set) Token: 0x06006957 RID: 26967 RVA: 0x00290D33 File Offset: 0x0028EF33
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

		// Token: 0x06006958 RID: 26968 RVA: 0x00290D3C File Offset: 0x0028EF3C
		public virtual void Stop()
		{
			this.fadeWhenDone = true;
			this.GetWriter().Stop();
		}

		// Token: 0x06006959 RID: 26969 RVA: 0x00290D50 File Offset: 0x0028EF50
		public virtual void Clear()
		{
			this.ClearStoryText();
			base.StopAllCoroutines();
		}

		// Token: 0x04005935 RID: 22837
		[Tooltip("Duration to fade dialogue in/out")]
		[SerializeField]
		protected float fadeDuration = 0.25f;

		// Token: 0x04005936 RID: 22838
		[Tooltip("The continue button UI object")]
		[SerializeField]
		protected Button continueButton;

		// Token: 0x04005937 RID: 22839
		[Tooltip("The canvas UI object")]
		[SerializeField]
		protected Canvas dialogCanvas;

		// Token: 0x04005938 RID: 22840
		[Tooltip("The name text UI object")]
		[SerializeField]
		protected Text nameText;

		// Token: 0x04005939 RID: 22841
		[Tooltip("TextAdapter will search for appropriate output on this GameObject if nameText is null")]
		[SerializeField]
		protected GameObject nameTextGO;

		// Token: 0x0400593A RID: 22842
		protected TextAdapter nameTextAdapter = new TextAdapter();

		// Token: 0x0400593B RID: 22843
		[Tooltip("The story text UI object")]
		[SerializeField]
		protected Text storyText;

		// Token: 0x0400593C RID: 22844
		[Tooltip("TextAdapter will search for appropriate output on this GameObject if storyText is null")]
		[SerializeField]
		protected GameObject storyTextGO;

		// Token: 0x0400593D RID: 22845
		protected TextAdapter storyTextAdapter = new TextAdapter();

		// Token: 0x0400593E RID: 22846
		public List<Sprite> bgKuang = new List<Sprite>();

		// Token: 0x0400593F RID: 22847
		public Image panle;

		// Token: 0x04005940 RID: 22848
		[Tooltip("The character UI object")]
		[SerializeField]
		protected Image characterImage;

		// Token: 0x04005941 RID: 22849
		[Tooltip("The Face0Image UI object")]
		[SerializeField]
		protected GameObject FaceImage;

		// Token: 0x04005942 RID: 22850
		[Tooltip("The Face0Image UI object")]
		[SerializeField]
		protected GameObject FaceNolmeImage;

		// Token: 0x04005943 RID: 22851
		[Tooltip("The character UI object")]
		[SerializeField]
		protected Text chengHao;

		// Token: 0x04005944 RID: 22852
		[Tooltip("Adjust width of story text when Character Image is displayed (to avoid overlapping)")]
		[SerializeField]
		protected bool fitTextWithImage = true;

		// Token: 0x04005945 RID: 22853
		[Tooltip("Close any other open Say Dialogs when this one is active")]
		[SerializeField]
		protected bool closeOtherDialogs;

		// Token: 0x04005946 RID: 22854
		protected float startStoryTextHight;

		// Token: 0x04005947 RID: 22855
		protected float startStoryTextWidth;

		// Token: 0x04005948 RID: 22856
		protected float startStoryTextInset;

		// Token: 0x04005949 RID: 22857
		protected WriterAudio writerAudio;

		// Token: 0x0400594A RID: 22858
		protected Writer writer;

		// Token: 0x0400594B RID: 22859
		protected CanvasGroup canvasGroup;

		// Token: 0x0400594C RID: 22860
		protected bool fadeWhenDone = true;

		// Token: 0x0400594D RID: 22861
		protected float targetAlpha;

		// Token: 0x0400594E RID: 22862
		protected float fadeCoolDownTimer;

		// Token: 0x0400594F RID: 22863
		protected Sprite currentCharacterImage;

		// Token: 0x04005950 RID: 22864
		protected static Character speakingCharacter;

		// Token: 0x04005951 RID: 22865
		protected StringSubstituter stringSubstituter = new StringSubstituter(5);

		// Token: 0x04005952 RID: 22866
		protected static List<SayDialog> activeSayDialogs = new List<SayDialog>();
	}
}
