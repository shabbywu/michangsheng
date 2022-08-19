using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class C_EasyTouchTemplate : MonoBehaviour
{
	// Token: 0x06000C4F RID: 3151 RVA: 0x0004A05C File Offset: 0x0004825C
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

	// Token: 0x06000C50 RID: 3152 RVA: 0x0004A2AC File Offset: 0x000484AC
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

	// Token: 0x06000C51 RID: 3153 RVA: 0x0004A4FC File Offset: 0x000486FC
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

	// Token: 0x06000C52 RID: 3154 RVA: 0x00004095 File Offset: 0x00002295
	private void On_Cancel(Gesture gesture)
	{
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TouchStart(Gesture gesture)
	{
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TouchDown(Gesture gesture)
	{
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TouchUp(Gesture gesture)
	{
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00004095 File Offset: 0x00002295
	private void On_SimpleTap(Gesture gesture)
	{
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00004095 File Offset: 0x00002295
	private void On_DoubleTap(Gesture gesture)
	{
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00004095 File Offset: 0x00002295
	private void On_LongTapStart(Gesture gesture)
	{
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00004095 File Offset: 0x00002295
	private void On_LongTap(Gesture gesture)
	{
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00004095 File Offset: 0x00002295
	private void On_LongTapEnd(Gesture gesture)
	{
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00004095 File Offset: 0x00002295
	private void On_DragStart(Gesture gesture)
	{
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00004095 File Offset: 0x00002295
	private void On_Drag(Gesture gesture)
	{
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00004095 File Offset: 0x00002295
	private void On_DragEnd(Gesture gesture)
	{
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00004095 File Offset: 0x00002295
	private void On_SwipeStart(Gesture gesture)
	{
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00004095 File Offset: 0x00002295
	private void On_Swipe(Gesture gesture)
	{
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x00004095 File Offset: 0x00002295
	private void On_SwipeEnd(Gesture gesture)
	{
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TouchStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TouchDown2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TouchUp2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00004095 File Offset: 0x00002295
	private void On_SimpleTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00004095 File Offset: 0x00002295
	private void On_DoubleTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00004095 File Offset: 0x00002295
	private void On_LongTapStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00004095 File Offset: 0x00002295
	private void On_LongTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00004095 File Offset: 0x00002295
	private void On_LongTapEnd2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x00004095 File Offset: 0x00002295
	private void On_Twist(Gesture gesture)
	{
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00004095 File Offset: 0x00002295
	private void On_TwistEnd(Gesture gesture)
	{
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00004095 File Offset: 0x00002295
	private void On_PinchIn(Gesture gesture)
	{
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x00004095 File Offset: 0x00002295
	private void On_PinchOut(Gesture gesture)
	{
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00004095 File Offset: 0x00002295
	private void On_PinchEnd(Gesture gesture)
	{
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00004095 File Offset: 0x00002295
	private void On_DragStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00004095 File Offset: 0x00002295
	private void On_Drag2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00004095 File Offset: 0x00002295
	private void On_DragEnd2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00004095 File Offset: 0x00002295
	private void On_SwipeStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00004095 File Offset: 0x00002295
	private void On_Swipe2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x00004095 File Offset: 0x00002295
	private void On_SwipeEnd2Fingers(Gesture gesture)
	{
	}
}
