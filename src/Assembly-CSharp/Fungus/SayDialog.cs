using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

public class SayDialog : MonoBehaviour
{
	[Tooltip("Duration to fade dialogue in/out")]
	[SerializeField]
	protected float fadeDuration = 0.25f;

	[Tooltip("The continue button UI object")]
	[SerializeField]
	protected Button continueButton;

	[Tooltip("The canvas UI object")]
	[SerializeField]
	protected Canvas dialogCanvas;

	[Tooltip("The name text UI object")]
	[SerializeField]
	protected Text nameText;

	[Tooltip("TextAdapter will search for appropriate output on this GameObject if nameText is null")]
	[SerializeField]
	protected GameObject nameTextGO;

	protected TextAdapter nameTextAdapter = new TextAdapter();

	[Tooltip("The story text UI object")]
	[SerializeField]
	protected Text storyText;

	[Tooltip("TextAdapter will search for appropriate output on this GameObject if storyText is null")]
	[SerializeField]
	protected GameObject storyTextGO;

	protected TextAdapter storyTextAdapter = new TextAdapter();

	public List<Sprite> bgKuang = new List<Sprite>();

	public Image panle;

	[Tooltip("The character UI object")]
	[SerializeField]
	protected Image characterImage;

	[Tooltip("The Face0Image UI object")]
	[SerializeField]
	protected GameObject FaceImage;

	[Tooltip("The Face0Image UI object")]
	[SerializeField]
	protected GameObject FaceNolmeImage;

	[Tooltip("The character UI object")]
	[SerializeField]
	protected Text chengHao;

	[Tooltip("Adjust width of story text when Character Image is displayed (to avoid overlapping)")]
	[SerializeField]
	protected bool fitTextWithImage = true;

	[Tooltip("Close any other open Say Dialogs when this one is active")]
	[SerializeField]
	protected bool closeOtherDialogs;

	protected float startStoryTextHight;

	protected float startStoryTextWidth;

	protected float startStoryTextInset;

	protected WriterAudio writerAudio;

	protected Writer writer;

	protected CanvasGroup canvasGroup;

	protected bool fadeWhenDone = true;

	protected float targetAlpha;

	protected float fadeCoolDownTimer;

	protected Sprite currentCharacterImage;

	protected static Character speakingCharacter;

	protected StringSubstituter stringSubstituter = new StringSubstituter();

	protected static List<SayDialog> activeSayDialogs = new List<SayDialog>();

	public virtual string NameText
	{
		get
		{
			return nameTextAdapter.Text;
		}
		set
		{
			nameTextAdapter.Text = value;
		}
	}

	public virtual string StoryText
	{
		get
		{
			return storyTextAdapter.Text;
		}
		set
		{
			storyTextAdapter.Text = value;
		}
	}

	public virtual RectTransform StoryTextRectTrans
	{
		get
		{
			if (!((Object)(object)storyText != (Object)null))
			{
				return storyTextGO.GetComponent<RectTransform>();
			}
			return ((Graphic)storyText).rectTransform;
		}
	}

	public virtual Image CharacterImage => characterImage;

	public Character SpeakingCharacter => speakingCharacter;

	public static SayDialog ActiveSayDialog { get; set; }

	public virtual bool FadeWhenDone
	{
		get
		{
			return fadeWhenDone;
		}
		set
		{
			fadeWhenDone = value;
		}
	}

	protected virtual void Awake()
	{
		if (!activeSayDialogs.Contains(this))
		{
			activeSayDialogs.Add(this);
		}
		nameTextAdapter.InitFromGameObject(((Object)(object)nameText != (Object)null) ? ((Component)nameText).gameObject : nameTextGO);
		storyTextAdapter.InitFromGameObject(((Object)(object)storyText != (Object)null) ? ((Component)storyText).gameObject : storyTextGO);
		if ((Object)(object)currentCharacterImage == (Object)null)
		{
			SetCharacterImage(null);
		}
		CanClickManager.Inst.SayDialogCache.Add(this);
	}

	protected virtual void OnDestroy()
	{
		activeSayDialogs.Remove(this);
	}

	protected virtual Writer GetWriter()
	{
		if ((Object)(object)writer != (Object)null)
		{
			return writer;
		}
		writer = ((Component)this).GetComponent<Writer>();
		if ((Object)(object)writer == (Object)null)
		{
			writer = ((Component)this).gameObject.AddComponent<Writer>();
		}
		return writer;
	}

	protected virtual CanvasGroup GetCanvasGroup()
	{
		if ((Object)(object)canvasGroup != (Object)null)
		{
			return canvasGroup;
		}
		canvasGroup = ((Component)this).GetComponent<CanvasGroup>();
		if ((Object)(object)canvasGroup == (Object)null)
		{
			canvasGroup = ((Component)this).gameObject.AddComponent<CanvasGroup>();
		}
		return canvasGroup;
	}

