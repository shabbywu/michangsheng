using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Prototyping
{
	// Token: 0x02000E13 RID: 3603
	public class SpineEventUnityHandler : MonoBehaviour
	{
		// Token: 0x06005708 RID: 22280 RVA: 0x00243804 File Offset: 0x00241A04
		private void Start()
		{
			this.skeletonComponent = (this.skeletonComponent ?? base.GetComponent<ISkeletonComponent>());
			if (this.skeletonComponent == null)
			{
				return;
			}
			this.animationStateComponent = (this.animationStateComponent ?? (this.skeletonComponent as IAnimationStateComponent));
			if (this.animationStateComponent == null)
			{
				return;
			}
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			if (skeleton == null)
			{
				return;
			}
			SkeletonData data = skeleton.Data;
			AnimationState animationState = this.animationStateComponent.AnimationState;
			using (List<SpineEventUnityHandler.EventPair>.Enumerator enumerator = this.events.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpineEventUnityHandler.<>c__DisplayClass4_0 CS$<>8__locals1 = new SpineEventUnityHandler.<>c__DisplayClass4_0();
					CS$<>8__locals1.ep = enumerator.Current;
					EventData eventData = data.FindEvent(CS$<>8__locals1.ep.spineEvent);
					CS$<>8__locals1.ep.eventDelegate = (CS$<>8__locals1.ep.eventDelegate ?? delegate(TrackEntry trackEntry, Event e)
					{
						if (e.Data == eventData)
						{
							CS$<>8__locals1.ep.unityHandler.Invoke();
						}
					});
					animationState.Event += CS$<>8__locals1.ep.eventDelegate;
				}
			}
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x00243944 File Offset: 0x00241B44
		private void OnDestroy()
		{
			this.animationStateComponent = (this.animationStateComponent ?? base.GetComponent<IAnimationStateComponent>());
			if (this.animationStateComponent == null)
			{
				return;
			}
			AnimationState animationState = this.animationStateComponent.AnimationState;
			foreach (SpineEventUnityHandler.EventPair eventPair in this.events)
			{
				if (eventPair.eventDelegate != null)
				{
					animationState.Event -= eventPair.eventDelegate;
				}
				eventPair.eventDelegate = null;
			}
		}

		// Token: 0x040056B5 RID: 22197
		public List<SpineEventUnityHandler.EventPair> events = new List<SpineEventUnityHandler.EventPair>();

		// Token: 0x040056B6 RID: 22198
		private ISkeletonComponent skeletonComponent;

		// Token: 0x040056B7 RID: 22199
		private IAnimationStateComponent animationStateComponent;

		// Token: 0x02000E14 RID: 3604
		[Serializable]
		public class EventPair
		{
			// Token: 0x040056B8 RID: 22200
			[SpineEvent("", "", true, false, false)]
			public string spineEvent;

			// Token: 0x040056B9 RID: 22201
			public UnityEvent unityHandler;

			// Token: 0x040056BA RID: 22202
			public AnimationState.TrackEntryEventDelegate eventDelegate;
		}
	}
}
