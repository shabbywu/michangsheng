using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	public enum InputType
	{
		Standard,
		AutoCorrect,
		Password
	}

	public enum Validation
	{
		None,
		Integer,
		Float,
		Alphanumeric,
		Username,
		Name
	}

	public enum KeyboardType
	{
		Default,
		ASCIICapable,
		NumbersAndPunctuation,
		URL,
		NumberPad,
		PhonePad,
		NamePhonePad,
		EmailAddress
	}

	public enum OnReturnKey
	{
		Default,
		Submit,
		NewLine
	}

	public delegate char OnValidate(string text, int charIndex, char addedChar);

	public static UIInput current;

	public static UIInput selection;

	public UILabel label;

	public InputType inputType;

	public OnReturnKey onReturnKey;

	public KeyboardType keyboardType;

	public bool hideInput;

	public Validation validation;

	public int characterLimit;

	public string savedAs;

	public GameObject selectOnTab;

	public Color activeTextColor = Color.white;

	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	public Color selectionColor = new Color(1f, 0.8745098f, 47f / 85f, 0.5f);

	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	public List<EventDelegate> onChange = new List<EventDelegate>();

	public OnValidate onValidate;

	[SerializeField]
	[HideInInspector]
	protected string mValue;

	[NonSerialized]
	protected string mDefaultText = "";

	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	[NonSerialized]
	protected float mPosition;

	[NonSerialized]
	protected bool mDoInit = true;

	[NonSerialized]
	protected UIWidget.Pivot mPivot;

	[NonSerialized]
	protected bool mLoadSavedValue = true;

	protected static int mDrawStart = 0;

	protected static string mLastIME = "";

	[NonSerialized]
	protected int mSelectionStart;

	[NonSerialized]
	protected int mSelectionEnd;

	[NonSerialized]
	protected UITexture mHighlight;

	[NonSerialized]
	protected UITexture mCaret;

	[NonSerialized]
	protected Texture2D mBlankTex;

	[NonSerialized]
	protected float mNextBlink;

	[NonSerialized]
	protected float mLastAlpha;

	[NonSerialized]
	protected string mCached = "";

	[NonSerialized]
	protected int mSelectMe = -1;

	public string defaultText
	{
		get
		{
			return mDefaultText;
		}
		set
		{
			if (mDoInit)
			{
				Init();
			}
			mDefaultText = value;
			UpdateLabel();
		}
	}

	public bool inputShouldBeHidden
	{
		get
		{
			if (hideInput && (Object)(object)label != (Object)null && !label.multiLine)
			{
				return inputType != InputType.Password;
			}
			return false;
		}
	}

	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public string value
	{
		get
		{
			if (mDoInit)
			{
				Init();
			}
			return mValue;
		}
		set
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Invalid comparison between Unknown and I4
			if (mDoInit)
			{
				Init();
			}
			mDrawStart = 0;
			if ((int)Application.platform == 22)
			{
				value = value.Replace("\\b", "\b");
			}
			value = Validate(value);
			if (!(mValue != value))
			{
				return;
			}
			mValue = value;
			mLoadSavedValue = false;
			if (isSelected)
			{
				if (string.IsNullOrEmpty(value))
				{
					mSelectionStart = 0;
					mSelectionEnd = 0;
				}
				else
				{
					mSelectionStart = value.Length;
					mSelectionEnd = mSelectionStart;
				}
			}
			else
			{
				SaveToPlayerPrefs(value);
			}
			UpdateLabel();
			ExecuteOnChange();
		}
	}

	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return isSelected;
		}
		set
		{
			isSelected = value;
		}
	}

	public bool isSelected
	{
		get
		{
			return (Object)(object)selection == (Object)(object)this;
		}
		set
		{
			if (!value)
			{
				if (isSelected)
				{
					UICamera.selectedObject = null;
				}
			}
			else
			{
				UICamera.selectedObject = ((Component)this).gameObject;
			}
		}
	}

	public int cursorPosition
	{
		get
		{
			if (!isSelected)
			{
				return value.Length;
			}
			return mSelectionEnd;
		}
		set
		{
			if (isSelected)
			{
				mSelectionEnd = value;
				UpdateLabel();
			}
		}
	}

	public int selectionStart
	{
		get
		{
			if (!isSelected)
			{
				return value.Length;
			}
			return mSelectionStart;
		}
		set
		{
			if (isSelected)
			{
				mSelectionStart = value;
				UpdateLabel();
			}
		}
	}

	public int selectionEnd
	{
		get
		{
			if (!isSelected)
			{
				return value.Length;
			}
			return mSelectionEnd;
		}
		set
		{
			if (isSelected)
			{
				mSelectionEnd = value;
				UpdateLabel();
			}
		}
	}

	public UITexture caret => mCaret;

	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		for (int i = 0; i < val.Length; i++)
		{
			char c = val[i];
			if (onValidate != null)
			{
				c = onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (validation != 0)
			{
				c = Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != 0)
			{
				stringBuilder.Append(c);
			}
		}
		if (characterLimit > 0 && stringBuilder.Length > characterLimit)
		{
			return stringBuilder.ToString(0, characterLimit);
		}
		return stringBuilder.ToString();
	}

	private void Start()
	{
		if (mLoadSavedValue && !string.IsNullOrEmpty(savedAs))
		{
			LoadValue();
		}
		else
		{
			value = mValue.Replace("\\n", "\n");
		}
	}

	protected void Init()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (mDoInit && (Object)(object)label != (Object)null)
		{
			mDoInit = false;
			mDefaultText = label.text;
			mDefaultColor = label.color;
			label.supportEncoding = false;
			if (label.alignment == NGUIText.Alignment.Justified)
			{
				label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning((object)"Input fields using labels with justified alignment are not supported at this time", (Object)(object)this);
			}
			mPivot = label.pivot;
			mPosition = label.cachedTransform.localPosition.x;
			UpdateLabel();
		}
	}

	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(savedAs);
			}
			else
			{
				PlayerPrefs.SetString(savedAs, val);
			}
		}
	}

	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			OnSelectEvent();
		}
		else
		{
			OnDeselectEvent();
		}
	}

	protected void OnSelectEvent()
	{
		selection = this;
		if (mDoInit)
		{
			Init();
		}
		if ((Object)(object)label != (Object)null && NGUITools.GetActive((Behaviour)(object)this))
		{
			mSelectMe = Time.frameCount;
		}
	}

	protected void OnDeselectEvent()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (mDoInit)
		{
			Init();
		}
		if ((Object)(object)label != (Object)null && NGUITools.GetActive((Behaviour)(object)this))
		{
			mValue = value;
			if (string.IsNullOrEmpty(mValue))
			{
				label.text = mDefaultText;
				label.color = mDefaultColor;
			}
			else
			{
				label.text = mValue;
			}
			Input.imeCompositionMode = (IMECompositionMode)0;
			RestoreLabelPivot();
		}
		selection = null;
		UpdateLabel();
	}

	private void Update()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if (!isSelected)
		{
			return;
		}
		if (mDoInit)
		{
			Init();
		}
		if (mSelectMe != -1 && mSelectMe != Time.frameCount)
		{
			mSelectMe = -1;
			mSelectionStart = 0;
			mSelectionEnd = ((!string.IsNullOrEmpty(mValue)) ? mValue.Length : 0);
			mDrawStart = 0;
			label.color = activeTextColor;
			Vector2 val = Vector2.op_Implicit(((Object)(object)UICamera.current != (Object)null && (Object)(object)UICamera.current.cachedCamera != (Object)null) ? UICamera.current.cachedCamera.WorldToScreenPoint(label.worldCorners[0]) : label.worldCorners[0]);
			val.y = (float)Screen.height - val.y;
			Input.imeCompositionMode = (IMECompositionMode)1;
			Input.compositionCursorPos = val;
			UpdateLabel();
			return;
		}
		if ((Object)(object)selectOnTab != (Object)null && Input.GetKeyDown((KeyCode)9))
		{
			UICamera.selectedObject = selectOnTab;
			return;
		}
		string compositionString = Input.compositionString;
		if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
		{
			string inputString = Input.inputString;
			for (int i = 0; i < inputString.Length; i++)
			{
				char c = inputString[i];
				if (c >= ' ' && c != '\uf700' && c != '\uf701' && c != '\uf702' && c != '\uf703')
				{
					Insert(c.ToString());
				}
			}
		}
		if (mLastIME != compositionString)
		{
			mSelectionEnd = (string.IsNullOrEmpty(compositionString) ? mSelectionStart : (mValue.Length + compositionString.Length));
			mLastIME = compositionString;
			UpdateLabel();
			ExecuteOnChange();
		}
		if ((Object)(object)mCaret != (Object)null && mNextBlink < RealTime.time)
		{
			mNextBlink = RealTime.time + 0.5f;
			((Behaviour)mCaret).enabled = !((Behaviour)mCaret).enabled;
		}
		if (isSelected && mLastAlpha != label.finalAlpha)
		{
			UpdateLabel();
		}
	}

	private void OnGUI()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		if (isSelected && (int)Event.current.rawType == 4)
		{
			ProcessEvent(Event.current);
		}
	}

	protected void DoBackspace()
	{
		if (string.IsNullOrEmpty(mValue))
		{
			return;
		}
		if (mSelectionStart == mSelectionEnd)
		{
			if (mSelectionStart < 1)
			{
				return;
			}
			mSelectionEnd--;
		}
		Insert("");
	}

	protected virtual bool ProcessEvent(Event ev)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Invalid comparison between Unknown and I4
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Invalid comparison between Unknown and I4
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Invalid comparison between Unknown and I4
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Invalid comparison between Unknown and I4
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Invalid comparison between Unknown and I4
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Invalid comparison between Unknown and I4
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Invalid comparison between Unknown and I4
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Invalid comparison between Unknown and I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Invalid comparison between Unknown and I4
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected I4, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Invalid comparison between Unknown and I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Invalid comparison between Unknown and I4
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Invalid comparison between Unknown and I4
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)label == (Object)null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = (((int)platform == 0 || (int)platform == 1) ? ((ev.modifiers & 8) > 0) : ((ev.modifiers & 2) > 0));
		bool flag2 = (ev.modifiers & 1) > 0;
		KeyCode keyCode = ev.keyCode;
		if ((int)keyCode <= 99)
		{
			if ((int)keyCode <= 13)
			{
				if ((int)keyCode == 8)
				{
					ev.Use();
					DoBackspace();
					return true;
				}
				if ((int)keyCode == 13)
				{
					goto IL_0446;
				}
			}
			else
			{
				if ((int)keyCode == 97)
				{
					if (flag)
					{
						ev.Use();
						mSelectionStart = 0;
						mSelectionEnd = mValue.Length;
						UpdateLabel();
					}
					return true;
				}
				if ((int)keyCode == 99)
				{
					if (flag)
					{
						ev.Use();
						NGUITools.clipboard = GetSelection();
					}
					return true;
				}
			}
		}
		else
		{
			if ((int)keyCode > 120)
			{
				if ((int)keyCode != 127)
				{
					switch (keyCode - 271)
					{
					case 5:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							mSelectionEnd = Mathf.Max(mSelectionEnd - 1, 0);
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 4:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							mSelectionEnd = Mathf.Min(mSelectionEnd + 1, mValue.Length);
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 9:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							mSelectionEnd = 0;
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 10:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							mSelectionEnd = mValue.Length;
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 7:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							if (label.multiLine)
							{
								mSelectionEnd = label.GetCharacterIndex(mSelectionEnd, (KeyCode)278);
							}
							else
							{
								mSelectionEnd = 0;
							}
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 8:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							if (label.multiLine)
							{
								mSelectionEnd = label.GetCharacterIndex(mSelectionEnd, (KeyCode)279);
							}
							else
							{
								mSelectionEnd = mValue.Length;
							}
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 2:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							mSelectionEnd = label.GetCharacterIndex(mSelectionEnd, (KeyCode)273);
							if (mSelectionEnd != 0)
							{
								mSelectionEnd += mDrawStart;
							}
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 3:
						ev.Use();
						if (!string.IsNullOrEmpty(mValue))
						{
							mSelectionEnd = label.GetCharacterIndex(mSelectionEnd, (KeyCode)274);
							if (mSelectionEnd != label.processedText.Length)
							{
								mSelectionEnd += mDrawStart;
							}
							else
							{
								mSelectionEnd = mValue.Length;
							}
							if (!flag2)
							{
								mSelectionStart = mSelectionEnd;
							}
							UpdateLabel();
						}
						return true;
					case 0:
						break;
					default:
						goto IL_04b8;
					}
					goto IL_0446;
				}
				ev.Use();
				if (!string.IsNullOrEmpty(mValue))
				{
					if (mSelectionStart == mSelectionEnd)
					{
						if (mSelectionStart >= mValue.Length)
						{
							return true;
						}
						mSelectionEnd++;
					}
					Insert("");
				}
				return true;
			}
			if ((int)keyCode == 118)
			{
				if (flag)
				{
					ev.Use();
					Insert(NGUITools.clipboard);
				}
				return true;
			}
			if ((int)keyCode == 120)
			{
				if (flag)
				{
					ev.Use();
					NGUITools.clipboard = GetSelection();
					Insert("");
				}
				return true;
			}
		}
		goto IL_04b8;
		IL_04b8:
		return false;
		IL_0446:
		ev.Use();
		if (onReturnKey == OnReturnKey.NewLine || (onReturnKey == OnReturnKey.Default && label.multiLine && !flag && label.overflowMethod != UILabel.Overflow.ClampContent && validation == Validation.None))
		{
			Insert("\n");
		}
		else
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentKey = ev.keyCode;
			Submit();
			UICamera.currentKey = (KeyCode)0;
		}
		return true;
	}

	protected virtual void Insert(string text)
	{
		string leftText = GetLeftText();
		string rightText = GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		for (int length2 = text.Length; i < length2; i++)
		{
			char c = text[i];
			if (c == '\b')
			{
				DoBackspace();
				continue;
			}
			if (characterLimit > 0 && stringBuilder.Length + length >= characterLimit)
			{
				break;
			}
			if (onValidate != null)
			{
				c = onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (validation != 0)
			{
				c = Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != 0)
			{
				stringBuilder.Append(c);
			}
		}
		mSelectionStart = stringBuilder.Length;
		mSelectionEnd = mSelectionStart;
		int j = 0;
		for (int length3 = rightText.Length; j < length3; j++)
		{
			char c2 = rightText[j];
			if (onValidate != null)
			{
				c2 = onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (validation != 0)
			{
				c2 = Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != 0)
			{
				stringBuilder.Append(c2);
			}
		}
		mValue = stringBuilder.ToString();
		UpdateLabel();
		ExecuteOnChange();
	}

	protected string GetLeftText()
	{
		int num = Mathf.Min(mSelectionStart, mSelectionEnd);
		if (!string.IsNullOrEmpty(mValue) && num >= 0)
		{
			return mValue.Substring(0, num);
		}
		return "";
	}

	protected string GetRightText()
	{
		int num = Mathf.Max(mSelectionStart, mSelectionEnd);
		if (!string.IsNullOrEmpty(mValue) && num < mValue.Length)
		{
			return mValue.Substring(num);
		}
		return "";
	}

	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(mValue) || mSelectionStart == mSelectionEnd)
		{
			return "";
		}
		int num = Mathf.Min(mSelectionStart, mSelectionEnd);
		int num2 = Mathf.Max(mSelectionStart, mSelectionEnd);
		return mValue.Substring(num, num2 - num);
	}

	protected int GetCharUnderMouse()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Vector3[] worldCorners = label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane val = default(Plane);
		((Plane)(ref val))._002Ector(worldCorners[0], worldCorners[1], worldCorners[2]);
		float num = default(float);
		if (!((Plane)(ref val)).Raycast(currentRay, ref num))
		{
			return 0;
		}
		return mDrawStart + label.GetCharacterIndexAtPosition(((Ray)(ref currentRay)).GetPoint(num));
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && isSelected && (Object)(object)label != (Object)null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			selectionEnd = GetCharUnderMouse();
			if (!Input.GetKey((KeyCode)304) && !Input.GetKey((KeyCode)303))
			{
				selectionStart = mSelectionEnd;
			}
		}
	}

	protected virtual void OnDrag(Vector2 delta)
	{
		if ((Object)(object)label != (Object)null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			selectionEnd = GetCharUnderMouse();
		}
	}

	private void OnDisable()
	{
		Cleanup();
	}

	protected virtual void Cleanup()
	{
		if (Object.op_Implicit((Object)(object)mHighlight))
		{
			((Behaviour)mHighlight).enabled = false;
		}
		if (Object.op_Implicit((Object)(object)mCaret))
		{
			((Behaviour)mCaret).enabled = false;
		}
		if (Object.op_Implicit((Object)(object)mBlankTex))
		{
			NGUITools.Destroy((Object)(object)mBlankTex);
			mBlankTex = null;
		}
	}

	public void Submit()
	{
		if (NGUITools.GetActive((Behaviour)(object)this))
		{
			mValue = value;
			if ((Object)(object)current == (Object)null)
			{
				current = this;
				EventDelegate.Execute(onSubmit);
				current = null;
			}
			SaveToPlayerPrefs(mValue);
		}
	}

	public void UpdateLabel()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)label != (Object)null))
		{
			return;
		}
		if (mDoInit)
		{
			Init();
		}
		bool flag = isSelected;
		string text = value;
		bool flag2 = string.IsNullOrEmpty(text) && string.IsNullOrEmpty(Input.compositionString);
		label.color = ((flag2 && !flag) ? mDefaultColor : activeTextColor);
		string text2;
		if (flag2)
		{
			text2 = (flag ? "" : mDefaultText);
			RestoreLabelPivot();
		}
		else
		{
			if (inputType == InputType.Password)
			{
				text2 = "";
				string text3 = "*";
				if ((Object)(object)label.bitmapFont != (Object)null && label.bitmapFont.bmFont != null && label.bitmapFont.bmFont.GetGlyph(42) == null)
				{
					text3 = "x";
				}
				int i = 0;
				for (int length = text.Length; i < length; i++)
				{
					text2 += text3;
				}
			}
			else
			{
				text2 = text;
			}
			int num = (flag ? Mathf.Min(text2.Length, cursorPosition) : 0);
			string text4 = text2.Substring(0, num);
			if (flag)
			{
				text4 += Input.compositionString;
			}
			text2 = text4 + text2.Substring(num, text2.Length - num);
			if (flag && label.overflowMethod == UILabel.Overflow.ClampContent && label.maxLineCount == 1)
			{
				int num2 = label.CalculateOffsetToFit(text2);
				if (num2 == 0)
				{
					mDrawStart = 0;
					RestoreLabelPivot();
				}
				else if (num < mDrawStart)
				{
					mDrawStart = num;
					SetPivotToLeft();
				}
				else if (num2 < mDrawStart)
				{
					mDrawStart = num2;
					SetPivotToLeft();
				}
				else
				{
					num2 = label.CalculateOffsetToFit(text2.Substring(0, num));
					if (num2 > mDrawStart)
					{
						mDrawStart = num2;
						SetPivotToRight();
					}
				}
				if (mDrawStart != 0)
				{
					text2 = text2.Substring(mDrawStart, text2.Length - mDrawStart);
				}
			}
			else
			{
				mDrawStart = 0;
				RestoreLabelPivot();
			}
		}
		label.text = text2;
		if (flag)
		{
			int num3 = mSelectionStart - mDrawStart;
			int num4 = mSelectionEnd - mDrawStart;
			if ((Object)(object)mBlankTex == (Object)null)
			{
				mBlankTex = new Texture2D(2, 2, (TextureFormat)5, false);
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						mBlankTex.SetPixel(k, j, Color.white);
					}
				}
				mBlankTex.Apply();
			}
			if (num3 != num4)
			{
				if ((Object)(object)mHighlight == (Object)null)
				{
					mHighlight = NGUITools.AddWidget<UITexture>(label.cachedGameObject);
					((Object)mHighlight).name = "Input Highlight";
					mHighlight.mainTexture = (Texture)(object)mBlankTex;
					mHighlight.fillGeometry = false;
					mHighlight.pivot = label.pivot;
					mHighlight.SetAnchor(label.cachedTransform);
				}
				else
				{
					mHighlight.pivot = label.pivot;
					mHighlight.mainTexture = (Texture)(object)mBlankTex;
					mHighlight.MarkAsChanged();
					((Behaviour)mHighlight).enabled = true;
				}
			}
			if ((Object)(object)mCaret == (Object)null)
			{
				mCaret = NGUITools.AddWidget<UITexture>(label.cachedGameObject);
				((Object)mCaret).name = "Input Caret";
				mCaret.mainTexture = (Texture)(object)mBlankTex;
				mCaret.fillGeometry = false;
				mCaret.pivot = label.pivot;
				mCaret.SetAnchor(label.cachedTransform);
			}
			else
			{
				mCaret.pivot = label.pivot;
				mCaret.mainTexture = (Texture)(object)mBlankTex;
				mCaret.MarkAsChanged();
				((Behaviour)mCaret).enabled = true;
			}
			if (num3 != num4)
			{
				label.PrintOverlay(num3, num4, mCaret.geometry, mHighlight.geometry, caretColor, selectionColor);
				((Behaviour)mHighlight).enabled = mHighlight.geometry.hasVertices;
			}
			else
			{
				label.PrintOverlay(num3, num4, mCaret.geometry, null, caretColor, selectionColor);
				if ((Object)(object)mHighlight != (Object)null)
				{
					((Behaviour)mHighlight).enabled = false;
				}
			}
			mNextBlink = RealTime.time + 0.5f;
			mLastAlpha = label.finalAlpha;
		}
		else
		{
			Cleanup();
		}
	}

	protected void SetPivotToLeft()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(mPivot);
		pivotOffset.x = 0f;
		label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	protected void SetPivotToRight()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(mPivot);
		pivotOffset.x = 1f;
		label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	protected void RestoreLabelPivot()
	{
		if ((Object)(object)label != (Object)null && label.pivot != mPivot)
		{
			label.pivot = mPivot;
		}
	}

	protected char Validate(string text, int pos, char ch)
	{
		if (validation == Validation.None || !((Behaviour)this).enabled)
		{
			return ch;
		}
		if (validation == Validation.Integer)
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
		else if (validation == Validation.Float)
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
		else if (validation == Validation.Alphanumeric)
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
		else if (validation == Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return (char)(ch - 65 + 97);
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
		else if (validation == Validation.Name)
		{
			char c = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
			char c2 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return (char)(ch - 97 + 65);
				}
				return ch;
			}
			if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return (char)(ch - 65 + 97);
				}
				return ch;
			}
			switch (ch)
			{
			case '\'':
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
				break;
			case ' ':
				if (c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
				{
					return ch;
				}
				break;
			}
		}
		return '\0';
	}

	protected void ExecuteOnChange()
	{
		if ((Object)(object)current == (Object)null && EventDelegate.IsValid(onChange))
		{
			current = this;
			EventDelegate.Execute(onChange);
			current = null;
		}
	}

	public void RemoveFocus()
	{
		isSelected = false;
	}

	public void SaveValue()
	{
		SaveToPlayerPrefs(mValue);
	}

	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(savedAs))
		{
			string text = mValue.Replace("\\n", "\n");
			mValue = "";
			value = (PlayerPrefs.HasKey(savedAs) ? PlayerPrefs.GetString(savedAs) : text);
		}
	}
}
