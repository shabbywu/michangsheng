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
	// Token: 0x02000A11 RID: 2577
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Soft Mask", 14)]
	[RequireComponent(typeof(RectTransform))]
	[HelpURL("https://docs.google.com/document/d/1xFZQGn_odhTCokMFR0LyCPXWtqWXN-bBGVS9GETglx8")]
	public class SoftMask : UIBehaviour, ISoftMask, ICanvasRaycastFilter
	{
		// Token: 0x060042AB RID: 17067 RVA: 0x001CB12C File Offset: 0x001C932C
		public SoftMask()
		{
			MaterialReplacerChain replacer = new MaterialReplacerChain(MaterialReplacer.globalReplacers, new SoftMask.MaterialReplacerImpl(this));
			this._materials = new MaterialReplacements(replacer, delegate(Material m)
			{
				this._parameters.Apply(m);
			});
			this._warningReporter = new SoftMask.WarningReporter(this);
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x0002F821 File Offset: 0x0002DA21
		// (set) Token: 0x060042AD RID: 17069 RVA: 0x0002F829 File Offset: 0x0002DA29
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

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x0002F839 File Offset: 0x0002DA39
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x0002F841 File Offset: 0x0002DA41
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

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x0002F851 File Offset: 0x0002DA51
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x0002F859 File Offset: 0x0002DA59
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

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x0002F871 File Offset: 0x0002DA71
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x0002F879 File Offset: 0x0002DA79
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

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		// (set) Token: 0x060042B5 RID: 17077 RVA: 0x0002F8AC File Offset: 0x0002DAAC
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

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x0002F8C9 File Offset: 0x0002DAC9
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x0002F8D1 File Offset: 0x0002DAD1
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

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x0002F8E9 File Offset: 0x0002DAE9
		// (set) Token: 0x060042B9 RID: 17081 RVA: 0x0002F8F1 File Offset: 0x0002DAF1
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

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x0002F90E File Offset: 0x0002DB0E
		// (set) Token: 0x060042BB RID: 17083 RVA: 0x0002F91B File Offset: 0x0002DB1B
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

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x0002F938 File Offset: 0x0002DB38
		// (set) Token: 0x060042BD RID: 17085 RVA: 0x0002F91B File Offset: 0x0002DB1B
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

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x0002F945 File Offset: 0x0002DB45
		// (set) Token: 0x060042BF RID: 17087 RVA: 0x0002F94D File Offset: 0x0002DB4D
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

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x0002F96A File Offset: 0x0002DB6A
		// (set) Token: 0x060042C1 RID: 17089 RVA: 0x0002F972 File Offset: 0x0002DB72
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

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x0002F98F File Offset: 0x0002DB8F
		// (set) Token: 0x060042C3 RID: 17091 RVA: 0x0002F997 File Offset: 0x0002DB97
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

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		// (set) Token: 0x060042C5 RID: 17093 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
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

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		// (set) Token: 0x060042C7 RID: 17095 RVA: 0x0002F9C8 File Offset: 0x0002DBC8
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

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x0002F9E0 File Offset: 0x0002DBE0
		public bool isUsingRaycastFiltering
		{
			get
			{
				return this._raycastThreshold > 0f;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x0002F9EF File Offset: 0x0002DBEF
		public bool isMaskingEnabled
		{
			get
			{
				return base.isActiveAndEnabled && this.canvas;
			}
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x001CB198 File Offset: 0x001C9398
		public SoftMask.Errors PollErrors()
		{
			return new SoftMask.Diagnostics(this).PollErrors();
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x001CB1B4 File Offset: 0x001C93B4
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

		// Token: 0x060042CC RID: 17100 RVA: 0x0002FA06 File Offset: 0x0002DC06
		protected override void Start()
		{
			base.Start();
			this.WarnIfDefaultShaderIsNotSet();
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0002FA14 File Offset: 0x0002DC14
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

		// Token: 0x060042CE RID: 17102 RVA: 0x001CB254 File Offset: 0x001C9454
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

		// Token: 0x060042CF RID: 17103 RVA: 0x0002FA48 File Offset: 0x0002DC48
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._destroyed = true;
			this.NotifyChildrenThatMaskMightChanged();
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x001CB2BC File Offset: 0x001C94BC
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

		// Token: 0x060042D1 RID: 17105 RVA: 0x0002FA5D File Offset: 0x0002DC5D
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this._dirty = true;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0002FA6C File Offset: 0x0002DC6C
		protected override void OnDidApplyAnimationProperties()
		{
			base.OnDidApplyAnimationProperties();
			this._dirty = true;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0002FA7B File Offset: 0x0002DC7B
		private static float ClampPixelsPerUnitMultiplier(float value)
		{
			return Mathf.Max(value, 0.01f);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0002FA88 File Offset: 0x0002DC88
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this._canvas = null;
			this._dirty = true;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0002FA9E File Offset: 0x0002DC9E
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this._canvas = null;
			this._dirty = true;
			this.NotifyChildrenThatMaskMightChanged();
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0002FABA File Offset: 0x0002DCBA
		private void OnTransformChildrenChanged()
		{
			this.SpawnMaskablesInChildren(base.transform);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		private void SubscribeOnWillRenderCanvases()
		{
			SoftMask.Touch<CanvasUpdateRegistry>(CanvasUpdateRegistry.instance);
			Canvas.willRenderCanvases += new Canvas.WillRenderCanvases(this.OnWillRenderCanvases);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0002FAE6 File Offset: 0x0002DCE6
		private void UnsubscribeFromWillRenderCanvases()
		{
			Canvas.willRenderCanvases -= new Canvas.WillRenderCanvases(this.OnWillRenderCanvases);
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0002FAF9 File Offset: 0x0002DCF9
		private void OnWillRenderCanvases()
		{
			if (this.isMaskingEnabled)
			{
				this.UpdateMaskParameters();
			}
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0002FB09 File Offset: 0x0002DD09
		private static T Touch<T>(T obj)
		{
			return obj;
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x001CB324 File Offset: 0x001C9524
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

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x001CB36C File Offset: 0x001C956C
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

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x0002FB0C File Offset: 0x0002DD0C
		private bool isBasedOnGraphic
		{
			get
			{
				return this._source == SoftMask.MaskSource.Graphic;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x0002FB17 File Offset: 0x0002DD17
		bool ISoftMask.isAlive
		{
			get
			{
				return this && !this._destroyed;
			}
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x0002FB2C File Offset: 0x0002DD2C
		Material ISoftMask.GetReplacement(Material original)
		{
			return this._materials.Get(original);
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0002FB3A File Offset: 0x0002DD3A
		void ISoftMask.ReleaseReplacement(Material replacement)
		{
			this._materials.Release(replacement);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0002FB48 File Offset: 0x0002DD48
		void ISoftMask.UpdateTransformChildren(Transform transform)
		{
			this.SpawnMaskablesInChildren(transform);
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0002FB51 File Offset: 0x0002DD51
		private void OnGraphicDirty()
		{
			if (this.isBasedOnGraphic)
			{
				this._dirty = true;
			}
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x001CB39C File Offset: 0x001C959C
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

		// Token: 0x060042E4 RID: 17124 RVA: 0x001CB40C File Offset: 0x001C960C
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

		// Token: 0x060042E5 RID: 17125 RVA: 0x001CB440 File Offset: 0x001C9640
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

		// Token: 0x060042E6 RID: 17126 RVA: 0x001CB498 File Offset: 0x001C9698
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

		// Token: 0x060042E7 RID: 17127 RVA: 0x0002FB62 File Offset: 0x0002DD62
		private void InvalidateChildren()
		{
			this.ForEachChildMaskable(delegate(SoftMaskable x)
			{
				x.Invalidate();
			});
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0002FB89 File Offset: 0x0002DD89
		private void NotifyChildrenThatMaskMightChanged()
		{
			this.ForEachChildMaskable(delegate(SoftMaskable x)
			{
				x.MaskMightChanged();
			});
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x001CB510 File Offset: 0x001C9710
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

		// Token: 0x060042EA RID: 17130 RVA: 0x0002FBB0 File Offset: 0x0002DDB0
		private void DestroyMaterials()
		{
			this._materials.DestroyAllAndClear();
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x001CB59C File Offset: 0x001C979C
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

		// Token: 0x060042EC RID: 17132 RVA: 0x0002FBBD File Offset: 0x0002DDBD
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

		// Token: 0x060042ED RID: 17133 RVA: 0x0002FBDA File Offset: 0x0002DDDA
		public static bool IsImageTypeSupported(Image.Type type)
		{
			return type == null || type == 1 || type == 2;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x001CB704 File Offset: 0x001C9904
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

		// Token: 0x060042EF RID: 17135 RVA: 0x001CB7A0 File Offset: 0x001C99A0
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

		// Token: 0x060042F0 RID: 17136 RVA: 0x001CB8B0 File Offset: 0x001C9AB0
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

		// Token: 0x060042F1 RID: 17137 RVA: 0x001CB93C File Offset: 0x001C9B3C
		private void CalculateTextureBased(Texture texture, Rect uvRect)
		{
			this.FillCommonParameters();
			this._parameters.maskRect = this.LocalMaskRect(Vector4.zero);
			this._parameters.maskRectUV = SoftMask.Mathr.ToVector(uvRect);
			this._parameters.texture = texture;
			this._parameters.borderMode = SoftMask.BorderMode.Simple;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0002FBE9 File Offset: 0x0002DDE9
		private void CalculateSolidFill()
		{
			this.CalculateTextureBased(null, SoftMask.DefaultUVRect);
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x001CB990 File Offset: 0x001C9B90
		private void FillCommonParameters()
		{
			this._parameters.worldToMask = this.WorldToMask();
			this._parameters.maskChannelWeights = this._channelWeights;
			this._parameters.invertMask = this._invertMask;
			this._parameters.invertOutsides = this._invertOutsides;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0002FBF7 File Offset: 0x0002DDF7
		private float SpriteToCanvasScale(float spritePixelsPerUnit)
		{
			return (this.canvas ? this.canvas.referencePixelsPerUnit : 100f) / spritePixelsPerUnit;
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x0002FC1A File Offset: 0x0002DE1A
		private Matrix4x4 WorldToMask()
		{
			return this.maskTransform.worldToLocalMatrix * this.canvas.rootCanvas.transform.localToWorldMatrix;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x0002FC41 File Offset: 0x0002DE41
		private Vector4 LocalMaskRect(Vector4 border)
		{
			return SoftMask.Mathr.ApplyBorder(SoftMask.Mathr.ToVector(this.maskTransform.rect), border);
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x001CB9E4 File Offset: 0x001C9BE4
		private Vector2 MaskRepeat(Sprite sprite, float spritePixelsPerUnit, Vector4 centralPart)
		{
			Vector4 r = SoftMask.Mathr.ApplyBorder(SoftMask.Mathr.ToVector(sprite.rect), sprite.border);
			return SoftMask.Mathr.Div(SoftMask.Mathr.Size(centralPart) * this.SpriteToCanvasScale(spritePixelsPerUnit), SoftMask.Mathr.Size(r));
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x0002FC59 File Offset: 0x0002DE59
		private void WarnIfDefaultShaderIsNotSet()
		{
			if (!this._defaultShader)
			{
				Debug.LogWarning("SoftMask may not work because its defaultShader is not set", this);
			}
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x0002FC73 File Offset: 0x0002DE73
		private void Set<T>(ref T field, T value)
		{
			field = value;
			this._dirty = true;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x0002FC83 File Offset: 0x0002DE83
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

		// Token: 0x04003AFB RID: 15099
		[SerializeField]
		private Shader _defaultShader;

		// Token: 0x04003AFC RID: 15100
		[SerializeField]
		private Shader _defaultETC1Shader;

		// Token: 0x04003AFD RID: 15101
		[SerializeField]
		private SoftMask.MaskSource _source;

		// Token: 0x04003AFE RID: 15102
		[SerializeField]
		private RectTransform _separateMask;

		// Token: 0x04003AFF RID: 15103
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x04003B00 RID: 15104
		[SerializeField]
		private SoftMask.BorderMode _spriteBorderMode;

		// Token: 0x04003B01 RID: 15105
		[SerializeField]
		private float _spritePixelsPerUnitMultiplier = 1f;

		// Token: 0x04003B02 RID: 15106
		[SerializeField]
		private Texture _texture;

		// Token: 0x04003B03 RID: 15107
		[SerializeField]
		private Rect _textureUVRect = SoftMask.DefaultUVRect;

		// Token: 0x04003B04 RID: 15108
		[SerializeField]
		private Color _channelWeights = MaskChannel.alpha;

		// Token: 0x04003B05 RID: 15109
		[SerializeField]
		private float _raycastThreshold;

		// Token: 0x04003B06 RID: 15110
		[SerializeField]
		private bool _invertMask;

		// Token: 0x04003B07 RID: 15111
		[SerializeField]
		private bool _invertOutsides;

		// Token: 0x04003B08 RID: 15112
		private MaterialReplacements _materials;

		// Token: 0x04003B09 RID: 15113
		private SoftMask.MaterialParameters _parameters;

		// Token: 0x04003B0A RID: 15114
		private SoftMask.WarningReporter _warningReporter;

		// Token: 0x04003B0B RID: 15115
		private Rect _lastMaskRect;

		// Token: 0x04003B0C RID: 15116
		private bool _maskingWasEnabled;

		// Token: 0x04003B0D RID: 15117
		private bool _destroyed;

		// Token: 0x04003B0E RID: 15118
		private bool _dirty;

		// Token: 0x04003B0F RID: 15119
		private RectTransform _maskTransform;

		// Token: 0x04003B10 RID: 15120
		private Graphic _graphic;

		// Token: 0x04003B11 RID: 15121
		private Canvas _canvas;

		// Token: 0x04003B12 RID: 15122
		private static readonly Rect DefaultUVRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04003B13 RID: 15123
		private const float DefaultPixelsPerUnit = 100f;

		// Token: 0x04003B14 RID: 15124
		private static readonly List<SoftMask> s_masks = new List<SoftMask>();

		// Token: 0x04003B15 RID: 15125
		private static readonly List<SoftMaskable> s_maskables = new List<SoftMaskable>();

		// Token: 0x02000A12 RID: 2578
		[Serializable]
		public enum MaskSource
		{
			// Token: 0x04003B17 RID: 15127
			Graphic,
			// Token: 0x04003B18 RID: 15128
			Sprite,
			// Token: 0x04003B19 RID: 15129
			Texture
		}

		// Token: 0x02000A13 RID: 2579
		[Serializable]
		public enum BorderMode
		{
			// Token: 0x04003B1B RID: 15131
			Simple,
			// Token: 0x04003B1C RID: 15132
			Sliced,
			// Token: 0x04003B1D RID: 15133
			Tiled
		}

		// Token: 0x02000A14 RID: 2580
		[Flags]
		[Serializable]
		public enum Errors
		{
			// Token: 0x04003B1F RID: 15135
			NoError = 0,
			// Token: 0x04003B20 RID: 15136
			UnsupportedShaders = 1,
			// Token: 0x04003B21 RID: 15137
			NestedMasks = 2,
			// Token: 0x04003B22 RID: 15138
			TightPackedSprite = 4,
			// Token: 0x04003B23 RID: 15139
			AlphaSplitSprite = 8,
			// Token: 0x04003B24 RID: 15140
			UnsupportedImageType = 16,
			// Token: 0x04003B25 RID: 15141
			UnreadableTexture = 32,
			// Token: 0x04003B26 RID: 15142
			UnreadableRenderTexture = 64
		}

		// Token: 0x02000A15 RID: 2581
		private struct SourceParameters
		{
			// Token: 0x04003B27 RID: 15143
			public Image image;

			// Token: 0x04003B28 RID: 15144
			public Sprite sprite;

			// Token: 0x04003B29 RID: 15145
			public SoftMask.BorderMode spriteBorderMode;

			// Token: 0x04003B2A RID: 15146
			public float spritePixelsPerUnit;

			// Token: 0x04003B2B RID: 15147
			public Texture texture;

			// Token: 0x04003B2C RID: 15148
			public Rect textureUVRect;
		}

		// Token: 0x02000A16 RID: 2582
		private class MaterialReplacerImpl : IMaterialReplacer
		{
			// Token: 0x060042FD RID: 17149 RVA: 0x0002FCE9 File Offset: 0x0002DEE9
			public MaterialReplacerImpl(SoftMask owner)
			{
				this._owner = owner;
			}

			// Token: 0x170007C5 RID: 1989
			// (get) Token: 0x060042FE RID: 17150 RVA: 0x00004050 File Offset: 0x00002250
			public int order
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060042FF RID: 17151 RVA: 0x001CBA28 File Offset: 0x001C9C28
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

			// Token: 0x06004300 RID: 17152 RVA: 0x001CBA84 File Offset: 0x001C9C84
			private static Material Replace(Material original, Shader defaultReplacementShader)
			{
				Material material = defaultReplacementShader ? new Material(defaultReplacementShader) : null;
				if (material && original)
				{
					material.CopyPropertiesFromMaterial(original);
				}
				return material;
			}

			// Token: 0x04003B2D RID: 15149
			private readonly SoftMask _owner;
		}

		// Token: 0x02000A17 RID: 2583
		private static class Mathr
		{
			// Token: 0x06004301 RID: 17153 RVA: 0x0002FCF8 File Offset: 0x0002DEF8
			public static Vector4 ToVector(Rect r)
			{
				return new Vector4(r.xMin, r.yMin, r.xMax, r.yMax);
			}

			// Token: 0x06004302 RID: 17154 RVA: 0x0002FD1B File Offset: 0x0002DF1B
			public static Vector4 Div(Vector4 v, Vector2 s)
			{
				return new Vector4(v.x / s.x, v.y / s.y, v.z / s.x, v.w / s.y);
			}

			// Token: 0x06004303 RID: 17155 RVA: 0x0002FD56 File Offset: 0x0002DF56
			public static Vector2 Div(Vector2 v, Vector2 s)
			{
				return new Vector2(v.x / s.x, v.y / s.y);
			}

			// Token: 0x06004304 RID: 17156 RVA: 0x0002FD77 File Offset: 0x0002DF77
			public static Vector4 Mul(Vector4 v, Vector2 s)
			{
				return new Vector4(v.x * s.x, v.y * s.y, v.z * s.x, v.w * s.y);
			}

			// Token: 0x06004305 RID: 17157 RVA: 0x0002FDB2 File Offset: 0x0002DFB2
			public static Vector2 Size(Vector4 r)
			{
				return new Vector2(r.z - r.x, r.w - r.y);
			}

			// Token: 0x06004306 RID: 17158 RVA: 0x0002FDD3 File Offset: 0x0002DFD3
			public static Vector4 Move(Vector4 v, Vector2 o)
			{
				return new Vector4(v.x + o.x, v.y + o.y, v.z + o.x, v.w + o.y);
			}

			// Token: 0x06004307 RID: 17159 RVA: 0x0002FE0E File Offset: 0x0002E00E
			public static Vector4 BorderOf(Vector4 outer, Vector4 inner)
			{
				return new Vector4(inner.x - outer.x, inner.y - outer.y, outer.z - inner.z, outer.w - inner.w);
			}

			// Token: 0x06004308 RID: 17160 RVA: 0x0002FE49 File Offset: 0x0002E049
			public static Vector4 ApplyBorder(Vector4 v, Vector4 b)
			{
				return new Vector4(v.x + b.x, v.y + b.y, v.z - b.z, v.w - b.w);
			}

			// Token: 0x06004309 RID: 17161 RVA: 0x0002FE84 File Offset: 0x0002E084
			public static Vector2 Min(Vector4 r)
			{
				return new Vector2(r.x, r.y);
			}

			// Token: 0x0600430A RID: 17162 RVA: 0x0002FE97 File Offset: 0x0002E097
			public static Vector2 Max(Vector4 r)
			{
				return new Vector2(r.z, r.w);
			}

			// Token: 0x0600430B RID: 17163 RVA: 0x001CBABC File Offset: 0x001C9CBC
			public static Vector2 Remap(Vector2 c, Vector4 from, Vector4 to)
			{
				Vector2 s = SoftMask.Mathr.Max(from) - SoftMask.Mathr.Min(from);
				Vector2 vector = SoftMask.Mathr.Max(to) - SoftMask.Mathr.Min(to);
				return Vector2.Scale(SoftMask.Mathr.Div(c - SoftMask.Mathr.Min(from), s), vector) + SoftMask.Mathr.Min(to);
			}

			// Token: 0x0600430C RID: 17164 RVA: 0x0002FEAA File Offset: 0x0002E0AA
			public static bool Inside(Vector2 v, Vector4 r)
			{
				return v.x >= r.x && v.y >= r.y && v.x <= r.z && v.y <= r.w;
			}
		}

		// Token: 0x02000A18 RID: 2584
		private struct MaterialParameters
		{
			// Token: 0x170007C6 RID: 1990
			// (get) Token: 0x0600430D RID: 17165 RVA: 0x0002FEE9 File Offset: 0x0002E0E9
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

			// Token: 0x0600430E RID: 17166 RVA: 0x001CBB10 File Offset: 0x001C9D10
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

			// Token: 0x0600430F RID: 17167 RVA: 0x001CBB78 File Offset: 0x001C9D78
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

			// Token: 0x06004310 RID: 17168 RVA: 0x001CBC90 File Offset: 0x001C9E90
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

			// Token: 0x06004311 RID: 17169 RVA: 0x0002FF04 File Offset: 0x0002E104
			private Vector2 MapSimple(Vector2 localPos)
			{
				return SoftMask.Mathr.Remap(localPos, this.maskRect, this.maskRectUV);
			}

			// Token: 0x06004312 RID: 17170 RVA: 0x001CBCDC File Offset: 0x001C9EDC
			private Vector2 MapBorder(Vector2 localPos, bool repeat)
			{
				return new Vector2(this.Inset(localPos.x, this.maskRect.x, this.maskBorder.x, this.maskBorder.z, this.maskRect.z, this.maskRectUV.x, this.maskBorderUV.x, this.maskBorderUV.z, this.maskRectUV.z, repeat ? this.tileRepeat.x : 1f), this.Inset(localPos.y, this.maskRect.y, this.maskBorder.y, this.maskBorder.w, this.maskRect.w, this.maskRectUV.y, this.maskBorderUV.y, this.maskBorderUV.w, this.maskRectUV.w, repeat ? this.tileRepeat.y : 1f));
			}

			// Token: 0x06004313 RID: 17171 RVA: 0x001CBDE0 File Offset: 0x001C9FE0
			private float Inset(float v, float x1, float x2, float u1, float u2, float repeat = 1f)
			{
				float num = x2 - x1;
				return Mathf.Lerp(u1, u2, (num != 0f) ? this.Frac((v - x1) / num * repeat) : 0f);
			}

			// Token: 0x06004314 RID: 17172 RVA: 0x001CBE18 File Offset: 0x001CA018
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

			// Token: 0x06004315 RID: 17173 RVA: 0x0002FF18 File Offset: 0x0002E118
			private float Frac(float v)
			{
				return v - Mathf.Floor(v);
			}

			// Token: 0x06004316 RID: 17174 RVA: 0x001CBE68 File Offset: 0x001CA068
			private float MaskValue(Color mask)
			{
				Color color = mask * this.maskChannelWeights;
				return color.a + color.r + color.g + color.b;
			}

			// Token: 0x04003B2E RID: 15150
			public Vector4 maskRect;

			// Token: 0x04003B2F RID: 15151
			public Vector4 maskBorder;

			// Token: 0x04003B30 RID: 15152
			public Vector4 maskRectUV;

			// Token: 0x04003B31 RID: 15153
			public Vector4 maskBorderUV;

			// Token: 0x04003B32 RID: 15154
			public Vector2 tileRepeat;

			// Token: 0x04003B33 RID: 15155
			public Color maskChannelWeights;

			// Token: 0x04003B34 RID: 15156
			public Matrix4x4 worldToMask;

			// Token: 0x04003B35 RID: 15157
			public Texture texture;

			// Token: 0x04003B36 RID: 15158
			public SoftMask.BorderMode borderMode;

			// Token: 0x04003B37 RID: 15159
			public bool invertMask;

			// Token: 0x04003B38 RID: 15160
			public bool invertOutsides;

			// Token: 0x02000A19 RID: 2585
			public enum SampleMaskResult
			{
				// Token: 0x04003B3A RID: 15162
				Success,
				// Token: 0x04003B3B RID: 15163
				NonReadable,
				// Token: 0x04003B3C RID: 15164
				NonTexture2D
			}

			// Token: 0x02000A1A RID: 2586
			private static class Ids
			{
				// Token: 0x04003B3D RID: 15165
				public static readonly int SoftMask = Shader.PropertyToID("_SoftMask");

				// Token: 0x04003B3E RID: 15166
				public static readonly int SoftMask_Rect = Shader.PropertyToID("_SoftMask_Rect");

				// Token: 0x04003B3F RID: 15167
				public static readonly int SoftMask_UVRect = Shader.PropertyToID("_SoftMask_UVRect");

				// Token: 0x04003B40 RID: 15168
				public static readonly int SoftMask_ChannelWeights = Shader.PropertyToID("_SoftMask_ChannelWeights");

				// Token: 0x04003B41 RID: 15169
				public static readonly int SoftMask_WorldToMask = Shader.PropertyToID("_SoftMask_WorldToMask");

				// Token: 0x04003B42 RID: 15170
				public static readonly int SoftMask_BorderRect = Shader.PropertyToID("_SoftMask_BorderRect");

				// Token: 0x04003B43 RID: 15171
				public static readonly int SoftMask_UVBorderRect = Shader.PropertyToID("_SoftMask_UVBorderRect");

				// Token: 0x04003B44 RID: 15172
				public static readonly int SoftMask_TileRepeat = Shader.PropertyToID("_SoftMask_TileRepeat");

				// Token: 0x04003B45 RID: 15173
				public static readonly int SoftMask_InvertMask = Shader.PropertyToID("_SoftMask_InvertMask");

				// Token: 0x04003B46 RID: 15174
				public static readonly int SoftMask_InvertOutsides = Shader.PropertyToID("_SoftMask_InvertOutsides");
			}
		}

		// Token: 0x02000A1B RID: 2587
		private struct Diagnostics
		{
			// Token: 0x06004318 RID: 17176 RVA: 0x0002FF22 File Offset: 0x0002E122
			public Diagnostics(SoftMask softMask)
			{
				this._softMask = softMask;
			}

			// Token: 0x06004319 RID: 17177 RVA: 0x001CBF44 File Offset: 0x001CA144
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

			// Token: 0x0600431A RID: 17178 RVA: 0x001CBFEC File Offset: 0x001CA1EC
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

			// Token: 0x170007C7 RID: 1991
			// (get) Token: 0x0600431B RID: 17179 RVA: 0x0002FF2B File Offset: 0x0002E12B
			private Image image
			{
				get
				{
					return this._softMask.DeduceSourceParameters().image;
				}
			}

			// Token: 0x170007C8 RID: 1992
			// (get) Token: 0x0600431C RID: 17180 RVA: 0x0002FF3D File Offset: 0x0002E13D
			private Sprite sprite
			{
				get
				{
					return this._softMask.DeduceSourceParameters().sprite;
				}
			}

			// Token: 0x170007C9 RID: 1993
			// (get) Token: 0x0600431D RID: 17181 RVA: 0x0002FF4F File Offset: 0x0002E14F
			private Texture texture
			{
				get
				{
					return this._softMask.DeduceSourceParameters().texture;
				}
			}

			// Token: 0x0600431E RID: 17182 RVA: 0x001CC02C File Offset: 0x001CA22C
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

			// Token: 0x0600431F RID: 17183 RVA: 0x001CC0D0 File Offset: 0x001CA2D0
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

			// Token: 0x06004320 RID: 17184 RVA: 0x001CC114 File Offset: 0x001CA314
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

			// Token: 0x06004321 RID: 17185 RVA: 0x001CC168 File Offset: 0x001CA368
			private static bool AreCompeting(SoftMask softMask, SoftMask other)
			{
				return softMask.isMaskingEnabled && softMask != other && other.isMaskingEnabled && softMask.canvas.rootCanvas == other.canvas.rootCanvas && !SoftMask.Diagnostics.SelectChild<SoftMask>(softMask, other).canvas.overrideSorting;
			}

			// Token: 0x06004322 RID: 17186 RVA: 0x0002FF61 File Offset: 0x0002E161
			private static T SelectChild<T>(T first, T second) where T : Component
			{
				if (!first.transform.IsChildOf(second.transform))
				{
					return second;
				}
				return first;
			}

			// Token: 0x06004323 RID: 17187 RVA: 0x001CC1C4 File Offset: 0x001CA3C4
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

			// Token: 0x04003B47 RID: 15175
			private SoftMask _softMask;
		}

		// Token: 0x02000A1E RID: 2590
		private struct WarningReporter
		{
			// Token: 0x06004329 RID: 17193 RVA: 0x0002FFA9 File Offset: 0x0002E1A9
			public WarningReporter(Object owner)
			{
				this._owner = owner;
				this._lastReadTexture = null;
				this._lastUsedSprite = null;
				this._lastUsedImageSprite = null;
				this._lastUsedImageType = 0;
			}

			// Token: 0x0600432A RID: 17194 RVA: 0x001CC1F4 File Offset: 0x001CA3F4
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

			// Token: 0x0600432B RID: 17195 RVA: 0x001CC260 File Offset: 0x001CA460
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

			// Token: 0x0600432C RID: 17196 RVA: 0x001CC2B0 File Offset: 0x001CA4B0
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

			// Token: 0x04003B4A RID: 15178
			private Object _owner;

			// Token: 0x04003B4B RID: 15179
			private Texture _lastReadTexture;

			// Token: 0x04003B4C RID: 15180
			private Sprite _lastUsedSprite;

			// Token: 0x04003B4D RID: 15181
			private Sprite _lastUsedImageSprite;

			// Token: 0x04003B4E RID: 15182
			private Image.Type _lastUsedImageType;
		}
	}
}
