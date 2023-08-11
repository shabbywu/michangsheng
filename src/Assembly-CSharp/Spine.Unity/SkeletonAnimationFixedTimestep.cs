using UnityEngine;

namespace Spine.Unity;

[DisallowMultipleComponent]
public sealed class SkeletonAnimationFixedTimestep : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	[Tooltip("The duration of each frame in seconds. For 12 fps: enter '1/12' in the Unity inspector.")]
	public float frameDeltaTime = 1f / 15f;

	[Header("Advanced")]
	[Tooltip("The maximum number of fixed timesteps. If the game framerate drops below the If the framerate is consistently faster than the limited frames, this does nothing.")]
	public int maxFrameSkip = 4;

	[Tooltip("If enabled, the Skeleton mesh will be updated only on the same frame when the animation and skeleton are updated. Disable this or call SkeletonAnimation.LateUpdate yourself if you are modifying the Skeleton using other components that don't run in the same fixed timestep.")]
	public bool frameskipMeshUpdate = true;

	[Tooltip("This is the amount the internal accumulator starts with. Set it to some fraction of your frame delta time if you want to stagger updates between multiple skeletons.")]
	public float timeOffset;

	private float accumulatedTime;

	private bool requiresNewMesh;

	private void OnValidate()
	{
		skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		if (frameDeltaTime <= 0f)
		{
			frameDeltaTime = 1f / 60f;
		}
		if (maxFrameSkip < 1)
		{
			maxFrameSkip = 1;
		}
	}

	private void Awake()
	{
		requiresNewMesh = true;
		accumulatedTime = timeOffset;
	}

	private void Update()
	{
		if (((Behaviour)skeletonAnimation).enabled)
		{
			((Behaviour)skeletonAnimation).enabled = false;
		}
		accumulatedTime += Time.deltaTime;
		float num = 0f;
		while (accumulatedTime >= frameDeltaTime)
		{
			num += 1f;
			if (num > (float)maxFrameSkip)
			{
				break;
			}
			accumulatedTime -= frameDeltaTime;
		}
		if (num > 0f)
		{
			skeletonAnimation.Update(num * frameDeltaTime);
			requiresNewMesh = true;
		}
	}

	private void LateUpdate()
	{
		if (!frameskipMeshUpdate || requiresNewMesh)
		{
			((SkeletonRenderer)skeletonAnimation).LateUpdate();
			requiresNewMesh = false;
		}
	}
}
