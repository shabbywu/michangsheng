using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
[ExecuteInEditMode]
public class EasyJoystick : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000C92 RID: 3218 RVA: 0x0004B1FC File Offset: 0x000493FC
	// (remove) Token: 0x06000C93 RID: 3219 RVA: 0x0004B230 File Offset: 0x00049430
	public static event EasyJoystick.JoystickMoveStartHandler On_JoystickMoveStart;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000C94 RID: 3220 RVA: 0x0004B264 File Offset: 0x00049464
	// (remove) Token: 0x06000C95 RID: 3221 RVA: 0x0004B298 File Offset: 0x00049498
	public static event EasyJoystick.JoystickMoveHandler On_JoystickMove;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000C96 RID: 3222 RVA: 0x0004B2CC File Offset: 0x000494CC
	// (remove) Token: 0x06000C97 RID: 3223 RVA: 0x0004B300 File Offset: 0x00049500
	public static event EasyJoystick.JoystickMoveEndHandler On_JoystickMoveEnd;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000C98 RID: 3224 RVA: 0x0004B334 File Offset: 0x00049534
	// (remove) Token: 0x06000C99 RID: 3225 RVA: 0x0004B368 File Offset: 0x00049568
	public static event EasyJoystick.JoystickTouchStartHandler On_JoystickTouchStart;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000C9A RID: 3226 RVA: 0x0004B39C File Offset: 0x0004959C
	// (remove) Token: 0x06000C9B RID: 3227 RVA: 0x0004B3D0 File Offset: 0x000495D0
	public static event EasyJoystick.JoystickTapHandler On_JoystickTap;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000C9C RID: 3228 RVA: 0x0004B404 File Offset: 0x00049604
	// (remove) Token: 0x06000C9D RID: 3229 RVA: 0x0004B438 File Offset: 0x00049638
	public static event EasyJoystick.JoystickDoubleTapHandler On_JoystickDoubleTap;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06000C9E RID: 3230 RVA: 0x0004B46C File Offset: 0x0004966C
	// (remove) Token: 0x06000C9F RID: 3231 RVA: 0x0004B4A0 File Offset: 0x000496A0
	public static event EasyJoystick.JoystickTouchUpHandler On_JoystickTouchUp;

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0004B4D3 File Offset: 0x000496D3
	public Vector2 JoystickAxis
	{
		get
		{
			return this.joystickAxis;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0004B4DB File Offset: 0x000496DB
	// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0004B508 File Offset: 0x00049708
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

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0004B55C File Offset: 0x0004975C
	public Vector2 JoystickValue
	{
		get
		{
			return this.joystickValue;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0004B564 File Offset: 0x00049764
	// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0004B56C File Offset: 0x0004976C
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

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0004B5A6 File Offset: 0x000497A6
	// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0004B5AE File Offset: 0x000497AE
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

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0004B5C3 File Offset: 0x000497C3
	// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0004B5CB File Offset: 0x000497CB
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

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0004B5E0 File Offset: 0x000497E0
	// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0004B5E8 File Offset: 0x000497E8
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

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0004B5FD File Offset: 0x000497FD
	// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0004B608 File Offset: 0x00049808
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

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0004B656 File Offset: 0x00049856
	// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0004B65E File Offset: 0x0004985E
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

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0004B694 File Offset: 0x00049894
	// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0004B69C File Offset: 0x0004989C
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

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0004B6BD File Offset: 0x000498BD
	// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0004B6C5 File Offset: 0x000498C5
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

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0004B700 File Offset: 0x00049900
	// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0004B708 File Offset: 0x00049908
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

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0004B728 File Offset: 0x00049928
	// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0004B730 File Offset: 0x00049930
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

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0004B74F File Offset: 0x0004994F
	// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0004B757 File Offset: 0x00049957
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

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0004B776 File Offset: 0x00049976
	// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0004B77E File Offset: 0x0004997E
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

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0004B7B9 File Offset: 0x000499B9
	// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0004B7C1 File Offset: 0x000499C1
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

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0004B7E1 File Offset: 0x000499E1
	// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0004B7E9 File Offset: 0x000499E9
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

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0004B808 File Offset: 0x00049A08
	// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x0004B810 File Offset: 0x00049A10
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

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0004B82F File Offset: 0x00049A2F
	// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0004B838 File Offset: 0x00049A38
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

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0004B890 File Offset: 0x00049A90
	// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0004B898 File Offset: 0x00049A98
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

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0004B8F0 File Offset: 0x00049AF0
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0004B954 File Offset: 0x00049B54
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

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0004B9C8 File Offset: 0x00049BC8
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

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0004BA3C File Offset: 0x00049C3C
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

	// Token: 0x06000CCA RID: 3274 RVA: 0x0004BAAE File Offset: 0x00049CAE
	private void Update()
	{
		if (!this.useFixedUpdate && this.enable)
		{
			this.UpdateJoystick();
		}
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0004BAC6 File Offset: 0x00049CC6
	private void FixedUpdate()
	{
		if (this.useFixedUpdate && this.enable)
		{
			this.UpdateJoystick();
		}
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0004BAE0 File Offset: 0x00049CE0
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

	// Token: 0x06000CCD RID: 3277 RVA: 0x0004BDF0 File Offset: 0x00049FF0
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

	// Token: 0x06000CCE RID: 3278 RVA: 0x00004095 File Offset: 0x00002295
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0004C1A8 File Offset: 0x0004A3A8
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

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0004C358 File Offset: 0x0004A558
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

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0004C450 File Offset: 0x0004A650
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

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0004C4E0 File Offset: 0x0004A6E0
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

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0004C52C File Offset: 0x0004A72C
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

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0004C638 File Offset: 0x0004A838
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

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0004C740 File Offset: 0x0004A940
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

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0004C8D8 File Offset: 0x0004AAD8
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

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0004C964 File Offset: 0x0004AB64
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

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
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

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0004CC50 File Offset: 0x0004AE50
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

	// Token: 0x06000CDA RID: 3290 RVA: 0x0004CF1C File Offset: 0x0004B11C
	private void On_SimpleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickTap);
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0004CF53 File Offset: 0x0004B153
	private void On_DoubleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickDoubleTap);
		}
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0004CF8C File Offset: 0x0004B18C
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

	// Token: 0x06000CDD RID: 3293 RVA: 0x0004D138 File Offset: 0x0004B338
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

	// Token: 0x040008B4 RID: 2228
	private Vector2 joystickAxis;

	// Token: 0x040008B5 RID: 2229
	private Vector2 joystickTouch;

	// Token: 0x040008B6 RID: 2230
	private Vector2 joystickValue;

	// Token: 0x040008B7 RID: 2231
	public bool enable = true;

	// Token: 0x040008B8 RID: 2232
	public bool isActivated = true;

	// Token: 0x040008B9 RID: 2233
	public bool showDebugRadius;

	// Token: 0x040008BA RID: 2234
	public bool useFixedUpdate;

	// Token: 0x040008BB RID: 2235
	public bool isUseGuiLayout = true;

	// Token: 0x040008BC RID: 2236
	[SerializeField]
	private bool dynamicJoystick;

	// Token: 0x040008BD RID: 2237
	public EasyJoystick.DynamicArea area;

	// Token: 0x040008BE RID: 2238
	[SerializeField]
	private EasyJoystick.JoystickAnchor joyAnchor = EasyJoystick.JoystickAnchor.LowerLeft;

	// Token: 0x040008BF RID: 2239
	[SerializeField]
	private Vector2 joystickPositionOffset = Vector2.zero;

	// Token: 0x040008C0 RID: 2240
	[SerializeField]
	private float zoneRadius = 100f;

	// Token: 0x040008C1 RID: 2241
	[SerializeField]
	private float touchSize = 30f;

	// Token: 0x040008C2 RID: 2242
	public float deadZone = 20f;

	// Token: 0x040008C3 RID: 2243
	[SerializeField]
	private bool restrictArea;

	// Token: 0x040008C4 RID: 2244
	public bool resetFingerExit;

	// Token: 0x040008C5 RID: 2245
	[SerializeField]
	private EasyJoystick.InteractionType interaction;

	// Token: 0x040008C6 RID: 2246
	public bool useBroadcast;

	// Token: 0x040008C7 RID: 2247
	public EasyJoystick.Broadcast messageMode;

	// Token: 0x040008C8 RID: 2248
	public GameObject receiverGameObject;

	// Token: 0x040008C9 RID: 2249
	public Vector2 speed;

	// Token: 0x040008CA RID: 2250
	public bool enableXaxis = true;

	// Token: 0x040008CB RID: 2251
	[SerializeField]
	private Transform xAxisTransform;

	// Token: 0x040008CC RID: 2252
	public CharacterController xAxisCharacterController;

	// Token: 0x040008CD RID: 2253
	public float xAxisGravity;

	// Token: 0x040008CE RID: 2254
	[SerializeField]
	private EasyJoystick.PropertiesInfluenced xTI;

	// Token: 0x040008CF RID: 2255
	public EasyJoystick.AxisInfluenced xAI;

	// Token: 0x040008D0 RID: 2256
	public bool inverseXAxis;

	// Token: 0x040008D1 RID: 2257
	public bool enableXClamp;

	// Token: 0x040008D2 RID: 2258
	public float clampXMax;

	// Token: 0x040008D3 RID: 2259
	public float clampXMin;

	// Token: 0x040008D4 RID: 2260
	public bool enableXAutoStab;

	// Token: 0x040008D5 RID: 2261
	[SerializeField]
	private float thresholdX = 0.01f;

	// Token: 0x040008D6 RID: 2262
	[SerializeField]
	private float stabSpeedX = 20f;

	// Token: 0x040008D7 RID: 2263
	public bool enableYaxis = true;

	// Token: 0x040008D8 RID: 2264
	[SerializeField]
	private Transform yAxisTransform;

	// Token: 0x040008D9 RID: 2265
	public CharacterController yAxisCharacterController;

	// Token: 0x040008DA RID: 2266
	public float yAxisGravity;

	// Token: 0x040008DB RID: 2267
	[SerializeField]
	private EasyJoystick.PropertiesInfluenced yTI;

	// Token: 0x040008DC RID: 2268
	public EasyJoystick.AxisInfluenced yAI;

	// Token: 0x040008DD RID: 2269
	public bool inverseYAxis;

	// Token: 0x040008DE RID: 2270
	public bool enableYClamp;

	// Token: 0x040008DF RID: 2271
	public float clampYMax;

	// Token: 0x040008E0 RID: 2272
	public float clampYMin;

	// Token: 0x040008E1 RID: 2273
	public bool enableYAutoStab;

	// Token: 0x040008E2 RID: 2274
	[SerializeField]
	private float thresholdY = 0.01f;

	// Token: 0x040008E3 RID: 2275
	[SerializeField]
	private float stabSpeedY = 20f;

	// Token: 0x040008E4 RID: 2276
	public bool enableSmoothing;

	// Token: 0x040008E5 RID: 2277
	[SerializeField]
	public Vector2 smoothing = new Vector2(2f, 2f);

	// Token: 0x040008E6 RID: 2278
	public bool enableInertia;

	// Token: 0x040008E7 RID: 2279
	[SerializeField]
	public Vector2 inertia = new Vector2(100f, 100f);

	// Token: 0x040008E8 RID: 2280
	public int guiDepth;

	// Token: 0x040008E9 RID: 2281
	public bool showZone = true;

	// Token: 0x040008EA RID: 2282
	public bool showTouch = true;

	// Token: 0x040008EB RID: 2283
	public bool showDeadZone = true;

	// Token: 0x040008EC RID: 2284
	public Texture areaTexture;

	// Token: 0x040008ED RID: 2285
	public Color areaColor = Color.white;

	// Token: 0x040008EE RID: 2286
	public Texture touchTexture;

	// Token: 0x040008EF RID: 2287
	public Color touchColor = Color.white;

	// Token: 0x040008F0 RID: 2288
	public Texture deadTexture;

	// Token: 0x040008F1 RID: 2289
	public bool showProperties = true;

	// Token: 0x040008F2 RID: 2290
	public bool showInteraction;

	// Token: 0x040008F3 RID: 2291
	public bool showAppearance;

	// Token: 0x040008F4 RID: 2292
	public bool showPosition = true;

	// Token: 0x040008F5 RID: 2293
	private Vector2 joystickCenter;

	// Token: 0x040008F6 RID: 2294
	private Rect areaRect;

	// Token: 0x040008F7 RID: 2295
	private Rect deadRect;

	// Token: 0x040008F8 RID: 2296
	private Vector2 anchorPosition = Vector2.zero;

	// Token: 0x040008F9 RID: 2297
	private bool virtualJoystick = true;

	// Token: 0x040008FA RID: 2298
	private int joystickIndex = -1;

	// Token: 0x040008FB RID: 2299
	private float touchSizeCoef;

	// Token: 0x040008FC RID: 2300
	private bool sendEnd = true;

	// Token: 0x040008FD RID: 2301
	private float startXLocalAngle;

	// Token: 0x040008FE RID: 2302
	private float startYLocalAngle;

	// Token: 0x02001250 RID: 4688
	// (Invoke) Token: 0x060078E4 RID: 30948
	public delegate void JoystickMoveStartHandler(MovingJoystick move);

	// Token: 0x02001251 RID: 4689
	// (Invoke) Token: 0x060078E8 RID: 30952
	public delegate void JoystickMoveHandler(MovingJoystick move);

	// Token: 0x02001252 RID: 4690
	// (Invoke) Token: 0x060078EC RID: 30956
	public delegate void JoystickMoveEndHandler(MovingJoystick move);

	// Token: 0x02001253 RID: 4691
	// (Invoke) Token: 0x060078F0 RID: 30960
	public delegate void JoystickTouchStartHandler(MovingJoystick move);

	// Token: 0x02001254 RID: 4692
	// (Invoke) Token: 0x060078F4 RID: 30964
	public delegate void JoystickTapHandler(MovingJoystick move);

	// Token: 0x02001255 RID: 4693
	// (Invoke) Token: 0x060078F8 RID: 30968
	public delegate void JoystickDoubleTapHandler(MovingJoystick move);

	// Token: 0x02001256 RID: 4694
	// (Invoke) Token: 0x060078FC RID: 30972
	public delegate void JoystickTouchUpHandler(MovingJoystick move);

	// Token: 0x02001257 RID: 4695
	public enum JoystickAnchor
	{
		// Token: 0x04006566 RID: 25958
		None,
		// Token: 0x04006567 RID: 25959
		UpperLeft,
		// Token: 0x04006568 RID: 25960
		UpperCenter,
		// Token: 0x04006569 RID: 25961
		UpperRight,
		// Token: 0x0400656A RID: 25962
		MiddleLeft,
		// Token: 0x0400656B RID: 25963
		MiddleCenter,
		// Token: 0x0400656C RID: 25964
		MiddleRight,
		// Token: 0x0400656D RID: 25965
		LowerLeft,
		// Token: 0x0400656E RID: 25966
		LowerCenter,
		// Token: 0x0400656F RID: 25967
		LowerRight
	}

	// Token: 0x02001258 RID: 4696
	public enum PropertiesInfluenced
	{
		// Token: 0x04006571 RID: 25969
		Rotate,
		// Token: 0x04006572 RID: 25970
		RotateLocal,
		// Token: 0x04006573 RID: 25971
		Translate,
		// Token: 0x04006574 RID: 25972
		TranslateLocal,
		// Token: 0x04006575 RID: 25973
		Scale
	}

	// Token: 0x02001259 RID: 4697
	public enum AxisInfluenced
	{
		// Token: 0x04006577 RID: 25975
		X,
		// Token: 0x04006578 RID: 25976
		Y,
		// Token: 0x04006579 RID: 25977
		Z,
		// Token: 0x0400657A RID: 25978
		XYZ
	}

	// Token: 0x0200125A RID: 4698
	public enum DynamicArea
	{
		// Token: 0x0400657C RID: 25980
		FullScreen,
		// Token: 0x0400657D RID: 25981
		Left,
		// Token: 0x0400657E RID: 25982
		Right,
		// Token: 0x0400657F RID: 25983
		Top,
		// Token: 0x04006580 RID: 25984
		Bottom,
		// Token: 0x04006581 RID: 25985
		TopLeft,
		// Token: 0x04006582 RID: 25986
		TopRight,
		// Token: 0x04006583 RID: 25987
		BottomLeft,
		// Token: 0x04006584 RID: 25988
		BottomRight
	}

	// Token: 0x0200125B RID: 4699
	public enum InteractionType
	{
		// Token: 0x04006586 RID: 25990
		Direct,
		// Token: 0x04006587 RID: 25991
		Include,
		// Token: 0x04006588 RID: 25992
		EventNotification,
		// Token: 0x04006589 RID: 25993
		DirectAndEvent
	}

	// Token: 0x0200125C RID: 4700
	public enum Broadcast
	{
		// Token: 0x0400658B RID: 25995
		SendMessage,
		// Token: 0x0400658C RID: 25996
		SendMessageUpwards,
		// Token: 0x0400658D RID: 25997
		BroadcastMessage
	}

	// Token: 0x0200125D RID: 4701
	private enum MessageName
	{
		// Token: 0x0400658F RID: 25999
		On_JoystickMoveStart,
		// Token: 0x04006590 RID: 26000
		On_JoystickTouchStart,
		// Token: 0x04006591 RID: 26001
		On_JoystickTouchUp,
		// Token: 0x04006592 RID: 26002
		On_JoystickMove,
		// Token: 0x04006593 RID: 26003
		On_JoystickMoveEnd,
		// Token: 0x04006594 RID: 26004
		On_JoystickTap,
		// Token: 0x04006595 RID: 26005
		On_JoystickDoubleTap
	}
}
