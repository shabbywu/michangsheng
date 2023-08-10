using UnityEngine;

public class BabunAnimationEvents : MonoBehaviour
{
	private Animator anim;

	private Transform babun;

	private void Awake()
	{
		babun = ((Component)this).transform.GetChild(0);
		anim = ((Component)babun).GetComponent<Animator>();
	}

	private void startPatrolRight()
	{
		anim.SetBool("changeSide", true);
	}

	private void startPatrolLeft()
	{
		anim.SetBool("changeSide", false);
	}

	private void landBaboon()
	{
		anim.SetBool("Land", true);
	}

	private void startJumpBaboon()
	{
		anim.Play("Jump");
		anim.SetBool("Land", false);
	}
}
