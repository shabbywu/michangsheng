using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples;

public class CombinedSkin : MonoBehaviour
{
	[SpineSkin("", "", true, false, false)]
	public List<string> skinsToCombine;

	private Skin combinedSkin;

	private void Start()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		ISkeletonComponent component = ((Component)this).GetComponent<ISkeletonComponent>();
		if (component == null)
		{
			return;
		}
		Skeleton skeleton = component.Skeleton;
		if (skeleton == null)
		{
			return;
		}
		combinedSkin = (Skin)(((object)combinedSkin) ?? ((object)new Skin("combined")));
		SkinUtilities.Clear(combinedSkin);
		foreach (string item in skinsToCombine)
		{
			Skin val = skeleton.Data.FindSkin(item);
			if (val != null)
			{
				SkinUtilities.AddAttachments(combinedSkin, val);
			}
		}
		skeleton.SetSkin(combinedSkin);
		skeleton.SetToSetupPose();
		IAnimationStateComponent val2 = (IAnimationStateComponent)(object)((component is IAnimationStateComponent) ? component : null);
		if (val2 != null)
		{
			val2.AnimationState.Apply(skeleton);
		}
	}
}
