using System.Collections.Generic;
using UnityEngine;

public class EasyTouch : MonoBehaviour
{
	public delegate void TouchCancelHandler(Gesture gesture);

	public delegate void Cancel2FingersHandler(Gesture gesture);

	public delegate void TouchStartHandler(Gesture gesture);

	public delegate void TouchDownHandler(Gesture gesture);

	public delegate void TouchUpHandler(Gesture gesture);

	public delegate void SimpleTapHandler(Gesture gesture);

	public delegate void DoubleTapHandler(Gesture gesture);

	public delegate void LongTapStartHandler(Gesture gesture);

	public delegate void LongTapHandler(Gesture gesture);

	public delegate void LongTapEndHandler(Gesture gesture);

	public delegate void DragStartHandler(Gesture gesture);

	public delegate void DragHandler(Gesture gesture);

	public delegate void DragEndHandler(Gesture gesture);

	public delegate void SwipeStartHandler(Gesture gesture);

	public delegate void SwipeHandler(Gesture gesture);

	public delegate void SwipeEndHandler(Gesture gesture);

	public delegate void TouchStart2FingersHandler(Gesture gesture);

	public delegate void TouchDown2FingersHandler(Gesture gesture);

	public delegate void TouchUp2FingersHandler(Gesture gesture);

	public delegate void SimpleTap2FingersHandler(Gesture gesture);

	public delegate void DoubleTap2FingersHandler(Gesture gesture);

	public delegate void LongTapStart2FingersHandler(Gesture gesture);

	public delegate void LongTap2FingersHandler(Gesture gesture);

	public delegate void LongTapEnd2FingersHandler(Gesture gesture);

	public delegate void TwistHandler(Gesture gesture);

	public delegate void TwistEndHandler(Gesture gesture);

	public delegate void PinchInHandler(Gesture gesture);

	public delegate void PinchOutHandler(Gesture gesture);

	public delegate void PinchEndHandler(Gesture gesture);

	public delegate void DragStart2FingersHandler(Gesture gesture);

	public delegate void Drag2FingersHandler(Gesture gesture);

	public delegate void DragEnd2FingersHandler(Gesture gesture);

	public delegate void SwipeStart2FingersHandler(Gesture gesture);

	public delegate void Swipe2FingersHandler(Gesture gesture);

	public delegate void SwipeEnd2FingersHandler(Gesture gesture);

	public enum GestureType
	{
		Tap,
		Drag,
		Swipe,
		None,
		LongTap,
		Pinch,
		Twist,
		Cancel,
		Acquisition
	}

	public enum SwipeType
	{
		None,
		Left,
		Right,
		Up,
		Down,
		Other
	}

	private enum EventName
	{
		None,
		On_Cancel,
		On_Cancel2Fingers,
		On_TouchStart,
		On_TouchDown,
		On_TouchUp,
		On_SimpleTap,
		On_DoubleTap,
		On_LongTapStart,
		On_LongTap,
		On_LongTapEnd,
		On_DragStart,
		On_Drag,
		On_DragEnd,
		On_SwipeStart,
		On_Swipe,
		On_SwipeEnd,
		On_TouchStart2Fingers,
		On_TouchDown2Fingers,
		On_TouchUp2Fingers,
		On_SimpleTap2Fingers,
		On_DoubleTap2Fingers,
		On_LongTapStart2Fingers,
		On_LongTap2Fingers,
		On_LongTapEnd2Fingers,
		On_Twist,
		On_TwistEnd,
		On_PinchIn,
		On_PinchOut,
		On_PinchEnd,
		On_DragStart2Fingers,
		On_Drag2Fingers,
		On_DragEnd2Fingers,
		On_SwipeStart2Fingers,
		On_Swipe2Fingers,
		On_SwipeEnd2Fingers
	}

	public bool enable = true;

	public bool enableRemote;

	public bool useBroadcastMessage = true;

	public GameObject receiverObject;

	public bool isExtension;

	public bool enable2FingersGesture = true;

	public bool enableTwist = true;

	public bool enablePinch = true;

	public Camera easyTouchCamera;

	public bool autoSelect;

	public LayerMask pickableLayers;

	public float StationnaryTolerance = 25f;

	public float longTapTime = 1f;

	public float swipeTolerance = 0.85f;

	public float minPinchLength;

	public float minTwistAngle = 1f;

	public bool enabledNGuiMode;

	public LayerMask nGUILayers;

	public List<Camera> nGUICameras = new List<Camera>();

	private bool isStartHoverNGUI;

	public List<Rect> reservedAreas = new List<Rect>();

	public bool enableReservedArea = true;

	public KeyCode twistKey = (KeyCode)308;

	public KeyCode swipeKey = (KeyCode)306;

	public bool showGeneral = true;

	public bool showSelect = true;

	public bool showGesture = true;

	public bool showTwoFinger = true;

	public bool showSecondFinger = true;

	public static EasyTouch instance;

	private EasyTouchInput input;

	private GestureType complexCurrentGesture = GestureType.None;

	private GestureType oldGesture = GestureType.None;

	private float startTimeAction;

	private Finger[] fingers = new Finger[10];

	private GameObject pickObject2Finger;

	private GameObject oldPickObject2Finger;

	public Texture secondFingerTexture;

	private Vector2 startPosition2Finger;

	private int twoFinger0;

	private int twoFinger1;

	private Vector2 oldStartPosition2Finger;

	private float oldFingerDistance;

	private bool twoFingerDragStart;

	private bool twoFingerSwipeStart;

	private int oldTouchCount;

	public static event TouchCancelHandler On_Cancel;

	public static event Cancel2FingersHandler On_Cancel2Fingers;

	public static event TouchStartHandler On_TouchStart;

	public static event TouchDownHandler On_TouchDown;

	public static event TouchUpHandler On_TouchUp;

	public static event SimpleTapHandler On_SimpleTap;

	public static event DoubleTapHandler On_DoubleTap;

	public static event LongTapStartHandler On_LongTapStart;

