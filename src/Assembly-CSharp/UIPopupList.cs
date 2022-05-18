using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000094 RID: 148
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00009481 File Offset: 0x00007681
	// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00073AF0 File Offset: 0x00071CF0
	public Object ambigiousFont
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.trueTypeFont;
			}
			if (this.bitmapFont != null)
			{
				return this.bitmapFont;
			}
			return this.font;
		}
		set
		{
			if (value is Font)
			{
				this.trueTypeFont = (value as Font);
				this.bitmapFont = null;
				this.font = null;
				return;
			}
			if (value is UIFont)
			{
				this.bitmapFont = (value as UIFont);
				this.trueTypeFont = null;
				this.font = null;
			}
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060005CA RID: 1482 RVA: 0x000094B3 File Offset: 0x000076B3
	// (set) Token: 0x060005CB RID: 1483 RVA: 0x000094BB File Offset: 0x000076BB
	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public UIPopupList.LegacyEvent onSelectionChange
	{
		get
		{
			return this.mLegacyEvent;
		}
		set
		{
			this.mLegacyEvent = value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060005CC RID: 1484 RVA: 0x000094C4 File Offset: 0x000076C4
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060005CD RID: 1485 RVA: 0x000094D2 File Offset: 0x000076D2
	// (set) Token: 0x060005CE RID: 1486 RVA: 0x000094DA File Offset: 0x000076DA
	public string value
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			this.mSelectedItem = value;
			if (this.mSelectedItem == null)
			{
				return;
			}
			if (this.mSelectedItem != null)
			{
				this.TriggerCallbacks();
			}
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060005CF RID: 1487 RVA: 0x000094FA File Offset: 0x000076FA
	// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00009502 File Offset: 0x00007702
	[Obsolete("Use 'value' instead")]
	public string selection
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

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00073B44 File Offset: 0x00071D44
	// (set) Token: 0x060005D2 RID: 1490 RVA: 0x00073B6C File Offset: 0x00071D6C
	private bool handleEvents
	{
		get
		{
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			return component == null || !component.enabled;
		}
		set
		{
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000950B File Offset: 0x0000770B
	private bool isValid
	{
		get
		{
			return this.bitmapFont != null || this.trueTypeFont != null;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00009529 File Offset: 0x00007729
	private int activeFontSize
	{
		get
		{
			if (!(this.trueTypeFont != null) && !(this.bitmapFont == null))
			{
				return this.bitmapFont.defaultSize;
			}
			return this.fontSize;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00009559 File Offset: 0x00007759
	private float activeFontScale
	{
		get
		{
			if (!(this.trueTypeFont != null) && !(this.bitmapFont == null))
			{
				return (float)this.fontSize / (float)this.bitmapFont.defaultSize;
			}
			return 1f;
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00073B94 File Offset: 0x00071D94
	protected void TriggerCallbacks()
	{
		if (UIPopupList.current != this)
		{
			UIPopupList uipopupList = UIPopupList.current;
			UIPopupList.current = this;
			if (this.mLegacyEvent != null)
			{
				this.mLegacyEvent(this.mSelectedItem);
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				EventDelegate.Execute(this.onChange);
			}
			else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, 1);
			}
			UIPopupList.current = uipopupList;
		}
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00073C24 File Offset: 0x00071E24
	private void OnEnable()
	{
		if (EventDelegate.IsValid(this.onChange))
		{
			this.eventReceiver = null;
			this.functionName = null;
		}
		if (this.font != null)
		{
			if (this.font.isDynamic)
			{
				this.trueTypeFont = this.font.dynamicFont;
				this.fontStyle = this.font.dynamicFontStyle;
				this.mUseDynamicFont = true;
			}
			else if (this.bitmapFont == null)
			{
				this.bitmapFont = this.font;
				this.mUseDynamicFont = false;
			}
			this.font = null;
		}
		if (this.textScale != 0f)
		{
			this.fontSize = ((this.bitmapFont != null) ? Mathf.RoundToInt((float)this.bitmapFont.defaultSize * this.textScale) : 16);
			this.textScale = 0f;
		}
		if (this.trueTypeFont == null && this.bitmapFont != null && this.bitmapFont.isDynamic)
		{
			this.trueTypeFont = this.bitmapFont.dynamicFont;
			this.bitmapFont = null;
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00073D44 File Offset: 0x00071F44
	private void OnValidate()
	{
		Font font = this.trueTypeFont;
		UIFont uifont = this.bitmapFont;
		this.bitmapFont = null;
		this.trueTypeFont = null;
		if (font != null && (uifont == null || !this.mUseDynamicFont))
		{
			this.bitmapFont = null;
			this.trueTypeFont = font;
			this.mUseDynamicFont = true;
			return;
		}
		if (!(uifont != null))
		{
			this.trueTypeFont = font;
			this.mUseDynamicFont = true;
			return;
		}
		if (uifont.isDynamic)
		{
			this.trueTypeFont = uifont.dynamicFont;
			this.fontStyle = uifont.dynamicFontStyle;
			this.fontSize = uifont.defaultSize;
			this.mUseDynamicFont = true;
			return;
		}
		this.bitmapFont = uifont;
		this.mUseDynamicFont = false;
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00073DF8 File Offset: 0x00071FF8
	private void Start()
	{
		if (this.textLabel != null)
		{
			EventDelegate.Add(this.onChange, new EventDelegate.Callback(this.textLabel.SetCurrentSelection));
			this.textLabel = null;
		}
		if (Application.isPlaying)
		{
			if (string.IsNullOrEmpty(this.mSelectedItem))
			{
				if (this.items.Count > 0)
				{
					this.value = this.items[0];
					return;
				}
			}
			else
			{
				string value = this.mSelectedItem;
				this.mSelectedItem = null;
				this.value = value;
			}
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00009591 File Offset: 0x00007791
	private void OnLocalize()
	{
		if (this.isLocalized)
		{
			this.TriggerCallbacks();
		}
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00073E84 File Offset: 0x00072084
	private void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			this.mHighlightedLabel = lbl;
			if (this.mHighlight.GetAtlasSprite() == null)
			{
				return;
			}
			Vector3 highlightPosition = this.GetHighlightPosition();
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = highlightPosition;
				return;
			}
			TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
			if (!this.mTweening)
			{
				this.mTweening = true;
				base.StartCoroutine(this.UpdateTweenPosition());
			}
		}
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00073F10 File Offset: 0x00072110
	private Vector3 GetHighlightPosition()
	{
		if (this.mHighlightedLabel == null)
		{
			return Vector3.zero;
		}
		UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return Vector3.zero;
		}
		float pixelSize = this.atlas.pixelSize;
		float num = (float)atlasSprite.borderLeft * pixelSize;
		float num2 = (float)atlasSprite.borderTop * pixelSize;
		return this.mHighlightedLabel.cachedTransform.localPosition + new Vector3(-num, num2, 1f);
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x000095A1 File Offset: 0x000077A1
	private IEnumerator UpdateTweenPosition()
	{
		if (this.mHighlight != null && this.mHighlightedLabel != null)
		{
			TweenPosition tp = this.mHighlight.GetComponent<TweenPosition>();
			while (tp != null && tp.enabled)
			{
				tp.to = this.GetHighlightPosition();
				yield return null;
			}
			tp = null;
		}
		this.mTweening = false;
		yield break;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00073F88 File Offset: 0x00072188
	public void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00073FA8 File Offset: 0x000721A8
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		UIEventListener component = lbl.gameObject.GetComponent<UIEventListener>();
		this.value = (component.parameter as string);
		UIPlaySound[] components = base.GetComponents<UIPlaySound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIPlaySound uiplaySound = components[i];
			if (uiplaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uiplaySound.audioClip, uiplaySound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x000095B0 File Offset: 0x000077B0
	public void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000095C2 File Offset: 0x000077C2
	public void OnItemClick(GameObject go)
	{
		this.Close();
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00074018 File Offset: 0x00072218
	private void OnKey(KeyCode key)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.handleEvents)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (num == -1)
			{
				num = 0;
			}
			if (key == 273)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
					return;
				}
			}
			else if (key == 274)
			{
				if (num + 1 < this.mLabelList.Count)
				{
					this.Select(this.mLabelList[num + 1], false);
					return;
				}
			}
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x000740B8 File Offset: 0x000722B8
	private void OnSelect(bool isSelected)
	{
		if (!isSelected)
		{
			this.Close();
			UIpopulTooll component = base.GetComponent<UIpopulTooll>();
			if (component != null)
			{
				component.Close();
			}
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x000740E4 File Offset: 0x000722E4
	public void Close()
	{
		if (this.mChild != null)
		{
			this.mLabelList.Clear();
			this.handleEvents = false;
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				Collider[] componentsInChildren2 = this.mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				int num2 = componentsInChildren2.Length;
				while (j < num2)
				{
					componentsInChildren2[j].enabled = false;
					j++;
				}
				Object.Destroy(this.mChild, 0.15f);
			}
			else
			{
				Object.Destroy(this.mChild);
			}
			this.mBackground = null;
			this.mHighlight = null;
			this.mChild = null;
		}
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x000741C8 File Offset: 0x000723C8
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00074218 File Offset: 0x00072418
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = placeAbove ? new Vector3(localPosition.x, bottom, localPosition.z) : new Vector3(localPosition.x, 0f, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		TweenPosition.Begin(widget.gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00074284 File Offset: 0x00072484
	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.activeFontSize * this.activeFontScale + this.mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(1f, num / (float)widget.height, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x000095CA File Offset: 0x000077CA
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00074334 File Offset: 0x00072534
	private void OnClick()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mChild == null && this.atlas != null && this.isValid && this.items.Count > 0)
		{
			this.mLabelList.Clear();
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform);
				if (this.mPanel == null)
				{
					return;
				}
			}
			this.handleEvents = true;
			Transform transform = base.transform;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = bounds.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.depth = NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = (float)atlasSprite.borderTop;
			float num2 = (float)this.activeFontSize;
			float activeFontScale = this.activeFontScale;
			float num3 = num2 * activeFontScale;
			float num4 = 0f;
			float num5 = -this.padding.y;
			int num6 = (this.bitmapFont != null) ? this.bitmapFont.defaultSize : this.fontSize;
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = NGUITools.AddWidget<UILabel>(this.mChild);
				uilabel.name = i.ToString();
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.bitmapFont = this.bitmapFont;
				uilabel.trueTypeFont = this.trueTypeFont;
				uilabel.fontSize = num6;
				uilabel.fontStyle = this.fontStyle;
				uilabel.text = (this.isLocalized ? Localization.Get(text) : text);
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x + this.padding.x, num5, -1f);
				uilabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uilabel.alignment = this.alignment;
				uilabel.MakePixelPerfect();
				if (activeFontScale != 1f)
				{
					uilabel.cachedTransform.localScale = Vector3.one * activeFontScale;
				}
				list.Add(uilabel);
				num5 -= num3;
				num5 -= this.padding.y;
				num4 = Mathf.Max(num4, uilabel.printedSize.x);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.onClick = new UIEventListener.VoidDelegate(this.OnItemClick);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(this.mSelectedItem)))
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num4 = Mathf.Max(num4, bounds.size.x * activeFontScale - (border.x + this.padding.x) * 2f);
			float num7 = num4 / activeFontScale;
			Vector3 vector;
			vector..ctor(num7 * 0.5f, -num2 * 0.5f, 0f);
			Vector3 vector2;
			vector2..ctor(num7, (num3 + this.padding.y) / activeFontScale, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				UILabel uilabel2 = list[j];
				NGUITools.AddWidgetCollider(uilabel2.gameObject);
				uilabel2.autoResizeBoxCollider = false;
				BoxCollider component = uilabel2.GetComponent<BoxCollider>();
				if (component != null)
				{
					vector.z = component.center.z;
					component.center = vector;
					component.size = vector2;
				}
				else
				{
					BoxCollider2D component2 = uilabel2.GetComponent<BoxCollider2D>();
					component2.offset = vector;
					component2.size = vector2;
				}
				j++;
			}
			int width = Mathf.RoundToInt(num4);
			num4 += (border.x + this.padding.x) * 2f;
			num5 -= border.y;
			this.mBackground.width = Mathf.RoundToInt(num4);
			this.mBackground.height = Mathf.RoundToInt(-num5 + border.y);
			int k = 0;
			int count3 = list.Count;
			while (k < count3)
			{
				UILabel uilabel3 = list[k];
				uilabel3.overflowMethod = UILabel.Overflow.ShrinkContent;
				uilabel3.width = width;
				k++;
			}
			float num8 = 2f * this.atlas.pixelSize;
			float num9 = num4 - (border.x + this.padding.x) * 2f + (float)atlasSprite.borderLeft * num8;
			float num10 = num3 + num * num8;
			this.mHighlight.width = Mathf.RoundToInt(num9);
			this.mHighlight.height = Mathf.RoundToInt(num10);
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f);
				}
			}
			if (this.isAnimated)
			{
				float bottom = num5 + num3;
				this.Animate(this.mHighlight, flag, bottom);
				int l = 0;
				int count4 = list.Count;
				while (l < count4)
				{
					this.Animate(list[l], flag, bottom);
					l++;
				}
				this.AnimateColor(this.mBackground);
				this.AnimateScale(this.mBackground, flag, bottom);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(bounds.min.x, bounds.max.y - num5 - border.y, bounds.min.z);
				return;
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x04000437 RID: 1079
	public static UIPopupList current;

	// Token: 0x04000438 RID: 1080
	private const float animSpeed = 0.15f;

	// Token: 0x04000439 RID: 1081
	public UIAtlas atlas;

	// Token: 0x0400043A RID: 1082
	public UIFont bitmapFont;

	// Token: 0x0400043B RID: 1083
	public Font trueTypeFont;

	// Token: 0x0400043C RID: 1084
	public int fontSize = 16;

	// Token: 0x0400043D RID: 1085
	public FontStyle fontStyle;

	// Token: 0x0400043E RID: 1086
	public string backgroundSprite;

	// Token: 0x0400043F RID: 1087
	public string highlightSprite;

	// Token: 0x04000440 RID: 1088
	public UIPopupList.Position position;

	// Token: 0x04000441 RID: 1089
	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	// Token: 0x04000442 RID: 1090
	public List<string> items = new List<string>();

	// Token: 0x04000443 RID: 1091
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x04000444 RID: 1092
	public Color textColor = Color.white;

	// Token: 0x04000445 RID: 1093
	public Color backgroundColor = Color.white;

	// Token: 0x04000446 RID: 1094
	public Color highlightColor = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x04000447 RID: 1095
	public bool isAnimated = true;

	// Token: 0x04000448 RID: 1096
	public bool isLocalized;

	// Token: 0x04000449 RID: 1097
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x0400044A RID: 1098
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x0400044B RID: 1099
	private UIPanel mPanel;

	// Token: 0x0400044C RID: 1100
	private GameObject mChild;

	// Token: 0x0400044D RID: 1101
	private UISprite mBackground;

	// Token: 0x0400044E RID: 1102
	private UISprite mHighlight;

	// Token: 0x0400044F RID: 1103
	private UILabel mHighlightedLabel;

	// Token: 0x04000450 RID: 1104
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x04000451 RID: 1105
	private float mBgBorder;

	// Token: 0x04000452 RID: 1106
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000453 RID: 1107
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	// Token: 0x04000454 RID: 1108
	[HideInInspector]
	[SerializeField]
	private float textScale;

	// Token: 0x04000455 RID: 1109
	[HideInInspector]
	[SerializeField]
	private UIFont font;

	// Token: 0x04000456 RID: 1110
	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	// Token: 0x04000457 RID: 1111
	private UIPopupList.LegacyEvent mLegacyEvent;

	// Token: 0x04000458 RID: 1112
	private bool mUseDynamicFont;

	// Token: 0x04000459 RID: 1113
	private bool mTweening;

	// Token: 0x02000095 RID: 149
	public enum Position
	{
		// Token: 0x0400045B RID: 1115
		Auto,
		// Token: 0x0400045C RID: 1116
		Above,
		// Token: 0x0400045D RID: 1117
		Below
	}

	// Token: 0x02000096 RID: 150
	// (Invoke) Token: 0x060005EC RID: 1516
	public delegate void LegacyEvent(string val);
}
