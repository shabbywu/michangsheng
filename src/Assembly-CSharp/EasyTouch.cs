using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class EasyTouch : MonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000E28 RID: 3624 RVA: 0x0009E2A4 File Offset: 0x0009C4A4
	// (remove) Token: 0x06000E29 RID: 3625 RVA: 0x0009E2D8 File Offset: 0x0009C4D8
	public static event EasyTouch.TouchCancelHandler On_Cancel;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06000E2A RID: 3626 RVA: 0x0009E30C File Offset: 0x0009C50C
	// (remove) Token: 0x06000E2B RID: 3627 RVA: 0x0009E340 File Offset: 0x0009C540
	public static event EasyTouch.Cancel2FingersHandler On_Cancel2Fingers;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000E2C RID: 3628 RVA: 0x0009E374 File Offset: 0x0009C574
	// (remove) Token: 0x06000E2D RID: 3629 RVA: 0x0009E3A8 File Offset: 0x0009C5A8
	public static event EasyTouch.TouchStartHandler On_TouchStart;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000E2E RID: 3630 RVA: 0x0009E3DC File Offset: 0x0009C5DC
	// (remove) Token: 0x06000E2F RID: 3631 RVA: 0x0009E410 File Offset: 0x0009C610
	public static event EasyTouch.TouchDownHandler On_TouchDown;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000E30 RID: 3632 RVA: 0x0009E444 File Offset: 0x0009C644
	// (remove) Token: 0x06000E31 RID: 3633 RVA: 0x0009E478 File Offset: 0x0009C678
	public static event EasyTouch.TouchUpHandler On_TouchUp;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000E32 RID: 3634 RVA: 0x0009E4AC File Offset: 0x0009C6AC
	// (remove) Token: 0x06000E33 RID: 3635 RVA: 0x0009E4E0 File Offset: 0x0009C6E0
	public static event EasyTouch.SimpleTapHandler On_SimpleTap;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x06000E34 RID: 3636 RVA: 0x0009E514 File Offset: 0x0009C714
	// (remove) Token: 0x06000E35 RID: 3637 RVA: 0x0009E548 File Offset: 0x0009C748
	public static event EasyTouch.DoubleTapHandler On_DoubleTap;

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x06000E36 RID: 3638 RVA: 0x0009E57C File Offset: 0x0009C77C
	// (remove) Token: 0x06000E37 RID: 3639 RVA: 0x0009E5B0 File Offset: 0x0009C7B0
	public static event EasyTouch.LongTapStartHandler On_LongTapStart;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06000E38 RID: 3640 RVA: 0x0009E5E4 File Offset: 0x0009C7E4
	// (remove) Token: 0x06000E39 RID: 3641 RVA: 0x0009E618 File Offset: 0x0009C818
	public static event EasyTouch.LongTapHandler On_LongTap;

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06000E3A RID: 3642 RVA: 0x0009E64C File Offset: 0x0009C84C
	// (remove) Token: 0x06000E3B RID: 3643 RVA: 0x0009E680 File Offset: 0x0009C880
	public static event EasyTouch.LongTapEndHandler On_LongTapEnd;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000E3C RID: 3644 RVA: 0x0009E6B4 File Offset: 0x0009C8B4
	// (remove) Token: 0x06000E3D RID: 3645 RVA: 0x0009E6E8 File Offset: 0x0009C8E8
	public static event EasyTouch.DragStartHandler On_DragStart;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000E3E RID: 3646 RVA: 0x0009E71C File Offset: 0x0009C91C
	// (remove) Token: 0x06000E3F RID: 3647 RVA: 0x0009E750 File Offset: 0x0009C950
	public static event EasyTouch.DragHandler On_Drag;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000E40 RID: 3648 RVA: 0x0009E784 File Offset: 0x0009C984
	// (remove) Token: 0x06000E41 RID: 3649 RVA: 0x0009E7B8 File Offset: 0x0009C9B8
	public static event EasyTouch.DragEndHandler On_DragEnd;

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06000E42 RID: 3650 RVA: 0x0009E7EC File Offset: 0x0009C9EC
	// (remove) Token: 0x06000E43 RID: 3651 RVA: 0x0009E820 File Offset: 0x0009CA20
	public static event EasyTouch.SwipeStartHandler On_SwipeStart;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06000E44 RID: 3652 RVA: 0x0009E854 File Offset: 0x0009CA54
	// (remove) Token: 0x06000E45 RID: 3653 RVA: 0x0009E888 File Offset: 0x0009CA88
	public static event EasyTouch.SwipeHandler On_Swipe;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x06000E46 RID: 3654 RVA: 0x0009E8BC File Offset: 0x0009CABC
	// (remove) Token: 0x06000E47 RID: 3655 RVA: 0x0009E8F0 File Offset: 0x0009CAF0
	public static event EasyTouch.SwipeEndHandler On_SwipeEnd;

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06000E48 RID: 3656 RVA: 0x0009E924 File Offset: 0x0009CB24
	// (remove) Token: 0x06000E49 RID: 3657 RVA: 0x0009E958 File Offset: 0x0009CB58
	public static event EasyTouch.TouchStart2FingersHandler On_TouchStart2Fingers;

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x06000E4A RID: 3658 RVA: 0x0009E98C File Offset: 0x0009CB8C
	// (remove) Token: 0x06000E4B RID: 3659 RVA: 0x0009E9C0 File Offset: 0x0009CBC0
	public static event EasyTouch.TouchDown2FingersHandler On_TouchDown2Fingers;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06000E4C RID: 3660 RVA: 0x0009E9F4 File Offset: 0x0009CBF4
	// (remove) Token: 0x06000E4D RID: 3661 RVA: 0x0009EA28 File Offset: 0x0009CC28
	public static event EasyTouch.TouchUp2FingersHandler On_TouchUp2Fingers;

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06000E4E RID: 3662 RVA: 0x0009EA5C File Offset: 0x0009CC5C
	// (remove) Token: 0x06000E4F RID: 3663 RVA: 0x0009EA90 File Offset: 0x0009CC90
	public static event EasyTouch.SimpleTap2FingersHandler On_SimpleTap2Fingers;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06000E50 RID: 3664 RVA: 0x0009EAC4 File Offset: 0x0009CCC4
	// (remove) Token: 0x06000E51 RID: 3665 RVA: 0x0009EAF8 File Offset: 0x0009CCF8
	public static event EasyTouch.DoubleTap2FingersHandler On_DoubleTap2Fingers;

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06000E52 RID: 3666 RVA: 0x0009EB2C File Offset: 0x0009CD2C
	// (remove) Token: 0x06000E53 RID: 3667 RVA: 0x0009EB60 File Offset: 0x0009CD60
	public static event EasyTouch.LongTapStart2FingersHandler On_LongTapStart2Fingers;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06000E54 RID: 3668 RVA: 0x0009EB94 File Offset: 0x0009CD94
	// (remove) Token: 0x06000E55 RID: 3669 RVA: 0x0009EBC8 File Offset: 0x0009CDC8
	public static event EasyTouch.LongTap2FingersHandler On_LongTap2Fingers;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06000E56 RID: 3670 RVA: 0x0009EBFC File Offset: 0x0009CDFC
	// (remove) Token: 0x06000E57 RID: 3671 RVA: 0x0009EC30 File Offset: 0x0009CE30
	public static event EasyTouch.LongTapEnd2FingersHandler On_LongTapEnd2Fingers;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06000E58 RID: 3672 RVA: 0x0009EC64 File Offset: 0x0009CE64
	// (remove) Token: 0x06000E59 RID: 3673 RVA: 0x0009EC98 File Offset: 0x0009CE98
	public static event EasyTouch.TwistHandler On_Twist;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06000E5A RID: 3674 RVA: 0x0009ECCC File Offset: 0x0009CECC
	// (remove) Token: 0x06000E5B RID: 3675 RVA: 0x0009ED00 File Offset: 0x0009CF00
	public static event EasyTouch.TwistEndHandler On_TwistEnd;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x06000E5C RID: 3676 RVA: 0x0009ED34 File Offset: 0x0009CF34
	// (remove) Token: 0x06000E5D RID: 3677 RVA: 0x0009ED68 File Offset: 0x0009CF68
	public static event EasyTouch.PinchInHandler On_PinchIn;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06000E5E RID: 3678 RVA: 0x0009ED9C File Offset: 0x0009CF9C
	// (remove) Token: 0x06000E5F RID: 3679 RVA: 0x0009EDD0 File Offset: 0x0009CFD0
	public static event EasyTouch.PinchOutHandler On_PinchOut;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06000E60 RID: 3680 RVA: 0x0009EE04 File Offset: 0x0009D004
	// (remove) Token: 0x06000E61 RID: 3681 RVA: 0x0009EE38 File Offset: 0x0009D038
	public static event EasyTouch.PinchEndHandler On_PinchEnd;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06000E62 RID: 3682 RVA: 0x0009EE6C File Offset: 0x0009D06C
	// (remove) Token: 0x06000E63 RID: 3683 RVA: 0x0009EEA0 File Offset: 0x0009D0A0
	public static event EasyTouch.DragStart2FingersHandler On_DragStart2Fingers;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06000E64 RID: 3684 RVA: 0x0009EED4 File Offset: 0x0009D0D4
	// (remove) Token: 0x06000E65 RID: 3685 RVA: 0x0009EF08 File Offset: 0x0009D108
	public static event EasyTouch.Drag2FingersHandler On_Drag2Fingers;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06000E66 RID: 3686 RVA: 0x0009EF3C File Offset: 0x0009D13C
	// (remove) Token: 0x06000E67 RID: 3687 RVA: 0x0009EF70 File Offset: 0x0009D170
	public static event EasyTouch.DragEnd2FingersHandler On_DragEnd2Fingers;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06000E68 RID: 3688 RVA: 0x0009EFA4 File Offset: 0x0009D1A4
	// (remove) Token: 0x06000E69 RID: 3689 RVA: 0x0009EFD8 File Offset: 0x0009D1D8
	public static event EasyTouch.SwipeStart2FingersHandler On_SwipeStart2Fingers;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06000E6A RID: 3690 RVA: 0x0009F00C File Offset: 0x0009D20C
	// (remove) Token: 0x06000E6B RID: 3691 RVA: 0x0009F040 File Offset: 0x0009D240
	public static event EasyTouch.Swipe2FingersHandler On_Swipe2Fingers;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06000E6C RID: 3692 RVA: 0x0009F074 File Offset: 0x0009D274
	// (remove) Token: 0x06000E6D RID: 3693 RVA: 0x0009F0A8 File Offset: 0x0009D2A8
	public static event EasyTouch.SwipeEnd2FingersHandler On_SwipeEnd2Fingers;

	// Token: 0x06000E6E RID: 3694 RVA: 0x0009F0DC File Offset: 0x0009D2DC
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

	// Token: 0x06000E6F RID: 3695 RVA: 0x0000F654 File Offset: 0x0000D854
	private void OnEnable()
	{
		if (Application.isPlaying && Application.isEditor)
		{
			this.InitEasyTouch();
		}
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0000F66A File Offset: 0x0000D86A
	private void Start()
	{
		this.InitEasyTouch();
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0009F210 File Offset: 0x0009D410
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

	// Token: 0x06000E72 RID: 3698 RVA: 0x0009F298 File Offset: 0x0009D498
	private void OnGUI()
	{
		Vector2 secondFingerPosition = this.input.GetSecondFingerPosition();
		if (secondFingerPosition != new Vector2(-1f, -1f))
		{
			GUI.DrawTexture(new Rect(secondFingerPosition.x - 16f, (float)Screen.height - secondFingerPosition.y - 16f, 32f, 32f), this.secondFingerTexture);
		}
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x000042DD File Offset: 0x000024DD
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0009F304 File Offset: 0x0009D504
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

	// Token: 0x06000E75 RID: 3701 RVA: 0x0009F3E8 File Offset: 0x0009D5E8
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

	// Token: 0x06000E76 RID: 3702 RVA: 0x0009F56C File Offset: 0x0009D76C
	private void ResetTouches()
	{
		for (int i = 0; i < 10; i++)
		{
			this.fingers[i] = null;
		}
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0009F590 File Offset: 0x0009D790
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

	// Token: 0x06000E78 RID: 3704 RVA: 0x0009FC28 File Offset: 0x0009DE28
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

	// Token: 0x06000E79 RID: 3705 RVA: 0x0009FD34 File Offset: 0x0009DF34
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

	// Token: 0x06000E7A RID: 3706 RVA: 0x0009FDC4 File Offset: 0x0009DFC4
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

	// Token: 0x06000E7B RID: 3707 RVA: 0x000A0610 File Offset: 0x0009E810
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

	// Token: 0x06000E7C RID: 3708 RVA: 0x000A0648 File Offset: 0x0009E848
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

	// Token: 0x06000E7D RID: 3709 RVA: 0x000A0818 File Offset: 0x0009EA18
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

	// Token: 0x06000E7E RID: 3710 RVA: 0x000A0958 File Offset: 0x0009EB58
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

	// Token: 0x06000E7F RID: 3711 RVA: 0x000A09E0 File Offset: 0x0009EBE0
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

	// Token: 0x06000E80 RID: 3712 RVA: 0x000A0D70 File Offset: 0x0009EF70
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

	// Token: 0x06000E81 RID: 3713 RVA: 0x000A0DD0 File Offset: 0x0009EFD0
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

	// Token: 0x06000E82 RID: 3714 RVA: 0x000A0E64 File Offset: 0x0009F064
	private bool FingerInTolerance(Finger finger)
	{
		return (finger.position - finger.startPosition).sqrMagnitude <= this.StationnaryTolerance * this.StationnaryTolerance;
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0000F672 File Offset: 0x0000D872
	private float DeltaAngle(Vector2 start, Vector2 end)
	{
		return Mathf.Atan2(start.x * end.y - start.y * end.x, Vector2.Dot(start, end));
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000A0E9C File Offset: 0x0009F09C
	private float TwistAngle()
	{
		Vector2 end = this.fingers[this.twoFinger0].position - this.fingers[this.twoFinger1].position;
		Vector2 start = this.fingers[this.twoFinger0].oldPosition - this.fingers[this.twoFinger1].oldPosition;
		return 57.29578f * this.DeltaAngle(start, end);
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x000A0F0C File Offset: 0x0009F10C
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

	// Token: 0x06000E86 RID: 3718 RVA: 0x000A0F7C File Offset: 0x0009F17C
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

	// Token: 0x06000E87 RID: 3719 RVA: 0x000A1008 File Offset: 0x0009F208
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

	// Token: 0x06000E88 RID: 3720 RVA: 0x0000F69B File Offset: 0x0000D89B
	public static void SetEnabled(bool enable)
	{
		EasyTouch.instance.enable = enable;
		if (enable)
		{
			EasyTouch.instance.ResetTouches();
		}
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0000F6B5 File Offset: 0x0000D8B5
	public static bool GetEnabled()
	{
		return EasyTouch.instance.enable;
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0000F6C1 File Offset: 0x0000D8C1
	public static int GetTouchCount()
	{
		return EasyTouch.instance.input.TouchCount();
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0000F6D2 File Offset: 0x0000D8D2
	public static void SetCamera(Camera cam)
	{
		EasyTouch.instance.easyTouchCamera = cam;
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0000F6DF File Offset: 0x0000D8DF
	public static Camera GetCamera()
	{
		return EasyTouch.instance.easyTouchCamera;
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0000F6EB File Offset: 0x0000D8EB
	public static void SetEnable2FingersGesture(bool enable)
	{
		EasyTouch.instance.enable2FingersGesture = enable;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
	public static bool GetEnable2FingersGesture()
	{
		return EasyTouch.instance.enable2FingersGesture;
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x0000F704 File Offset: 0x0000D904
	public static void SetEnableTwist(bool enable)
	{
		EasyTouch.instance.enableTwist = enable;
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x0000F711 File Offset: 0x0000D911
	public static bool GetEnableTwist()
	{
		return EasyTouch.instance.enableTwist;
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0000F71D File Offset: 0x0000D91D
	public static void SetEnablePinch(bool enable)
	{
		EasyTouch.instance.enablePinch = enable;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0000F72A File Offset: 0x0000D92A
	public static bool GetEnablePinch()
	{
		return EasyTouch.instance.enablePinch;
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0000F736 File Offset: 0x0000D936
	public static void SetEnableAutoSelect(bool enable)
	{
		EasyTouch.instance.autoSelect = enable;
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0000F743 File Offset: 0x0000D943
	public static bool GetEnableAutoSelect()
	{
		return EasyTouch.instance.autoSelect;
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0000F74F File Offset: 0x0000D94F
	public static void SetOtherReceiverObject(GameObject receiver)
	{
		EasyTouch.instance.receiverObject = receiver;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0000F75C File Offset: 0x0000D95C
	public static GameObject GetOtherReceiverObject()
	{
		return EasyTouch.instance.receiverObject;
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0000F768 File Offset: 0x0000D968
	public static void SetStationnaryTolerance(float tolerance)
	{
		EasyTouch.instance.StationnaryTolerance = tolerance;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0000F775 File Offset: 0x0000D975
	public static float GetStationnaryTolerance()
	{
		return EasyTouch.instance.StationnaryTolerance;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0000F781 File Offset: 0x0000D981
	public static void SetlongTapTime(float time)
	{
		EasyTouch.instance.longTapTime = time;
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0000F78E File Offset: 0x0000D98E
	public static float GetlongTapTime()
	{
		return EasyTouch.instance.longTapTime;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0000F79A File Offset: 0x0000D99A
	public static void SetSwipeTolerance(float tolerance)
	{
		EasyTouch.instance.swipeTolerance = tolerance;
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0000F7A7 File Offset: 0x0000D9A7
	public static float GetSwipeTolerance()
	{
		return EasyTouch.instance.swipeTolerance;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0000F7B3 File Offset: 0x0000D9B3
	public static void SetMinPinchLength(float length)
	{
		EasyTouch.instance.minPinchLength = length;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
	public static float GetMinPinchLength()
	{
		return EasyTouch.instance.minPinchLength;
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0000F7CC File Offset: 0x0000D9CC
	public static void SetMinTwistAngle(float angle)
	{
		EasyTouch.instance.minTwistAngle = angle;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
	public static float GetMinTwistAngle()
	{
		return EasyTouch.instance.minTwistAngle;
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0000F7E5 File Offset: 0x0000D9E5
	public static GameObject GetCurrentPickedObject(int fingerIndex)
	{
		return EasyTouch.instance.GetPickeGameObject(EasyTouch.instance.GetFinger(fingerIndex).position);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x000A104C File Offset: 0x0009F24C
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

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0000F801 File Offset: 0x0000DA01
	public static Vector2 GetFingerPosition(int fingerIndex)
	{
		if (EasyTouch.instance.fingers[fingerIndex] != null)
		{
			return EasyTouch.instance.GetFinger(fingerIndex).position;
		}
		return Vector2.zero;
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0000F827 File Offset: 0x0000DA27
	public static bool GetIsReservedArea()
	{
		return EasyTouch.instance.enableReservedArea;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0000F833 File Offset: 0x0000DA33
	public static void SetIsReservedArea(bool enable)
	{
		EasyTouch.instance.enableReservedArea = enable;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x0000F840 File Offset: 0x0000DA40
	public static void AddReservedArea(Rect rec)
	{
		EasyTouch.instance.reservedAreas.Add(rec);
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0000F852 File Offset: 0x0000DA52
	public static void RemoveReservedArea(Rect rec)
	{
		EasyTouch.instance.reservedAreas.Remove(rec);
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0000F865 File Offset: 0x0000DA65
	public static void ResetTouch(int fingerIndex)
	{
		EasyTouch.instance.GetFinger(fingerIndex).gesture = EasyTouch.GestureType.None;
	}

	// Token: 0x04000B69 RID: 2921
	public bool enable = true;

	// Token: 0x04000B6A RID: 2922
	public bool enableRemote;

	// Token: 0x04000B6B RID: 2923
	public bool useBroadcastMessage = true;

	// Token: 0x04000B6C RID: 2924
	public GameObject receiverObject;

	// Token: 0x04000B6D RID: 2925
	public bool isExtension;

	// Token: 0x04000B6E RID: 2926
	public bool enable2FingersGesture = true;

	// Token: 0x04000B6F RID: 2927
	public bool enableTwist = true;

	// Token: 0x04000B70 RID: 2928
	public bool enablePinch = true;

	// Token: 0x04000B71 RID: 2929
	public Camera easyTouchCamera;

	// Token: 0x04000B72 RID: 2930
	public bool autoSelect;

	// Token: 0x04000B73 RID: 2931
	public LayerMask pickableLayers;

	// Token: 0x04000B74 RID: 2932
	public float StationnaryTolerance = 25f;

	// Token: 0x04000B75 RID: 2933
	public float longTapTime = 1f;

	// Token: 0x04000B76 RID: 2934
	public float swipeTolerance = 0.85f;

	// Token: 0x04000B77 RID: 2935
	public float minPinchLength;

	// Token: 0x04000B78 RID: 2936
	public float minTwistAngle = 1f;

	// Token: 0x04000B79 RID: 2937
	public bool enabledNGuiMode;

	// Token: 0x04000B7A RID: 2938
	public LayerMask nGUILayers;

	// Token: 0x04000B7B RID: 2939
	public List<Camera> nGUICameras = new List<Camera>();

	// Token: 0x04000B7C RID: 2940
	private bool isStartHoverNGUI;

	// Token: 0x04000B7D RID: 2941
	public List<Rect> reservedAreas = new List<Rect>();

	// Token: 0x04000B7E RID: 2942
	public bool enableReservedArea = true;

	// Token: 0x04000B7F RID: 2943
	public KeyCode twistKey = 308;

	// Token: 0x04000B80 RID: 2944
	public KeyCode swipeKey = 306;

	// Token: 0x04000B81 RID: 2945
	public bool showGeneral = true;

	// Token: 0x04000B82 RID: 2946
	public bool showSelect = true;

	// Token: 0x04000B83 RID: 2947
	public bool showGesture = true;

	// Token: 0x04000B84 RID: 2948
	public bool showTwoFinger = true;

	// Token: 0x04000B85 RID: 2949
	public bool showSecondFinger = true;

	// Token: 0x04000B86 RID: 2950
	public static EasyTouch instance;

	// Token: 0x04000B87 RID: 2951
	private EasyTouchInput input;

	// Token: 0x04000B88 RID: 2952
	private EasyTouch.GestureType complexCurrentGesture = EasyTouch.GestureType.None;

	// Token: 0x04000B89 RID: 2953
	private EasyTouch.GestureType oldGesture = EasyTouch.GestureType.None;

	// Token: 0x04000B8A RID: 2954
	private float startTimeAction;

	// Token: 0x04000B8B RID: 2955
	private Finger[] fingers = new Finger[10];

	// Token: 0x04000B8C RID: 2956
	private GameObject pickObject2Finger;

	// Token: 0x04000B8D RID: 2957
	private GameObject oldPickObject2Finger;

	// Token: 0x04000B8E RID: 2958
	public Texture secondFingerTexture;

	// Token: 0x04000B8F RID: 2959
	private Vector2 startPosition2Finger;

	// Token: 0x04000B90 RID: 2960
	private int twoFinger0;

	// Token: 0x04000B91 RID: 2961
	private int twoFinger1;

	// Token: 0x04000B92 RID: 2962
	private Vector2 oldStartPosition2Finger;

	// Token: 0x04000B93 RID: 2963
	private float oldFingerDistance;

	// Token: 0x04000B94 RID: 2964
	private bool twoFingerDragStart;

	// Token: 0x04000B95 RID: 2965
	private bool twoFingerSwipeStart;

	// Token: 0x04000B96 RID: 2966
	private int oldTouchCount;

	// Token: 0x020001AC RID: 428
	// (Invoke) Token: 0x06000EAA RID: 3754
	public delegate void TouchCancelHandler(Gesture gesture);

	// Token: 0x020001AD RID: 429
	// (Invoke) Token: 0x06000EAE RID: 3758
	public delegate void Cancel2FingersHandler(Gesture gesture);

	// Token: 0x020001AE RID: 430
	// (Invoke) Token: 0x06000EB2 RID: 3762
	public delegate void TouchStartHandler(Gesture gesture);

	// Token: 0x020001AF RID: 431
	// (Invoke) Token: 0x06000EB6 RID: 3766
	public delegate void TouchDownHandler(Gesture gesture);

	// Token: 0x020001B0 RID: 432
	// (Invoke) Token: 0x06000EBA RID: 3770
	public delegate void TouchUpHandler(Gesture gesture);

	// Token: 0x020001B1 RID: 433
	// (Invoke) Token: 0x06000EBE RID: 3774
	public delegate void SimpleTapHandler(Gesture gesture);

	// Token: 0x020001B2 RID: 434
	// (Invoke) Token: 0x06000EC2 RID: 3778
	public delegate void DoubleTapHandler(Gesture gesture);

	// Token: 0x020001B3 RID: 435
	// (Invoke) Token: 0x06000EC6 RID: 3782
	public delegate void LongTapStartHandler(Gesture gesture);

	// Token: 0x020001B4 RID: 436
	// (Invoke) Token: 0x06000ECA RID: 3786
	public delegate void LongTapHandler(Gesture gesture);

	// Token: 0x020001B5 RID: 437
	// (Invoke) Token: 0x06000ECE RID: 3790
	public delegate void LongTapEndHandler(Gesture gesture);

	// Token: 0x020001B6 RID: 438
	// (Invoke) Token: 0x06000ED2 RID: 3794
	public delegate void DragStartHandler(Gesture gesture);

	// Token: 0x020001B7 RID: 439
	// (Invoke) Token: 0x06000ED6 RID: 3798
	public delegate void DragHandler(Gesture gesture);

	// Token: 0x020001B8 RID: 440
	// (Invoke) Token: 0x06000EDA RID: 3802
	public delegate void DragEndHandler(Gesture gesture);

	// Token: 0x020001B9 RID: 441
	// (Invoke) Token: 0x06000EDE RID: 3806
	public delegate void SwipeStartHandler(Gesture gesture);

	// Token: 0x020001BA RID: 442
	// (Invoke) Token: 0x06000EE2 RID: 3810
	public delegate void SwipeHandler(Gesture gesture);

	// Token: 0x020001BB RID: 443
	// (Invoke) Token: 0x06000EE6 RID: 3814
	public delegate void SwipeEndHandler(Gesture gesture);

	// Token: 0x020001BC RID: 444
	// (Invoke) Token: 0x06000EEA RID: 3818
	public delegate void TouchStart2FingersHandler(Gesture gesture);

	// Token: 0x020001BD RID: 445
	// (Invoke) Token: 0x06000EEE RID: 3822
	public delegate void TouchDown2FingersHandler(Gesture gesture);

	// Token: 0x020001BE RID: 446
	// (Invoke) Token: 0x06000EF2 RID: 3826
	public delegate void TouchUp2FingersHandler(Gesture gesture);

	// Token: 0x020001BF RID: 447
	// (Invoke) Token: 0x06000EF6 RID: 3830
	public delegate void SimpleTap2FingersHandler(Gesture gesture);

	// Token: 0x020001C0 RID: 448
	// (Invoke) Token: 0x06000EFA RID: 3834
	public delegate void DoubleTap2FingersHandler(Gesture gesture);

	// Token: 0x020001C1 RID: 449
	// (Invoke) Token: 0x06000EFE RID: 3838
	public delegate void LongTapStart2FingersHandler(Gesture gesture);

	// Token: 0x020001C2 RID: 450
	// (Invoke) Token: 0x06000F02 RID: 3842
	public delegate void LongTap2FingersHandler(Gesture gesture);

	// Token: 0x020001C3 RID: 451
	// (Invoke) Token: 0x06000F06 RID: 3846
	public delegate void LongTapEnd2FingersHandler(Gesture gesture);

	// Token: 0x020001C4 RID: 452
	// (Invoke) Token: 0x06000F0A RID: 3850
	public delegate void TwistHandler(Gesture gesture);

	// Token: 0x020001C5 RID: 453
	// (Invoke) Token: 0x06000F0E RID: 3854
	public delegate void TwistEndHandler(Gesture gesture);

	// Token: 0x020001C6 RID: 454
	// (Invoke) Token: 0x06000F12 RID: 3858
	public delegate void PinchInHandler(Gesture gesture);

	// Token: 0x020001C7 RID: 455
	// (Invoke) Token: 0x06000F16 RID: 3862
	public delegate void PinchOutHandler(Gesture gesture);

	// Token: 0x020001C8 RID: 456
	// (Invoke) Token: 0x06000F1A RID: 3866
	public delegate void PinchEndHandler(Gesture gesture);

	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x06000F1E RID: 3870
	public delegate void DragStart2FingersHandler(Gesture gesture);

	// Token: 0x020001CA RID: 458
	// (Invoke) Token: 0x06000F22 RID: 3874
	public delegate void Drag2FingersHandler(Gesture gesture);

	// Token: 0x020001CB RID: 459
	// (Invoke) Token: 0x06000F26 RID: 3878
	public delegate void DragEnd2FingersHandler(Gesture gesture);

	// Token: 0x020001CC RID: 460
	// (Invoke) Token: 0x06000F2A RID: 3882
	public delegate void SwipeStart2FingersHandler(Gesture gesture);

	// Token: 0x020001CD RID: 461
	// (Invoke) Token: 0x06000F2E RID: 3886
	public delegate void Swipe2FingersHandler(Gesture gesture);

	// Token: 0x020001CE RID: 462
	// (Invoke) Token: 0x06000F32 RID: 3890
	public delegate void SwipeEnd2FingersHandler(Gesture gesture);

	// Token: 0x020001CF RID: 463
	public enum GestureType
	{
		// Token: 0x04000B98 RID: 2968
		Tap,
		// Token: 0x04000B99 RID: 2969
		Drag,
		// Token: 0x04000B9A RID: 2970
		Swipe,
		// Token: 0x04000B9B RID: 2971
		None,
		// Token: 0x04000B9C RID: 2972
		LongTap,
		// Token: 0x04000B9D RID: 2973
		Pinch,
		// Token: 0x04000B9E RID: 2974
		Twist,
		// Token: 0x04000B9F RID: 2975
		Cancel,
		// Token: 0x04000BA0 RID: 2976
		Acquisition
	}

	// Token: 0x020001D0 RID: 464
	public enum SwipeType
	{
		// Token: 0x04000BA2 RID: 2978
		None,
		// Token: 0x04000BA3 RID: 2979
		Left,
		// Token: 0x04000BA4 RID: 2980
		Right,
		// Token: 0x04000BA5 RID: 2981
		Up,
		// Token: 0x04000BA6 RID: 2982
		Down,
		// Token: 0x04000BA7 RID: 2983
		Other
	}

	// Token: 0x020001D1 RID: 465
	private enum EventName
	{
		// Token: 0x04000BA9 RID: 2985
		None,
		// Token: 0x04000BAA RID: 2986
		On_Cancel,
		// Token: 0x04000BAB RID: 2987
		On_Cancel2Fingers,
		// Token: 0x04000BAC RID: 2988
		On_TouchStart,
		// Token: 0x04000BAD RID: 2989
		On_TouchDown,
		// Token: 0x04000BAE RID: 2990
		On_TouchUp,
		// Token: 0x04000BAF RID: 2991
		On_SimpleTap,
		// Token: 0x04000BB0 RID: 2992
		On_DoubleTap,
		// Token: 0x04000BB1 RID: 2993
		On_LongTapStart,
		// Token: 0x04000BB2 RID: 2994
		On_LongTap,
		// Token: 0x04000BB3 RID: 2995
		On_LongTapEnd,
		// Token: 0x04000BB4 RID: 2996
		On_DragStart,
		// Token: 0x04000BB5 RID: 2997
		On_Drag,
		// Token: 0x04000BB6 RID: 2998
		On_DragEnd,
		// Token: 0x04000BB7 RID: 2999
		On_SwipeStart,
		// Token: 0x04000BB8 RID: 3000
		On_Swipe,
		// Token: 0x04000BB9 RID: 3001
		On_SwipeEnd,
		// Token: 0x04000BBA RID: 3002
		On_TouchStart2Fingers,
		// Token: 0x04000BBB RID: 3003
		On_TouchDown2Fingers,
		// Token: 0x04000BBC RID: 3004
		On_TouchUp2Fingers,
		// Token: 0x04000BBD RID: 3005
		On_SimpleTap2Fingers,
		// Token: 0x04000BBE RID: 3006
		On_DoubleTap2Fingers,
		// Token: 0x04000BBF RID: 3007
		On_LongTapStart2Fingers,
		// Token: 0x04000BC0 RID: 3008
		On_LongTap2Fingers,
		// Token: 0x04000BC1 RID: 3009
		On_LongTapEnd2Fingers,
		// Token: 0x04000BC2 RID: 3010
		On_Twist,
		// Token: 0x04000BC3 RID: 3011
		On_TwistEnd,
		// Token: 0x04000BC4 RID: 3012
		On_PinchIn,
		// Token: 0x04000BC5 RID: 3013
		On_PinchOut,
		// Token: 0x04000BC6 RID: 3014
		On_PinchEnd,
		// Token: 0x04000BC7 RID: 3015
		On_DragStart2Fingers,
		// Token: 0x04000BC8 RID: 3016
		On_Drag2Fingers,
		// Token: 0x04000BC9 RID: 3017
		On_DragEnd2Fingers,
		// Token: 0x04000BCA RID: 3018
		On_SwipeStart2Fingers,
		// Token: 0x04000BCB RID: 3019
		On_Swipe2Fingers,
		// Token: 0x04000BCC RID: 3020
		On_SwipeEnd2Fingers
	}
}
