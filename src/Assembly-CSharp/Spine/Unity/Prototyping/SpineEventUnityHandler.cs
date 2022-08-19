using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Prototyping
{
	// Token: 0x02000AD3 RID: 2771
	public class SpineEventUnityHandler : MonoBehaviour
	{
		// Token: 0x06004DB6 RID: 19894 RVA: 0x0021389C File Offset: 0x00211A9C
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

		// Token: 0x06004DB7 RID: 19895 RVA: 0x002139DC File Offset: 0x00211BDC
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

		// Token: 0x04004CD8 RID: 19672
		public List<SpineEventUnityHandler.EventPair> events = new List<SpineEventUnityHandler.EventPair>();

		// Token: 0x04004CD9 RID: 19673
		private ISkeletonComponent skeletonComponent;

		// Token: 0x04004CDA RID: 19674
		private IAnimationStateComponent animationStateComponent;

		// Token: 0x020015B6 RID: 5558
		[Serializable]
		public class EventPair
		{
			// Token: 0x04007034 RID: 28724
			[SpineEvent("", "", true, false, false)]
			public string spineEvent;

			// Token: 0x04007035 RID: 28725
			public UnityEvent unityHandler;

			// Token: 0x04007036 RID: 28726
			public AnimationState.TrackEntryEventDelegate eventDelegate;
		}
	}
}
