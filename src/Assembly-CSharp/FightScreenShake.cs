using DG.Tweening;
using UnityEngine;

public class FightScreenShake : MonoBehaviour
{
	public Vector3 ShakePosition = new Vector3(0.6f, 0.3f, 0f);

	public Vector3 ShakeRotation = new Vector3(1f, 0.7f, 0f);

	private Vector3 normalPosition;

	private Quaternion normalRotation;

	private void Start()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		normalPosition = ((Component)this).transform.position;
		normalRotation = ((Component)this).transform.rotation;
	}

	public void Shake()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		((Tween)ShortcutExtensions.DOShakePosition(((Component)this).transform, 0.3f, ShakePosition, 10, 90f, false, true)).onComplete = (TweenCallback)delegate
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			((Component)this).transform.position = normalPosition;
		};
		((Tween)ShortcutExtensions.DOShakeRotation(((Component)this).transform, 0.3f, ShakeRotation, 10, 90f, true)).onComplete = (TweenCallback)delegate
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			((Component)this).transform.rotation = normalRotation;
		};
	}
}
