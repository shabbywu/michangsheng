using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200072F RID: 1839
public class MushroomDogadjaji : MonoBehaviour
{
	// Token: 0x06002EAA RID: 11946 RVA: 0x00022A86 File Offset: 0x00020C86
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
		this.playerController = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x001743E0 File Offset: 0x001725E0
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey")
		{
			if (this.playerController.state == MonkeyController2D.State.jumped)
			{
				if (this.tip == MushroomDogadjaji.Tip.Feder)
				{
					this.brojac = 0;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_MushroomBounce();
					}
					base.GetComponent<Collider2D>().enabled = false;
					base.StartCoroutine(this.DelayAndBounce());
				}
				else if (this.tip == MushroomDogadjaji.Tip.Bunika)
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_MushroomBounce();
					}
					base.GetComponent<Collider2D>().enabled = false;
					this.anim.Play("Blong");
					this.playerController.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					this.playerController.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 1000f));
					Camera.main.GetComponent<Animator>().Play("CameraMovePoisonMushroom");
					GameObject.Find("Background").GetComponent<Renderer>().material.color = new Color(0.82f, 0.07f, 0.75f, 1f);
				}
			}
			else if (this.playerController.state == MonkeyController2D.State.running)
			{
				base.GetComponent<Collider2D>().enabled = false;
			}
			base.Invoke("UkljuciColliderOpet", 1f);
		}
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x00022AA9 File Offset: 0x00020CA9
	private IEnumerator DelayAndBounce()
	{
		this.playerController.transform.position = new Vector3(base.transform.Find("Goal").position.x, base.transform.Find("Goal").position.y, this.playerController.transform.position.z);
		this.playerController.GetComponent<Rigidbody2D>().isKinematic = true;
		this.anim.Play("Blong");
		this.playerController.state = MonkeyController2D.State.jumped;
		Transform goal = base.transform.Find("BounceObj");
		goal.GetComponent<Animation>().Play("MonkeyBounceFromMushroom");
		this.playerController.mushroomJumped = true;
		this.playerController.GetComponent<Rigidbody2D>().drag = 0f;
		this.playerController.animator.Play(this.playerController.fall_State);
		while (goal.GetComponent<Animation>().IsPlaying("MonkeyBounceFromMushroom"))
		{
			if (!this.playerController.mushroomJumped)
			{
				goal.GetComponent<Animation>().Stop();
			}
			yield return null;
			this.playerController.transform.position = new Vector3(goal.position.x, goal.position.y, this.playerController.transform.position.z);
		}
		yield break;
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x00022AB8 File Offset: 0x00020CB8
	private void UkljuciColliderOpet()
	{
		base.GetComponent<Collider2D>().enabled = true;
	}

	// Token: 0x040029CA RID: 10698
	private Animator anim;

	// Token: 0x040029CB RID: 10699
	private MonkeyController2D playerController;

	// Token: 0x040029CC RID: 10700
	public MushroomDogadjaji.Tip tip;

	// Token: 0x040029CD RID: 10701
	private int brojac;

	// Token: 0x02000730 RID: 1840
	public enum Tip
	{
		// Token: 0x040029CF RID: 10703
		Feder,
		// Token: 0x040029D0 RID: 10704
		Bunika
	}
}