	public static event LongTapHandler On_LongTap;

	public static event LongTapEndHandler On_LongTapEnd;

	public static event DragStartHandler On_DragStart;

	public static event DragHandler On_Drag;

	public static event DragEndHandler On_DragEnd;

	public static event SwipeStartHandler On_SwipeStart;

	public static event SwipeHandler On_Swipe;

	public static event SwipeEndHandler On_SwipeEnd;

	public static event TouchStart2FingersHandler On_TouchStart2Fingers;

	public static event TouchDown2FingersHandler On_TouchDown2Fingers;

	public static event TouchUp2FingersHandler On_TouchUp2Fingers;

	public static event SimpleTap2FingersHandler On_SimpleTap2Fingers;

	public static event DoubleTap2FingersHandler On_DoubleTap2Fingers;

	public static event LongTapStart2FingersHandler On_LongTapStart2Fingers;

	public static event LongTap2FingersHandler On_LongTap2Fingers;

	public static event LongTapEnd2FingersHandler On_LongTapEnd2Fingers;

	public static event TwistHandler On_Twist;

	public static event TwistEndHandler On_TwistEnd;

	public static event PinchInHandler On_PinchIn;

	public static event PinchOutHandler On_PinchOut;

	public static event PinchEndHandler On_PinchEnd;

	public static event DragStart2FingersHandler On_DragStart2Fingers;

	public static event Drag2FingersHandler On_Drag2Fingers;

	public static event DragEnd2FingersHandler On_DragEnd2Fingers;

	public static event SwipeStart2FingersHandler On_SwipeStart2Fingers;

	public static event Swipe2FingersHandler On_Swipe2Fingers;

	public static event SwipeEnd2FingersHandler On_SwipeEnd2Fingers;

