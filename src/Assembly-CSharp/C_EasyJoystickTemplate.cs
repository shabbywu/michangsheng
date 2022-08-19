using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class C_EasyJoystickTemplate : MonoBehaviour
{
	// Token: 0x06000C44 RID: 3140 RVA: 0x00049ED0 File Offset: 0x000480D0
	private void OnEnable()
	{
		EasyJoystick.On_JoystickTouchStart += this.On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart += this.On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove += this.On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += this.On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp += this.On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap += this.On_JoystickTap;
		EasyJoystick.On_JoystickDoubleTap += this.On_JoystickDoubleTap;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00049F54 File Offset: 0x00048154
	private void OnDisable()
	{
		EasyJoystick.On_JoystickTouchStart -= this.On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart -= this.On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove -= this.On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= this.On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp -= this.On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap -= this.On_JoystickTap;
		EasyJoystick.On_JoystickDoubleTap -= this.On_JoystickDoubleTap;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00049FD8 File Offset: 0x000481D8
	private void OnDestroy()
	{
		EasyJoystick.On_JoystickTouchStart -= this.On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart -= this.On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove -= this.On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= this.On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp -= this.On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap -= this.On_JoystickTap;
		EasyJoystick.On_JoystickDoubleTap -= this.On_JoystickDoubleTap;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickDoubleTap(MovingJoystick move)
	{
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickTap(MovingJoystick move)
	{
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickTouchUp(MovingJoystick move)
	{
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickMoveEnd(MovingJoystick move)
	{
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickMove(MovingJoystick move)
	{
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickMoveStart(MovingJoystick move)
	{
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00004095 File Offset: 0x00002295
	private void On_JoystickTouchStart(MovingJoystick move)
	{
	}
}
