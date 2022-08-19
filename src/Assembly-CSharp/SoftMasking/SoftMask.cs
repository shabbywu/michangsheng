using System;
using System.Collections.Generic;
using System.Linq;
using SoftMasking.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace SoftMasking
{
	// Token: 0x020006E0 RID: 1760
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Soft Mask", 14)]
	[RequireComponent(typeof(RectTransform))]
	[HelpURL("https://docs.google.com/document/d/1xFZQGn_odhTCokMFR0LyCPXWtqWXN-bBGVS9GETglx8")]
	public class SoftMask : UIBehaviour, ISoftMask, ICanvasRaycastFilter
	{
		// Token: 0x06003878 RID: 14456 RVA: 0x00183844 File Offset: 0x00181A44
		public SoftMask()
		{
			MaterialReplacerChain replacer = new MaterialReplacerChain(MaterialReplacer.globalReplacers, new SoftMask.MaterialReplacerImpl(this));
			this._materials = new MaterialReplacements(replacer, delegate(Material m)
			{
				this._parameters.Apply(m);
			});
			this._warningReporter = new SoftMask.WarningReporter(this);
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x001838AD File Offset: 0x00181AAD
		// (set) Token: 0x0600387A RID: 14458 RVA: 0x001838B5 File Offset: 0x00181AB5
		public Shader defaultShader
		{
			get
			{
				return this._defaultShader;
			}
			set
			{
				this.SetShader(ref this._defaultShader, value, true);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x001838C5 File Offset: 0x00181AC5
		// (set) Token: 0x0600387C RID: 14460 RVA: 0x001838CD File Offset: 0x00181ACD
		public Shader defaultETC1Shader
		{
			get
			{
				return this._defaultETC1Shader;
			}
			set
			{
				this.SetShader(ref this._defaultETC1Shader, value, false);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x001838DD File Offset: 0x00181ADD
		// (set) Token: 0x0600387E RID: 14462 RVA: 0x001838E5 File Offset: 0x00181AE5
		public SoftMask.MaskSource source
		{
			get
			{
				return this._source;
			}
			set
			{
				if (this._source != value)
				{
					this.Set<SoftMask.MaskSource>(ref this._source, value);
				}
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x001838FD File Offset: 0x00181AFD
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x00183905 File Offset: 0x00181B05
		public RectTransform separateMask
		{
			get
			{
				return this._separateMask;
			}
			set
			{
				if (this._separateMask != value)
				{
					this.Set<RectTransform>(ref this._separateMask, value);
					this._graphic = null;
					this._maskTransform = null;
				}
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x00183930 File Offset: 0x00181B30
		// (set) Token: 0x06003882 RID: 14466 RVA: 0x00183938 File Offset: 0x00181B38
		public Sprite sprite
		{
			get
			{
				return this._sprite;
			}
			set
			{
				if (this._sprite != value)
				{
					this.Set<Sprite>(ref this._sprite, value);
				}
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x00183955 File Offset: 0x00181B55
		// (set) Token: 0x06003884 RID: 14468 RVA: 0x0018395D File Offset: 0x00181B5D
		public SoftMask.BorderMode spriteBorderMode
		{
			get
			{
				return this._spriteBorderMode;
			}
			set
			{
				if (this._spriteBorderMode != value)
				{
					this.Set<SoftMask.BorderMode>(ref this._spriteBorderMode, value);
				}
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x00183975 File Offset: 0x00181B75
		// (set) Token: 0x06003886 RID: 14470 RVA: 0x0018397D File Offset: 0x00181B7D
		public float spritePixelsPerUnitMultiplier
		{
			get
			{
				return this._spritePixelsPerUnitMultiplier;
			}
			set
			{
				if (this._spritePixelsPerUnitMultiplier != value)
				{
					this.Set<float>(ref this._spritePixelsPerUnitMultiplier, SoftMask.ClampPixelsPerUnitMultiplier(value));
				}
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x0018399A File Offset: 0x00181B9A
		// (set) Token: 0x06003888 RID: 14472 RVA: 0x001839A7 File Offset: 0x00181BA7
		public Texture2D texture
		{
			get
			{
				return this._texture as Texture2D;
			}
			set
			{
				if (this._texture != value)
				{
					this.Set<Texture>(ref this._texture, value);
				}
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x001839C4 File Offset: 0x00181BC4
		// (set) Token: 0x0600388A RID: 14474 RVA: 0x001839A7 File Offset: 0x00181BA7
		public RenderTexture renderTexture
		{
			get
			{
				return this._texture as RenderTexture;
			}
			set
			{
				if (this._texture != value)
				{
					this.Set<Texture>(ref this._texture, value);
				}
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x001839D1 File Offset: 0x00181BD1
		// (set) Token: 0x0600388C RID: 14476 RVA: 0x001839D9 File Offset: 0x00181BD9
		public Rect textureUVRect
		{
			get
			{
				return this._textureUVRect;
			}
			set
			{
				if (this._textureUVRect != value)
				{
					this.Set<Rect>(ref this._textureUVRect, value);
				}
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x001839F6 File Offset: 0x00181BF6
		// (set) Token: 0x0600388E RID: 14478 RVA: 0x001839FE File Offset: 0x00181BFE
		public Color channelWeights
		{
			get
			{
				return this._channelWeights;
			}
			set
			{
				if (this._channelWeights != value)
				{
					this.Set<Color>(ref this._channelWeights, value);
				}
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x00183A1B File Offset: 0x00181C1B
		// (set) Token: 0x06003890 RID: 14480 RVA: 0x00183A23 File Offset: 0x00181C23
		public float raycastThreshold
		{
			get
			{
				return this._raycastThreshold;
			}
			set
			{
				this._raycastThreshold = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x00183A2C File Offset: 0x00181C2C
		// (set) Token: 0x06003892 RID: 14482 RVA: 0x00183A34 File Offset: 0x00181C34
		public bool invertMask
		{
			get
			{
				return this._invertMask;
			}
			set
			{
				if (this._invertMask != value)
				{
					this.Set<bool>(ref this._invertMask, value);
				}
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x00183A4C File Offset: 0x00181C4C
		// (set) Token: 0x06003894 RID: 14484 RVA: 0x00183A54 File Offset: 0x00181C54
		public bool invertOutsides
		{
			get
			{
				return this._invertOutsides;
			}
			set
			{
				if (this._invertOutsides != value)
				{
					this.Set<bool>(ref this._invertOutsides, value);
				}
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x00183A6C File Offset: 0x00181C6C
		public bool isUsingRaycastFiltering
		{
			get
			{
				return this._raycastThreshold > 0f;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x00183A7B File Offset: 0x00181C7B
		public bool isMaskingEnabled
		{
			get
			{
				return base.isActiveAndEnabled && this.canvas;
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x00183A94 File Offset: 0x00181C94
		public SoftMask.Errors PollErrors()
		{
			return new SoftMask.Diagnostics(this).PollErrors();
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x00183AB0 File Offset: 0x00181CB0
		public bool IsRaycastLocationValid(Vector2 sp, Camera cam)
		{
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.maskTransform, sp, cam, ref vector))
			{
				return false;
			}
			if (!SoftMask.Mathr.Inside(vector, this.LocalMaskRect(Vector4.zero)))
			{
				return this._invertOutsides;
			}
			if (!this._parameters.texture)
			{
				return true;
			}
			if (!this.isUsingRaycastFiltering)
			{
				return true;
			}
			float num;
			SoftMask.MaterialParameters.SampleMaskResult sampleMaskResult = this._parameters.SampleMask(vector, out num);
			this._warningReporter.TextureRead(this._parameters.texture, sampleMaskResult);
			if (sampleMaskResult != SoftMask.MaterialParameters.SampleMaskResult.Success)
			{
				return true;
			}
			if (this._invertMask)
			{
				num = 1f - num;
			}
			return num >= this._raycastThreshold;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00183B4F File Offset: 0x00181D4F
		protected override void Start()
		{
			base.Start();
			this.WarnIfDefaultShaderIsNotSet();
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x00183B5D File Offset: 0x00181D5D
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SubscribeOnWillRenderCanvases();
			this.SpawnMaskablesInChildren(base.transform);
			this.FindGraphic();
			if (this.isMaskingEnabled)
			{
				this.UpdateMaskParameters();
			}
			this.NotifyChildrenThatMaskMightChanged();
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x00183B94 File Offset: 0x00181D94
		protected override void OnDisable()
		{
			base.OnDisable();
			this.UnsubscribeFromWillRenderCanvases();
			if (this._graphic)
			{
				this._graphic.UnregisterDirtyVerticesCallback(new UnityAction(this.OnGraphicDirty));
				this._graphic.UnregisterDirtyMaterialCallback(new UnityAction(this.OnGraphicDirty));
				this._graphic = null;
			}
			this.NotifyChildrenThatMaskMightChanged();
			this.DestroyMaterials();
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x00183BFB File Offset: 0x00181DFB
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._destroyed = true;
			this.NotifyChildrenThatMaskMightChanged();
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x00183C10 File Offset: 0x00181E10
		protected virtual void LateUpdate()
		{
			bool isMaskingEnabled = this.isMaskingEnabled;
			if (isMaskingEnabled)
			{
				if (this._maskingWasEnabled != isMaskingEnabled)
				{
					this.SpawnMaskablesInChildren(base.transform);
				}
				Graphic graphic = this._graphic;
				this.FindGraphic();
				if (this._lastMaskRect != this.maskTransform.rect || this._graphic != graphic)
				{
					this._dirty = true;
				}
			}
			this._maskingWasEnabled = isMaskingEnabled;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x00183C78 File Offset: 0x00181E78
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this._dirty = true;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x00183C87 File Offset: 0x00181E87
		protected override void OnDidApplyAnimationProperties()
		{
			base.OnDidApplyAnimationProperties();
			this._dirty = true;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x00183C96 File Offset: 0x00181E96
		private static float ClampPixelsPerUnitMultiplier(float value)
		{
			return Mathf.Max(value, 0.01f);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x00183CA3 File Offset: 0x00181EA3
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this._canvas = null;
			this._dirty = true;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x00183CB9 File Offset: 0x00181EB9
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this._canvas = null;
			this._dirty = true;
			this.NotifyChildrenThatMaskMightChanged();
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x00183CD5 File Offset: 0x00181ED5
		private void OnTransformChildrenChanged()
		{
			this.SpawnMaskablesInChildren(base.transform);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x00183CE3 File Offset: 0x00181EE3
		private void SubscribeOnWillRenderCanvases()
		{
			SoftMask.Touch<CanvasUpdateRegistry>(CanvasUpdateRegistry.instance);
			Canvas.willRenderCanvases += new Canvas.WillRenderCanvases(this.OnWillRenderCanvases);
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x00183D01 File Offset: 0x00181F01
		private void UnsubscribeFromWillRenderCanvases()
		{
			Canvas.willRenderCanvases -= new Canvas.WillRenderCanvases(this.OnWillRenderCanvases);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x00183D14 File Offset: 0x00181F14
		private void OnWillRenderCanvases()
		{
			if (this.isMaskingEnabled)
			{
				this.UpdateMaskParameters();
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x00183D24 File Offset: 0x00181F24
		private static T Touch<T>(T obj)
		{
			return obj;
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x00183D28 File Offset: 0x00181F28
		private RectTransform maskTransform
		{
			get
			{
				if (!this._maskTransform)
				{
					return this._maskTransform = (this._separateMask ? this._separateMask : base.GetComponent<RectTransform>());
				}
				return this._maskTransform;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x00183D70 File Offset: 0x00181F70
		private Canvas canvas
		{
			get
			{
				if (!this._canvas)
				{
					return this._canvas = this.NearestEnabledCanvas();
				}
				return this._canvas;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x00183DA0 File Offset: 0x00181FA0
		private bool isBasedOnGraphic
		{
			get
			{
				return this._source == SoftMask.MaskSource.Graphic;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x00183DAB File Offset: 0x00181FAB
		bool ISoftMask.isAlive
		{
			get
			{
				return this && !this._destroyed;
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x00183DC0 File Offset: 0x00181FC0
		Material ISoftMask.GetReplacement(Material original)
		{
			return this._materials.Get(original);
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x00183DCE File Offset: 0x00181FCE
		void ISoftMask.ReleaseReplacement(Material replacement)
		{
			this._materials.Release(replacement);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x00183DDC File Offset: 0x00181FDC
		void ISoftMask.UpdateTransformChildren(Transform transform)
		{
			this.SpawnMaskablesInChildren(transform);
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x00183DE5 File Offset: 0x00181FE5
		private void OnGraphicDirty()
		{
			if (this.isBasedOnGraphic)
			{
				this._dirty = true;
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x00183DF8 File Offset: 0x00181FF8
		private void FindGraphic()
		{
			if (!this._graphic && this.isBasedOnGraphic)
			{
				this._graphic = this.maskTransform.GetComponent<Graphic>();
				if (this._graphic)
				{
					this._graphic.RegisterDirtyVerticesCallback(new UnityAction(this.OnGraphicDirty));
					this._graphic.RegisterDirtyMaterialCallback(new UnityAction(this.OnGraphicDirty));
				}
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x00183E68 File Offset: 0x00182068
		private Canvas NearestEnabledCanvas()
		{
			Canvas[] componentsInParent = base.GetComponentsInParent<Canvas>(false);
			for (int i = 0; i < componentsInParent.Length; i++)
			{
				if (componentsInParent[i].isActiveAndEnabled)
				{
					return componentsInParent[i];
				}
			}
			return null;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x00183E9C File Offset: 0x0018209C
		private void UpdateMaskParameters()
		{
			if (this._dirty || this.maskTransform.hasChanged)
			{
				this.CalculateMaskParameters();
				this.maskTransform.hasChanged = false;
				this._lastMaskRect = this.maskTransform.rect;
				this._dirty = false;
			}
			this._materials.ApplyAll();
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00183EF4 File Offset: 0x001820F4
		private void SpawnMaskablesInChildren(Transform root)
		{
			using (new ClearListAtExit<SoftMaskable>(SoftMask.s_maskables))
			{
				for (int i = 0; i < root.childCount; i++)
				{
					Transform child = root.GetChild(i);
					child.GetComponents<SoftMaskable>(SoftMask.s_maskables);
					if (SoftMask.s_maskables.Count == 0)
					{
						child.gameObject.AddComponent<SoftMaskable>();
					}
				}
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x00183F6C File Offset: 0x0018216C
		private void InvalidateChildren()
		{
			this.ForEachChildMaskable(delegate(SoftMaskable x)
			{
				x.Invalidate();
			});
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x00183F93 File Offset: 0x00182193
		private void NotifyChildrenThatMaskMightChanged()
		{
			this.ForEachChildMaskable(delegate(SoftMaskable x)
			{
				x.MaskMightChanged();
			});
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x00183FBC File Offset: 0x001821BC
		private void ForEachChildMaskable(Action<SoftMaskable> f)
		{
			base.transform.GetComponentsInChildren<SoftMaskable>(SoftMask.s_maskables);
			using (new ClearListAtExit<SoftMaskable>(SoftMask.s_maskables))
			{
				for (int i = 0; i < SoftMask.s_maskables.Count; i++)
				{
					SoftMaskable softMaskable = SoftMask.s_maskables[i];
					if (softMaskable && softMaskable.gameObject != base.gameObject)
					{
						f(softMaskable);
					}
				}
			}
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x00184048 File Offset: 0x00182248
		private void DestroyMaterials()
		{
			this._materials.DestroyAllAndClear();
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00184058 File Offset: 0x00182258
		private SoftMask.SourceParameters DeduceSourceParameters()
		{
			SoftMask.SourceParameters result = default(SoftMask.SourceParameters);
			switch (this._source)
			{
			case SoftMask.MaskSource.Graphic:
				if (this._graphic is Image)
				{
					Image image = (Image)this._graphic;
					Sprite sprite = image.sprite;
					result.image = image;
					result.sprite = sprite;
					result.spriteBorderMode = SoftMask.ImageTypeToBorderMode(image.type);
					if (sprite)
					{
						result.spritePixelsPerUnit = sprite.pixelsPerUnit;
						result.texture = sprite.texture;
					}
					else
					{
						result.spritePixelsPerUnit = 100f;
					}
				}
				else if (this._graphic is RawImage)
				{
					RawImage rawImage = (RawImage)this._graphic;
					result.texture = rawImage.texture;
					result.textureUVRect = rawImage.uvRect;
				}
				break;
			case SoftMask.MaskSource.Sprite:
				result.sprite = this._sprite;
				result.spriteBorderMode = this._spriteBorderMode;
				if (this._sprite)
				{
					result.spritePixelsPerUnit = this._sprite.pixelsPerUnit * this._spritePixelsPerUnitMultiplier;
					result.texture = this._sprite.texture;
				}
				else
				{
					result.spritePixelsPerUnit = 100f;
				}
				break;
			case SoftMask.MaskSource.Texture:
				result.texture = this._texture;
				result.textureUVRect = this._textureUVRect;
				break;
			}
			return result;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x001841BD File Offset: 0x001823BD
		public static SoftMask.BorderMode ImageTypeToBorderMode(Image.Type type)
		{
			switch (type)
			{
			case 0:
				return SoftMask.BorderMode.Simple;
			case 1:
				return SoftMask.BorderMode.Sliced;
			case 2:
				return SoftMask.BorderMode.Tiled;
			default:
				return SoftMask.BorderMode.Simple;
			}
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x001841DA File Offset: 0x001823DA
		public static bool IsImageTypeSupported(Image.Type type)
		{
			return type == null || type == 1 || type == 2;
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x001841EC File Offset: 0x001823EC
		private void CalculateMaskParameters()
		{
			SoftMask.SourceParameters sourceParameters = this.DeduceSourceParameters();
			this._warningReporter.ImageUsed(sourceParameters.image);
			SoftMask.Errors errors = SoftMask.Diagnostics.CheckSprite(sourceParameters.sprite);
			this._warningReporter.SpriteUsed(sourceParameters.sprite, errors);
			if (sourceParameters.sprite)
			{
				if (errors == SoftMask.Errors.NoError)
				{
					this.CalculateSpriteBased(sourceParameters.sprite, sourceParameters.spriteBorderMode, sourceParameters.spritePixelsPerUnit);
					return;
				}
				this.CalculateSolidFill();
				return;
			}
			else
			{
				if (sourceParameters.texture)
				{
					this.CalculateTextureBased(sourceParameters.texture, sourceParameters.textureUVRect);
					return;
				}
				this.CalculateSolidFill();
				return;
			}
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x00184288 File Offset: 0x00182488
		private void CalculateSpriteBased(Sprite sprite, SoftMask.BorderMode borderMode, float spritePixelsPerUnit)
		{
			this.FillCommonParameters();
			Vector4 innerUV = DataUtility.GetInnerUV(sprite);
			Vector4 outerUV = DataUtility.GetOuterUV(sprite);
			Vector4 padding = DataUtility.GetPadding(sprite);
			Vector4 vector = this.LocalMaskRect(Vector4.zero);
			this._parameters.maskRectUV = outerUV;
			if (borderMode == SoftMask.BorderMode.Simple)
			{
				Vector4 v = SoftMask.Mathr.Div(padding, sprite.rect.size);
				this._parameters.maskRect = SoftMask.Mathr.ApplyBorder(vector, SoftMask.Mathr.Mul(v, SoftMask.Mathr.Size(vector)));
			}
			else
			{
				float num = this.SpriteToCanvasScale(spritePixelsPerUnit);
				this._parameters.maskRect = SoftMask.Mathr.ApplyBorder(vector, padding * num);
				Vector4 border = SoftMask.AdjustBorders(sprite.border * num, vector);
				this._parameters.maskBorder = this.LocalMaskRect(border);
				this._parameters.maskBorderUV = innerUV;
			}
			this._parameters.texture = sprite.texture;
			this._parameters.borderMode = borderMode;
			if (borderMode == SoftMask.BorderMode.Tiled)
			{
				this._parameters.tileRepeat = this.MaskRepeat(sprite, spritePixelsPerUnit, this._parameters.maskBorder);
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x00184398 File Offset: 0x00182598
		private static Vector4 AdjustBorders(Vector4 border, Vector4 rect)
		{
			Vector2 vector = SoftMask.Mathr.Size(rect);
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (vector[i] < num && num != 0f)
				{
					float num2 = vector[i] / num;
					ref Vector4 ptr = ref border;
					int num3 = i;
					ptr[num3] *= num2;
					ptr = ref border;
					num3 = i + 2;
					ptr[num3] *= num2;
				}
			}
			return border;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x00184424 File Offset: 0x00182624
		private void CalculateTextureBased(Texture texture, Rect uvRect)
		{
			this.FillCommonParameters();
			this._parameters.maskRect = this.LocalMaskRect(Vector4.zero);
			this._parameters.maskRectUV = SoftMask.Mathr.ToVector(uvRect);
			this._parameters.texture = texture;
			this._parameters.borderMode = SoftMask.BorderMode.Simple;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00184476 File Offset: 0x00182676
		private void CalculateSolidFill()
		{
			this.CalculateTextureBased(null, SoftMask.DefaultUVRect);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x00184484 File Offset: 0x00182684
		private void FillCommonParameters()
		{
			this._parameters.worldToMask = this.WorldToMask();
			this._parameters.maskChannelWeights = this._channelWeights;
			this._parameters.invertMask = this._invertMask;
			this._parameters.invertOutsides = this._invertOutsides;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x001844D5 File Offset: 0x001826D5
		private float SpriteToCanvasScale(float spritePixelsPerUnit)
		{
			return (this.canvas ? this.canvas.referencePixelsPerUnit : 100f) / spritePixelsPerUnit;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x001844F8 File Offset: 0x001826F8
		private Matrix4x4 WorldToMask()
		{
			return this.maskTransform.worldToLocalMatrix * this.canvas.rootCanvas.transform.localToWorldMatrix;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x0018451F File Offset: 0x0018271F
		private Vector4 LocalMaskRect(Vector4 border)
		{
			return SoftMask.Mathr.ApplyBorder(SoftMask.Mathr.ToVector(this.maskTransform.rect), border);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x00184538 File Offset: 0x00182738
		private Vector2 MaskRepeat(Sprite sprite, float spritePixelsPerUnit, Vector4 centralPart)
		{
			Vector4 r = SoftMask.Mathr.ApplyBorder(SoftMask.Mathr.ToVector(sprite.rect), sprite.border);
			return SoftMask.Mathr.Div(SoftMask.Mathr.Size(centralPart) * this.SpriteToCanvasScale(spritePixelsPerUnit), SoftMask.Mathr.Size(r));
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x00184579 File Offset: 0x00182779
		private void WarnIfDefaultShaderIsNotSet()
		{
			if (!this._defaultShader)
			{
				Debug.LogWarning("SoftMask may not work because its defaultShader is not set", this);
			}
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x00184593 File Offset: 0x00182793
		private void Set<T>(ref T field, T value)
		{
			field = value;
			this._dirty = true;
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x001845A3 File Offset: 0x001827A3
		private void SetShader(ref Shader field, Shader value, bool warnIfNotSet = true)
		{
			if (field != value)
			{
				field = value;
				if (warnIfNotSet)
				{
					this.WarnIfDefaultShaderIsNotSet();
				}
				this.DestroyMaterials();
				this.InvalidateChildren();
			}
		}

		// Token: 0x040030D8 RID: 12504
		[SerializeField]
		private Shader _defaultShader;

		// Token: 0x040030D9 RID: 12505
		[SerializeField]
		private Shader _defaultETC1Shader;

		// Token: 0x040030DA RID: 12506
		[SerializeField]
		private SoftMask.MaskSource _source;

		// Token: 0x040030DB RID: 12507
		[SerializeField]
		private RectTransform _separateMask;

		// Token: 0x040030DC RID: 12508
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x040030DD RID: 12509
		[SerializeField]
		private SoftMask.BorderMode _spriteBorderMode;

		// Token: 0x040030DE RID: 12510
		[SerializeField]
		private float _spritePixelsPerUnitMultiplier = 1f;

		// Token: 0x040030DF RID: 12511
		[SerializeField]
		private Texture _texture;

		// Token: 0x040030E0 RID: 12512
		[SerializeField]
		private Rect _textureUVRect = SoftMask.DefaultUVRect;

		// Token: 0x040030E1 RID: 12513
		[SerializeField]
		private Color _channelWeights = MaskChannel.alpha;

		// Token: 0x040030E2 RID: 12514
		[SerializeField]
		private float _raycastThreshold;

		// Token: 0x040030E3 RID: 12515
		[SerializeField]
		private bool _invertMask;

		// Token: 0x040030E4 RID: 12516
		[SerializeField]
		private bool _invertOutsides;

		// Token: 0x040030E5 RID: 12517
		private MaterialReplacements _materials;

		// Token: 0x040030E6 RID: 12518
		private SoftMask.MaterialParameters _parameters;

		// Token: 0x040030E7 RID: 12519
		private SoftMask.WarningReporter _warningReporter;

		// Token: 0x040030E8 RID: 12520
		private Rect _lastMaskRect;

		// Token: 0x040030E9 RID: 12521
		private bool _maskingWasEnabled;

		// Token: 0x040030EA RID: 12522
		private bool _destroyed;

		// Token: 0x040030EB RID: 12523
		private bool _dirty;

		// Token: 0x040030EC RID: 12524
		private RectTransform _maskTransform;

		// Token: 0x040030ED RID: 12525
		private Graphic _graphic;

		// Token: 0x040030EE RID: 12526
		private Canvas _canvas;

		// Token: 0x040030EF RID: 12527
		private static readonly Rect DefaultUVRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040030F0 RID: 12528
		private const float DefaultPixelsPerUnit = 100f;

		// Token: 0x040030F1 RID: 12529
		private static readonly List<SoftMask> s_masks = new List<SoftMask>();

		// Token: 0x040030F2 RID: 12530
		private static readonly List<SoftMaskable> s_maskables = new List<SoftMaskable>();

		// Token: 0x02001517 RID: 5399
		[Serializable]
		public enum MaskSource
		{
			// Token: 0x04006E67 RID: 28263
			Graphic,
			// Token: 0x04006E68 RID: 28264
			Sprite,
			// Token: 0x04006E69 RID: 28265
			Texture
		}

		// Token: 0x02001518 RID: 5400
		[Serializable]
		public enum BorderMode
		{
			// Token: 0x04006E6B RID: 28267
			Simple,
			// Token: 0x04006E6C RID: 28268
			Sliced,
			// Token: 0x04006E6D RID: 28269
			Tiled
		}

		// Token: 0x02001519 RID: 5401
		[Flags]
		[Serializable]
		public enum Errors
		{
			// Token: 0x04006E6F RID: 28271
			NoError = 0,
			// Token: 0x04006E70 RID: 28272
			UnsupportedShaders = 1,
			// Token: 0x04006E71 RID: 28273
			NestedMasks = 2,
			// Token: 0x04006E72 RID: 28274
			TightPackedSprite = 4,
			// Token: 0x04006E73 RID: 28275
			AlphaSplitSprite = 8,
			// Token: 0x04006E74 RID: 28276
			UnsupportedImageType = 16,
			// Token: 0x04006E75 RID: 28277
			UnreadableTexture = 32,
			// Token: 0x04006E76 RID: 28278
			UnreadableRenderTexture = 64
		}

		// Token: 0x0200151A RID: 5402
		private struct SourceParameters
		{
			// Token: 0x04006E77 RID: 28279
			public Image image;

			// Token: 0x04006E78 RID: 28280
			public Sprite sprite;

			// Token: 0x04006E79 RID: 28281
			public SoftMask.BorderMode spriteBorderMode;

			// Token: 0x04006E7A RID: 28282
			public float spritePixelsPerUnit;

			// Token: 0x04006E7B RID: 28283
			public Texture texture;

			// Token: 0x04006E7C RID: 28284
			public Rect textureUVRect;
		}

		// Token: 0x0200151B RID: 5403
		private class MaterialReplacerImpl : IMaterialReplacer
		{
			// Token: 0x06008307 RID: 33543 RVA: 0x002DD56D File Offset: 0x002DB76D
			public MaterialReplacerImpl(SoftMask owner)
			{
				this._owner = owner;
			}

			// Token: 0x17000B34 RID: 2868
			// (get) Token: 0x06008308 RID: 33544 RVA: 0x0000280F File Offset: 0x00000A0F
			public int order
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06008309 RID: 33545 RVA: 0x002DD57C File Offset: 0x002DB77C
			public Material Replace(Material original)
			{
				if (original == null || original.HasDefaultUIShader())
				{
					return SoftMask.MaterialReplacerImpl.Replace(original, this._owner._defaultShader);
				}
				if (original.HasDefaultETC1UIShader())
				{
					return SoftMask.MaterialReplacerImpl.Replace(original, this._owner._defaultETC1Shader);
				}
				if (original.SupportsSoftMask())
				{
					return new Material(original);
				}
				return null;
			}

			// Token: 0x0600830A RID: 33546 RVA: 0x002DD5D8 File Offset: 0x002DB7D8
			private static Material Replace(Material original, Shader defaultReplacementShader)
			{
				Material material = defaultReplacementShader ? new Material(defaultReplacementShader) : null;
				if (material && original)
				{
					material.CopyPropertiesFromMaterial(original);
				}
				return material;
			}

			// Token: 0x04006E7D RID: 28285
			private readonly SoftMask _owner;
		}

		// Token: 0x0200151C RID: 5404
		private static class Mathr
		{
			// Token: 0x0600830B RID: 33547 RVA: 0x002DD60F File Offset: 0x002DB80F
			public static Vector4 ToVector(Rect r)
			{
				return new Vector4(r.xMin, r.yMin, r.xMax, r.yMax);
			}

			// Token: 0x0600830C RID: 33548 RVA: 0x002DD632 File Offset: 0x002DB832
			public static Vector4 Div(Vector4 v, Vector2 s)
			{
				return new Vector4(v.x / s.x, v.y / s.y, v.z / s.x, v.w / s.y);
			}

			// Token: 0x0600830D RID: 33549 RVA: 0x002DD66D File Offset: 0x002DB86D
			public static Vector2 Div(Vector2 v, Vector2 s)
			{
				return new Vector2(v.x / s.x, v.y / s.y);
			}

			// Token: 0x0600830E RID: 33550 RVA: 0x002DD68E File Offset: 0x002DB88E
			public static Vector4 Mul(Vector4 v, Vector2 s)
			{
				return new Vector4(v.x * s.x, v.y * s.y, v.z * s.x, v.w * s.y);
			}

			// Token: 0x0600830F RID: 33551 RVA: 0x002DD6C9 File Offset: 0x002DB8C9
			public static Vector2 Size(Vector4 r)
			{
				return new Vector2(r.z - r.x, r.w - r.y);
			}

			// Token: 0x06008310 RID: 33552 RVA: 0x002DD6EA File Offset: 0x002DB8EA
			public static Vector4 Move(Vector4 v, Vector2 o)
			{
				return new Vector4(v.x + o.x, v.y + o.y, v.z + o.x, v.w + o.y);
			}

			// Token: 0x06008311 RID: 33553 RVA: 0x002DD725 File Offset: 0x002DB925
			public static Vector4 BorderOf(Vector4 outer, Vector4 inner)
			{
				return new Vector4(inner.x - outer.x, inner.y - outer.y, outer.z - inner.z, outer.w - inner.w);
			}

			// Token: 0x06008312 RID: 33554 RVA: 0x002DD760 File Offset: 0x002DB960
			public static Vector4 ApplyBorder(Vector4 v, Vector4 b)
			{
				return new Vector4(v.x + b.x, v.y + b.y, v.z - b.z, v.w - b.w);
			}

			// Token: 0x06008313 RID: 33555 RVA: 0x002DD79B File Offset: 0x002DB99B
			public static Vector2 Min(Vector4 r)
			{
				return new Vector2(r.x, r.y);
			}

			// Token: 0x06008314 RID: 33556 RVA: 0x002DD7AE File Offset: 0x002DB9AE
			public static Vector2 Max(Vector4 r)
			{
				return new Vector2(r.z, r.w);
			}

			// Token: 0x06008315 RID: 33557 RVA: 0x002DD7C4 File Offset: 0x002DB9C4
			public static Vector2 Remap(Vector2 c, Vector4 from, Vector4 to)
			{
				Vector2 s = SoftMask.Mathr.Max(from) - SoftMask.Mathr.Min(from);
				Vector2 vector = SoftMask.Mathr.Max(to) - SoftMask.Mathr.Min(to);
				return Vector2.Scale(SoftMask.Mathr.Div(c - SoftMask.Mathr.Min(from), s), vector) + SoftMask.Mathr.Min(to);
			}

			// Token: 0x06008316 RID: 33558 RVA: 0x002DD818 File Offset: 0x002DBA18
			public static bool Inside(Vector2 v, Vector4 r)
			{
				return v.x >= r.x && v.y >= r.y && v.x <= r.z && v.y <= r.w;
			}
		}

		// Token: 0x0200151D RID: 5405
		private struct MaterialParameters
		{
			// Token: 0x17000B35 RID: 2869
			// (get) Token: 0x06008317 RID: 33559 RVA: 0x002DD857 File Offset: 0x002DBA57
			public Texture activeTexture
			{
				get
				{
					if (!this.texture)
					{
						return Texture2D.whiteTexture;
					}
					return this.texture;
				}
			}

			// Token: 0x06008318 RID: 33560 RVA: 0x002DD874 File Offset: 0x002DBA74
			public SoftMask.MaterialParameters.SampleMaskResult SampleMask(Vector2 localPos, out float mask)
			{
				mask = 0f;
				Texture2D texture2D = this.texture as Texture2D;
				if (!texture2D)
				{
					return SoftMask.MaterialParameters.SampleMaskResult.NonTexture2D;
				}
				Vector2 vector = this.XY2UV(localPos);
				SoftMask.MaterialParameters.SampleMaskResult result;
				try
				{
					mask = this.MaskValue(texture2D.GetPixelBilinear(vector.x, vector.y));
					result = SoftMask.MaterialParameters.SampleMaskResult.Success;
				}
				catch (UnityException)
				{
					result = SoftMask.MaterialParameters.SampleMaskResult.NonReadable;
				}
				return result;
			}

			// Token: 0x06008319 RID: 33561 RVA: 0x002DD8DC File Offset: 0x002DBADC
			public void Apply(Material mat)
			{
				mat.SetTexture(SoftMask.MaterialParameters.Ids.SoftMask, this.activeTexture);
				mat.SetVector(SoftMask.MaterialParameters.Ids.SoftMask_Rect, this.maskRect);
				mat.SetVector(SoftMask.MaterialParameters.Ids.SoftMask_UVRect, this.maskRectUV);
				mat.SetColor(SoftMask.MaterialParameters.Ids.SoftMask_ChannelWeights, this.maskChannelWeights);
				mat.SetMatrix(SoftMask.MaterialParameters.Ids.SoftMask_WorldToMask, this.worldToMask);
				mat.SetFloat(SoftMask.MaterialParameters.Ids.SoftMask_InvertMask, (float)(this.invertMask ? 1 : 0));
				mat.SetFloat(SoftMask.MaterialParameters.Ids.SoftMask_InvertOutsides, (float)(this.invertOutsides ? 1 : 0));
				mat.EnableKeyword("SOFTMASK_SIMPLE", this.borderMode == SoftMask.BorderMode.Simple);
				mat.EnableKeyword("SOFTMASK_SLICED", this.borderMode == SoftMask.BorderMode.Sliced);
				mat.EnableKeyword("SOFTMASK_TILED", this.borderMode == SoftMask.BorderMode.Tiled);
				if (this.borderMode != SoftMask.BorderMode.Simple)
				{
					mat.SetVector(SoftMask.MaterialParameters.Ids.SoftMask_BorderRect, this.maskBorder);
					mat.SetVector(SoftMask.MaterialParameters.Ids.SoftMask_UVBorderRect, this.maskBorderUV);
					if (this.borderMode == SoftMask.BorderMode.Tiled)
					{
						mat.SetVector(SoftMask.MaterialParameters.Ids.SoftMask_TileRepeat, this.tileRepeat);
					}
				}
			}

			// Token: 0x0600831A RID: 33562 RVA: 0x002DD9F4 File Offset: 0x002DBBF4
			private Vector2 XY2UV(Vector2 localPos)
			{
				switch (this.borderMode)
				{
				case SoftMask.BorderMode.Simple:
					return this.MapSimple(localPos);
				case SoftMask.BorderMode.Sliced:
					return this.MapBorder(localPos, false);
				case SoftMask.BorderMode.Tiled:
					return this.MapBorder(localPos, true);
				default:
					return this.MapSimple(localPos);
				}
			}

			// Token: 0x0600831B RID: 33563 RVA: 0x002DDA3D File Offset: 0x002DBC3D
			private Vector2 MapSimple(Vector2 localPos)
			{
				return SoftMask.Mathr.Remap(localPos, this.maskRect, this.maskRectUV);
			}

			// Token: 0x0600831C RID: 33564 RVA: 0x002DDA54 File Offset: 0x002DBC54
			private Vector2 MapBorder(Vector2 localPos, bool repeat)
			{
				return new Vector2(this.Inset(localPos.x, this.maskRect.x, this.maskBorder.x, this.maskBorder.z, this.maskRect.z, this.maskRectUV.x, this.maskBorderUV.x, this.maskBorderUV.z, this.maskRectUV.z, repeat ? this.tileRepeat.x : 1f), this.Inset(localPos.y, this.maskRect.y, this.maskBorder.y, this.maskBorder.w, this.maskRect.w, this.maskRectUV.y, this.maskBorderUV.y, this.maskBorderUV.w, this.maskRectUV.w, repeat ? this.tileRepeat.y : 1f));
			}

			// Token: 0x0600831D RID: 33565 RVA: 0x002DDB58 File Offset: 0x002DBD58
			private float Inset(float v, float x1, float x2, float u1, float u2, float repeat = 1f)
			{
				float num = x2 - x1;
				return Mathf.Lerp(u1, u2, (num != 0f) ? this.Frac((v - x1) / num * repeat) : 0f);
			}

			// Token: 0x0600831E RID: 33566 RVA: 0x002DDB90 File Offset: 0x002DBD90
			private float Inset(float v, float x1, float x2, float x3, float x4, float u1, float u2, float u3, float u4, float repeat = 1f)
			{
				if (v < x2)
				{
					return this.Inset(v, x1, x2, u1, u2, 1f);
				}
				if (v < x3)
				{
					return this.Inset(v, x2, x3, u2, u3, repeat);
				}
				return this.Inset(v, x3, x4, u3, u4, 1f);
			}

			// Token: 0x0600831F RID: 33567 RVA: 0x002DDBDE File Offset: 0x002DBDDE
			private float Frac(float v)
			{
				return v - Mathf.Floor(v);
			}

			// Token: 0x06008320 RID: 33568 RVA: 0x002DDBE8 File Offset: 0x002DBDE8
			private float MaskValue(Color mask)
			{
				Color color = mask * this.maskChannelWeights;
				return color.a + color.r + color.g + color.b;
			}

			// Token: 0x04006E7E RID: 28286
			public Vector4 maskRect;

			// Token: 0x04006E7F RID: 28287
			public Vector4 maskBorder;

			// Token: 0x04006E80 RID: 28288
			public Vector4 maskRectUV;

			// Token: 0x04006E81 RID: 28289
			public Vector4 maskBorderUV;

			// Token: 0x04006E82 RID: 28290
			public Vector2 tileRepeat;

			// Token: 0x04006E83 RID: 28291
			public Color maskChannelWeights;

			// Token: 0x04006E84 RID: 28292
			public Matrix4x4 worldToMask;

			// Token: 0x04006E85 RID: 28293
			public Texture texture;

			// Token: 0x04006E86 RID: 28294
			public SoftMask.BorderMode borderMode;

			// Token: 0x04006E87 RID: 28295
			public bool invertMask;

			// Token: 0x04006E88 RID: 28296
			public bool invertOutsides;

			// Token: 0x0200175B RID: 5979
			public enum SampleMaskResult
			{
				// Token: 0x0400759D RID: 30109
				Success,
				// Token: 0x0400759E RID: 30110
				NonReadable,
				// Token: 0x0400759F RID: 30111
				NonTexture2D
			}

			// Token: 0x0200175C RID: 5980
			private static class Ids
			{
				// Token: 0x040075A0 RID: 30112
				public static readonly int SoftMask = Shader.PropertyToID("_SoftMask");

				// Token: 0x040075A1 RID: 30113
				public static readonly int SoftMask_Rect = Shader.PropertyToID("_SoftMask_Rect");

				// Token: 0x040075A2 RID: 30114
				public static readonly int SoftMask_UVRect = Shader.PropertyToID("_SoftMask_UVRect");

				// Token: 0x040075A3 RID: 30115
				public static readonly int SoftMask_ChannelWeights = Shader.PropertyToID("_SoftMask_ChannelWeights");

				// Token: 0x040075A4 RID: 30116
				public static readonly int SoftMask_WorldToMask = Shader.PropertyToID("_SoftMask_WorldToMask");

				// Token: 0x040075A5 RID: 30117
				public static readonly int SoftMask_BorderRect = Shader.PropertyToID("_SoftMask_BorderRect");

				// Token: 0x040075A6 RID: 30118
				public static readonly int SoftMask_UVBorderRect = Shader.PropertyToID("_SoftMask_UVBorderRect");

				// Token: 0x040075A7 RID: 30119
				public static readonly int SoftMask_TileRepeat = Shader.PropertyToID("_SoftMask_TileRepeat");

				// Token: 0x040075A8 RID: 30120
				public static readonly int SoftMask_InvertMask = Shader.PropertyToID("_SoftMask_InvertMask");

				// Token: 0x040075A9 RID: 30121
				public static readonly int SoftMask_InvertOutsides = Shader.PropertyToID("_SoftMask_InvertOutsides");
			}
		}

		// Token: 0x0200151E RID: 5406
		private struct Diagnostics
		{
			// Token: 0x06008321 RID: 33569 RVA: 0x002DDC1D File Offset: 0x002DBE1D
			public Diagnostics(SoftMask softMask)
			{
				this._softMask = softMask;
			}

			// Token: 0x06008322 RID: 33570 RVA: 0x002DDC28 File Offset: 0x002DBE28
			public SoftMask.Errors PollErrors()
			{
				SoftMask softMask = this._softMask;
				SoftMask.Errors errors = SoftMask.Errors.NoError;
				softMask.GetComponentsInChildren<SoftMaskable>(SoftMask.s_maskables);
				using (new ClearListAtExit<SoftMaskable>(SoftMask.s_maskables))
				{
					if (SoftMask.s_maskables.Any((SoftMaskable m) => m.mask == softMask && m.shaderIsNotSupported))
					{
						errors |= SoftMask.Errors.UnsupportedShaders;
					}
				}
				if (this.ThereAreNestedMasks())
				{
					errors |= SoftMask.Errors.NestedMasks;
				}
				errors |= SoftMask.Diagnostics.CheckSprite(this.sprite);
				errors |= this.CheckImage();
				errors |= this.CheckTexture();
				return errors;
			}

			// Token: 0x06008323 RID: 33571 RVA: 0x002DDCD0 File Offset: 0x002DBED0
			public static SoftMask.Errors CheckSprite(Sprite sprite)
			{
				SoftMask.Errors errors = SoftMask.Errors.NoError;
				if (!sprite)
				{
					return errors;
				}
				if (sprite.packed && sprite.packingMode == null)
				{
					errors |= SoftMask.Errors.TightPackedSprite;
				}
				if (sprite.associatedAlphaSplitTexture)
				{
					errors |= SoftMask.Errors.AlphaSplitSprite;
				}
				return errors;
			}

			// Token: 0x17000B36 RID: 2870
			// (get) Token: 0x06008324 RID: 33572 RVA: 0x002DDD0F File Offset: 0x002DBF0F
			private Image image
			{
				get
				{
					return this._softMask.DeduceSourceParameters().image;
				}
			}

			// Token: 0x17000B37 RID: 2871
			// (get) Token: 0x06008325 RID: 33573 RVA: 0x002DDD21 File Offset: 0x002DBF21
			private Sprite sprite
			{
				get
				{
					return this._softMask.DeduceSourceParameters().sprite;
				}
			}

			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x06008326 RID: 33574 RVA: 0x002DDD33 File Offset: 0x002DBF33
			private Texture texture
			{
				get
				{
					return this._softMask.DeduceSourceParameters().texture;
				}
			}

			// Token: 0x06008327 RID: 33575 RVA: 0x002DDD48 File Offset: 0x002DBF48
			private bool ThereAreNestedMasks()
			{
				SoftMask softMask = this._softMask;
				bool flag = false;
				using (new ClearListAtExit<SoftMask>(SoftMask.s_masks))
				{
					softMask.GetComponentsInParent<SoftMask>(false, SoftMask.s_masks);
					flag |= SoftMask.s_masks.Any((SoftMask x) => SoftMask.Diagnostics.AreCompeting(softMask, x));
					softMask.GetComponentsInChildren<SoftMask>(false, SoftMask.s_masks);
					flag |= SoftMask.s_masks.Any((SoftMask x) => SoftMask.Diagnostics.AreCompeting(softMask, x));
				}
				return flag;
			}

			// Token: 0x06008328 RID: 33576 RVA: 0x002DDDEC File Offset: 0x002DBFEC
			private SoftMask.Errors CheckImage()
			{
				SoftMask.Errors errors = SoftMask.Errors.NoError;
				if (!this._softMask.isBasedOnGraphic)
				{
					return errors;
				}
				if (this.image && !SoftMask.IsImageTypeSupported(this.image.type))
				{
					errors |= SoftMask.Errors.UnsupportedImageType;
				}
				return errors;
			}

			// Token: 0x06008329 RID: 33577 RVA: 0x002DDE30 File Offset: 0x002DC030
			private SoftMask.Errors CheckTexture()
			{
				SoftMask.Errors errors = SoftMask.Errors.NoError;
				if (this._softMask.isUsingRaycastFiltering && this.texture)
				{
					Texture2D texture2D = this.texture as Texture2D;
					if (!texture2D)
					{
						errors |= SoftMask.Errors.UnreadableRenderTexture;
					}
					else if (!SoftMask.Diagnostics.IsReadable(texture2D))
					{
						errors |= SoftMask.Errors.UnreadableTexture;
					}
				}
				return errors;
			}

			// Token: 0x0600832A RID: 33578 RVA: 0x002DDE84 File Offset: 0x002DC084
			private static bool AreCompeting(SoftMask softMask, SoftMask other)
			{
				return softMask.isMaskingEnabled && softMask != other && other.isMaskingEnabled && softMask.canvas.rootCanvas == other.canvas.rootCanvas && !SoftMask.Diagnostics.SelectChild<SoftMask>(softMask, other).canvas.overrideSorting;
			}

			// Token: 0x0600832B RID: 33579 RVA: 0x002DDEDD File Offset: 0x002DC0DD
			private static T SelectChild<T>(T first, T second) where T : Component
			{
				if (!first.transform.IsChildOf(second.transform))
				{
					return second;
				}
				return first;
			}

			// Token: 0x0600832C RID: 33580 RVA: 0x002DDF00 File Offset: 0x002DC100
			private static bool IsReadable(Texture2D texture)
			{
				bool result;
				try
				{
					texture.GetPixel(0, 0);
					result = true;
				}
				catch (UnityException)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x04006E89 RID: 28297
			private SoftMask _softMask;
		}

		// Token: 0x0200151F RID: 5407
		private struct WarningReporter
		{
			// Token: 0x0600832D RID: 33581 RVA: 0x002DDF30 File Offset: 0x002DC130
			public WarningReporter(Object owner)
			{
				this._owner = owner;
				this._lastReadTexture = null;
				this._lastUsedSprite = null;
				this._lastUsedImageSprite = null;
				this._lastUsedImageType = 0;
			}

			// Token: 0x0600832E RID: 33582 RVA: 0x002DDF58 File Offset: 0x002DC158
			public void TextureRead(Texture texture, SoftMask.MaterialParameters.SampleMaskResult sampleResult)
			{
				if (this._lastReadTexture == texture)
				{
					return;
				}
				this._lastReadTexture = texture;
				if (sampleResult == SoftMask.MaterialParameters.SampleMaskResult.NonReadable)
				{
					Debug.LogErrorFormat(this._owner, "Raycast Threshold greater than 0 can't be used on Soft Mask with texture '{0}' because it's not readable. You can make the texture readable in the Texture Import Settings.", new object[]
					{
						texture.name
					});
					return;
				}
				if (sampleResult != SoftMask.MaterialParameters.SampleMaskResult.NonTexture2D)
				{
					return;
				}
				Debug.LogErrorFormat(this._owner, "Raycast Threshold greater than 0 can't be used on Soft Mask with texture '{0}' because it's not a Texture2D. Raycast Threshold may be used only with regular 2D textures.", new object[]
				{
					texture.name
				});
			}

			// Token: 0x0600832F RID: 33583 RVA: 0x002DDFC4 File Offset: 0x002DC1C4
			public void SpriteUsed(Sprite sprite, SoftMask.Errors errors)
			{
				if (this._lastUsedSprite == sprite)
				{
					return;
				}
				this._lastUsedSprite = sprite;
				if ((errors & SoftMask.Errors.TightPackedSprite) != SoftMask.Errors.NoError)
				{
					Debug.LogError("SoftMask doesn't support tight packed sprites", this._owner);
				}
				if ((errors & SoftMask.Errors.AlphaSplitSprite) != SoftMask.Errors.NoError)
				{
					Debug.LogError("SoftMask doesn't support sprites with an alpha split texture", this._owner);
				}
			}

			// Token: 0x06008330 RID: 33584 RVA: 0x002DE014 File Offset: 0x002DC214
			public void ImageUsed(Image image)
			{
				if (!image)
				{
					this._lastUsedImageSprite = null;
					this._lastUsedImageType = 0;
					return;
				}
				if (this._lastUsedImageSprite == image.sprite && this._lastUsedImageType == image.type)
				{
					return;
				}
				this._lastUsedImageSprite = image.sprite;
				this._lastUsedImageType = image.type;
				if (!image)
				{
					return;
				}
				if (SoftMask.IsImageTypeSupported(image.type))
				{
					return;
				}
				Debug.LogErrorFormat(this._owner, "SoftMask doesn't support image type {0}. Image type Simple will be used.", new object[]
				{
					image.type
				});
			}

			// Token: 0x04006E8A RID: 28298
			private Object _owner;

			// Token: 0x04006E8B RID: 28299
			private Texture _lastReadTexture;

			// Token: 0x04006E8C RID: 28300
			private Sprite _lastUsedSprite;

			// Token: 0x04006E8D RID: 28301
			private Sprite _lastUsedImageSprite;

			// Token: 0x04006E8E RID: 28302
			private Image.Type _lastUsedImageType;
		}
	}
}
