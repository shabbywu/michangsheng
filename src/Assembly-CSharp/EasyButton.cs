using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
[ExecuteInEditMode]
public class EasyButton : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000C75 RID: 3189 RVA: 0x0004A74C File Offset: 0x0004894C
	// (remove) Token: 0x06000C76 RID: 3190 RVA: 0x0004A780 File Offset: 0x00048980
	public static event EasyButton.ButtonDownHandler On_ButtonDown;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000C77 RID: 3191 RVA: 0x0004A7B4 File Offset: 0x000489B4
	// (remove) Token: 0x06000C78 RID: 3192 RVA: 0x0004A7E8 File Offset: 0x000489E8
	public static event EasyButton.ButtonPressHandler On_ButtonPress;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000C79 RID: 3193 RVA: 0x0004A81C File Offset: 0x00048A1C
	// (remove) Token: 0x06000C7A RID: 3194 RVA: 0x0004A850 File Offset: 0x00048A50
	public static event EasyButton.ButtonUpHandler On_ButtonUp;

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0004A883 File Offset: 0x00048A83
	// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0004A88B File Offset: 0x00048A8B
	public EasyButton.ButtonAnchor Anchor
	{
		get
		{
			return this.anchor;
		}
		set
		{
			this.anchor = value;
			this.ComputeButtonAnchor(this.anchor);
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0004A8A0 File Offset: 0x00048AA0
	// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0004A8A8 File Offset: 0x00048AA8
	public Vector2 Offset
	{
		get
		{
			return this.offset;
		}
		set
		{
			this.offset = value;
			this.ComputeButtonAnchor(this.anchor);
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0004A8BD File Offset: 0x00048ABD
	// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0004A8C5 File Offset: 0x00048AC5
	public Vector2 Scale
	{
		get
		{
			return this.scale;
		}
		set
		{
			this.scale = value;
			this.ComputeButtonAnchor(this.anchor);
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0004A8DA File Offset: 0x00048ADA
	// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0004A8E2 File Offset: 0x00048AE2
	public Texture2D NormalTexture
	{
		get
		{
			return this.normalTexture;
		}
		set
		{
			this.normalTexture = value;
			if (this.normalTexture != null)
			{
				this.ComputeButtonAnchor(this.anchor);
				this.currentTexture = this.normalTexture;
			}
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0004A911 File Offset: 0x00048B11
	// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0004A919 File Offset: 0x00048B19
	public Texture2D ActiveTexture
	{
		get
		{
			return this.activeTexture;
		}
		set
		{
			this.activeTexture = value;
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0004A922 File Offset: 0x00048B22
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0004A958 File Offset: 0x00048B58
	private void OnDisable()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.buttonRect);
		}
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0004A9AC File Offset: 0x00048BAC
	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.buttonRect);
		}
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0004A9FE File Offset: 0x00048BFE
	private void Start()
	{
		this.currentTexture = this.normalTexture;
		this.currentColor = this.buttonNormalColor;
		this.buttonState = EasyButton.ButtonState.None;
		VirtualScreen.ComputeVirtualScreen();
		this.ComputeButtonAnchor(this.anchor);
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0004AA30 File Offset: 0x00048C30
	private void OnGUI()
	{
		if (this.enable)
		{
			GUI.depth = this.guiDepth;
			base.useGUILayout = this.isUseGuiLayout;
			VirtualScreen.ComputeVirtualScreen();
			VirtualScreen.SetGuiScaleMatrix();
			if (this.normalTexture != null && this.activeTexture != null)
			{
				this.ComputeButtonAnchor(this.anchor);
				if (this.normalTexture != null)
				{
					if (Application.isEditor && !Application.isPlaying)
					{
						this.currentTexture = this.normalTexture;
					}
					if (this.showDebugArea && Application.isEditor)
					{
						GUI.Box(this.buttonRect, "");
					}
					if (this.currentTexture != null)
					{
						if (this.isActivated)
						{
							GUI.color = this.currentColor;
							if (Application.isPlaying)
							{
								EasyTouch.RemoveReservedArea(this.buttonRect);
								EasyTouch.AddReservedArea(this.buttonRect);
							}
						}
						else
						{
							GUI.color = new Color(this.currentColor.r, this.currentColor.g, this.currentColor.b, 0.2f);
							if (Application.isPlaying)
							{
								EasyTouch.RemoveReservedArea(this.buttonRect);
							}
						}
						GUI.DrawTexture(this.buttonRect, this.currentTexture);
						GUI.color = Color.white;
						return;
					}
				}
			}
		}
		else
		{
			EasyTouch.RemoveReservedArea(this.buttonRect);
		}
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0004AB8D File Offset: 0x00048D8D
	private void Update()
	{
		if (this.buttonState == EasyButton.ButtonState.Up)
		{
			this.buttonState = EasyButton.ButtonState.None;
		}
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00004095 File Offset: 0x00002295
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0004ABA0 File Offset: 0x00048DA0
	private void ComputeButtonAnchor(EasyButton.ButtonAnchor anchor)
	{
		if (this.normalTexture != null)
		{
			Vector2 vector;
			vector..ctor((float)this.normalTexture.width * this.scale.x, (float)this.normalTexture.height * this.scale.y);
			Vector2 zero = Vector2.zero;
			switch (anchor)
			{
			case EasyButton.ButtonAnchor.UpperLeft:
				zero..ctor(0f, 0f);
				break;
			case EasyButton.ButtonAnchor.UpperCenter:
				zero..ctor(VirtualScreen.width / 2f - vector.x / 2f, this.offset.y);
				break;
			case EasyButton.ButtonAnchor.UpperRight:
				zero..ctor(VirtualScreen.width - vector.x, 0f);
				break;
			case EasyButton.ButtonAnchor.MiddleLeft:
				zero..ctor(0f, VirtualScreen.height / 2f - vector.y / 2f);
				break;
			case EasyButton.ButtonAnchor.MiddleCenter:
				zero..ctor(VirtualScreen.width / 2f - vector.x / 2f, VirtualScreen.height / 2f - vector.y / 2f);
				break;
			case EasyButton.ButtonAnchor.MiddleRight:
				zero..ctor(VirtualScreen.width - vector.x, VirtualScreen.height / 2f - vector.y / 2f);
				break;
			case EasyButton.ButtonAnchor.LowerLeft:
				zero..ctor(0f, VirtualScreen.height - vector.y);
				break;
			case EasyButton.ButtonAnchor.LowerCenter:
				zero..ctor(VirtualScreen.width / 2f - vector.x / 2f, VirtualScreen.height - vector.y);
				break;
			case EasyButton.ButtonAnchor.LowerRight:
				zero..ctor(VirtualScreen.width - vector.x, VirtualScreen.height - vector.y);
				break;
			}
			this.buttonRect = new Rect(zero.x + this.offset.x, zero.y + this.offset.y, vector.x, vector.y);
		}
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0004ADC0 File Offset: 0x00048FC0
	private void RaiseEvent(EasyButton.MessageName msg)
	{
		if (this.interaction == EasyButton.InteractionType.Event)
		{
			if (!this.useBroadcast)
			{
				switch (msg)
				{
				case EasyButton.MessageName.On_ButtonDown:
					if (EasyButton.On_ButtonDown != null)
					{
						EasyButton.On_ButtonDown(base.gameObject.name);
						return;
					}
					break;
				case EasyButton.MessageName.On_ButtonPress:
					if (EasyButton.On_ButtonPress != null)
					{
						EasyButton.On_ButtonPress(base.gameObject.name);
						return;
					}
					break;
				case EasyButton.MessageName.On_ButtonUp:
					if (EasyButton.On_ButtonUp != null)
					{
						EasyButton.On_ButtonUp(base.gameObject.name);
						return;
					}
					break;
				default:
					return;
				}
			}
			else
			{
				string text = msg.ToString();
				if (msg == EasyButton.MessageName.On_ButtonDown && this.downMethodName != "" && this.useSpecificalMethod)
				{
					text = this.downMethodName;
				}
				if (msg == EasyButton.MessageName.On_ButtonPress && this.pressMethodName != "" && this.useSpecificalMethod)
				{
					text = this.pressMethodName;
				}
				if (msg == EasyButton.MessageName.On_ButtonUp && this.upMethodName != "" && this.useSpecificalMethod)
				{
					text = this.upMethodName;
				}
				if (this.receiverGameObject != null)
				{
					switch (this.messageMode)
					{
					case EasyButton.Broadcast.SendMessage:
						this.receiverGameObject.SendMessage(text, base.name, 1);
						return;
					case EasyButton.Broadcast.SendMessageUpwards:
						this.receiverGameObject.SendMessageUpwards(text, base.name, 1);
						return;
					case EasyButton.Broadcast.BroadcastMessage:
						this.receiverGameObject.BroadcastMessage(text, base.name, 1);
						return;
					default:
						return;
					}
				}
				else
				{
					Debug.LogError("Button : " + base.gameObject.name + " : you must setup receiver gameobject");
				}
			}
		}
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x0004AF54 File Offset: 0x00049154
	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.IsInRect(VirtualScreen.GetRealRect(this.buttonRect), true) && this.enable && this.isActivated)
		{
			this.buttonFingerIndex = gesture.fingerIndex;
			this.currentTexture = this.activeTexture;
			this.currentColor = this.buttonActiveColor;
			this.buttonState = EasyButton.ButtonState.Down;
			this.frame = 0;
			this.RaiseEvent(EasyButton.MessageName.On_ButtonDown);
		}
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0004AFC0 File Offset: 0x000491C0
	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.fingerIndex == this.buttonFingerIndex || (this.isSwipeIn && this.buttonState == EasyButton.ButtonState.None))
		{
			if (gesture.IsInRect(VirtualScreen.GetRealRect(this.buttonRect), true) && this.enable && this.isActivated)
			{
				this.currentTexture = this.activeTexture;
				this.currentColor = this.buttonActiveColor;
				this.frame++;
				if ((this.buttonState == EasyButton.ButtonState.Down || this.buttonState == EasyButton.ButtonState.Press) && this.frame >= 2)
				{
					this.RaiseEvent(EasyButton.MessageName.On_ButtonPress);
					this.buttonState = EasyButton.ButtonState.Press;
				}
				if (this.buttonState == EasyButton.ButtonState.None)
				{
					this.buttonFingerIndex = gesture.fingerIndex;
					this.buttonState = EasyButton.ButtonState.Down;
					this.frame = 0;
					this.RaiseEvent(EasyButton.MessageName.On_ButtonDown);
					return;
				}
			}
			else
			{
				if ((this.isSwipeIn || !this.isSwipeIn) && !this.isSwipeOut && this.buttonState == EasyButton.ButtonState.Press)
				{
					this.buttonFingerIndex = -1;
					this.currentTexture = this.normalTexture;
					this.currentColor = this.buttonNormalColor;
					this.buttonState = EasyButton.ButtonState.None;
					return;
				}
				if (this.isSwipeOut && this.buttonState == EasyButton.ButtonState.Press)
				{
					this.RaiseEvent(EasyButton.MessageName.On_ButtonPress);
					this.buttonState = EasyButton.ButtonState.Press;
				}
			}
		}
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0004B100 File Offset: 0x00049300
	private void On_TouchUp(Gesture gesture)
	{
		if (gesture.fingerIndex == this.buttonFingerIndex)
		{
			if ((EasyTouch.IsRectUnderTouch(VirtualScreen.GetRealRect(this.buttonRect), true) || (this.isSwipeOut && this.buttonState == EasyButton.ButtonState.Press)) && this.enable && this.isActivated)
			{
				this.RaiseEvent(EasyButton.MessageName.On_ButtonUp);
			}
			this.buttonState = EasyButton.ButtonState.Up;
			this.buttonFingerIndex = -1;
			this.currentTexture = this.normalTexture;
			this.currentColor = this.buttonNormalColor;
		}
	}

	// Token: 0x0400088D RID: 2189
	public bool enable = true;

	// Token: 0x0400088E RID: 2190
	public bool isActivated = true;

	// Token: 0x0400088F RID: 2191
	public bool showDebugArea = true;

	// Token: 0x04000890 RID: 2192
	public bool isUseGuiLayout = true;

	// Token: 0x04000891 RID: 2193
	public EasyButton.ButtonState buttonState = EasyButton.ButtonState.None;

	// Token: 0x04000892 RID: 2194
	[SerializeField]
	private EasyButton.ButtonAnchor anchor = EasyButton.ButtonAnchor.LowerRight;

	// Token: 0x04000893 RID: 2195
	[SerializeField]
	private Vector2 offset = Vector2.zero;

	// Token: 0x04000894 RID: 2196
	[SerializeField]
	private Vector2 scale = Vector2.one;

	// Token: 0x04000895 RID: 2197
	public bool isSwipeIn;

	// Token: 0x04000896 RID: 2198
	public bool isSwipeOut;

	// Token: 0x04000897 RID: 2199
	public EasyButton.InteractionType interaction;

	// Token: 0x04000898 RID: 2200
	public bool useBroadcast;

	// Token: 0x04000899 RID: 2201
	public GameObject receiverGameObject;

	// Token: 0x0400089A RID: 2202
	public EasyButton.Broadcast messageMode;

	// Token: 0x0400089B RID: 2203
	public bool useSpecificalMethod;

	// Token: 0x0400089C RID: 2204
	public string downMethodName;

	// Token: 0x0400089D RID: 2205
	public string pressMethodName;

	// Token: 0x0400089E RID: 2206
	public string upMethodName;

	// Token: 0x0400089F RID: 2207
	public int guiDepth;

	// Token: 0x040008A0 RID: 2208
	[SerializeField]
	private Texture2D normalTexture;

	// Token: 0x040008A1 RID: 2209
	public Color buttonNormalColor = Color.white;

	// Token: 0x040008A2 RID: 2210
	[SerializeField]
	private Texture2D activeTexture;

	// Token: 0x040008A3 RID: 2211
	public Color buttonActiveColor = Color.white;

	// Token: 0x040008A4 RID: 2212
	public bool showInspectorProperties = true;

	// Token: 0x040008A5 RID: 2213
	public bool showInspectorPosition = true;

	// Token: 0x040008A6 RID: 2214
	public bool showInspectorEvent;

	// Token: 0x040008A7 RID: 2215
	public bool showInspectorTexture;

	// Token: 0x040008A8 RID: 2216
	private Rect buttonRect;

	// Token: 0x040008A9 RID: 2217
	private int buttonFingerIndex = -1;

	// Token: 0x040008AA RID: 2218
	private Texture2D currentTexture;

	// Token: 0x040008AB RID: 2219
	private Color currentColor;

	// Token: 0x040008AC RID: 2220
	private int frame;

	// Token: 0x02001248 RID: 4680
	// (Invoke) Token: 0x060078D8 RID: 30936
	public delegate void ButtonUpHandler(string buttonName);

	// Token: 0x02001249 RID: 4681
	// (Invoke) Token: 0x060078DC RID: 30940
	public delegate void ButtonPressHandler(string buttonName);

	// Token: 0x0200124A RID: 4682
	// (Invoke) Token: 0x060078E0 RID: 30944
	public delegate void ButtonDownHandler(string buttonName);

	// Token: 0x0200124B RID: 4683
	public enum ButtonAnchor
	{
		// Token: 0x0400654C RID: 25932
		UpperLeft,
		// Token: 0x0400654D RID: 25933
		UpperCenter,
		// Token: 0x0400654E RID: 25934
		UpperRight,
		// Token: 0x0400654F RID: 25935
		MiddleLeft,
		// Token: 0x04006550 RID: 25936
		MiddleCenter,
		// Token: 0x04006551 RID: 25937
		MiddleRight,
		// Token: 0x04006552 RID: 25938
		LowerLeft,
		// Token: 0x04006553 RID: 25939
		LowerCenter,
		// Token: 0x04006554 RID: 25940
		LowerRight
	}

	// Token: 0x0200124C RID: 4684
	public enum Broadcast
	{
		// Token: 0x04006556 RID: 25942
		SendMessage,
		// Token: 0x04006557 RID: 25943
		SendMessageUpwards,
		// Token: 0x04006558 RID: 25944
		BroadcastMessage
	}

	// Token: 0x0200124D RID: 4685
	public enum ButtonState
	{
		// Token: 0x0400655A RID: 25946
		Down,
		// Token: 0x0400655B RID: 25947
		Press,
		// Token: 0x0400655C RID: 25948
		Up,
		// Token: 0x0400655D RID: 25949
		None
	}

	// Token: 0x0200124E RID: 4686
	public enum InteractionType
	{
		// Token: 0x0400655F RID: 25951
		Event,
		// Token: 0x04006560 RID: 25952
		Include
	}

	// Token: 0x0200124F RID: 4687
	private enum MessageName
	{
		// Token: 0x04006562 RID: 25954
		On_ButtonDown,
		// Token: 0x04006563 RID: 25955
		On_ButtonPress,
		// Token: 0x04006564 RID: 25956
		On_ButtonUp
	}
}
