using System;
using UnityEngine;

public abstract class UIRect : MonoBehaviour
{
	[Serializable]
	public class AnchorPoint
	{
		public Transform target;

		public float relative;

		public int absolute;

		[NonSerialized]
		public UIRect rect;

		[NonSerialized]
		public Camera targetCam;

		public AnchorPoint()
		{
		}

		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				Set(rel0, abs0);
			}
			else if (num2 < num && num2 < num3)
			{
				Set(rel1, abs1);
			}
			else
			{
				Set(rel2, abs2);
			}
		}

		public void SetHorizontal(Transform parent, float localPos)
		{
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			if (Object.op_Implicit((Object)(object)rect))
			{
				Vector3[] sides = rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, relative);
				absolute = Mathf.FloorToInt(localPos - num + 0.5f);
				return;
			}
			Vector3 val = target.position;
			if ((Object)(object)parent != (Object)null)
			{
				val = parent.InverseTransformPoint(val);
			}
			absolute = Mathf.FloorToInt(localPos - val.x + 0.5f);
		}

		public void SetVertical(Transform parent, float localPos)
		{
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			if (Object.op_Implicit((Object)(object)rect))
			{
				Vector3[] sides = rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, relative);
				absolute = Mathf.FloorToInt(localPos - num + 0.5f);
				return;
			}
			Vector3 val = target.position;
			if ((Object)(object)parent != (Object)null)
			{
				val = parent.InverseTransformPoint(val);
			}
			absolute = Mathf.FloorToInt(localPos - val.y + 0.5f);
		}

		public Vector3[] GetSides(Transform relativeTo)
		{
			if ((Object)(object)target != (Object)null)
			{
				if ((Object)(object)rect != (Object)null)
				{
					return rect.GetSides(relativeTo);
				}
				if ((Object)(object)((Component)target).GetComponent<Camera>() != (Object)null)
				{
					return ((Component)target).GetComponent<Camera>().GetSides(relativeTo);
				}
			}
			return null;
		}
	}

	public enum AnchorUpdate
	{
		OnEnable,
		OnUpdate
	}

	public AnchorPoint leftAnchor = new AnchorPoint();

	public AnchorPoint rightAnchor = new AnchorPoint(1f);

	public AnchorPoint bottomAnchor = new AnchorPoint();

	public AnchorPoint topAnchor = new AnchorPoint(1f);

	public AnchorUpdate updateAnchors = AnchorUpdate.OnUpdate;

	protected GameObject mGo;

	protected Transform mTrans;

	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	protected bool mChanged = true;

	protected bool mStarted;

	protected bool mParentFound;

	protected bool mUpdateAnchors;

	[NonSerialized]
	public float finalAlpha = 1f;

	private UIRoot mRoot;

	private UIRect mParent;

	private Camera mMyCam;

	private int mUpdateFrame = -1;

	private bool mAnchorsCached;

	private bool mRootSet;

	private static Vector3[] mSides = (Vector3[])(object)new Vector3[4];

	public GameObject cachedGameObject
	{
		get
		{
			if ((Object)(object)mGo == (Object)null)
			{
				mGo = ((Component)this).gameObject;
			}
			return mGo;
		}
	}

	public Transform cachedTransform
	{
		get
		{
			if ((Object)(object)mTrans == (Object)null)
			{
				mTrans = ((Component)this).transform;
			}
			return mTrans;
		}
	}

	public Camera anchorCamera
	{
		get
		{
			if (!mAnchorsCached)
			{
				ResetAnchors();
			}
			return mMyCam;
		}
	}

	public bool isFullyAnchored
	{
		get
		{
			if (Object.op_Implicit((Object)(object)leftAnchor.target) && Object.op_Implicit((Object)(object)rightAnchor.target) && Object.op_Implicit((Object)(object)topAnchor.target))
			{
				return Object.op_Implicit((Object)(object)bottomAnchor.target);
			}
			return false;
		}
	}

	public virtual bool isAnchoredHorizontally
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)leftAnchor.target))
			{
				return Object.op_Implicit((Object)(object)rightAnchor.target);
			}
			return true;
		}
	}

	public virtual bool isAnchoredVertically
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)bottomAnchor.target))
			{
				return Object.op_Implicit((Object)(object)topAnchor.target);
			}
			return true;
		}
	}

	public virtual bool canBeAnchored => true;

	public UIRect parent
	{
		get
		{
			if (!mParentFound)
			{
				mParentFound = true;
				mParent = NGUITools.FindInParents<UIRect>(cachedTransform.parent);
			}
			return mParent;
		}
	}

	public UIRoot root
	{
		get
		{
			if ((Object)(object)parent != (Object)null)
			{
				return mParent.root;
			}
			if (!mRootSet)
			{
				mRootSet = true;
				mRoot = NGUITools.FindInParents<UIRoot>(cachedTransform);
			}
			return mRoot;
		}
	}

	public bool isAnchored
	{
		get
		{
			if (Object.op_Implicit((Object)(object)leftAnchor.target) || Object.op_Implicit((Object)(object)rightAnchor.target) || Object.op_Implicit((Object)(object)topAnchor.target) || Object.op_Implicit((Object)(object)bottomAnchor.target))
			{
				return canBeAnchored;
			}
			return false;
		}
	}

	public abstract float alpha { get; set; }

	public abstract Vector3[] localCorners { get; }

	public abstract Vector3[] worldCorners { get; }

	public abstract float CalculateFinalAlpha(int frameID);

	public virtual void Invalidate(bool includeChildren)
	{
		mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < mChildren.size; i++)
			{
				mChildren.buffer[i].Invalidate(includeChildren: true);
			}
		}
	}

	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)anchorCamera != (Object)null)
		{
			Vector3[] sides = anchorCamera.GetSides(relativeTo);
			UIRoot uIRoot = root;
			if ((Object)(object)uIRoot != (Object)null)
			{
				float pixelSizeAdjustment = uIRoot.pixelSizeAdjustment;
				for (int i = 0; i < 4; i++)
				{
					ref Vector3 reference = ref sides[i];
					reference *= pixelSizeAdjustment;
				}
			}
			return sides;
		}
		Vector3 position = cachedTransform.position;
		for (int j = 0; j < 4; j++)
		{
			mSides[j] = position;
		}
		if ((Object)(object)relativeTo != (Object)null)
		{
			for (int k = 0; k < 4; k++)
			{
				mSides[k] = relativeTo.InverseTransformPoint(mSides[k]);
			}
		}
		return mSides;
	}

	protected Vector3 GetLocalPos(AnchorPoint ac, Transform trans)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)anchorCamera == (Object)null || (Object)(object)ac.targetCam == (Object)null)
		{
			return cachedTransform.localPosition;
		}
		Vector3 val = mMyCam.ViewportToWorldPoint(ac.targetCam.WorldToViewportPoint(ac.target.position));
		if ((Object)(object)trans != (Object)null)
		{
			val = trans.InverseTransformPoint(val);
		}
		val.x = Mathf.Floor(val.x + 0.5f);
		val.y = Mathf.Floor(val.y + 0.5f);
		return val;
	}

	protected virtual void OnEnable()
	{
		mAnchorsCached = false;
		mUpdateFrame = -1;
		if (updateAnchors == AnchorUpdate.OnEnable)
		{
			mUpdateAnchors = true;
		}
		if (mStarted)
		{
			OnInit();
		}
		mUpdateFrame = -1;
	}

	protected virtual void OnInit()
	{
		mChanged = true;
		mRootSet = false;
		mParentFound = false;
		if ((Object)(object)parent != (Object)null)
		{
			mParent.mChildren.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		if (Object.op_Implicit((Object)(object)mParent))
		{
			mParent.mChildren.Remove(this);
		}
		mParent = null;
		mRoot = null;
		mRootSet = false;
		mParentFound = false;
	}

	protected void Start()
	{
		mStarted = true;
		OnInit();
		OnStart();
	}

	public void Update()
	{
		if (!mAnchorsCached)
		{
			ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (mUpdateFrame == frameCount)
		{
			return;
		}
		if (updateAnchors == AnchorUpdate.OnUpdate || mUpdateAnchors)
		{
			mUpdateFrame = frameCount;
			mUpdateAnchors = false;
			bool flag = false;
			if (Object.op_Implicit((Object)(object)leftAnchor.target))
			{
				flag = true;
				if ((Object)(object)leftAnchor.rect != (Object)null && leftAnchor.rect.mUpdateFrame != frameCount)
				{
					leftAnchor.rect.Update();
				}
			}
			if (Object.op_Implicit((Object)(object)bottomAnchor.target))
			{
				flag = true;
				if ((Object)(object)bottomAnchor.rect != (Object)null && bottomAnchor.rect.mUpdateFrame != frameCount)
				{
					bottomAnchor.rect.Update();
				}
			}
			if (Object.op_Implicit((Object)(object)rightAnchor.target))
			{
				flag = true;
				if ((Object)(object)rightAnchor.rect != (Object)null && rightAnchor.rect.mUpdateFrame != frameCount)
				{
					rightAnchor.rect.Update();
				}
			}
			if (Object.op_Implicit((Object)(object)topAnchor.target))
			{
				flag = true;
				if ((Object)(object)topAnchor.rect != (Object)null && topAnchor.rect.mUpdateFrame != frameCount)
				{
					topAnchor.rect.Update();
				}
			}
			if (flag)
			{
				OnAnchor();
			}
		}
		OnUpdate();
	}

	public void updatePostion()
	{
		int num = (mUpdateFrame = Time.frameCount);
		mUpdateAnchors = false;
		bool flag = false;
		if (Object.op_Implicit((Object)(object)leftAnchor.target))
		{
			flag = true;
			if ((Object)(object)leftAnchor.rect != (Object)null && leftAnchor.rect.mUpdateFrame != num)
			{
				leftAnchor.rect.Update();
			}
		}
		if (Object.op_Implicit((Object)(object)bottomAnchor.target))
		{
			flag = true;
			if ((Object)(object)bottomAnchor.rect != (Object)null && bottomAnchor.rect.mUpdateFrame != num)
			{
				bottomAnchor.rect.Update();
			}
		}
		if (Object.op_Implicit((Object)(object)rightAnchor.target))
		{
			flag = true;
			if ((Object)(object)rightAnchor.rect != (Object)null && rightAnchor.rect.mUpdateFrame != num)
			{
				rightAnchor.rect.Update();
			}
		}
		if (Object.op_Implicit((Object)(object)topAnchor.target))
		{
			flag = true;
			if ((Object)(object)topAnchor.rect != (Object)null && topAnchor.rect.mUpdateFrame != num)
			{
				topAnchor.rect.Update();
			}
		}
		if (flag)
		{
			OnAnchor();
		}
	}

	public void UpdateAnchors()
	{
		if (isAnchored)
		{
			OnAnchor();
		}
	}

	protected abstract void OnAnchor();

	public void SetAnchor(Transform t)
	{
		leftAnchor.target = t;
		rightAnchor.target = t;
		topAnchor.target = t;
		bottomAnchor.target = t;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject go)
	{
		Transform target = (((Object)(object)go != (Object)null) ? go.transform : null);
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (((Object)(object)go != (Object)null) ? go.transform : null);
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		leftAnchor.relative = 0f;
		rightAnchor.relative = 1f;
		bottomAnchor.relative = 0f;
		topAnchor.relative = 1f;
		leftAnchor.absolute = left;
		rightAnchor.absolute = right;
		bottomAnchor.absolute = bottom;
		topAnchor.absolute = top;
		ResetAnchors();
		UpdateAnchors();
	}

	public void ResetAnchors()
	{
		mAnchorsCached = true;
		leftAnchor.rect = (Object.op_Implicit((Object)(object)leftAnchor.target) ? ((Component)leftAnchor.target).GetComponent<UIRect>() : null);
		bottomAnchor.rect = (Object.op_Implicit((Object)(object)bottomAnchor.target) ? ((Component)bottomAnchor.target).GetComponent<UIRect>() : null);
		rightAnchor.rect = (Object.op_Implicit((Object)(object)rightAnchor.target) ? ((Component)rightAnchor.target).GetComponent<UIRect>() : null);
		topAnchor.rect = (Object.op_Implicit((Object)(object)topAnchor.target) ? ((Component)topAnchor.target).GetComponent<UIRect>() : null);
		mMyCam = NGUITools.FindCameraForLayer(cachedGameObject.layer);
		FindCameraFor(leftAnchor);
		FindCameraFor(bottomAnchor);
		FindCameraFor(rightAnchor);
		FindCameraFor(topAnchor);
		mUpdateAnchors = true;
	}

	public void ResetAndUpdateAnchors()
	{
		ResetAnchors();
		UpdateAnchors();
	}

	public abstract void SetRect(float x, float y, float width, float height);

	private void FindCameraFor(AnchorPoint ap)
	{
		if ((Object)(object)ap.target == (Object)null || (Object)(object)ap.rect != (Object)null)
		{
			ap.targetCam = null;
		}
		else
		{
			ap.targetCam = NGUITools.FindCameraForLayer(((Component)ap.target).gameObject.layer);
		}
	}

	public virtual void ParentHasChanged()
	{
		mParentFound = false;
		UIRect uIRect = NGUITools.FindInParents<UIRect>(cachedTransform.parent);
		if ((Object)(object)mParent != (Object)(object)uIRect)
		{
			if (Object.op_Implicit((Object)(object)mParent))
			{
				mParent.mChildren.Remove(this);
			}
			mParent = uIRect;
			if (Object.op_Implicit((Object)(object)mParent))
			{
				mParent.mChildren.Add(this);
			}
			mRootSet = false;
		}
	}

	protected abstract void OnStart();

	protected virtual void OnUpdate()
	{
	}
}
