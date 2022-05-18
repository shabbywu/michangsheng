using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public abstract class UIRect : MonoBehaviour
{
	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000854 RID: 2132 RVA: 0x0000ADA5 File Offset: 0x00008FA5
	public GameObject cachedGameObject
	{
		get
		{
			if (this.mGo == null)
			{
				this.mGo = base.gameObject;
			}
			return this.mGo;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000855 RID: 2133 RVA: 0x0000ADC7 File Offset: 0x00008FC7
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000856 RID: 2134 RVA: 0x0000ADE9 File Offset: 0x00008FE9
	public Camera anchorCamera
	{
		get
		{
			if (!this.mAnchorsCached)
			{
				this.ResetAnchors();
			}
			return this.mMyCam;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000857 RID: 2135 RVA: 0x00083D40 File Offset: 0x00081F40
	public bool isFullyAnchored
	{
		get
		{
			return this.leftAnchor.target && this.rightAnchor.target && this.topAnchor.target && this.bottomAnchor.target;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000858 RID: 2136 RVA: 0x0000ADFF File Offset: 0x00008FFF
	public virtual bool isAnchoredHorizontally
	{
		get
		{
			return this.leftAnchor.target || this.rightAnchor.target;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000859 RID: 2137 RVA: 0x0000AE25 File Offset: 0x00009025
	public virtual bool isAnchoredVertically
	{
		get
		{
			return this.bottomAnchor.target || this.topAnchor.target;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000A093 File Offset: 0x00008293
	public virtual bool canBeAnchored
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000AE4B File Offset: 0x0000904B
	public UIRect parent
	{
		get
		{
			if (!this.mParentFound)
			{
				this.mParentFound = true;
				this.mParent = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
			}
			return this.mParent;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x0600085C RID: 2140 RVA: 0x00083D98 File Offset: 0x00081F98
	public UIRoot root
	{
		get
		{
			if (this.parent != null)
			{
				return this.mParent.root;
			}
			if (!this.mRootSet)
			{
				this.mRootSet = true;
				this.mRoot = NGUITools.FindInParents<UIRoot>(this.cachedTransform);
			}
			return this.mRoot;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x0600085D RID: 2141 RVA: 0x00083DE8 File Offset: 0x00081FE8
	public bool isAnchored
	{
		get
		{
			return (this.leftAnchor.target || this.rightAnchor.target || this.topAnchor.target || this.bottomAnchor.target) && this.canBeAnchored;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x0600085E RID: 2142
	// (set) Token: 0x0600085F RID: 2143
	public abstract float alpha { get; set; }

	// Token: 0x06000860 RID: 2144
	public abstract float CalculateFinalAlpha(int frameID);

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000861 RID: 2145
	public abstract Vector3[] localCorners { get; }

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000862 RID: 2146
	public abstract Vector3[] worldCorners { get; }

	// Token: 0x06000863 RID: 2147 RVA: 0x00083E48 File Offset: 0x00082048
	public virtual void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < this.mChildren.size; i++)
			{
				this.mChildren.buffer[i].Invalidate(true);
			}
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00083E88 File Offset: 0x00082088
	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		if (this.anchorCamera != null)
		{
			Vector3[] sides = this.anchorCamera.GetSides(relativeTo);
			UIRoot root = this.root;
			if (root != null)
			{
				float pixelSizeAdjustment = root.pixelSizeAdjustment;
				for (int i = 0; i < 4; i++)
				{
					sides[i] *= pixelSizeAdjustment;
				}
			}
			return sides;
		}
		Vector3 position = this.cachedTransform.position;
		for (int j = 0; j < 4; j++)
		{
			UIRect.mSides[j] = position;
		}
		if (relativeTo != null)
		{
			for (int k = 0; k < 4; k++)
			{
				UIRect.mSides[k] = relativeTo.InverseTransformPoint(UIRect.mSides[k]);
			}
		}
		return UIRect.mSides;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00083F54 File Offset: 0x00082154
	protected Vector3 GetLocalPos(UIRect.AnchorPoint ac, Transform trans)
	{
		if (this.anchorCamera == null || ac.targetCam == null)
		{
			return this.cachedTransform.localPosition;
		}
		Vector3 vector = this.mMyCam.ViewportToWorldPoint(ac.targetCam.WorldToViewportPoint(ac.target.position));
		if (trans != null)
		{
			vector = trans.InverseTransformPoint(vector);
		}
		vector.x = Mathf.Floor(vector.x + 0.5f);
		vector.y = Mathf.Floor(vector.y + 0.5f);
		return vector;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0000AE78 File Offset: 0x00009078
	protected virtual void OnEnable()
	{
		this.mAnchorsCached = false;
		this.mUpdateFrame = -1;
		if (this.updateAnchors == UIRect.AnchorUpdate.OnEnable)
		{
			this.mUpdateAnchors = true;
		}
		if (this.mStarted)
		{
			this.OnInit();
		}
		this.mUpdateFrame = -1;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0000AEAC File Offset: 0x000090AC
	protected virtual void OnInit()
	{
		this.mChanged = true;
		this.mRootSet = false;
		this.mParentFound = false;
		if (this.parent != null)
		{
			this.mParent.mChildren.Add(this);
		}
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0000AEE2 File Offset: 0x000090E2
	protected virtual void OnDisable()
	{
		if (this.mParent)
		{
			this.mParent.mChildren.Remove(this);
		}
		this.mParent = null;
		this.mRoot = null;
		this.mRootSet = false;
		this.mParentFound = false;
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0000AF1F File Offset: 0x0000911F
	protected void Start()
	{
		this.mStarted = true;
		this.OnInit();
		this.OnStart();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00083FF0 File Offset: 0x000821F0
	public void Update()
	{
		if (!this.mAnchorsCached)
		{
			this.ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (this.mUpdateFrame != frameCount)
		{
			if (this.updateAnchors == UIRect.AnchorUpdate.OnUpdate || this.mUpdateAnchors)
			{
				this.mUpdateFrame = frameCount;
				this.mUpdateAnchors = false;
				bool flag = false;
				if (this.leftAnchor.target)
				{
					flag = true;
					if (this.leftAnchor.rect != null && this.leftAnchor.rect.mUpdateFrame != frameCount)
					{
						this.leftAnchor.rect.Update();
					}
				}
				if (this.bottomAnchor.target)
				{
					flag = true;
					if (this.bottomAnchor.rect != null && this.bottomAnchor.rect.mUpdateFrame != frameCount)
					{
						this.bottomAnchor.rect.Update();
					}
				}
				if (this.rightAnchor.target)
				{
					flag = true;
					if (this.rightAnchor.rect != null && this.rightAnchor.rect.mUpdateFrame != frameCount)
					{
						this.rightAnchor.rect.Update();
					}
				}
				if (this.topAnchor.target)
				{
					flag = true;
					if (this.topAnchor.rect != null && this.topAnchor.rect.mUpdateFrame != frameCount)
					{
						this.topAnchor.rect.Update();
					}
				}
				if (flag)
				{
					this.OnAnchor();
				}
			}
			this.OnUpdate();
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00084178 File Offset: 0x00082378
	public void updatePostion()
	{
		int frameCount = Time.frameCount;
		this.mUpdateFrame = frameCount;
		this.mUpdateAnchors = false;
		bool flag = false;
		if (this.leftAnchor.target)
		{
			flag = true;
			if (this.leftAnchor.rect != null && this.leftAnchor.rect.mUpdateFrame != frameCount)
			{
				this.leftAnchor.rect.Update();
			}
		}
		if (this.bottomAnchor.target)
		{
			flag = true;
			if (this.bottomAnchor.rect != null && this.bottomAnchor.rect.mUpdateFrame != frameCount)
			{
				this.bottomAnchor.rect.Update();
			}
		}
		if (this.rightAnchor.target)
		{
			flag = true;
			if (this.rightAnchor.rect != null && this.rightAnchor.rect.mUpdateFrame != frameCount)
			{
				this.rightAnchor.rect.Update();
			}
		}
		if (this.topAnchor.target)
		{
			flag = true;
			if (this.topAnchor.rect != null && this.topAnchor.rect.mUpdateFrame != frameCount)
			{
				this.topAnchor.rect.Update();
			}
		}
		if (flag)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0000AF34 File Offset: 0x00009134
	public void UpdateAnchors()
	{
		if (this.isAnchored)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x0600086D RID: 2157
	protected abstract void OnAnchor();

	// Token: 0x0600086E RID: 2158 RVA: 0x0000AF44 File Offset: 0x00009144
	public void SetAnchor(Transform t)
	{
		this.leftAnchor.target = t;
		this.rightAnchor.target = t;
		this.topAnchor.target = t;
		this.bottomAnchor.target = t;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000842CC File Offset: 0x000824CC
	public void SetAnchor(GameObject go)
	{
		Transform target = (go != null) ? go.transform : null;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00084328 File Offset: 0x00082528
	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (go != null) ? go.transform : null;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = 0f;
		this.rightAnchor.relative = 1f;
		this.bottomAnchor.relative = 0f;
		this.topAnchor.relative = 1f;
		this.leftAnchor.absolute = left;
		this.rightAnchor.absolute = right;
		this.bottomAnchor.absolute = bottom;
		this.topAnchor.absolute = top;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x000843F8 File Offset: 0x000825F8
	public void ResetAnchors()
	{
		this.mAnchorsCached = true;
		this.leftAnchor.rect = (this.leftAnchor.target ? this.leftAnchor.target.GetComponent<UIRect>() : null);
		this.bottomAnchor.rect = (this.bottomAnchor.target ? this.bottomAnchor.target.GetComponent<UIRect>() : null);
		this.rightAnchor.rect = (this.rightAnchor.target ? this.rightAnchor.target.GetComponent<UIRect>() : null);
		this.topAnchor.rect = (this.topAnchor.target ? this.topAnchor.target.GetComponent<UIRect>() : null);
		this.mMyCam = NGUITools.FindCameraForLayer(this.cachedGameObject.layer);
		this.FindCameraFor(this.leftAnchor);
		this.FindCameraFor(this.bottomAnchor);
		this.FindCameraFor(this.rightAnchor);
		this.FindCameraFor(this.topAnchor);
		this.mUpdateAnchors = true;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0000AF82 File Offset: 0x00009182
	public void ResetAndUpdateAnchors()
	{
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000873 RID: 2163
	public abstract void SetRect(float x, float y, float width, float height);

	// Token: 0x06000874 RID: 2164 RVA: 0x0008451C File Offset: 0x0008271C
	private void FindCameraFor(UIRect.AnchorPoint ap)
	{
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
			return;
		}
		ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00084568 File Offset: 0x00082768
	public virtual void ParentHasChanged()
	{
		this.mParentFound = false;
		UIRect uirect = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
		if (this.mParent != uirect)
		{
			if (this.mParent)
			{
				this.mParent.mChildren.Remove(this);
			}
			this.mParent = uirect;
			if (this.mParent)
			{
				this.mParent.mChildren.Add(this);
			}
			this.mRootSet = false;
		}
	}

	// Token: 0x06000876 RID: 2166
	protected abstract void OnStart();

	// Token: 0x06000877 RID: 2167 RVA: 0x000042DD File Offset: 0x000024DD
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x040005DE RID: 1502
	public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

	// Token: 0x040005DF RID: 1503
	public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x040005E0 RID: 1504
	public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

	// Token: 0x040005E1 RID: 1505
	public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x040005E2 RID: 1506
	public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnUpdate;

	// Token: 0x040005E3 RID: 1507
	protected GameObject mGo;

	// Token: 0x040005E4 RID: 1508
	protected Transform mTrans;

	// Token: 0x040005E5 RID: 1509
	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	// Token: 0x040005E6 RID: 1510
	protected bool mChanged = true;

	// Token: 0x040005E7 RID: 1511
	protected bool mStarted;

	// Token: 0x040005E8 RID: 1512
	protected bool mParentFound;

	// Token: 0x040005E9 RID: 1513
	protected bool mUpdateAnchors;

	// Token: 0x040005EA RID: 1514
	[NonSerialized]
	public float finalAlpha = 1f;

	// Token: 0x040005EB RID: 1515
	private UIRoot mRoot;

	// Token: 0x040005EC RID: 1516
	private UIRect mParent;

	// Token: 0x040005ED RID: 1517
	private Camera mMyCam;

	// Token: 0x040005EE RID: 1518
	private int mUpdateFrame = -1;

	// Token: 0x040005EF RID: 1519
	private bool mAnchorsCached;

	// Token: 0x040005F0 RID: 1520
	private bool mRootSet;

	// Token: 0x040005F1 RID: 1521
	private static Vector3[] mSides = new Vector3[4];

	// Token: 0x020000DA RID: 218
	[Serializable]
	public class AnchorPoint
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0000403D File Offset: 0x0000223D
		public AnchorPoint()
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000AF9D File Offset: 0x0000919D
		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000AFAC File Offset: 0x000091AC
		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000AFC7 File Offset: 0x000091C7
		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0000AFE9 File Offset: 0x000091E9
		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			this.SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0008465C File Offset: 0x0008285C
		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				this.Set(rel0, abs0);
				return;
			}
			if (num2 < num && num2 < num3)
			{
				this.Set(rel1, abs1);
				return;
			}
			this.Set(rel2, abs2);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000846B0 File Offset: 0x000828B0
		public void SetHorizontal(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
				return;
			}
			Vector3 vector = this.target.position;
			if (parent != null)
			{
				vector = parent.InverseTransformPoint(vector);
			}
			this.absolute = Mathf.FloorToInt(localPos - vector.x + 0.5f);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00084748 File Offset: 0x00082948
		public void SetVertical(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
				return;
			}
			Vector3 vector = this.target.position;
			if (parent != null)
			{
				vector = parent.InverseTransformPoint(vector);
			}
			this.absolute = Mathf.FloorToInt(localPos - vector.y + 0.5f);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000847E0 File Offset: 0x000829E0
		public Vector3[] GetSides(Transform relativeTo)
		{
			if (this.target != null)
			{
				if (this.rect != null)
				{
					return this.rect.GetSides(relativeTo);
				}
				if (this.target.GetComponent<Camera>() != null)
				{
					return this.target.GetComponent<Camera>().GetSides(relativeTo);
				}
			}
			return null;
		}

		// Token: 0x040005F2 RID: 1522
		public Transform target;

		// Token: 0x040005F3 RID: 1523
		public float relative;

		// Token: 0x040005F4 RID: 1524
		public int absolute;

		// Token: 0x040005F5 RID: 1525
		[NonSerialized]
		public UIRect rect;

		// Token: 0x040005F6 RID: 1526
		[NonSerialized]
		public Camera targetCam;
	}

	// Token: 0x020000DB RID: 219
	public enum AnchorUpdate
	{
		// Token: 0x040005F8 RID: 1528
		OnEnable,
		// Token: 0x040005F9 RID: 1529
		OnUpdate
	}
}
