using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF6 RID: 2806
	public class CombinedSkin : MonoBehaviour
	{
		// Token: 0x06004E4A RID: 20042 RVA: 0x002161B4 File Offset: 0x002143B4
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

		// Token: 0x04004DC2 RID: 19906
		[SpineSkin("", "", true, false, false)]
		public List<string> skinsToCombine;

		// Token: 0x04004DC3 RID: 19907
		private Skin combinedSkin;
	}
}