	protected virtual WriterAudio GetWriterAudio()
	{
		if ((Object)(object)writerAudio != (Object)null)
		{
			return writerAudio;
		}
		writerAudio = ((Component)this).GetComponent<WriterAudio>();
		if ((Object)(object)writerAudio == (Object)null)
		{
			writerAudio = ((Component)this).gameObject.AddComponent<WriterAudio>();
		}
		return writerAudio;
	}

	protected virtual void Start()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		GetCanvasGroup().alpha = 0f;
		if ((Object)(object)((Component)this).GetComponent<GraphicRaycaster>() == (Object)null)
		{
			((Component)this).gameObject.AddComponent<GraphicRaycaster>();
		}
		if (NameText == "")
		{
			SetCharacterName("", Color.black);
		}
	}

	protected virtual void LateUpdate()
	{
		UpdateAlpha();
		if ((Object)(object)continueButton != (Object)null)
		{
			((Component)continueButton).gameObject.SetActive(GetWriter().IsWaitingForInput);
		}
		if ((Object)(object)PanelMamager.inst != (Object)null)
		{
			if (((Component)this).gameObject.activeSelf)
			{
				PanelMamager.inst.canOpenPanel = false;
			}
			else
			{
				PanelMamager.inst.canOpenPanel = true;
			}
		}
	}

	protected virtual void UpdateAlpha()
	{
		if (GetWriter().IsWriting)
		{
			targetAlpha = 1f;
			fadeCoolDownTimer = 0.1f;
		}
		else if (fadeWhenDone && Mathf.Approximately(fadeCoolDownTimer, 0f))
		{
			targetAlpha = 0f;
		}
		else
		{
			fadeCoolDownTimer = Mathf.Max(0f, fadeCoolDownTimer - Time.deltaTime);
		}
		CanvasGroup val = GetCanvasGroup();
		if (fadeDuration <= 0f)
		{
			val.alpha = targetAlpha;
			return;
		}
		float num = 1f / fadeDuration * Time.deltaTime;
		float num3 = (val.alpha = Mathf.MoveTowards(val.alpha, targetAlpha, num));
		if (num3 <= 0f)
		{
			((Component)this).gameObject.SetActive(false);
		}
	}

	protected virtual void ClearStoryText()
	{
		StoryText = "";
	}

	public static SayDialog GetSayDialog()
	{
		if ((Object)(object)ActiveSayDialog == (Object)null)
		{
			SayDialog sayDialog = null;
			if (activeSayDialogs.Count > 0)
			{
				sayDialog = activeSayDialogs[0];
			}
			if ((Object)(object)sayDialog != (Object)null)
			{
				ActiveSayDialog = sayDialog;
			}
			if ((Object)(object)ActiveSayDialog == (Object)null)
			{
				GameObject val = Resources.Load<GameObject>("Prefabs/SayDialog");
				if ((Object)(object)val != (Object)null)
				{
					GameObject obj = Object.Instantiate<GameObject>(val);
					obj.SetActive(false);
					((Object)obj).name = "SayDialog";
					ActiveSayDialog = obj.GetComponent<SayDialog>();
					Object.DontDestroyOnLoad((Object)(object)obj);
				}
			}
		}
		return ActiveSayDialog;
	}

	public static void StopPortraitTweens()
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		List<Character> activeCharacters = Character.ActiveCharacters;
		for (int i = 0; i < activeCharacters.Count; i++)
		{
			Character character = activeCharacters[i];
			if ((Object)(object)character.State.portraitImage != (Object)null && LeanTween.isTweening(((Component)character.State.portraitImage).gameObject))
			{
				LeanTween.cancel(((Component)character.State.portraitImage).gameObject, callOnComplete: true);
				PortraitController.SetRectTransform(((Graphic)character.State.portraitImage).rectTransform, character.State.position);
				if (character.State.dimmed)
				{
					((Graphic)character.State.portraitImage).color = new Color(0.5f, 0.5f, 0.5f, 1f);
				}
				else
				{
					((Graphic)character.State.portraitImage).color = Color.white;
				}
			}
		}
	}

	public virtual void SetActive(bool state)
	{
		((Component)this).gameObject.SetActive(state);
	}

	public virtual void SetCharacter(Character character, int characterID = 0)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)nameText).color = new Color(1f, 0.9843137f, 74f / 85f);
		if ((Object)(object)character == (Object)null)
		{
			if ((Object)(object)characterImage != (Object)null)
			{
				((Component)characterImage).gameObject.SetActive(false);
			}
			if (NameText != null)
			{
				JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(characterID);
				if ((Object)(object)jsonData.instance != (Object)null && jsonData.instance.AvatarRandomJsonData.HasField(string.Concat(characterID)))
				{
					if (characterID == 1)
					{
						NameText = Tools.instance.getPlayer().name + "：";
					}
					else
					{
						NameText = ((wuJiangBangDing == null) ? Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(characterID)]["Name"].str) : Tools.Code64(wuJiangBangDing["Name"].str)) + "：";
					}
					chengHao.text = ((wuJiangBangDing == null) ? Tools.getMonstarTitle(characterID) : Tools.Code64(wuJiangBangDing["Title"].str));
				}
				else
				{
					NameText = "";
				}
			}
			speakingCharacter = null;
			return;
		}
		Character character2 = speakingCharacter;
		speakingCharacter = character;
		chengHao.text = "";
		List<Stage> activeStages = Stage.ActiveStages;
		for (int i = 0; i < activeStages.Count; i++)
		{
			Stage stage = activeStages[i];
			if (!stage.DimPortraits)
			{
				continue;
			}
			List<Character> charactersOnStage = stage.CharactersOnStage;
			for (int j = 0; j < charactersOnStage.Count; j++)
			{
				Character character3 = charactersOnStage[j];
				if ((Object)(object)character2 != (Object)(object)speakingCharacter)
				{
					if ((Object)(object)character3 != (Object)null && !((object)character3).Equals((object?)speakingCharacter))
					{
						stage.SetDimmed(character3, dimmedState: true);
					}
					else
					{
						stage.SetDimmed(character3, dimmedState: false);
					}
				}
			}
		}
		string objectName = character.NameText;
		if (objectName == "")
		{
			objectName = character.GetObjectName();
		}
		SetCharacterName(objectName, character.NameColor);
	}

	public virtual void SetCharacterImage(Sprite image, int characterID = 0)
	{
		if ((Object)(object)characterImage == (Object)null)
		{
			return;
		}
		if ((Object)(object)image != (Object)null || characterID != 0)
		{
			characterImage.sprite = image;
			((Component)characterImage).gameObject.SetActive(true);
			currentCharacterImage = image;
			panle.sprite = bgKuang[0];
			((Component)chengHao).gameObject.SetActive(true);
			if ((Object)(object)image == (Object)null)
			{
				((Component)characterImage).gameObject.SetActive(false);
				FaceImage.SetActive(true);
				JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(characterID);
				FaceImage.GetComponent<PlayerSetRandomFace>().randomAvatar((wuJiangBangDing == null) ? characterID : ((int)wuJiangBangDing["Image"].n));
			}
			else
			{
				FaceImage.SetActive(false);
			}
		}
		else
		{
			FaceNolmeImage.gameObject.SetActive(false);
			FaceImage.SetActive(false);
			((Component)characterImage).gameObject.SetActive(false);
			((Component)chengHao).gameObject.SetActive(false);
			panle.sprite = bgKuang[1];
		}
	}

	public virtual void SetCharacterName(string name, Color color)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (NameText != null)
		{
			string text = stringSubstituter.SubstituteStrings(name);
			NameText = text;
			nameTextAdapter.SetTextColor(color);
		}
	}

	public virtual void Say(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
	{
		((MonoBehaviour)this).StartCoroutine(DoSay(text, clearPrevious, waitForInput, fadeWhenDone, stopVoiceover, waitForVO, voiceOverClip, onComplete));
	}

	public virtual IEnumerator DoSay(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
	{
		Writer writer = GetWriter();
		if (writer.IsWriting || writer.IsWaitingForInput)
		{
			writer.Stop();
			while (writer.IsWriting || writer.IsWaitingForInput)
			{
				yield return null;
			}
		}
		if (closeOtherDialogs)
		{
			for (int i = 0; i < activeSayDialogs.Count; i++)
			{
				SayDialog sayDialog = activeSayDialogs[i];
				if ((Object)(object)((Component)sayDialog).gameObject != (Object)(object)((Component)this).gameObject)
				{
					sayDialog.SetActive(state: false);
				}
			}
		}
		((Component)this).gameObject.SetActive(true);
		this.fadeWhenDone = fadeWhenDone;
		AudioClip audioClip = null;
		if ((Object)(object)voiceOverClip != (Object)null)
		{
			GetWriterAudio().OnVoiceover(voiceOverClip);
		}
		else if ((Object)(object)speakingCharacter != (Object)null)
		{
			audioClip = speakingCharacter.SoundEffect;
		}
		writer.AttachedWriterAudio = writerAudio;
		yield return ((MonoBehaviour)this).StartCoroutine(writer.Write(text, clearPrevious, waitForInput, stopVoiceover, waitForVO, audioClip, onComplete));
	}

	public virtual void Stop()
	{
		fadeWhenDone = true;
		GetWriter().Stop();
	}

	public virtual void Clear()
	{
		ClearStoryText();
		((MonoBehaviour)this).StopAllCoroutines();
	}
}
