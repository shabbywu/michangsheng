using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004CB RID: 1227
public class MushroomDogadjaji : MonoBehaviour
{
	// Token: 0x060027A2 RID: 10146 RVA: 0x00128B5B File Offset: 0x00126D5B
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
		this.playerController = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x00128B80 File Offset: 0x00126D80
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

	// Token: 0x060027A4 RID: 10148 RVA: 0x00128CC5 File Offset: 0x00126EC5
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

	// Token: 0x060027A5 RID: 10149 RVA: 0x00128CD4 File Offset: 0x00126ED4
	private void UkljuciColliderOpet()
	{
		base.GetComponent<Collider2D>().enabled = true;
	}

	// Token: 0x04002283 RID: 8835
	private Animator anim;

	// Token: 0x04002284 RID: 8836
	private MonkeyController2D playerController;

	// Token: 0x04002285 RID: 8837
	public MushroomDogadjaji.Tip tip;

	// Token: 0x04002286 RID: 8838
	private int brojac;

	// Token: 0x0200144E RID: 5198
	public enum Tip
	{
		// Token: 0x04006B8A RID: 27530
		Feder,
		// Token: 0x04006B8B RID: 27531
		Bunika
	}
}
