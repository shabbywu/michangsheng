using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06000943 RID: 2371 RVA: 0x00037E02 File Offset: 0x00036002
	// (set) Token: 0x06000944 RID: 2372 RVA: 0x00037E0A File Offset: 0x0003600A
	public string defaultText
	{
		get
		{
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06000945 RID: 2373 RVA: 0x00037E27 File Offset: 0x00036027
	public bool inputShouldBeHidden
	{
		get
		{
			return this.hideInput && this.label != null && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000946 RID: 2374 RVA: 0x00037E5A File Offset: 0x0003605A
	// (set) Token: 0x06000947 RID: 2375 RVA: 0x00037E62 File Offset: 0x00036062
	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000948 RID: 2376 RVA: 0x00037E6B File Offset: 0x0003606B
	// (set) Token: 0x06000949 RID: 2377 RVA: 0x00037E84 File Offset: 0x00036084
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			UIInput.mDrawStart = 0;
			if (Application.platform == 22)
			{
				value = value.Replace("\\b", "\b");
			}
			value = this.Validate(value);
			if (this.mValue != value)
			{
				this.mValue = value;
				this.mLoadSavedValue = false;
				if (this.isSelected)
				{
					if (string.IsNullOrEmpty(value))
					{
						this.mSelectionStart = 0;
						this.mSelectionEnd = 0;
					}
					else
					{
						this.mSelectionStart = value.Length;
						this.mSelectionEnd = this.mSelectionStart;
					}
				}
				else
				{
					this.SaveToPlayerPrefs(value);
				}
				this.UpdateLabel();
				this.ExecuteOnChange();
			}
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x0600094A RID: 2378 RVA: 0x00037F32 File Offset: 0x00036132
	// (set) Token: 0x0600094B RID: 2379 RVA: 0x00037F3A File Offset: 0x0003613A
	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x0600094C RID: 2380 RVA: 0x00037F43 File Offset: 0x00036143
	// (set) Token: 0x0600094D RID: 2381 RVA: 0x00037F50 File Offset: 0x00036150
	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
					return;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x0600094E RID: 2382 RVA: 0x00037F6F File Offset: 0x0003616F
	// (set) Token: 0x0600094F RID: 2383 RVA: 0x00037F8B File Offset: 0x0003618B
	public int cursorPosition
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000950 RID: 2384 RVA: 0x00037FA2 File Offset: 0x000361A2
	// (set) Token: 0x06000951 RID: 2385 RVA: 0x00037FBE File Offset: 0x000361BE
	public int selectionStart
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionStart;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionStart = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000952 RID: 2386 RVA: 0x00037F6F File Offset: 0x0003616F
	// (set) Token: 0x06000953 RID: 2387 RVA: 0x00037F8B File Offset: 0x0003618B
	public int selectionEnd
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06000954 RID: 2388 RVA: 0x00037FD5 File Offset: 0x000361D5
	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00037FE0 File Offset: 0x000361E0
	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		foreach (char c in val)
		{
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00038095 File Offset: 0x00036295
	private void Start()
	{
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
			return;
		}
		this.value = this.mValue.Replace("\\n", "\n");
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x000380D0 File Offset: 0x000362D0
	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.label.supportEncoding = false;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mPivot = this.label.pivot;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00038185 File Offset: 0x00036385
	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
				return;
			}
			PlayerPrefs.SetString(this.savedAs, val);
		}
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x000381B4 File Offset: 0x000363B4
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			this.OnSelectEvent();
			return;
		}
		this.OnDeselectEvent();
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000381C6 File Offset: 0x000363C6
	protected void OnSelectEvent()
	{
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mSelectMe = Time.frameCount;
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00038200 File Offset: 0x00036400
	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = 0;
			this.RestoreLabelPivot();
		}
		UIInput.selection = null;
		this.UpdateLabel();
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00038298 File Offset: 0x00036498
	private void Update()
	{
		if (this.isSelected)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
			{
				this.mSelectMe = -1;
				this.mSelectionStart = 0;
				this.mSelectionEnd = (string.IsNullOrEmpty(this.mValue) ? 0 : this.mValue.Length);
				UIInput.mDrawStart = 0;
				this.label.color = this.activeTextColor;
				Vector2 vector = (UICamera.current != null && UICamera.current.cachedCamera != null) ? UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]) : this.label.worldCorners[0];
				vector.y = (float)Screen.height - vector.y;
				Input.imeCompositionMode = 1;
				Input.compositionCursorPos = vector;
				this.UpdateLabel();
				return;
			}
			if (this.selectOnTab != null && Input.GetKeyDown(9))
			{
				UICamera.selectedObject = this.selectOnTab;
				return;
			}
			string compositionString = Input.compositionString;
			if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
			{
				foreach (char c in Input.inputString)
				{
					if (c >= ' ' && c != '' && c != '' && c != '' && c != '')
					{
						this.Insert(c.ToString());
					}
				}
			}
			if (UIInput.mLastIME != compositionString)
			{
				this.mSelectionEnd = (string.IsNullOrEmpty(compositionString) ? this.mSelectionStart : (this.mValue.Length + compositionString.Length));
				UIInput.mLastIME = compositionString;
				this.UpdateLabel();
				this.ExecuteOnChange();
			}
			if (this.mCaret != null && this.mNextBlink < RealTime.time)
			{
				this.mNextBlink = RealTime.time + 0.5f;
				this.mCaret.enabled = !this.mCaret.enabled;
			}
			if (this.isSelected && this.mLastAlpha != this.label.finalAlpha)
			{
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000384E0 File Offset: 0x000366E0
	private void OnGUI()
	{
		if (this.isSelected && Event.current.rawType == 4)
		{
			this.ProcessEvent(Event.current);
		}
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00038503 File Offset: 0x00036703
	protected void DoBackspace()
	{
		if (!string.IsNullOrEmpty(this.mValue))
		{
			if (this.mSelectionStart == this.mSelectionEnd)
			{
				if (this.mSelectionStart < 1)
				{
					return;
				}
				this.mSelectionEnd--;
			}
			this.Insert("");
		}
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00038544 File Offset: 0x00036744
	protected virtual bool ProcessEvent(Event ev)
	{
		if (this.label == null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = (platform == null || platform == 1) ? ((ev.modifiers & 8) > 0) : ((ev.modifiers & 2) > 0);
		bool flag2 = (ev.modifiers & 1) > 0;
		KeyCode keyCode = ev.keyCode;
		if (keyCode <= 99)
		{
			if (keyCode <= 13)
			{
				if (keyCode == 8)
				{
					ev.Use();
					this.DoBackspace();
					return true;
				}
				if (keyCode != 13)
				{
					return false;
				}
			}
			else
			{
				if (keyCode == 97)
				{
					if (flag)
					{
						ev.Use();
						this.mSelectionStart = 0;
						this.mSelectionEnd = this.mValue.Length;
						this.UpdateLabel();
					}
					return true;
				}
				if (keyCode != 99)
				{
					return false;
				}
				if (flag)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
				}
				return true;
			}
		}
		else if (keyCode <= 120)
		{
			if (keyCode == 118)
			{
				if (flag)
				{
					ev.Use();
					this.Insert(NGUITools.clipboard);
				}
				return true;
			}
			if (keyCode != 120)
			{
				return false;
			}
			if (flag)
			{
				ev.Use();
				NGUITools.clipboard = this.GetSelection();
				this.Insert("");
			}
			return true;
		}
		else
		{
			if (keyCode == 127)
			{
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.mSelectionStart == this.mSelectionEnd)
					{
						if (this.mSelectionStart >= this.mValue.Length)
						{
							return true;
						}
						this.mSelectionEnd++;
					}
					this.Insert("");
				}
				return true;
			}
			switch (keyCode)
			{
			case 271:
				break;
			case 272:
			case 277:
				return false;
			case 273:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, 273);
					if (this.mSelectionEnd != 0)
					{
						this.mSelectionEnd += UIInput.mDrawStart;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 274:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, 274);
					if (this.mSelectionEnd != this.label.processedText.Length)
					{
						this.mSelectionEnd += UIInput.mDrawStart;
					}
					else
					{
						this.mSelectionEnd = this.mValue.Length;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 275:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = Mathf.Min(this.mSelectionEnd + 1, this.mValue.Length);
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 276:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = Mathf.Max(this.mSelectionEnd - 1, 0);
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 278:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.label.multiLine)
					{
						this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, 278);
					}
					else
					{
						this.mSelectionEnd = 0;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 279:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.label.multiLine)
					{
						this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, 279);
					}
					else
					{
						this.mSelectionEnd = this.mValue.Length;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 280:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = 0;
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case 281:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.mValue.Length;
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			default:
				return false;
			}
		}
		ev.Use();
		if (this.onReturnKey == UIInput.OnReturnKey.NewLine || (this.onReturnKey == UIInput.OnReturnKey.Default && this.label.multiLine && !flag && this.label.overflowMethod != UILabel.Overflow.ClampContent && this.validation == UIInput.Validation.None))
		{
			this.Insert("\n");
		}
		else
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentKey = ev.keyCode;
			this.Submit();
			UICamera.currentKey = 0;
		}
		return true;
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00038A0C File Offset: 0x00036C0C
	protected virtual void Insert(string text)
	{
		string leftText = this.GetLeftText();
		string rightText = this.GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			char c = text[i];
			if (c == '\b')
			{
				this.DoBackspace();
			}
			else
			{
				if (this.characterLimit > 0 && stringBuilder.Length + length >= this.characterLimit)
				{
					break;
				}
				if (this.onValidate != null)
				{
					c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				else if (this.validation != UIInput.Validation.None)
				{
					c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
			}
			i++;
		}
		this.mSelectionStart = stringBuilder.Length;
		this.mSelectionEnd = this.mSelectionStart;
		int j = 0;
		int length3 = rightText.Length;
		while (j < length3)
		{
			char c2 = rightText[j];
			if (this.onValidate != null)
			{
				c2 = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c2 = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			j++;
		}
		this.mValue = stringBuilder.ToString();
		this.UpdateLabel();
		this.ExecuteOnChange();
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00038B94 File Offset: 0x00036D94
	protected string GetLeftText()
	{
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		if (!string.IsNullOrEmpty(this.mValue) && num >= 0)
		{
			return this.mValue.Substring(0, num);
		}
		return "";
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00038BD8 File Offset: 0x00036DD8
	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		if (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length)
		{
			return this.mValue.Substring(num);
		}
		return "";
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00038C24 File Offset: 0x00036E24
	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
		{
			return "";
		}
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		int num2 = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return this.mValue.Substring(num, num2 - num);
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00038C88 File Offset: 0x00036E88
	protected int GetCharUnderMouse()
	{
		Vector3[] worldCorners = this.label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane plane;
		plane..ctor(worldCorners[0], worldCorners[1], worldCorners[2]);
		float num;
		if (!plane.Raycast(currentRay, ref num))
		{
			return 0;
		}
		return UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(currentRay.GetPoint(num));
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00038CEC File Offset: 0x00036EEC
	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && this.isSelected && this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
			if (!Input.GetKey(304) && !Input.GetKey(303))
			{
				this.selectionStart = this.mSelectionEnd;
			}
		}
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00038D51 File Offset: 0x00036F51
	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00038D7C File Offset: 0x00036F7C
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00038D84 File Offset: 0x00036F84
	protected virtual void Cleanup()
	{
		if (this.mHighlight)
		{
			this.mHighlight.enabled = false;
		}
		if (this.mCaret)
		{
			this.mCaret.enabled = false;
		}
		if (this.mBlankTex)
		{
			NGUITools.Destroy(this.mBlankTex);
			this.mBlankTex = null;
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00038DE4 File Offset: 0x00036FE4
	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00038E38 File Offset: 0x00037038
	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((flag && !isSelected) ? this.mDefaultColor : this.activeTextColor);
			string text;
			if (flag)
			{
				text = (isSelected ? "" : this.mDefaultText);
				this.RestoreLabelPivot();
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = "";
					string str = "*";
					if (this.label.bitmapFont != null && this.label.bitmapFont.bmFont != null && this.label.bitmapFont.bmFont.GetGlyph(42) == null)
					{
						str = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += str;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = isSelected ? Mathf.Min(text.Length, this.cursorPosition) : 0;
				string str2 = text.Substring(0, num);
				if (isSelected)
				{
					str2 += Input.compositionString;
				}
				text = str2 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.RestoreLabelPivot();
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.SetPivotToLeft();
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.SetPivotToLeft();
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.SetPivotToRight();
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.RestoreLabelPivot();
				}
			}
			this.label.text = text;
			if (isSelected)
			{
				int num3 = this.mSelectionStart - UIInput.mDrawStart;
				int num4 = this.mSelectionEnd - UIInput.mDrawStart;
				if (this.mBlankTex == null)
				{
					this.mBlankTex = new Texture2D(2, 2, 5, false);
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							this.mBlankTex.SetPixel(k, j, Color.white);
						}
					}
					this.mBlankTex.Apply();
				}
				if (num3 != num4)
				{
					if (this.mHighlight == null)
					{
						this.mHighlight = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
						this.mHighlight.name = "Input Highlight";
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.fillGeometry = false;
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.SetAnchor(this.label.cachedTransform);
					}
					else
					{
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.MarkAsChanged();
						this.mHighlight.enabled = true;
					}
				}
				if (this.mCaret == null)
				{
					this.mCaret = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
					this.mCaret.name = "Input Caret";
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.fillGeometry = false;
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.SetAnchor(this.label.cachedTransform);
				}
				else
				{
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.MarkAsChanged();
					this.mCaret.enabled = true;
				}
				if (num3 != num4)
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
					this.mHighlight.enabled = this.mHighlight.geometry.hasVertices;
				}
				else
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, null, this.caretColor, this.selectionColor);
					if (this.mHighlight != null)
					{
						this.mHighlight.enabled = false;
					}
				}
				this.mNextBlink = RealTime.time + 0.5f;
				this.mLastAlpha = this.label.finalAlpha;
				return;
			}
			this.Cleanup();
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00039334 File Offset: 0x00037534
	protected void SetPivotToLeft()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 0f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0003936C File Offset: 0x0003756C
	protected void SetPivotToRight()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 1f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x000393A2 File Offset: 0x000375A2
	protected void RestoreLabelPivot()
	{
		if (this.label != null && this.label.pivot != this.mPivot)
		{
			this.label.pivot = this.mPivot;
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x000393D8 File Offset: 0x000375D8
	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ';
			char c2 = (text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x000395AB File Offset: 0x000377AB
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x000395DE File Offset: 0x000377DE
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x000395E7 File Offset: 0x000377E7
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x000395F8 File Offset: 0x000377F8
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			string text = this.mValue.Replace("\\n", "\n");
			this.mValue = "";
			this.value = (PlayerPrefs.HasKey(this.savedAs) ? PlayerPrefs.GetString(this.savedAs) : text);
		}
	}

	// Token: 0x040005C1 RID: 1473
	public static UIInput current;

	// Token: 0x040005C2 RID: 1474
	public static UIInput selection;

	// Token: 0x040005C3 RID: 1475
	public UILabel label;

	// Token: 0x040005C4 RID: 1476
	public UIInput.InputType inputType;

	// Token: 0x040005C5 RID: 1477
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x040005C6 RID: 1478
	public UIInput.KeyboardType keyboardType;

	// Token: 0x040005C7 RID: 1479
	public bool hideInput;

	// Token: 0x040005C8 RID: 1480
	public UIInput.Validation validation;

	// Token: 0x040005C9 RID: 1481
	public int characterLimit;

	// Token: 0x040005CA RID: 1482
	public string savedAs;

	// Token: 0x040005CB RID: 1483
	public GameObject selectOnTab;

	// Token: 0x040005CC RID: 1484
	public Color activeTextColor = Color.white;

	// Token: 0x040005CD RID: 1485
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x040005CE RID: 1486
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x040005CF RID: 1487
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x040005D0 RID: 1488
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040005D1 RID: 1489
	public UIInput.OnValidate onValidate;

	// Token: 0x040005D2 RID: 1490
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x040005D3 RID: 1491
	[NonSerialized]
	protected string mDefaultText = "";

	// Token: 0x040005D4 RID: 1492
	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	// Token: 0x040005D5 RID: 1493
	[NonSerialized]
	protected float mPosition;

	// Token: 0x040005D6 RID: 1494
	[NonSerialized]
	protected bool mDoInit = true;

	// Token: 0x040005D7 RID: 1495
	[NonSerialized]
	protected UIWidget.Pivot mPivot;

	// Token: 0x040005D8 RID: 1496
	[NonSerialized]
	protected bool mLoadSavedValue = true;

	// Token: 0x040005D9 RID: 1497
	protected static int mDrawStart = 0;

	// Token: 0x040005DA RID: 1498
	protected static string mLastIME = "";

	// Token: 0x040005DB RID: 1499
	[NonSerialized]
	protected int mSelectionStart;

	// Token: 0x040005DC RID: 1500
	[NonSerialized]
	protected int mSelectionEnd;

	// Token: 0x040005DD RID: 1501
	[NonSerialized]
	protected UITexture mHighlight;

	// Token: 0x040005DE RID: 1502
	[NonSerialized]
	protected UITexture mCaret;

	// Token: 0x040005DF RID: 1503
	[NonSerialized]
	protected Texture2D mBlankTex;

	// Token: 0x040005E0 RID: 1504
	[NonSerialized]
	protected float mNextBlink;

	// Token: 0x040005E1 RID: 1505
	[NonSerialized]
	protected float mLastAlpha;

	// Token: 0x040005E2 RID: 1506
	[NonSerialized]
	protected string mCached = "";

	// Token: 0x040005E3 RID: 1507
	[NonSerialized]
	protected int mSelectMe = -1;

	// Token: 0x02001224 RID: 4644
	public enum InputType
	{
		// Token: 0x040064BA RID: 25786
		Standard,
		// Token: 0x040064BB RID: 25787
		AutoCorrect,
		// Token: 0x040064BC RID: 25788
		Password
	}

	// Token: 0x02001225 RID: 4645
	public enum Validation
	{
		// Token: 0x040064BE RID: 25790
		None,
		// Token: 0x040064BF RID: 25791
		Integer,
		// Token: 0x040064C0 RID: 25792
		Float,
		// Token: 0x040064C1 RID: 25793
		Alphanumeric,
		// Token: 0x040064C2 RID: 25794
		Username,
		// Token: 0x040064C3 RID: 25795
		Name
	}

	// Token: 0x02001226 RID: 4646
	public enum KeyboardType
	{
		// Token: 0x040064C5 RID: 25797
		Default,
		// Token: 0x040064C6 RID: 25798
		ASCIICapable,
		// Token: 0x040064C7 RID: 25799
		NumbersAndPunctuation,
		// Token: 0x040064C8 RID: 25800
		URL,
		// Token: 0x040064C9 RID: 25801
		NumberPad,
		// Token: 0x040064CA RID: 25802
		PhonePad,
		// Token: 0x040064CB RID: 25803
		NamePhonePad,
		// Token: 0x040064CC RID: 25804
		EmailAddress
	}

	// Token: 0x02001227 RID: 4647
	public enum OnReturnKey
	{
		// Token: 0x040064CE RID: 25806
		Default,
		// Token: 0x040064CF RID: 25807
		Submit,
		// Token: 0x040064D0 RID: 25808
		NewLine
	}

	// Token: 0x02001228 RID: 4648
	// (Invoke) Token: 0x06007887 RID: 30855
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
