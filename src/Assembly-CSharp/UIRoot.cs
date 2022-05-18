using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000118 RID: 280
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000B08 RID: 2824 RVA: 0x000904A4 File Offset: 0x0008E6A4
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

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
	public float pixelSizeAdjustment
	{
		get
		{
			return this.GetPixelSizeAdjustment(Mathf.RoundToInt(NGUITools.screenSize.y));
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0009056C File Offset: 0x0008E76C
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		if (!(uiroot != null))
		{
			return 1f;
		}
		return uiroot.pixelSizeAdjustment;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00090598 File Offset: 0x0008E798
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

	// Token: 0x06000B0C RID: 2828 RVA: 0x0000D1BB File Offset: 0x0000B3BB
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0000D1C9 File Offset: 0x0000B3C9
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x0000D1D6 File Offset: 0x0000B3D6
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x000905F0 File Offset: 0x0008E7F0
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

	// Token: 0x06000B10 RID: 2832 RVA: 0x00090648 File Offset: 0x0008E848
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

	// Token: 0x06000B11 RID: 2833 RVA: 0x000906D8 File Offset: 0x0008E8D8
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

	// Token: 0x06000B12 RID: 2834 RVA: 0x0009071C File Offset: 0x0008E91C
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

	// Token: 0x040007CA RID: 1994
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x040007CB RID: 1995
	public UIRoot.Scaling scalingStyle;

	// Token: 0x040007CC RID: 1996
	public int manualHeight = 720;

	// Token: 0x040007CD RID: 1997
	public int minimumHeight = 320;

	// Token: 0x040007CE RID: 1998
	public int maximumHeight = 1536;

	// Token: 0x040007CF RID: 1999
	public bool adjustByDPI;

	// Token: 0x040007D0 RID: 2000
	public bool shrinkPortraitUI;

	// Token: 0x040007D1 RID: 2001
	private Transform mTrans;

	// Token: 0x02000119 RID: 281
	public enum Scaling
	{
		// Token: 0x040007D3 RID: 2003
		PixelPerfect,
		// Token: 0x040007D4 RID: 2004
		FixedSize,
		// Token: 0x040007D5 RID: 2005
		FixedSizeOnMobiles
	}
}
