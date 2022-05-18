using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class C_EasyJoystickTemplate : MonoBehaviour
{
	// Token: 0x06000D65 RID: 3429 RVA: 0x0009B6E4 File Offset: 0x000998E4
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

	// Token: 0x06000D66 RID: 3430 RVA: 0x0009B768 File Offset: 0x00099968
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

	// Token: 0x06000D67 RID: 3431 RVA: 0x0009B768 File Offset: 0x00099968
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

	// Token: 0x06000D68 RID: 3432 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickDoubleTap(MovingJoystick move)
	{
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickTap(MovingJoystick move)
	{
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickTouchUp(MovingJoystick move)
	{
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickMoveEnd(MovingJoystick move)
	{
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickMove(MovingJoystick move)
	{
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickMoveStart(MovingJoystick move)
	{
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x000042DD File Offset: 0x000024DD
	private void On_JoystickTouchStart(MovingJoystick move)
	{
	}
}
