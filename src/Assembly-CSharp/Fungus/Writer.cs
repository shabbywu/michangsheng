using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E91 RID: 3729
	public class Writer : MonoBehaviour, IDialogInputListener
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600699F RID: 27039 RVA: 0x0029147F File Offset: 0x0028F67F
		// (set) Token: 0x060069A0 RID: 27040 RVA: 0x00291487 File Offset: 0x0028F687
		public WriterAudio AttachedWriterAudio { get; set; }

		// Token: 0x060069A1 RID: 27041 RVA: 0x00291490 File Offset: 0x0028F690
		protected virtual void Awake()
		{
			GameObject gameObject = this.targetTextObject;
			if (gameObject == null)
			{
				gameObject = base.gameObject;
			}
			this.textAdapter.InitFromGameObject(gameObject, false);
			Component[] componentsInChildren = base.GetComponentsInChildren<Component>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				IWriterListener writerListener = componentsInChildren[i] as IWriterListener;
				if (writerListener != null)
				{
					this.writerListeners.Add(writerListener);
				}
			}
			this.CacheHiddenColorStrings();
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x002914F4 File Offset: 0x0028F6F4
		protected virtual void CacheHiddenColorStrings()
		{
			Color32 color = this.hiddenTextColor;
			this.hiddenColorOpen = string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>", new object[]
			{
				color.r,
				color.g,
				color.b,
				color.a
			});
			this.hiddenColorClose = "</color>";
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x00291566 File Offset: 0x0028F766
		protected virtual void Start()
		{
			if (this.forceRichText)
			{
				this.textAdapter.ForceRichText();
			}
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x0029157C File Offset: 0x0028F77C
		protected virtual void UpdateOpenMarkup()
		{
			this.openString.Length = 0;
			if (this.textAdapter.SupportsRichText())
			{
				if (this.sizeActive)
				{
					this.openString.Append("<size=");
					this.openString.Append(this.sizeValue);
					this.openString.Append(">");
				}
				if (this.colorActive)
				{
					this.openString.Append("<color=");
					this.openString.Append(this.colorText);
					this.openString.Append(">");
				}
				if (this.boldActive)
				{
					this.openString.Append("<b>");
				}
				if (this.italicActive)
				{
					this.openString.Append("<i>");
				}
			}
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x00291650 File Offset: 0x0028F850
		protected virtual void UpdateCloseMarkup()
		{
			this.closeString.Length = 0;
			if (this.textAdapter.SupportsRichText())
			{
				if (this.italicActive)
				{
					this.closeString.Append("</i>");
				}
				if (this.boldActive)
				{
					this.closeString.Append("</b>");
				}
				if (this.colorActive)
				{
					this.closeString.Append("</color>");
				}
				if (this.sizeActive)
				{
					this.closeString.Append("</size>");
				}
			}
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x002916DA File Offset: 0x0028F8DA
		protected virtual bool CheckParamCount(List<string> paramList, int count)
		{
			if (paramList == null)
			{
				Debug.LogError("paramList is null");
				return false;
			}
			if (paramList.Count != count)
			{
				Debug.LogError("There must be exactly " + paramList.Count + " parameters.");
				return false;
			}
			return true;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x00291716 File Offset: 0x0028F916
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

		// Token: 0x060069A8 RID: 27048 RVA: 0x00291737 File Offset: 0x0028F937
		protected virtual IEnumerator ProcessTokens(List<TextTagToken> tokens, bool stopAudio, Action onComplete)
		{
			this.boldActive = false;
			this.italicActive = false;
			this.colorActive = false;
			this.sizeActive = false;
			this.colorText = "";
			this.sizeValue = 16f;
			this.currentPunctuationPause = this.punctuationPause;
			this.currentWritingSpeed = this.writingSpeed;
			this.exitFlag = false;
			this.isWriting = true;
			TokenType previousTokenType = TokenType.Invalid;
			int num4;
			for (int i = 0; i < tokens.Count; i = num4)
			{
				while (this.Paused)
				{
					yield return null;
				}
				TextTagToken token = tokens[i];
				WriterSignals.DoTextTagToken(this, token, i, tokens.Count);
				this.readAheadString.Length = 0;
				for (int j = i + 1; j < tokens.Count; j++)
				{
					TextTagToken textTagToken = tokens[j];
					if (textTagToken.type == TokenType.Words && textTagToken.paramList.Count == 1)
					{
						this.readAheadString.Append(textTagToken.paramList[0]);
					}
					else if (textTagToken.type == TokenType.WaitForInputAndClear)
					{
						break;
					}
				}
				switch (token.type)
				{
				case TokenType.Words:
					yield return base.StartCoroutine(this.DoWords(token.paramList, previousTokenType));
					break;
				case TokenType.BoldStart:
					this.boldActive = true;
					break;
				case TokenType.BoldEnd:
					this.boldActive = false;
					break;
				case TokenType.ItalicStart:
					this.italicActive = true;
					break;
				case TokenType.ItalicEnd:
					this.italicActive = false;
					break;
				case TokenType.ColorStart:
					if (this.CheckParamCount(token.paramList, 1))
					{
						this.colorActive = true;
						this.colorText = token.paramList[0];
					}
					break;
				case TokenType.ColorEnd:
					this.colorActive = false;
					break;
				case TokenType.SizeStart:
					if (this.TryGetSingleParam(token.paramList, 0, 16f, out this.sizeValue))
					{
						this.sizeActive = true;
					}
					break;
				case TokenType.SizeEnd:
					this.sizeActive = false;
					break;
				case TokenType.Wait:
					yield return base.StartCoroutine(this.DoWait(token.paramList));
					break;
				case TokenType.WaitForInputNoClear:
					yield return base.StartCoroutine(this.DoWaitForInput(false));
					break;
				case TokenType.WaitForInputAndClear:
					yield return base.StartCoroutine(this.DoWaitForInput(true));
					break;
				case TokenType.WaitOnPunctuationStart:
					this.TryGetSingleParam(token.paramList, 0, this.punctuationPause, out this.currentPunctuationPause);
					break;
				case TokenType.WaitOnPunctuationEnd:
					this.currentPunctuationPause = this.punctuationPause;
					break;
				case TokenType.Clear:
					this.textAdapter.Text = "";
					break;
				case TokenType.SpeedStart:
					this.TryGetSingleParam(token.paramList, 0, this.writingSpeed, out this.currentWritingSpeed);
					break;
				case TokenType.SpeedEnd:
					this.currentWritingSpeed = this.writingSpeed;
					break;
				case TokenType.Exit:
					this.exitFlag = true;
					break;
				case TokenType.Message:
					if (this.CheckParamCount(token.paramList, 1))
					{
						Flowchart.BroadcastFungusMessage(token.paramList[0]);
					}
					break;
				case TokenType.VerticalPunch:
				{
					float num;
					this.TryGetSingleParam(token.paramList, 0, 10f, out num);
					float time;
					this.TryGetSingleParam(token.paramList, 1, 0.5f, out time);
					this.Punch(new Vector3(0f, num, 0f), time);
					break;
				}
				case TokenType.HorizontalPunch:
				{
					float num2;
					this.TryGetSingleParam(token.paramList, 0, 10f, out num2);
					float time2;
					this.TryGetSingleParam(token.paramList, 1, 0.5f, out time2);
					this.Punch(new Vector3(num2, 0f, 0f), time2);
					break;
				}
				case TokenType.Punch:
				{
					float num3;
					this.TryGetSingleParam(token.paramList, 0, 10f, out num3);
					float time3;
					this.TryGetSingleParam(token.paramList, 1, 0.5f, out time3);
					this.Punch(new Vector3(num3, num3, 0f), time3);
					break;
				}
				case TokenType.Flash:
				{
					float duration;
					this.TryGetSingleParam(token.paramList, 0, 0.2f, out duration);
					this.Flash(duration);
					break;
				}
				case TokenType.Audio:
				{
					AudioSource audioSource = null;
					if (this.CheckParamCount(token.paramList, 1))
					{
						audioSource = this.FindAudio(token.paramList[0]);
					}
					if (audioSource != null)
					{
						audioSource.PlayOneShot(audioSource.clip);
					}
					break;
				}
				case TokenType.AudioLoop:
				{
					AudioSource audioSource2 = null;
					if (this.CheckParamCount(token.paramList, 1))
					{
						audioSource2 = this.FindAudio(token.paramList[0]);
					}
					if (audioSource2 != null)
					{
						audioSource2.Play();
						audioSource2.loop = true;
					}
					break;
				}
				case TokenType.AudioPause:
				{
					AudioSource audioSource3 = null;
					if (this.CheckParamCount(token.paramList, 1))
					{
						audioSource3 = this.FindAudio(token.paramList[0]);
					}
					if (audioSource3 != null)
					{
						audioSource3.Pause();
					}
					break;
				}
				case TokenType.AudioStop:
				{
					AudioSource audioSource4 = null;
					if (this.CheckParamCount(token.paramList, 1))
					{
						audioSource4 = this.FindAudio(token.paramList[0]);
					}
					if (audioSource4 != null)
					{
						audioSource4.Stop();
					}
					break;
				}
				case TokenType.WaitForVoiceOver:
					yield return base.StartCoroutine(this.DoWaitVO());
					break;
				}
				previousTokenType = token.type;
				if (this.exitFlag)
				{
					break;
				}
				token = null;
				num4 = i + 1;
			}
			this.inputFlag = false;
			this.exitFlag = false;
			this.isWaitingForInput = false;
			this.isWriting = false;
			this.NotifyEnd(stopAudio);
			if (onComplete != null)
			{
				onComplete();
			}
			yield break;
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x0029175B File Offset: 0x0028F95B
		protected virtual IEnumerator DoWords(List<string> paramList, TokenType previousTokenType)
		{
			if (!this.CheckParamCount(paramList, 1))
			{
				yield break;
			}
			string param = paramList[0].Replace("\\n", "\n");
			if (previousTokenType == TokenType.WaitForInputAndClear || previousTokenType == TokenType.Clear)
			{
				param = param.TrimStart(new char[]
				{
					' ',
					'\t',
					'\r',
					'\n'
				});
			}
			string startText = "";
			if (this.visibleCharacterCount > 0 && this.visibleCharacterCount <= this.textAdapter.Text.Length)
			{
				startText = this.textAdapter.Text.Substring(0, this.visibleCharacterCount);
			}
			this.UpdateOpenMarkup();
			this.UpdateCloseMarkup();
			float timeAccumulator = Time.deltaTime;
			int num;
			for (int i = 0; i < param.Length + 1; i = num)
			{
				if (this.exitFlag)
				{
					break;
				}
				while (this.Paused)
				{
					yield return null;
				}
				this.PartitionString(this.writeWholeWords, param, i);
				this.ConcatenateString(startText);
				this.textAdapter.Text = this.outputString.ToString();
				this.NotifyGlyph();
				if (!this.instantComplete || !this.inputFlag)
				{
					if (this.leftString.Length > 0 && this.rightString.Length > 0 && this.IsPunctuation(this.leftString.ToString(this.leftString.Length - 1, 1)[0]))
					{
						yield return base.StartCoroutine(this.DoWait(this.currentPunctuationPause));
					}
					if (this.currentWritingSpeed > 0f)
					{
						if (timeAccumulator > 0f)
						{
							timeAccumulator -= 1f / this.currentWritingSpeed;
						}
						else
						{
							yield return new WaitForSeconds(1f / this.currentWritingSpeed);
						}
					}
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x00291778 File Offset: 0x0028F978
		protected virtual void PartitionString(bool wholeWords, string inputString, int i)
		{
			this.leftString.Length = 0;
			this.rightString.Length = 0;
			this.leftString.Append(inputString);
			if (i >= inputString.Length)
			{
				return;
			}
			this.rightString.Append(inputString);
			if (wholeWords)
			{
				for (int j = i; j < inputString.Length + 1; j++)
				{
					if (j == inputString.Length || char.IsWhiteSpace(inputString[j]))
					{
						this.leftString.Length = j;
						this.rightString.Remove(0, j);
						return;
					}
				}
				return;
			}
			this.leftString.Remove(i, inputString.Length - i);
			this.rightString.Remove(0, i);
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x00291830 File Offset: 0x0028FA30
		protected virtual void ConcatenateString(string startText)
		{
			this.outputString.Length = 0;
			this.outputString.Append(startText);
			this.outputString.Append(this.openString);
			this.outputString.Append(this.leftString);
			this.outputString.Append(this.closeString);
			this.visibleCharacterCount = this.outputString.Length;
			if (this.textAdapter.SupportsRichText() && this.rightString.Length + this.readAheadString.Length > 0)
			{
				if (this.hiddenColorOpen.Length == 0)
				{
					this.CacheHiddenColorStrings();
				}
				this.outputString.Append(this.hiddenColorOpen);
				this.outputString.Append(this.rightString);
				this.outputString.Append(this.readAheadString);
				this.outputString.Append(this.hiddenColorClose);
			}
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x0029191F File Offset: 0x0028FB1F
		protected virtual IEnumerator DoWait(List<string> paramList)
		{
			string s = "";
			if (paramList.Count == 1)
			{
				s = paramList[0];
			}
			float duration = 1f;
			if (!float.TryParse(s, out duration))
			{
				duration = 1f;
			}
			yield return base.StartCoroutine(this.DoWait(duration));
			yield break;
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x00291935 File Offset: 0x0028FB35
		protected virtual IEnumerator DoWaitVO()
		{
			float duration = 0f;
			if (this.AttachedWriterAudio != null)
			{
				duration = this.AttachedWriterAudio.GetSecondsRemaining();
			}
			yield return base.StartCoroutine(this.DoWait(duration));
			yield break;
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x00291944 File Offset: 0x0028FB44
		protected virtual IEnumerator DoWait(float duration)
		{
			this.NotifyPause();
			float timeRemaining = duration;
			while (timeRemaining > 0f && !this.exitFlag && (!this.instantComplete || !this.inputFlag))
			{
				timeRemaining -= Time.deltaTime;
				yield return null;
			}
			this.NotifyResume();
			yield break;
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x0029195A File Offset: 0x0028FB5A
		protected virtual IEnumerator DoWaitForInput(bool clear)
		{
			this.NotifyPause();
			this.inputFlag = false;
			this.isWaitingForInput = true;
			while (!this.inputFlag && !this.exitFlag)
			{
				yield return null;
			}
			this.isWaitingForInput = false;
			this.inputFlag = false;
			if (clear)
			{
				this.textAdapter.Text = "";
			}
			this.NotifyResume();
			yield break;
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x00291970 File Offset: 0x0028FB70
		protected virtual bool IsPunctuation(char character)
		{
			return character == '.' || character == '?' || character == '!' || character == ',' || character == ':' || character == ';' || character == ')';
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x00291998 File Offset: 0x0028FB98
		protected virtual void Punch(Vector3 axis, float time)
		{
			GameObject gameObject = this.punchObject;
			if (gameObject == null)
			{
				gameObject = Camera.main.gameObject;
			}
			if (gameObject != null)
			{
				iTween.ShakePosition(gameObject, axis, time);
			}
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x002919D4 File Offset: 0x0028FBD4
		protected virtual void Flash(float duration)
		{
			CameraManager cameraManager = FungusManager.Instance.CameraManager;
			cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(new Color(1f, 1f, 1f, 1f), 32, 32);
			cameraManager.Fade(1f, duration, delegate
			{
				cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(new Color(1f, 1f, 1f, 1f), 32, 32);
				cameraManager.Fade(0f, duration, null);
			});
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x00291A50 File Offset: 0x0028FC50
		protected virtual AudioSource FindAudio(string audioObjectName)
		{
			GameObject gameObject = GameObject.Find(audioObjectName);
			if (gameObject == null)
			{
				return null;
			}
			return gameObject.GetComponent<AudioSource>();
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x00291A78 File Offset: 0x0028FC78
		protected virtual void NotifyInput()
		{
			WriterSignals.DoWriterInput(this);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnInput();
			}
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x00291AB4 File Offset: 0x0028FCB4
		protected virtual void NotifyStart(AudioClip audioClip)
		{
			WriterSignals.DoWriterState(this, WriterState.Start);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnStart(audioClip);
			}
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x00291AF0 File Offset: 0x0028FCF0
		protected virtual void NotifyPause()
		{
			WriterSignals.DoWriterState(this, WriterState.Pause);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnPause();
			}
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x00291B2C File Offset: 0x0028FD2C
		protected virtual void NotifyResume()
		{
			WriterSignals.DoWriterState(this, WriterState.Resume);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnResume();
			}
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x00291B68 File Offset: 0x0028FD68
		protected virtual void NotifyEnd(bool stopAudio)
		{
			WriterSignals.DoWriterState(this, WriterState.End);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnEnd(stopAudio);
			}
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x00291BA4 File Offset: 0x0028FDA4
		protected virtual void NotifyGlyph()
		{
			WriterSignals.DoWriterGlyph(this);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnGlyph();
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060069BA RID: 27066 RVA: 0x00291BDE File Offset: 0x0028FDDE
		public virtual bool IsWriting
		{
			get
			{
				return this.isWriting;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060069BB RID: 27067 RVA: 0x00291BE6 File Offset: 0x0028FDE6
		public virtual bool IsWaitingForInput
		{
			get
			{
				return this.isWaitingForInput;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060069BD RID: 27069 RVA: 0x00291BF7 File Offset: 0x0028FDF7
		// (set) Token: 0x060069BC RID: 27068 RVA: 0x00291BEE File Offset: 0x0028FDEE
		public virtual bool Paused { get; set; }

		// Token: 0x060069BE RID: 27070 RVA: 0x00291BFF File Offset: 0x0028FDFF
		public virtual void Stop()
		{
			if (this.isWriting || this.isWaitingForInput)
			{
				this.exitFlag = true;
			}
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x00291C18 File Offset: 0x0028FE18
		public virtual IEnumerator Write(string content, bool clear, bool waitForInput, bool stopAudio, bool waitForVO, AudioClip audioClip, Action onComplete)
		{
			if (clear)
			{
				this.textAdapter.Text = "";
				this.visibleCharacterCount = 0;
			}
			if (!this.textAdapter.HasTextObject())
			{
				yield break;
			}
			this.NotifyStart(audioClip);
			string text = TextVariationHandler.SelectVariations(content, 0);
			if (waitForInput)
			{
				text += "{wi}";
			}
			if (waitForVO)
			{
				text += "{wvo}";
			}
			List<TextTagToken> tokens = TextTagParser.Tokenize(text);
			base.gameObject.SetActive(true);
			yield return base.StartCoroutine(this.ProcessTokens(tokens, stopAudio, onComplete));
			yield break;
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x00291C67 File Offset: 0x0028FE67
		public void SetTextColor(Color textColor)
		{
			this.textAdapter.SetTextColor(textColor);
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x00291C75 File Offset: 0x0028FE75
		public void SetTextAlpha(float textAlpha)
		{
			this.textAdapter.SetTextAlpha(textAlpha);
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x00291C83 File Offset: 0x0028FE83
		public virtual void OnNextLineEvent()
		{
			this.inputFlag = true;
			if (this.isWriting)
			{
				this.NotifyInput();
			}
		}

		// Token: 0x04005991 RID: 22929
		[Tooltip("Gameobject containing a Text, Inout Field or Text Mesh object to write to")]
		[SerializeField]
		protected GameObject targetTextObject;

		// Token: 0x04005992 RID: 22930
		[Tooltip("Gameobject to punch when the punch tags are displayed. If none is set, the main camera will shake instead.")]
		[SerializeField]
		protected GameObject punchObject;

		// Token: 0x04005993 RID: 22931
		[Tooltip("Writing characters per second")]
		[SerializeField]
		protected float writingSpeed = 60f;

		// Token: 0x04005994 RID: 22932
		[Tooltip("Pause duration for punctuation characters")]
		[SerializeField]
		protected float punctuationPause = 0.25f;

		// Token: 0x04005995 RID: 22933
		[Tooltip("Color of text that has not been revealed yet")]
		[SerializeField]
		protected Color hiddenTextColor = new Color(1f, 1f, 1f, 0f);

		// Token: 0x04005996 RID: 22934
		[Tooltip("Write one word at a time rather one character at a time")]
		[SerializeField]
		protected bool writeWholeWords;

		// Token: 0x04005997 RID: 22935
		[Tooltip("Force the target text object to use Rich Text mode so text color and alpha appears correctly")]
		[SerializeField]
		protected bool forceRichText = true;

		// Token: 0x04005998 RID: 22936
		[Tooltip("Click while text is writing to finish writing immediately")]
		[SerializeField]
		protected bool instantComplete = true;

		// Token: 0x04005999 RID: 22937
		protected bool isWaitingForInput;

		// Token: 0x0400599A RID: 22938
		protected bool isWriting;

		// Token: 0x0400599B RID: 22939
		protected float currentWritingSpeed;

		// Token: 0x0400599C RID: 22940
		protected float currentPunctuationPause;

		// Token: 0x0400599D RID: 22941
		protected TextAdapter textAdapter = new TextAdapter();

		// Token: 0x0400599E RID: 22942
		protected bool boldActive;

		// Token: 0x0400599F RID: 22943
		protected bool italicActive;

		// Token: 0x040059A0 RID: 22944
		protected bool colorActive;

		// Token: 0x040059A1 RID: 22945
		protected string colorText = "";

		// Token: 0x040059A2 RID: 22946
		protected bool sizeActive;

		// Token: 0x040059A3 RID: 22947
		protected float sizeValue = 16f;

		// Token: 0x040059A4 RID: 22948
		protected bool inputFlag;

		// Token: 0x040059A5 RID: 22949
		protected bool exitFlag;

		// Token: 0x040059A6 RID: 22950
		protected List<IWriterListener> writerListeners = new List<IWriterListener>();

		// Token: 0x040059A7 RID: 22951
		protected StringBuilder openString = new StringBuilder(256);

		// Token: 0x040059A8 RID: 22952
		protected StringBuilder closeString = new StringBuilder(256);

		// Token: 0x040059A9 RID: 22953
		protected StringBuilder leftString = new StringBuilder(1024);

		// Token: 0x040059AA RID: 22954
		protected StringBuilder rightString = new StringBuilder(1024);

		// Token: 0x040059AB RID: 22955
		protected StringBuilder outputString = new StringBuilder(1024);

		// Token: 0x040059AC RID: 22956
		protected StringBuilder readAheadString = new StringBuilder(1024);

		// Token: 0x040059AD RID: 22957
		protected string hiddenColorOpen = "";

		// Token: 0x040059AE RID: 22958
		protected string hiddenColorClose = "";

		// Token: 0x040059AF RID: 22959
		protected int visibleCharacterCount;
	}
}