	public EasyTouch()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		enable = true;
		useBroadcastMessage = false;
		enable2FingersGesture = true;
		enableTwist = true;
		enablePinch = true;
		autoSelect = false;
		StationnaryTolerance = 25f;
		longTapTime = 1f;
		swipeTolerance = 0.85f;
		minPinchLength = 0f;
		minTwistAngle = 1f;
	}

	private void OnEnable()
	{
		if (Application.isPlaying && Application.isEditor)
		{
			InitEasyTouch();
		}
	}

	private void Start()
	{
		InitEasyTouch();
	}

	private void InitEasyTouch()
	{
		input = new EasyTouchInput();
		if ((Object)(object)instance == (Object)null)
		{
			instance = this;
		}
		if ((Object)(object)easyTouchCamera == (Object)null)
		{
			easyTouchCamera = Camera.main;
			if ((Object)(object)easyTouchCamera == (Object)null && autoSelect)
			{
				Debug.LogWarning((object)"No camera with flag \"MainCam\" was found in the scene, please setup the camera");
			}
		}
		if ((Object)(object)secondFingerTexture == (Object)null)
		{
			ref Texture reference = ref secondFingerTexture;
			Object obj = Resources.Load("secondFinger");
			reference = (Texture)(object)((obj is Texture) ? obj : null);
		}
	}

	private void OnGUI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 secondFingerPosition = input.GetSecondFingerPosition();
		if (secondFingerPosition != new Vector2(-1f, -1f))
		{
			GUI.DrawTexture(new Rect(secondFingerPosition.x - 16f, (float)Screen.height - secondFingerPosition.y - 16f, 32f, 32f), secondFingerTexture);
		}
	}

	private void OnDrawGizmos()
	{
	}

	private void Update()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (!enable || !((Object)(object)instance == (Object)(object)this))
		{
			return;
		}
		int num = input.TouchCount();
		if (oldTouchCount == 2 && num != 2 && num > 0)
		{
			CreateGesture2Finger(EventName.On_Cancel2Fingers, Vector2.zero, Vector2.zero, Vector2.zero, 0f, SwipeType.None, 0f, Vector2.zero, 0f, 0f, 0f);
		}
		UpdateTouches(realTouch: false, num);
		oldPickObject2Finger = pickObject2Finger;
		if (enable2FingersGesture)
		{
			if (num == 2)
			{
				TwoFinger();
			}
			else
			{
				complexCurrentGesture = GestureType.None;
				pickObject2Finger = null;
				twoFingerSwipeStart = false;
				twoFingerDragStart = false;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			if (fingers[i] != null)
			{
				OneFinger(i);
			}
		}
		oldTouchCount = num;
	}

	private void UpdateTouches(bool realTouch, int touchCount)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Finger[] array = new Finger[10];
		fingers.CopyTo(array, 0);
		if (realTouch || enableRemote)
		{
			ResetTouches();
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				for (int j = 0; j < 10; j++)
				{
					if (fingers[i] != null)
					{
						break;
					}
					if (array[j] != null && array[j].fingerIndex == ((Touch)(ref touch)).fingerId)
					{
						fingers[i] = array[j];
					}
				}
				if (fingers[i] == null)
				{
					fingers[i] = new Finger();
					fingers[i].fingerIndex = ((Touch)(ref touch)).fingerId;
					fingers[i].gesture = GestureType.None;
					fingers[i].phase = (TouchPhase)0;
				}
				else
				{
					fingers[i].phase = ((Touch)(ref touch)).phase;
				}
				fingers[i].position = ((Touch)(ref touch)).position;
				fingers[i].deltaPosition = ((Touch)(ref touch)).deltaPosition;
				fingers[i].tapCount = ((Touch)(ref touch)).tapCount;
				fingers[i].deltaTime = ((Touch)(ref touch)).deltaTime;
				fingers[i].touchCount = touchCount;
			}
		}
		else
		{
			for (int k = 0; k < touchCount; k++)
			{
				fingers[k] = input.GetMouseTouch(k, fingers[k]);
				fingers[k].touchCount = touchCount;
			}
		}
	}

	private void ResetTouches()
	{
		for (int i = 0; i < 10; i++)
		{
			fingers[i] = null;
		}
	}

	private void OneFinger(int fingerIndex)
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Invalid comparison between Unknown and I4
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Invalid comparison between Unknown and I4
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Invalid comparison between Unknown and I4
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Invalid comparison between Unknown and I4
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		if (fingers[fingerIndex].gesture == GestureType.None)
		{
			startTimeAction = Time.realtimeSinceStartup;
			fingers[fingerIndex].gesture = GestureType.Acquisition;
			fingers[fingerIndex].startPosition = fingers[fingerIndex].position;
			if (autoSelect)
			{
				fingers[fingerIndex].pickedObject = GetPickeGameObject(fingers[fingerIndex].startPosition);
			}
			CreateGesture(fingerIndex, EventName.On_TouchStart, fingers[fingerIndex], 0f, SwipeType.None, 0f, Vector2.zero);
		}
		num = Time.realtimeSinceStartup - startTimeAction;
		if ((int)fingers[fingerIndex].phase == 4)
		{
			fingers[fingerIndex].gesture = GestureType.Cancel;
		}
		if ((int)fingers[fingerIndex].phase != 3 && (int)fingers[fingerIndex].phase != 4)
		{
			if ((int)fingers[fingerIndex].phase == 2 && num >= longTapTime && fingers[fingerIndex].gesture == GestureType.Acquisition)
			{
				fingers[fingerIndex].gesture = GestureType.LongTap;
				CreateGesture(fingerIndex, EventName.On_LongTapStart, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
			}
			if ((fingers[fingerIndex].gesture == GestureType.Acquisition || fingers[fingerIndex].gesture == GestureType.LongTap) && !FingerInTolerance(fingers[fingerIndex]))
			{
				if (fingers[fingerIndex].gesture == GestureType.LongTap)
				{
					fingers[fingerIndex].gesture = GestureType.Cancel;
					CreateGesture(fingerIndex, EventName.On_LongTapEnd, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
					fingers[fingerIndex].gesture = GestureType.None;
				}
				else if (Object.op_Implicit((Object)(object)fingers[fingerIndex].pickedObject))
				{
					fingers[fingerIndex].gesture = GestureType.Drag;
					CreateGesture(fingerIndex, EventName.On_DragStart, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
				}
				else
				{
					fingers[fingerIndex].gesture = GestureType.Swipe;
					CreateGesture(fingerIndex, EventName.On_SwipeStart, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
				}
			}
			EventName eventName = EventName.None;
			switch (fingers[fingerIndex].gesture)
			{
			case GestureType.LongTap:
				eventName = EventName.On_LongTap;
				break;
			case GestureType.Drag:
				eventName = EventName.On_Drag;
				break;
			case GestureType.Swipe:
				eventName = EventName.On_Swipe;
				break;
			}
			SwipeType swipe = SwipeType.None;
			if (eventName != 0)
			{
				swipe = GetSwipe(new Vector2(0f, 0f), fingers[fingerIndex].deltaPosition);
				CreateGesture(fingerIndex, eventName, fingers[fingerIndex], num, swipe, 0f, fingers[fingerIndex].deltaPosition);
			}
			CreateGesture(fingerIndex, EventName.On_TouchDown, fingers[fingerIndex], num, swipe, 0f, fingers[fingerIndex].deltaPosition);
			return;
		}
		bool flag = true;
		Vector2 val;
		switch (fingers[fingerIndex].gesture)
		{
		case GestureType.Acquisition:
		{
			if (FingerInTolerance(fingers[fingerIndex]))
			{
				if (fingers[fingerIndex].tapCount < 2)
				{
					CreateGesture(fingerIndex, EventName.On_SimpleTap, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
				}
				else
				{
					CreateGesture(fingerIndex, EventName.On_DoubleTap, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
				}
				break;
			}
			SwipeType swipe4 = GetSwipe(new Vector2(0f, 0f), fingers[fingerIndex].deltaPosition);
			if (Object.op_Implicit((Object)(object)fingers[fingerIndex].pickedObject))
			{
				CreateGesture(fingerIndex, EventName.On_DragStart, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
				CreateGesture(fingerIndex, EventName.On_Drag, fingers[fingerIndex], num, swipe4, 0f, fingers[fingerIndex].deltaPosition);
				Finger finger3 = fingers[fingerIndex];
				float actionTime3 = num;
				SwipeType swipe5 = GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position);
				val = fingers[fingerIndex].startPosition - fingers[fingerIndex].position;
				CreateGesture(fingerIndex, EventName.On_DragEnd, finger3, actionTime3, swipe5, ((Vector2)(ref val)).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
			}
			else
			{
				CreateGesture(fingerIndex, EventName.On_SwipeStart, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
				CreateGesture(fingerIndex, EventName.On_Swipe, fingers[fingerIndex], num, swipe4, 0f, fingers[fingerIndex].deltaPosition);
				Finger finger4 = fingers[fingerIndex];
				float actionTime4 = num;
				SwipeType swipe6 = GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position);
				val = fingers[fingerIndex].position - fingers[fingerIndex].startPosition;
				CreateGesture(fingerIndex, EventName.On_SwipeEnd, finger4, actionTime4, swipe6, ((Vector2)(ref val)).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
			}
			break;
		}
		case GestureType.LongTap:
			CreateGesture(fingerIndex, EventName.On_LongTapEnd, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
			break;
		case GestureType.Drag:
		{
			Finger finger2 = fingers[fingerIndex];
			float actionTime2 = num;
			SwipeType swipe3 = GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position);
			val = fingers[fingerIndex].startPosition - fingers[fingerIndex].position;
			CreateGesture(fingerIndex, EventName.On_DragEnd, finger2, actionTime2, swipe3, ((Vector2)(ref val)).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
			break;
		}
		case GestureType.Swipe:
		{
			Finger finger = fingers[fingerIndex];
			float actionTime = num;
			SwipeType swipe2 = GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position);
			val = fingers[fingerIndex].position - fingers[fingerIndex].startPosition;
			CreateGesture(fingerIndex, EventName.On_SwipeEnd, finger, actionTime, swipe2, ((Vector2)(ref val)).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
			break;
		}
		case GestureType.Cancel:
			CreateGesture(fingerIndex, EventName.On_Cancel, fingers[fingerIndex], 0f, SwipeType.None, 0f, Vector2.zero);
			break;
		}
		if (flag)
		{
			CreateGesture(fingerIndex, EventName.On_TouchUp, fingers[fingerIndex], num, SwipeType.None, 0f, Vector2.zero);
			fingers[fingerIndex] = null;
		}
	}

	private void CreateGesture(int touchIndex, EventName message, Finger finger, float actionTime, SwipeType swipe, float swipeLength, Vector2 swipeVector)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (message == EventName.On_TouchStart)
		{
			isStartHoverNGUI = IsTouchHoverNGui(touchIndex);
		}
		if (message == EventName.On_Cancel || message == EventName.On_TouchUp)
		{
			isStartHoverNGUI = false;
		}
		if (!isStartHoverNGUI)
		{
			Gesture gesture = new Gesture();
			gesture.fingerIndex = finger.fingerIndex;
			gesture.touchCount = finger.touchCount;
			gesture.startPosition = finger.startPosition;
			gesture.position = finger.position;
			gesture.deltaPosition = finger.deltaPosition;
			gesture.actionTime = actionTime;
			gesture.deltaTime = finger.deltaTime;
			gesture.swipe = swipe;
			gesture.swipeLength = swipeLength;
			gesture.swipeVector = swipeVector;
			gesture.deltaPinch = 0f;
			gesture.twistAngle = 0f;
			gesture.pickObject = finger.pickedObject;
			gesture.otherReceiver = receiverObject;
			gesture.isHoverReservedArea = IsTouchHoverVirtualControll(touchIndex);
			if (useBroadcastMessage)
			{
				SendGesture(message, gesture);
			}
			if (!useBroadcastMessage || isExtension)
			{
				RaiseEvent(message, gesture);
			}
		}
	}

	private void SendGesture(EventName message, Gesture gesture)
	{
		if (useBroadcastMessage)
		{
			if ((Object)(object)receiverObject != (Object)null && (Object)(object)receiverObject != (Object)(object)gesture.pickObject)
			{
				receiverObject.SendMessage(message.ToString(), (object)gesture, (SendMessageOptions)1);
			}
			if (Object.op_Implicit((Object)(object)gesture.pickObject))
			{
				gesture.pickObject.SendMessage(message.ToString(), (object)gesture, (SendMessageOptions)1);
			}
			else
			{
				((Component)this).SendMessage(message.ToString(), (object)gesture, (SendMessageOptions)1);
			}
		}
	}

	private void TwoFinger()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Invalid comparison between Unknown and I4
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Invalid comparison between Unknown and I4
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Invalid comparison between Unknown and I4
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Invalid comparison between Unknown and I4
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		float actionTime = 0f;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		float num = 0f;
		if (complexCurrentGesture == GestureType.None)
		{
			twoFinger0 = GetTwoFinger(-1);
			twoFinger1 = GetTwoFinger(twoFinger0);
			startTimeAction = Time.realtimeSinceStartup;
			complexCurrentGesture = GestureType.Tap;
			fingers[twoFinger0].complexStartPosition = fingers[twoFinger0].position;
			fingers[twoFinger1].complexStartPosition = fingers[twoFinger1].position;
			fingers[twoFinger0].oldPosition = fingers[twoFinger0].position;
			fingers[twoFinger1].oldPosition = fingers[twoFinger1].position;
			oldFingerDistance = Mathf.Abs(Vector2.Distance(fingers[twoFinger0].position, fingers[twoFinger1].position));
			startPosition2Finger = new Vector2((fingers[twoFinger0].position.x + fingers[twoFinger1].position.x) / 2f, (fingers[twoFinger0].position.y + fingers[twoFinger1].position.y) / 2f);
			zero2 = Vector2.zero;
			if (autoSelect)
			{
				pickObject2Finger = GetPickeGameObject(fingers[twoFinger0].complexStartPosition);
				if ((Object)(object)pickObject2Finger != (Object)(object)GetPickeGameObject(fingers[twoFinger1].complexStartPosition))
				{
					pickObject2Finger = null;
				}
			}
			CreateGesture2Finger(EventName.On_TouchStart2Fingers, startPosition2Finger, startPosition2Finger, zero2, actionTime, SwipeType.None, 0f, Vector2.zero, 0f, 0f, oldFingerDistance);
		}
		actionTime = Time.realtimeSinceStartup - startTimeAction;
		((Vector2)(ref zero))._002Ector((fingers[twoFinger0].position.x + fingers[twoFinger1].position.x) / 2f, (fingers[twoFinger0].position.y + fingers[twoFinger1].position.y) / 2f);
		zero2 = zero - oldStartPosition2Finger;
		num = Mathf.Abs(Vector2.Distance(fingers[twoFinger0].position, fingers[twoFinger1].position));
		if ((int)fingers[twoFinger0].phase == 4 || (int)fingers[twoFinger1].phase == 4)
		{
			complexCurrentGesture = GestureType.Cancel;
		}
		if ((int)fingers[twoFinger0].phase != 3 && (int)fingers[twoFinger1].phase != 3 && complexCurrentGesture != GestureType.Cancel)
		{
			if (complexCurrentGesture == GestureType.Tap && actionTime >= longTapTime && FingerInTolerance(fingers[twoFinger0]) && FingerInTolerance(fingers[twoFinger1]))
			{
				complexCurrentGesture = GestureType.LongTap;
				CreateGesture2Finger(EventName.On_LongTapStart2Fingers, startPosition2Finger, zero, zero2, actionTime, SwipeType.None, 0f, Vector2.zero, 0f, 0f, num);
			}
			if (true)
			{
				float num2 = Vector2.Dot(((Vector2)(ref fingers[twoFinger0].deltaPosition)).normalized, ((Vector2)(ref fingers[twoFinger1].deltaPosition)).normalized);
				if (enablePinch && num != oldFingerDistance)
				{
					if (Mathf.Abs(num - oldFingerDistance) >= minPinchLength)
					{
						complexCurrentGesture = GestureType.Pinch;
					}
					if (complexCurrentGesture == GestureType.Pinch)
					{
						if (num < oldFingerDistance)
						{
							if (oldGesture != GestureType.Pinch)
							{
								CreateStateEnd2Fingers(oldGesture, startPosition2Finger, zero, zero2, actionTime, realEnd: false, num);
								startTimeAction = Time.realtimeSinceStartup;
							}
							CreateGesture2Finger(EventName.On_PinchIn, startPosition2Finger, zero, zero2, actionTime, GetSwipe(fingers[twoFinger0].complexStartPosition, fingers[twoFinger0].position), 0f, Vector2.zero, 0f, Mathf.Abs(num - oldFingerDistance), num);
							complexCurrentGesture = GestureType.Pinch;
						}
						else if (num > oldFingerDistance)
						{
							if (oldGesture != GestureType.Pinch)
							{
								CreateStateEnd2Fingers(oldGesture, startPosition2Finger, zero, zero2, actionTime, realEnd: false, num);
								startTimeAction = Time.realtimeSinceStartup;
							}
							CreateGesture2Finger(EventName.On_PinchOut, startPosition2Finger, zero, zero2, actionTime, GetSwipe(fingers[twoFinger0].complexStartPosition, fingers[twoFinger0].position), 0f, Vector2.zero, 0f, Mathf.Abs(num - oldFingerDistance), num);
							complexCurrentGesture = GestureType.Pinch;
						}
					}
				}
				if (enableTwist)
				{
					if (Mathf.Abs(TwistAngle()) > minTwistAngle)
					{
						if (complexCurrentGesture != GestureType.Twist)
						{
							CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, zero, zero2, actionTime, realEnd: false, num);
							startTimeAction = Time.realtimeSinceStartup;
						}
						complexCurrentGesture = GestureType.Twist;
					}
					if (complexCurrentGesture == GestureType.Twist)
					{
						CreateGesture2Finger(EventName.On_Twist, startPosition2Finger, zero, zero2, actionTime, SwipeType.None, 0f, Vector2.zero, TwistAngle(), 0f, num);
					}
					fingers[twoFinger0].oldPosition = fingers[twoFinger0].position;
					fingers[twoFinger1].oldPosition = fingers[twoFinger1].position;
				}
				if (num2 > 0f)
				{
					if (Object.op_Implicit((Object)(object)pickObject2Finger) && !twoFingerDragStart)
					{
						if (complexCurrentGesture != 0)
						{
							CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, zero, zero2, actionTime, realEnd: false, num);
							startTimeAction = Time.realtimeSinceStartup;
						}
						CreateGesture2Finger(EventName.On_DragStart2Fingers, startPosition2Finger, zero, zero2, actionTime, SwipeType.None, 0f, Vector2.zero, 0f, 0f, num);
						twoFingerDragStart = true;
					}
					else if (!Object.op_Implicit((Object)(object)pickObject2Finger) && !twoFingerSwipeStart)
					{
						if (complexCurrentGesture != 0)
						{
							CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, zero, zero2, actionTime, realEnd: false, num);
							startTimeAction = Time.realtimeSinceStartup;
						}
						CreateGesture2Finger(EventName.On_SwipeStart2Fingers, startPosition2Finger, zero, zero2, actionTime, SwipeType.None, 0f, Vector2.zero, 0f, 0f, num);
						twoFingerSwipeStart = true;
					}
				}
				else if (num2 < 0f)
				{
					twoFingerDragStart = false;
					twoFingerSwipeStart = false;
				}
				if (twoFingerDragStart)
				{
					CreateGesture2Finger(EventName.On_Drag2Fingers, startPosition2Finger, zero, zero2, actionTime, GetSwipe(oldStartPosition2Finger, zero), 0f, zero2, 0f, 0f, num);
				}
				if (twoFingerSwipeStart)
				{
					CreateGesture2Finger(EventName.On_Swipe2Fingers, startPosition2Finger, zero, zero2, actionTime, GetSwipe(oldStartPosition2Finger, zero), 0f, zero2, 0f, 0f, num);
				}
			}
			else if (complexCurrentGesture == GestureType.LongTap)
			{
				CreateGesture2Finger(EventName.On_LongTap2Fingers, startPosition2Finger, zero, zero2, actionTime, SwipeType.None, 0f, Vector2.zero, 0f, 0f, num);
			}
			CreateGesture2Finger(EventName.On_TouchDown2Fingers, startPosition2Finger, zero, zero2, actionTime, GetSwipe(oldStartPosition2Finger, zero), 0f, zero2, 0f, 0f, num);
			oldFingerDistance = num;
			oldStartPosition2Finger = zero;
			oldGesture = complexCurrentGesture;
		}
		else
		{
			CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, zero, zero2, actionTime, realEnd: true, num);
			complexCurrentGesture = GestureType.None;
			pickObject2Finger = null;
			twoFingerSwipeStart = false;
			twoFingerDragStart = false;
		}
	}

	private int GetTwoFinger(int index)
	{
		int i = index + 1;
		bool flag = false;
		for (; i < 10; i++)
		{
			if (flag)
			{
				break;
			}
			if (fingers[i] != null && i >= index)
			{
				flag = true;
			}
		}
		return i - 1;
	}

	private void CreateStateEnd2Fingers(GestureType gesture, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float time, bool realEnd, float fingerDistance)
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		switch (gesture)
		{
		case GestureType.Tap:
			if (fingers[twoFinger0].tapCount < 2 && fingers[twoFinger1].tapCount < 2)
			{
				CreateGesture2Finger(EventName.On_SimpleTap2Fingers, startPosition, position, deltaPosition, time, SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
			else
			{
				CreateGesture2Finger(EventName.On_DoubleTap2Fingers, startPosition, position, deltaPosition, time, SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
			break;
		case GestureType.LongTap:
			CreateGesture2Finger(EventName.On_LongTapEnd2Fingers, startPosition, position, deltaPosition, time, SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		case GestureType.Pinch:
			CreateGesture2Finger(EventName.On_PinchEnd, startPosition, position, deltaPosition, time, SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		case GestureType.Twist:
			CreateGesture2Finger(EventName.On_TwistEnd, startPosition, position, deltaPosition, time, SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		}
		if (realEnd)
		{
			Vector2 val;
			if (twoFingerDragStart)
			{
				SwipeType swipe = GetSwipe(startPosition, position);
				val = position - startPosition;
				CreateGesture2Finger(EventName.On_DragEnd2Fingers, startPosition, position, deltaPosition, time, swipe, ((Vector2)(ref val)).magnitude, position - startPosition, 0f, 0f, fingerDistance);
			}
			if (twoFingerSwipeStart)
			{
				SwipeType swipe2 = GetSwipe(startPosition, position);
				val = position - startPosition;
				CreateGesture2Finger(EventName.On_SwipeEnd2Fingers, startPosition, position, deltaPosition, time, swipe2, ((Vector2)(ref val)).magnitude, position - startPosition, 0f, 0f, fingerDistance);
			}
			CreateGesture2Finger(EventName.On_TouchUp2Fingers, startPosition, position, deltaPosition, time, SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
		}
	}

	private void CreateGesture2Finger(EventName message, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float actionTime, SwipeType swipe, float swipeLength, Vector2 swipeVector, float twist, float pinch, float twoDistance)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (message == EventName.On_TouchStart2Fingers)
		{
			isStartHoverNGUI = IsTouchHoverNGui(twoFinger1) & IsTouchHoverNGui(twoFinger0);
		}
		if (!isStartHoverNGUI)
		{
			Gesture gesture = new Gesture();
			gesture.touchCount = 2;
			gesture.fingerIndex = -1;
			gesture.startPosition = startPosition;
			gesture.position = position;
			gesture.deltaPosition = deltaPosition;
			gesture.actionTime = actionTime;
			if (fingers[twoFinger0] != null)
			{
				gesture.deltaTime = fingers[twoFinger0].deltaTime;
			}
			else if (fingers[twoFinger1] != null)
			{
				gesture.deltaTime = fingers[twoFinger1].deltaTime;
			}
			else
			{
				gesture.deltaTime = 0f;
			}
			gesture.swipe = swipe;
			gesture.swipeLength = swipeLength;
			gesture.swipeVector = swipeVector;
			gesture.deltaPinch = pinch;
			gesture.twistAngle = twist;
			gesture.twoFingerDistance = twoDistance;
			if (message != EventName.On_Cancel2Fingers)
			{
				gesture.pickObject = pickObject2Finger;
			}
			else
			{
				gesture.pickObject = oldPickObject2Finger;
			}
			gesture.otherReceiver = receiverObject;
			if (useBroadcastMessage)
			{
				SendGesture2Finger(message, gesture);
			}
			else
			{
				RaiseEvent(message, gesture);
			}
		}
	}

	private void SendGesture2Finger(EventName message, Gesture gesture)
	{
		if ((Object)(object)receiverObject != (Object)null && (Object)(object)receiverObject != (Object)(object)gesture.pickObject)
		{
			receiverObject.SendMessage(message.ToString(), (object)gesture, (SendMessageOptions)1);
		}
		if ((Object)(object)gesture.pickObject != (Object)null)
		{
			gesture.pickObject.SendMessage(message.ToString(), (object)gesture, (SendMessageOptions)1);
		}
		else
		{
			((Component)this).SendMessage(message.ToString(), (object)gesture, (SendMessageOptions)1);
		}
	}

	private void RaiseEvent(EventName evnt, Gesture gesture)
	{
		switch (evnt)
		{
		case EventName.On_Cancel:
			if (EasyTouch.On_Cancel != null)
			{
				EasyTouch.On_Cancel(gesture);
			}
			break;
		case EventName.On_Cancel2Fingers:
			if (EasyTouch.On_Cancel2Fingers != null)
			{
				EasyTouch.On_Cancel2Fingers(gesture);
			}
			break;
		case EventName.On_TouchStart:
			if (EasyTouch.On_TouchStart != null)
			{
				EasyTouch.On_TouchStart(gesture);
			}
			break;
		case EventName.On_TouchDown:
			if (EasyTouch.On_TouchDown != null)
			{
				EasyTouch.On_TouchDown(gesture);
			}
			break;
		case EventName.On_TouchUp:
			if (EasyTouch.On_TouchUp != null)
			{
				EasyTouch.On_TouchUp(gesture);
			}
			break;
		case EventName.On_SimpleTap:
			if (EasyTouch.On_SimpleTap != null)
			{
				EasyTouch.On_SimpleTap(gesture);
			}
			break;
		case EventName.On_DoubleTap:
			if (EasyTouch.On_DoubleTap != null)
			{
				EasyTouch.On_DoubleTap(gesture);
			}
			break;
		case EventName.On_LongTapStart:
			if (EasyTouch.On_LongTapStart != null)
			{
				EasyTouch.On_LongTapStart(gesture);
			}
			break;
		case EventName.On_LongTap:
			if (EasyTouch.On_LongTap != null)
			{
				EasyTouch.On_LongTap(gesture);
			}
			break;
		case EventName.On_LongTapEnd:
			if (EasyTouch.On_LongTapEnd != null)
			{
				EasyTouch.On_LongTapEnd(gesture);
			}
			break;
		case EventName.On_DragStart:
			if (EasyTouch.On_DragStart != null)
			{
				EasyTouch.On_DragStart(gesture);
			}
			break;
		case EventName.On_Drag:
			if (EasyTouch.On_Drag != null)
			{
				EasyTouch.On_Drag(gesture);
			}
			break;
		case EventName.On_DragEnd:
			if (EasyTouch.On_DragEnd != null)
			{
				EasyTouch.On_DragEnd(gesture);
			}
			break;
		case EventName.On_SwipeStart:
			if (EasyTouch.On_SwipeStart != null)
			{
				EasyTouch.On_SwipeStart(gesture);
			}
			break;
		case EventName.On_Swipe:
			if (EasyTouch.On_Swipe != null)
			{
				EasyTouch.On_Swipe(gesture);
			}
			break;
		case EventName.On_SwipeEnd:
			if (EasyTouch.On_SwipeEnd != null)
			{
				EasyTouch.On_SwipeEnd(gesture);
			}
			break;
		case EventName.On_TouchStart2Fingers:
			if (EasyTouch.On_TouchStart2Fingers != null)
			{
				EasyTouch.On_TouchStart2Fingers(gesture);
			}
			break;
		case EventName.On_TouchDown2Fingers:
			if (EasyTouch.On_TouchDown2Fingers != null)
			{
				EasyTouch.On_TouchDown2Fingers(gesture);
			}
			break;
		case EventName.On_TouchUp2Fingers:
			if (EasyTouch.On_TouchUp2Fingers != null)
			{
				EasyTouch.On_TouchUp2Fingers(gesture);
			}
			break;
		case EventName.On_SimpleTap2Fingers:
			if (EasyTouch.On_SimpleTap2Fingers != null)
			{
				EasyTouch.On_SimpleTap2Fingers(gesture);
			}
			break;
		case EventName.On_DoubleTap2Fingers:
			if (EasyTouch.On_DoubleTap2Fingers != null)
			{
				EasyTouch.On_DoubleTap2Fingers(gesture);
			}
			break;
		case EventName.On_LongTapStart2Fingers:
			if (EasyTouch.On_LongTapStart2Fingers != null)
			{
				EasyTouch.On_LongTapStart2Fingers(gesture);
			}
			break;
		case EventName.On_LongTap2Fingers:
			if (EasyTouch.On_LongTap2Fingers != null)
			{
				EasyTouch.On_LongTap2Fingers(gesture);
			}
			break;
		case EventName.On_LongTapEnd2Fingers:
			if (EasyTouch.On_LongTapEnd2Fingers != null)
			{
				EasyTouch.On_LongTapEnd2Fingers(gesture);
			}
			break;
		case EventName.On_Twist:
			if (EasyTouch.On_Twist != null)
			{
				EasyTouch.On_Twist(gesture);
			}
			break;
		case EventName.On_TwistEnd:
			if (EasyTouch.On_TwistEnd != null)
			{
				EasyTouch.On_TwistEnd(gesture);
			}
			break;
		case EventName.On_PinchIn:
			if (EasyTouch.On_PinchIn != null)
			{
				EasyTouch.On_PinchIn(gesture);
			}
			break;
		case EventName.On_PinchOut:
			if (EasyTouch.On_PinchOut != null)
			{
				EasyTouch.On_PinchOut(gesture);
			}
			break;
		case EventName.On_PinchEnd:
			if (EasyTouch.On_PinchEnd != null)
			{
				EasyTouch.On_PinchEnd(gesture);
			}
			break;
		case EventName.On_DragStart2Fingers:
			if (EasyTouch.On_DragStart2Fingers != null)
			{
				EasyTouch.On_DragStart2Fingers(gesture);
			}
			break;
		case EventName.On_Drag2Fingers:
			if (EasyTouch.On_Drag2Fingers != null)
			{
				EasyTouch.On_Drag2Fingers(gesture);
			}
			break;
		case EventName.On_DragEnd2Fingers:
			if (EasyTouch.On_DragEnd2Fingers != null)
			{
				EasyTouch.On_DragEnd2Fingers(gesture);
			}
			break;
		case EventName.On_SwipeStart2Fingers:
			if (EasyTouch.On_SwipeStart2Fingers != null)
			{
				EasyTouch.On_SwipeStart2Fingers(gesture);
			}
			break;
		case EventName.On_Swipe2Fingers:
			if (EasyTouch.On_Swipe2Fingers != null)
			{
				EasyTouch.On_Swipe2Fingers(gesture);
			}
			break;
		case EventName.On_SwipeEnd2Fingers:
			if (EasyTouch.On_SwipeEnd2Fingers != null)
			{
				EasyTouch.On_SwipeEnd2Fingers(gesture);
			}
			break;
		}
	}

	private GameObject GetPickeGameObject(Vector2 screenPos)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)easyTouchCamera != (Object)null)
		{
			Ray val = easyTouchCamera.ScreenPointToRay(Vector2.op_Implicit(screenPos));
			LayerMask val2 = pickableLayers;
			RaycastHit val3 = default(RaycastHit);
			if (Physics.Raycast(val, ref val3, float.MaxValue, LayerMask.op_Implicit(val2)))
			{
				return ((Component)((RaycastHit)(ref val3)).collider).gameObject;
			}
		}
		else
		{
			Debug.LogWarning((object)"No camera is assigned to EasyTouch");
		}
		return null;
	}

	private SwipeType GetSwipe(Vector2 start, Vector2 end)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = end - start;
		Vector2 normalized = ((Vector2)(ref val)).normalized;
		if (Mathf.Abs(normalized.y) > Mathf.Abs(normalized.x))
		{
			if (Vector2.Dot(normalized, Vector2.up) >= swipeTolerance)
			{
				return SwipeType.Up;
			}
			if (Vector2.Dot(normalized, -Vector2.up) >= swipeTolerance)
			{
				return SwipeType.Down;
			}
		}
		else
		{
			if (Vector2.Dot(normalized, Vector2.right) >= swipeTolerance)
			{
				return SwipeType.Right;
			}
			if (Vector2.Dot(normalized, -Vector2.right) >= swipeTolerance)
			{
				return SwipeType.Left;
			}
		}
		return SwipeType.Other;
	}

	private bool FingerInTolerance(Finger finger)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = finger.position - finger.startPosition;
		if (((Vector2)(ref val)).sqrMagnitude <= StationnaryTolerance * StationnaryTolerance)
		{
			return true;
		}
		return false;
	}

	private float DeltaAngle(Vector2 start, Vector2 end)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return Mathf.Atan2(start.x * end.y - start.y * end.x, Vector2.Dot(start, end));
	}

	private float TwistAngle()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 end = fingers[twoFinger0].position - fingers[twoFinger1].position;
		Vector2 start = fingers[twoFinger0].oldPosition - fingers[twoFinger1].oldPosition;
		return 57.29578f * DeltaAngle(start, end);
	}

	private bool IsTouchHoverNGui(int touchIndex)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		if (enabledNGuiMode)
		{
			LayerMask val = nGUILayers;
			int num = 0;
			RaycastHit val2 = default(RaycastHit);
			while (!flag && num < nGUICameras.Count)
			{
				flag = Physics.Raycast(nGUICameras[num].ScreenPointToRay(Vector2.op_Implicit(fingers[touchIndex].position)), ref val2, float.MaxValue, LayerMask.op_Implicit(val));
				num++;
			}
		}
		return flag;
	}

	private bool IsTouchHoverVirtualControll(int touchIndex)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		if (enableReservedArea)
		{
			int num = 0;
			while (!flag && num < reservedAreas.Count)
			{
				Rect realRect = VirtualScreen.GetRealRect(reservedAreas[num]);
				((Rect)(ref realRect))._002Ector(((Rect)(ref realRect)).x, (float)Screen.height - ((Rect)(ref realRect)).y - ((Rect)(ref realRect)).height, ((Rect)(ref realRect)).width, ((Rect)(ref realRect)).height);
				flag = ((Rect)(ref realRect)).Contains(fingers[touchIndex].position);
				num++;
			}
		}
		return flag;
	}

	private Finger GetFinger(int finderId)
	{
		int i = 0;
		Finger finger = null;
		for (; i < 10; i++)
		{
			if (finger != null)
			{
				break;
			}
			if (fingers[i] != null && fingers[i].fingerIndex == finderId)
			{
				finger = fingers[i];
			}
		}
		return finger;
	}

	public static void SetEnabled(bool enable)
	{
		instance.enable = enable;
		if (enable)
		{
			instance.ResetTouches();
		}
	}

	public static bool GetEnabled()
	{
		return instance.enable;
	}

	public static int GetTouchCount()
	{
		return instance.input.TouchCount();
	}

	public static void SetCamera(Camera cam)
	{
		instance.easyTouchCamera = cam;
	}

	public static Camera GetCamera()
	{
		return instance.easyTouchCamera;
	}

	public static void SetEnable2FingersGesture(bool enable)
	{
		instance.enable2FingersGesture = enable;
	}

	public static bool GetEnable2FingersGesture()
	{
		return instance.enable2FingersGesture;
	}

	public static void SetEnableTwist(bool enable)
	{
		instance.enableTwist = enable;
	}

	public static bool GetEnableTwist()
	{
		return instance.enableTwist;
	}

	public static void SetEnablePinch(bool enable)
	{
		instance.enablePinch = enable;
	}

	public static bool GetEnablePinch()
	{
		return instance.enablePinch;
	}

	public static void SetEnableAutoSelect(bool enable)
	{
		instance.autoSelect = enable;
	}

	public static bool GetEnableAutoSelect()
	{
		return instance.autoSelect;
	}

	public static void SetOtherReceiverObject(GameObject receiver)
	{
		instance.receiverObject = receiver;
	}

	public static GameObject GetOtherReceiverObject()
	{
		return instance.receiverObject;
	}

	public static void SetStationnaryTolerance(float tolerance)
	{
		instance.StationnaryTolerance = tolerance;
	}

	public static float GetStationnaryTolerance()
	{
		return instance.StationnaryTolerance;
	}

	public static void SetlongTapTime(float time)
	{
		instance.longTapTime = time;
	}

	public static float GetlongTapTime()
	{
		return instance.longTapTime;
	}

	public static void SetSwipeTolerance(float tolerance)
	{
		instance.swipeTolerance = tolerance;
	}

	public static float GetSwipeTolerance()
	{
		return instance.swipeTolerance;
	}

	public static void SetMinPinchLength(float length)
	{
		instance.minPinchLength = length;
	}

	public static float GetMinPinchLength()
	{
		return instance.minPinchLength;
	}

	public static void SetMinTwistAngle(float angle)
	{
		instance.minTwistAngle = angle;
	}

	public static float GetMinTwistAngle()
	{
		return instance.minTwistAngle;
	}

	public static GameObject GetCurrentPickedObject(int fingerIndex)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return instance.GetPickeGameObject(instance.GetFinger(fingerIndex).position);
	}

	public static bool IsRectUnderTouch(Rect rect, bool guiRect = false)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		for (int i = 0; i < 10; i++)
		{
			if (instance.fingers[i] != null)
			{
				if (guiRect)
				{
					((Rect)(ref rect))._002Ector(((Rect)(ref rect)).x, (float)Screen.height - ((Rect)(ref rect)).y - ((Rect)(ref rect)).height, ((Rect)(ref rect)).width, ((Rect)(ref rect)).height);
				}
				result = ((Rect)(ref rect)).Contains(instance.fingers[i].position);
				break;
			}
		}
		return result;
	}

	public static Vector2 GetFingerPosition(int fingerIndex)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (instance.fingers[fingerIndex] != null)
		{
			return instance.GetFinger(fingerIndex).position;
		}
		return Vector2.zero;
	}

	public static bool GetIsReservedArea()
	{
		return instance.enableReservedArea;
	}

	public static void SetIsReservedArea(bool enable)
	{
		instance.enableReservedArea = enable;
	}

	public static void AddReservedArea(Rect rec)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		instance.reservedAreas.Add(rec);
	}

	public static void RemoveReservedArea(Rect rec)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		instance.reservedAreas.Remove(rec);
	}

	public static void ResetTouch(int fingerIndex)
	{
		instance.GetFinger(fingerIndex).gesture = GestureType.None;
	}
}
