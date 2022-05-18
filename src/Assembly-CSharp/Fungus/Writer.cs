using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001302 RID: 4866
	public class Writer : MonoBehaviour, IDialogInputListener
	{
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600769E RID: 30366 RVA: 0x00050C09 File Offset: 0x0004EE09
		// (set) Token: 0x0600769F RID: 30367 RVA: 0x00050C11 File Offset: 0x0004EE11
		public WriterAudio AttachedWriterAudio { get; set; }

		// Token: 0x060076A0 RID: 30368 RVA: 0x002B3674 File Offset: 0x002B1874
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

		// Token: 0x060076A1 RID: 30369 RVA: 0x002B36D8 File Offset: 0x002B18D8
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

		// Token: 0x060076A2 RID: 30370 RVA: 0x00050C1A File Offset: 0x0004EE1A
		protected virtual void Start()
		{
			if (this.forceRichText)
			{
				this.textAdapter.ForceRichText();
			}
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x002B374C File Offset: 0x002B194C
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

		// Token: 0x060076A4 RID: 30372 RVA: 0x002B3820 File Offset: 0x002B1A20
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

		// Token: 0x060076A5 RID: 30373 RVA: 0x00050C2F File Offset: 0x0004EE2F
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

		// Token: 0x060076A6 RID: 30374 RVA: 0x00050C6B File Offset: 0x0004EE6B
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

		// Token: 0x060076A7 RID: 30375 RVA: 0x00050C8C File Offset: 0x0004EE8C
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

		// Token: 0x060076A8 RID: 30376 RVA: 0x00050CB0 File Offset: 0x0004EEB0
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

		// Token: 0x060076A9 RID: 30377 RVA: 0x002B38AC File Offset: 0x002B1AAC
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

		// Token: 0x060076AA RID: 30378 RVA: 0x002B3964 File Offset: 0x002B1B64
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

		// Token: 0x060076AB RID: 30379 RVA: 0x00050CCD File Offset: 0x0004EECD
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

		// Token: 0x060076AC RID: 30380 RVA: 0x00050CE3 File Offset: 0x0004EEE3
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

		// Token: 0x060076AD RID: 30381 RVA: 0x00050CF2 File Offset: 0x0004EEF2
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

		// Token: 0x060076AE RID: 30382 RVA: 0x00050D08 File Offset: 0x0004EF08
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

		// Token: 0x060076AF RID: 30383 RVA: 0x00050D1E File Offset: 0x0004EF1E
		protected virtual bool IsPunctuation(char character)
		{
			return character == '.' || character == '?' || character == '!' || character == ',' || character == ':' || character == ';' || character == ')';
		}

		// Token: 0x060076B0 RID: 30384 RVA: 0x002B3A54 File Offset: 0x002B1C54
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

		// Token: 0x060076B1 RID: 30385 RVA: 0x002B3A90 File Offset: 0x002B1C90
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

		// Token: 0x060076B2 RID: 30386 RVA: 0x002B3B0C File Offset: 0x002B1D0C
		protected virtual AudioSource FindAudio(string audioObjectName)
		{
			GameObject gameObject = GameObject.Find(audioObjectName);
			if (gameObject == null)
			{
				return null;
			}
			return gameObject.GetComponent<AudioSource>();
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x002B3B34 File Offset: 0x002B1D34
		protected virtual void NotifyInput()
		{
			WriterSignals.DoWriterInput(this);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnInput();
			}
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x002B3B70 File Offset: 0x002B1D70
		protected virtual void NotifyStart(AudioClip audioClip)
		{
			WriterSignals.DoWriterState(this, WriterState.Start);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnStart(audioClip);
			}
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x002B3BAC File Offset: 0x002B1DAC
		protected virtual void NotifyPause()
		{
			WriterSignals.DoWriterState(this, WriterState.Pause);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnPause();
			}
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x002B3BE8 File Offset: 0x002B1DE8
		protected virtual void NotifyResume()
		{
			WriterSignals.DoWriterState(this, WriterState.Resume);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnResume();
			}
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x002B3C24 File Offset: 0x002B1E24
		protected virtual void NotifyEnd(bool stopAudio)
		{
			WriterSignals.DoWriterState(this, WriterState.End);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnEnd(stopAudio);
			}
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x002B3C60 File Offset: 0x002B1E60
		protected virtual void NotifyGlyph()
		{
			WriterSignals.DoWriterGlyph(this);
			for (int i = 0; i < this.writerListeners.Count; i++)
			{
				this.writerListeners[i].OnGlyph();
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060076B9 RID: 30393 RVA: 0x00050D45 File Offset: 0x0004EF45
		public virtual bool IsWriting
		{
			get
			{
				return this.isWriting;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x00050D4D File Offset: 0x0004EF4D
		public virtual bool IsWaitingForInput
		{
			get
			{
				return this.isWaitingForInput;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x00050D5E File Offset: 0x0004EF5E
		// (set) Token: 0x060076BB RID: 30395 RVA: 0x00050D55 File Offset: 0x0004EF55
		public virtual bool Paused { get; set; }

		// Token: 0x060076BD RID: 30397 RVA: 0x00050D66 File Offset: 0x0004EF66
		public virtual void Stop()
		{
			if (this.isWriting || this.isWaitingForInput)
			{
				this.exitFlag = true;
			}
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x002B3C9C File Offset: 0x002B1E9C
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

		// Token: 0x060076BF RID: 30399 RVA: 0x00050D7F File Offset: 0x0004EF7F
		public void SetTextColor(Color textColor)
		{
			this.textAdapter.SetTextColor(textColor);
		}

		// Token: 0x060076C0 RID: 30400 RVA: 0x00050D8D File Offset: 0x0004EF8D
		public void SetTextAlpha(float textAlpha)
		{
			this.textAdapter.SetTextAlpha(textAlpha);
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x00050D9B File Offset: 0x0004EF9B
		public virtual void OnNextLineEvent()
		{
			this.inputFlag = true;
			if (this.isWriting)
			{
				this.NotifyInput();
			}
		}

		// Token: 0x04006772 RID: 26482
		[Tooltip("Gameobject containing a Text, Inout Field or Text Mesh object to write to")]
		[SerializeField]
		protected GameObject targetTextObject;

		// Token: 0x04006773 RID: 26483
		[Tooltip("Gameobject to punch when the punch tags are displayed. If none is set, the main camera will shake instead.")]
		[SerializeField]
		protected GameObject punchObject;

		// Token: 0x04006774 RID: 26484
		[Tooltip("Writing characters per second")]
		[SerializeField]
		protected float writingSpeed = 60f;

		// Token: 0x04006775 RID: 26485
		[Tooltip("Pause duration for punctuation characters")]
		[SerializeField]
		protected float punctuationPause = 0.25f;

		// Token: 0x04006776 RID: 26486
		[Tooltip("Color of text that has not been revealed yet")]
		[SerializeField]
		protected Color hiddenTextColor = new Color(1f, 1f, 1f, 0f);

		// Token: 0x04006777 RID: 26487
		[Tooltip("Write one word at a time rather one character at a time")]
		[SerializeField]
		protected bool writeWholeWords;

		// Token: 0x04006778 RID: 26488
		[Tooltip("Force the target text object to use Rich Text mode so text color and alpha appears correctly")]
		[SerializeField]
		protected bool forceRichText = true;

		// Token: 0x04006779 RID: 26489
		[Tooltip("Click while text is writing to finish writing immediately")]
		[SerializeField]
		protected bool instantComplete = true;

		// Token: 0x0400677A RID: 26490
		protected bool isWaitingForInput;

		// Token: 0x0400677B RID: 26491
		protected bool isWriting;

		// Token: 0x0400677C RID: 26492
		protected float currentWritingSpeed;

		// Token: 0x0400677D RID: 26493
		protected float currentPunctuationPause;

		// Token: 0x0400677E RID: 26494
		protected TextAdapter textAdapter = new TextAdapter();

		// Token: 0x0400677F RID: 26495
		protected bool boldActive;

		// Token: 0x04006780 RID: 26496
		protected bool italicActive;

		// Token: 0x04006781 RID: 26497
		protected bool colorActive;

		// Token: 0x04006782 RID: 26498
		protected string colorText = "";

		// Token: 0x04006783 RID: 26499
		protected bool sizeActive;

		// Token: 0x04006784 RID: 26500
		protected float sizeValue = 16f;

		// Token: 0x04006785 RID: 26501
		protected bool inputFlag;

		// Token: 0x04006786 RID: 26502
		protected bool exitFlag;

		// Token: 0x04006787 RID: 26503
		protected List<IWriterListener> writerListeners = new List<IWriterListener>();

		// Token: 0x04006788 RID: 26504
		protected StringBuilder openString = new StringBuilder(256);

		// Token: 0x04006789 RID: 26505
		protected StringBuilder closeString = new StringBuilder(256);

		// Token: 0x0400678A RID: 26506
		protected StringBuilder leftString = new StringBuilder(1024);

		// Token: 0x0400678B RID: 26507
		protected StringBuilder rightString = new StringBuilder(1024);

		// Token: 0x0400678C RID: 26508
		protected StringBuilder outputString = new StringBuilder(1024);

		// Token: 0x0400678D RID: 26509
		protected StringBuilder readAheadString = new StringBuilder(1024);

		// Token: 0x0400678E RID: 26510
		protected string hiddenColorOpen = "";

		// Token: 0x0400678F RID: 26511
		protected string hiddenColorClose = "";

		// Token: 0x04006790 RID: 26512
		protected int visibleCharacterCount;
	}
}
