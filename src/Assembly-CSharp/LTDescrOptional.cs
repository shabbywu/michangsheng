using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class LTDescrOptional
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060002C1 RID: 705 RVA: 0x00006A22 File Offset: 0x00004C22
	// (set) Token: 0x060002C2 RID: 706 RVA: 0x00006A2A File Offset: 0x00004C2A
	public Transform toTrans { get; set; }

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x00006A33 File Offset: 0x00004C33
	// (set) Token: 0x060002C4 RID: 708 RVA: 0x00006A3B File Offset: 0x00004C3B
	public Vector3 point { get; set; }

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060002C5 RID: 709 RVA: 0x00006A44 File Offset: 0x00004C44
	// (set) Token: 0x060002C6 RID: 710 RVA: 0x00006A4C File Offset: 0x00004C4C
	public Vector3 axis { get; set; }

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x00006A55 File Offset: 0x00004C55
	// (set) Token: 0x060002C8 RID: 712 RVA: 0x00006A5D File Offset: 0x00004C5D
	public float lastVal { get; set; }

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x00006A66 File Offset: 0x00004C66
	// (set) Token: 0x060002CA RID: 714 RVA: 0x00006A6E File Offset: 0x00004C6E
	public Quaternion origRotation { get; set; }

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060002CB RID: 715 RVA: 0x00006A77 File Offset: 0x00004C77
	// (set) Token: 0x060002CC RID: 716 RVA: 0x00006A7F File Offset: 0x00004C7F
	public LTBezierPath path { get; set; }

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060002CD RID: 717 RVA: 0x00006A88 File Offset: 0x00004C88
	// (set) Token: 0x060002CE RID: 718 RVA: 0x00006A90 File Offset: 0x00004C90
	public LTSpline spline { get; set; }

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060002CF RID: 719 RVA: 0x00006A99 File Offset: 0x00004C99
	// (set) Token: 0x060002D0 RID: 720 RVA: 0x00006AA1 File Offset: 0x00004CA1
	public LTRect ltRect { get; set; }

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006AAA File Offset: 0x00004CAA
	// (set) Token: 0x060002D2 RID: 722 RVA: 0x00006AB2 File Offset: 0x00004CB2
	public Action<float> onUpdateFloat { get; set; }

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060002D3 RID: 723 RVA: 0x00006ABB File Offset: 0x00004CBB
	// (set) Token: 0x060002D4 RID: 724 RVA: 0x00006AC3 File Offset: 0x00004CC3
	public Action<float, float> onUpdateFloatRatio { get; set; }

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060002D5 RID: 725 RVA: 0x00006ACC File Offset: 0x00004CCC
	// (set) Token: 0x060002D6 RID: 726 RVA: 0x00006AD4 File Offset: 0x00004CD4
	public Action<float, object> onUpdateFloatObject { get; set; }

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002D7 RID: 727 RVA: 0x00006ADD File Offset: 0x00004CDD
	// (set) Token: 0x060002D8 RID: 728 RVA: 0x00006AE5 File Offset: 0x00004CE5
	public Action<Vector2> onUpdateVector2 { get; set; }

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002D9 RID: 729 RVA: 0x00006AEE File Offset: 0x00004CEE
	// (set) Token: 0x060002DA RID: 730 RVA: 0x00006AF6 File Offset: 0x00004CF6
	public Action<Vector3> onUpdateVector3 { get; set; }

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060002DB RID: 731 RVA: 0x00006AFF File Offset: 0x00004CFF
	// (set) Token: 0x060002DC RID: 732 RVA: 0x00006B07 File Offset: 0x00004D07
	public Action<Vector3, object> onUpdateVector3Object { get; set; }

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060002DD RID: 733 RVA: 0x00006B10 File Offset: 0x00004D10
	// (set) Token: 0x060002DE RID: 734 RVA: 0x00006B18 File Offset: 0x00004D18
	public Action<Color> onUpdateColor { get; set; }

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060002DF RID: 735 RVA: 0x00006B21 File Offset: 0x00004D21
	// (set) Token: 0x060002E0 RID: 736 RVA: 0x00006B29 File Offset: 0x00004D29
	public Action<Color, object> onUpdateColorObject { get; set; }

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060002E1 RID: 737 RVA: 0x00006B32 File Offset: 0x00004D32
	// (set) Token: 0x060002E2 RID: 738 RVA: 0x00006B3A File Offset: 0x00004D3A
	public Action onComplete { get; set; }

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060002E3 RID: 739 RVA: 0x00006B43 File Offset: 0x00004D43
	// (set) Token: 0x060002E4 RID: 740 RVA: 0x00006B4B File Offset: 0x00004D4B
	public Action<object> onCompleteObject { get; set; }

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060002E5 RID: 741 RVA: 0x00006B54 File Offset: 0x00004D54
	// (set) Token: 0x060002E6 RID: 742 RVA: 0x00006B5C File Offset: 0x00004D5C
	public object onCompleteParam { get; set; }

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060002E7 RID: 743 RVA: 0x00006B65 File Offset: 0x00004D65
	// (set) Token: 0x060002E8 RID: 744 RVA: 0x00006B6D File Offset: 0x00004D6D
	public object onUpdateParam { get; set; }

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060002E9 RID: 745 RVA: 0x00006B76 File Offset: 0x00004D76
	// (set) Token: 0x060002EA RID: 746 RVA: 0x00006B7E File Offset: 0x00004D7E
	public Action onStart { get; set; }

	// Token: 0x060002EB RID: 747 RVA: 0x00066B18 File Offset: 0x00064D18
	public void reset()
	{
		this.animationCurve = null;
		this.onUpdateFloat = null;
		this.onUpdateFloatRatio = null;
		this.onUpdateVector2 = null;
		this.onUpdateVector3 = null;
		this.onUpdateFloatObject = null;
		this.onUpdateVector3Object = null;
		this.onUpdateColor = null;
		this.onComplete = null;
		this.onCompleteObject = null;
		this.onCompleteParam = null;
		this.onStart = null;
		this.point = Vector3.zero;
		this.initFrameCount = 0;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00066B8C File Offset: 0x00064D8C
	public void callOnUpdate(float val, float ratioPassed)
	{
		if (this.onUpdateFloat != null)
		{
			this.onUpdateFloat(val);
		}
		if (this.onUpdateFloatRatio != null)
		{
			this.onUpdateFloatRatio(val, ratioPassed);
			return;
		}
		if (this.onUpdateFloatObject != null)
		{
			this.onUpdateFloatObject(val, this.onUpdateParam);
			return;
		}
		if (this.onUpdateVector3Object != null)
		{
			this.onUpdateVector3Object(LTDescr.newVect, this.onUpdateParam);
			return;
		}
		if (this.onUpdateVector3 != null)
		{
			this.onUpdateVector3(LTDescr.newVect);
			return;
		}
		if (this.onUpdateVector2 != null)
		{
			this.onUpdateVector2(new Vector2(LTDescr.newVect.x, LTDescr.newVect.y));
		}
	}

	// Token: 0x0400016E RID: 366
	public AnimationCurve animationCurve;

	// Token: 0x0400016F RID: 367
	public int initFrameCount;
}
