using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
	private static BossScript instance;

	private Animator anim;

	private bool run;

	public float maxSpeedX = 16f;

	private float moveForce = 500f;

	private bool stop;

	public static BossScript Instance
	{
		get
		{
			if ((Object)(object)instance != (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(BossScript)) as BossScript;
			}
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		anim = ((Component)((Component)this).transform.Find("Boss 1")).GetComponent<Animator>();
	}

	public void comeIntoTheWorld()
	{
		((MonoBehaviour)this).StartCoroutine(ShowUp());
	}

	private IEnumerator ShowUp()
	{
		float t = 0.05f;
		while (((Component)this).transform.Find("Boss 1").localPosition != new Vector3(0f, -1f, 0f))
		{
			((Component)this).transform.Find("Boss 1").localPosition = Vector3.Lerp(((Component)this).transform.Find("Boss 1").localPosition, new Vector3(0f, -1f, 0f), t);
			yield return null;
			Debug.Log((object)("AASASDAD: " + t));
		}
		anim.SetTrigger("Stomp");
		((MonoBehaviour)this).Invoke("goPlayer", 2f);
	}

	private void goPlayer()
	{
		MonkeyController2D.Instance.state = MonkeyController2D.State.running;
		((Component)MonkeyController2D.Instance.majmun).GetComponent<Animator>().SetBool("Run", true);
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		run = true;
	}

	private void FixedUpdate()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (run)
		{
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.x < maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveForce);
			}
			if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.x) > maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}
}
