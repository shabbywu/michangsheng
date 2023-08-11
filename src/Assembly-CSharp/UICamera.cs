using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	public enum ControlScheme
	{
		Mouse,
		Touch,
		Controller
	}

	public enum ClickNotification
	{
		None,
		Always,
		BasedOnDelta
	}

	public class MouseOrTouch
	{
		public Vector2 pos;

		public Vector2 lastPos;

		public Vector2 delta;

		public Vector2 totalDelta;

		public Camera pressedCam;

		public GameObject last;

		public GameObject current;

		public GameObject pressed;

		public GameObject dragged;

		public float clickTime;

		public ClickNotification clickNotification = ClickNotification.Always;

		public bool touchBegan = true;

		public bool pressStarted;

		public bool dragStarted;
	}

	public enum EventType
	{
		World_3D,
		UI_3D,
		World_2D,
		UI_2D
	}

	public delegate void OnScreenResize();

	public delegate void OnCustomInput();

	private struct DepthEntry
	{
		public int depth;

		public RaycastHit hit;

		public Vector3 point;

		public GameObject go;
	}

	public static BetterList<UICamera> list = new BetterList<UICamera>();

	public static OnScreenResize onScreenResize;

	public EventType eventType = EventType.UI_3D;

	public LayerMask eventReceiverMask = LayerMask.op_Implicit(-1);

	public bool debug;

	public bool useMouse = true;

	public bool useTouch = true;

	public bool allowMultiTouch = true;

	public bool useKeyboard = true;

	public bool useController = true;

	public bool stickyTooltip = true;

	public float tooltipDelay = 1f;

	public float mouseDragThreshold = 4f;

	public float mouseClickThreshold = 10f;

	public float touchDragThreshold = 40f;

	public float touchClickThreshold = 40f;

	public float rangeDistance = -1f;

	public string scrollAxisName = "Mouse ScrollWheel";

	public string verticalAxisName = "Vertical";

	public string horizontalAxisName = "Horizontal";

	public KeyCode submitKey0 = (KeyCode)13;

	public KeyCode submitKey1 = (KeyCode)330;

	public KeyCode cancelKey0 = (KeyCode)27;

	public KeyCode cancelKey1 = (KeyCode)331;

	public static OnCustomInput onCustomInput;

	public static bool showTooltips = true;

	public static Vector2 lastTouchPosition = Vector2.zero;

	public static Vector3 lastWorldPosition = Vector3.zero;

	public static RaycastHit lastHit;

	public static UICamera current = null;

	public static Camera currentCamera = null;

	public static ControlScheme currentScheme = ControlScheme.Mouse;

	public static int currentTouchID = -1;

	public static KeyCode currentKey = (KeyCode)0;

	public static MouseOrTouch currentTouch = null;

	public static bool inputHasFocus = false;

	public static GameObject genericEventHandler;

	public static GameObject fallThrough;

	private static GameObject mCurrentSelection = null;

	private static GameObject mNextSelection = null;

	private static ControlScheme mNextScheme = ControlScheme.Controller;

	private static MouseOrTouch[] mMouse = new MouseOrTouch[3]
	{
		new MouseOrTouch(),
		new MouseOrTouch(),
		new MouseOrTouch()
	};

	private static GameObject mHover;

	public static MouseOrTouch controller = new MouseOrTouch();

	private static float mNextEvent = 0f;

	private static Dictionary<int, MouseOrTouch> mTouches = new Dictionary<int, MouseOrTouch>();

	private static int mWidth = 0;

	private static int mHeight = 0;

	private GameObject mTooltip;

	private Camera mCam;

	private float mTooltipTime;

	private float mNextRaycast;

	public static bool isDragging = false;

	public static GameObject hoveredObject;

	private static DepthEntry mHit = default(DepthEntry);

	private static BetterList<DepthEntry> mHits = new BetterList<DepthEntry>();

	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	private static bool mNotifying = false;

	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress => true;

	public static Ray currentRay
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)currentCamera != (Object)null) || currentTouch == null)
			{
				return default(Ray);
			}
			return currentCamera.ScreenPointToRay(Vector2.op_Implicit(currentTouch.pos));
		}
	}

	private bool handlesEvents => (Object)(object)eventHandler == (Object)(object)this;

	public Camera cachedCamera
	{
		get
		{
			if ((Object)(object)mCam == (Object)null)
			{
				mCam = ((Component)this).GetComponent<Camera>();
			}
			return mCam;
		}
	}

	public static GameObject selectedObject
	{
		get
		{
			return mCurrentSelection;
		}
		set
		{
			SetSelection(value, currentScheme);
		}
	}

	public static int touchCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, MouseOrTouch> mTouch in mTouches)
			{
				if ((Object)(object)mTouch.Value.pressed != (Object)null)
				{
					num++;
				}
			}
			for (int i = 0; i < mMouse.Length; i++)
			{
				if ((Object)(object)mMouse[i].pressed != (Object)null)
				{
					num++;
				}
			}
			if ((Object)(object)controller.pressed != (Object)null)
			{
				num++;
			}
			return num;
		}
	}

	public static int dragCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, MouseOrTouch> mTouch in mTouches)
			{
				if ((Object)(object)mTouch.Value.dragged != (Object)null)
				{
					num++;
				}
			}
			for (int i = 0; i < mMouse.Length; i++)
			{
				if ((Object)(object)mMouse[i].dragged != (Object)null)
				{
					num++;
				}
			}
			if ((Object)(object)controller.dragged != (Object)null)
			{
				num++;
			}
			return num;
		}
	}

	public static Camera mainCamera
	{
		get
		{
			UICamera uICamera = eventHandler;
			if (!((Object)(object)uICamera != (Object)null))
			{
				return null;
			}
			return uICamera.cachedCamera;
		}
	}

	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < list.size; i++)
			{
				UICamera uICamera = list.buffer[i];
				if (!((Object)(object)uICamera == (Object)null) && ((Behaviour)uICamera).enabled && NGUITools.GetActive(((Component)uICamera).gameObject))
				{
					return uICamera;
				}
			}
			return null;
		}
	}

	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if ((Object)(object)mMouse[i].pressed == (Object)(object)go)
			{
				return true;
			}
		}
		foreach (KeyValuePair<int, MouseOrTouch> mTouch in mTouches)
		{
			if ((Object)(object)mTouch.Value.pressed == (Object)(object)go)
			{
				return true;
			}
		}
		if ((Object)(object)controller.pressed == (Object)(object)go)
		{
			return true;
		}
		return false;
	}

	protected static void SetSelection(GameObject go, ControlScheme scheme)
	{
		if ((Object)(object)mNextSelection != (Object)null)
		{
			mNextSelection = go;
		}
		else
		{
			if (!((Object)(object)mCurrentSelection != (Object)(object)go))
			{
				return;
			}
			mNextSelection = go;
			mNextScheme = scheme;
			if (list.size > 0)
			{
				UICamera uICamera = (((Object)(object)mNextSelection != (Object)null) ? FindCameraForLayer(mNextSelection.layer) : list[0]);
				if ((Object)(object)uICamera != (Object)null)
				{
					((MonoBehaviour)uICamera).StartCoroutine(uICamera.ChangeSelection());
				}
			}
		}
	}

	private IEnumerator ChangeSelection()
	{
		yield return (object)new WaitForEndOfFrame();
		Notify(mCurrentSelection, "OnSelect", false);
		mCurrentSelection = mNextSelection;
		mNextSelection = null;
		if ((Object)(object)mCurrentSelection != (Object)null)
		{
			current = this;
			currentCamera = mCam;
			currentScheme = mNextScheme;
			inputHasFocus = (Object)(object)mCurrentSelection.GetComponent<UIInput>() != (Object)null;
			Notify(mCurrentSelection, "OnSelect", true);
			current = null;
		}
		else
		{
			inputHasFocus = false;
		}
	}

	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	public static bool Raycast(Vector3 inPos)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < list.size; i++)
		{
			UICamera uICamera = list.buffer[i];
			if (!((Behaviour)uICamera).enabled || !NGUITools.GetActive(((Component)uICamera).gameObject))
			{
				continue;
			}
			currentCamera = uICamera.cachedCamera;
			Vector3 val = currentCamera.ScreenToViewportPoint(inPos);
			if (float.IsNaN(val.x) || float.IsNaN(val.y) || val.x < 0f || val.x > 1f || val.y < 0f || val.y > 1f)
			{
				continue;
			}
			Ray val2 = currentCamera.ScreenPointToRay(inPos);
			int num = currentCamera.cullingMask & LayerMask.op_Implicit(uICamera.eventReceiverMask);
			float num2 = ((uICamera.rangeDistance > 0f) ? uICamera.rangeDistance : (currentCamera.farClipPlane - currentCamera.nearClipPlane));
			if (uICamera.eventType == EventType.World_3D)
			{
				if (Physics.Raycast(val2, ref lastHit, num2, num))
				{
					lastWorldPosition = ((RaycastHit)(ref lastHit)).point;
					hoveredObject = ((Component)((RaycastHit)(ref lastHit)).collider).gameObject;
					return true;
				}
			}
			else if (uICamera.eventType == EventType.UI_3D)
			{
				RaycastHit[] array = Physics.RaycastAll(val2, num2, num);
				if (array.Length > 1)
				{
					for (int j = 0; j < array.Length; j++)
					{
						GameObject gameObject = ((Component)((RaycastHit)(ref array[j])).collider).gameObject;
						UIWidget component = gameObject.GetComponent<UIWidget>();
						if ((Object)(object)component != (Object)null)
						{
							if (!component.isVisible || (component.hitCheck != null && !component.hitCheck(((RaycastHit)(ref array[j])).point)))
							{
								continue;
							}
						}
						else
						{
							UIRect uIRect = NGUITools.FindInParents<UIRect>(gameObject);
							if ((Object)(object)uIRect != (Object)null && uIRect.finalAlpha < 0.001f)
							{
								continue;
							}
						}
						mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
						if (mHit.depth != int.MaxValue)
						{
							mHit.hit = array[j];
							mHit.point = ((RaycastHit)(ref array[j])).point;
							mHit.go = ((Component)((RaycastHit)(ref array[j])).collider).gameObject;
							mHits.Add(mHit);
						}
					}
					mHits.Sort((DepthEntry r1, DepthEntry r2) => r2.depth.CompareTo(r1.depth));
					for (int k = 0; k < mHits.size; k++)
					{
						if (IsVisible(ref mHits.buffer[k]))
						{
							lastHit = mHits[k].hit;
							hoveredObject = mHits[k].go;
							lastWorldPosition = mHits[k].point;
							mHits.Clear();
							return true;
						}
					}
					mHits.Clear();
				}
				else
				{
					if (array.Length != 1)
					{
						continue;
					}
					GameObject gameObject2 = ((Component)((RaycastHit)(ref array[0])).collider).gameObject;
					UIWidget component2 = gameObject2.GetComponent<UIWidget>();
					if ((Object)(object)component2 != (Object)null)
					{
						if (!component2.isVisible || (component2.hitCheck != null && !component2.hitCheck(((RaycastHit)(ref array[0])).point)))
						{
							continue;
						}
					}
					else
					{
						UIRect uIRect2 = NGUITools.FindInParents<UIRect>(gameObject2);
						if ((Object)(object)uIRect2 != (Object)null && uIRect2.finalAlpha < 0.001f)
						{
							continue;
						}
					}
					if (IsVisible(((RaycastHit)(ref array[0])).point, ((Component)((RaycastHit)(ref array[0])).collider).gameObject))
					{
						lastHit = array[0];
						lastWorldPosition = ((RaycastHit)(ref array[0])).point;
						hoveredObject = ((Component)((RaycastHit)(ref lastHit)).collider).gameObject;
						return true;
					}
				}
			}
			else if (uICamera.eventType == EventType.World_2D)
			{
				if (((Plane)(ref m2DPlane)).Raycast(val2, ref num2))
				{
					Vector3 point = ((Ray)(ref val2)).GetPoint(num2);
					Collider2D val3 = Physics2D.OverlapPoint(Vector2.op_Implicit(point), num);
					if (Object.op_Implicit((Object)(object)val3))
					{
						lastWorldPosition = point;
						hoveredObject = ((Component)val3).gameObject;
						return true;
					}
				}
			}
			else
			{
				if (uICamera.eventType != EventType.UI_2D || !((Plane)(ref m2DPlane)).Raycast(val2, ref num2))
				{
					continue;
				}
				lastWorldPosition = ((Ray)(ref val2)).GetPoint(num2);
				Collider2D[] array2 = Physics2D.OverlapPointAll(Vector2.op_Implicit(lastWorldPosition), num);
				if (array2.Length > 1)
				{
					for (int l = 0; l < array2.Length; l++)
					{
						GameObject gameObject3 = ((Component)array2[l]).gameObject;
						UIWidget component3 = gameObject3.GetComponent<UIWidget>();
						if ((Object)(object)component3 != (Object)null)
						{
							if (!component3.isVisible || (component3.hitCheck != null && !component3.hitCheck(lastWorldPosition)))
							{
								continue;
							}
						}
						else
						{
							UIRect uIRect3 = NGUITools.FindInParents<UIRect>(gameObject3);
							if ((Object)(object)uIRect3 != (Object)null && uIRect3.finalAlpha < 0.001f)
							{
								continue;
							}
						}
						mHit.depth = NGUITools.CalculateRaycastDepth(gameObject3);
						if (mHit.depth != int.MaxValue)
						{
							mHit.go = gameObject3;
							mHit.point = lastWorldPosition;
							mHits.Add(mHit);
						}
					}
					mHits.Sort((DepthEntry r1, DepthEntry r2) => r2.depth.CompareTo(r1.depth));
					for (int m = 0; m < mHits.size; m++)
					{
						if (IsVisible(ref mHits.buffer[m]))
						{
							hoveredObject = mHits[m].go;
							mHits.Clear();
							return true;
						}
					}
					mHits.Clear();
				}
				else
				{
					if (array2.Length != 1)
					{
						continue;
					}
					GameObject gameObject4 = ((Component)array2[0]).gameObject;
					UIWidget component4 = gameObject4.GetComponent<UIWidget>();
					if ((Object)(object)component4 != (Object)null)
					{
						if (!component4.isVisible || (component4.hitCheck != null && !component4.hitCheck(lastWorldPosition)))
						{
							continue;
						}
					}
					else
					{
						UIRect uIRect4 = NGUITools.FindInParents<UIRect>(gameObject4);
						if ((Object)(object)uIRect4 != (Object)null && uIRect4.finalAlpha < 0.001f)
						{
							continue;
						}
					}
					if (IsVisible(lastWorldPosition, gameObject4))
					{
						hoveredObject = gameObject4;
						return true;
					}
				}
			}
		}
		return false;
	}

	private static bool IsVisible(Vector3 worldPoint, GameObject go)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(go);
		while ((Object)(object)uIPanel != (Object)null)
		{
			if (!uIPanel.IsVisible(worldPoint))
			{
				return false;
			}
			uIPanel = uIPanel.parentPanel;
		}
		return true;
	}

	private static bool IsVisible(ref DepthEntry de)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(de.go);
		while ((Object)(object)uIPanel != (Object)null)
		{
			if (!uIPanel.IsVisible(de.point))
			{
				return false;
			}
			uIPanel = uIPanel.parentPanel;
		}
		return true;
	}

	public static bool IsHighlighted(GameObject go)
	{
		if (currentScheme == ControlScheme.Mouse)
		{
			return (Object)(object)hoveredObject == (Object)(object)go;
		}
		if (currentScheme == ControlScheme.Controller)
		{
			return (Object)(object)selectedObject == (Object)(object)go;
		}
		return false;
	}

	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < list.size; i++)
		{
			UICamera uICamera = list.buffer[i];
			Camera val = uICamera.cachedCamera;
			if ((Object)(object)val != (Object)null && (val.cullingMask & num) != 0)
			{
				return uICamera;
			}
		}
		return null;
	}

	private static int GetDirection(KeyCode up, KeyCode down)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(up))
		{
			return 1;
		}
		if (Input.GetKeyDown(down))
		{
			return -1;
		}
		return 0;
	}

	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(up0) || Input.GetKeyDown(up1))
		{
			return 1;
		}
		if (Input.GetKeyDown(down0) || Input.GetKeyDown(down1))
		{
			return -1;
		}
		return 0;
	}

	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				mNextEvent = time + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (mNotifying)
		{
			return;
		}
		mNotifying = true;
		if (NGUITools.GetActive(go))
		{
			go.SendMessage(funcName, obj, (SendMessageOptions)1);
			if ((Object)(object)genericEventHandler != (Object)null && (Object)(object)genericEventHandler != (Object)(object)go)
			{
				genericEventHandler.SendMessage(funcName, obj, (SendMessageOptions)1);
			}
		}
		mNotifying = false;
	}

	public static MouseOrTouch GetMouse(int button)
	{
		return mMouse[button];
	}

	public static MouseOrTouch GetTouch(int id)
	{
		MouseOrTouch value = null;
		if (id < 0)
		{
			return GetMouse(-id - 1);
		}
		if (!mTouches.TryGetValue(id, out value))
		{
			value = new MouseOrTouch();
			value.touchBegan = true;
			mTouches.Add(id, value);
		}
		return value;
	}

	public static void RemoveTouch(int id)
	{
		mTouches.Remove(id);
	}

	private void Awake()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Invalid comparison between Unknown and I4
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Invalid comparison between Unknown and I4
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Invalid comparison between Unknown and I4
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Invalid comparison between Unknown and I4
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Invalid comparison between Unknown and I4
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Invalid comparison between Unknown and I4
		mWidth = Screen.width;
		mHeight = Screen.height;
		if ((int)Application.platform == 11 || (int)Application.platform == 8 || (int)Application.platform == 21 || (int)Application.platform == 22)
		{
			useMouse = false;
			useTouch = true;
			if ((int)Application.platform == 8)
			{
				useKeyboard = false;
				useController = false;
			}
		}
		else if ((int)Application.platform == 9 || (int)Application.platform == 10)
		{
			useMouse = false;
			useTouch = false;
			useKeyboard = false;
			useController = true;
		}
		mMouse[0].pos.x = Input.mousePosition.x;
		mMouse[0].pos.y = Input.mousePosition.y;
		for (int i = 1; i < 3; i++)
		{
			mMouse[i].pos = mMouse[0].pos;
			mMouse[i].lastPos = mMouse[0].pos;
		}
		lastTouchPosition = mMouse[0].pos;
	}

	private void OnEnable()
	{
		list.Add(this);
		list.Sort(CompareFunc);
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	private void Start()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Invalid comparison between Unknown and I4
		if (eventType != 0 && (int)cachedCamera.transparencySortMode != 2)
		{
			cachedCamera.transparencySortMode = (TransparencySortMode)2;
		}
		if (Application.isPlaying)
		{
			cachedCamera.eventMask = 0;
		}
		if (handlesEvents)
		{
			NGUIDebug.debugRaycast = debug;
		}
	}

	private void Update()
	{
		if (!handlesEvents)
		{
			return;
		}
		current = this;
		if (useTouch)
		{
			ProcessTouches();
		}
		else if (useMouse)
		{
			ProcessMouse();
		}
		if (onCustomInput != null)
		{
			onCustomInput();
		}
		if ((Object)(object)mCurrentSelection == (Object)null)
		{
			inputHasFocus = false;
		}
		if ((Object)(object)mCurrentSelection != (Object)null)
		{
			ProcessOthers();
		}
		if (useMouse && (Object)(object)mHover != (Object)null)
		{
			float num = ((!string.IsNullOrEmpty(scrollAxisName)) ? Input.GetAxis(scrollAxisName) : 0f);
			if (num != 0f)
			{
				Notify(mHover, "OnScroll", num);
			}
			if (showTooltips && mTooltipTime != 0f && (mTooltipTime < RealTime.time || Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303)))
			{
				mTooltip = mHover;
				ShowTooltip(val: true);
			}
		}
		current = null;
	}

	private void LateUpdate()
	{
		if (!handlesEvents)
		{
			return;
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != mWidth || height != mHeight)
		{
			mWidth = width;
			mHeight = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (onScreenResize != null)
			{
				onScreenResize();
			}
		}
	}

	public void ProcessMouse()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		lastTouchPosition = Vector2.op_Implicit(Input.mousePosition);
		mMouse[0].delta = lastTouchPosition - mMouse[0].pos;
		mMouse[0].pos = lastTouchPosition;
		bool flag = ((Vector2)(ref mMouse[0].delta)).sqrMagnitude > 0.001f;
		for (int i = 1; i < 3; i++)
		{
			mMouse[i].pos = mMouse[0].pos;
			mMouse[i].delta = mMouse[0].delta;
		}
		bool flag2 = false;
		bool flag3 = false;
		for (int j = 0; j < 3; j++)
		{
			if (Input.GetMouseButtonDown(j))
			{
				currentScheme = ControlScheme.Mouse;
				flag3 = true;
				flag2 = true;
			}
			else if (Input.GetMouseButton(j))
			{
				currentScheme = ControlScheme.Mouse;
				flag2 = true;
			}
		}
		if (flag2 || flag || mNextRaycast < RealTime.time)
		{
			mNextRaycast = RealTime.time + 0.02f;
			if (!Raycast(Input.mousePosition))
			{
				hoveredObject = fallThrough;
			}
			if ((Object)(object)hoveredObject == (Object)null)
			{
				hoveredObject = genericEventHandler;
			}
			for (int k = 0; k < 3; k++)
			{
				mMouse[k].current = hoveredObject;
			}
		}
		bool flag4 = (Object)(object)mMouse[0].last != (Object)(object)mMouse[0].current;
		if (flag4)
		{
			currentScheme = ControlScheme.Mouse;
		}
		if (flag2)
		{
			mTooltipTime = 0f;
		}
		else if (flag && (!stickyTooltip || flag4))
		{
			if (mTooltipTime != 0f)
			{
				mTooltipTime = RealTime.time + tooltipDelay;
			}
			else if ((Object)(object)mTooltip != (Object)null)
			{
				ShowTooltip(val: false);
			}
		}
		if ((flag3 || !flag2) && (Object)(object)mHover != (Object)null && flag4)
		{
			currentScheme = ControlScheme.Mouse;
			if ((Object)(object)mTooltip != (Object)null)
			{
				ShowTooltip(val: false);
			}
			Notify(mHover, "OnHover", false);
			mHover = null;
		}
		for (int l = 0; l < 3; l++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(l);
			bool mouseButtonUp = Input.GetMouseButtonUp(l);
			if (mouseButtonDown || mouseButtonUp)
			{
				currentScheme = ControlScheme.Mouse;
			}
			currentTouch = mMouse[l];
			currentTouchID = -1 - l;
			currentKey = (KeyCode)(323 + l);
			if (mouseButtonDown)
			{
				currentTouch.pressedCam = currentCamera;
			}
			else if ((Object)(object)currentTouch.pressed != (Object)null)
			{
				currentCamera = currentTouch.pressedCam;
			}
			ProcessTouch(mouseButtonDown, mouseButtonUp);
			currentKey = (KeyCode)0;
		}
		currentTouch = null;
		if (!flag2 && flag4)
		{
			currentScheme = ControlScheme.Mouse;
			mTooltipTime = RealTime.time + tooltipDelay;
			mHover = mMouse[0].current;
			Notify(mHover, "OnHover", true);
		}
		mMouse[0].last = mMouse[0].current;
		for (int m = 1; m < 3; m++)
		{
			mMouse[m].last = mMouse[0].last;
		}
	}

	public void ProcessTouches()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Invalid comparison between Unknown and I4
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Invalid comparison between Unknown and I4
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		currentScheme = ControlScheme.Touch;
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			currentTouchID = ((!allowMultiTouch) ? 1 : ((Touch)(ref touch)).fingerId);
			currentTouch = GetTouch(currentTouchID);
			bool flag = (int)((Touch)(ref touch)).phase == 0 || currentTouch.touchBegan;
			bool flag2 = (int)((Touch)(ref touch)).phase == 4 || (int)((Touch)(ref touch)).phase == 3;
			currentTouch.touchBegan = false;
			currentTouch.delta = (flag ? Vector2.zero : (((Touch)(ref touch)).position - currentTouch.pos));
			currentTouch.pos = ((Touch)(ref touch)).position;
			if (!Raycast(Vector2.op_Implicit(currentTouch.pos)))
			{
				hoveredObject = fallThrough;
			}
			if ((Object)(object)hoveredObject == (Object)null)
			{
				hoveredObject = genericEventHandler;
			}
			currentTouch.last = currentTouch.current;
			currentTouch.current = hoveredObject;
			lastTouchPosition = currentTouch.pos;
			if (flag)
			{
				currentTouch.pressedCam = currentCamera;
			}
			else if ((Object)(object)currentTouch.pressed != (Object)null)
			{
				currentCamera = currentTouch.pressedCam;
			}
			if (((Touch)(ref touch)).tapCount > 1)
			{
				currentTouch.clickTime = RealTime.time;
			}
			ProcessTouch(flag, flag2);
			if (flag2)
			{
				RemoveTouch(currentTouchID);
			}
			currentTouch.last = null;
			currentTouch = null;
			if (!allowMultiTouch)
			{
				break;
			}
		}
		if (Input.touchCount == 0 && useMouse)
		{
			ProcessMouse();
		}
	}

	private void ProcessFakeTouches()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			currentTouchID = 1;
			currentTouch = mMouse[0];
			currentTouch.touchBegan = mouseButtonDown;
			Vector2 val = Vector2.op_Implicit(Input.mousePosition);
			currentTouch.delta = (mouseButtonDown ? Vector2.zero : (val - currentTouch.pos));
			currentTouch.pos = val;
			if (!Raycast(Vector2.op_Implicit(currentTouch.pos)))
			{
				hoveredObject = fallThrough;
			}
			if ((Object)(object)hoveredObject == (Object)null)
			{
				hoveredObject = genericEventHandler;
			}
			currentTouch.last = currentTouch.current;
			currentTouch.current = hoveredObject;
			lastTouchPosition = currentTouch.pos;
			if (mouseButtonDown)
			{
				currentTouch.pressedCam = currentCamera;
			}
			else if ((Object)(object)currentTouch.pressed != (Object)null)
			{
				currentCamera = currentTouch.pressedCam;
			}
			ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				RemoveTouch(currentTouchID);
			}
			currentTouch.last = null;
			currentTouch = null;
		}
	}

	public void ProcessOthers()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		currentTouchID = -100;
		currentTouch = controller;
		bool flag = false;
		bool flag2 = false;
		if ((int)submitKey0 != 0 && Input.GetKeyDown(submitKey0))
		{
			currentKey = submitKey0;
			flag = true;
		}
		if ((int)submitKey1 != 0 && Input.GetKeyDown(submitKey1))
		{
			currentKey = submitKey1;
			flag = true;
		}
		if ((int)submitKey0 != 0 && Input.GetKeyUp(submitKey0))
		{
			currentKey = submitKey0;
			flag2 = true;
		}
		if ((int)submitKey1 != 0 && Input.GetKeyUp(submitKey1))
		{
			currentKey = submitKey1;
			flag2 = true;
		}
		if (flag || flag2)
		{
			currentScheme = ControlScheme.Controller;
			currentTouch.last = currentTouch.current;
			currentTouch.current = mCurrentSelection;
			ProcessTouch(flag, flag2);
			currentTouch.last = null;
		}
		int num = 0;
		int num2 = 0;
		if (useKeyboard)
		{
			if (inputHasFocus)
			{
				num += GetDirection((KeyCode)273, (KeyCode)274);
				num2 += GetDirection((KeyCode)275, (KeyCode)276);
			}
			else
			{
				num += GetDirection((KeyCode)119, (KeyCode)273, (KeyCode)115, (KeyCode)274);
				num2 += GetDirection((KeyCode)100, (KeyCode)275, (KeyCode)97, (KeyCode)276);
			}
		}
		if (useController)
		{
			if (!string.IsNullOrEmpty(verticalAxisName))
			{
				num += GetDirection(verticalAxisName);
			}
			if (!string.IsNullOrEmpty(horizontalAxisName))
			{
				num2 += GetDirection(horizontalAxisName);
			}
		}
		if (num != 0)
		{
			currentScheme = ControlScheme.Controller;
			Notify(mCurrentSelection, "OnKey", (object)(KeyCode)((num > 0) ? 273 : 274));
		}
		if (num2 != 0)
		{
			currentScheme = ControlScheme.Controller;
			Notify(mCurrentSelection, "OnKey", (object)(KeyCode)((num2 > 0) ? 275 : 276));
		}
		currentTouch = null;
		currentKey = (KeyCode)0;
	}

	public void ProcessTouch(bool pressed, bool unpressed)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		bool flag = currentScheme == ControlScheme.Mouse;
		float num = (flag ? mouseDragThreshold : touchDragThreshold);
		float num2 = (flag ? mouseClickThreshold : touchClickThreshold);
		num *= num;
		num2 *= num2;
		if (pressed)
		{
			if ((Object)(object)mTooltip != (Object)null)
			{
				ShowTooltip(val: false);
			}
			currentTouch.pressStarted = true;
			Notify(currentTouch.pressed, "OnPress", false);
			currentTouch.pressed = currentTouch.current;
			currentTouch.dragged = currentTouch.current;
			currentTouch.clickNotification = ClickNotification.BasedOnDelta;
			currentTouch.totalDelta = Vector2.zero;
			currentTouch.dragStarted = false;
			Notify(currentTouch.pressed, "OnPress", true);
			if ((Object)(object)currentTouch.pressed != (Object)(object)mCurrentSelection)
			{
				if ((Object)(object)mTooltip != (Object)null)
				{
					ShowTooltip(val: false);
				}
				currentScheme = ControlScheme.Touch;
				selectedObject = currentTouch.pressed;
			}
		}
		else if ((Object)(object)currentTouch.pressed != (Object)null && (((Vector2)(ref currentTouch.delta)).sqrMagnitude != 0f || (Object)(object)currentTouch.current != (Object)(object)currentTouch.last))
		{
			MouseOrTouch mouseOrTouch = currentTouch;
			mouseOrTouch.totalDelta += currentTouch.delta;
			float sqrMagnitude = ((Vector2)(ref currentTouch.totalDelta)).sqrMagnitude;
			bool flag2 = false;
			if (!currentTouch.dragStarted && (Object)(object)currentTouch.last != (Object)(object)currentTouch.current)
			{
				currentTouch.dragStarted = true;
				currentTouch.delta = currentTouch.totalDelta;
				isDragging = true;
				Notify(currentTouch.dragged, "OnDragStart", null);
				Notify(currentTouch.last, "OnDragOver", currentTouch.dragged);
				isDragging = false;
			}
			else if (!currentTouch.dragStarted && num < sqrMagnitude)
			{
				flag2 = true;
				currentTouch.dragStarted = true;
				currentTouch.delta = currentTouch.totalDelta;
			}
			if (currentTouch.dragStarted)
			{
				if ((Object)(object)mTooltip != (Object)null)
				{
					ShowTooltip(val: false);
				}
				isDragging = true;
				bool num3 = currentTouch.clickNotification == ClickNotification.None;
				if (flag2)
				{
					Notify(currentTouch.dragged, "OnDragStart", null);
					Notify(currentTouch.current, "OnDragOver", currentTouch.dragged);
				}
				else if ((Object)(object)currentTouch.last != (Object)(object)currentTouch.current)
				{
					Notify(currentTouch.last, "OnDragOut", currentTouch.dragged);
					Notify(currentTouch.current, "OnDragOver", currentTouch.dragged);
				}
				Notify(currentTouch.dragged, "OnDrag", currentTouch.delta);
				currentTouch.last = currentTouch.current;
				isDragging = false;
				if (num3)
				{
					currentTouch.clickNotification = ClickNotification.None;
				}
				else if (currentTouch.clickNotification == ClickNotification.BasedOnDelta && num2 < sqrMagnitude)
				{
					currentTouch.clickNotification = ClickNotification.None;
				}
			}
		}
		if (!unpressed)
		{
			return;
		}
		currentTouch.pressStarted = false;
		if ((Object)(object)mTooltip != (Object)null)
		{
			ShowTooltip(val: false);
		}
		if ((Object)(object)currentTouch.pressed != (Object)null)
		{
			if (currentTouch.dragStarted)
			{
				Notify(currentTouch.last, "OnDragOut", currentTouch.dragged);
				Notify(currentTouch.dragged, "OnDragEnd", null);
			}
			Notify(currentTouch.pressed, "OnPress", false);
			if (flag)
			{
				Notify(currentTouch.current, "OnHover", true);
			}
			mHover = currentTouch.current;
			if ((Object)(object)currentTouch.dragged == (Object)(object)currentTouch.current || (currentScheme != ControlScheme.Controller && currentTouch.clickNotification != 0 && ((Vector2)(ref currentTouch.totalDelta)).sqrMagnitude < num))
			{
				if ((Object)(object)currentTouch.pressed != (Object)(object)mCurrentSelection)
				{
					mNextSelection = null;
					mCurrentSelection = currentTouch.pressed;
					Notify(currentTouch.pressed, "OnSelect", true);
				}
				else
				{
					mNextSelection = null;
					mCurrentSelection = currentTouch.pressed;
				}
				if (currentTouch.clickNotification != 0 && (Object)(object)currentTouch.pressed == (Object)(object)currentTouch.current)
				{
					float time = RealTime.time;
					Notify(currentTouch.pressed, "OnClick", null);
					if (currentTouch.clickTime + 0.35f > time)
					{
						Notify(currentTouch.pressed, "OnDoubleClick", null);
					}
					currentTouch.clickTime = time;
				}
			}
			else if (currentTouch.dragStarted)
			{
				Notify(currentTouch.current, "OnDrop", currentTouch.dragged);
			}
		}
		currentTouch.dragStarted = false;
		currentTouch.pressed = null;
		currentTouch.dragged = null;
	}

	public void ShowTooltip(bool val)
	{
		mTooltipTime = 0f;
		Notify(mTooltip, "OnTooltip", val);
		if (!val)
		{
			mTooltip = null;
		}
	}

	private void OnApplicationPause()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		MouseOrTouch mouseOrTouch = currentTouch;
		if (useTouch)
		{
			BetterList<int> betterList = new BetterList<int>();
			foreach (KeyValuePair<int, MouseOrTouch> mTouch in mTouches)
			{
				if (mTouch.Value != null && Object.op_Implicit((Object)(object)mTouch.Value.pressed))
				{
					currentTouch = mTouch.Value;
					currentTouchID = mTouch.Key;
					currentScheme = ControlScheme.Touch;
					currentTouch.clickNotification = ClickNotification.None;
					ProcessTouch(pressed: false, unpressed: true);
					betterList.Add(currentTouchID);
				}
			}
			for (int i = 0; i < betterList.size; i++)
			{
				RemoveTouch(betterList[i]);
			}
		}
		if (useMouse)
		{
			for (int j = 0; j < 3; j++)
			{
				if (Object.op_Implicit((Object)(object)mMouse[j].pressed))
				{
					currentTouch = mMouse[j];
					currentTouchID = -1 - j;
					currentKey = (KeyCode)(323 + j);
					currentScheme = ControlScheme.Mouse;
					currentTouch.clickNotification = ClickNotification.None;
					ProcessTouch(pressed: false, unpressed: true);
				}
			}
		}
		if (useController && Object.op_Implicit((Object)(object)controller.pressed))
		{
			currentTouch = controller;
			currentTouchID = -100;
			currentScheme = ControlScheme.Controller;
			currentTouch.last = currentTouch.current;
			currentTouch.current = mCurrentSelection;
			currentTouch.clickNotification = ClickNotification.None;
			ProcessTouch(pressed: false, unpressed: true);
			currentTouch.last = null;
		}
		currentTouch = mouseOrTouch;
	}
}
