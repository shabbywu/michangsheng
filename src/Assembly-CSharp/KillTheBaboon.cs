using UnityEngine;

public class KillTheBaboon : MonoBehaviour
{
	private Transform babun;

	private BoxCollider2D[] boxColliders;

	private BabunDogadjaji_new babunScript;

	private bool collidersTurnedOff;

	private void Awake()
	{
		babun = ((Component)this).transform.parent.Find("_MajmunceNadrlja");
		boxColliders = ((Component)this).GetComponents<BoxCollider2D>();
		babunScript = ((Component)babun).GetComponent<BabunDogadjaji_new>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Monkey")
		{
			turnOffColliders();
			babunScript.killBaboonStuff();
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (((Component)col).gameObject.tag == "Monkey")
		{
			turnOffColliders();
			babunScript.killBaboonStuff();
		}
	}

	public void turnOffColliders()
	{
		((Behaviour)boxColliders[0]).enabled = false;
		collidersTurnedOff = true;
	}

	public void turnOnColliders()
	{
		if (collidersTurnedOff)
		{
			((Behaviour)boxColliders[0]).enabled = true;
			collidersTurnedOff = false;
		}
	}

	public void DestoyEnemy()
	{
		turnOffColliders();
		babunScript.killBaboonStuff();
	}
}
