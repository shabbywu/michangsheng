using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
public abstract class UIRect : MonoBehaviour
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0002F30E File Offset: 0x0002D50E
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

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002F330 File Offset: 0x0002D530
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

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0002F352 File Offset: 0x0002D552
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

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0002F368 File Offset: 0x0002D568
	public bool isFullyAnchored
	{
		get
		{
			return this.leftAnchor.target && this.rightAnchor.target && this.topAnchor.target && this.bottomAnchor.target;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0002F3BD File Offset: 0x0002D5BD
	public virtual bool isAnchoredHorizontally
	{
		get
		{
			return this.leftAnchor.target || this.rightAnchor.target;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002F3E3 File Offset: 0x0002D5E3
	public virtual bool isAnchoredVertically
	{
		get
		{
			return this.bottomAnchor.target || this.topAnchor.target;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060007BB RID: 1979 RVA: 0x00024C5F File Offset: 0x00022E5F
	public virtual bool canBeAnchored
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060007BC RID: 1980 RVA: 0x0002F409 File Offset: 0x0002D609
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

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060007BD RID: 1981 RVA: 0x0002F438 File Offset: 0x0002D638
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

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002F488 File Offset: 0x0002D688
	public bool isAnchored
	{
		get
		{
			return (this.leftAnchor.target || this.rightAnchor.target || this.topAnchor.target || this.bottomAnchor.target) && this.canBeAnchored;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060007BF RID: 1983
	// (set) Token: 0x060007C0 RID: 1984
	public abstract float alpha { get; set; }

	// Token: 0x060007C1 RID: 1985
	public abstract float CalculateFinalAlpha(int frameID);

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060007C2 RID: 1986
	public abstract Vector3[] localCorners { get; }

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060007C3 RID: 1987
	public abstract Vector3[] worldCorners { get; }

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
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

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002F528 File Offset: 0x0002D728
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

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
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

	// Token: 0x060007C7 RID: 1991 RVA: 0x0002F68D File Offset: 0x0002D88D
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

	// Token: 0x060007C8 RID: 1992 RVA: 0x0002F6C1 File Offset: 0x0002D8C1
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

	// Token: 0x060007C9 RID: 1993 RVA: 0x0002F6F7 File Offset: 0x0002D8F7
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

	// Token: 0x060007CA RID: 1994 RVA: 0x0002F734 File Offset: 0x0002D934
	protected void Start()
	{
		this.mStarted = true;
		this.OnInit();
		this.OnStart();
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002F74C File Offset: 0x0002D94C
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

	// Token: 0x060007CC RID: 1996 RVA: 0x0002F8D4 File Offset: 0x0002DAD4
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

	// Token: 0x060007CD RID: 1997 RVA: 0x0002FA28 File Offset: 0x0002DC28
	public void UpdateAnchors()
	{
		if (this.isAnchored)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x060007CE RID: 1998
	protected abstract void OnAnchor();

	// Token: 0x060007CF RID: 1999 RVA: 0x0002FA38 File Offset: 0x0002DC38
	public void SetAnchor(Transform t)
	{
		this.leftAnchor.target = t;
		this.rightAnchor.target = t;
		this.topAnchor.target = t;
		this.bottomAnchor.target = t;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002FA78 File Offset: 0x0002DC78
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

	// Token: 0x060007D1 RID: 2001 RVA: 0x0002FAD4 File Offset: 0x0002DCD4
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

	// Token: 0x060007D2 RID: 2002 RVA: 0x0002FBA4 File Offset: 0x0002DDA4
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

	// Token: 0x060007D3 RID: 2003 RVA: 0x0002FCC5 File Offset: 0x0002DEC5
	public void ResetAndUpdateAnchors()
	{
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060007D4 RID: 2004
	public abstract void SetRect(float x, float y, float width, float height);

	// Token: 0x060007D5 RID: 2005 RVA: 0x0002FCD4 File Offset: 0x0002DED4
	private void FindCameraFor(UIRect.AnchorPoint ap)
	{
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
			return;
		}
		ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0002FD20 File Offset: 0x0002DF20
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

	// Token: 0x060007D7 RID: 2007
	protected abstract void OnStart();

	// Token: 0x060007D8 RID: 2008 RVA: 0x00004095 File Offset: 0x00002295
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x040004D1 RID: 1233
	public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

	// Token: 0x040004D2 RID: 1234
	public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x040004D3 RID: 1235
	public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

	// Token: 0x040004D4 RID: 1236
	public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x040004D5 RID: 1237
	public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnUpdate;

	// Token: 0x040004D6 RID: 1238
	protected GameObject mGo;

	// Token: 0x040004D7 RID: 1239
	protected Transform mTrans;

	// Token: 0x040004D8 RID: 1240
	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	// Token: 0x040004D9 RID: 1241
	protected bool mChanged = true;

	// Token: 0x040004DA RID: 1242
	protected bool mStarted;

	// Token: 0x040004DB RID: 1243
	protected bool mParentFound;

	// Token: 0x040004DC RID: 1244
	protected bool mUpdateAnchors;

	// Token: 0x040004DD RID: 1245
	[NonSerialized]
	public float finalAlpha = 1f;

	// Token: 0x040004DE RID: 1246
	private UIRoot mRoot;

	// Token: 0x040004DF RID: 1247
	private UIRect mParent;

	// Token: 0x040004E0 RID: 1248
	private Camera mMyCam;

	// Token: 0x040004E1 RID: 1249
	private int mUpdateFrame = -1;

	// Token: 0x040004E2 RID: 1250
	private bool mAnchorsCached;

	// Token: 0x040004E3 RID: 1251
	private bool mRootSet;

	// Token: 0x040004E4 RID: 1252
	private static Vector3[] mSides = new Vector3[4];

	// Token: 0x0200120D RID: 4621
	[Serializable]
	public class AnchorPoint
	{
		// Token: 0x06007855 RID: 30805 RVA: 0x000027FC File Offset: 0x000009FC
		public AnchorPoint()
		{
		}

		// Token: 0x06007856 RID: 30806 RVA: 0x002BA6B8 File Offset: 0x002B88B8
		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x002BA6C7 File Offset: 0x002B88C7
		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x002BA6E2 File Offset: 0x002B88E2
		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x06007859 RID: 30809 RVA: 0x002BA704 File Offset: 0x002B8904
		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			this.SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		// Token: 0x0600785A RID: 30810 RVA: 0x002BA720 File Offset: 0x002B8920
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

		// Token: 0x0600785B RID: 30811 RVA: 0x002BA774 File Offset: 0x002B8974
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

		// Token: 0x0600785C RID: 30812 RVA: 0x002BA80C File Offset: 0x002B8A0C
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

		// Token: 0x0600785D RID: 30813 RVA: 0x002BA8A4 File Offset: 0x002B8AA4
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

		// Token: 0x0400645C RID: 25692
		public Transform target;

		// Token: 0x0400645D RID: 25693
		public float relative;

		// Token: 0x0400645E RID: 25694
		public int absolute;

		// Token: 0x0400645F RID: 25695
		[NonSerialized]
		public UIRect rect;

		// Token: 0x04006460 RID: 25696
		[NonSerialized]
		public Camera targetCam;
	}

	// Token: 0x0200120E RID: 4622
	public enum AnchorUpdate
	{
		// Token: 0x04006462 RID: 25698
		OnEnable,
		// Token: 0x04006463 RID: 25699
		OnUpdate
	}
}
