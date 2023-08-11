using UnityEngine;

public class CollectKey : MonoBehaviour
{
	private ManageFull manage;

	private Animator anim;

	private void Start()
	{
		manage = GameObject.Find("_GameManager").GetComponent<ManageFull>();
		anim = ((Component)this).GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (((Component)col).tag == "Monkey")
		{
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
			anim.Play("CollectKey");
			((MonoBehaviour)this).Invoke("NotifyManager", 0.25f);
		}
	}

	private void NotifyManager()
	{
		GameObject.Find("_GameManager").SendMessage("KeyCollected");
	}
}
