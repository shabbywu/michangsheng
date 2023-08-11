using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples;

public class HeroEffectsHandlerExample : MonoBehaviour
{
	public BasicPlatformerController eventSource;

	public UnityEvent OnJump;

	public UnityEvent OnLand;

	public UnityEvent OnHardLand;

	public void Awake()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		if (!((Object)(object)eventSource == (Object)null))
		{
			eventSource.OnLand += new UnityAction(OnLand.Invoke);
			eventSource.OnJump += new UnityAction(OnJump.Invoke);
			eventSource.OnHardLand += new UnityAction(OnHardLand.Invoke);
		}
	}
}
