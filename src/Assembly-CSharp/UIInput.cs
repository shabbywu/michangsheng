using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000108 RID: 264
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0000C5FB File Offset: 0x0000A7FB
	// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0000C603 File Offset: 0x0000A803
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

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0000C620 File Offset: 0x0000A820
	public bool inputShouldBeHidden
	{
		get
		{
			return this.hideInput && this.label != null && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0000C653 File Offset: 0x0000A853
	// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0000C65B File Offset: 0x0000A85B
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

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0000C664 File Offset: 0x0000A864
	// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0008B3DC File Offset: 0x000895DC
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

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0000C67A File Offset: 0x0000A87A
	// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0000C682 File Offset: 0x0000A882
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

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0000C68B File Offset: 0x0000A88B
	// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0000C698 File Offset: 0x0000A898
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

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0000C6B7 File Offset: 0x0000A8B7
	// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
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

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0000C6EA File Offset: 0x0000A8EA
	// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0000C706 File Offset: 0x0000A906
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

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0000C6B7 File Offset: 0x0000A8B7
	// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
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

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0000C71D File Offset: 0x0000A91D
	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0008B48C File Offset: 0x0008968C
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

	// Token: 0x06000A26 RID: 2598 RVA: 0x0000C725 File Offset: 0x0000A925
	private void Start()
	{
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
			return;
		}
		this.value = this.mValue.Replace("\\n", "\n");
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0008B544 File Offset: 0x00089744
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

	// Token: 0x06000A28 RID: 2600 RVA: 0x0000C75E File Offset: 0x0000A95E
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

	// Token: 0x06000A29 RID: 2601 RVA: 0x0000C78D File Offset: 0x0000A98D
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			this.OnSelectEvent();
			return;
		}
		this.OnDeselectEvent();
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0000C79F File Offset: 0x0000A99F
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

	// Token: 0x06000A2B RID: 2603 RVA: 0x0008B5FC File Offset: 0x000897FC
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

	// Token: 0x06000A2C RID: 2604 RVA: 0x0008B694 File Offset: 0x00089894
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

	// Token: 0x06000A2D RID: 2605 RVA: 0x0000C7D6 File Offset: 0x0000A9D6
	private void OnGUI()
	{
		if (this.isSelected && Event.current.rawType == 4)
		{
			this.ProcessEvent(Event.current);
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0000C7F9 File Offset: 0x0000A9F9
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

	// Token: 0x06000A2F RID: 2607 RVA: 0x0008B8DC File Offset: 0x00089ADC
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

	// Token: 0x06000A30 RID: 2608 RVA: 0x0008BDA4 File Offset: 0x00089FA4
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

	// Token: 0x06000A31 RID: 2609 RVA: 0x0008BF2C File Offset: 0x0008A12C
	protected string GetLeftText()
	{
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		if (!string.IsNullOrEmpty(this.mValue) && num >= 0)
		{
			return this.mValue.Substring(0, num);
		}
		return "";
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0008BF70 File Offset: 0x0008A170
	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		if (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length)
		{
			return this.mValue.Substring(num);
		}
		return "";
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0008BFBC File Offset: 0x0008A1BC
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

	// Token: 0x06000A34 RID: 2612 RVA: 0x0008C020 File Offset: 0x0008A220
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

	// Token: 0x06000A35 RID: 2613 RVA: 0x0008C084 File Offset: 0x0008A284
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

	// Token: 0x06000A36 RID: 2614 RVA: 0x0000C839 File Offset: 0x0000AA39
	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0000C864 File Offset: 0x0000AA64
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0008C0EC File Offset: 0x0008A2EC
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

	// Token: 0x06000A39 RID: 2617 RVA: 0x0008C14C File Offset: 0x0008A34C
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

	// Token: 0x06000A3A RID: 2618 RVA: 0x0008C1A0 File Offset: 0x0008A3A0
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

	// Token: 0x06000A3B RID: 2619 RVA: 0x0008C69C File Offset: 0x0008A89C
	protected void SetPivotToLeft()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 0f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x0008C6D4 File Offset: 0x0008A8D4
	protected void SetPivotToRight()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 1f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0000C86C File Offset: 0x0000AA6C
	protected void RestoreLabelPivot()
	{
		if (this.label != null && this.label.pivot != this.mPivot)
		{
			this.label.pivot = this.mPivot;
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0008C70C File Offset: 0x0008A90C
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

	// Token: 0x06000A3F RID: 2623 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0000C8DC File Offset: 0x0000AADC
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0008C8E0 File Offset: 0x0008AAE0
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			string text = this.mValue.Replace("\\n", "\n");
			this.mValue = "";
			this.value = (PlayerPrefs.HasKey(this.savedAs) ? PlayerPrefs.GetString(this.savedAs) : text);
		}
	}

	// Token: 0x0400072B RID: 1835
	public static UIInput current;

	// Token: 0x0400072C RID: 1836
	public static UIInput selection;

	// Token: 0x0400072D RID: 1837
	public UILabel label;

	// Token: 0x0400072E RID: 1838
	public UIInput.InputType inputType;

	// Token: 0x0400072F RID: 1839
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x04000730 RID: 1840
	public UIInput.KeyboardType keyboardType;

	// Token: 0x04000731 RID: 1841
	public bool hideInput;

	// Token: 0x04000732 RID: 1842
	public UIInput.Validation validation;

	// Token: 0x04000733 RID: 1843
	public int characterLimit;

	// Token: 0x04000734 RID: 1844
	public string savedAs;

	// Token: 0x04000735 RID: 1845
	public GameObject selectOnTab;

	// Token: 0x04000736 RID: 1846
	public Color activeTextColor = Color.white;

	// Token: 0x04000737 RID: 1847
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x04000738 RID: 1848
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x04000739 RID: 1849
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x0400073A RID: 1850
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x0400073B RID: 1851
	public UIInput.OnValidate onValidate;

	// Token: 0x0400073C RID: 1852
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x0400073D RID: 1853
	[NonSerialized]
	protected string mDefaultText = "";

	// Token: 0x0400073E RID: 1854
	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	// Token: 0x0400073F RID: 1855
	[NonSerialized]
	protected float mPosition;

	// Token: 0x04000740 RID: 1856
	[NonSerialized]
	protected bool mDoInit = true;

	// Token: 0x04000741 RID: 1857
	[NonSerialized]
	protected UIWidget.Pivot mPivot;

	// Token: 0x04000742 RID: 1858
	[NonSerialized]
	protected bool mLoadSavedValue = true;

	// Token: 0x04000743 RID: 1859
	protected static int mDrawStart = 0;

	// Token: 0x04000744 RID: 1860
	protected static string mLastIME = "";

	// Token: 0x04000745 RID: 1861
	[NonSerialized]
	protected int mSelectionStart;

	// Token: 0x04000746 RID: 1862
	[NonSerialized]
	protected int mSelectionEnd;

	// Token: 0x04000747 RID: 1863
	[NonSerialized]
	protected UITexture mHighlight;

	// Token: 0x04000748 RID: 1864
	[NonSerialized]
	protected UITexture mCaret;

	// Token: 0x04000749 RID: 1865
	[NonSerialized]
	protected Texture2D mBlankTex;

	// Token: 0x0400074A RID: 1866
	[NonSerialized]
	protected float mNextBlink;

	// Token: 0x0400074B RID: 1867
	[NonSerialized]
	protected float mLastAlpha;

	// Token: 0x0400074C RID: 1868
	[NonSerialized]
	protected string mCached = "";

	// Token: 0x0400074D RID: 1869
	[NonSerialized]
	protected int mSelectMe = -1;

	// Token: 0x02000109 RID: 265
	public enum InputType
	{
		// Token: 0x0400074F RID: 1871
		Standard,
		// Token: 0x04000750 RID: 1872
		AutoCorrect,
		// Token: 0x04000751 RID: 1873
		Password
	}

	// Token: 0x0200010A RID: 266
	public enum Validation
	{
		// Token: 0x04000753 RID: 1875
		None,
		// Token: 0x04000754 RID: 1876
		Integer,
		// Token: 0x04000755 RID: 1877
		Float,
		// Token: 0x04000756 RID: 1878
		Alphanumeric,
		// Token: 0x04000757 RID: 1879
		Username,
		// Token: 0x04000758 RID: 1880
		Name
	}

	// Token: 0x0200010B RID: 267
	public enum KeyboardType
	{
		// Token: 0x0400075A RID: 1882
		Default,
		// Token: 0x0400075B RID: 1883
		ASCIICapable,
		// Token: 0x0400075C RID: 1884
		NumbersAndPunctuation,
		// Token: 0x0400075D RID: 1885
		URL,
		// Token: 0x0400075E RID: 1886
		NumberPad,
		// Token: 0x0400075F RID: 1887
		PhonePad,
		// Token: 0x04000760 RID: 1888
		NamePhonePad,
		// Token: 0x04000761 RID: 1889
		EmailAddress
	}

	// Token: 0x0200010C RID: 268
	public enum OnReturnKey
	{
		// Token: 0x04000763 RID: 1891
		Default,
		// Token: 0x04000764 RID: 1892
		Submit,
		// Token: 0x04000765 RID: 1893
		NewLine
	}

	// Token: 0x0200010D RID: 269
	// (Invoke) Token: 0x06000A46 RID: 2630
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
