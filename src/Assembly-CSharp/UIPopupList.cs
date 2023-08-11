using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	public enum Position
	{
		Auto,
		Above,
		Below
	}

	public delegate void LegacyEvent(string val);

	public static UIPopupList current;

	private const float animSpeed = 0.15f;

	public UIAtlas atlas;

	public UIFont bitmapFont;

	public Font trueTypeFont;

	public int fontSize = 16;

	public FontStyle fontStyle;

	public string backgroundSprite;

	public string highlightSprite;

	public Position position;

	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	public List<string> items = new List<string>();

	public Vector2 padding = Vector2.op_Implicit(new Vector3(4f, 4f));

	public Color textColor = Color.white;

	public Color backgroundColor = Color.white;

	public Color highlightColor = new Color(0.88235295f, 40f / 51f, 0.5882353f, 1f);

	public bool isAnimated = true;

	public bool isLocalized;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	private UIPanel mPanel;

	private GameObject mChild;

	private UISprite mBackground;

	private UISprite mHighlight;

	private UILabel mHighlightedLabel;

	private List<UILabel> mLabelList = new List<UILabel>();

	private float mBgBorder;

	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	[HideInInspector]
	[SerializeField]
	private float textScale;

	[HideInInspector]
	[SerializeField]
	private UIFont font;

	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	private LegacyEvent mLegacyEvent;

	private bool mUseDynamicFont;

	private bool mTweening;

	public Object ambigiousFont
	{
		get
		{
			if ((Object)(object)trueTypeFont != (Object)null)
			{
				return (Object)(object)trueTypeFont;
			}
			if ((Object)(object)bitmapFont != (Object)null)
			{
				return (Object)(object)bitmapFont;
			}
			return (Object)(object)font;
		}
		set
		{
			if (value is Font)
			{
				trueTypeFont = (Font)(object)((value is Font) ? value : null);
				bitmapFont = null;
				font = null;
			}
			else if (value is UIFont)
			{
				bitmapFont = value as UIFont;
				trueTypeFont = null;
				font = null;
			}
		}
	}

	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public LegacyEvent onSelectionChange
	{
		get
		{
			return mLegacyEvent;
		}
		set
		{
			mLegacyEvent = value;
		}
	}

	public bool isOpen => (Object)(object)mChild != (Object)null;

	public string value
	{
		get
		{
			return mSelectedItem;
		}
		set
		{
			mSelectedItem = value;
			if (mSelectedItem != null && mSelectedItem != null)
			{
				TriggerCallbacks();
			}
		}
	}

	[Obsolete("Use 'value' instead")]
	public string selection
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

	private bool handleEvents
	{
		get
		{
			UIKeyNavigation component = ((Component)this).GetComponent<UIKeyNavigation>();
			if (!((Object)(object)component == (Object)null))
			{
				return !((Behaviour)component).enabled;
			}
			return true;
		}
		set
		{
			UIKeyNavigation component = ((Component)this).GetComponent<UIKeyNavigation>();
			if ((Object)(object)component != (Object)null)
			{
				((Behaviour)component).enabled = !value;
			}
		}
	}

	private bool isValid
	{
		get
		{
			if (!((Object)(object)bitmapFont != (Object)null))
			{
				return (Object)(object)trueTypeFont != (Object)null;
			}
			return true;
		}
	}

	private int activeFontSize
	{
		get
		{
			if (!((Object)(object)trueTypeFont != (Object)null) && !((Object)(object)bitmapFont == (Object)null))
			{
				return bitmapFont.defaultSize;
			}
			return fontSize;
		}
	}

	private float activeFontScale
	{
		get
		{
			if (!((Object)(object)trueTypeFont != (Object)null) && !((Object)(object)bitmapFont == (Object)null))
			{
				return (float)fontSize / (float)bitmapFont.defaultSize;
			}
			return 1f;
		}
	}

	protected void TriggerCallbacks()
	{
		if ((Object)(object)current != (Object)(object)this)
		{
			UIPopupList uIPopupList = current;
			current = this;
			if (mLegacyEvent != null)
			{
				mLegacyEvent(mSelectedItem);
			}
			if (EventDelegate.IsValid(onChange))
			{
				EventDelegate.Execute(onChange);
			}
			else if ((Object)(object)eventReceiver != (Object)null && !string.IsNullOrEmpty(functionName))
			{
				eventReceiver.SendMessage(functionName, (object)mSelectedItem, (SendMessageOptions)1);
			}
			current = uIPopupList;
		}
	}

	private void OnEnable()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (EventDelegate.IsValid(onChange))
		{
			eventReceiver = null;
			functionName = null;
		}
		if ((Object)(object)font != (Object)null)
		{
			if (font.isDynamic)
			{
				trueTypeFont = font.dynamicFont;
				fontStyle = font.dynamicFontStyle;
				mUseDynamicFont = true;
			}
			else if ((Object)(object)bitmapFont == (Object)null)
			{
				bitmapFont = font;
				mUseDynamicFont = false;
			}
			font = null;
		}
		if (textScale != 0f)
		{
			fontSize = (((Object)(object)bitmapFont != (Object)null) ? Mathf.RoundToInt((float)bitmapFont.defaultSize * textScale) : 16);
			textScale = 0f;
		}
		if ((Object)(object)trueTypeFont == (Object)null && (Object)(object)bitmapFont != (Object)null && bitmapFont.isDynamic)
		{
			trueTypeFont = bitmapFont.dynamicFont;
			bitmapFont = null;
		}
	}

	private void OnValidate()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Font val = trueTypeFont;
		UIFont uIFont = bitmapFont;
		bitmapFont = null;
		trueTypeFont = null;
		if ((Object)(object)val != (Object)null && ((Object)(object)uIFont == (Object)null || !mUseDynamicFont))
		{
			bitmapFont = null;
			trueTypeFont = val;
			mUseDynamicFont = true;
		}
		else if ((Object)(object)uIFont != (Object)null)
		{
			if (uIFont.isDynamic)
			{
				trueTypeFont = uIFont.dynamicFont;
				fontStyle = uIFont.dynamicFontStyle;
				fontSize = uIFont.defaultSize;
				mUseDynamicFont = true;
			}
			else
			{
				bitmapFont = uIFont;
				mUseDynamicFont = false;
			}
		}
		else
		{
			trueTypeFont = val;
			mUseDynamicFont = true;
		}
	}

	private void Start()
	{
		if ((Object)(object)textLabel != (Object)null)
		{
			EventDelegate.Add(onChange, textLabel.SetCurrentSelection);
			textLabel = null;
		}
		if (!Application.isPlaying)
		{
			return;
		}
		if (string.IsNullOrEmpty(mSelectedItem))
		{
			if (items.Count > 0)
			{
				value = items[0];
			}
		}
		else
		{
			string text = mSelectedItem;
			mSelectedItem = null;
			value = text;
		}
	}

	private void OnLocalize()
	{
		if (isLocalized)
		{
			TriggerCallbacks();
		}
	}

	private void Highlight(UILabel lbl, bool instant)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)mHighlight != (Object)null))
		{
			return;
		}
		mHighlightedLabel = lbl;
		if (mHighlight.GetAtlasSprite() == null)
		{
			return;
		}
		Vector3 highlightPosition = GetHighlightPosition();
		if (instant || !isAnimated)
		{
			mHighlight.cachedTransform.localPosition = highlightPosition;
			return;
		}
		TweenPosition.Begin(((Component)mHighlight).gameObject, 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
		if (!mTweening)
		{
			mTweening = true;
			((MonoBehaviour)this).StartCoroutine(UpdateTweenPosition());
		}
	}

	private Vector3 GetHighlightPosition()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mHighlightedLabel == (Object)null)
		{
			return Vector3.zero;
		}
		UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return Vector3.zero;
		}
		float pixelSize = atlas.pixelSize;
		float num = (float)atlasSprite.borderLeft * pixelSize;
		float num2 = (float)atlasSprite.borderTop * pixelSize;
		return mHighlightedLabel.cachedTransform.localPosition + new Vector3(0f - num, num2, 1f);
	}

	private IEnumerator UpdateTweenPosition()
	{
		if ((Object)(object)mHighlight != (Object)null && (Object)(object)mHighlightedLabel != (Object)null)
		{
			TweenPosition tp = ((Component)mHighlight).GetComponent<TweenPosition>();
			while ((Object)(object)tp != (Object)null && ((Behaviour)tp).enabled)
			{
				tp.to = GetHighlightPosition();
				yield return null;
			}
		}
		mTweening = false;
	}

	public void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			Highlight(component, instant: false);
		}
	}

	private void Select(UILabel lbl, bool instant)
	{
		Highlight(lbl, instant);
		UIEventListener component = ((Component)lbl).gameObject.GetComponent<UIEventListener>();
		value = component.parameter as string;
		UIPlaySound[] components = ((Component)this).GetComponents<UIPlaySound>();
		int i = 0;
		for (int num = components.Length; i < num; i++)
		{
			UIPlaySound uIPlaySound = components[i];
			if (uIPlaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uIPlaySound.audioClip, uIPlaySound.volume, 1f);
			}
		}
	}

	public void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			Select(go.GetComponent<UILabel>(), instant: true);
		}
	}

	public void OnItemClick(GameObject go)
	{
		Close();
	}

	private void OnKey(KeyCode key)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Invalid comparison between Unknown and I4
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Invalid comparison between Unknown and I4
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (!((Behaviour)this).enabled || !NGUITools.GetActive(((Component)this).gameObject) || !handleEvents)
		{
			return;
		}
		int num = mLabelList.IndexOf(mHighlightedLabel);
		if (num == -1)
		{
			num = 0;
		}
		if ((int)key == 273)
		{
			if (num > 0)
			{
				Select(mLabelList[--num], instant: false);
			}
		}
		else if ((int)key == 274)
		{
			if (num + 1 < mLabelList.Count)
			{
				Select(mLabelList[++num], instant: false);
			}
		}
		else
		{
			_ = 27;
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (!isSelected)
		{
			Close();
			UIpopulTooll component = ((Component)this).GetComponent<UIpopulTooll>();
			if ((Object)(object)component != (Object)null)
			{
				component.Close();
			}
		}
	}

	public void Close()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)mChild != (Object)null))
		{
			return;
		}
		mLabelList.Clear();
		handleEvents = false;
		if (isAnimated)
		{
			UIWidget[] componentsInChildren = mChild.GetComponentsInChildren<UIWidget>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				UIWidget obj = componentsInChildren[i];
				Color color = obj.color;
				color.a = 0f;
				TweenColor.Begin(((Component)obj).gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
			}
			Collider[] componentsInChildren2 = mChild.GetComponentsInChildren<Collider>();
			int j = 0;
			for (int num2 = componentsInChildren2.Length; j < num2; j++)
			{
				componentsInChildren2[j].enabled = false;
			}
			Object.Destroy((Object)(object)mChild, 0.15f);
		}
		else
		{
			Object.Destroy((Object)(object)mChild);
		}
		mBackground = null;
		mHighlight = null;
		mChild = null;
	}

	private void AnimateColor(UIWidget widget)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(((Component)widget).gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (placeAbove ? new Vector3(localPosition.x, bottom, localPosition.z) : new Vector3(localPosition.x, 0f, localPosition.z));
		widget.cachedTransform.localPosition = localPosition2;
		TweenPosition.Begin(((Component)widget).gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = ((Component)widget).gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)activeFontSize * activeFontScale + mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(1f, num / (float)widget.height, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		AnimateColor(widget);
		AnimatePosition(widget, placeAbove, bottom);
	}

	private void OnClick()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled && NGUITools.GetActive(((Component)this).gameObject) && (Object)(object)mChild == (Object)null && (Object)(object)atlas != (Object)null && isValid && items.Count > 0)
		{
			mLabelList.Clear();
			if ((Object)(object)mPanel == (Object)null)
			{
				mPanel = UIPanel.Find(((Component)this).transform);
				if ((Object)(object)mPanel == (Object)null)
				{
					return;
				}
			}
			handleEvents = true;
			Transform transform = ((Component)this).transform;
			Bounds val = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			mChild = new GameObject("Drop-down List");
			mChild.layer = ((Component)this).gameObject.layer;
			Transform transform2 = mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = ((Bounds)(ref val)).min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			mBackground = NGUITools.AddSprite(mChild, atlas, backgroundSprite);
			mBackground.pivot = UIWidget.Pivot.TopLeft;
			mBackground.depth = NGUITools.CalculateNextDepth(((Component)mPanel).gameObject);
			mBackground.color = backgroundColor;
			Vector4 border = mBackground.border;
			mBgBorder = border.y;
			mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			mHighlight = NGUITools.AddSprite(mChild, atlas, highlightSprite);
			mHighlight.pivot = UIWidget.Pivot.TopLeft;
			mHighlight.color = highlightColor;
			UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = atlasSprite.borderTop;
			float num2 = activeFontSize;
			float num3 = activeFontScale;
			float num4 = num2 * num3;
			float num5 = 0f;
			float num6 = 0f - padding.y;
			int num7 = (((Object)(object)bitmapFont != (Object)null) ? bitmapFont.defaultSize : fontSize);
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			for (int count = items.Count; i < count; i++)
			{
				string text = items[i];
				UILabel uILabel = NGUITools.AddWidget<UILabel>(mChild);
				((Object)uILabel).name = i.ToString();
				uILabel.pivot = UIWidget.Pivot.TopLeft;
				uILabel.bitmapFont = bitmapFont;
				uILabel.trueTypeFont = trueTypeFont;
				uILabel.fontSize = num7;
				uILabel.fontStyle = fontStyle;
				uILabel.text = (isLocalized ? Localization.Get(text) : text);
				uILabel.color = textColor;
				uILabel.cachedTransform.localPosition = new Vector3(border.x + padding.x, num6, -1f);
				uILabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uILabel.alignment = alignment;
				uILabel.MakePixelPerfect();
				if (num3 != 1f)
				{
					uILabel.cachedTransform.localScale = Vector3.one * num3;
				}
				list.Add(uILabel);
				num6 -= num4;
				num6 -= padding.y;
				num5 = Mathf.Max(num5, uILabel.printedSize.x);
				UIEventListener uIEventListener = UIEventListener.Get(((Component)uILabel).gameObject);
				uIEventListener.onHover = OnItemHover;
				uIEventListener.onPress = OnItemPress;
				uIEventListener.onClick = OnItemClick;
				uIEventListener.parameter = text;
				if (mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(mSelectedItem)))
				{
					Highlight(uILabel, instant: true);
				}
				mLabelList.Add(uILabel);
			}
			num5 = Mathf.Max(num5, ((Bounds)(ref val)).size.x * num3 - (border.x + padding.x) * 2f);
			float num8 = num5 / num3;
			Vector3 val2 = default(Vector3);
			((Vector3)(ref val2))._002Ector(num8 * 0.5f, (0f - num2) * 0.5f, 0f);
			Vector3 val3 = default(Vector3);
			((Vector3)(ref val3))._002Ector(num8, (num4 + padding.y) / num3, 1f);
			int j = 0;
			for (int count2 = list.Count; j < count2; j++)
			{
				UILabel uILabel2 = list[j];
				NGUITools.AddWidgetCollider(((Component)uILabel2).gameObject);
				uILabel2.autoResizeBoxCollider = false;
				BoxCollider component = ((Component)uILabel2).GetComponent<BoxCollider>();
				if ((Object)(object)component != (Object)null)
				{
					val2.z = component.center.z;
					component.center = val2;
					component.size = val3;
				}
				else
				{
					BoxCollider2D component2 = ((Component)uILabel2).GetComponent<BoxCollider2D>();
					((Collider2D)component2).offset = Vector2.op_Implicit(val2);
					component2.size = Vector2.op_Implicit(val3);
				}
			}
			int width = Mathf.RoundToInt(num5);
			num5 += (border.x + padding.x) * 2f;
			num6 -= border.y;
			mBackground.width = Mathf.RoundToInt(num5);
			mBackground.height = Mathf.RoundToInt(0f - num6 + border.y);
			int k = 0;
			for (int count3 = list.Count; k < count3; k++)
			{
				UILabel uILabel3 = list[k];
				uILabel3.overflowMethod = UILabel.Overflow.ShrinkContent;
				uILabel3.width = width;
			}
			float num9 = 2f * atlas.pixelSize;
			float num10 = num5 - (border.x + padding.x) * 2f + (float)atlasSprite.borderLeft * num9;
			float num11 = num4 + num * num9;
			mHighlight.width = Mathf.RoundToInt(num10);
			mHighlight.height = Mathf.RoundToInt(num11);
			bool flag = position == Position.Above;
			if (position == Position.Auto)
			{
				UICamera uICamera = UICamera.FindCameraForLayer(((Component)this).gameObject.layer);
				if ((Object)(object)uICamera != (Object)null)
				{
					flag = uICamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f;
				}
			}
			if (isAnimated)
			{
				float bottom = num6 + num4;
				Animate(mHighlight, flag, bottom);
				int l = 0;
				for (int count4 = list.Count; l < count4; l++)
				{
					Animate(list[l], flag, bottom);
				}
				AnimateColor(mBackground);
				AnimateScale(mBackground, flag, bottom);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(((Bounds)(ref val)).min.x, ((Bounds)(ref val)).max.y - num6 - border.y, ((Bounds)(ref val)).min.z);
			}
		}
		else
		{
			OnSelect(isSelected: false);
		}
	}
}
