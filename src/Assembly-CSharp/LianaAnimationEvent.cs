using System.Collections;
using UnityEngine;

public class LianaAnimationEvent : MonoBehaviour
{
	private MonkeyController2D player;

	public Transform lijanaTarget;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	public void OtkaciMajmuna()
	{
		((MonoBehaviour)this).StartCoroutine(SacekajIOtkaciMajmuna());
	}

	private IEnumerator SacekajIOtkaciMajmuna()
	{
		yield return (object)new WaitForSeconds(0.6f);
		player.OtkaciMajmuna();
	}
}
