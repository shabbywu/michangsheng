using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class EasyTouch : MonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000CDF RID: 3295 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
	// (remove) Token: 0x06000CE0 RID: 3296 RVA: 0x0004D2DC File Offset: 0x0004B4DC
	public static event EasyTouch.TouchCancelHandler On_Cancel;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06000CE1 RID: 3297 RVA: 0x0004D310 File Offset: 0x0004B510
	// (remove) Token: 0x06000CE2 RID: 3298 RVA: 0x0004D344 File Offset: 0x0004B544
	public static event EasyTouch.Cancel2FingersHandler On_Cancel2Fingers;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000CE3 RID: 3299 RVA: 0x0004D378 File Offset: 0x0004B578
	// (remove) Token: 0x06000CE4 RID: 3300 RVA: 0x0004D3AC File Offset: 0x0004B5AC
	public static event EasyTouch.TouchStartHandler On_TouchStart;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000CE5 RID: 3301 RVA: 0x0004D3E0 File Offset: 0x0004B5E0
	// (remove) Token: 0x06000CE6 RID: 3302 RVA: 0x0004D414 File Offset: 0x0004B614
	public static event EasyTouch.TouchDownHandler On_TouchDown;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000CE7 RID: 3303 RVA: 0x0004D448 File Offset: 0x0004B648
	// (remove) Token: 0x06000CE8 RID: 3304 RVA: 0x0004D47C File Offset: 0x0004B67C
	public static event EasyTouch.TouchUpHandler On_TouchUp;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000CE9 RID: 3305 RVA: 0x0004D4B0 File Offset: 0x0004B6B0
	// (remove) Token: 0x06000CEA RID: 3306 RVA: 0x0004D4E4 File Offset: 0x0004B6E4
	public static event EasyTouch.SimpleTapHandler On_SimpleTap;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x06000CEB RID: 3307 RVA: 0x0004D518 File Offset: 0x0004B718
	// (remove) Token: 0x06000CEC RID: 3308 RVA: 0x0004D54C File Offset: 0x0004B74C
	public static event EasyTouch.DoubleTapHandler On_DoubleTap;

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x06000CED RID: 3309 RVA: 0x0004D580 File Offset: 0x0004B780
	// (remove) Token: 0x06000CEE RID: 3310 RVA: 0x0004D5B4 File Offset: 0x0004B7B4
	public static event EasyTouch.LongTapStartHandler On_LongTapStart;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06000CEF RID: 3311 RVA: 0x0004D5E8 File Offset: 0x0004B7E8
	// (remove) Token: 0x06000CF0 RID: 3312 RVA: 0x0004D61C File Offset: 0x0004B81C
	public static event EasyTouch.LongTapHandler On_LongTap;

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06000CF1 RID: 3313 RVA: 0x0004D650 File Offset: 0x0004B850
	// (remove) Token: 0x06000CF2 RID: 3314 RVA: 0x0004D684 File Offset: 0x0004B884
	public static event EasyTouch.LongTapEndHandler On_LongTapEnd;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000CF3 RID: 3315 RVA: 0x0004D6B8 File Offset: 0x0004B8B8
	// (remove) Token: 0x06000CF4 RID: 3316 RVA: 0x0004D6EC File Offset: 0x0004B8EC
	public static event EasyTouch.DragStartHandler On_DragStart;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000CF5 RID: 3317 RVA: 0x0004D720 File Offset: 0x0004B920
	// (remove) Token: 0x06000CF6 RID: 3318 RVA: 0x0004D754 File Offset: 0x0004B954
	public static event EasyTouch.DragHandler On_Drag;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000CF7 RID: 3319 RVA: 0x0004D788 File Offset: 0x0004B988
	// (remove) Token: 0x06000CF8 RID: 3320 RVA: 0x0004D7BC File Offset: 0x0004B9BC
	public static event EasyTouch.DragEndHandler On_DragEnd;

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06000CF9 RID: 3321 RVA: 0x0004D7F0 File Offset: 0x0004B9F0
	// (remove) Token: 0x06000CFA RID: 3322 RVA: 0x0004D824 File Offset: 0x0004BA24
	public static event EasyTouch.SwipeStartHandler On_SwipeStart;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06000CFB RID: 3323 RVA: 0x0004D858 File Offset: 0x0004BA58
	// (remove) Token: 0x06000CFC RID: 3324 RVA: 0x0004D88C File Offset: 0x0004BA8C
	public static event EasyTouch.SwipeHandler On_Swipe;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x06000CFD RID: 3325 RVA: 0x0004D8C0 File Offset: 0x0004BAC0
	// (remove) Token: 0x06000CFE RID: 3326 RVA: 0x0004D8F4 File Offset: 0x0004BAF4
	public static event EasyTouch.SwipeEndHandler On_SwipeEnd;

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06000CFF RID: 3327 RVA: 0x0004D928 File Offset: 0x0004BB28
	// (remove) Token: 0x06000D00 RID: 3328 RVA: 0x0004D95C File Offset: 0x0004BB5C
	public static event EasyTouch.TouchStart2FingersHandler On_TouchStart2Fingers;

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x06000D01 RID: 3329 RVA: 0x0004D990 File Offset: 0x0004BB90
	// (remove) Token: 0x06000D02 RID: 3330 RVA: 0x0004D9C4 File Offset: 0x0004BBC4
	public static event EasyTouch.TouchDown2FingersHandler On_TouchDown2Fingers;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06000D03 RID: 3331 RVA: 0x0004D9F8 File Offset: 0x0004BBF8
	// (remove) Token: 0x06000D04 RID: 3332 RVA: 0x0004DA2C File Offset: 0x0004BC2C
	public static event EasyTouch.TouchUp2FingersHandler On_TouchUp2Fingers;

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06000D05 RID: 3333 RVA: 0x0004DA60 File Offset: 0x0004BC60
	// (remove) Token: 0x06000D06 RID: 3334 RVA: 0x0004DA94 File Offset: 0x0004BC94
	public static event EasyTouch.SimpleTap2FingersHandler On_SimpleTap2Fingers;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06000D07 RID: 3335 RVA: 0x0004DAC8 File Offset: 0x0004BCC8
	// (remove) Token: 0x06000D08 RID: 3336 RVA: 0x0004DAFC File Offset: 0x0004BCFC
	public static event EasyTouch.DoubleTap2FingersHandler On_DoubleTap2Fingers;

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06000D09 RID: 3337 RVA: 0x0004DB30 File Offset: 0x0004BD30
	// (remove) Token: 0x06000D0A RID: 3338 RVA: 0x0004DB64 File Offset: 0x0004BD64
	public static event EasyTouch.LongTapStart2FingersHandler On_LongTapStart2Fingers;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06000D0B RID: 3339 RVA: 0x0004DB98 File Offset: 0x0004BD98
	// (remove) Token: 0x06000D0C RID: 3340 RVA: 0x0004DBCC File Offset: 0x0004BDCC
	public static event EasyTouch.LongTap2FingersHandler On_LongTap2Fingers;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06000D0D RID: 3341 RVA: 0x0004DC00 File Offset: 0x0004BE00
	// (remove) Token: 0x06000D0E RID: 3342 RVA: 0x0004DC34 File Offset: 0x0004BE34
	public static event EasyTouch.LongTapEnd2FingersHandler On_LongTapEnd2Fingers;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06000D0F RID: 3343 RVA: 0x0004DC68 File Offset: 0x0004BE68
	// (remove) Token: 0x06000D10 RID: 3344 RVA: 0x0004DC9C File Offset: 0x0004BE9C
	public static event EasyTouch.TwistHandler On_Twist;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06000D11 RID: 3345 RVA: 0x0004DCD0 File Offset: 0x0004BED0
	// (remove) Token: 0x06000D12 RID: 3346 RVA: 0x0004DD04 File Offset: 0x0004BF04
	public static event EasyTouch.TwistEndHandler On_TwistEnd;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x06000D13 RID: 3347 RVA: 0x0004DD38 File Offset: 0x0004BF38
	// (remove) Token: 0x06000D14 RID: 3348 RVA: 0x0004DD6C File Offset: 0x0004BF6C
	public static event EasyTouch.PinchInHandler On_PinchIn;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06000D15 RID: 3349 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
	// (remove) Token: 0x06000D16 RID: 3350 RVA: 0x0004DDD4 File Offset: 0x0004BFD4
	public static event EasyTouch.PinchOutHandler On_PinchOut;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06000D17 RID: 3351 RVA: 0x0004DE08 File Offset: 0x0004C008
	// (remove) Token: 0x06000D18 RID: 3352 RVA: 0x0004DE3C File Offset: 0x0004C03C
	public static event EasyTouch.PinchEndHandler On_PinchEnd;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06000D19 RID: 3353 RVA: 0x0004DE70 File Offset: 0x0004C070
	// (remove) Token: 0x06000D1A RID: 3354 RVA: 0x0004DEA4 File Offset: 0x0004C0A4
	public static event EasyTouch.DragStart2FingersHandler On_DragStart2Fingers;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06000D1B RID: 3355 RVA: 0x0004DED8 File Offset: 0x0004C0D8
	// (remove) Token: 0x06000D1C RID: 3356 RVA: 0x0004DF0C File Offset: 0x0004C10C
	public static event EasyTouch.Drag2FingersHandler On_Drag2Fingers;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06000D1D RID: 3357 RVA: 0x0004DF40 File Offset: 0x0004C140
	// (remove) Token: 0x06000D1E RID: 3358 RVA: 0x0004DF74 File Offset: 0x0004C174
	public static event EasyTouch.DragEnd2FingersHandler On_DragEnd2Fingers;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06000D1F RID: 3359 RVA: 0x0004DFA8 File Offset: 0x0004C1A8
	// (remove) Token: 0x06000D20 RID: 3360 RVA: 0x0004DFDC File Offset: 0x0004C1DC
	public static event EasyTouch.SwipeStart2FingersHandler On_SwipeStart2Fingers;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06000D21 RID: 3361 RVA: 0x0004E010 File Offset: 0x0004C210
	// (remove) Token: 0x06000D22 RID: 3362 RVA: 0x0004E044 File Offset: 0x0004C244
	public static event EasyTouch.Swipe2FingersHandler On_Swipe2Fingers;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06000D23 RID: 3363 RVA: 0x0004E078 File Offset: 0x0004C278
	// (remove) Token: 0x06000D24 RID: 3364 RVA: 0x0004E0AC File Offset: 0x0004C2AC
	public static event EasyTouch.SwipeEnd2FingersHandler On_SwipeEnd2Fingers;

	// Token: 0x06000D25 RID: 3365 RVA: 0x0004E0E0 File Offset: 0x0004C2E0
	public EasyTouch()
	{
		this.enable = true;
		this.useBroadcastMessage = false;
		this.enable2FingersGesture = true;
		this.enableTwist = true;
		this.enablePinch = true;
		this.autoSelect = false;
		this.StationnaryTolerance = 25f;
		this.longTapTime = 1f;
		this.swipeTolerance = 0.85f;
		this.minPinchLength = 0f;
		this.minTwistAngle = 1f;
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0004E214 File Offset: 0x0004C414
	private void OnEnable()
	{
		if (Application.isPlaying && Application.isEditor)
		{
			this.InitEasyTouch();
		}
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0004E22A File Offset: 0x0004C42A
	private void Start()
	{
		this.InitEasyTouch();
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0004E234 File Offset: 0x0004C434
	private void InitEasyTouch()
	{
		this.input = new EasyTouchInput();
		if (EasyTouch.instance == null)
		{
			EasyTouch.instance = this;
		}
		if (this.easyTouchCamera == null)
		{
			this.easyTouchCamera = Camera.main;
			if (this.easyTouchCamera == null && this.autoSelect)
			{
				Debug.LogWarning("No camera with flag \"MainCam\" was found in the scene, please setup the camera");
			}
		}
		if (this.secondFingerTexture == null)
		{
			this.secondFingerTexture = (Resources.Load("secondFinger") as Texture);
		}
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0004E2BC File Offset: 0x0004C4BC
	private void OnGUI()
	{
		Vector2 secondFingerPosition = this.input.GetSecondFingerPosition();
		if (secondFingerPosition != new Vector2(-1f, -1f))
		{
			GUI.DrawTexture(new Rect(secondFingerPosition.x - 16f, (float)Screen.height - secondFingerPosition.y - 16f, 32f, 32f), this.secondFingerTexture);
		}
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x00004095 File Offset: 0x00002295
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0004E328 File Offset: 0x0004C528
	private void Update()
	{
		if (this.enable && EasyTouch.instance == this)
		{
			int num = this.input.TouchCount();
			if (this.oldTouchCount == 2 && num != 2 && num > 0)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_Cancel2Fingers, Vector2.zero, Vector2.zero, Vector2.zero, 0f, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, 0f);
			}
			this.UpdateTouches(false, num);
			this.oldPickObject2Finger = this.pickObject2Finger;
			if (this.enable2FingersGesture)
			{
				if (num == 2)
				{
					this.TwoFinger();
				}
				else
				{
					this.complexCurrentGesture = EasyTouch.GestureType.None;
					this.pickObject2Finger = null;
					this.twoFingerSwipeStart = false;
					this.twoFingerDragStart = false;
				}
			}
			for (int i = 0; i < 10; i++)
			{
				if (this.fingers[i] != null)
				{
					this.OneFinger(i);
				}
			}
			this.oldTouchCount = num;
		}
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0004E40C File Offset: 0x0004C60C
	private void UpdateTouches(bool realTouch, int touchCount)
	{
		Finger[] array = new Finger[10];
		this.fingers.CopyTo(array, 0);
		if (realTouch || this.enableRemote)
		{
			this.ResetTouches();
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				int num = 0;
				while (num < 10 && this.fingers[i] == null)
				{
					if (array[num] != null && array[num].fingerIndex == touch.fingerId)
					{
						this.fingers[i] = array[num];
					}
					num++;
				}
				if (this.fingers[i] == null)
				{
					this.fingers[i] = new Finger();
					this.fingers[i].fingerIndex = touch.fingerId;
					this.fingers[i].gesture = EasyTouch.GestureType.None;
					this.fingers[i].phase = 0;
				}
				else
				{
					this.fingers[i].phase = touch.phase;
				}
				this.fingers[i].position = touch.position;
				this.fingers[i].deltaPosition = touch.deltaPosition;
				this.fingers[i].tapCount = touch.tapCount;
				this.fingers[i].deltaTime = touch.deltaTime;
				this.fingers[i].touchCount = touchCount;
			}
			return;
		}
		for (int j = 0; j < touchCount; j++)
		{
			this.fingers[j] = this.input.GetMouseTouch(j, this.fingers[j]);
			this.fingers[j].touchCount = touchCount;
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0004E590 File Offset: 0x0004C790
	private void ResetTouches()
	{
		for (int i = 0; i < 10; i++)
		{
			this.fingers[i] = null;
		}
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0004E5B4 File Offset: 0x0004C7B4
	private void OneFinger(int fingerIndex)
	{
		if (this.fingers[fingerIndex].gesture == EasyTouch.GestureType.None)
		{
			this.startTimeAction = Time.realtimeSinceStartup;
			this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Acquisition;
			this.fingers[fingerIndex].startPosition = this.fingers[fingerIndex].position;
			if (this.autoSelect)
			{
				this.fingers[fingerIndex].pickedObject = this.GetPickeGameObject(this.fingers[fingerIndex].startPosition);
			}
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_TouchStart, this.fingers[fingerIndex], 0f, EasyTouch.SwipeType.None, 0f, Vector2.zero);
		}
		float num = Time.realtimeSinceStartup - this.startTimeAction;
		if (this.fingers[fingerIndex].phase == 4)
		{
			this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Cancel;
		}
		if (this.fingers[fingerIndex].phase != 3 && this.fingers[fingerIndex].phase != 4)
		{
			if (this.fingers[fingerIndex].phase == 2 && num >= this.longTapTime && this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition)
			{
				this.fingers[fingerIndex].gesture = EasyTouch.GestureType.LongTap;
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_LongTapStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
			}
			if ((this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition || this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap) && !this.FingerInTolerance(this.fingers[fingerIndex]))
			{
				if (this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap)
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Cancel;
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_LongTapEnd, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.None;
				}
				else if (this.fingers[fingerIndex].pickedObject)
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Drag;
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				}
				else
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Swipe;
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				}
			}
			EasyTouch.EventName eventName = EasyTouch.EventName.None;
			switch (this.fingers[fingerIndex].gesture)
			{
			case EasyTouch.GestureType.Drag:
				eventName = EasyTouch.EventName.On_Drag;
				break;
			case EasyTouch.GestureType.Swipe:
				eventName = EasyTouch.EventName.On_Swipe;
				break;
			case EasyTouch.GestureType.LongTap:
				eventName = EasyTouch.EventName.On_LongTap;
				break;
			}
			EasyTouch.SwipeType swipe = EasyTouch.SwipeType.None;
			if (eventName != EasyTouch.EventName.None)
			{
				swipe = this.GetSwipe(new Vector2(0f, 0f), this.fingers[fingerIndex].deltaPosition);
				this.CreateGesture(fingerIndex, eventName, this.fingers[fingerIndex], num, swipe, 0f, this.fingers[fingerIndex].deltaPosition);
			}
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_TouchDown, this.fingers[fingerIndex], num, swipe, 0f, this.fingers[fingerIndex].deltaPosition);
			return;
		}
		bool flag = true;
		switch (this.fingers[fingerIndex].gesture)
		{
		case EasyTouch.GestureType.Drag:
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].startPosition - this.fingers[fingerIndex].position).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
			break;
		case EasyTouch.GestureType.Swipe:
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
			break;
		case EasyTouch.GestureType.LongTap:
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_LongTapEnd, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
			break;
		case EasyTouch.GestureType.Cancel:
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_Cancel, this.fingers[fingerIndex], 0f, EasyTouch.SwipeType.None, 0f, Vector2.zero);
			break;
		case EasyTouch.GestureType.Acquisition:
			if (this.FingerInTolerance(this.fingers[fingerIndex]))
			{
				if (this.fingers[fingerIndex].tapCount < 2)
				{
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SimpleTap, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				}
				else
				{
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DoubleTap, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				}
			}
			else
			{
				EasyTouch.SwipeType swipe2 = this.GetSwipe(new Vector2(0f, 0f), this.fingers[fingerIndex].deltaPosition);
				if (this.fingers[fingerIndex].pickedObject)
				{
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_Drag, this.fingers[fingerIndex], num, swipe2, 0f, this.fingers[fingerIndex].deltaPosition);
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].startPosition - this.fingers[fingerIndex].position).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
				}
				else
				{
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_Swipe, this.fingers[fingerIndex], num, swipe2, 0f, this.fingers[fingerIndex].deltaPosition);
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
				}
			}
			break;
		}
		if (flag)
		{
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_TouchUp, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
			this.fingers[fingerIndex] = null;
		}
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0004EC4C File Offset: 0x0004CE4C
	private void CreateGesture(int touchIndex, EasyTouch.EventName message, Finger finger, float actionTime, EasyTouch.SwipeType swipe, float swipeLength, Vector2 swipeVector)
	{
		if (message == EasyTouch.EventName.On_TouchStart)
		{
			this.isStartHoverNGUI = this.IsTouchHoverNGui(touchIndex);
		}
		if (message == EasyTouch.EventName.On_Cancel || message == EasyTouch.EventName.On_TouchUp)
		{
			this.isStartHoverNGUI = false;
		}
		if (!this.isStartHoverNGUI)
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
			gesture.otherReceiver = this.receiverObject;
			gesture.isHoverReservedArea = this.IsTouchHoverVirtualControll(touchIndex);
			if (this.useBroadcastMessage)
			{
				this.SendGesture(message, gesture);
			}
			if (!this.useBroadcastMessage || this.isExtension)
			{
				this.RaiseEvent(message, gesture);
			}
		}
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0004ED58 File Offset: 0x0004CF58
	private void SendGesture(EasyTouch.EventName message, Gesture gesture)
	{
		if (this.useBroadcastMessage)
		{
			if (this.receiverObject != null && this.receiverObject != gesture.pickObject)
			{
				this.receiverObject.SendMessage(message.ToString(), gesture, 1);
			}
			if (gesture.pickObject)
			{
				gesture.pickObject.SendMessage(message.ToString(), gesture, 1);
				return;
			}
			base.SendMessage(message.ToString(), gesture, 1);
		}
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0004EDE8 File Offset: 0x0004CFE8
	private void TwoFinger()
	{
		float num = 0f;
		Vector2 zero = Vector2.zero;
		Vector2 vector = Vector2.zero;
		if (this.complexCurrentGesture == EasyTouch.GestureType.None)
		{
			this.twoFinger0 = this.GetTwoFinger(-1);
			this.twoFinger1 = this.GetTwoFinger(this.twoFinger0);
			this.startTimeAction = Time.realtimeSinceStartup;
			this.complexCurrentGesture = EasyTouch.GestureType.Tap;
			this.fingers[this.twoFinger0].complexStartPosition = this.fingers[this.twoFinger0].position;
			this.fingers[this.twoFinger1].complexStartPosition = this.fingers[this.twoFinger1].position;
			this.fingers[this.twoFinger0].oldPosition = this.fingers[this.twoFinger0].position;
			this.fingers[this.twoFinger1].oldPosition = this.fingers[this.twoFinger1].position;
			this.oldFingerDistance = Mathf.Abs(Vector2.Distance(this.fingers[this.twoFinger0].position, this.fingers[this.twoFinger1].position));
			this.startPosition2Finger = new Vector2((this.fingers[this.twoFinger0].position.x + this.fingers[this.twoFinger1].position.x) / 2f, (this.fingers[this.twoFinger0].position.y + this.fingers[this.twoFinger1].position.y) / 2f);
			vector = Vector2.zero;
			if (this.autoSelect)
			{
				this.pickObject2Finger = this.GetPickeGameObject(this.fingers[this.twoFinger0].complexStartPosition);
				if (this.pickObject2Finger != this.GetPickeGameObject(this.fingers[this.twoFinger1].complexStartPosition))
				{
					this.pickObject2Finger = null;
				}
			}
			this.CreateGesture2Finger(EasyTouch.EventName.On_TouchStart2Fingers, this.startPosition2Finger, this.startPosition2Finger, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, this.oldFingerDistance);
		}
		num = Time.realtimeSinceStartup - this.startTimeAction;
		zero..ctor((this.fingers[this.twoFinger0].position.x + this.fingers[this.twoFinger1].position.x) / 2f, (this.fingers[this.twoFinger0].position.y + this.fingers[this.twoFinger1].position.y) / 2f);
		vector = zero - this.oldStartPosition2Finger;
		float num2 = Mathf.Abs(Vector2.Distance(this.fingers[this.twoFinger0].position, this.fingers[this.twoFinger1].position));
		if (this.fingers[this.twoFinger0].phase == 4 || this.fingers[this.twoFinger1].phase == 4)
		{
			this.complexCurrentGesture = EasyTouch.GestureType.Cancel;
		}
		if (this.fingers[this.twoFinger0].phase != 3 && this.fingers[this.twoFinger1].phase != 3 && this.complexCurrentGesture != EasyTouch.GestureType.Cancel)
		{
			if (this.complexCurrentGesture == EasyTouch.GestureType.Tap && num >= this.longTapTime && this.FingerInTolerance(this.fingers[this.twoFinger0]) && this.FingerInTolerance(this.fingers[this.twoFinger1]))
			{
				this.complexCurrentGesture = EasyTouch.GestureType.LongTap;
				this.CreateGesture2Finger(EasyTouch.EventName.On_LongTapStart2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
			}
			if (true)
			{
				float num3 = Vector2.Dot(this.fingers[this.twoFinger0].deltaPosition.normalized, this.fingers[this.twoFinger1].deltaPosition.normalized);
				if (this.enablePinch && num2 != this.oldFingerDistance)
				{
					if (Mathf.Abs(num2 - this.oldFingerDistance) >= this.minPinchLength)
					{
						this.complexCurrentGesture = EasyTouch.GestureType.Pinch;
					}
					if (this.complexCurrentGesture == EasyTouch.GestureType.Pinch)
					{
						if (num2 < this.oldFingerDistance)
						{
							if (this.oldGesture != EasyTouch.GestureType.Pinch)
							{
								this.CreateStateEnd2Fingers(this.oldGesture, this.startPosition2Finger, zero, vector, num, false, num2);
								this.startTimeAction = Time.realtimeSinceStartup;
							}
							this.CreateGesture2Finger(EasyTouch.EventName.On_PinchIn, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.fingers[this.twoFinger0].complexStartPosition, this.fingers[this.twoFinger0].position), 0f, Vector2.zero, 0f, Mathf.Abs(num2 - this.oldFingerDistance), num2);
							this.complexCurrentGesture = EasyTouch.GestureType.Pinch;
						}
						else if (num2 > this.oldFingerDistance)
						{
							if (this.oldGesture != EasyTouch.GestureType.Pinch)
							{
								this.CreateStateEnd2Fingers(this.oldGesture, this.startPosition2Finger, zero, vector, num, false, num2);
								this.startTimeAction = Time.realtimeSinceStartup;
							}
							this.CreateGesture2Finger(EasyTouch.EventName.On_PinchOut, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.fingers[this.twoFinger0].complexStartPosition, this.fingers[this.twoFinger0].position), 0f, Vector2.zero, 0f, Mathf.Abs(num2 - this.oldFingerDistance), num2);
							this.complexCurrentGesture = EasyTouch.GestureType.Pinch;
						}
					}
				}
				if (this.enableTwist)
				{
					if (Mathf.Abs(this.TwistAngle()) > this.minTwistAngle)
					{
						if (this.complexCurrentGesture != EasyTouch.GestureType.Twist)
						{
							this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, false, num2);
							this.startTimeAction = Time.realtimeSinceStartup;
						}
						this.complexCurrentGesture = EasyTouch.GestureType.Twist;
					}
					if (this.complexCurrentGesture == EasyTouch.GestureType.Twist)
					{
						this.CreateGesture2Finger(EasyTouch.EventName.On_Twist, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, this.TwistAngle(), 0f, num2);
					}
					this.fingers[this.twoFinger0].oldPosition = this.fingers[this.twoFinger0].position;
					this.fingers[this.twoFinger1].oldPosition = this.fingers[this.twoFinger1].position;
				}
				if (num3 > 0f)
				{
					if (this.pickObject2Finger && !this.twoFingerDragStart)
					{
						if (this.complexCurrentGesture != EasyTouch.GestureType.Tap)
						{
							this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, false, num2);
							this.startTimeAction = Time.realtimeSinceStartup;
						}
						this.CreateGesture2Finger(EasyTouch.EventName.On_DragStart2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
						this.twoFingerDragStart = true;
					}
					else if (!this.pickObject2Finger && !this.twoFingerSwipeStart)
					{
						if (this.complexCurrentGesture != EasyTouch.GestureType.Tap)
						{
							this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, false, num2);
							this.startTimeAction = Time.realtimeSinceStartup;
						}
						this.CreateGesture2Finger(EasyTouch.EventName.On_SwipeStart2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
						this.twoFingerSwipeStart = true;
					}
				}
				else if (num3 < 0f)
				{
					this.twoFingerDragStart = false;
					this.twoFingerSwipeStart = false;
				}
				if (this.twoFingerDragStart)
				{
					this.CreateGesture2Finger(EasyTouch.EventName.On_Drag2Fingers, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.oldStartPosition2Finger, zero), 0f, vector, 0f, 0f, num2);
				}
				if (this.twoFingerSwipeStart)
				{
					this.CreateGesture2Finger(EasyTouch.EventName.On_Swipe2Fingers, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.oldStartPosition2Finger, zero), 0f, vector, 0f, 0f, num2);
				}
			}
			else if (this.complexCurrentGesture == EasyTouch.GestureType.LongTap)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_LongTap2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
			}
			this.CreateGesture2Finger(EasyTouch.EventName.On_TouchDown2Fingers, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.oldStartPosition2Finger, zero), 0f, vector, 0f, 0f, num2);
			this.oldFingerDistance = num2;
			this.oldStartPosition2Finger = zero;
			this.oldGesture = this.complexCurrentGesture;
			return;
		}
		this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, true, num2);
		this.complexCurrentGesture = EasyTouch.GestureType.None;
		this.pickObject2Finger = null;
		this.twoFingerSwipeStart = false;
		this.twoFingerDragStart = false;
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0004F634 File Offset: 0x0004D834
	private int GetTwoFinger(int index)
	{
		int num = index + 1;
		bool flag = false;
		while (num < 10 && !flag)
		{
			if (this.fingers[num] != null && num >= index)
			{
				flag = true;
			}
			num++;
		}
		return num - 1;
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0004F66C File Offset: 0x0004D86C
	private void CreateStateEnd2Fingers(EasyTouch.GestureType gesture, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float time, bool realEnd, float fingerDistance)
	{
		switch (gesture)
		{
		case EasyTouch.GestureType.Tap:
			if (this.fingers[this.twoFinger0].tapCount < 2 && this.fingers[this.twoFinger1].tapCount < 2)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_SimpleTap2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
			else
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_DoubleTap2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
			break;
		case EasyTouch.GestureType.LongTap:
			this.CreateGesture2Finger(EasyTouch.EventName.On_LongTapEnd2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		case EasyTouch.GestureType.Pinch:
			this.CreateGesture2Finger(EasyTouch.EventName.On_PinchEnd, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		case EasyTouch.GestureType.Twist:
			this.CreateGesture2Finger(EasyTouch.EventName.On_TwistEnd, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		}
		if (realEnd)
		{
			if (this.twoFingerDragStart)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_DragEnd2Fingers, startPosition, position, deltaPosition, time, this.GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0f, 0f, fingerDistance);
			}
			if (this.twoFingerSwipeStart)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_SwipeEnd2Fingers, startPosition, position, deltaPosition, time, this.GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0f, 0f, fingerDistance);
			}
			this.CreateGesture2Finger(EasyTouch.EventName.On_TouchUp2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
		}
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0004F83C File Offset: 0x0004DA3C
	private void CreateGesture2Finger(EasyTouch.EventName message, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float actionTime, EasyTouch.SwipeType swipe, float swipeLength, Vector2 swipeVector, float twist, float pinch, float twoDistance)
	{
		if (message == EasyTouch.EventName.On_TouchStart2Fingers)
		{
			this.isStartHoverNGUI = (this.IsTouchHoverNGui(this.twoFinger1) & this.IsTouchHoverNGui(this.twoFinger0));
		}
		if (!this.isStartHoverNGUI)
		{
			Gesture gesture = new Gesture();
			gesture.touchCount = 2;
			gesture.fingerIndex = -1;
			gesture.startPosition = startPosition;
			gesture.position = position;
			gesture.deltaPosition = deltaPosition;
			gesture.actionTime = actionTime;
			if (this.fingers[this.twoFinger0] != null)
			{
				gesture.deltaTime = this.fingers[this.twoFinger0].deltaTime;
			}
			else if (this.fingers[this.twoFinger1] != null)
			{
				gesture.deltaTime = this.fingers[this.twoFinger1].deltaTime;
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
			if (message != EasyTouch.EventName.On_Cancel2Fingers)
			{
				gesture.pickObject = this.pickObject2Finger;
			}
			else
			{
				gesture.pickObject = this.oldPickObject2Finger;
			}
			gesture.otherReceiver = this.receiverObject;
			if (this.useBroadcastMessage)
			{
				this.SendGesture2Finger(message, gesture);
				return;
			}
			this.RaiseEvent(message, gesture);
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0004F97C File Offset: 0x0004DB7C
	private void SendGesture2Finger(EasyTouch.EventName message, Gesture gesture)
	{
		if (this.receiverObject != null && this.receiverObject != gesture.pickObject)
		{
			this.receiverObject.SendMessage(message.ToString(), gesture, 1);
		}
		if (gesture.pickObject != null)
		{
			gesture.pickObject.SendMessage(message.ToString(), gesture, 1);
			return;
		}
		base.SendMessage(message.ToString(), gesture, 1);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0004FA04 File Offset: 0x0004DC04
	private void RaiseEvent(EasyTouch.EventName evnt, Gesture gesture)
	{
		switch (evnt)
		{
		case EasyTouch.EventName.On_Cancel:
			if (EasyTouch.On_Cancel != null)
			{
				EasyTouch.On_Cancel(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_Cancel2Fingers:
			if (EasyTouch.On_Cancel2Fingers != null)
			{
				EasyTouch.On_Cancel2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TouchStart:
			if (EasyTouch.On_TouchStart != null)
			{
				EasyTouch.On_TouchStart(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TouchDown:
			if (EasyTouch.On_TouchDown != null)
			{
				EasyTouch.On_TouchDown(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TouchUp:
			if (EasyTouch.On_TouchUp != null)
			{
				EasyTouch.On_TouchUp(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_SimpleTap:
			if (EasyTouch.On_SimpleTap != null)
			{
				EasyTouch.On_SimpleTap(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_DoubleTap:
			if (EasyTouch.On_DoubleTap != null)
			{
				EasyTouch.On_DoubleTap(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_LongTapStart:
			if (EasyTouch.On_LongTapStart != null)
			{
				EasyTouch.On_LongTapStart(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_LongTap:
			if (EasyTouch.On_LongTap != null)
			{
				EasyTouch.On_LongTap(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_LongTapEnd:
			if (EasyTouch.On_LongTapEnd != null)
			{
				EasyTouch.On_LongTapEnd(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_DragStart:
			if (EasyTouch.On_DragStart != null)
			{
				EasyTouch.On_DragStart(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_Drag:
			if (EasyTouch.On_Drag != null)
			{
				EasyTouch.On_Drag(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_DragEnd:
			if (EasyTouch.On_DragEnd != null)
			{
				EasyTouch.On_DragEnd(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_SwipeStart:
			if (EasyTouch.On_SwipeStart != null)
			{
				EasyTouch.On_SwipeStart(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_Swipe:
			if (EasyTouch.On_Swipe != null)
			{
				EasyTouch.On_Swipe(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_SwipeEnd:
			if (EasyTouch.On_SwipeEnd != null)
			{
				EasyTouch.On_SwipeEnd(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TouchStart2Fingers:
			if (EasyTouch.On_TouchStart2Fingers != null)
			{
				EasyTouch.On_TouchStart2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TouchDown2Fingers:
			if (EasyTouch.On_TouchDown2Fingers != null)
			{
				EasyTouch.On_TouchDown2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TouchUp2Fingers:
			if (EasyTouch.On_TouchUp2Fingers != null)
			{
				EasyTouch.On_TouchUp2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_SimpleTap2Fingers:
			if (EasyTouch.On_SimpleTap2Fingers != null)
			{
				EasyTouch.On_SimpleTap2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_DoubleTap2Fingers:
			if (EasyTouch.On_DoubleTap2Fingers != null)
			{
				EasyTouch.On_DoubleTap2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_LongTapStart2Fingers:
			if (EasyTouch.On_LongTapStart2Fingers != null)
			{
				EasyTouch.On_LongTapStart2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_LongTap2Fingers:
			if (EasyTouch.On_LongTap2Fingers != null)
			{
				EasyTouch.On_LongTap2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_LongTapEnd2Fingers:
			if (EasyTouch.On_LongTapEnd2Fingers != null)
			{
				EasyTouch.On_LongTapEnd2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_Twist:
			if (EasyTouch.On_Twist != null)
			{
				EasyTouch.On_Twist(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_TwistEnd:
			if (EasyTouch.On_TwistEnd != null)
			{
				EasyTouch.On_TwistEnd(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_PinchIn:
			if (EasyTouch.On_PinchIn != null)
			{
				EasyTouch.On_PinchIn(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_PinchOut:
			if (EasyTouch.On_PinchOut != null)
			{
				EasyTouch.On_PinchOut(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_PinchEnd:
			if (EasyTouch.On_PinchEnd != null)
			{
				EasyTouch.On_PinchEnd(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_DragStart2Fingers:
			if (EasyTouch.On_DragStart2Fingers != null)
			{
				EasyTouch.On_DragStart2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_Drag2Fingers:
			if (EasyTouch.On_Drag2Fingers != null)
			{
				EasyTouch.On_Drag2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_DragEnd2Fingers:
			if (EasyTouch.On_DragEnd2Fingers != null)
			{
				EasyTouch.On_DragEnd2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_SwipeStart2Fingers:
			if (EasyTouch.On_SwipeStart2Fingers != null)
			{
				EasyTouch.On_SwipeStart2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_Swipe2Fingers:
			if (EasyTouch.On_Swipe2Fingers != null)
			{
				EasyTouch.On_Swipe2Fingers(gesture);
				return;
			}
			break;
		case EasyTouch.EventName.On_SwipeEnd2Fingers:
			if (EasyTouch.On_SwipeEnd2Fingers != null)
			{
				EasyTouch.On_SwipeEnd2Fingers(gesture);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0004FD94 File Offset: 0x0004DF94
	private GameObject GetPickeGameObject(Vector2 screenPos)
	{
		if (this.easyTouchCamera != null)
		{
			Ray ray = this.easyTouchCamera.ScreenPointToRay(screenPos);
			LayerMask layerMask = this.pickableLayers;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, ref raycastHit, 3.4028235E+38f, layerMask))
			{
				return raycastHit.collider.gameObject;
			}
		}
		else
		{
			Debug.LogWarning("No camera is assigned to EasyTouch");
		}
		return null;
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0004FDF4 File Offset: 0x0004DFF4
	private EasyTouch.SwipeType GetSwipe(Vector2 start, Vector2 end)
	{
		Vector2 normalized = (end - start).normalized;
		if (Mathf.Abs(normalized.y) > Mathf.Abs(normalized.x))
		{
			if (Vector2.Dot(normalized, Vector2.up) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Up;
			}
			if (Vector2.Dot(normalized, -Vector2.up) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Down;
			}
		}
		else
		{
			if (Vector2.Dot(normalized, Vector2.right) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Right;
			}
			if (Vector2.Dot(normalized, -Vector2.right) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Left;
			}
		}
		return EasyTouch.SwipeType.Other;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0004FE88 File Offset: 0x0004E088
	private bool FingerInTolerance(Finger finger)
	{
		return (finger.position - finger.startPosition).sqrMagnitude <= this.StationnaryTolerance * this.StationnaryTolerance;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0004FEC0 File Offset: 0x0004E0C0
	private float DeltaAngle(Vector2 start, Vector2 end)
	{
		return Mathf.Atan2(start.x * end.y - start.y * end.x, Vector2.Dot(start, end));
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0004FEEC File Offset: 0x0004E0EC
	private float TwistAngle()
	{
		Vector2 end = this.fingers[this.twoFinger0].position - this.fingers[this.twoFinger1].position;
		Vector2 start = this.fingers[this.twoFinger0].oldPosition - this.fingers[this.twoFinger1].oldPosition;
		return 57.29578f * this.DeltaAngle(start, end);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0004FF5C File Offset: 0x0004E15C
	private bool IsTouchHoverNGui(int touchIndex)
	{
		bool flag = false;
		if (this.enabledNGuiMode)
		{
			LayerMask layerMask = this.nGUILayers;
			int num = 0;
			while (!flag && num < this.nGUICameras.Count)
			{
				RaycastHit raycastHit;
				flag = Physics.Raycast(this.nGUICameras[num].ScreenPointToRay(this.fingers[touchIndex].position), ref raycastHit, float.MaxValue, layerMask);
				num++;
			}
		}
		return flag;
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0004FFCC File Offset: 0x0004E1CC
	private bool IsTouchHoverVirtualControll(int touchIndex)
	{
		bool flag = false;
		if (this.enableReservedArea)
		{
			int num = 0;
			while (!flag && num < this.reservedAreas.Count)
			{
				Rect realRect = VirtualScreen.GetRealRect(this.reservedAreas[num]);
				realRect..ctor(realRect.x, (float)Screen.height - realRect.y - realRect.height, realRect.width, realRect.height);
				flag = realRect.Contains(this.fingers[touchIndex].position);
				num++;
			}
		}
		return flag;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x00050058 File Offset: 0x0004E258
	private Finger GetFinger(int finderId)
	{
		int num = 0;
		Finger finger = null;
		while (num < 10 && finger == null)
		{
			if (this.fingers[num] != null && this.fingers[num].fingerIndex == finderId)
			{
				finger = this.fingers[num];
			}
			num++;
		}
		return finger;
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0005009B File Offset: 0x0004E29B
	public static void SetEnabled(bool enable)
	{
		EasyTouch.instance.enable = enable;
		if (enable)
		{
			EasyTouch.instance.ResetTouches();
		}
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x000500B5 File Offset: 0x0004E2B5
	public static bool GetEnabled()
	{
		return EasyTouch.instance.enable;
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x000500C1 File Offset: 0x0004E2C1
	public static int GetTouchCount()
	{
		return EasyTouch.instance.input.TouchCount();
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x000500D2 File Offset: 0x0004E2D2
	public static void SetCamera(Camera cam)
	{
		EasyTouch.instance.easyTouchCamera = cam;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x000500DF File Offset: 0x0004E2DF
	public static Camera GetCamera()
	{
		return EasyTouch.instance.easyTouchCamera;
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x000500EB File Offset: 0x0004E2EB
	public static void SetEnable2FingersGesture(bool enable)
	{
		EasyTouch.instance.enable2FingersGesture = enable;
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x000500F8 File Offset: 0x0004E2F8
	public static bool GetEnable2FingersGesture()
	{
		return EasyTouch.instance.enable2FingersGesture;
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x00050104 File Offset: 0x0004E304
	public static void SetEnableTwist(bool enable)
	{
		EasyTouch.instance.enableTwist = enable;
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x00050111 File Offset: 0x0004E311
	public static bool GetEnableTwist()
	{
		return EasyTouch.instance.enableTwist;
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0005011D File Offset: 0x0004E31D
	public static void SetEnablePinch(bool enable)
	{
		EasyTouch.instance.enablePinch = enable;
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0005012A File Offset: 0x0004E32A
	public static bool GetEnablePinch()
	{
		return EasyTouch.instance.enablePinch;
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00050136 File Offset: 0x0004E336
	public static void SetEnableAutoSelect(bool enable)
	{
		EasyTouch.instance.autoSelect = enable;
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00050143 File Offset: 0x0004E343
	public static bool GetEnableAutoSelect()
	{
		return EasyTouch.instance.autoSelect;
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0005014F File Offset: 0x0004E34F
	public static void SetOtherReceiverObject(GameObject receiver)
	{
		EasyTouch.instance.receiverObject = receiver;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0005015C File Offset: 0x0004E35C
	public static GameObject GetOtherReceiverObject()
	{
		return EasyTouch.instance.receiverObject;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00050168 File Offset: 0x0004E368
	public static void SetStationnaryTolerance(float tolerance)
	{
		EasyTouch.instance.StationnaryTolerance = tolerance;
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00050175 File Offset: 0x0004E375
	public static float GetStationnaryTolerance()
	{
		return EasyTouch.instance.StationnaryTolerance;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00050181 File Offset: 0x0004E381
	public static void SetlongTapTime(float time)
	{
		EasyTouch.instance.longTapTime = time;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0005018E File Offset: 0x0004E38E
	public static float GetlongTapTime()
	{
		return EasyTouch.instance.longTapTime;
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0005019A File Offset: 0x0004E39A
	public static void SetSwipeTolerance(float tolerance)
	{
		EasyTouch.instance.swipeTolerance = tolerance;
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x000501A7 File Offset: 0x0004E3A7
	public static float GetSwipeTolerance()
	{
		return EasyTouch.instance.swipeTolerance;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x000501B3 File Offset: 0x0004E3B3
	public static void SetMinPinchLength(float length)
	{
		EasyTouch.instance.minPinchLength = length;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x000501C0 File Offset: 0x0004E3C0
	public static float GetMinPinchLength()
	{
		return EasyTouch.instance.minPinchLength;
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x000501CC File Offset: 0x0004E3CC
	public static void SetMinTwistAngle(float angle)
	{
		EasyTouch.instance.minTwistAngle = angle;
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x000501D9 File Offset: 0x0004E3D9
	public static float GetMinTwistAngle()
	{
		return EasyTouch.instance.minTwistAngle;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x000501E5 File Offset: 0x0004E3E5
	public static GameObject GetCurrentPickedObject(int fingerIndex)
	{
		return EasyTouch.instance.GetPickeGameObject(EasyTouch.instance.GetFinger(fingerIndex).position);
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00050204 File Offset: 0x0004E404
	public static bool IsRectUnderTouch(Rect rect, bool guiRect = false)
	{
		bool result = false;
		for (int i = 0; i < 10; i++)
		{
			if (EasyTouch.instance.fingers[i] != null)
			{
				if (guiRect)
				{
					rect..ctor(rect.x, (float)Screen.height - rect.y - rect.height, rect.width, rect.height);
				}
				result = rect.Contains(EasyTouch.instance.fingers[i].position);
				break;
			}
		}
		return result;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0005027F File Offset: 0x0004E47F
	public static Vector2 GetFingerPosition(int fingerIndex)
	{
		if (EasyTouch.instance.fingers[fingerIndex] != null)
		{
			return EasyTouch.instance.GetFinger(fingerIndex).position;
		}
		return Vector2.zero;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x000502A5 File Offset: 0x0004E4A5
	public static bool GetIsReservedArea()
	{
		return EasyTouch.instance.enableReservedArea;
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x000502B1 File Offset: 0x0004E4B1
	public static void SetIsReservedArea(bool enable)
	{
		EasyTouch.instance.enableReservedArea = enable;
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x000502BE File Offset: 0x0004E4BE
	public static void AddReservedArea(Rect rec)
	{
		EasyTouch.instance.reservedAreas.Add(rec);
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x000502D0 File Offset: 0x0004E4D0
	public static void RemoveReservedArea(Rect rec)
	{
		EasyTouch.instance.reservedAreas.Remove(rec);
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x000502E3 File Offset: 0x0004E4E3
	public static void ResetTouch(int fingerIndex)
	{
		EasyTouch.instance.GetFinger(fingerIndex).gesture = EasyTouch.GestureType.None;
	}

	// Token: 0x04000922 RID: 2338
	public bool enable = true;

	// Token: 0x04000923 RID: 2339
	public bool enableRemote;

	// Token: 0x04000924 RID: 2340
	public bool useBroadcastMessage = true;

	// Token: 0x04000925 RID: 2341
	public GameObject receiverObject;

	// Token: 0x04000926 RID: 2342
	public bool isExtension;

	// Token: 0x04000927 RID: 2343
	public bool enable2FingersGesture = true;

	// Token: 0x04000928 RID: 2344
	public bool enableTwist = true;

	// Token: 0x04000929 RID: 2345
	public bool enablePinch = true;

	// Token: 0x0400092A RID: 2346
	public Camera easyTouchCamera;

	// Token: 0x0400092B RID: 2347
	public bool autoSelect;

	// Token: 0x0400092C RID: 2348
	public LayerMask pickableLayers;

	// Token: 0x0400092D RID: 2349
	public float StationnaryTolerance = 25f;

	// Token: 0x0400092E RID: 2350
	public float longTapTime = 1f;

	// Token: 0x0400092F RID: 2351
	public float swipeTolerance = 0.85f;

	// Token: 0x04000930 RID: 2352
	public float minPinchLength;

	// Token: 0x04000931 RID: 2353
	public float minTwistAngle = 1f;

	// Token: 0x04000932 RID: 2354
	public bool enabledNGuiMode;

	// Token: 0x04000933 RID: 2355
	public LayerMask nGUILayers;

	// Token: 0x04000934 RID: 2356
	public List<Camera> nGUICameras = new List<Camera>();

	// Token: 0x04000935 RID: 2357
	private bool isStartHoverNGUI;

	// Token: 0x04000936 RID: 2358
	public List<Rect> reservedAreas = new List<Rect>();

	// Token: 0x04000937 RID: 2359
	public bool enableReservedArea = true;

	// Token: 0x04000938 RID: 2360
	public KeyCode twistKey = 308;

	// Token: 0x04000939 RID: 2361
	public KeyCode swipeKey = 306;

	// Token: 0x0400093A RID: 2362
	public bool showGeneral = true;

	// Token: 0x0400093B RID: 2363
	public bool showSelect = true;

	// Token: 0x0400093C RID: 2364
	public bool showGesture = true;

	// Token: 0x0400093D RID: 2365
	public bool showTwoFinger = true;

	// Token: 0x0400093E RID: 2366
	public bool showSecondFinger = true;

	// Token: 0x0400093F RID: 2367
	public static EasyTouch instance;

	// Token: 0x04000940 RID: 2368
	private EasyTouchInput input;

	// Token: 0x04000941 RID: 2369
	private EasyTouch.GestureType complexCurrentGesture = EasyTouch.GestureType.None;

	// Token: 0x04000942 RID: 2370
	private EasyTouch.GestureType oldGesture = EasyTouch.GestureType.None;

	// Token: 0x04000943 RID: 2371
	private float startTimeAction;

	// Token: 0x04000944 RID: 2372
	private Finger[] fingers = new Finger[10];

	// Token: 0x04000945 RID: 2373
	private GameObject pickObject2Finger;

	// Token: 0x04000946 RID: 2374
	private GameObject oldPickObject2Finger;

	// Token: 0x04000947 RID: 2375
	public Texture secondFingerTexture;

	// Token: 0x04000948 RID: 2376
	private Vector2 startPosition2Finger;

	// Token: 0x04000949 RID: 2377
	private int twoFinger0;

	// Token: 0x0400094A RID: 2378
	private int twoFinger1;

	// Token: 0x0400094B RID: 2379
	private Vector2 oldStartPosition2Finger;

	// Token: 0x0400094C RID: 2380
	private float oldFingerDistance;

	// Token: 0x0400094D RID: 2381
	private bool twoFingerDragStart;

	// Token: 0x0400094E RID: 2382
	private bool twoFingerSwipeStart;

	// Token: 0x0400094F RID: 2383
	private int oldTouchCount;

	// Token: 0x0200125E RID: 4702
	// (Invoke) Token: 0x06007900 RID: 30976
	public delegate void TouchCancelHandler(Gesture gesture);

	// Token: 0x0200125F RID: 4703
	// (Invoke) Token: 0x06007904 RID: 30980
	public delegate void Cancel2FingersHandler(Gesture gesture);

	// Token: 0x02001260 RID: 4704
	// (Invoke) Token: 0x06007908 RID: 30984
	public delegate void TouchStartHandler(Gesture gesture);

	// Token: 0x02001261 RID: 4705
	// (Invoke) Token: 0x0600790C RID: 30988
	public delegate void TouchDownHandler(Gesture gesture);

	// Token: 0x02001262 RID: 4706
	// (Invoke) Token: 0x06007910 RID: 30992
	public delegate void TouchUpHandler(Gesture gesture);

	// Token: 0x02001263 RID: 4707
	// (Invoke) Token: 0x06007914 RID: 30996
	public delegate void SimpleTapHandler(Gesture gesture);

	// Token: 0x02001264 RID: 4708
	// (Invoke) Token: 0x06007918 RID: 31000
	public delegate void DoubleTapHandler(Gesture gesture);

	// Token: 0x02001265 RID: 4709
	// (Invoke) Token: 0x0600791C RID: 31004
	public delegate void LongTapStartHandler(Gesture gesture);

	// Token: 0x02001266 RID: 4710
	// (Invoke) Token: 0x06007920 RID: 31008
	public delegate void LongTapHandler(Gesture gesture);

	// Token: 0x02001267 RID: 4711
	// (Invoke) Token: 0x06007924 RID: 31012
	public delegate void LongTapEndHandler(Gesture gesture);

	// Token: 0x02001268 RID: 4712
	// (Invoke) Token: 0x06007928 RID: 31016
	public delegate void DragStartHandler(Gesture gesture);

	// Token: 0x02001269 RID: 4713
	// (Invoke) Token: 0x0600792C RID: 31020
	public delegate void DragHandler(Gesture gesture);

	// Token: 0x0200126A RID: 4714
	// (Invoke) Token: 0x06007930 RID: 31024
	public delegate void DragEndHandler(Gesture gesture);

	// Token: 0x0200126B RID: 4715
	// (Invoke) Token: 0x06007934 RID: 31028
	public delegate void SwipeStartHandler(Gesture gesture);

	// Token: 0x0200126C RID: 4716
	// (Invoke) Token: 0x06007938 RID: 31032
	public delegate void SwipeHandler(Gesture gesture);

	// Token: 0x0200126D RID: 4717
	// (Invoke) Token: 0x0600793C RID: 31036
	public delegate void SwipeEndHandler(Gesture gesture);

	// Token: 0x0200126E RID: 4718
	// (Invoke) Token: 0x06007940 RID: 31040
	public delegate void TouchStart2FingersHandler(Gesture gesture);

	// Token: 0x0200126F RID: 4719
	// (Invoke) Token: 0x06007944 RID: 31044
	public delegate void TouchDown2FingersHandler(Gesture gesture);

	// Token: 0x02001270 RID: 4720
	// (Invoke) Token: 0x06007948 RID: 31048
	public delegate void TouchUp2FingersHandler(Gesture gesture);

	// Token: 0x02001271 RID: 4721
	// (Invoke) Token: 0x0600794C RID: 31052
	public delegate void SimpleTap2FingersHandler(Gesture gesture);

	// Token: 0x02001272 RID: 4722
	// (Invoke) Token: 0x06007950 RID: 31056
	public delegate void DoubleTap2FingersHandler(Gesture gesture);

	// Token: 0x02001273 RID: 4723
	// (Invoke) Token: 0x06007954 RID: 31060
	public delegate void LongTapStart2FingersHandler(Gesture gesture);

	// Token: 0x02001274 RID: 4724
	// (Invoke) Token: 0x06007958 RID: 31064
	public delegate void LongTap2FingersHandler(Gesture gesture);

	// Token: 0x02001275 RID: 4725
	// (Invoke) Token: 0x0600795C RID: 31068
	public delegate void LongTapEnd2FingersHandler(Gesture gesture);

	// Token: 0x02001276 RID: 4726
	// (Invoke) Token: 0x06007960 RID: 31072
	public delegate void TwistHandler(Gesture gesture);

	// Token: 0x02001277 RID: 4727
	// (Invoke) Token: 0x06007964 RID: 31076
	public delegate void TwistEndHandler(Gesture gesture);

	// Token: 0x02001278 RID: 4728
	// (Invoke) Token: 0x06007968 RID: 31080
	public delegate void PinchInHandler(Gesture gesture);

	// Token: 0x02001279 RID: 4729
	// (Invoke) Token: 0x0600796C RID: 31084
	public delegate void PinchOutHandler(Gesture gesture);

	// Token: 0x0200127A RID: 4730
	// (Invoke) Token: 0x06007970 RID: 31088
	public delegate void PinchEndHandler(Gesture gesture);

	// Token: 0x0200127B RID: 4731
	// (Invoke) Token: 0x06007974 RID: 31092
	public delegate void DragStart2FingersHandler(Gesture gesture);

	// Token: 0x0200127C RID: 4732
	// (Invoke) Token: 0x06007978 RID: 31096
	public delegate void Drag2FingersHandler(Gesture gesture);

	// Token: 0x0200127D RID: 4733
	// (Invoke) Token: 0x0600797C RID: 31100
	public delegate void DragEnd2FingersHandler(Gesture gesture);

	// Token: 0x0200127E RID: 4734
	// (Invoke) Token: 0x06007980 RID: 31104
	public delegate void SwipeStart2FingersHandler(Gesture gesture);

	// Token: 0x0200127F RID: 4735
	// (Invoke) Token: 0x06007984 RID: 31108
	public delegate void Swipe2FingersHandler(Gesture gesture);

	// Token: 0x02001280 RID: 4736
	// (Invoke) Token: 0x06007988 RID: 31112
	public delegate void SwipeEnd2FingersHandler(Gesture gesture);

	// Token: 0x02001281 RID: 4737
	public enum GestureType
	{
		// Token: 0x04006597 RID: 26007
		Tap,
		// Token: 0x04006598 RID: 26008
		Drag,
		// Token: 0x04006599 RID: 26009
		Swipe,
		// Token: 0x0400659A RID: 26010
		None,
		// Token: 0x0400659B RID: 26011
		LongTap,
		// Token: 0x0400659C RID: 26012
		Pinch,
		// Token: 0x0400659D RID: 26013
		Twist,
		// Token: 0x0400659E RID: 26014
		Cancel,
		// Token: 0x0400659F RID: 26015
		Acquisition
	}

	// Token: 0x02001282 RID: 4738
	public enum SwipeType
	{
		// Token: 0x040065A1 RID: 26017
		None,
		// Token: 0x040065A2 RID: 26018
		Left,
		// Token: 0x040065A3 RID: 26019
		Right,
		// Token: 0x040065A4 RID: 26020
		Up,
		// Token: 0x040065A5 RID: 26021
		Down,
		// Token: 0x040065A6 RID: 26022
		Other
	}

	// Token: 0x02001283 RID: 4739
	private enum EventName
	{
		// Token: 0x040065A8 RID: 26024
		None,
		// Token: 0x040065A9 RID: 26025
		On_Cancel,
		// Token: 0x040065AA RID: 26026
		On_Cancel2Fingers,
		// Token: 0x040065AB RID: 26027
		On_TouchStart,
		// Token: 0x040065AC RID: 26028
		On_TouchDown,
		// Token: 0x040065AD RID: 26029
		On_TouchUp,
		// Token: 0x040065AE RID: 26030
		On_SimpleTap,
		// Token: 0x040065AF RID: 26031
		On_DoubleTap,
		// Token: 0x040065B0 RID: 26032
		On_LongTapStart,
		// Token: 0x040065B1 RID: 26033
		On_LongTap,
		// Token: 0x040065B2 RID: 26034
		On_LongTapEnd,
		// Token: 0x040065B3 RID: 26035
		On_DragStart,
		// Token: 0x040065B4 RID: 26036
		On_Drag,
		// Token: 0x040065B5 RID: 26037
		On_DragEnd,
		// Token: 0x040065B6 RID: 26038
		On_SwipeStart,
		// Token: 0x040065B7 RID: 26039
		On_Swipe,
		// Token: 0x040065B8 RID: 26040
		On_SwipeEnd,
		// Token: 0x040065B9 RID: 26041
		On_TouchStart2Fingers,
		// Token: 0x040065BA RID: 26042
		On_TouchDown2Fingers,
		// Token: 0x040065BB RID: 26043
		On_TouchUp2Fingers,
		// Token: 0x040065BC RID: 26044
		On_SimpleTap2Fingers,
		// Token: 0x040065BD RID: 26045
		On_DoubleTap2Fingers,
		// Token: 0x040065BE RID: 26046
		On_LongTapStart2Fingers,
		// Token: 0x040065BF RID: 26047
		On_LongTap2Fingers,
		// Token: 0x040065C0 RID: 26048
		On_LongTapEnd2Fingers,
		// Token: 0x040065C1 RID: 26049
		On_Twist,
		// Token: 0x040065C2 RID: 26050
		On_TwistEnd,
		// Token: 0x040065C3 RID: 26051
		On_PinchIn,
		// Token: 0x040065C4 RID: 26052
		On_PinchOut,
		// Token: 0x040065C5 RID: 26053
		On_PinchEnd,
		// Token: 0x040065C6 RID: 26054
		On_DragStart2Fingers,
		// Token: 0x040065C7 RID: 26055
		On_Drag2Fingers,
		// Token: 0x040065C8 RID: 26056
		On_DragEnd2Fingers,
		// Token: 0x040065C9 RID: 26057
		On_SwipeStart2Fingers,
		// Token: 0x040065CA RID: 26058
		On_Swipe2Fingers,
		// Token: 0x040065CB RID: 26059
		On_SwipeEnd2Fingers
	}
}
