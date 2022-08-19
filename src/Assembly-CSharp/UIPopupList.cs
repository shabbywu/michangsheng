using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001D9F2 File Offset: 0x0001BBF2
	// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001DA24 File Offset: 0x0001BC24
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

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001DA76 File Offset: 0x0001BC76
	// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001DA7E File Offset: 0x0001BC7E
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

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001DA87 File Offset: 0x0001BC87
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001DA95 File Offset: 0x0001BC95
	// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001DA9D File Offset: 0x0001BC9D
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

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001DABD File Offset: 0x0001BCBD
	// (set) Token: 0x0600057A RID: 1402 RVA: 0x0001DAC5 File Offset: 0x0001BCC5
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

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
	// (set) Token: 0x0600057C RID: 1404 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
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

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001DB1F File Offset: 0x0001BD1F
	private bool isValid
	{
		get
		{
			return this.bitmapFont != null || this.trueTypeFont != null;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001DB3D File Offset: 0x0001BD3D
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

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001DB6D File Offset: 0x0001BD6D
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

	// Token: 0x06000580 RID: 1408 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
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

	// Token: 0x06000581 RID: 1409 RVA: 0x0001DC38 File Offset: 0x0001BE38
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

	// Token: 0x06000582 RID: 1410 RVA: 0x0001DD58 File Offset: 0x0001BF58
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

	// Token: 0x06000583 RID: 1411 RVA: 0x0001DE0C File Offset: 0x0001C00C
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

	// Token: 0x06000584 RID: 1412 RVA: 0x0001DE95 File Offset: 0x0001C095
	private void OnLocalize()
	{
		if (this.isLocalized)
		{
			this.TriggerCallbacks();
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
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

	// Token: 0x06000586 RID: 1414 RVA: 0x0001DF34 File Offset: 0x0001C134
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

	// Token: 0x06000587 RID: 1415 RVA: 0x0001DFAC File Offset: 0x0001C1AC
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

	// Token: 0x06000588 RID: 1416 RVA: 0x0001DFBC File Offset: 0x0001C1BC
	public void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001DFDC File Offset: 0x0001C1DC
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

	// Token: 0x0600058A RID: 1418 RVA: 0x0001E04C File Offset: 0x0001C24C
	public void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0001E05E File Offset: 0x0001C25E
	public void OnItemClick(GameObject go)
	{
		this.Close();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0001E068 File Offset: 0x0001C268
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

	// Token: 0x0600058D RID: 1421 RVA: 0x0001E108 File Offset: 0x0001C308
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

	// Token: 0x0600058E RID: 1422 RVA: 0x0001E134 File Offset: 0x0001C334
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

	// Token: 0x0600058F RID: 1423 RVA: 0x0001E218 File Offset: 0x0001C418
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0001E268 File Offset: 0x0001C468
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = placeAbove ? new Vector3(localPosition.x, bottom, localPosition.z) : new Vector3(localPosition.x, 0f, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		TweenPosition.Begin(widget.gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
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

	// Token: 0x06000592 RID: 1426 RVA: 0x0001E382 File Offset: 0x0001C582
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001E394 File Offset: 0x0001C594
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

	// Token: 0x04000392 RID: 914
	public static UIPopupList current;

	// Token: 0x04000393 RID: 915
	private const float animSpeed = 0.15f;

	// Token: 0x04000394 RID: 916
	public UIAtlas atlas;

	// Token: 0x04000395 RID: 917
	public UIFont bitmapFont;

	// Token: 0x04000396 RID: 918
	public Font trueTypeFont;

	// Token: 0x04000397 RID: 919
	public int fontSize = 16;

	// Token: 0x04000398 RID: 920
	public FontStyle fontStyle;

	// Token: 0x04000399 RID: 921
	public string backgroundSprite;

	// Token: 0x0400039A RID: 922
	public string highlightSprite;

	// Token: 0x0400039B RID: 923
	public UIPopupList.Position position;

	// Token: 0x0400039C RID: 924
	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	// Token: 0x0400039D RID: 925
	public List<string> items = new List<string>();

	// Token: 0x0400039E RID: 926
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x0400039F RID: 927
	public Color textColor = Color.white;

	// Token: 0x040003A0 RID: 928
	public Color backgroundColor = Color.white;

	// Token: 0x040003A1 RID: 929
	public Color highlightColor = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x040003A2 RID: 930
	public bool isAnimated = true;

	// Token: 0x040003A3 RID: 931
	public bool isLocalized;

	// Token: 0x040003A4 RID: 932
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040003A5 RID: 933
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x040003A6 RID: 934
	private UIPanel mPanel;

	// Token: 0x040003A7 RID: 935
	private GameObject mChild;

	// Token: 0x040003A8 RID: 936
	private UISprite mBackground;

	// Token: 0x040003A9 RID: 937
	private UISprite mHighlight;

	// Token: 0x040003AA RID: 938
	private UILabel mHighlightedLabel;

	// Token: 0x040003AB RID: 939
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x040003AC RID: 940
	private float mBgBorder;

	// Token: 0x040003AD RID: 941
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x040003AE RID: 942
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	// Token: 0x040003AF RID: 943
	[HideInInspector]
	[SerializeField]
	private float textScale;

	// Token: 0x040003B0 RID: 944
	[HideInInspector]
	[SerializeField]
	private UIFont font;

	// Token: 0x040003B1 RID: 945
	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	// Token: 0x040003B2 RID: 946
	private UIPopupList.LegacyEvent mLegacyEvent;

	// Token: 0x040003B3 RID: 947
	private bool mUseDynamicFont;

	// Token: 0x040003B4 RID: 948
	private bool mTweening;

	// Token: 0x020011E9 RID: 4585
	public enum Position
	{
		// Token: 0x040063F5 RID: 25589
		Auto,
		// Token: 0x040063F6 RID: 25590
		Above,
		// Token: 0x040063F7 RID: 25591
		Below
	}

	// Token: 0x020011EA RID: 4586
	// (Invoke) Token: 0x0600780D RID: 30733
	public delegate void LegacyEvent(string val);
}
