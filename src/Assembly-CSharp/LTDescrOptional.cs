using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class LTDescrOptional
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060002AF RID: 687 RVA: 0x0000EC06 File Offset: 0x0000CE06
	// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000EC0E File Offset: 0x0000CE0E
	public Transform toTrans { get; set; }

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000EC17 File Offset: 0x0000CE17
	// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000EC1F File Offset: 0x0000CE1F
	public Vector3 point { get; set; }

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000EC28 File Offset: 0x0000CE28
	// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000EC30 File Offset: 0x0000CE30
	public Vector3 axis { get; set; }

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000EC39 File Offset: 0x0000CE39
	// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000EC41 File Offset: 0x0000CE41
	public float lastVal { get; set; }

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000EC4A File Offset: 0x0000CE4A
	// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000EC52 File Offset: 0x0000CE52
	public Quaternion origRotation { get; set; }

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000EC5B File Offset: 0x0000CE5B
	// (set) Token: 0x060002BA RID: 698 RVA: 0x0000EC63 File Offset: 0x0000CE63
	public LTBezierPath path { get; set; }

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060002BB RID: 699 RVA: 0x0000EC6C File Offset: 0x0000CE6C
	// (set) Token: 0x060002BC RID: 700 RVA: 0x0000EC74 File Offset: 0x0000CE74
	public LTSpline spline { get; set; }

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060002BD RID: 701 RVA: 0x0000EC7D File Offset: 0x0000CE7D
	// (set) Token: 0x060002BE RID: 702 RVA: 0x0000EC85 File Offset: 0x0000CE85
	public LTRect ltRect { get; set; }

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060002BF RID: 703 RVA: 0x0000EC8E File Offset: 0x0000CE8E
	// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000EC96 File Offset: 0x0000CE96
	public Action<float> onUpdateFloat { get; set; }

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000EC9F File Offset: 0x0000CE9F
	// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000ECA7 File Offset: 0x0000CEA7
	public Action<float, float> onUpdateFloatRatio { get; set; }

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
	// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
	public Action<float, object> onUpdateFloatObject { get; set; }

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000ECC1 File Offset: 0x0000CEC1
	// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
	public Action<Vector2> onUpdateVector2 { get; set; }

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000ECD2 File Offset: 0x0000CED2
	// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000ECDA File Offset: 0x0000CEDA
	public Action<Vector3> onUpdateVector3 { get; set; }

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000ECE3 File Offset: 0x0000CEE3
	// (set) Token: 0x060002CA RID: 714 RVA: 0x0000ECEB File Offset: 0x0000CEEB
	public Action<Vector3, object> onUpdateVector3Object { get; set; }

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002CB RID: 715 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
	// (set) Token: 0x060002CC RID: 716 RVA: 0x0000ECFC File Offset: 0x0000CEFC
	public Action<Color> onUpdateColor { get; set; }

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060002CD RID: 717 RVA: 0x0000ED05 File Offset: 0x0000CF05
	// (set) Token: 0x060002CE RID: 718 RVA: 0x0000ED0D File Offset: 0x0000CF0D
	public Action<Color, object> onUpdateColorObject { get; set; }

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060002CF RID: 719 RVA: 0x0000ED16 File Offset: 0x0000CF16
	// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000ED1E File Offset: 0x0000CF1E
	public Action onComplete { get; set; }

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000ED27 File Offset: 0x0000CF27
	// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000ED2F File Offset: 0x0000CF2F
	public Action<object> onCompleteObject { get; set; }

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000ED38 File Offset: 0x0000CF38
	// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000ED40 File Offset: 0x0000CF40
	public object onCompleteParam { get; set; }

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000ED49 File Offset: 0x0000CF49
	// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000ED51 File Offset: 0x0000CF51
	public object onUpdateParam { get; set; }

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000ED5A File Offset: 0x0000CF5A
	// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000ED62 File Offset: 0x0000CF62
	public Action onStart { get; set; }

	// Token: 0x060002D9 RID: 729 RVA: 0x0000ED6C File Offset: 0x0000CF6C
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

	// Token: 0x060002DA RID: 730 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
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

	// Token: 0x04000159 RID: 345
	public AnimationCurve animationCurve;

	// Token: 0x0400015A RID: 346
	public int initFrameCount;
}
