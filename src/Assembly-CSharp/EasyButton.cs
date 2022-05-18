using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
[ExecuteInEditMode]
public class EasyButton : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000D96 RID: 3478 RVA: 0x0009BC8C File Offset: 0x00099E8C
	// (remove) Token: 0x06000D97 RID: 3479 RVA: 0x0009BCC0 File Offset: 0x00099EC0
	public static event EasyButton.ButtonDownHandler On_ButtonDown;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000D98 RID: 3480 RVA: 0x0009BCF4 File Offset: 0x00099EF4
	// (remove) Token: 0x06000D99 RID: 3481 RVA: 0x0009BD28 File Offset: 0x00099F28
	public static event EasyButton.ButtonPressHandler On_ButtonPress;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000D9A RID: 3482 RVA: 0x0009BD5C File Offset: 0x00099F5C
	// (remove) Token: 0x06000D9B RID: 3483 RVA: 0x0009BD90 File Offset: 0x00099F90
	public static event EasyButton.ButtonUpHandler On_ButtonUp;

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0000F1D9 File Offset: 0x0000D3D9
	// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0000F1E1 File Offset: 0x0000D3E1
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

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
	// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0000F1FE File Offset: 0x0000D3FE
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

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0000F213 File Offset: 0x0000D413
	// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0000F21B File Offset: 0x0000D41B
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

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0000F230 File Offset: 0x0000D430
	// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0000F238 File Offset: 0x0000D438
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

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0000F267 File Offset: 0x0000D467
	// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0000F26F File Offset: 0x0000D46F
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

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0000F278 File Offset: 0x0000D478
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0009BDC4 File Offset: 0x00099FC4
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

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0009BDC4 File Offset: 0x00099FC4
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

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0000F2AD File Offset: 0x0000D4AD
	private void Start()
	{
		this.currentTexture = this.normalTexture;
		this.currentColor = this.buttonNormalColor;
		this.buttonState = EasyButton.ButtonState.None;
		VirtualScreen.ComputeVirtualScreen();
		this.ComputeButtonAnchor(this.anchor);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0009BE18 File Offset: 0x0009A018
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

	// Token: 0x06000DAB RID: 3499 RVA: 0x0000F2DF File Offset: 0x0000D4DF
	private void Update()
	{
		if (this.buttonState == EasyButton.ButtonState.Up)
		{
			this.buttonState = EasyButton.ButtonState.None;
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000042DD File Offset: 0x000024DD
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0009BF78 File Offset: 0x0009A178
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

	// Token: 0x06000DAE RID: 3502 RVA: 0x0009C198 File Offset: 0x0009A398
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

	// Token: 0x06000DAF RID: 3503 RVA: 0x0009C32C File Offset: 0x0009A52C
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

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0009C398 File Offset: 0x0009A598
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

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0009C4D8 File Offset: 0x0009A6D8
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

	// Token: 0x04000A89 RID: 2697
	public bool enable = true;

	// Token: 0x04000A8A RID: 2698
	public bool isActivated = true;

	// Token: 0x04000A8B RID: 2699
	public bool showDebugArea = true;

	// Token: 0x04000A8C RID: 2700
	public bool isUseGuiLayout = true;

	// Token: 0x04000A8D RID: 2701
	public EasyButton.ButtonState buttonState = EasyButton.ButtonState.None;

	// Token: 0x04000A8E RID: 2702
	[SerializeField]
	private EasyButton.ButtonAnchor anchor = EasyButton.ButtonAnchor.LowerRight;

	// Token: 0x04000A8F RID: 2703
	[SerializeField]
	private Vector2 offset = Vector2.zero;

	// Token: 0x04000A90 RID: 2704
	[SerializeField]
	private Vector2 scale = Vector2.one;

	// Token: 0x04000A91 RID: 2705
	public bool isSwipeIn;

	// Token: 0x04000A92 RID: 2706
	public bool isSwipeOut;

	// Token: 0x04000A93 RID: 2707
	public EasyButton.InteractionType interaction;

	// Token: 0x04000A94 RID: 2708
	public bool useBroadcast;

	// Token: 0x04000A95 RID: 2709
	public GameObject receiverGameObject;

	// Token: 0x04000A96 RID: 2710
	public EasyButton.Broadcast messageMode;

	// Token: 0x04000A97 RID: 2711
	public bool useSpecificalMethod;

	// Token: 0x04000A98 RID: 2712
	public string downMethodName;

	// Token: 0x04000A99 RID: 2713
	public string pressMethodName;

	// Token: 0x04000A9A RID: 2714
	public string upMethodName;

	// Token: 0x04000A9B RID: 2715
	public int guiDepth;

	// Token: 0x04000A9C RID: 2716
	[SerializeField]
	private Texture2D normalTexture;

	// Token: 0x04000A9D RID: 2717
	public Color buttonNormalColor = Color.white;

	// Token: 0x04000A9E RID: 2718
	[SerializeField]
	private Texture2D activeTexture;

	// Token: 0x04000A9F RID: 2719
	public Color buttonActiveColor = Color.white;

	// Token: 0x04000AA0 RID: 2720
	public bool showInspectorProperties = true;

	// Token: 0x04000AA1 RID: 2721
	public bool showInspectorPosition = true;

	// Token: 0x04000AA2 RID: 2722
	public bool showInspectorEvent;

	// Token: 0x04000AA3 RID: 2723
	public bool showInspectorTexture;

	// Token: 0x04000AA4 RID: 2724
	private Rect buttonRect;

	// Token: 0x04000AA5 RID: 2725
	private int buttonFingerIndex = -1;

	// Token: 0x04000AA6 RID: 2726
	private Texture2D currentTexture;

	// Token: 0x04000AA7 RID: 2727
	private Color currentColor;

	// Token: 0x04000AA8 RID: 2728
	private int frame;

	// Token: 0x02000194 RID: 404
	// (Invoke) Token: 0x06000DB4 RID: 3508
	public delegate void ButtonUpHandler(string buttonName);

	// Token: 0x02000195 RID: 405
	// (Invoke) Token: 0x06000DB8 RID: 3512
	public delegate void ButtonPressHandler(string buttonName);

	// Token: 0x02000196 RID: 406
	// (Invoke) Token: 0x06000DBC RID: 3516
	public delegate void ButtonDownHandler(string buttonName);

	// Token: 0x02000197 RID: 407
	public enum ButtonAnchor
	{
		// Token: 0x04000AAA RID: 2730
		UpperLeft,
		// Token: 0x04000AAB RID: 2731
		UpperCenter,
		// Token: 0x04000AAC RID: 2732
		UpperRight,
		// Token: 0x04000AAD RID: 2733
		MiddleLeft,
		// Token: 0x04000AAE RID: 2734
		MiddleCenter,
		// Token: 0x04000AAF RID: 2735
		MiddleRight,
		// Token: 0x04000AB0 RID: 2736
		LowerLeft,
		// Token: 0x04000AB1 RID: 2737
		LowerCenter,
		// Token: 0x04000AB2 RID: 2738
		LowerRight
	}

	// Token: 0x02000198 RID: 408
	public enum Broadcast
	{
		// Token: 0x04000AB4 RID: 2740
		SendMessage,
		// Token: 0x04000AB5 RID: 2741
		SendMessageUpwards,
		// Token: 0x04000AB6 RID: 2742
		BroadcastMessage
	}

	// Token: 0x02000199 RID: 409
	public enum ButtonState
	{
		// Token: 0x04000AB8 RID: 2744
		Down,
		// Token: 0x04000AB9 RID: 2745
		Press,
		// Token: 0x04000ABA RID: 2746
		Up,
		// Token: 0x04000ABB RID: 2747
		None
	}

	// Token: 0x0200019A RID: 410
	public enum InteractionType
	{
		// Token: 0x04000ABD RID: 2749
		Event,
		// Token: 0x04000ABE RID: 2750
		Include
	}

	// Token: 0x0200019B RID: 411
	private enum MessageName
	{
		// Token: 0x04000AC0 RID: 2752
		On_ButtonDown,
		// Token: 0x04000AC1 RID: 2753
		On_ButtonPress,
		// Token: 0x04000AC2 RID: 2754
		On_ButtonUp
	}
}
