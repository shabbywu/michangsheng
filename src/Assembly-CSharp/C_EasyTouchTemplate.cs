using System;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class C_EasyTouchTemplate : MonoBehaviour
{
	// Token: 0x06000D70 RID: 3440 RVA: 0x0009B7EC File Offset: 0x000999EC
	private void OnEnable()
	{
		EasyTouch.On_Cancel += this.On_Cancel;
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
		EasyTouch.On_LongTapStart += this.On_LongTapStart;
		EasyTouch.On_LongTap += this.On_LongTap;
		EasyTouch.On_LongTapEnd += this.On_LongTapEnd;
		EasyTouch.On_DragStart += this.On_DragStart;
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_DragEnd += this.On_DragEnd;
		EasyTouch.On_SwipeStart += this.On_SwipeStart;
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers += this.On_SimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers += this.On_DoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers += this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers += this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers += this.On_LongTapEnd2Fingers;
		EasyTouch.On_Twist += this.On_Twist;
		EasyTouch.On_TwistEnd += this.On_TwistEnd;
		EasyTouch.On_PinchIn += this.On_PinchIn;
		EasyTouch.On_PinchOut += this.On_PinchOut;
		EasyTouch.On_PinchEnd += this.On_PinchEnd;
		EasyTouch.On_DragStart2Fingers += this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers += this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers += this.On_DragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers += this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers += this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers += this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0009BA3C File Offset: 0x00099C3C
	private void OnDisable()
	{
		EasyTouch.On_Cancel -= this.On_Cancel;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		EasyTouch.On_LongTapStart -= this.On_LongTapStart;
		EasyTouch.On_LongTap -= this.On_LongTap;
		EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
		EasyTouch.On_DragStart -= this.On_DragStart;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DragEnd -= this.On_DragEnd;
		EasyTouch.On_SwipeStart -= this.On_SwipeStart;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
		EasyTouch.On_Twist -= this.On_Twist;
		EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		EasyTouch.On_PinchIn -= this.On_PinchIn;
		EasyTouch.On_PinchOut -= this.On_PinchOut;
		EasyTouch.On_PinchEnd -= this.On_PinchEnd;
		EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0009BA3C File Offset: 0x00099C3C
	private void OnDestroy()
	{
		EasyTouch.On_Cancel -= this.On_Cancel;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		EasyTouch.On_LongTapStart -= this.On_LongTapStart;
		EasyTouch.On_LongTap -= this.On_LongTap;
		EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
		EasyTouch.On_DragStart -= this.On_DragStart;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DragEnd -= this.On_DragEnd;
		EasyTouch.On_SwipeStart -= this.On_SwipeStart;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
		EasyTouch.On_Twist -= this.On_Twist;
		EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		EasyTouch.On_PinchIn -= this.On_PinchIn;
		EasyTouch.On_PinchOut -= this.On_PinchOut;
		EasyTouch.On_PinchEnd -= this.On_PinchEnd;
		EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_Cancel(Gesture gesture)
	{
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TouchStart(Gesture gesture)
	{
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TouchDown(Gesture gesture)
	{
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TouchUp(Gesture gesture)
	{
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_SimpleTap(Gesture gesture)
	{
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_DoubleTap(Gesture gesture)
	{
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_LongTapStart(Gesture gesture)
	{
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_LongTap(Gesture gesture)
	{
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_LongTapEnd(Gesture gesture)
	{
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_DragStart(Gesture gesture)
	{
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_Drag(Gesture gesture)
	{
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_DragEnd(Gesture gesture)
	{
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_SwipeStart(Gesture gesture)
	{
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_Swipe(Gesture gesture)
	{
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_SwipeEnd(Gesture gesture)
	{
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TouchStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TouchDown2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TouchUp2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_SimpleTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_DoubleTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_LongTapStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_LongTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_LongTapEnd2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_Twist(Gesture gesture)
	{
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_TwistEnd(Gesture gesture)
	{
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_PinchIn(Gesture gesture)
	{
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_PinchOut(Gesture gesture)
	{
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_PinchEnd(Gesture gesture)
	{
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_DragStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_Drag2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_DragEnd2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_SwipeStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_Swipe2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_SwipeEnd2Fingers(Gesture gesture)
	{
	}
}
