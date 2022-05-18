using System;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class MovingJoystick
{
	// Token: 0x06000F49 RID: 3913 RVA: 0x000A18A8 File Offset: 0x0009FAA8
	public float Axis2Angle(bool inDegree = true)
	{
		float num = Mathf.Atan2(this.joystickAxis.x, this.joystickAxis.y);
		if (inDegree)
		{
			return num * 57.29578f;
		}
		return num;
	}

	// Token: 0x04000BF3 RID: 3059
	public string joystickName;

	// Token: 0x04000BF4 RID: 3060
	public Vector2 joystickAxis;

	// Token: 0x04000BF5 RID: 3061
	public Vector2 joystickValue;

	// Token: 0x04000BF6 RID: 3062
	public EasyJoystick joystick;
}
