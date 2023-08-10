using UnityEngine;

[ExecuteInEditMode]
public class EasyJoystick : MonoBehaviour
{
	public delegate void JoystickMoveStartHandler(MovingJoystick move);

	public delegate void JoystickMoveHandler(MovingJoystick move);

	public delegate void JoystickMoveEndHandler(MovingJoystick move);

	public delegate void JoystickTouchStartHandler(MovingJoystick move);

	public delegate void JoystickTapHandler(MovingJoystick move);

	public delegate void JoystickDoubleTapHandler(MovingJoystick move);

	public delegate void JoystickTouchUpHandler(MovingJoystick move);

	public enum JoystickAnchor
	{
		None,
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

	public enum PropertiesInfluenced
	{
		Rotate,
		RotateLocal,
		Translate,
		TranslateLocal,
		Scale
	}

	public enum AxisInfluenced
	{
		X,
		Y,
		Z,
		XYZ
	}

	public enum DynamicArea
	{
		FullScreen,
		Left,
		Right,
		Top,
		Bottom,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	public enum InteractionType
	{
		Direct,
		Include,
		EventNotification,
		DirectAndEvent
	}

	public enum Broadcast
	{
		SendMessage,
		SendMessageUpwards,
		BroadcastMessage
	}

	private enum MessageName
	{
		On_JoystickMoveStart,
		On_JoystickTouchStart,
		On_JoystickTouchUp,
		On_JoystickMove,
		On_JoystickMoveEnd,
		On_JoystickTap,
		On_JoystickDoubleTap
	}

	private Vector2 joystickAxis;

	private Vector2 joystickTouch;

	private Vector2 joystickValue;

	public bool enable = true;

	public bool isActivated = true;

	public bool showDebugRadius;

	public bool useFixedUpdate;

	public bool isUseGuiLayout = true;

	[SerializeField]
	private bool dynamicJoystick;

	public DynamicArea area;

	[SerializeField]
	private JoystickAnchor joyAnchor = JoystickAnchor.LowerLeft;

	[SerializeField]
	private Vector2 joystickPositionOffset = Vector2.zero;

	[SerializeField]
	private float zoneRadius = 100f;

	[SerializeField]
	private float touchSize = 30f;

	public float deadZone = 20f;

	[SerializeField]
	private bool restrictArea;

	public bool resetFingerExit;

	[SerializeField]
	private InteractionType interaction;

	public bool useBroadcast;

	public Broadcast messageMode;

	public GameObject receiverGameObject;

	public Vector2 speed;

	public bool enableXaxis = true;

	[SerializeField]
	private Transform xAxisTransform;

	public CharacterController xAxisCharacterController;

	public float xAxisGravity;

	[SerializeField]
	private PropertiesInfluenced xTI;

	public AxisInfluenced xAI;

	public bool inverseXAxis;

	public bool enableXClamp;

	public float clampXMax;

	public float clampXMin;

	public bool enableXAutoStab;

	[SerializeField]
	private float thresholdX = 0.01f;

	[SerializeField]
	private float stabSpeedX = 20f;

	public bool enableYaxis = true;

	[SerializeField]
	private Transform yAxisTransform;

	public CharacterController yAxisCharacterController;

	public float yAxisGravity;

	[SerializeField]
	private PropertiesInfluenced yTI;

	public AxisInfluenced yAI;

	public bool inverseYAxis;

	public bool enableYClamp;

	public float clampYMax;

	public float clampYMin;

	public bool enableYAutoStab;

	[SerializeField]
	private float thresholdY = 0.01f;

	[SerializeField]
	private float stabSpeedY = 20f;

	public bool enableSmoothing;

	[SerializeField]
	public Vector2 smoothing = new Vector2(2f, 2f);

	public bool enableInertia;

	[SerializeField]
	public Vector2 inertia = new Vector2(100f, 100f);

	public int guiDepth;

	public bool showZone = true;

	public bool showTouch = true;

	public bool showDeadZone = true;

	public Texture areaTexture;

	public Color areaColor = Color.white;

	public Texture touchTexture;

	public Color touchColor = Color.white;

	public Texture deadTexture;

	public bool showProperties = true;

	public bool showInteraction;

	public bool showAppearance;

	public bool showPosition = true;

	private Vector2 joystickCenter;

	private Rect areaRect;

