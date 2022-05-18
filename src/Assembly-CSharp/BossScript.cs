using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200066E RID: 1646
public class BossScript : MonoBehaviour
{
	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06002920 RID: 10528 RVA: 0x0001FF88 File Offset: 0x0001E188
	public static BossScript Instance
	{
		get
		{
			if (BossScript.instance != null)
			{
				BossScript.instance = (Object.FindObjectOfType(typeof(BossScript)) as BossScript);
			}
			return BossScript.instance;
		}
	}

	// Token: 0x06002921 RID: 10529 RVA: 0x0001FFB5 File Offset: 0x0001E1B5
	private void Awake()
	{
		BossScript.instance = this;
		this.anim = base.transform.Find("Boss 1").GetComponent<Animator>();
	}

	// Token: 0x06002922 RID: 10530 RVA: 0x0001FFD8 File Offset: 0x0001E1D8
	public void comeIntoTheWorld()
	{
		base.StartCoroutine(this.ShowUp());
	}

	// Token: 0x06002923 RID: 10531 RVA: 0x0001FFE7 File Offset: 0x0001E1E7
	private IEnumerator ShowUp()
	{
		float t = 0.05f;
		while (base.transform.Find("Boss 1").localPosition != new Vector3(0f, -1f, 0f))
		{
			base.transform.Find("Boss 1").localPosition = Vector3.Lerp(base.transform.Find("Boss 1").localPosition, new Vector3(0f, -1f, 0f), t);
			yield return null;
			Debug.Log("AASASDAD: " + t);
		}
		this.anim.SetTrigger("Stomp");
		base.Invoke("goPlayer", 2f);
		yield break;
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x0001FFF6 File Offset: 0x0001E1F6
	private void goPlayer()
	{
		MonkeyController2D.Instance.state = MonkeyController2D.State.running;
		MonkeyController2D.Instance.majmun.GetComponent<Animator>().SetBool("Run", true);
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.run = true;
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x00141460 File Offset: 0x0013F660
	private void FixedUpdate()
	{
		if (this.run)
		{
			if (base.GetComponent<Rigidbody2D>().velocity.x < this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.moveForce);
			}
			if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}

	// Token: 0x040022E6 RID: 8934
	private static BossScript instance;

	// Token: 0x040022E7 RID: 8935
	private Animator anim;

	// Token: 0x040022E8 RID: 8936
	private bool run;

	// Token: 0x040022E9 RID: 8937
	public float maxSpeedX = 16f;

	// Token: 0x040022EA RID: 8938
	private float moveForce = 500f;

	// Token: 0x040022EB RID: 8939
	private bool stop;
}
