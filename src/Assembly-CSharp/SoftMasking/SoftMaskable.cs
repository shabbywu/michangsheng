using System;
using System.Collections.Generic;
using SoftMasking.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoftMasking
{
	// Token: 0x02000A20 RID: 2592
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	public class SoftMaskable : UIBehaviour, IMaterialModifier
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x0002FFEA File Offset: 0x0002E1EA
		// (set) Token: 0x06004332 RID: 17202 RVA: 0x0002FFF2 File Offset: 0x0002E1F2
		public bool shaderIsNotSupported { get; private set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x0002FFFB File Offset: 0x0002E1FB
		public bool isMaskingEnabled
		{
			get
			{
				return this.mask != null && this.mask.isAlive && this.mask.isMaskingEnabled && this._affectedByMask;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x00030027 File Offset: 0x0002E227
		// (set) Token: 0x06004335 RID: 17205 RVA: 0x0003002F File Offset: 0x0002E22F
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

		// Token: 0x06004336 RID: 17206 RVA: 0x001CC34C File Offset: 0x001CA54C
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

		// Token: 0x06004337 RID: 17207 RVA: 0x00030064 File Offset: 0x0002E264
		public void Invalidate()
		{
			if (this.graphic)
			{
				this.graphic.SetMaterialDirty();
			}
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x0003007E File Offset: 0x0002E27E
		public void MaskMightChanged()
		{
			if (this.FindMaskOrDie())
			{
				this.Invalidate();
			}
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0003008E File Offset: 0x0002E28E
		protected override void Awake()
		{
			base.Awake();
			base.hideFlags = 2;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x0003009D File Offset: 0x0002E29D
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.FindMaskOrDie())
			{
				this.RequestChildTransformUpdate();
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000300B3 File Offset: 0x0002E2B3
		protected override void OnDisable()
		{
			base.OnDisable();
			this.mask = null;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000300C2 File Offset: 0x0002E2C2
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._destroyed = true;
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000300D1 File Offset: 0x0002E2D1
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.FindMaskOrDie();
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000300E0 File Offset: 0x0002E2E0
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.FindMaskOrDie();
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000300EF File Offset: 0x0002E2EF
		private void OnTransformChildrenChanged()
		{
			this.RequestChildTransformUpdate();
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x000300F7 File Offset: 0x0002E2F7
		private void RequestChildTransformUpdate()
		{
			if (this.mask != null)
			{
				this.mask.UpdateTransformChildren(base.transform);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x001CC3B0 File Offset: 0x001CA5B0
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

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x00030112 File Offset: 0x0002E312
		// (set) Token: 0x06004343 RID: 17219 RVA: 0x0003011A File Offset: 0x0002E31A
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

		// Token: 0x06004344 RID: 17220 RVA: 0x001CC3E0 File Offset: 0x001CA5E0
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

		// Token: 0x06004345 RID: 17221 RVA: 0x001CC440 File Offset: 0x001CA640
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

		// Token: 0x06004346 RID: 17222 RVA: 0x001CC488 File Offset: 0x001CA688
		private static ISoftMask GetISoftMask(Transform current, bool shouldBeEnabled = true)
		{
			ISoftMask component = SoftMaskable.GetComponent<ISoftMask>(current, SoftMaskable.s_softMasks);
			if (component != null && component.isAlive && (!shouldBeEnabled || component.isMaskingEnabled))
			{
				return component;
			}
			return null;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x001CC4BC File Offset: 0x001CA6BC
		private static bool IsOverridingSortingCanvas(Transform transform)
		{
			Canvas component = SoftMaskable.GetComponent<Canvas>(transform, SoftMaskable.s_canvases);
			return component && component.overrideSorting;
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x001CC4E8 File Offset: 0x001CA6E8
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

		// Token: 0x06004349 RID: 17225 RVA: 0x00030158 File Offset: 0x0002E358
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

		// Token: 0x04003B52 RID: 15186
		private ISoftMask _mask;

		// Token: 0x04003B53 RID: 15187
		private Graphic _graphic;

		// Token: 0x04003B54 RID: 15188
		private Material _replacement;

		// Token: 0x04003B55 RID: 15189
		private bool _affectedByMask;

		// Token: 0x04003B56 RID: 15190
		private bool _destroyed;

		// Token: 0x04003B58 RID: 15192
		private static List<ISoftMask> s_softMasks = new List<ISoftMask>();

		// Token: 0x04003B59 RID: 15193
		private static List<Canvas> s_canvases = new List<Canvas>();
	}
}
