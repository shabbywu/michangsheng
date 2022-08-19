using System;
using System.Collections.Generic;
using SoftMasking.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoftMasking
{
	// Token: 0x020006E1 RID: 1761
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	public class SoftMaskable : UIBehaviour, IMaterialModifier
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x00184609 File Offset: 0x00182809
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x00184611 File Offset: 0x00182811
		public bool shaderIsNotSupported { get; private set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x0018461A File Offset: 0x0018281A
		public bool isMaskingEnabled
		{
			get
			{
				return this.mask != null && this.mask.isAlive && this.mask.isMaskingEnabled && this._affectedByMask;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060038CD RID: 14541 RVA: 0x00184646 File Offset: 0x00182846
		// (set) Token: 0x060038CE RID: 14542 RVA: 0x0018464E File Offset: 0x0018284E
		public ISoftMask mask
		{
			get
			{
				return this._mask;
			}
			private set
			{
				if (this._mask != value)
				{
					if (this._mask != null)
					{
						this.replacement = null;
					}
					this._mask = ((value != null && value.isAlive) ? value : null);
					this.Invalidate();
				}
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x00184684 File Offset: 0x00182884
		public Material GetModifiedMaterial(Material baseMaterial)
		{
			if (this.isMaskingEnabled)
			{
				Material replacement = this.mask.GetReplacement(baseMaterial);
				this.replacement = replacement;
				if (this.replacement)
				{
					this.shaderIsNotSupported = false;
					return this.replacement;
				}
				if (!baseMaterial.HasDefaultUIShader())
				{
					this.SetShaderNotSupported(baseMaterial);
				}
			}
			else
			{
				this.shaderIsNotSupported = false;
				this.replacement = null;
			}
			return baseMaterial;
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x001846E8 File Offset: 0x001828E8
		public void Invalidate()
		{
			if (this.graphic)
			{
				this.graphic.SetMaterialDirty();
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x00184702 File Offset: 0x00182902
		public void MaskMightChanged()
		{
			if (this.FindMaskOrDie())
			{
				this.Invalidate();
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00184712 File Offset: 0x00182912
		protected override void Awake()
		{
			base.Awake();
			base.hideFlags = 2;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x00184721 File Offset: 0x00182921
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.FindMaskOrDie())
			{
				this.RequestChildTransformUpdate();
			}
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x00184737 File Offset: 0x00182937
		protected override void OnDisable()
		{
			base.OnDisable();
			this.mask = null;
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x00184746 File Offset: 0x00182946
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._destroyed = true;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x00184755 File Offset: 0x00182955
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.FindMaskOrDie();
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x00184764 File Offset: 0x00182964
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.FindMaskOrDie();
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x00184773 File Offset: 0x00182973
		private void OnTransformChildrenChanged()
		{
			this.RequestChildTransformUpdate();
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x0018477B File Offset: 0x0018297B
		private void RequestChildTransformUpdate()
		{
			if (this.mask != null)
			{
				this.mask.UpdateTransformChildren(base.transform);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x00184798 File Offset: 0x00182998
		private Graphic graphic
		{
			get
			{
				if (!this._graphic)
				{
					return this._graphic = base.GetComponent<Graphic>();
				}
				return this._graphic;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x001847C8 File Offset: 0x001829C8
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x001847D0 File Offset: 0x001829D0
		private Material replacement
		{
			get
			{
				return this._replacement;
			}
			set
			{
				if (this._replacement != value)
				{
					if (this._replacement != null && this.mask != null)
					{
						this.mask.ReleaseReplacement(this._replacement);
					}
					this._replacement = value;
				}
			}
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x00184810 File Offset: 0x00182A10
		private bool FindMaskOrDie()
		{
			if (this._destroyed)
			{
				return false;
			}
			this.mask = (SoftMaskable.NearestMask(base.transform, out this._affectedByMask, true) ?? SoftMaskable.NearestMask(base.transform, out this._affectedByMask, false));
			if (this.mask == null)
			{
				this._destroyed = true;
				Object.DestroyImmediate(this);
				return false;
			}
			return true;
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x00184870 File Offset: 0x00182A70
		private static ISoftMask NearestMask(Transform transform, out bool processedByThisMask, bool enabledOnly = true)
		{
			processedByThisMask = true;
			Transform transform2 = transform;
			while (transform2)
			{
				if (transform2 != transform)
				{
					ISoftMask isoftMask = SoftMaskable.GetISoftMask(transform2, enabledOnly);
					if (isoftMask != null)
					{
						return isoftMask;
					}
				}
				if (SoftMaskable.IsOverridingSortingCanvas(transform2))
				{
					processedByThisMask = false;
				}
				transform2 = transform2.parent;
			}
			return null;
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x001848B8 File Offset: 0x00182AB8
		private static ISoftMask GetISoftMask(Transform current, bool shouldBeEnabled = true)
		{
			ISoftMask component = SoftMaskable.GetComponent<ISoftMask>(current, SoftMaskable.s_softMasks);
			if (component != null && component.isAlive && (!shouldBeEnabled || component.isMaskingEnabled))
			{
				return component;
			}
			return null;
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x001848EC File Offset: 0x00182AEC
		private static bool IsOverridingSortingCanvas(Transform transform)
		{
			Canvas component = SoftMaskable.GetComponent<Canvas>(transform, SoftMaskable.s_canvases);
			return component && component.overrideSorting;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00184918 File Offset: 0x00182B18
		private static T GetComponent<T>(Component component, List<T> cachedList) where T : class
		{
			component.GetComponents<T>(cachedList);
			T t;
			using (new ClearListAtExit<T>(cachedList))
			{
				T t2;
				if (cachedList.Count <= 0)
				{
					t = default(T);
					t2 = t;
				}
				else
				{
					t2 = cachedList[0];
				}
				t = t2;
			}
			return t;
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x00184974 File Offset: 0x00182B74
		private void SetShaderNotSupported(Material material)
		{
			if (!this.shaderIsNotSupported)
			{
				Debug.LogWarningFormat(base.gameObject, "SoftMask will not work on {0} because material {1} doesn't support masking. Add masking support to your material or set Graphic's material to None to use a default one.", new object[]
				{
					this.graphic,
					material
				});
				this.shaderIsNotSupported = true;
			}
		}

		// Token: 0x040030F3 RID: 12531
		private ISoftMask _mask;

		// Token: 0x040030F4 RID: 12532
		private Graphic _graphic;

		// Token: 0x040030F5 RID: 12533
		private Material _replacement;

		// Token: 0x040030F6 RID: 12534
		private bool _affectedByMask;

		// Token: 0x040030F7 RID: 12535
		private bool _destroyed;

		// Token: 0x040030F9 RID: 12537
		private static List<ISoftMask> s_softMasks = new List<ISoftMask>();

		// Token: 0x040030FA RID: 12538
		private static List<Canvas> s_canvases = new List<Canvas>();
	}
}
