using UnityEngine;

[ExecuteInEditMode]
public class EasyButton : MonoBehaviour
{
	public delegate void ButtonUpHandler(string buttonName);

	public delegate void ButtonPressHandler(string buttonName);

	public delegate void ButtonDownHandler(string buttonName);

	public enum ButtonAnchor
	{
		UpperLeft,
		UpperCenter,
		UpperRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		LowerLeft,
		LowerCenter,
		LowerRight
	}

	public enum Broadcast
	{
		SendMessage,
		SendMessageUpwards,
		BroadcastMessage
	}

	public enum ButtonState
	{
		Down,
		Press,
		Up,
		None
	}

	public enum InteractionType
	{
		Event,
		Include
	}

	private enum MessageName
	{
		On_ButtonDown,
		On_ButtonPress,
		On_ButtonUp
	}

	public bool enable = true;

	public bool isActivated = true;

	public bool showDebugArea = true;

	public bool isUseGuiLayout = true;

	public ButtonState buttonState = ButtonState.None;

	[SerializeField]
	private ButtonAnchor anchor = ButtonAnchor.LowerRight;

	[SerializeField]
	private Vector2 offset = Vector2.zero;

	[SerializeField]
	private Vector2 scale = Vector2.one;

	public bool isSwipeIn;

	public bool isSwipeOut;

	public InteractionType interaction;

	public bool useBroadcast;

	public GameObject receiverGameObject;

	public Broadcast messageMode;

	public bool useSpecificalMethod;

	public string downMethodName;

	public string pressMethodName;

	public string upMethodName;

	public int guiDepth;

	[SerializeField]
	private Texture2D normalTexture;

	public Color buttonNormalColor = Color.white;

	[SerializeField]
	private Texture2D activeTexture;

	public Color buttonActiveColor = Color.white;

	public bool showInspectorProperties = true;

	public bool showInspectorPosition = true;

	public bool showInspectorEvent;

	public bool showInspectorTexture;

	private Rect buttonRect;

	private int buttonFingerIndex = -1;

	private Texture2D currentTexture;

	private Color currentColor;

	private int frame;

	public ButtonAnchor Anchor
	{
		get
		{
			return anchor;
		}
		set
		{
			anchor = value;
			ComputeButtonAnchor(anchor);
		}
	}

