using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
[ExecuteInEditMode]
public class EasyJoystick : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000DBF RID: 3519 RVA: 0x0009C5D4 File Offset: 0x0009A7D4
	// (remove) Token: 0x06000DC0 RID: 3520 RVA: 0x0009C608 File Offset: 0x0009A808
	public static event EasyJoystick.JoystickMoveStartHandler On_JoystickMoveStart;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000DC1 RID: 3521 RVA: 0x0009C63C File Offset: 0x0009A83C
	// (remove) Token: 0x06000DC2 RID: 3522 RVA: 0x0009C670 File Offset: 0x0009A870
	public static event EasyJoystick.JoystickMoveHandler On_JoystickMove;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000DC3 RID: 3523 RVA: 0x0009C6A4 File Offset: 0x0009A8A4
	// (remove) Token: 0x06000DC4 RID: 3524 RVA: 0x0009C6D8 File Offset: 0x0009A8D8
	public static event EasyJoystick.JoystickMoveEndHandler On_JoystickMoveEnd;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000DC5 RID: 3525 RVA: 0x0009C70C File Offset: 0x0009A90C
	// (remove) Token: 0x06000DC6 RID: 3526 RVA: 0x0009C740 File Offset: 0x0009A940
	public static event EasyJoystick.JoystickTouchStartHandler On_JoystickTouchStart;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000DC7 RID: 3527 RVA: 0x0009C774 File Offset: 0x0009A974
	// (remove) Token: 0x06000DC8 RID: 3528 RVA: 0x0009C7A8 File Offset: 0x0009A9A8
	public static event EasyJoystick.JoystickTapHandler On_JoystickTap;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000DC9 RID: 3529 RVA: 0x0009C7DC File Offset: 0x0009A9DC
	// (remove) Token: 0x06000DCA RID: 3530 RVA: 0x0009C810 File Offset: 0x0009AA10
	public static event EasyJoystick.JoystickDoubleTapHandler On_JoystickDoubleTap;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06000DCB RID: 3531 RVA: 0x0009C844 File Offset: 0x0009AA44
	// (remove) Token: 0x06000DCC RID: 3532 RVA: 0x0009C878 File Offset: 0x0009AA78
	public static event EasyJoystick.JoystickTouchUpHandler On_JoystickTouchUp;

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0000F2F1 File Offset: 0x0000D4F1
	public Vector2 JoystickAxis
	{
		get
		{
			return this.joystickAxis;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
	// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0009C8AC File Offset: 0x0009AAAC
	public Vector2 JoystickTouch
	{
		get
		{
			return new Vector2(this.joystickTouch.x / this.zoneRadius, this.joystickTouch.y / this.zoneRadius);
		}
		set
		{
			float num = Mathf.Clamp(value.x, -1f, 1f) * this.zoneRadius;
			float num2 = Mathf.Clamp(value.y, -1f, 1f) * this.zoneRadius;
			this.joystickTouch = new Vector2(num, num2);
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0000F324 File Offset: 0x0000D524
	public Vector2 JoystickValue
	{
		get
		{
			return this.joystickValue;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0000F32C File Offset: 0x0000D52C
	// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x0000F334 File Offset: 0x0000D534
	public bool DynamicJoystick
	{
		get
		{
			return this.dynamicJoystick;
		}
		set
		{
			if (!Application.isPlaying)
			{
				this.joystickIndex = -1;
				this.dynamicJoystick = value;
				if (this.dynamicJoystick)
				{
					this.virtualJoystick = false;
					return;
				}
				this.virtualJoystick = true;
				this.joystickCenter = this.joystickPositionOffset;
			}
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0000F36E File Offset: 0x0000D56E
	// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x0000F376 File Offset: 0x0000D576
	public EasyJoystick.JoystickAnchor JoyAnchor
	{
		get
		{
			return this.joyAnchor;
		}
		set
		{
			this.joyAnchor = value;
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0000F38B File Offset: 0x0000D58B
	// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x0000F393 File Offset: 0x0000D593
	public Vector2 JoystickPositionOffset
	{
		get
		{
			return this.joystickPositionOffset;
		}
		set
		{
			this.joystickPositionOffset = value;
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
	// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
	public float ZoneRadius
	{
		get
		{
			return this.zoneRadius;
		}
		set
		{
			this.zoneRadius = value;
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0000F3C5 File Offset: 0x0000D5C5
	// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0009C900 File Offset: 0x0009AB00
	public float TouchSize
	{
		get
		{
			return this.touchSize;
		}
		set
		{
			this.touchSize = value;
			if (this.touchSize > this.zoneRadius / 2f && this.restrictArea)
			{
				this.touchSize = this.zoneRadius / 2f;
			}
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0000F3CD File Offset: 0x0000D5CD
	// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0000F3D5 File Offset: 0x0000D5D5
	public bool RestrictArea
	{
		get
		{
			return this.restrictArea;
		}
		set
		{
			this.restrictArea = value;
			if (this.restrictArea)
			{
				this.touchSizeCoef = this.touchSize;
			}
			else
			{
				this.touchSizeCoef = 0f;
			}
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0000F40B File Offset: 0x0000D60B
	// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0000F413 File Offset: 0x0000D613
	public EasyJoystick.InteractionType Interaction
	{
		get
		{
			return this.interaction;
		}
		set
		{
			this.interaction = value;
			if (this.interaction == EasyJoystick.InteractionType.Direct || this.interaction == EasyJoystick.InteractionType.Include)
			{
				this.useBroadcast = false;
			}
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0000F434 File Offset: 0x0000D634
	// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0000F43C File Offset: 0x0000D63C
	public Transform XAxisTransform
	{
		get
		{
			return this.xAxisTransform;
		}
		set
		{
			this.xAxisTransform = value;
			if (this.xAxisTransform != null)
			{
				this.xAxisCharacterController = this.xAxisTransform.GetComponent<CharacterController>();
				return;
			}
			this.xAxisCharacterController = null;
			this.xAxisGravity = 0f;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0000F477 File Offset: 0x0000D677
	// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0000F47F File Offset: 0x0000D67F
	public EasyJoystick.PropertiesInfluenced XTI
	{
		get
		{
			return this.xTI;
		}
		set
		{
			this.xTI = value;
			if (this.xTI != EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.enableXAutoStab = false;
				this.enableXClamp = false;
			}
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0000F49F File Offset: 0x0000D69F
	// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0000F4A7 File Offset: 0x0000D6A7
	public float ThresholdX
	{
		get
		{
			return this.thresholdX;
		}
		set
		{
			if (value <= 0f)
			{
				this.thresholdX = value * -1f;
				return;
			}
			this.thresholdX = value;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0000F4C6 File Offset: 0x0000D6C6
	// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0000F4CE File Offset: 0x0000D6CE
	public float StabSpeedX
	{
		get
		{
			return this.stabSpeedX;
		}
		set
		{
			if (value <= 0f)
			{
				this.stabSpeedX = value * -1f;
				return;
			}
			this.stabSpeedX = value;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0000F4ED File Offset: 0x0000D6ED
	// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0000F4F5 File Offset: 0x0000D6F5
	public Transform YAxisTransform
	{
		get
		{
			return this.yAxisTransform;
		}
		set
		{
			this.yAxisTransform = value;
			if (this.yAxisTransform != null)
			{
				this.yAxisCharacterController = this.yAxisTransform.GetComponent<CharacterController>();
				return;
			}
			this.yAxisCharacterController = null;
			this.yAxisGravity = 0f;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0000F530 File Offset: 0x0000D730
	// (set) Token: 0x06000DEA RID: 3562 RVA: 0x0000F538 File Offset: 0x0000D738
	public EasyJoystick.PropertiesInfluenced YTI
	{
		get
		{
			return this.yTI;
		}
		set
		{
			this.yTI = value;
			if (this.yTI != EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.enableYAutoStab = false;
				this.enableYClamp = false;
			}
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0000F558 File Offset: 0x0000D758
	// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0000F560 File Offset: 0x0000D760
	public float ThresholdY
	{
		get
		{
			return this.thresholdY;
		}
		set
		{
			if (value <= 0f)
			{
				this.thresholdY = value * -1f;
				return;
			}
			this.thresholdY = value;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000DED RID: 3565 RVA: 0x0000F57F File Offset: 0x0000D77F
	// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0000F587 File Offset: 0x0000D787
	public float StabSpeedY
	{
		get
		{
			return this.stabSpeedY;
		}
		set
		{
			if (value <= 0f)
			{
				this.stabSpeedY = value * -1f;
				return;
			}
			this.stabSpeedY = value;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0000F5A6 File Offset: 0x0000D7A6
	// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0009C950 File Offset: 0x0009AB50
	public Vector2 Smoothing
	{
		get
		{
			return this.smoothing;
		}
		set
		{
			this.smoothing = value;
			if (this.smoothing.x < 0f)
			{
				this.smoothing.x = 0f;
			}
			if (this.smoothing.y < 0f)
			{
				this.smoothing.y = 0f;
			}
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0000F5AE File Offset: 0x0000D7AE
	// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x0009C9A8 File Offset: 0x0009ABA8
	public Vector2 Inertia
	{
		get
		{
			return this.inertia;
		}
		set
		{
			this.inertia = value;
			if (this.inertia.x <= 0f)
			{
				this.inertia.x = 1f;
			}
			if (this.inertia.y <= 0f)
			{
				this.inertia.y = 1f;
			}
		}
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0009CA00 File Offset: 0x0009AC00
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0009CA64 File Offset: 0x0009AC64
	private void OnDisable()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.areaRect);
		}
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0009CA64 File Offset: 0x0009AC64
	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.areaRect);
		}
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0009CAD8 File Offset: 0x0009ACD8
	private void Start()
	{
		if (!this.dynamicJoystick)
		{
			this.joystickCenter = this.joystickPositionOffset;
			this.ComputeJoystickAnchor(this.joyAnchor);
			this.virtualJoystick = true;
		}
		else
		{
			this.virtualJoystick = false;
		}
		VirtualScreen.ComputeVirtualScreen();
		this.startXLocalAngle = this.GetStartAutoStabAngle(this.xAxisTransform, this.xAI);
		this.startYLocalAngle = this.GetStartAutoStabAngle(this.yAxisTransform, this.yAI);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
	private void Update()
	{
		if (!this.useFixedUpdate && this.enable)
		{
			this.UpdateJoystick();
		}
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0000F5CE File Offset: 0x0000D7CE
	private void FixedUpdate()
	{
		if (this.useFixedUpdate && this.enable)
		{
			this.UpdateJoystick();
		}
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0009CB4C File Offset: 0x0009AD4C
	private void UpdateJoystick()
	{
		if (Application.isPlaying && this.isActivated)
		{
			if (this.joystickIndex == -1 || (this.joystickAxis == Vector2.zero && this.joystickIndex > -1))
			{
				if (this.enableXAutoStab)
				{
					this.DoAutoStabilisation(this.xAxisTransform, this.xAI, this.thresholdX, this.stabSpeedX, this.startXLocalAngle);
				}
				if (this.enableYAutoStab)
				{
					this.DoAutoStabilisation(this.yAxisTransform, this.yAI, this.thresholdY, this.stabSpeedY, this.startYLocalAngle);
				}
			}
			if (!this.dynamicJoystick)
			{
				this.joystickCenter = this.joystickPositionOffset;
			}
			if (this.joystickIndex == -1)
			{
				if (!this.enableSmoothing)
				{
					this.joystickTouch = Vector2.zero;
				}
				else if ((double)this.joystickTouch.sqrMagnitude > 0.0001)
				{
					this.joystickTouch = new Vector2(this.joystickTouch.x - this.joystickTouch.x * this.smoothing.x * Time.deltaTime, this.joystickTouch.y - this.joystickTouch.y * this.smoothing.y * Time.deltaTime);
				}
				else
				{
					this.joystickTouch = Vector2.zero;
				}
			}
			Vector2 vector = new Vector2(this.joystickAxis.x, this.joystickAxis.y);
			float num = this.ComputeDeadZone();
			this.joystickAxis = new Vector2(this.joystickTouch.x * num, this.joystickTouch.y * num);
			if (this.inverseXAxis)
			{
				this.joystickAxis.x = this.joystickAxis.x * -1f;
			}
			if (this.inverseYAxis)
			{
				this.joystickAxis.y = this.joystickAxis.y * -1f;
			}
			Vector2 vector2;
			vector2..ctor(this.speed.x * this.joystickAxis.x, this.speed.y * this.joystickAxis.y);
			if (this.enableInertia)
			{
				Vector2 vector3 = vector2 - this.joystickValue;
				vector3.x /= this.inertia.x;
				vector3.y /= this.inertia.y;
				this.joystickValue += vector3;
			}
			else
			{
				this.joystickValue = vector2;
			}
			if (vector == Vector2.zero && this.joystickAxis != Vector2.zero && this.interaction != EasyJoystick.InteractionType.Direct && this.interaction != EasyJoystick.InteractionType.Include)
			{
				this.CreateEvent(EasyJoystick.MessageName.On_JoystickMoveStart);
			}
			this.UpdateGravity();
			if (this.joystickAxis != Vector2.zero)
			{
				this.sendEnd = false;
				switch (this.interaction)
				{
				case EasyJoystick.InteractionType.Direct:
					this.UpdateDirect();
					return;
				case EasyJoystick.InteractionType.Include:
					break;
				case EasyJoystick.InteractionType.EventNotification:
					this.CreateEvent(EasyJoystick.MessageName.On_JoystickMove);
					return;
				case EasyJoystick.InteractionType.DirectAndEvent:
					this.UpdateDirect();
					this.CreateEvent(EasyJoystick.MessageName.On_JoystickMove);
					return;
				default:
					return;
				}
			}
			else if (!this.sendEnd)
			{
				this.CreateEvent(EasyJoystick.MessageName.On_JoystickMoveEnd);
				this.sendEnd = true;
			}
		}
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0009CE5C File Offset: 0x0009B05C
	private void OnGUI()
	{
		if (this.enable)
		{
			GUI.depth = this.guiDepth;
			base.useGUILayout = this.isUseGuiLayout;
			if (this.dynamicJoystick && Application.isEditor && !Application.isPlaying)
			{
				switch (this.area)
				{
				case EasyJoystick.DynamicArea.FullScreen:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.MiddleCenter);
					break;
				case EasyJoystick.DynamicArea.Left:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.MiddleLeft);
					break;
				case EasyJoystick.DynamicArea.Right:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.MiddleRight);
					break;
				case EasyJoystick.DynamicArea.Top:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.UpperCenter);
					break;
				case EasyJoystick.DynamicArea.Bottom:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.LowerCenter);
					break;
				case EasyJoystick.DynamicArea.TopLeft:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.UpperLeft);
					break;
				case EasyJoystick.DynamicArea.TopRight:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.UpperRight);
					break;
				case EasyJoystick.DynamicArea.BottomLeft:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.LowerLeft);
					break;
				case EasyJoystick.DynamicArea.BottomRight:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.LowerRight);
					break;
				}
			}
			if (Application.isEditor && !Application.isPlaying)
			{
				VirtualScreen.ComputeVirtualScreen();
				this.ComputeJoystickAnchor(this.joyAnchor);
			}
			VirtualScreen.SetGuiScaleMatrix();
			if ((this.showZone && this.areaTexture != null && !this.dynamicJoystick) || (this.showZone && this.dynamicJoystick && this.virtualJoystick && this.areaTexture != null) || (this.dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (this.isActivated)
				{
					GUI.color = this.areaColor;
					if (Application.isPlaying && !this.dynamicJoystick)
					{
						EasyTouch.RemoveReservedArea(this.areaRect);
						EasyTouch.AddReservedArea(this.areaRect);
					}
				}
				else
				{
					GUI.color = new Color(this.areaColor.r, this.areaColor.g, this.areaColor.b, 0.2f);
					if (Application.isPlaying && !this.dynamicJoystick)
					{
						EasyTouch.RemoveReservedArea(this.areaRect);
					}
				}
				if (this.showDebugRadius && Application.isEditor)
				{
					GUI.Box(this.areaRect, "");
				}
				GUI.DrawTexture(this.areaRect, this.areaTexture, 0, true);
			}
			if ((this.showTouch && this.touchTexture != null && !this.dynamicJoystick) || (this.showTouch && this.dynamicJoystick && this.virtualJoystick && this.touchTexture != null) || (this.dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (this.isActivated)
				{
					GUI.color = this.touchColor;
				}
				else
				{
					GUI.color = new Color(this.touchColor.r, this.touchColor.g, this.touchColor.b, 0.2f);
				}
				GUI.DrawTexture(new Rect(this.anchorPosition.x + this.joystickCenter.x + (this.joystickTouch.x - this.touchSize), this.anchorPosition.y + this.joystickCenter.y - (this.joystickTouch.y + this.touchSize), this.touchSize * 2f, this.touchSize * 2f), this.touchTexture, 2, true);
			}
			if ((this.showDeadZone && this.deadTexture != null && !this.dynamicJoystick) || (this.showDeadZone && this.dynamicJoystick && this.virtualJoystick && this.deadTexture != null) || (this.dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				GUI.DrawTexture(this.deadRect, this.deadTexture, 2, true);
			}
			GUI.color = Color.white;
			return;
		}
		EasyTouch.RemoveReservedArea(this.areaRect);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x000042DD File Offset: 0x000024DD
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0009D214 File Offset: 0x0009B414
	private void CreateEvent(EasyJoystick.MessageName message)
	{
		MovingJoystick movingJoystick = new MovingJoystick();
		movingJoystick.joystickName = base.gameObject.name;
		movingJoystick.joystickAxis = this.joystickAxis;
		movingJoystick.joystickValue = this.joystickValue;
		movingJoystick.joystick = this;
		if (!this.useBroadcast)
		{
			switch (message)
			{
			case EasyJoystick.MessageName.On_JoystickMoveStart:
				if (EasyJoystick.On_JoystickMoveStart != null)
				{
					EasyJoystick.On_JoystickMoveStart(movingJoystick);
					return;
				}
				break;
			case EasyJoystick.MessageName.On_JoystickTouchStart:
				if (EasyJoystick.On_JoystickTouchStart != null)
				{
					EasyJoystick.On_JoystickTouchStart(movingJoystick);
					return;
				}
				break;
			case EasyJoystick.MessageName.On_JoystickTouchUp:
				if (EasyJoystick.On_JoystickTouchUp != null)
				{
					EasyJoystick.On_JoystickTouchUp(movingJoystick);
					return;
				}
				break;
			case EasyJoystick.MessageName.On_JoystickMove:
				if (EasyJoystick.On_JoystickMove != null)
				{
					EasyJoystick.On_JoystickMove(movingJoystick);
					return;
				}
				break;
			case EasyJoystick.MessageName.On_JoystickMoveEnd:
				if (EasyJoystick.On_JoystickMoveEnd != null)
				{
					EasyJoystick.On_JoystickMoveEnd(movingJoystick);
					return;
				}
				break;
			case EasyJoystick.MessageName.On_JoystickTap:
				if (EasyJoystick.On_JoystickTap != null)
				{
					EasyJoystick.On_JoystickTap(movingJoystick);
					return;
				}
				break;
			case EasyJoystick.MessageName.On_JoystickDoubleTap:
				if (EasyJoystick.On_JoystickDoubleTap != null)
				{
					EasyJoystick.On_JoystickDoubleTap(movingJoystick);
					return;
				}
				break;
			default:
				return;
			}
		}
		else if (this.useBroadcast)
		{
			if (this.receiverGameObject != null)
			{
				switch (this.messageMode)
				{
				case EasyJoystick.Broadcast.SendMessage:
					this.receiverGameObject.SendMessage(message.ToString(), movingJoystick, 1);
					return;
				case EasyJoystick.Broadcast.SendMessageUpwards:
					this.receiverGameObject.SendMessageUpwards(message.ToString(), movingJoystick, 1);
					return;
				case EasyJoystick.Broadcast.BroadcastMessage:
					this.receiverGameObject.BroadcastMessage(message.ToString(), movingJoystick, 1);
					return;
				default:
					return;
				}
			}
			else
			{
				Debug.LogError("Joystick : " + base.gameObject.name + " : you must setup receiver gameobject");
			}
		}
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0009D3C4 File Offset: 0x0009B5C4
	private void UpdateDirect()
	{
		if (this.xAxisTransform != null)
		{
			Vector3 influencedAxis = this.GetInfluencedAxis(this.xAI);
			this.DoActionDirect(this.xAxisTransform, this.xTI, influencedAxis, this.joystickValue.x, this.xAxisCharacterController);
			if (this.enableXClamp && this.xTI == EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.DoAngleLimitation(this.xAxisTransform, this.xAI, this.clampXMin, this.clampXMax, this.startXLocalAngle);
			}
		}
		if (this.YAxisTransform != null)
		{
			Vector3 influencedAxis2 = this.GetInfluencedAxis(this.yAI);
			this.DoActionDirect(this.yAxisTransform, this.yTI, influencedAxis2, this.joystickValue.y, this.yAxisCharacterController);
			if (this.enableYClamp && this.yTI == EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.DoAngleLimitation(this.yAxisTransform, this.yAI, this.clampYMin, this.clampYMax, this.startYLocalAngle);
			}
		}
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0009D4BC File Offset: 0x0009B6BC
	private void UpdateGravity()
	{
		if (this.xAxisCharacterController != null && this.xAxisGravity > 0f)
		{
			this.xAxisCharacterController.Move(Vector3.down * this.xAxisGravity * Time.deltaTime);
		}
		if (this.yAxisCharacterController != null && this.yAxisGravity > 0f)
		{
			this.yAxisCharacterController.Move(Vector3.down * this.yAxisGravity * Time.deltaTime);
		}
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0009D54C File Offset: 0x0009B74C
	private Vector3 GetInfluencedAxis(EasyJoystick.AxisInfluenced axisInfluenced)
	{
		Vector3 result = Vector3.zero;
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			result = Vector3.right;
			break;
		case EasyJoystick.AxisInfluenced.Y:
			result = Vector3.up;
			break;
		case EasyJoystick.AxisInfluenced.Z:
			result = Vector3.forward;
			break;
		case EasyJoystick.AxisInfluenced.XYZ:
			result = Vector3.one;
			break;
		}
		return result;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x0009D598 File Offset: 0x0009B798
	private void DoActionDirect(Transform axisTransform, EasyJoystick.PropertiesInfluenced inlfuencedProperty, Vector3 axis, float sensibility, CharacterController charact)
	{
		switch (inlfuencedProperty)
		{
		case EasyJoystick.PropertiesInfluenced.Rotate:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, 0);
			return;
		case EasyJoystick.PropertiesInfluenced.RotateLocal:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, 1);
			return;
		case EasyJoystick.PropertiesInfluenced.Translate:
			if (charact == null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, 0);
				return;
			}
			charact.Move(axis * sensibility * Time.deltaTime);
			return;
		case EasyJoystick.PropertiesInfluenced.TranslateLocal:
			if (charact == null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, 1);
				return;
			}
			charact.Move(charact.transform.TransformDirection(axis) * sensibility * Time.deltaTime);
			return;
		case EasyJoystick.PropertiesInfluenced.Scale:
			axisTransform.localScale += axis * sensibility * Time.deltaTime;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x0009D6A4 File Offset: 0x0009B8A4
	private void DoAngleLimitation(Transform axisTransform, EasyJoystick.AxisInfluenced axisInfluenced, float clampMin, float clampMax, float startAngle)
	{
		float num = 0f;
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			num = axisTransform.localRotation.eulerAngles.x;
			break;
		case EasyJoystick.AxisInfluenced.Y:
			num = axisTransform.localRotation.eulerAngles.y;
			break;
		case EasyJoystick.AxisInfluenced.Z:
			num = axisTransform.localRotation.eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		num = Mathf.Clamp(num, -clampMax, clampMin);
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			axisTransform.localEulerAngles = new Vector3(num, axisTransform.localEulerAngles.y, axisTransform.localEulerAngles.z);
			return;
		case EasyJoystick.AxisInfluenced.Y:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, num, axisTransform.localEulerAngles.z);
			return;
		case EasyJoystick.AxisInfluenced.Z:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, axisTransform.localEulerAngles.y, num);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0009D7AC File Offset: 0x0009B9AC
	private void DoAutoStabilisation(Transform axisTransform, EasyJoystick.AxisInfluenced axisInfluenced, float threshold, float speed, float startAngle)
	{
		float num = 0f;
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			num = axisTransform.localRotation.eulerAngles.x;
			break;
		case EasyJoystick.AxisInfluenced.Y:
			num = axisTransform.localRotation.eulerAngles.y;
			break;
		case EasyJoystick.AxisInfluenced.Z:
			num = axisTransform.localRotation.eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		if (num > startAngle - threshold || num < startAngle + threshold)
		{
			float num2 = 0f;
			Vector3 zero = Vector3.zero;
			if (num > startAngle - threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime * -1f;
			}
			if (num < startAngle + threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime;
			}
			switch (axisInfluenced)
			{
			case EasyJoystick.AxisInfluenced.X:
				zero..ctor(num2, axisTransform.localRotation.eulerAngles.y, axisTransform.localRotation.eulerAngles.z);
				break;
			case EasyJoystick.AxisInfluenced.Y:
				zero..ctor(axisTransform.localRotation.eulerAngles.x, num2, axisTransform.localRotation.eulerAngles.z);
				break;
			case EasyJoystick.AxisInfluenced.Z:
				zero..ctor(axisTransform.localRotation.eulerAngles.x, axisTransform.localRotation.eulerAngles.y, num2);
				break;
			}
			axisTransform.localRotation = Quaternion.Euler(zero);
		}
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x0009D944 File Offset: 0x0009BB44
	private float GetStartAutoStabAngle(Transform axisTransform, EasyJoystick.AxisInfluenced axisInfluenced)
	{
		float num = 0f;
		if (axisTransform != null)
		{
			switch (axisInfluenced)
			{
			case EasyJoystick.AxisInfluenced.X:
				num = axisTransform.localRotation.eulerAngles.x;
				break;
			case EasyJoystick.AxisInfluenced.Y:
				num = axisTransform.localRotation.eulerAngles.y;
				break;
			case EasyJoystick.AxisInfluenced.Z:
				num = axisTransform.localRotation.eulerAngles.z;
				break;
			}
			if (num <= 360f && num >= 180f)
			{
				num -= 360f;
			}
		}
		return num;
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
	private float ComputeDeadZone()
	{
		float num = Mathf.Max(this.joystickTouch.magnitude, 0.1f);
		float result;
		if (this.restrictArea)
		{
			result = Mathf.Max(num - this.deadZone, 0f) / (this.zoneRadius - this.touchSize - this.deadZone) / num;
		}
		else
		{
			result = Mathf.Max(num - this.deadZone, 0f) / (this.zoneRadius - this.deadZone) / num;
		}
		return result;
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x0009DA54 File Offset: 0x0009BC54
	private void ComputeJoystickAnchor(EasyJoystick.JoystickAnchor anchor)
	{
		float num = 0f;
		if (!this.restrictArea)
		{
			num = this.touchSize;
		}
		switch (anchor)
		{
		case EasyJoystick.JoystickAnchor.None:
			this.anchorPosition = Vector2.zero;
			break;
		case EasyJoystick.JoystickAnchor.UpperLeft:
			this.anchorPosition = new Vector2(this.zoneRadius + num, this.zoneRadius + num);
			break;
		case EasyJoystick.JoystickAnchor.UpperCenter:
			this.anchorPosition = new Vector2(VirtualScreen.width / 2f, this.zoneRadius + num);
			break;
		case EasyJoystick.JoystickAnchor.UpperRight:
			this.anchorPosition = new Vector2(VirtualScreen.width - this.zoneRadius - num, this.zoneRadius + num);
			break;
		case EasyJoystick.JoystickAnchor.MiddleLeft:
			this.anchorPosition = new Vector2(this.zoneRadius + num, VirtualScreen.height / 2f);
			break;
		case EasyJoystick.JoystickAnchor.MiddleCenter:
			this.anchorPosition = new Vector2(VirtualScreen.width / 2f, VirtualScreen.height / 2f);
			break;
		case EasyJoystick.JoystickAnchor.MiddleRight:
			this.anchorPosition = new Vector2(VirtualScreen.width - this.zoneRadius - num, VirtualScreen.height / 2f);
			break;
		case EasyJoystick.JoystickAnchor.LowerLeft:
			this.anchorPosition = new Vector2(this.zoneRadius + num, VirtualScreen.height - this.zoneRadius - num);
			break;
		case EasyJoystick.JoystickAnchor.LowerCenter:
			this.anchorPosition = new Vector2(VirtualScreen.width / 2f, VirtualScreen.height - this.zoneRadius - num);
			break;
		case EasyJoystick.JoystickAnchor.LowerRight:
			this.anchorPosition = new Vector2(VirtualScreen.width - this.zoneRadius - num, VirtualScreen.height - this.zoneRadius - num);
			break;
		}
		this.areaRect = new Rect(this.anchorPosition.x + this.joystickCenter.x - this.zoneRadius, this.anchorPosition.y + this.joystickCenter.y - this.zoneRadius, this.zoneRadius * 2f, this.zoneRadius * 2f);
		this.deadRect = new Rect(this.anchorPosition.x + this.joystickCenter.x - this.deadZone, this.anchorPosition.y + this.joystickCenter.y - this.deadZone, this.deadZone * 2f, this.deadZone * 2f);
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0009DCBC File Offset: 0x0009BEBC
	private void On_TouchStart(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated)
		{
			if (!this.dynamicJoystick)
			{
				Vector2 vector;
				vector..ctor((this.anchorPosition.x + this.joystickCenter.x) * VirtualScreen.xRatio, (VirtualScreen.height - this.anchorPosition.y - this.joystickCenter.y) * VirtualScreen.yRatio);
				if ((gesture.position - vector).sqrMagnitude < this.zoneRadius * VirtualScreen.xRatio * (this.zoneRadius * VirtualScreen.xRatio))
				{
					this.joystickIndex = gesture.fingerIndex;
					this.CreateEvent(EasyJoystick.MessageName.On_JoystickTouchStart);
					return;
				}
			}
			else if (!this.virtualJoystick)
			{
				switch (this.area)
				{
				case EasyJoystick.DynamicArea.FullScreen:
					this.virtualJoystick = true;
					break;
				case EasyJoystick.DynamicArea.Left:
					if (gesture.position.x < (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.Right:
					if (gesture.position.x > (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.Top:
					if (gesture.position.y > (float)(Screen.height / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.Bottom:
					if (gesture.position.y < (float)(Screen.height / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.TopLeft:
					if (gesture.position.y > (float)(Screen.height / 2) && gesture.position.x < (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.TopRight:
					if (gesture.position.y > (float)(Screen.height / 2) && gesture.position.x > (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.BottomLeft:
					if (gesture.position.y < (float)(Screen.height / 2) && gesture.position.x < (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.BottomRight:
					if (gesture.position.y < (float)(Screen.height / 2) && gesture.position.x > (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				}
				if (this.virtualJoystick)
				{
					this.joystickCenter = new Vector2(gesture.position.x / VirtualScreen.xRatio, VirtualScreen.height - gesture.position.y / VirtualScreen.yRatio);
					this.JoyAnchor = EasyJoystick.JoystickAnchor.None;
					this.joystickIndex = gesture.fingerIndex;
				}
			}
		}
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0000F5E6 File Offset: 0x0000D7E6
	private void On_SimpleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickTap);
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0000F61D File Offset: 0x0000D81D
	private void On_DoubleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickDoubleTap);
		}
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0009DF88 File Offset: 0x0009C188
	private void On_TouchDown(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated)
		{
			Vector2 vector;
			vector..ctor((this.anchorPosition.x + this.joystickCenter.x) * VirtualScreen.xRatio, (VirtualScreen.height - (this.anchorPosition.y + this.joystickCenter.y)) * VirtualScreen.yRatio);
			if (gesture.fingerIndex == this.joystickIndex)
			{
				if (((gesture.position - vector).sqrMagnitude < this.zoneRadius * VirtualScreen.xRatio * (this.zoneRadius * VirtualScreen.xRatio) && this.resetFingerExit) || !this.resetFingerExit)
				{
					this.joystickTouch = new Vector2(gesture.position.x, gesture.position.y) - vector;
					this.joystickTouch = new Vector2(this.joystickTouch.x / VirtualScreen.xRatio, this.joystickTouch.y / VirtualScreen.yRatio);
					if (!this.enableXaxis)
					{
						this.joystickTouch.x = 0f;
					}
					if (!this.enableYaxis)
					{
						this.joystickTouch.y = 0f;
					}
					if ((this.joystickTouch / (this.zoneRadius - this.touchSizeCoef)).sqrMagnitude > 1f)
					{
						this.joystickTouch.Normalize();
						this.joystickTouch *= this.zoneRadius - this.touchSizeCoef;
						return;
					}
				}
				else
				{
					this.On_TouchUp(gesture);
				}
			}
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0009E134 File Offset: 0x0009C334
	private void On_TouchUp(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.joystickIndex = -1;
			if (this.dynamicJoystick)
			{
				this.virtualJoystick = false;
			}
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickTouchUp);
		}
	}

	// Token: 0x04000ACA RID: 2762
	private Vector2 joystickAxis;

	// Token: 0x04000ACB RID: 2763
	private Vector2 joystickTouch;

	// Token: 0x04000ACC RID: 2764
	private Vector2 joystickValue;

	// Token: 0x04000ACD RID: 2765
	public bool enable = true;

	// Token: 0x04000ACE RID: 2766
	public bool isActivated = true;

	// Token: 0x04000ACF RID: 2767
	public bool showDebugRadius;

	// Token: 0x04000AD0 RID: 2768
	public bool useFixedUpdate;

	// Token: 0x04000AD1 RID: 2769
	public bool isUseGuiLayout = true;

	// Token: 0x04000AD2 RID: 2770
	[SerializeField]
	private bool dynamicJoystick;

	// Token: 0x04000AD3 RID: 2771
	public EasyJoystick.DynamicArea area;

	// Token: 0x04000AD4 RID: 2772
	[SerializeField]
	private EasyJoystick.JoystickAnchor joyAnchor = EasyJoystick.JoystickAnchor.LowerLeft;

	// Token: 0x04000AD5 RID: 2773
	[SerializeField]
	private Vector2 joystickPositionOffset = Vector2.zero;

	// Token: 0x04000AD6 RID: 2774
	[SerializeField]
	private float zoneRadius = 100f;

	// Token: 0x04000AD7 RID: 2775
	[SerializeField]
	private float touchSize = 30f;

	// Token: 0x04000AD8 RID: 2776
	public float deadZone = 20f;

	// Token: 0x04000AD9 RID: 2777
	[SerializeField]
	private bool restrictArea;

	// Token: 0x04000ADA RID: 2778
	public bool resetFingerExit;

	// Token: 0x04000ADB RID: 2779
	[SerializeField]
	private EasyJoystick.InteractionType interaction;

	// Token: 0x04000ADC RID: 2780
	public bool useBroadcast;

	// Token: 0x04000ADD RID: 2781
	public EasyJoystick.Broadcast messageMode;

	// Token: 0x04000ADE RID: 2782
	public GameObject receiverGameObject;

	// Token: 0x04000ADF RID: 2783
	public Vector2 speed;

	// Token: 0x04000AE0 RID: 2784
	public bool enableXaxis = true;

	// Token: 0x04000AE1 RID: 2785
	[SerializeField]
	private Transform xAxisTransform;

	// Token: 0x04000AE2 RID: 2786
	public CharacterController xAxisCharacterController;

	// Token: 0x04000AE3 RID: 2787
	public float xAxisGravity;

	// Token: 0x04000AE4 RID: 2788
	[SerializeField]
	private EasyJoystick.PropertiesInfluenced xTI;

	// Token: 0x04000AE5 RID: 2789
	public EasyJoystick.AxisInfluenced xAI;

	// Token: 0x04000AE6 RID: 2790
	public bool inverseXAxis;

	// Token: 0x04000AE7 RID: 2791
	public bool enableXClamp;

	// Token: 0x04000AE8 RID: 2792
	public float clampXMax;

	// Token: 0x04000AE9 RID: 2793
	public float clampXMin;

	// Token: 0x04000AEA RID: 2794
	public bool enableXAutoStab;

	// Token: 0x04000AEB RID: 2795
	[SerializeField]
	private float thresholdX = 0.01f;

	// Token: 0x04000AEC RID: 2796
	[SerializeField]
	private float stabSpeedX = 20f;

	// Token: 0x04000AED RID: 2797
	public bool enableYaxis = true;

	// Token: 0x04000AEE RID: 2798
	[SerializeField]
	private Transform yAxisTransform;

	// Token: 0x04000AEF RID: 2799
	public CharacterController yAxisCharacterController;

	// Token: 0x04000AF0 RID: 2800
	public float yAxisGravity;

	// Token: 0x04000AF1 RID: 2801
	[SerializeField]
	private EasyJoystick.PropertiesInfluenced yTI;

	// Token: 0x04000AF2 RID: 2802
	public EasyJoystick.AxisInfluenced yAI;

	// Token: 0x04000AF3 RID: 2803
	public bool inverseYAxis;

	// Token: 0x04000AF4 RID: 2804
	public bool enableYClamp;

	// Token: 0x04000AF5 RID: 2805
	public float clampYMax;

	// Token: 0x04000AF6 RID: 2806
	public float clampYMin;

	// Token: 0x04000AF7 RID: 2807
	public bool enableYAutoStab;

	// Token: 0x04000AF8 RID: 2808
	[SerializeField]
	private float thresholdY = 0.01f;

	// Token: 0x04000AF9 RID: 2809
	[SerializeField]
	private float stabSpeedY = 20f;

	// Token: 0x04000AFA RID: 2810
	public bool enableSmoothing;

	// Token: 0x04000AFB RID: 2811
	[SerializeField]
	public Vector2 smoothing = new Vector2(2f, 2f);

	// Token: 0x04000AFC RID: 2812
	public bool enableInertia;

	// Token: 0x04000AFD RID: 2813
	[SerializeField]
	public Vector2 inertia = new Vector2(100f, 100f);

	// Token: 0x04000AFE RID: 2814
	public int guiDepth;

	// Token: 0x04000AFF RID: 2815
	public bool showZone = true;

	// Token: 0x04000B00 RID: 2816
	public bool showTouch = true;

	// Token: 0x04000B01 RID: 2817
	public bool showDeadZone = true;

	// Token: 0x04000B02 RID: 2818
	public Texture areaTexture;

	// Token: 0x04000B03 RID: 2819
	public Color areaColor = Color.white;

	// Token: 0x04000B04 RID: 2820
	public Texture touchTexture;

	// Token: 0x04000B05 RID: 2821
	public Color touchColor = Color.white;

	// Token: 0x04000B06 RID: 2822
	public Texture deadTexture;

	// Token: 0x04000B07 RID: 2823
	public bool showProperties = true;

	// Token: 0x04000B08 RID: 2824
	public bool showInteraction;

	// Token: 0x04000B09 RID: 2825
	public bool showAppearance;

	// Token: 0x04000B0A RID: 2826
	public bool showPosition = true;

	// Token: 0x04000B0B RID: 2827
	private Vector2 joystickCenter;

	// Token: 0x04000B0C RID: 2828
	private Rect areaRect;

	// Token: 0x04000B0D RID: 2829
	private Rect deadRect;

	// Token: 0x04000B0E RID: 2830
	private Vector2 anchorPosition = Vector2.zero;

	// Token: 0x04000B0F RID: 2831
	private bool virtualJoystick = true;

	// Token: 0x04000B10 RID: 2832
	private int joystickIndex = -1;

	// Token: 0x04000B11 RID: 2833
	private float touchSizeCoef;

	// Token: 0x04000B12 RID: 2834
	private bool sendEnd = true;

	// Token: 0x04000B13 RID: 2835
	private float startXLocalAngle;

	// Token: 0x04000B14 RID: 2836
	private float startYLocalAngle;

	// Token: 0x0200019D RID: 413
	// (Invoke) Token: 0x06000E0D RID: 3597
	public delegate void JoystickMoveStartHandler(MovingJoystick move);

	// Token: 0x0200019E RID: 414
	// (Invoke) Token: 0x06000E11 RID: 3601
	public delegate void JoystickMoveHandler(MovingJoystick move);

	// Token: 0x0200019F RID: 415
	// (Invoke) Token: 0x06000E15 RID: 3605
	public delegate void JoystickMoveEndHandler(MovingJoystick move);

	// Token: 0x020001A0 RID: 416
	// (Invoke) Token: 0x06000E19 RID: 3609
	public delegate void JoystickTouchStartHandler(MovingJoystick move);

	// Token: 0x020001A1 RID: 417
	// (Invoke) Token: 0x06000E1D RID: 3613
	public delegate void JoystickTapHandler(MovingJoystick move);

	// Token: 0x020001A2 RID: 418
	// (Invoke) Token: 0x06000E21 RID: 3617
	public delegate void JoystickDoubleTapHandler(MovingJoystick move);

	// Token: 0x020001A3 RID: 419
	// (Invoke) Token: 0x06000E25 RID: 3621
	public delegate void JoystickTouchUpHandler(MovingJoystick move);

	// Token: 0x020001A4 RID: 420
	public enum JoystickAnchor
	{
		// Token: 0x04000B16 RID: 2838
		None,
		// Token: 0x04000B17 RID: 2839
		UpperLeft,
		// Token: 0x04000B18 RID: 2840
		UpperCenter,
		// Token: 0x04000B19 RID: 2841
		UpperRight,
		// Token: 0x04000B1A RID: 2842
		MiddleLeft,
		// Token: 0x04000B1B RID: 2843
		MiddleCenter,
		// Token: 0x04000B1C RID: 2844
		MiddleRight,
		// Token: 0x04000B1D RID: 2845
		LowerLeft,
		// Token: 0x04000B1E RID: 2846
		LowerCenter,
		// Token: 0x04000B1F RID: 2847
		LowerRight
	}

	// Token: 0x020001A5 RID: 421
	public enum PropertiesInfluenced
	{
		// Token: 0x04000B21 RID: 2849
		Rotate,
		// Token: 0x04000B22 RID: 2850
		RotateLocal,
		// Token: 0x04000B23 RID: 2851
		Translate,
		// Token: 0x04000B24 RID: 2852
		TranslateLocal,
		// Token: 0x04000B25 RID: 2853
		Scale
	}

	// Token: 0x020001A6 RID: 422
	public enum AxisInfluenced
	{
		// Token: 0x04000B27 RID: 2855
		X,
		// Token: 0x04000B28 RID: 2856
		Y,
		// Token: 0x04000B29 RID: 2857
		Z,
		// Token: 0x04000B2A RID: 2858
		XYZ
	}

	// Token: 0x020001A7 RID: 423
	public enum DynamicArea
	{
		// Token: 0x04000B2C RID: 2860
		FullScreen,
		// Token: 0x04000B2D RID: 2861
		Left,
		// Token: 0x04000B2E RID: 2862
		Right,
		// Token: 0x04000B2F RID: 2863
		Top,
		// Token: 0x04000B30 RID: 2864
		Bottom,
		// Token: 0x04000B31 RID: 2865
		TopLeft,
		// Token: 0x04000B32 RID: 2866
		TopRight,
		// Token: 0x04000B33 RID: 2867
		BottomLeft,
		// Token: 0x04000B34 RID: 2868
		BottomRight
	}

	// Token: 0x020001A8 RID: 424
	public enum InteractionType
	{
		// Token: 0x04000B36 RID: 2870
		Direct,
		// Token: 0x04000B37 RID: 2871
		Include,
		// Token: 0x04000B38 RID: 2872
		EventNotification,
		// Token: 0x04000B39 RID: 2873
		DirectAndEvent
	}

	// Token: 0x020001A9 RID: 425
	public enum Broadcast
	{
		// Token: 0x04000B3B RID: 2875
		SendMessage,
		// Token: 0x04000B3C RID: 2876
		SendMessageUpwards,
		// Token: 0x04000B3D RID: 2877
		BroadcastMessage
	}

	// Token: 0x020001AA RID: 426
	private enum MessageName
	{
		// Token: 0x04000B3F RID: 2879
		On_JoystickMoveStart,
		// Token: 0x04000B40 RID: 2880
		On_JoystickTouchStart,
		// Token: 0x04000B41 RID: 2881
		On_JoystickTouchUp,
		// Token: 0x04000B42 RID: 2882
		On_JoystickMove,
		// Token: 0x04000B43 RID: 2883
		On_JoystickMoveEnd,
		// Token: 0x04000B44 RID: 2884
		On_JoystickTap,
		// Token: 0x04000B45 RID: 2885
		On_JoystickDoubleTap
	}
}
