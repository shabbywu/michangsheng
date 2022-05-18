using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E4F RID: 3663
	public class CombinedSkin : MonoBehaviour
	{
		// Token: 0x060057EB RID: 22507 RVA: 0x002461F0 File Offset: 0x002443F0
		private void Start()
		{
			ISkeletonComponent component = base.GetComponent<ISkeletonComponent>();
			if (component == null)
			{
				return;
			}
			Skeleton skeleton = component.Skeleton;
			if (skeleton == null)
			{
				return;
			}
			this.combinedSkin = (this.combinedSkin ?? new Skin("combined"));
			SkinUtilities.Clear(this.combinedSkin);
			foreach (string text in this.skinsToCombine)
			{
				Skin skin = skeleton.Data.FindSkin(text);
				if (skin != null)
				{
					SkinUtilities.AddAttachments(this.combinedSkin, skin);
				}
			}
			skeleton.SetSkin(this.combinedSkin);
			skeleton.SetToSetupPose();
			IAnimationStateComponent animationStateComponent = component as IAnimationStateComponent;
			if (animationStateComponent != null)
			{
				animationStateComponent.AnimationState.Apply(skeleton);
			}
		}

		// Token: 0x040057EE RID: 22510
		[SpineSkin("", "", true, false, false)]
		public List<string> skinsToCombine;

		// Token: 0x040057EF RID: 22511
		private Skin combinedSkin;
	}
}
