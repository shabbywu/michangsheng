using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FD RID: 253
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060009AA RID: 2474 RVA: 0x0000A093 File Offset: 0x00008293
	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060009AB RID: 2475 RVA: 0x000888E0 File Offset: 0x00086AE0
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

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060009AC RID: 2476 RVA: 0x0000C06A File Offset: 0x0000A26A
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060009AD RID: 2477 RVA: 0x0000C077 File Offset: 0x0000A277
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

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x0000C099 File Offset: 0x0000A299
	// (set) Token: 0x060009AF RID: 2479 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
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

	// Token: 0x060009B0 RID: 2480 RVA: 0x00088924 File Offset: 0x00086B24
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

	// Token: 0x060009B1 RID: 2481 RVA: 0x000889C0 File Offset: 0x00086BC0
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

	// Token: 0x060009B2 RID: 2482 RVA: 0x0000C0AD File Offset: 0x0000A2AD
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

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00088A48 File Offset: 0x00086C48
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

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00088AF0 File Offset: 0x00086CF0
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

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00088B98 File Offset: 0x00086D98
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

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00088BBC File Offset: 0x00086DBC
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

	// Token: 0x060009B7 RID: 2487 RVA: 0x0000C0BC File Offset: 0x0000A2BC
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

	// Token: 0x060009B8 RID: 2488 RVA: 0x00088C0C File Offset: 0x00086E0C
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

	// Token: 0x060009B9 RID: 2489 RVA: 0x000892B8 File Offset: 0x000874B8
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

	// Token: 0x060009BA RID: 2490 RVA: 0x000892EC File Offset: 0x000874EC
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

	// Token: 0x060009BB RID: 2491 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
	public static bool IsHighlighted(GameObject go)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Mouse)
		{
			return UICamera.hoveredObject == go;
		}
		return UICamera.currentScheme == UICamera.ControlScheme.Controller && UICamera.selectedObject == go;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00089328 File Offset: 0x00087528
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

	// Token: 0x060009BD RID: 2493 RVA: 0x0000C11D File Offset: 0x0000A31D
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

	// Token: 0x060009BE RID: 2494 RVA: 0x0000C134 File Offset: 0x0000A334
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

	// Token: 0x060009BF RID: 2495 RVA: 0x0008937C File Offset: 0x0008757C
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

	// Token: 0x060009C0 RID: 2496 RVA: 0x000893D4 File Offset: 0x000875D4
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

	// Token: 0x060009C1 RID: 2497 RVA: 0x0000C15B File Offset: 0x0000A35B
	public static UICamera.MouseOrTouch GetMouse(int button)
	{
		return UICamera.mMouse[button];
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00089430 File Offset: 0x00087630
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

	// Token: 0x060009C3 RID: 2499 RVA: 0x0000C164 File Offset: 0x0000A364
	public static void RemoveTouch(int id)
	{
		UICamera.mTouches.Remove(id);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00089478 File Offset: 0x00087678
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

	// Token: 0x060009C5 RID: 2501 RVA: 0x0000C172 File Offset: 0x0000A372
	private void OnEnable()
	{
		UICamera.list.Add(this);
		UICamera.list.Sort(new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc));
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0000C195 File Offset: 0x0000A395
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00089594 File Offset: 0x00087794
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

	// Token: 0x060009C8 RID: 2504 RVA: 0x000895EC File Offset: 0x000877EC
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

	// Token: 0x060009C9 RID: 2505 RVA: 0x00089708 File Offset: 0x00087908
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

	// Token: 0x060009CA RID: 2506 RVA: 0x00089764 File Offset: 0x00087964
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

	// Token: 0x060009CB RID: 2507 RVA: 0x00089AB0 File Offset: 0x00087CB0
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

	// Token: 0x060009CC RID: 2508 RVA: 0x00089C78 File Offset: 0x00087E78
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

	// Token: 0x060009CD RID: 2509 RVA: 0x00089DC0 File Offset: 0x00087FC0
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

	// Token: 0x060009CE RID: 2510 RVA: 0x00089FB4 File Offset: 0x000881B4
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

	// Token: 0x060009CF RID: 2511 RVA: 0x0000C1A3 File Offset: 0x0000A3A3
	public void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		UICamera.Notify(this.mTooltip, "OnTooltip", val);
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0008A578 File Offset: 0x00088778
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

	// Token: 0x040006C0 RID: 1728
	public static BetterList<UICamera> list = new BetterList<UICamera>();

	// Token: 0x040006C1 RID: 1729
	public static UICamera.OnScreenResize onScreenResize;

	// Token: 0x040006C2 RID: 1730
	public UICamera.EventType eventType = UICamera.EventType.UI_3D;

	// Token: 0x040006C3 RID: 1731
	public LayerMask eventReceiverMask = -1;

	// Token: 0x040006C4 RID: 1732
	public bool debug;

	// Token: 0x040006C5 RID: 1733
	public bool useMouse = true;

	// Token: 0x040006C6 RID: 1734
	public bool useTouch = true;

	// Token: 0x040006C7 RID: 1735
	public bool allowMultiTouch = true;

	// Token: 0x040006C8 RID: 1736
	public bool useKeyboard = true;

	// Token: 0x040006C9 RID: 1737
	public bool useController = true;

	// Token: 0x040006CA RID: 1738
	public bool stickyTooltip = true;

	// Token: 0x040006CB RID: 1739
	public float tooltipDelay = 1f;

	// Token: 0x040006CC RID: 1740
	public float mouseDragThreshold = 4f;

	// Token: 0x040006CD RID: 1741
	public float mouseClickThreshold = 10f;

	// Token: 0x040006CE RID: 1742
	public float touchDragThreshold = 40f;

	// Token: 0x040006CF RID: 1743
	public float touchClickThreshold = 40f;

	// Token: 0x040006D0 RID: 1744
	public float rangeDistance = -1f;

	// Token: 0x040006D1 RID: 1745
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x040006D2 RID: 1746
	public string verticalAxisName = "Vertical";

	// Token: 0x040006D3 RID: 1747
	public string horizontalAxisName = "Horizontal";

	// Token: 0x040006D4 RID: 1748
	public KeyCode submitKey0 = 13;

	// Token: 0x040006D5 RID: 1749
	public KeyCode submitKey1 = 330;

	// Token: 0x040006D6 RID: 1750
	public KeyCode cancelKey0 = 27;

	// Token: 0x040006D7 RID: 1751
	public KeyCode cancelKey1 = 331;

	// Token: 0x040006D8 RID: 1752
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x040006D9 RID: 1753
	public static bool showTooltips = true;

	// Token: 0x040006DA RID: 1754
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x040006DB RID: 1755
	public static Vector3 lastWorldPosition = Vector3.zero;

	// Token: 0x040006DC RID: 1756
	public static RaycastHit lastHit;

	// Token: 0x040006DD RID: 1757
	public static UICamera current = null;

	// Token: 0x040006DE RID: 1758
	public static Camera currentCamera = null;

	// Token: 0x040006DF RID: 1759
	public static UICamera.ControlScheme currentScheme = UICamera.ControlScheme.Mouse;

	// Token: 0x040006E0 RID: 1760
	public static int currentTouchID = -1;

	// Token: 0x040006E1 RID: 1761
	public static KeyCode currentKey = 0;

	// Token: 0x040006E2 RID: 1762
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x040006E3 RID: 1763
	public static bool inputHasFocus = false;

	// Token: 0x040006E4 RID: 1764
	public static GameObject genericEventHandler;

	// Token: 0x040006E5 RID: 1765
	public static GameObject fallThrough;

	// Token: 0x040006E6 RID: 1766
	private static GameObject mCurrentSelection = null;

	// Token: 0x040006E7 RID: 1767
	private static GameObject mNextSelection = null;

	// Token: 0x040006E8 RID: 1768
	private static UICamera.ControlScheme mNextScheme = UICamera.ControlScheme.Controller;

	// Token: 0x040006E9 RID: 1769
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x040006EA RID: 1770
	private static GameObject mHover;

	// Token: 0x040006EB RID: 1771
	public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();

	// Token: 0x040006EC RID: 1772
	private static float mNextEvent = 0f;

	// Token: 0x040006ED RID: 1773
	private static Dictionary<int, UICamera.MouseOrTouch> mTouches = new Dictionary<int, UICamera.MouseOrTouch>();

	// Token: 0x040006EE RID: 1774
	private static int mWidth = 0;

	// Token: 0x040006EF RID: 1775
	private static int mHeight = 0;

	// Token: 0x040006F0 RID: 1776
	private GameObject mTooltip;

	// Token: 0x040006F1 RID: 1777
	private Camera mCam;

	// Token: 0x040006F2 RID: 1778
	private float mTooltipTime;

	// Token: 0x040006F3 RID: 1779
	private float mNextRaycast;

	// Token: 0x040006F4 RID: 1780
	public static bool isDragging = false;

	// Token: 0x040006F5 RID: 1781
	public static GameObject hoveredObject;

	// Token: 0x040006F6 RID: 1782
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x040006F7 RID: 1783
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x040006F8 RID: 1784
	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	// Token: 0x040006F9 RID: 1785
	private static bool mNotifying = false;

	// Token: 0x020000FE RID: 254
	public enum ControlScheme
	{
		// Token: 0x040006FB RID: 1787
		Mouse,
		// Token: 0x040006FC RID: 1788
		Touch,
		// Token: 0x040006FD RID: 1789
		Controller
	}

	// Token: 0x020000FF RID: 255
	public enum ClickNotification
	{
		// Token: 0x040006FF RID: 1791
		None,
		// Token: 0x04000700 RID: 1792
		Always,
		// Token: 0x04000701 RID: 1793
		BasedOnDelta
	}

	// Token: 0x02000100 RID: 256
	public class MouseOrTouch
	{
		// Token: 0x04000702 RID: 1794
		public Vector2 pos;

		// Token: 0x04000703 RID: 1795
		public Vector2 lastPos;

		// Token: 0x04000704 RID: 1796
		public Vector2 delta;

		// Token: 0x04000705 RID: 1797
		public Vector2 totalDelta;

		// Token: 0x04000706 RID: 1798
		public Camera pressedCam;

		// Token: 0x04000707 RID: 1799
		public GameObject last;

		// Token: 0x04000708 RID: 1800
		public GameObject current;

		// Token: 0x04000709 RID: 1801
		public GameObject pressed;

		// Token: 0x0400070A RID: 1802
		public GameObject dragged;

		// Token: 0x0400070B RID: 1803
		public float clickTime;

		// Token: 0x0400070C RID: 1804
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x0400070D RID: 1805
		public bool touchBegan = true;

		// Token: 0x0400070E RID: 1806
		public bool pressStarted;

		// Token: 0x0400070F RID: 1807
		public bool dragStarted;
	}

	// Token: 0x02000101 RID: 257
	public enum EventType
	{
		// Token: 0x04000711 RID: 1809
		World_3D,
		// Token: 0x04000712 RID: 1810
		UI_3D,
		// Token: 0x04000713 RID: 1811
		World_2D,
		// Token: 0x04000714 RID: 1812
		UI_2D
	}

	// Token: 0x02000102 RID: 258
	// (Invoke) Token: 0x060009D5 RID: 2517
	public delegate void OnScreenResize();

	// Token: 0x02000103 RID: 259
	// (Invoke) Token: 0x060009D9 RID: 2521
	public delegate void OnCustomInput();

	// Token: 0x02000104 RID: 260
	private struct DepthEntry
	{
		// Token: 0x04000715 RID: 1813
		public int depth;

		// Token: 0x04000716 RID: 1814
		public RaycastHit hit;

		// Token: 0x04000717 RID: 1815
		public Vector3 point;

		// Token: 0x04000718 RID: 1816
		public GameObject go;
	}
}
