using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A6 RID: 166
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x17000143 RID: 323
	// (get) Token: 0x060008ED RID: 2285 RVA: 0x00024C5F File Offset: 0x00022E5F
	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x060008EE RID: 2286 RVA: 0x00034E88 File Offset: 0x00033088
	public static Ray currentRay
	{
		get
		{
			if (!(UICamera.currentCamera != null) || UICamera.currentTouch == null)
			{
				return default(Ray);
			}
			return UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x060008EF RID: 2287 RVA: 0x00034ECC File Offset: 0x000330CC
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00034ED9 File Offset: 0x000330D9
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00034EFB File Offset: 0x000330FB
	// (set) Token: 0x060008F2 RID: 2290 RVA: 0x00034F02 File Offset: 0x00033102
	public static GameObject selectedObject
	{
		get
		{
			return UICamera.mCurrentSelection;
		}
		set
		{
			UICamera.SetSelection(value, UICamera.currentScheme);
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00034F10 File Offset: 0x00033110
	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if (UICamera.mMouse[i].pressed == go)
			{
				return true;
			}
		}
		foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
		{
			if (keyValuePair.Value.pressed == go)
			{
				return true;
			}
		}
		return UICamera.controller.pressed == go;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00034FAC File Offset: 0x000331AC
	protected static void SetSelection(GameObject go, UICamera.ControlScheme scheme)
	{
		if (UICamera.mNextSelection != null)
		{
			UICamera.mNextSelection = go;
			return;
		}
		if (UICamera.mCurrentSelection != go)
		{
			UICamera.mNextSelection = go;
			UICamera.mNextScheme = scheme;
			if (UICamera.list.size > 0)
			{
				UICamera uicamera = (UICamera.mNextSelection != null) ? UICamera.FindCameraForLayer(UICamera.mNextSelection.layer) : UICamera.list[0];
				if (uicamera != null)
				{
					uicamera.StartCoroutine(uicamera.ChangeSelection());
				}
			}
		}
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00035033 File Offset: 0x00033233
	private IEnumerator ChangeSelection()
	{
		yield return new WaitForEndOfFrame();
		UICamera.Notify(UICamera.mCurrentSelection, "OnSelect", false);
		UICamera.mCurrentSelection = UICamera.mNextSelection;
		UICamera.mNextSelection = null;
		if (UICamera.mCurrentSelection != null)
		{
			UICamera.current = this;
			UICamera.currentCamera = this.mCam;
			UICamera.currentScheme = UICamera.mNextScheme;
			UICamera.inputHasFocus = (UICamera.mCurrentSelection.GetComponent<UIInput>() != null);
			UICamera.Notify(UICamera.mCurrentSelection, "OnSelect", true);
			UICamera.current = null;
		}
		else
		{
			UICamera.inputHasFocus = false;
		}
		yield break;
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00035044 File Offset: 0x00033244
	public static int touchCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value.pressed != null)
				{
					num++;
				}
			}
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].pressed != null)
				{
					num++;
				}
			}
			if (UICamera.controller.pressed != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000350EC File Offset: 0x000332EC
	public static int dragCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value.dragged != null)
				{
					num++;
				}
			}
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].dragged != null)
				{
					num++;
				}
			}
			if (UICamera.controller.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00035194 File Offset: 0x00033394
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			if (!(eventHandler != null))
			{
				return null;
			}
			return eventHandler.cachedCamera;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000351B8 File Offset: 0x000333B8
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.list.size; i++)
			{
				UICamera uicamera = UICamera.list.buffer[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00035208 File Offset: 0x00033408
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

	// Token: 0x060008FB RID: 2299 RVA: 0x00035240 File Offset: 0x00033440
	public static bool Raycast(Vector3 inPos)
	{
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
				if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y) && vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
				{
					Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
					int num = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
					float num2 = (uicamera.rangeDistance > 0f) ? uicamera.rangeDistance : (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane);
					if (uicamera.eventType == UICamera.EventType.World_3D)
					{
						if (Physics.Raycast(ray, ref UICamera.lastHit, num2, num))
						{
							UICamera.lastWorldPosition = UICamera.lastHit.point;
							UICamera.hoveredObject = UICamera.lastHit.collider.gameObject;
							return true;
						}
					}
					else if (uicamera.eventType == UICamera.EventType.UI_3D)
					{
						RaycastHit[] array = Physics.RaycastAll(ray, num2, num);
						if (array.Length > 1)
						{
							int j = 0;
							while (j < array.Length)
							{
								GameObject gameObject = array[j].collider.gameObject;
								UIWidget component = gameObject.GetComponent<UIWidget>();
								if (component != null)
								{
									if (component.isVisible)
									{
										if (component.hitCheck == null || component.hitCheck(array[j].point))
										{
											goto IL_1E0;
										}
									}
								}
								else
								{
									UIRect uirect = NGUITools.FindInParents<UIRect>(gameObject);
									if (!(uirect != null) || uirect.finalAlpha >= 0.001f)
									{
										goto IL_1E0;
									}
								}
								IL_259:
								j++;
								continue;
								IL_1E0:
								UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
								if (UICamera.mHit.depth != 2147483647)
								{
									UICamera.mHit.hit = array[j];
									UICamera.mHit.point = array[j].point;
									UICamera.mHit.go = array[j].collider.gameObject;
									UICamera.mHits.Add(UICamera.mHit);
									goto IL_259;
								}
								goto IL_259;
							}
							UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
							for (int k = 0; k < UICamera.mHits.size; k++)
							{
								if (UICamera.IsVisible(ref UICamera.mHits.buffer[k]))
								{
									UICamera.lastHit = UICamera.mHits[k].hit;
									UICamera.hoveredObject = UICamera.mHits[k].go;
									UICamera.lastWorldPosition = UICamera.mHits[k].point;
									UICamera.mHits.Clear();
									return true;
								}
							}
							UICamera.mHits.Clear();
						}
						else if (array.Length == 1)
						{
							GameObject gameObject2 = array[0].collider.gameObject;
							UIWidget component2 = gameObject2.GetComponent<UIWidget>();
							if (component2 != null)
							{
								if (!component2.isVisible)
								{
									goto IL_687;
								}
								if (component2.hitCheck != null && !component2.hitCheck(array[0].point))
								{
									goto IL_687;
								}
							}
							else
							{
								UIRect uirect2 = NGUITools.FindInParents<UIRect>(gameObject2);
								if (uirect2 != null && uirect2.finalAlpha < 0.001f)
								{
									goto IL_687;
								}
							}
							if (UICamera.IsVisible(array[0].point, array[0].collider.gameObject))
							{
								UICamera.lastHit = array[0];
								UICamera.lastWorldPosition = array[0].point;
								UICamera.hoveredObject = UICamera.lastHit.collider.gameObject;
								return true;
							}
						}
					}
					else if (uicamera.eventType == UICamera.EventType.World_2D)
					{
						if (UICamera.m2DPlane.Raycast(ray, ref num2))
						{
							Vector3 point = ray.GetPoint(num2);
							Collider2D collider2D = Physics2D.OverlapPoint(point, num);
							if (collider2D)
							{
								UICamera.lastWorldPosition = point;
								UICamera.hoveredObject = collider2D.gameObject;
								return true;
							}
						}
					}
					else if (uicamera.eventType == UICamera.EventType.UI_2D && UICamera.m2DPlane.Raycast(ray, ref num2))
					{
						UICamera.lastWorldPosition = ray.GetPoint(num2);
						Collider2D[] array2 = Physics2D.OverlapPointAll(UICamera.lastWorldPosition, num);
						if (array2.Length > 1)
						{
							int l = 0;
							while (l < array2.Length)
							{
								GameObject gameObject3 = array2[l].gameObject;
								UIWidget component3 = gameObject3.GetComponent<UIWidget>();
								if (component3 != null)
								{
									if (component3.isVisible)
									{
										if (component3.hitCheck == null || component3.hitCheck(UICamera.lastWorldPosition))
										{
											goto IL_51B;
										}
									}
								}
								else
								{
									UIRect uirect3 = NGUITools.FindInParents<UIRect>(gameObject3);
									if (!(uirect3 != null) || uirect3.finalAlpha >= 0.001f)
									{
										goto IL_51B;
									}
								}
								IL_567:
								l++;
								continue;
								IL_51B:
								UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject3);
								if (UICamera.mHit.depth != 2147483647)
								{
									UICamera.mHit.go = gameObject3;
									UICamera.mHit.point = UICamera.lastWorldPosition;
									UICamera.mHits.Add(UICamera.mHit);
									goto IL_567;
								}
								goto IL_567;
							}
							UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
							for (int m = 0; m < UICamera.mHits.size; m++)
							{
								if (UICamera.IsVisible(ref UICamera.mHits.buffer[m]))
								{
									UICamera.hoveredObject = UICamera.mHits[m].go;
									UICamera.mHits.Clear();
									return true;
								}
							}
							UICamera.mHits.Clear();
						}
						else if (array2.Length == 1)
						{
							GameObject gameObject4 = array2[0].gameObject;
							UIWidget component4 = gameObject4.GetComponent<UIWidget>();
							if (component4 != null)
							{
								if (!component4.isVisible)
								{
									goto IL_687;
								}
								if (component4.hitCheck != null && !component4.hitCheck(UICamera.lastWorldPosition))
								{
									goto IL_687;
								}
							}
							else
							{
								UIRect uirect4 = NGUITools.FindInParents<UIRect>(gameObject4);
								if (uirect4 != null && uirect4.finalAlpha < 0.001f)
								{
									goto IL_687;
								}
							}
							if (UICamera.IsVisible(UICamera.lastWorldPosition, gameObject4))
							{
								UICamera.hoveredObject = gameObject4;
								return true;
							}
						}
					}
				}
			}
			IL_687:;
		}
		return false;
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000358EC File Offset: 0x00033AEC
	private static bool IsVisible(Vector3 worldPoint, GameObject go)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(worldPoint))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00035920 File Offset: 0x00033B20
	private static bool IsVisible(ref UICamera.DepthEntry de)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(de.go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(de.point))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0003595C File Offset: 0x00033B5C
	public static bool IsHighlighted(GameObject go)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Mouse)
		{
			return UICamera.hoveredObject == go;
		}
		return UICamera.currentScheme == UICamera.ControlScheme.Controller && UICamera.selectedObject == go;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00035988 File Offset: 0x00033B88
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x000359DB File Offset: 0x00033BDB
	private static int GetDirection(KeyCode up, KeyCode down)
	{
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

	// Token: 0x06000901 RID: 2305 RVA: 0x000359F2 File Offset: 0x00033BF2
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
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

	// Token: 0x06000902 RID: 2306 RVA: 0x00035A1C File Offset: 0x00033C1C
	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (UICamera.mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				UICamera.mNextEvent = time + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				UICamera.mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00035A74 File Offset: 0x00033C74
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (UICamera.mNotifying)
		{
			return;
		}
		UICamera.mNotifying = true;
		if (NGUITools.GetActive(go))
		{
			go.SendMessage(funcName, obj, 1);
			if (UICamera.genericEventHandler != null && UICamera.genericEventHandler != go)
			{
				UICamera.genericEventHandler.SendMessage(funcName, obj, 1);
			}
		}
		UICamera.mNotifying = false;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00035ACD File Offset: 0x00033CCD
	public static UICamera.MouseOrTouch GetMouse(int button)
	{
		return UICamera.mMouse[button];
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00035AD8 File Offset: 0x00033CD8
	public static UICamera.MouseOrTouch GetTouch(int id)
	{
		UICamera.MouseOrTouch mouseOrTouch = null;
		if (id < 0)
		{
			return UICamera.GetMouse(-id - 1);
		}
		if (!UICamera.mTouches.TryGetValue(id, out mouseOrTouch))
		{
			mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.touchBegan = true;
			UICamera.mTouches.Add(id, mouseOrTouch);
		}
		return mouseOrTouch;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00035B1E File Offset: 0x00033D1E
	public static void RemoveTouch(int id)
	{
		UICamera.mTouches.Remove(id);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00035B2C File Offset: 0x00033D2C
	private void Awake()
	{
		UICamera.mWidth = Screen.width;
		UICamera.mHeight = Screen.height;
		if (Application.platform == 11 || Application.platform == 8 || Application.platform == 21 || Application.platform == 22)
		{
			this.useMouse = false;
			this.useTouch = true;
			if (Application.platform == 8)
			{
				this.useKeyboard = false;
				this.useController = false;
			}
		}
		else if (Application.platform == 9 || Application.platform == 10)
		{
			this.useMouse = false;
			this.useTouch = false;
			this.useKeyboard = false;
			this.useController = true;
		}
		UICamera.mMouse[0].pos.x = Input.mousePosition.x;
		UICamera.mMouse[0].pos.y = Input.mousePosition.y;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].lastPos = UICamera.mMouse[0].pos;
		}
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00035C47 File Offset: 0x00033E47
	private void OnEnable()
	{
		UICamera.list.Add(this);
		UICamera.list.Sort(new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc));
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00035C6A File Offset: 0x00033E6A
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00035C78 File Offset: 0x00033E78
	private void Start()
	{
		if (this.eventType != UICamera.EventType.World_3D && this.cachedCamera.transparencySortMode != 2)
		{
			this.cachedCamera.transparencySortMode = 2;
		}
		if (Application.isPlaying)
		{
			this.cachedCamera.eventMask = 0;
		}
		if (this.handlesEvents)
		{
			NGUIDebug.debugRaycast = this.debug;
		}
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00035CD0 File Offset: 0x00033ED0
	private void Update()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		UICamera.current = this;
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		else if (this.useMouse)
		{
			this.ProcessMouse();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if (UICamera.mCurrentSelection == null)
		{
			UICamera.inputHasFocus = false;
		}
		if (UICamera.mCurrentSelection != null)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float num = (!string.IsNullOrEmpty(this.scrollAxisName)) ? Input.GetAxis(this.scrollAxisName) : 0f;
			if (num != 0f)
			{
				UICamera.Notify(UICamera.mHover, "OnScroll", num);
			}
			if (UICamera.showTooltips && this.mTooltipTime != 0f && (this.mTooltipTime < RealTime.time || Input.GetKey(304) || Input.GetKey(303)))
			{
				this.mTooltip = UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
		UICamera.current = null;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00035DEC File Offset: 0x00033FEC
	private void LateUpdate()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != UICamera.mWidth || height != UICamera.mHeight)
		{
			UICamera.mWidth = width;
			UICamera.mHeight = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (UICamera.onScreenResize != null)
			{
				UICamera.onScreenResize();
			}
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00035E48 File Offset: 0x00034048
	public void ProcessMouse()
	{
		UICamera.lastTouchPosition = Input.mousePosition;
		UICamera.mMouse[0].delta = UICamera.lastTouchPosition - UICamera.mMouse[0].pos;
		UICamera.mMouse[0].pos = UICamera.lastTouchPosition;
		bool flag = UICamera.mMouse[0].delta.sqrMagnitude > 0.001f;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].delta = UICamera.mMouse[0].delta;
		}
		bool flag2 = false;
		bool flag3 = false;
		for (int j = 0; j < 3; j++)
		{
			if (Input.GetMouseButtonDown(j))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Mouse;
				flag3 = true;
				flag2 = true;
			}
			else if (Input.GetMouseButton(j))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Mouse;
				flag2 = true;
			}
		}
		if (flag2 || flag || this.mNextRaycast < RealTime.time)
		{
			this.mNextRaycast = RealTime.time + 0.02f;
			if (!UICamera.Raycast(Input.mousePosition))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			for (int k = 0; k < 3; k++)
			{
				UICamera.mMouse[k].current = UICamera.hoveredObject;
			}
		}
		bool flag4 = UICamera.mMouse[0].last != UICamera.mMouse[0].current;
		if (flag4)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
		}
		if (flag2)
		{
			this.mTooltipTime = 0f;
		}
		else if (flag && (!this.stickyTooltip || flag4))
		{
			if (this.mTooltipTime != 0f)
			{
				this.mTooltipTime = RealTime.time + this.tooltipDelay;
			}
			else if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
		}
		if ((flag3 || !flag2) && UICamera.mHover != null && flag4)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.Notify(UICamera.mHover, "OnHover", false);
			UICamera.mHover = null;
		}
		for (int l = 0; l < 3; l++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(l);
			bool mouseButtonUp = Input.GetMouseButtonUp(l);
			if (mouseButtonDown || mouseButtonUp)
			{
				UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			}
			UICamera.currentTouch = UICamera.mMouse[l];
			UICamera.currentTouchID = -1 - l;
			UICamera.currentKey = 323 + l;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			UICamera.currentKey = 0;
		}
		UICamera.currentTouch = null;
		if (!flag2 && flag4)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			this.mTooltipTime = RealTime.time + this.tooltipDelay;
			UICamera.mHover = UICamera.mMouse[0].current;
			UICamera.Notify(UICamera.mHover, "OnHover", true);
		}
		UICamera.mMouse[0].last = UICamera.mMouse[0].current;
		for (int m = 1; m < 3; m++)
		{
			UICamera.mMouse[m].last = UICamera.mMouse[0].last;
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00036194 File Offset: 0x00034394
	public void ProcessTouches()
	{
		UICamera.currentScheme = UICamera.ControlScheme.Touch;
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			UICamera.currentTouchID = (this.allowMultiTouch ? touch.fingerId : 1);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID);
			bool flag = touch.phase == null || UICamera.currentTouch.touchBegan;
			bool flag2 = touch.phase == 4 || touch.phase == 3;
			UICamera.currentTouch.touchBegan = false;
			UICamera.currentTouch.delta = (flag ? Vector2.zero : (touch.position - UICamera.currentTouch.pos));
			UICamera.currentTouch.pos = touch.position;
			if (!UICamera.Raycast(UICamera.currentTouch.pos))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.hoveredObject;
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (touch.tapCount > 1)
			{
				UICamera.currentTouch.clickTime = RealTime.time;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
		if (Input.touchCount == 0 && this.useMouse)
		{
			this.ProcessMouse();
		}
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0003635C File Offset: 0x0003455C
	private void ProcessFakeTouches()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			UICamera.currentTouchID = 1;
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.currentTouch.touchBegan = mouseButtonDown;
			Vector2 vector = Input.mousePosition;
			UICamera.currentTouch.delta = (mouseButtonDown ? Vector2.zero : (vector - UICamera.currentTouch.pos));
			UICamera.currentTouch.pos = vector;
			if (!UICamera.Raycast(UICamera.currentTouch.pos))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.hoveredObject;
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x000364A4 File Offset: 0x000346A4
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.controller;
		bool flag = false;
		bool flag2 = false;
		if (this.submitKey0 != null && Input.GetKeyDown(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		if (this.submitKey1 != null && Input.GetKeyDown(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag = true;
		}
		if (this.submitKey0 != null && Input.GetKeyUp(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		if (this.submitKey1 != null && Input.GetKeyUp(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag2 = true;
		}
		if (flag || flag2)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.mCurrentSelection;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.last = null;
		}
		int num = 0;
		int num2 = 0;
		if (this.useKeyboard)
		{
			if (UICamera.inputHasFocus)
			{
				num += UICamera.GetDirection(273, 274);
				num2 += UICamera.GetDirection(275, 276);
			}
			else
			{
				num += UICamera.GetDirection(119, 273, 115, 274);
				num2 += UICamera.GetDirection(100, 275, 97, 276);
			}
		}
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", (num > 0) ? 273 : 274);
		}
		if (num2 != 0)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", (num2 > 0) ? 275 : 276);
		}
		UICamera.currentTouch = null;
		UICamera.currentKey = 0;
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00036698 File Offset: 0x00034898
	public void ProcessTouch(bool pressed, bool unpressed)
	{
		bool flag = UICamera.currentScheme == UICamera.ControlScheme.Mouse;
		float num = flag ? this.mouseDragThreshold : this.touchDragThreshold;
		float num2 = flag ? this.mouseClickThreshold : this.touchClickThreshold;
		num *= num;
		num2 *= num2;
		if (pressed)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.currentTouch.pressStarted = true;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.currentTouch.pressed != UICamera.mCurrentSelection)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.currentScheme = UICamera.ControlScheme.Touch;
				UICamera.selectedObject = UICamera.currentTouch.pressed;
			}
		}
		else if (UICamera.currentTouch.pressed != null && (UICamera.currentTouch.delta.sqrMagnitude != 0f || UICamera.currentTouch.current != UICamera.currentTouch.last))
		{
			UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
			float sqrMagnitude = UICamera.currentTouch.totalDelta.sqrMagnitude;
			bool flag2 = false;
			if (!UICamera.currentTouch.dragStarted && UICamera.currentTouch.last != UICamera.currentTouch.current)
			{
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
				UICamera.isDragging = true;
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOver", UICamera.currentTouch.dragged);
				UICamera.isDragging = false;
			}
			else if (!UICamera.currentTouch.dragStarted && num < sqrMagnitude)
			{
				flag2 = true;
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
			}
			if (UICamera.currentTouch.dragStarted)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.isDragging = true;
				bool flag3 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
				if (flag2)
				{
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				else if (UICamera.currentTouch.last != UICamera.currentTouch.current)
				{
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
				UICamera.currentTouch.last = UICamera.currentTouch.current;
				UICamera.isDragging = false;
				if (flag3)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
				else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && num2 < sqrMagnitude)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
			}
		}
		if (unpressed)
		{
			UICamera.currentTouch.pressStarted = false;
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			if (UICamera.currentTouch.pressed != null)
			{
				if (UICamera.currentTouch.dragStarted)
				{
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragEnd", null);
				}
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
				if (flag)
				{
					UICamera.Notify(UICamera.currentTouch.current, "OnHover", true);
				}
				UICamera.mHover = UICamera.currentTouch.current;
				if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentScheme != UICamera.ControlScheme.Controller && UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.sqrMagnitude < num))
				{
					if (UICamera.currentTouch.pressed != UICamera.mCurrentSelection)
					{
						UICamera.mNextSelection = null;
						UICamera.mCurrentSelection = UICamera.currentTouch.pressed;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnSelect", true);
					}
					else
					{
						UICamera.mNextSelection = null;
						UICamera.mCurrentSelection = UICamera.currentTouch.pressed;
					}
					if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.pressed == UICamera.currentTouch.current)
					{
						float time = RealTime.time;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
						if (UICamera.currentTouch.clickTime + 0.35f > time)
						{
							UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
						}
						UICamera.currentTouch.clickTime = time;
					}
				}
				else if (UICamera.currentTouch.dragStarted)
				{
					UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
				}
			}
			UICamera.currentTouch.dragStarted = false;
			UICamera.currentTouch.pressed = null;
			UICamera.currentTouch.dragged = null;
		}
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00036C5A File Offset: 0x00034E5A
	public void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		UICamera.Notify(this.mTooltip, "OnTooltip", val);
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00036C88 File Offset: 0x00034E88
	private void OnApplicationPause()
	{
		UICamera.MouseOrTouch mouseOrTouch = UICamera.currentTouch;
		if (this.useTouch)
		{
			BetterList<int> betterList = new BetterList<int>();
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.pressed)
				{
					UICamera.currentTouch = keyValuePair.Value;
					UICamera.currentTouchID = keyValuePair.Key;
					UICamera.currentScheme = UICamera.ControlScheme.Touch;
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
					this.ProcessTouch(false, true);
					betterList.Add(UICamera.currentTouchID);
				}
			}
			for (int i = 0; i < betterList.size; i++)
			{
				UICamera.RemoveTouch(betterList[i]);
			}
		}
		if (this.useMouse)
		{
			for (int j = 0; j < 3; j++)
			{
				if (UICamera.mMouse[j].pressed)
				{
					UICamera.currentTouch = UICamera.mMouse[j];
					UICamera.currentTouchID = -1 - j;
					UICamera.currentKey = 323 + j;
					UICamera.currentScheme = UICamera.ControlScheme.Mouse;
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
					this.ProcessTouch(false, true);
				}
			}
		}
		if (this.useController && UICamera.controller.pressed)
		{
			UICamera.currentTouch = UICamera.controller;
			UICamera.currentTouchID = -100;
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.mCurrentSelection;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			this.ProcessTouch(false, true);
			UICamera.currentTouch.last = null;
		}
		UICamera.currentTouch = mouseOrTouch;
	}

	// Token: 0x0400057B RID: 1403
	public static BetterList<UICamera> list = new BetterList<UICamera>();

	// Token: 0x0400057C RID: 1404
	public static UICamera.OnScreenResize onScreenResize;

	// Token: 0x0400057D RID: 1405
	public UICamera.EventType eventType = UICamera.EventType.UI_3D;

	// Token: 0x0400057E RID: 1406
	public LayerMask eventReceiverMask = -1;

	// Token: 0x0400057F RID: 1407
	public bool debug;

	// Token: 0x04000580 RID: 1408
	public bool useMouse = true;

	// Token: 0x04000581 RID: 1409
	public bool useTouch = true;

	// Token: 0x04000582 RID: 1410
	public bool allowMultiTouch = true;

	// Token: 0x04000583 RID: 1411
	public bool useKeyboard = true;

	// Token: 0x04000584 RID: 1412
	public bool useController = true;

	// Token: 0x04000585 RID: 1413
	public bool stickyTooltip = true;

	// Token: 0x04000586 RID: 1414
	public float tooltipDelay = 1f;

	// Token: 0x04000587 RID: 1415
	public float mouseDragThreshold = 4f;

	// Token: 0x04000588 RID: 1416
	public float mouseClickThreshold = 10f;

	// Token: 0x04000589 RID: 1417
	public float touchDragThreshold = 40f;

	// Token: 0x0400058A RID: 1418
	public float touchClickThreshold = 40f;

	// Token: 0x0400058B RID: 1419
	public float rangeDistance = -1f;

	// Token: 0x0400058C RID: 1420
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x0400058D RID: 1421
	public string verticalAxisName = "Vertical";

	// Token: 0x0400058E RID: 1422
	public string horizontalAxisName = "Horizontal";

	// Token: 0x0400058F RID: 1423
	public KeyCode submitKey0 = 13;

	// Token: 0x04000590 RID: 1424
	public KeyCode submitKey1 = 330;

	// Token: 0x04000591 RID: 1425
	public KeyCode cancelKey0 = 27;

	// Token: 0x04000592 RID: 1426
	public KeyCode cancelKey1 = 331;

	// Token: 0x04000593 RID: 1427
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x04000594 RID: 1428
	public static bool showTooltips = true;

	// Token: 0x04000595 RID: 1429
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x04000596 RID: 1430
	public static Vector3 lastWorldPosition = Vector3.zero;

	// Token: 0x04000597 RID: 1431
	public static RaycastHit lastHit;

	// Token: 0x04000598 RID: 1432
	public static UICamera current = null;

	// Token: 0x04000599 RID: 1433
	public static Camera currentCamera = null;

	// Token: 0x0400059A RID: 1434
	public static UICamera.ControlScheme currentScheme = UICamera.ControlScheme.Mouse;

	// Token: 0x0400059B RID: 1435
	public static int currentTouchID = -1;

	// Token: 0x0400059C RID: 1436
	public static KeyCode currentKey = 0;

	// Token: 0x0400059D RID: 1437
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x0400059E RID: 1438
	public static bool inputHasFocus = false;

	// Token: 0x0400059F RID: 1439
	public static GameObject genericEventHandler;

	// Token: 0x040005A0 RID: 1440
	public static GameObject fallThrough;

	// Token: 0x040005A1 RID: 1441
	private static GameObject mCurrentSelection = null;

	// Token: 0x040005A2 RID: 1442
	private static GameObject mNextSelection = null;

	// Token: 0x040005A3 RID: 1443
	private static UICamera.ControlScheme mNextScheme = UICamera.ControlScheme.Controller;

	// Token: 0x040005A4 RID: 1444
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x040005A5 RID: 1445
	private static GameObject mHover;

	// Token: 0x040005A6 RID: 1446
	public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();

	// Token: 0x040005A7 RID: 1447
	private static float mNextEvent = 0f;

	// Token: 0x040005A8 RID: 1448
	private static Dictionary<int, UICamera.MouseOrTouch> mTouches = new Dictionary<int, UICamera.MouseOrTouch>();

	// Token: 0x040005A9 RID: 1449
	private static int mWidth = 0;

	// Token: 0x040005AA RID: 1450
	private static int mHeight = 0;

	// Token: 0x040005AB RID: 1451
	private GameObject mTooltip;

	// Token: 0x040005AC RID: 1452
	private Camera mCam;

	// Token: 0x040005AD RID: 1453
	private float mTooltipTime;

	// Token: 0x040005AE RID: 1454
	private float mNextRaycast;

	// Token: 0x040005AF RID: 1455
	public static bool isDragging = false;

	// Token: 0x040005B0 RID: 1456
	public static GameObject hoveredObject;

	// Token: 0x040005B1 RID: 1457
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x040005B2 RID: 1458
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x040005B3 RID: 1459
	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	// Token: 0x040005B4 RID: 1460
	private static bool mNotifying = false;

	// Token: 0x0200121B RID: 4635
	public enum ControlScheme
	{
		// Token: 0x04006495 RID: 25749
		Mouse,
		// Token: 0x04006496 RID: 25750
		Touch,
		// Token: 0x04006497 RID: 25751
		Controller
	}

	// Token: 0x0200121C RID: 4636
	public enum ClickNotification
	{
		// Token: 0x04006499 RID: 25753
		None,
		// Token: 0x0400649A RID: 25754
		Always,
		// Token: 0x0400649B RID: 25755
		BasedOnDelta
	}

	// Token: 0x0200121D RID: 4637
	public class MouseOrTouch
	{
		// Token: 0x0400649C RID: 25756
		public Vector2 pos;

		// Token: 0x0400649D RID: 25757
		public Vector2 lastPos;

		// Token: 0x0400649E RID: 25758
		public Vector2 delta;

		// Token: 0x0400649F RID: 25759
		public Vector2 totalDelta;

		// Token: 0x040064A0 RID: 25760
		public Camera pressedCam;

		// Token: 0x040064A1 RID: 25761
		public GameObject last;

		// Token: 0x040064A2 RID: 25762
		public GameObject current;

		// Token: 0x040064A3 RID: 25763
		public GameObject pressed;

		// Token: 0x040064A4 RID: 25764
		public GameObject dragged;

		// Token: 0x040064A5 RID: 25765
		public float clickTime;

		// Token: 0x040064A6 RID: 25766
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x040064A7 RID: 25767
		public bool touchBegan = true;

		// Token: 0x040064A8 RID: 25768
		public bool pressStarted;

		// Token: 0x040064A9 RID: 25769
		public bool dragStarted;
	}

	// Token: 0x0200121E RID: 4638
	public enum EventType
	{
		// Token: 0x040064AB RID: 25771
		World_3D,
		// Token: 0x040064AC RID: 25772
		UI_3D,
		// Token: 0x040064AD RID: 25773
		World_2D,
		// Token: 0x040064AE RID: 25774
		UI_2D
	}

	// Token: 0x0200121F RID: 4639
	// (Invoke) Token: 0x06007875 RID: 30837
	public delegate void OnScreenResize();

	// Token: 0x02001220 RID: 4640
	// (Invoke) Token: 0x06007879 RID: 30841
	public delegate void OnCustomInput();

	// Token: 0x02001221 RID: 4641
	private struct DepthEntry
	{
		// Token: 0x040064AF RID: 25775
		public int depth;

		// Token: 0x040064B0 RID: 25776
		public RaycastHit hit;

		// Token: 0x040064B1 RID: 25777
		public Vector3 point;

		// Token: 0x040064B2 RID: 25778
		public GameObject go;
	}
}
