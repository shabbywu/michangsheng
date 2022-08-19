using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AD RID: 173
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0003DA78 File Offset: 0x0003BC78
	public int activeHeight
	{
		get
		{
			if (this.scalingStyle == UIRoot.Scaling.FixedSize)
			{
				return this.manualHeight;
			}
			Vector2 screenSize = NGUITools.screenSize;
			float num = screenSize.x / screenSize.y;
			if (screenSize.y < (float)this.minimumHeight)
			{
				screenSize.y = (float)this.minimumHeight;
				screenSize.x = screenSize.y * num;
			}
			else if (screenSize.y > (float)this.maximumHeight)
			{
				screenSize.y = (float)this.maximumHeight;
				screenSize.x = screenSize.y * num;
			}
			int num2 = Mathf.RoundToInt((this.shrinkPortraitUI && screenSize.y > screenSize.x) ? (screenSize.y / num) : screenSize.y);
			if (!this.adjustByDPI)
			{
				return num2;
			}
			return NGUIMath.AdjustByDPI((float)num2);
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0003DB40 File Offset: 0x0003BD40
	public float pixelSizeAdjustment
	{
		get
		{
			return this.GetPixelSizeAdjustment(Mathf.RoundToInt(NGUITools.screenSize.y));
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0003DB58 File Offset: 0x0003BD58
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		if (!(uiroot != null))
		{
			return 1f;
		}
		return uiroot.pixelSizeAdjustment;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0003DB84 File Offset: 0x0003BD84
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.scalingStyle == UIRoot.Scaling.FixedSize)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0003DBDB File Offset: 0x0003BDDB
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003DBE9 File Offset: 0x0003BDE9
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0003DBF6 File Offset: 0x0003BDF6
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0003DC04 File Offset: 0x0003BE04
	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
				return;
			}
		}
		else
		{
			this.Update();
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0003DC5C File Offset: 0x0003BE5C
	private void Update()
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1E-45f || Mathf.Abs(localScale.y - num2) > 1E-45f || Mathf.Abs(localScale.z - num2) > 1E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
				}
			}
		}
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0003DCEC File Offset: 0x0003BEEC
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, 1);
			}
			i++;
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0003DD30 File Offset: 0x0003BF30
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, param, 1);
			}
			i++;
		}
	}

	// Token: 0x04000637 RID: 1591
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x04000638 RID: 1592
	public UIRoot.Scaling scalingStyle;

	// Token: 0x04000639 RID: 1593
	public int manualHeight = 720;

	// Token: 0x0400063A RID: 1594
	public int minimumHeight = 320;

	// Token: 0x0400063B RID: 1595
	public int maximumHeight = 1536;

	// Token: 0x0400063C RID: 1596
	public bool adjustByDPI;

	// Token: 0x0400063D RID: 1597
	public bool shrinkPortraitUI;

	// Token: 0x0400063E RID: 1598
	private Transform mTrans;

	// Token: 0x0200122F RID: 4655
	public enum Scaling
	{
		// Token: 0x040064E3 RID: 25827
		PixelPerfect,
		// Token: 0x040064E4 RID: 25828
		FixedSize,
		// Token: 0x040064E5 RID: 25829
		FixedSizeOnMobiles
	}
}
