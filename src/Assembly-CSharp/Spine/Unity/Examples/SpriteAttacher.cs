using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF7 RID: 2807
	public class SpriteAttacher : MonoBehaviour
	{
		// Token: 0x06004E4C RID: 20044 RVA: 0x00216288 File Offset: 0x00214488
		private static AtlasPage GetPageFor(Texture texture, Shader shader)
		{
			if (SpriteAttacher.atlasPageCache == null)
			{
				SpriteAttacher.atlasPageCache = new Dictionary<Texture, AtlasPage>();
			}
			AtlasPage atlasPage;
			SpriteAttacher.atlasPageCache.TryGetValue(texture, out atlasPage);
			if (atlasPage == null)
			{
				atlasPage = AtlasUtilities.ToSpineAtlasPage(new Material(shader));
				SpriteAttacher.atlasPageCache[texture] = atlasPage;
			}
			return atlasPage;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x002162D0 File Offset: 0x002144D0
		private void Start()
		{
			this.Initialize(false);
			if (this.attachOnStart)
			{
				this.Attach();
			}
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x002162E7 File Offset: 0x002144E7
		private void AnimationOverrideSpriteAttach(ISkeletonAnimation animated)
		{
			if (this.overrideAnimation && base.isActiveAndEnabled)
			{
				this.Attach();
			}
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00216300 File Offset: 0x00214500
		public void Initialize(bool overwrite = true)
		{
			if (overwrite || this.attachment == null)
			{
				ISkeletonComponent component = base.GetComponent<ISkeletonComponent>();
				SkeletonRenderer skeletonRenderer = component as SkeletonRenderer;
				if (skeletonRenderer != null)
				{
					this.applyPMA = skeletonRenderer.pmaVertexColors;
				}
				else
				{
					SkeletonGraphic skeletonGraphic = component as SkeletonGraphic;
					if (skeletonGraphic != null)
					{
						this.applyPMA = skeletonGraphic.MeshGenerator.settings.pmaVertexColors;
					}
				}
				if (this.overrideAnimation)
				{
					ISkeletonAnimation skeletonAnimation = component as ISkeletonAnimation;
					if (skeletonAnimation != null)
					{
						skeletonAnimation.UpdateComplete -= new UpdateBonesDelegate(this.AnimationOverrideSpriteAttach);
						skeletonAnimation.UpdateComplete += new UpdateBonesDelegate(this.AnimationOverrideSpriteAttach);
					}
				}
				this.spineSlot = (this.spineSlot ?? component.Skeleton.FindSlot(this.slot));
				Shader shader = this.applyPMA ? Shader.Find("Spine/Skeleton") : Shader.Find("Sprites/Default");
				this.attachment = (this.applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(this.sprite, shader, 4, false, null, 0f) : AttachmentRegionExtensions.ToRegionAttachment(this.sprite, SpriteAttacher.GetPageFor(this.sprite.texture, shader), 0f));
			}
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00216428 File Offset: 0x00214628
		private void OnDestroy()
		{
			ISkeletonAnimation component = base.GetComponent<ISkeletonAnimation>();
			if (component != null)
			{
				component.UpdateComplete -= new UpdateBonesDelegate(this.AnimationOverrideSpriteAttach);
			}
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00216451 File Offset: 0x00214651
		public void Attach()
		{
			if (this.spineSlot != null)
			{
				this.spineSlot.Attachment = this.attachment;
			}
		}

		// Token: 0x04004DC4 RID: 19908
		public const string DefaultPMAShader = "Spine/Skeleton";

		// Token: 0x04004DC5 RID: 19909
		public const string DefaultStraightAlphaShader = "Sprites/Default";

		// Token: 0x04004DC6 RID: 19910
		public bool attachOnStart = true;

		// Token: 0x04004DC7 RID: 19911
		public bool overrideAnimation = true;

		// Token: 0x04004DC8 RID: 19912
		public Sprite sprite;

		// Token: 0x04004DC9 RID: 19913
		[SpineSlot("", "", false, true, false)]
		public string slot;

		// Token: 0x04004DCA RID: 19914
		private RegionAttachment attachment;

		// Token: 0x04004DCB RID: 19915
		private Slot spineSlot;

		// Token: 0x04004DCC RID: 19916
		private bool applyPMA;

		// Token: 0x04004DCD RID: 19917
		private static Dictionary<Texture, AtlasPage> atlasPageCache;
	}
}
