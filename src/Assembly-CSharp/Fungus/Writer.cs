using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fungus;

public class Writer : MonoBehaviour, IDialogInputListener
{
	[Tooltip("Gameobject containing a Text, Inout Field or Text Mesh object to write to")]
	[SerializeField]
	protected GameObject targetTextObject;

	[Tooltip("Gameobject to punch when the punch tags are displayed. If none is set, the main camera will shake instead.")]
	[SerializeField]
	protected GameObject punchObject;

	[Tooltip("Writing characters per second")]
	[SerializeField]
	protected float writingSpeed = 60f;

	[Tooltip("Pause duration for punctuation characters")]
	[SerializeField]
	protected float punctuationPause = 0.25f;

	[Tooltip("Color of text that has not been revealed yet")]
	[SerializeField]
	protected Color hiddenTextColor = new Color(1f, 1f, 1f, 0f);

	[Tooltip("Write one word at a time rather one character at a time")]
	[SerializeField]
	protected bool writeWholeWords;

	[Tooltip("Force the target text object to use Rich Text mode so text color and alpha appears correctly")]
	[SerializeField]
	protected bool forceRichText = true;

	[Tooltip("Click while text is writing to finish writing immediately")]
	[SerializeField]
	protected bool instantComplete = true;

	protected bool isWaitingForInput;

	protected bool isWriting;

	protected float currentWritingSpeed;

	protected float currentPunctuationPause;

	protected TextAdapter textAdapter = new TextAdapter();

	protected bool boldActive;

	protected bool italicActive;

	protected bool colorActive;

	protected string colorText = "";

	protected bool sizeActive;

	protected float sizeValue = 16f;

	protected bool inputFlag;

	protected bool exitFlag;

	protected List<IWriterListener> writerListeners = new List<IWriterListener>();

	protected StringBuilder openString = new StringBuilder(256);

	protected StringBuilder closeString = new StringBuilder(256);

	protected StringBuilder leftString = new StringBuilder(1024);

	protected StringBuilder rightString = new StringBuilder(1024);

	protected StringBuilder outputString = new StringBuilder(1024);

	protected StringBuilder readAheadString = new StringBuilder(1024);

	protected string hiddenColorOpen = "";

	protected string hiddenColorClose = "";

	protected int visibleCharacterCount;

	public WriterAudio AttachedWriterAudio { get; set; }

	public virtual bool IsWriting => isWriting;

	public virtual bool IsWaitingForInput => isWaitingForInput;

	public virtual bool Paused { get; set; }