	public Vector2 Offset
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return offset;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			offset = value;
			ComputeButtonAnchor(anchor);
		}
	}

	public Vector2 Scale
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return scale;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			scale = value;
			ComputeButtonAnchor(anchor);
		}
	}

	public Texture2D NormalTexture
	{
		get
		{
			return normalTexture;
		}
		set
		{
			normalTexture = value;
			if ((Object)(object)normalTexture != (Object)null)
			{
				ComputeButtonAnchor(anchor);
				currentTexture = normalTexture;
			}
		}
	}

	public Texture2D ActiveTexture
	{
		get
		{
			return activeTexture;
		}
		set
		{
			activeTexture = value;
		}
	}

	public static event ButtonDownHandler On_ButtonDown;

	public static event ButtonPressHandler On_ButtonPress;

	public static event ButtonUpHandler On_ButtonUp;

	private void OnEnable()
	{
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_TouchUp += On_TouchUp;
	}

	private void OnDisable()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_TouchUp -= On_TouchUp;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(buttonRect);
		}
	}

	private void OnDestroy()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_TouchUp -= On_TouchUp;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(buttonRect);
		}
	}

	private void Start()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		currentTexture = normalTexture;
		currentColor = buttonNormalColor;
		buttonState = ButtonState.None;
		VirtualScreen.ComputeVirtualScreen();
		ComputeButtonAnchor(anchor);
	}

	private void OnGUI()
	{
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			GUI.depth = guiDepth;
			((MonoBehaviour)this).useGUILayout = isUseGuiLayout;
			VirtualScreen.ComputeVirtualScreen();
			VirtualScreen.SetGuiScaleMatrix();
			if (!((Object)(object)normalTexture != (Object)null) || !((Object)(object)activeTexture != (Object)null))
			{
				return;
			}
			ComputeButtonAnchor(anchor);
			if (!((Object)(object)normalTexture != (Object)null))
			{
				return;
			}
			if (Application.isEditor && !Application.isPlaying)
			{
				currentTexture = normalTexture;
			}
			if (showDebugArea && Application.isEditor)
			{
				GUI.Box(buttonRect, "");
			}
			if (!((Object)(object)currentTexture != (Object)null))
			{
				return;
			}
			if (isActivated)
			{
				GUI.color = currentColor;
				if (Application.isPlaying)
				{
					EasyTouch.RemoveReservedArea(buttonRect);
					EasyTouch.AddReservedArea(buttonRect);
				}
			}
			else
			{
				GUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.2f);
				if (Application.isPlaying)
				{
					EasyTouch.RemoveReservedArea(buttonRect);
				}
			}
			GUI.DrawTexture(buttonRect, (Texture)(object)currentTexture);
			GUI.color = Color.white;
		}
		else
		{
			EasyTouch.RemoveReservedArea(buttonRect);
		}
	}

	private void Update()
	{
		if (buttonState == ButtonState.Up)
		{
			buttonState = ButtonState.None;
		}
	}

	private void OnDrawGizmos()
	{
	}

	private void ComputeButtonAnchor(ButtonAnchor anchor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)normalTexture != (Object)null)
		{
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Texture)normalTexture).width * scale.x, (float)((Texture)normalTexture).height * scale.y);
			Vector2 zero = Vector2.zero;
			switch (anchor)
			{
			case ButtonAnchor.UpperLeft:
				((Vector2)(ref zero))._002Ector(0f, 0f);
				break;
			case ButtonAnchor.UpperCenter:
				((Vector2)(ref zero))._002Ector(VirtualScreen.width / 2f - val.x / 2f, offset.y);
				break;
			case ButtonAnchor.UpperRight:
				((Vector2)(ref zero))._002Ector(VirtualScreen.width - val.x, 0f);
				break;
			case ButtonAnchor.MiddleLeft:
				((Vector2)(ref zero))._002Ector(0f, VirtualScreen.height / 2f - val.y / 2f);
				break;
			case ButtonAnchor.MiddleCenter:
				((Vector2)(ref zero))._002Ector(VirtualScreen.width / 2f - val.x / 2f, VirtualScreen.height / 2f - val.y / 2f);
				break;
			case ButtonAnchor.MiddleRight:
				((Vector2)(ref zero))._002Ector(VirtualScreen.width - val.x, VirtualScreen.height / 2f - val.y / 2f);
				break;
			case ButtonAnchor.LowerLeft:
				((Vector2)(ref zero))._002Ector(0f, VirtualScreen.height - val.y);
				break;
			case ButtonAnchor.LowerCenter:
				((Vector2)(ref zero))._002Ector(VirtualScreen.width / 2f - val.x / 2f, VirtualScreen.height - val.y);
				break;
			case ButtonAnchor.LowerRight:
				((Vector2)(ref zero))._002Ector(VirtualScreen.width - val.x, VirtualScreen.height - val.y);
				break;
			}
			buttonRect = new Rect(zero.x + offset.x, zero.y + offset.y, val.x, val.y);
		}
	}

	private void RaiseEvent(MessageName msg)
	{
		if (interaction != 0)
		{
			return;
		}
		if (!useBroadcast)
		{
			switch (msg)
			{
			case MessageName.On_ButtonDown:
				if (EasyButton.On_ButtonDown != null)
				{
					EasyButton.On_ButtonDown(((Object)((Component)this).gameObject).name);
				}
				break;
			case MessageName.On_ButtonUp:
				if (EasyButton.On_ButtonUp != null)
				{
					EasyButton.On_ButtonUp(((Object)((Component)this).gameObject).name);
				}
				break;
			case MessageName.On_ButtonPress:
				if (EasyButton.On_ButtonPress != null)
				{
					EasyButton.On_ButtonPress(((Object)((Component)this).gameObject).name);
				}
				break;
			}
			return;
		}
		string text = msg.ToString();
		if (msg == MessageName.On_ButtonDown && downMethodName != "" && useSpecificalMethod)
		{
			text = downMethodName;
		}
		if (msg == MessageName.On_ButtonPress && pressMethodName != "" && useSpecificalMethod)
		{
			text = pressMethodName;
		}
		if (msg == MessageName.On_ButtonUp && upMethodName != "" && useSpecificalMethod)
		{
			text = upMethodName;
		}
		if ((Object)(object)receiverGameObject != (Object)null)
		{
			switch (messageMode)
			{
			case Broadcast.BroadcastMessage:
				receiverGameObject.BroadcastMessage(text, (object)((Object)this).name, (SendMessageOptions)1);
				break;
			case Broadcast.SendMessage:
				receiverGameObject.SendMessage(text, (object)((Object)this).name, (SendMessageOptions)1);
				break;
			case Broadcast.SendMessageUpwards:
				receiverGameObject.SendMessageUpwards(text, (object)((Object)this).name, (SendMessageOptions)1);
				break;
			}
		}
		else
		{
			Debug.LogError((object)("Button : " + ((Object)((Component)this).gameObject).name + " : you must setup receiver gameobject"));
		}
	}

	private void On_TouchStart(Gesture gesture)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (gesture.IsInRect(VirtualScreen.GetRealRect(buttonRect), guiRect: true) && enable && isActivated)
		{
			buttonFingerIndex = gesture.fingerIndex;
			currentTexture = activeTexture;
			currentColor = buttonActiveColor;
			buttonState = ButtonState.Down;
			frame = 0;
			RaiseEvent(MessageName.On_ButtonDown);
		}
	}

	private void On_TouchDown(Gesture gesture)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (gesture.fingerIndex != buttonFingerIndex && (!isSwipeIn || buttonState != ButtonState.None))
		{
			return;
		}
		if (gesture.IsInRect(VirtualScreen.GetRealRect(buttonRect), guiRect: true) && enable && isActivated)
		{
			currentTexture = activeTexture;
			currentColor = buttonActiveColor;
			frame++;
			if ((buttonState == ButtonState.Down || buttonState == ButtonState.Press) && frame >= 2)
			{
				RaiseEvent(MessageName.On_ButtonPress);
				buttonState = ButtonState.Press;
			}
			if (buttonState == ButtonState.None)
			{
				buttonFingerIndex = gesture.fingerIndex;
				buttonState = ButtonState.Down;
				frame = 0;
				RaiseEvent(MessageName.On_ButtonDown);
			}
		}
		else if ((isSwipeIn || !isSwipeIn) && !isSwipeOut && buttonState == ButtonState.Press)
		{
			buttonFingerIndex = -1;
			currentTexture = normalTexture;
			currentColor = buttonNormalColor;
			buttonState = ButtonState.None;
		}
		else if (isSwipeOut && buttonState == ButtonState.Press)
		{
			RaiseEvent(MessageName.On_ButtonPress);
			buttonState = ButtonState.Press;
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (gesture.fingerIndex == buttonFingerIndex)
		{
			if ((EasyTouch.IsRectUnderTouch(VirtualScreen.GetRealRect(buttonRect), guiRect: true) || (isSwipeOut && buttonState == ButtonState.Press)) && enable && isActivated)
			{
				RaiseEvent(MessageName.On_ButtonUp);
			}
			buttonState = ButtonState.Up;
			buttonFingerIndex = -1;
			currentTexture = normalTexture;
			currentColor = buttonNormalColor;
		}
	}
}
