using UnityEngine;

public class RotateLogo : MonoBehaviour
{
	public static bool animationDone;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("PlaySound", 0.15f);
	}

	private void OnTriggerEnter(Collider col)
	{
		((Component)this).GetComponent<Collider>().enabled = false;
		((Component)this).GetComponent<Animator>().Play("RotateLogo_v2");
	}

	private void AnimationDone()
	{
		animationDone = true;
	}

	private void PlaySound()
	{
		((Component)((Component)this).transform.GetChild(0)).GetComponent<AudioSource>().Play();
	}
}