	protected virtual void Awake()
	{
		GameObject gameObject = targetTextObject;
		if ((Object)(object)gameObject == (Object)null)
		{
			gameObject = ((Component)this).gameObject;
		}
		textAdapter.InitFromGameObject(gameObject);
		Component[] componentsInChildren = ((Component)this).GetComponentsInChildren<Component>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i] is IWriterListener item)
			{
				writerListeners.Add(item);
			}
		}
		CacheHiddenColorStrings();
	}

	protected virtual void CacheHiddenColorStrings()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Color32 val = Color32.op_Implicit(hiddenTextColor);
		hiddenColorOpen = $"<color=#{val.r:X2}{val.g:X2}{val.b:X2}{val.a:X2}>";
		hiddenColorClose = "</color>";
	}

	protected virtual void Start()
	{
		if (forceRichText)
		{
			textAdapter.ForceRichText();
		}
	}

	protected virtual void UpdateOpenMarkup()
	{
		openString.Length = 0;
		if (textAdapter.SupportsRichText())
		{
			if (sizeActive)
			{
				openString.Append("<size=");
				openString.Append(sizeValue);
				openString.Append(">");
			}
			if (colorActive)
			{
				openString.Append("<color=");
				openString.Append(colorText);
				openString.Append(">");
			}
			if (boldActive)
			{
				openString.Append("<b>");
			}
			if (italicActive)
			{
				openString.Append("<i>");
			}
		}
	}

	protected virtual void UpdateCloseMarkup()
	{
		closeString.Length = 0;
		if (textAdapter.SupportsRichText())
		{
			if (italicActive)
			{
				closeString.Append("</i>");
			}
			if (boldActive)
			{
				closeString.Append("</b>");
			}
			if (colorActive)
			{
				closeString.Append("</color>");
			}
			if (sizeActive)
			{
				closeString.Append("</size>");
			}
		}
	}

	protected virtual bool CheckParamCount(List<string> paramList, int count)
	{
		if (paramList == null)
		{
			Debug.LogError((object)"paramList is null");
			return false;
		}
		if (paramList.Count != count)
		{
			Debug.LogError((object)("There must be exactly " + paramList.Count + " parameters."));
			return false;
		}
		return true;
	}

	protected virtual bool TryGetSingleParam(List<string> paramList, int index, float defaultValue, out float value)
	{
		value = defaultValue;
		if (paramList.Count > index)
		{
			float.TryParse(paramList[index], out value);
			return true;
		}
		return false;
	}

	protected virtual IEnumerator ProcessTokens(List<TextTagToken> tokens, bool stopAudio, Action onComplete)
	{
		boldActive = false;
		italicActive = false;
		colorActive = false;
		sizeActive = false;
		colorText = "";
		sizeValue = 16f;
		currentPunctuationPause = punctuationPause;
		currentWritingSpeed = writingSpeed;
		exitFlag = false;
		isWriting = true;
		TokenType previousTokenType = TokenType.Invalid;
		int i = 0;
		while (i < tokens.Count)
		{
			while (Paused)
			{
				yield return null;
			}
			TextTagToken token = tokens[i];
			WriterSignals.DoTextTagToken(this, token, i, tokens.Count);
			readAheadString.Length = 0;
			for (int j = i + 1; j < tokens.Count; j++)
			{
				TextTagToken textTagToken = tokens[j];
				if (textTagToken.type == TokenType.Words && textTagToken.paramList.Count == 1)
				{
					readAheadString.Append(textTagToken.paramList[0]);
				}
				else if (textTagToken.type == TokenType.WaitForInputAndClear)
				{
					break;
				}
			}
			switch (token.type)
			{
			case TokenType.Words:
				yield return ((MonoBehaviour)this).StartCoroutine(DoWords(token.paramList, previousTokenType));
				break;
			case TokenType.BoldStart:
				boldActive = true;
				break;
			case TokenType.BoldEnd:
				boldActive = false;
				break;
			case TokenType.ItalicStart:
				italicActive = true;
				break;
			case TokenType.ItalicEnd:
				italicActive = false;
				break;
			case TokenType.ColorStart:
				if (CheckParamCount(token.paramList, 1))
				{
					colorActive = true;
					colorText = token.paramList[0];
				}
				break;
			case TokenType.ColorEnd:
				colorActive = false;
				break;
			case TokenType.SizeStart:
				if (TryGetSingleParam(token.paramList, 0, 16f, out sizeValue))
				{
					sizeActive = true;
				}
				break;
			case TokenType.SizeEnd:
				sizeActive = false;
				break;
			case TokenType.Wait:
				yield return ((MonoBehaviour)this).StartCoroutine(DoWait(token.paramList));
				break;
			case TokenType.WaitForInputNoClear:
				yield return ((MonoBehaviour)this).StartCoroutine(DoWaitForInput(clear: false));
				break;
			case TokenType.WaitForInputAndClear:
				yield return ((MonoBehaviour)this).StartCoroutine(DoWaitForInput(clear: true));
				break;
			case TokenType.WaitForVoiceOver:
				yield return ((MonoBehaviour)this).StartCoroutine(DoWaitVO());
				break;
			case TokenType.WaitOnPunctuationStart:
				TryGetSingleParam(token.paramList, 0, punctuationPause, out currentPunctuationPause);
				break;
			case TokenType.WaitOnPunctuationEnd:
				currentPunctuationPause = punctuationPause;
				break;
			case TokenType.Clear:
				textAdapter.Text = "";
				break;
			case TokenType.SpeedStart:
				TryGetSingleParam(token.paramList, 0, writingSpeed, out currentWritingSpeed);
				break;
			case TokenType.SpeedEnd:
				currentWritingSpeed = writingSpeed;
				break;
			case TokenType.Exit:
				exitFlag = true;
				break;
			case TokenType.Message:
				if (CheckParamCount(token.paramList, 1))
				{
					Flowchart.BroadcastFungusMessage(token.paramList[0]);
				}
				break;
			case TokenType.VerticalPunch:
			{
				TryGetSingleParam(token.paramList, 0, 10f, out var value6);
				TryGetSingleParam(token.paramList, 1, 0.5f, out var value7);
				Punch(new Vector3(0f, value6, 0f), value7);
				break;
			}
			case TokenType.HorizontalPunch:
			{
				TryGetSingleParam(token.paramList, 0, 10f, out var value4);
				TryGetSingleParam(token.paramList, 1, 0.5f, out var value5);
				Punch(new Vector3(value4, 0f, 0f), value5);
				break;
			}
			case TokenType.Punch:
			{
				TryGetSingleParam(token.paramList, 0, 10f, out var value2);
				TryGetSingleParam(token.paramList, 1, 0.5f, out var value3);
				Punch(new Vector3(value2, value2, 0f), value3);
				break;
			}
			case TokenType.Flash:
			{
				TryGetSingleParam(token.paramList, 0, 0.2f, out var value);
				Flash(value);
				break;
			}
			case TokenType.Audio:
			{
				AudioSource val4 = null;
				if (CheckParamCount(token.paramList, 1))
				{
					val4 = FindAudio(token.paramList[0]);
				}
				if ((Object)(object)val4 != (Object)null)
				{
					val4.PlayOneShot(val4.clip);
				}
				break;
			}
			case TokenType.AudioLoop:
			{
				AudioSource val3 = null;
				if (CheckParamCount(token.paramList, 1))
				{
					val3 = FindAudio(token.paramList[0]);
				}
				if ((Object)(object)val3 != (Object)null)
				{
					val3.Play();
					val3.loop = true;
				}
				break;
			}
			case TokenType.AudioPause:
			{
				AudioSource val2 = null;
				if (CheckParamCount(token.paramList, 1))
				{
					val2 = FindAudio(token.paramList[0]);
				}
				if ((Object)(object)val2 != (Object)null)
				{
					val2.Pause();
				}
				break;
			}
			case TokenType.AudioStop:
			{
				AudioSource val = null;
				if (CheckParamCount(token.paramList, 1))
				{
					val = FindAudio(token.paramList[0]);
				}
				if ((Object)(object)val != (Object)null)
				{
					val.Stop();
				}
				break;
			}
			}
			previousTokenType = token.type;
			if (exitFlag)
			{
				break;
			}
			int num = i + 1;
			i = num;
		}
		inputFlag = false;
		exitFlag = false;
		isWaitingForInput = false;
		isWriting = false;
		NotifyEnd(stopAudio);
		onComplete?.Invoke();
	}

	protected virtual IEnumerator DoWords(List<string> paramList, TokenType previousTokenType)
	{
		if (!CheckParamCount(paramList, 1))
		{
			yield break;
		}
		string param = paramList[0].Replace("\\n", "\n");
		if (previousTokenType == TokenType.WaitForInputAndClear || previousTokenType == TokenType.Clear)
		{
			param = param.TrimStart(' ', '\t', '\r', '\n');
		}
		string startText = "";
		if (visibleCharacterCount > 0 && visibleCharacterCount <= textAdapter.Text.Length)
		{
			startText = textAdapter.Text.Substring(0, visibleCharacterCount);
		}
		UpdateOpenMarkup();
		UpdateCloseMarkup();
		float timeAccumulator = Time.deltaTime;
		int i = 0;
		while (i < param.Length + 1 && !exitFlag)
		{
			while (Paused)
			{
				yield return null;
			}
			PartitionString(writeWholeWords, param, i);
			ConcatenateString(startText);
			textAdapter.Text = outputString.ToString();
			NotifyGlyph();
			if (!instantComplete || !inputFlag)
			{
				if (leftString.Length > 0 && rightString.Length > 0 && IsPunctuation(leftString.ToString(leftString.Length - 1, 1)[0]))
				{
					yield return ((MonoBehaviour)this).StartCoroutine(DoWait(currentPunctuationPause));
				}
				if (currentWritingSpeed > 0f)
				{
					if (timeAccumulator > 0f)
					{
						timeAccumulator -= 1f / currentWritingSpeed;
					}
					else
					{
						yield return (object)new WaitForSeconds(1f / currentWritingSpeed);
					}
				}
			}
			int num = i + 1;
			i = num;
		}
	}

	protected virtual void PartitionString(bool wholeWords, string inputString, int i)
	{
		leftString.Length = 0;
		rightString.Length = 0;
		leftString.Append(inputString);
		if (i >= inputString.Length)
		{
			return;
		}
		rightString.Append(inputString);
		if (wholeWords)
		{
			for (int j = i; j < inputString.Length + 1; j++)
			{
				if (j == inputString.Length || char.IsWhiteSpace(inputString[j]))
				{
					leftString.Length = j;
					rightString.Remove(0, j);
					break;
				}
			}
		}
		else
		{
			leftString.Remove(i, inputString.Length - i);
			rightString.Remove(0, i);
		}
	}

	protected virtual void ConcatenateString(string startText)
	{
		outputString.Length = 0;
		outputString.Append(startText);
		outputString.Append((object?)openString);
		outputString.Append((object?)leftString);
		outputString.Append((object?)closeString);
		visibleCharacterCount = outputString.Length;
		if (textAdapter.SupportsRichText() && rightString.Length + readAheadString.Length > 0)
		{
			if (hiddenColorOpen.Length == 0)
			{
				CacheHiddenColorStrings();
			}
			outputString.Append(hiddenColorOpen);
			outputString.Append((object?)rightString);
			outputString.Append((object?)readAheadString);
			outputString.Append(hiddenColorClose);
		}
	}

	protected virtual IEnumerator DoWait(List<string> paramList)
	{
		string s = "";
		if (paramList.Count == 1)
		{
			s = paramList[0];
		}
		float result = 1f;
		if (!float.TryParse(s, out result))
		{
			result = 1f;
		}
		yield return ((MonoBehaviour)this).StartCoroutine(DoWait(result));
	}

	protected virtual IEnumerator DoWaitVO()
	{
		float duration = 0f;
		if ((Object)(object)AttachedWriterAudio != (Object)null)
		{
			duration = AttachedWriterAudio.GetSecondsRemaining();
		}
		yield return ((MonoBehaviour)this).StartCoroutine(DoWait(duration));
	}

	protected virtual IEnumerator DoWait(float duration)
	{
		NotifyPause();
		float timeRemaining = duration;
		while (timeRemaining > 0f && !exitFlag && (!instantComplete || !inputFlag))
		{
			timeRemaining -= Time.deltaTime;
			yield return null;
		}
		NotifyResume();
	}

	protected virtual IEnumerator DoWaitForInput(bool clear)
	{
		NotifyPause();
		inputFlag = false;
		isWaitingForInput = true;
		while (!inputFlag && !exitFlag)
		{
			yield return null;
		}
		isWaitingForInput = false;
		inputFlag = false;
		if (clear)
		{
			textAdapter.Text = "";
		}
		NotifyResume();
	}

	protected virtual bool IsPunctuation(char character)
	{
		if (character != '.' && character != '?' && character != '!' && character != ',' && character != ':' && character != ';')
		{
			return character == ')';
		}
		return true;
	}

	protected virtual void Punch(Vector3 axis, float time)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = punchObject;
		if ((Object)(object)gameObject == (Object)null)
		{
			gameObject = ((Component)Camera.main).gameObject;
		}
		if ((Object)(object)gameObject != (Object)null)
		{
			iTween.ShakePosition(gameObject, axis, time);
		}
	}

	protected virtual void Flash(float duration)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		CameraManager cameraManager = FungusManager.Instance.CameraManager;
		cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(new Color(1f, 1f, 1f, 1f), 32, 32);
		cameraManager.Fade(1f, duration, delegate
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(new Color(1f, 1f, 1f, 1f), 32, 32);
			cameraManager.Fade(0f, duration, null);
		});
	}

	protected virtual AudioSource FindAudio(string audioObjectName)
	{
		GameObject val = GameObject.Find(audioObjectName);
		if ((Object)(object)val == (Object)null)
		{
			return null;
		}
		return val.GetComponent<AudioSource>();
	}

	protected virtual void NotifyInput()
	{
		WriterSignals.DoWriterInput(this);
		for (int i = 0; i < writerListeners.Count; i++)
		{
			writerListeners[i].OnInput();
		}
	}

	protected virtual void NotifyStart(AudioClip audioClip)
	{
		WriterSignals.DoWriterState(this, WriterState.Start);
		for (int i = 0; i < writerListeners.Count; i++)
		{
			writerListeners[i].OnStart(audioClip);
		}
	}

	protected virtual void NotifyPause()
	{
		WriterSignals.DoWriterState(this, WriterState.Pause);
		for (int i = 0; i < writerListeners.Count; i++)
		{
			writerListeners[i].OnPause();
		}
	}

	protected virtual void NotifyResume()
	{
		WriterSignals.DoWriterState(this, WriterState.Resume);
		for (int i = 0; i < writerListeners.Count; i++)
		{
			writerListeners[i].OnResume();
		}
	}

	protected virtual void NotifyEnd(bool stopAudio)
	{
		WriterSignals.DoWriterState(this, WriterState.End);
		for (int i = 0; i < writerListeners.Count; i++)
		{
			writerListeners[i].OnEnd(stopAudio);
		}
	}

	protected virtual void NotifyGlyph()
	{
		WriterSignals.DoWriterGlyph(this);
		for (int i = 0; i < writerListeners.Count; i++)
		{
			writerListeners[i].OnGlyph();
		}
	}

	public virtual void Stop()
	{
		if (isWriting || isWaitingForInput)
		{
			exitFlag = true;
		}
	}

	public virtual IEnumerator Write(string content, bool clear, bool waitForInput, bool stopAudio, bool waitForVO, AudioClip audioClip, Action onComplete)
	{
		if (clear)
		{
			textAdapter.Text = "";
			visibleCharacterCount = 0;
		}
		if (textAdapter.HasTextObject())
		{
			NotifyStart(audioClip);
			string text = TextVariationHandler.SelectVariations(content);
			if (waitForInput)
			{
				text += "{wi}";
			}
			if (waitForVO)
			{
				text += "{wvo}";
			}
			List<TextTagToken> tokens = TextTagParser.Tokenize(text);
			((Component)this).gameObject.SetActive(true);
			yield return ((MonoBehaviour)this).StartCoroutine(ProcessTokens(tokens, stopAudio, onComplete));
		}
	}

	public void SetTextColor(Color textColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		textAdapter.SetTextColor(textColor);
	}

	public void SetTextAlpha(float textAlpha)
	{
		textAdapter.SetTextAlpha(textAlpha);
	}

	public virtual void OnNextLineEvent()
	{
		inputFlag = true;
		if (isWriting)
		{
			NotifyInput();
		}
	}
}
