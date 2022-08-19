using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class BossScript : MonoBehaviour
{
	// Token: 0x17000293 RID: 659
	// (get) Token: 0x0600250A RID: 9482 RVA: 0x00101871 File Offset: 0x000FFA71
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

	// Token: 0x0600250B RID: 9483 RVA: 0x0010189E File Offset: 0x000FFA9E
	private void Awake()
	{
		BossScript.instance = this;
		this.anim = base.transform.Find("Boss 1").GetComponent<Animator>();
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x001018C1 File Offset: 0x000FFAC1
	public void comeIntoTheWorld()
	{
		base.StartCoroutine(this.ShowUp());
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x001018D0 File Offset: 0x000FFAD0
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

	// Token: 0x0600250E RID: 9486 RVA: 0x001018DF File Offset: 0x000FFADF
	private void goPlayer()
	{
		MonkeyController2D.Instance.state = MonkeyController2D.State.running;
		MonkeyController2D.Instance.majmun.GetComponent<Animator>().SetBool("Run", true);
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.run = true;
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x0010191C File Offset: 0x000FFB1C
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

	// Token: 0x04001DD8 RID: 7640
	private static BossScript instance;

	// Token: 0x04001DD9 RID: 7641
	private Animator anim;

	// Token: 0x04001DDA RID: 7642
	private bool run;

	// Token: 0x04001DDB RID: 7643
	public float maxSpeedX = 16f;

	// Token: 0x04001DDC RID: 7644
	private float moveForce = 500f;

	// Token: 0x04001DDD RID: 7645
	private bool stop;
}
