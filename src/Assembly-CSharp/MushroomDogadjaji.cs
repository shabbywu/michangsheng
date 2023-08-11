using System.Collections;
using UnityEngine;

public class MushroomDogadjaji : MonoBehaviour
{
	public enum Tip
	{
		Feder,
		Bunika
	}

	private Animator anim;

	private MonkeyController2D playerController;

	public Tip tip;

	private int brojac;

	private void Awake()
	{
		anim = ((Component)this).GetComponent<Animator>();
		playerController = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (!(((Component)col).tag == "Monkey"))
		{
			return;
		}
		if (playerController.state == MonkeyController2D.State.jumped)
		{
			if (tip == Tip.Feder)
			{
				brojac = 0;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_MushroomBounce();
				}
				((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
				((MonoBehaviour)this).StartCoroutine(DelayAndBounce());
			}
			else if (tip == Tip.Bunika)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_MushroomBounce();
				}
				((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
				anim.Play("Blong");
				((Component)playerController).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				((Component)playerController).GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 1000f));
				((Component)Camera.main).GetComponent<Animator>().Play("CameraMovePoisonMushroom");
				GameObject.Find("Background").GetComponent<Renderer>().material.color = new Color(0.82f, 0.07f, 0.75f, 1f);
			}
		}
		else if (playerController.state == MonkeyController2D.State.running)
		{
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
		}
		((MonoBehaviour)this).Invoke("UkljuciColliderOpet", 1f);
	}

	private IEnumerator DelayAndBounce()
	{
		((Component)playerController).transform.position = new Vector3(((Component)this).transform.Find("Goal").position.x, ((Component)this).transform.Find("Goal").position.y, ((Component)playerController).transform.position.z);
		((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = true;
		anim.Play("Blong");
		playerController.state = MonkeyController2D.State.jumped;
		Transform goal = ((Component)this).transform.Find("BounceObj");
		((Component)goal).GetComponent<Animation>().Play("MonkeyBounceFromMushroom");
		playerController.mushroomJumped = true;
		((Component)playerController).GetComponent<Rigidbody2D>().drag = 0f;
		playerController.animator.Play(playerController.fall_State);
		while (((Component)goal).GetComponent<Animation>().IsPlaying("MonkeyBounceFromMushroom"))
		{
			if (!playerController.mushroomJumped)
			{
				((Component)goal).GetComponent<Animation>().Stop();
			}
			yield return null;
			((Component)playerController).transform.position = new Vector3(goal.position.x, goal.position.y, ((Component)playerController).transform.position.z);
		}
	}

	private void UkljuciColliderOpet()
	{
		((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = true;
	}
}
