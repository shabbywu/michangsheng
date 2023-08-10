using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spine.Unity.Examples;

public class AttackSpineboy : MonoBehaviour
{
	public SkeletonAnimation spineboy;

	public SkeletonAnimation attackerSpineboy;

	public SpineGauge gauge;

	public Text healthText;

	private int currentHealth = 100;

	private const int maxHealth = 100;

	public AnimationReferenceAsset shoot;

	public AnimationReferenceAsset hit;

	public AnimationReferenceAsset idle;

	public AnimationReferenceAsset death;

	public UnityEvent onAttack;

	private void Update()
	{
		if (Input.GetKeyDown((KeyCode)32))
		{
			currentHealth -= 10;
			healthText.text = currentHealth + "/" + 100;
			attackerSpineboy.AnimationState.SetAnimation(1, AnimationReferenceAsset.op_Implicit(shoot), false);
			attackerSpineboy.AnimationState.AddEmptyAnimation(1, 0.5f, 2f);
			if (currentHealth > 0)
			{
				spineboy.AnimationState.SetAnimation(0, AnimationReferenceAsset.op_Implicit(hit), false);
				spineboy.AnimationState.AddAnimation(0, AnimationReferenceAsset.op_Implicit(idle), true, 0f);
				gauge.fillPercent = (float)currentHealth / 100f;
				onAttack.Invoke();
			}
			else if (currentHealth >= 0)
			{
				gauge.fillPercent = 0f;
				spineboy.AnimationState.SetAnimation(0, AnimationReferenceAsset.op_Implicit(death), false).TrackEnd = float.PositiveInfinity;
			}
		}
	}
}