	private Rect deadRect;

	private Vector2 anchorPosition = Vector2.zero;

	private bool virtualJoystick = true;

	private int joystickIndex = -1;

	private float touchSizeCoef;

	private bool sendEnd = true;

	private float startXLocalAngle;

	private float startYLocalAngle;

	public Vector2 JoystickAxis => joystickAxis;

	public Vector2 JoystickTouch
	{
		get
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(joystickTouch.x / zoneRadius, joystickTouch.y / zoneRadius);
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			float num = Mathf.Clamp(value.x, -1f, 1f) * zoneRadius;
			float num2 = Mathf.Clamp(value.y, -1f, 1f) * zoneRadius;
			joystickTouch = new Vector2(num, num2);
		}
	}

	public Vector2 JoystickValue => joystickValue;

	public bool DynamicJoystick
	{
		get
		{
			return dynamicJoystick;
		}
		set
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			if (!Application.isPlaying)
			{
				joystickIndex = -1;
				dynamicJoystick = value;
				if (dynamicJoystick)
				{
					virtualJoystick = false;
					return;
				}
				virtualJoystick = true;
				joystickCenter = joystickPositionOffset;
			}
		}
	}

	public JoystickAnchor JoyAnchor
	{
		get
		{
			return joyAnchor;
		}
		set
		{
			joyAnchor = value;
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public Vector2 JoystickPositionOffset
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return joystickPositionOffset;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			joystickPositionOffset = value;
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public float ZoneRadius
	{
		get
		{
			return zoneRadius;
		}
		set
		{
			zoneRadius = value;
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public float TouchSize
	{
		get
		{
			return touchSize;
		}
		set
		{
			touchSize = value;
			if (touchSize > zoneRadius / 2f && restrictArea)
			{
				touchSize = zoneRadius / 2f;
			}
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public bool RestrictArea
	{
		get
		{
			return restrictArea;
		}
		set
		{
			restrictArea = value;
			if (restrictArea)
			{
				touchSizeCoef = touchSize;
			}
			else
			{
				touchSizeCoef = 0f;
			}
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public InteractionType Interaction
	{
		get
		{
			return interaction;
		}
		set
		{
			interaction = value;
			if (interaction == InteractionType.Direct || interaction == InteractionType.Include)
			{
				useBroadcast = false;
			}
		}
	}

	public Transform XAxisTransform
	{
		get
		{
			return xAxisTransform;
		}
		set
		{
			xAxisTransform = value;
			if ((Object)(object)xAxisTransform != (Object)null)
			{
				xAxisCharacterController = ((Component)xAxisTransform).GetComponent<CharacterController>();
				return;
			}
			xAxisCharacterController = null;
			xAxisGravity = 0f;
		}
	}

	public PropertiesInfluenced XTI
	{
		get
		{
			return xTI;
		}
		set
		{
			xTI = value;
			if (xTI != PropertiesInfluenced.RotateLocal)
			{
				enableXAutoStab = false;
				enableXClamp = false;
			}
		}
	}

	public float ThresholdX
	{
		get
		{
			return thresholdX;
		}
		set
		{
			if (value <= 0f)
			{
				thresholdX = value * -1f;
			}
			else
			{
				thresholdX = value;
			}
		}
	}

	public float StabSpeedX
	{
		get
		{
			return stabSpeedX;
		}
		set
		{
			if (value <= 0f)
			{
				stabSpeedX = value * -1f;
			}
			else
			{
				stabSpeedX = value;
			}
		}
	}

	public Transform YAxisTransform
	{
		get
		{
			return yAxisTransform;
		}
		set
		{
			yAxisTransform = value;
			if ((Object)(object)yAxisTransform != (Object)null)
			{
				yAxisCharacterController = ((Component)yAxisTransform).GetComponent<CharacterController>();
				return;
			}
			yAxisCharacterController = null;
			yAxisGravity = 0f;
		}
	}

	public PropertiesInfluenced YTI
	{
		get
		{
			return yTI;
		}
		set
		{
			yTI = value;
			if (yTI != PropertiesInfluenced.RotateLocal)
			{
				enableYAutoStab = false;
				enableYClamp = false;
			}
		}
	}

	public float ThresholdY
	{
		get
		{
			return thresholdY;
		}
		set
		{
			if (value <= 0f)
			{
				thresholdY = value * -1f;
			}
			else
			{
				thresholdY = value;
			}
		}
	}

	public float StabSpeedY
	{
		get
		{
			return stabSpeedY;
		}
		set
		{
			if (value <= 0f)
			{
				stabSpeedY = value * -1f;
			}
			else
			{
				stabSpeedY = value;
			}
		}
	}

	public Vector2 Smoothing
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return smoothing;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			smoothing = value;
			if (smoothing.x < 0f)
			{
				smoothing.x = 0f;
			}
			if (smoothing.y < 0f)
			{
				smoothing.y = 0f;
			}
		}
	}

	public Vector2 Inertia
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return inertia;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			inertia = value;
			if (inertia.x <= 0f)
			{
				inertia.x = 1f;
			}
			if (inertia.y <= 0f)
			{
				inertia.y = 1f;
			}
		}
	}

	public static event JoystickMoveStartHandler On_JoystickMoveStart;

	public static event JoystickMoveHandler On_JoystickMove;

	public static event JoystickMoveEndHandler On_JoystickMoveEnd;

	public static event JoystickTouchStartHandler On_JoystickTouchStart;

	public static event JoystickTapHandler On_JoystickTap;

	public static event JoystickDoubleTapHandler On_JoystickDoubleTap;

	public static event JoystickTouchUpHandler On_JoystickTouchUp;

	private void OnEnable()
	{
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchUp += On_TouchUp;
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_SimpleTap += On_SimpleTap;
		EasyTouch.On_DoubleTap += On_DoubleTap;
	}

	private void OnDisable()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchUp -= On_TouchUp;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_SimpleTap -= On_SimpleTap;
		EasyTouch.On_DoubleTap -= On_DoubleTap;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(areaRect);
		}
	}

	private void OnDestroy()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchUp -= On_TouchUp;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_SimpleTap -= On_SimpleTap;
		EasyTouch.On_DoubleTap -= On_DoubleTap;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(areaRect);
		}
	}

