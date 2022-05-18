using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E50 RID: 3664
	public class SpriteAttacher : MonoBehaviour
	{
		// Token: 0x060057ED RID: 22509 RVA: 0x002462C4 File Offset: 0x002444C4
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

		// Token: 0x060057EE RID: 22510 RVA: 0x0003EDEE File Offset: 0x0003CFEE
		private void Start()
		{
			this.Initialize(false);
			if (this.attachOnStart)
			{
				this.Attach();
			}
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x0003EE05 File Offset: 0x0003D005
		private void AnimationOverrideSpriteAttach(ISkeletonAnimation animated)
		{
			if (this.overrideAnimation && base.isActiveAndEnabled)
			{
				this.Attach();
			}
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x0024630C File Offset: 0x0024450C
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

		// Token: 0x060057F1 RID: 22513 RVA: 0x00246434 File Offset: 0x00244634
		private void OnDestroy()
		{
			ISkeletonAnimation component = base.GetComponent<ISkeletonAnimation>();
			if (component != null)
			{
				component.UpdateComplete -= new UpdateBonesDelegate(this.AnimationOverrideSpriteAttach);
			}
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x0003EE1D File Offset: 0x0003D01D
		public void Attach()
		{
			if (this.spineSlot != null)
			{
				this.spineSlot.Attachment = this.attachment;
			}
		}

		// Token: 0x040057F0 RID: 22512
		public const string DefaultPMAShader = "Spine/Skeleton";

		// Token: 0x040057F1 RID: 22513
		public const string DefaultStraightAlphaShader = "Sprites/Default";

		// Token: 0x040057F2 RID: 22514
		public bool attachOnStart = true;

		// Token: 0x040057F3 RID: 22515
		public bool overrideAnimation = true;

		// Token: 0x040057F4 RID: 22516
		public Sprite sprite;

		// Token: 0x040057F5 RID: 22517
		[SpineSlot("", "", false, true, false)]
		public string slot;

		// Token: 0x040057F6 RID: 22518
		private RegionAttachment attachment;

		// Token: 0x040057F7 RID: 22519
		private Slot spineSlot;

		// Token: 0x040057F8 RID: 22520
		private bool applyPMA;

		// Token: 0x040057F9 RID: 22521
		private static Dictionary<Texture, AtlasPage> atlasPageCache;
	}
}