	private void Start()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (!dynamicJoystick)
		{
			joystickCenter = joystickPositionOffset;
			ComputeJoystickAnchor(joyAnchor);
			virtualJoystick = true;
		}
		else
		{
			virtualJoystick = false;
		}
		VirtualScreen.ComputeVirtualScreen();
		startXLocalAngle = GetStartAutoStabAngle(xAxisTransform, xAI);
		startYLocalAngle = GetStartAutoStabAngle(yAxisTransform, yAI);
	}

	private void Update()
	{
		if (!useFixedUpdate && enable)
		{
			UpdateJoystick();
		}
	}

	private void FixedUpdate()
	{
		if (useFixedUpdate && enable)
		{
			UpdateJoystick();
		}
	}

	private void UpdateJoystick()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		if (!Application.isPlaying || !isActivated)
		{
			return;
		}
		if (joystickIndex == -1 || (joystickAxis == Vector2.zero && joystickIndex > -1))
		{
			if (enableXAutoStab)
			{
				DoAutoStabilisation(xAxisTransform, xAI, thresholdX, stabSpeedX, startXLocalAngle);
			}
			if (enableYAutoStab)
			{
				DoAutoStabilisation(yAxisTransform, yAI, thresholdY, stabSpeedY, startYLocalAngle);
			}
		}
		if (!dynamicJoystick)
		{
			joystickCenter = joystickPositionOffset;
		}
		if (joystickIndex == -1)
		{
			if (!enableSmoothing)
			{
				joystickTouch = Vector2.zero;
			}
			else if ((double)((Vector2)(ref joystickTouch)).sqrMagnitude > 0.0001)
			{
				joystickTouch = new Vector2(joystickTouch.x - joystickTouch.x * smoothing.x * Time.deltaTime, joystickTouch.y - joystickTouch.y * smoothing.y * Time.deltaTime);
			}
			else
			{
				joystickTouch = Vector2.zero;
			}
		}
		Vector2 val = new Vector2(joystickAxis.x, joystickAxis.y);
		float num = ComputeDeadZone();
		joystickAxis = new Vector2(joystickTouch.x * num, joystickTouch.y * num);
		if (inverseXAxis)
		{
			joystickAxis.x *= -1f;
		}
		if (inverseYAxis)
		{
			joystickAxis.y *= -1f;
		}
		Vector2 val2 = default(Vector2);
		((Vector2)(ref val2))._002Ector(speed.x * joystickAxis.x, speed.y * joystickAxis.y);
		if (enableInertia)
		{
			Vector2 val3 = val2 - joystickValue;
			val3.x /= inertia.x;
			val3.y /= inertia.y;
			joystickValue += val3;
		}
		else
		{
			joystickValue = val2;
		}
		if (val == Vector2.zero && joystickAxis != Vector2.zero && interaction != 0 && interaction != InteractionType.Include)
		{
			CreateEvent(MessageName.On_JoystickMoveStart);
		}
		UpdateGravity();
		if (joystickAxis != Vector2.zero)
		{
			sendEnd = false;
			switch (interaction)
			{
			case InteractionType.Direct:
				UpdateDirect();
				break;
			case InteractionType.EventNotification:
				CreateEvent(MessageName.On_JoystickMove);
				break;
			case InteractionType.DirectAndEvent:
				UpdateDirect();
				CreateEvent(MessageName.On_JoystickMove);
				break;
			case InteractionType.Include:
				break;
			}
		}
		else if (!sendEnd)
		{
			CreateEvent(MessageName.On_JoystickMoveEnd);
			sendEnd = true;
		}
	}

	private void OnGUI()
	{
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			GUI.depth = guiDepth;
			((MonoBehaviour)this).useGUILayout = isUseGuiLayout;
			if (dynamicJoystick && Application.isEditor && !Application.isPlaying)
			{
				switch (area)
				{
				case DynamicArea.Bottom:
					ComputeJoystickAnchor(JoystickAnchor.LowerCenter);
					break;
				case DynamicArea.BottomLeft:
					ComputeJoystickAnchor(JoystickAnchor.LowerLeft);
					break;
				case DynamicArea.BottomRight:
					ComputeJoystickAnchor(JoystickAnchor.LowerRight);
					break;
				case DynamicArea.FullScreen:
					ComputeJoystickAnchor(JoystickAnchor.MiddleCenter);
					break;
				case DynamicArea.Left:
					ComputeJoystickAnchor(JoystickAnchor.MiddleLeft);
					break;
				case DynamicArea.Right:
					ComputeJoystickAnchor(JoystickAnchor.MiddleRight);
					break;
				case DynamicArea.Top:
					ComputeJoystickAnchor(JoystickAnchor.UpperCenter);
					break;
				case DynamicArea.TopLeft:
					ComputeJoystickAnchor(JoystickAnchor.UpperLeft);
					break;
				case DynamicArea.TopRight:
					ComputeJoystickAnchor(JoystickAnchor.UpperRight);
					break;
				}
			}
			if (Application.isEditor && !Application.isPlaying)
			{
				VirtualScreen.ComputeVirtualScreen();
				ComputeJoystickAnchor(joyAnchor);
			}
			VirtualScreen.SetGuiScaleMatrix();
			if ((showZone && (Object)(object)areaTexture != (Object)null && !dynamicJoystick) || (showZone && dynamicJoystick && virtualJoystick && (Object)(object)areaTexture != (Object)null) || (dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (isActivated)
				{
					GUI.color = areaColor;
					if (Application.isPlaying && !dynamicJoystick)
					{
						EasyTouch.RemoveReservedArea(areaRect);
						EasyTouch.AddReservedArea(areaRect);
					}
				}
				else
				{
					GUI.color = new Color(areaColor.r, areaColor.g, areaColor.b, 0.2f);
					if (Application.isPlaying && !dynamicJoystick)
					{
						EasyTouch.RemoveReservedArea(areaRect);
					}
				}
				if (showDebugRadius && Application.isEditor)
				{
					GUI.Box(areaRect, "");
				}
				GUI.DrawTexture(areaRect, areaTexture, (ScaleMode)0, true);
			}
			if ((showTouch && (Object)(object)touchTexture != (Object)null && !dynamicJoystick) || (showTouch && dynamicJoystick && virtualJoystick && (Object)(object)touchTexture != (Object)null) || (dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (isActivated)
				{
					GUI.color = touchColor;
				}
				else
				{
					GUI.color = new Color(touchColor.r, touchColor.g, touchColor.b, 0.2f);
				}
				GUI.DrawTexture(new Rect(anchorPosition.x + joystickCenter.x + (joystickTouch.x - touchSize), anchorPosition.y + joystickCenter.y - (joystickTouch.y + touchSize), touchSize * 2f, touchSize * 2f), touchTexture, (ScaleMode)2, true);
			}
			if ((showDeadZone && (Object)(object)deadTexture != (Object)null && !dynamicJoystick) || (showDeadZone && dynamicJoystick && virtualJoystick && (Object)(object)deadTexture != (Object)null) || (dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				GUI.DrawTexture(deadRect, deadTexture, (ScaleMode)2, true);
			}
			GUI.color = Color.white;
		}
		else
		{
			EasyTouch.RemoveReservedArea(areaRect);
		}
	}

	private void OnDrawGizmos()
	{
	}

	private void CreateEvent(MessageName message)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		MovingJoystick movingJoystick = new MovingJoystick();
		movingJoystick.joystickName = ((Object)((Component)this).gameObject).name;
		movingJoystick.joystickAxis = joystickAxis;
		movingJoystick.joystickValue = joystickValue;
		movingJoystick.joystick = this;
		if (!useBroadcast)
		{
			switch (message)
			{
			case MessageName.On_JoystickMoveStart:
				if (EasyJoystick.On_JoystickMoveStart != null)
				{
					EasyJoystick.On_JoystickMoveStart(movingJoystick);
				}
				break;
			case MessageName.On_JoystickMove:
				if (EasyJoystick.On_JoystickMove != null)
				{
					EasyJoystick.On_JoystickMove(movingJoystick);
				}
				break;
			case MessageName.On_JoystickMoveEnd:
				if (EasyJoystick.On_JoystickMoveEnd != null)
				{
					EasyJoystick.On_JoystickMoveEnd(movingJoystick);
				}
				break;
			case MessageName.On_JoystickTouchStart:
				if (EasyJoystick.On_JoystickTouchStart != null)
				{
					EasyJoystick.On_JoystickTouchStart(movingJoystick);
				}
				break;
			case MessageName.On_JoystickTap:
				if (EasyJoystick.On_JoystickTap != null)
				{
					EasyJoystick.On_JoystickTap(movingJoystick);
				}
				break;
			case MessageName.On_JoystickDoubleTap:
				if (EasyJoystick.On_JoystickDoubleTap != null)
				{
					EasyJoystick.On_JoystickDoubleTap(movingJoystick);
				}
				break;
			case MessageName.On_JoystickTouchUp:
				if (EasyJoystick.On_JoystickTouchUp != null)
				{
					EasyJoystick.On_JoystickTouchUp(movingJoystick);
				}
				break;
			}
		}
		else
		{
			if (!useBroadcast)
			{
				return;
			}
			if ((Object)(object)receiverGameObject != (Object)null)
			{
				switch (messageMode)
				{
				case Broadcast.BroadcastMessage:
					receiverGameObject.BroadcastMessage(message.ToString(), (object)movingJoystick, (SendMessageOptions)1);
					break;
				case Broadcast.SendMessage:
					receiverGameObject.SendMessage(message.ToString(), (object)movingJoystick, (SendMessageOptions)1);
					break;
				case Broadcast.SendMessageUpwards:
					receiverGameObject.SendMessageUpwards(message.ToString(), (object)movingJoystick, (SendMessageOptions)1);
					break;
				}
			}
			else
			{
				Debug.LogError((object)("Joystick : " + ((Object)((Component)this).gameObject).name + " : you must setup receiver gameobject"));
			}
		}
	}

	private void UpdateDirect()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)xAxisTransform != (Object)null)
		{
			Vector3 influencedAxis = GetInfluencedAxis(xAI);
			DoActionDirect(xAxisTransform, xTI, influencedAxis, joystickValue.x, xAxisCharacterController);
			if (enableXClamp && xTI == PropertiesInfluenced.RotateLocal)
			{
				DoAngleLimitation(xAxisTransform, xAI, clampXMin, clampXMax, startXLocalAngle);
			}
		}
		if ((Object)(object)YAxisTransform != (Object)null)
		{
			Vector3 influencedAxis2 = GetInfluencedAxis(yAI);
			DoActionDirect(yAxisTransform, yTI, influencedAxis2, joystickValue.y, yAxisCharacterController);
			if (enableYClamp && yTI == PropertiesInfluenced.RotateLocal)
			{
				DoAngleLimitation(yAxisTransform, yAI, clampYMin, clampYMax, startYLocalAngle);
			}
		}
	}

	private void UpdateGravity()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)xAxisCharacterController != (Object)null && xAxisGravity > 0f)
		{
			xAxisCharacterController.Move(Vector3.down * xAxisGravity * Time.deltaTime);
		}
		if ((Object)(object)yAxisCharacterController != (Object)null && yAxisGravity > 0f)
		{
			yAxisCharacterController.Move(Vector3.down * yAxisGravity * Time.deltaTime);
		}
	}

	private Vector3 GetInfluencedAxis(AxisInfluenced axisInfluenced)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = Vector3.zero;
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			result = Vector3.right;
			break;
		case AxisInfluenced.Y:
			result = Vector3.up;
			break;
		case AxisInfluenced.Z:
			result = Vector3.forward;
			break;
		case AxisInfluenced.XYZ:
			result = Vector3.one;
			break;
		}
		return result;
	}

	private void DoActionDirect(Transform axisTransform, PropertiesInfluenced inlfuencedProperty, Vector3 axis, float sensibility, CharacterController charact)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		switch (inlfuencedProperty)
		{
		case PropertiesInfluenced.Rotate:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, (Space)0);
			break;
		case PropertiesInfluenced.RotateLocal:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, (Space)1);
			break;
		case PropertiesInfluenced.Translate:
			if ((Object)(object)charact == (Object)null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, (Space)0);
			}
			else
			{
				charact.Move(axis * sensibility * Time.deltaTime);
			}
			break;
		case PropertiesInfluenced.TranslateLocal:
			if ((Object)(object)charact == (Object)null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, (Space)1);
			}
			else
			{
				charact.Move(((Component)charact).transform.TransformDirection(axis) * sensibility * Time.deltaTime);
			}
			break;
		case PropertiesInfluenced.Scale:
			axisTransform.localScale += axis * sensibility * Time.deltaTime;
			break;
		}
	}

	private void DoAngleLimitation(Transform axisTransform, AxisInfluenced axisInfluenced, float clampMin, float clampMax, float startAngle)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		Quaternion localRotation;
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			localRotation = axisTransform.localRotation;
			num = ((Quaternion)(ref localRotation)).eulerAngles.x;
			break;
		case AxisInfluenced.Y:
			localRotation = axisTransform.localRotation;
			num = ((Quaternion)(ref localRotation)).eulerAngles.y;
			break;
		case AxisInfluenced.Z:
			localRotation = axisTransform.localRotation;
			num = ((Quaternion)(ref localRotation)).eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		num = Mathf.Clamp(num, 0f - clampMax, clampMin);
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			axisTransform.localEulerAngles = new Vector3(num, axisTransform.localEulerAngles.y, axisTransform.localEulerAngles.z);
			break;
		case AxisInfluenced.Y:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, num, axisTransform.localEulerAngles.z);
			break;
		case AxisInfluenced.Z:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, axisTransform.localEulerAngles.y, num);
			break;
		}
	}

	private void DoAutoStabilisation(Transform axisTransform, AxisInfluenced axisInfluenced, float threshold, float speed, float startAngle)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		Quaternion localRotation;
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			localRotation = axisTransform.localRotation;
			num = ((Quaternion)(ref localRotation)).eulerAngles.x;
			break;
		case AxisInfluenced.Y:
			localRotation = axisTransform.localRotation;
			num = ((Quaternion)(ref localRotation)).eulerAngles.y;
			break;
		case AxisInfluenced.Z:
			localRotation = axisTransform.localRotation;
			num = ((Quaternion)(ref localRotation)).eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		if (num > startAngle - threshold || num < startAngle + threshold)
		{
			float num2 = 0f;
			Vector3 zero = Vector3.zero;
			if (num > startAngle - threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime * -1f;
			}
			if (num < startAngle + threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime;
			}
			switch (axisInfluenced)
			{
			case AxisInfluenced.X:
			{
				float num4 = num2;
				localRotation = axisTransform.localRotation;
				float y = ((Quaternion)(ref localRotation)).eulerAngles.y;
				localRotation = axisTransform.localRotation;
				((Vector3)(ref zero))._002Ector(num4, y, ((Quaternion)(ref localRotation)).eulerAngles.z);
				break;
			}
			case AxisInfluenced.Y:
			{
				localRotation = axisTransform.localRotation;
				float x2 = ((Quaternion)(ref localRotation)).eulerAngles.x;
				float num3 = num2;
				localRotation = axisTransform.localRotation;
				((Vector3)(ref zero))._002Ector(x2, num3, ((Quaternion)(ref localRotation)).eulerAngles.z);
				break;
			}
			case AxisInfluenced.Z:
			{
				localRotation = axisTransform.localRotation;
				float x = ((Quaternion)(ref localRotation)).eulerAngles.x;
				localRotation = axisTransform.localRotation;
				((Vector3)(ref zero))._002Ector(x, ((Quaternion)(ref localRotation)).eulerAngles.y, num2);
				break;
			}
			}
			axisTransform.localRotation = Quaternion.Euler(zero);
		}
	}

	private float GetStartAutoStabAngle(Transform axisTransform, AxisInfluenced axisInfluenced)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		if ((Object)(object)axisTransform != (Object)null)
		{
			Quaternion localRotation;
			switch (axisInfluenced)
			{
			case AxisInfluenced.X:
				localRotation = axisTransform.localRotation;
				num = ((Quaternion)(ref localRotation)).eulerAngles.x;
				break;
			case AxisInfluenced.Y:
				localRotation = axisTransform.localRotation;
				num = ((Quaternion)(ref localRotation)).eulerAngles.y;
				break;
			case AxisInfluenced.Z:
				localRotation = axisTransform.localRotation;
				num = ((Quaternion)(ref localRotation)).eulerAngles.z;
				break;
			}
			if (num <= 360f && num >= 180f)
			{
				num -= 360f;
			}
		}
		return num;
	}

	private float ComputeDeadZone()
	{
		float num = 0f;
		float num2 = Mathf.Max(((Vector2)(ref joystickTouch)).magnitude, 0.1f);
		if (restrictArea)
		{
			return Mathf.Max(num2 - deadZone, 0f) / (zoneRadius - touchSize - deadZone) / num2;
		}
		return Mathf.Max(num2 - deadZone, 0f) / (zoneRadius - deadZone) / num2;
	}

	private void ComputeJoystickAnchor(JoystickAnchor anchor)
	{
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		if (!restrictArea)
		{
			num = touchSize;
		}
		switch (anchor)
		{
		case JoystickAnchor.UpperLeft:
			anchorPosition = new Vector2(zoneRadius + num, zoneRadius + num);
			break;
		case JoystickAnchor.UpperCenter:
			anchorPosition = new Vector2(VirtualScreen.width / 2f, zoneRadius + num);
			break;
		case JoystickAnchor.UpperRight:
			anchorPosition = new Vector2(VirtualScreen.width - zoneRadius - num, zoneRadius + num);
			break;
		case JoystickAnchor.MiddleLeft:
			anchorPosition = new Vector2(zoneRadius + num, VirtualScreen.height / 2f);
			break;
		case JoystickAnchor.MiddleCenter:
			anchorPosition = new Vector2(VirtualScreen.width / 2f, VirtualScreen.height / 2f);
			break;
		case JoystickAnchor.MiddleRight:
			anchorPosition = new Vector2(VirtualScreen.width - zoneRadius - num, VirtualScreen.height / 2f);
			break;
		case JoystickAnchor.LowerLeft:
			anchorPosition = new Vector2(zoneRadius + num, VirtualScreen.height - zoneRadius - num);
			break;
		case JoystickAnchor.LowerCenter:
			anchorPosition = new Vector2(VirtualScreen.width / 2f, VirtualScreen.height - zoneRadius - num);
			break;
		case JoystickAnchor.LowerRight:
			anchorPosition = new Vector2(VirtualScreen.width - zoneRadius - num, VirtualScreen.height - zoneRadius - num);
			break;
		case JoystickAnchor.None:
			anchorPosition = Vector2.zero;
			break;
		}
		areaRect = new Rect(anchorPosition.x + joystickCenter.x - zoneRadius, anchorPosition.y + joystickCenter.y - zoneRadius, zoneRadius * 2f, zoneRadius * 2f);
		deadRect = new Rect(anchorPosition.x + joystickCenter.x - deadZone, anchorPosition.y + joystickCenter.y - deadZone, deadZone * 2f, deadZone * 2f);
	}

	private void On_TouchStart(Gesture gesture)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		if (((gesture.isHoverReservedArea || !dynamicJoystick) && dynamicJoystick) || !isActivated)
		{
			return;
		}
		if (!dynamicJoystick)
		{
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((anchorPosition.x + joystickCenter.x) * VirtualScreen.xRatio, (VirtualScreen.height - anchorPosition.y - joystickCenter.y) * VirtualScreen.yRatio);
			Vector2 val2 = gesture.position - val;
			if (((Vector2)(ref val2)).sqrMagnitude < zoneRadius * VirtualScreen.xRatio * (zoneRadius * VirtualScreen.xRatio))
			{
				joystickIndex = gesture.fingerIndex;
				CreateEvent(MessageName.On_JoystickTouchStart);
			}
		}
		else
		{
			if (virtualJoystick)
			{
				return;
			}
			switch (area)
			{
			case DynamicArea.FullScreen:
				virtualJoystick = true;
				break;
			case DynamicArea.Bottom:
				if (gesture.position.y < (float)(Screen.height / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.Top:
				if (gesture.position.y > (float)(Screen.height / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.Right:
				if (gesture.position.x > (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.Left:
				if (gesture.position.x < (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.TopRight:
				if (gesture.position.y > (float)(Screen.height / 2) && gesture.position.x > (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.TopLeft:
				if (gesture.position.y > (float)(Screen.height / 2) && gesture.position.x < (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.BottomRight:
				if (gesture.position.y < (float)(Screen.height / 2) && gesture.position.x > (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.BottomLeft:
				if (gesture.position.y < (float)(Screen.height / 2) && gesture.position.x < (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			}
			if (virtualJoystick)
			{
				joystickCenter = new Vector2(gesture.position.x / VirtualScreen.xRatio, VirtualScreen.height - gesture.position.y / VirtualScreen.yRatio);
				JoyAnchor = JoystickAnchor.None;
				joystickIndex = gesture.fingerIndex;
			}
		}
	}

	private void On_SimpleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && dynamicJoystick) || !dynamicJoystick) && isActivated && gesture.fingerIndex == joystickIndex)
		{
			CreateEvent(MessageName.On_JoystickTap);
		}
	}

	private void On_DoubleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && dynamicJoystick) || !dynamicJoystick) && isActivated && gesture.fingerIndex == joystickIndex)
		{
			CreateEvent(MessageName.On_JoystickDoubleTap);
		}
	}

	private void On_TouchDown(Gesture gesture)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		if (((gesture.isHoverReservedArea || !dynamicJoystick) && dynamicJoystick) || !isActivated)
		{
			return;
		}
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector((anchorPosition.x + joystickCenter.x) * VirtualScreen.xRatio, (VirtualScreen.height - (anchorPosition.y + joystickCenter.y)) * VirtualScreen.yRatio);
		if (gesture.fingerIndex != joystickIndex)
		{
			return;
		}
		Vector2 val2 = gesture.position - val;
		if ((((Vector2)(ref val2)).sqrMagnitude < zoneRadius * VirtualScreen.xRatio * (zoneRadius * VirtualScreen.xRatio) && resetFingerExit) || !resetFingerExit)
		{
			joystickTouch = new Vector2(gesture.position.x, gesture.position.y) - val;
			joystickTouch = new Vector2(joystickTouch.x / VirtualScreen.xRatio, joystickTouch.y / VirtualScreen.yRatio);
			if (!enableXaxis)
			{
				joystickTouch.x = 0f;
			}
			if (!enableYaxis)
			{
				joystickTouch.y = 0f;
			}
			val2 = joystickTouch / (zoneRadius - touchSizeCoef);
			if (((Vector2)(ref val2)).sqrMagnitude > 1f)
			{
				((Vector2)(ref joystickTouch)).Normalize();
				joystickTouch *= zoneRadius - touchSizeCoef;
			}
		}
		else
		{
			On_TouchUp(gesture);
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && dynamicJoystick) || !dynamicJoystick) && isActivated && gesture.fingerIndex == joystickIndex)
		{
			joystickIndex = -1;
			if (dynamicJoystick)
			{
				virtualJoystick = false;
			}
			CreateEvent(MessageName.On_JoystickTouchUp);
		}
	}
}
