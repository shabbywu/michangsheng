using System.Collections;
using UnityEngine;

public class RunWithSpeed_jednakamera : MonoBehaviour
{
	public float speed = 5f;

	public bool continueMoving;

	private MonkeyController2D playerController;

	private GameObject player;

	private float offset;

	public bool FollowCameraHeight;

	public bool IskljuciKadIzadjeIzKadra;

	public bool smooth;

	private bool smoothMove;

	private float startSpeed;

	public Transform desnaGranica;

	private Camera bgCamera;

	private float bgCameraX;

	private bool dovoljno;

	private void Awake()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		player = GameObject.FindGameObjectWithTag("Monkey");
		playerController = player.GetComponent<MonkeyController2D>();
		bgCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		bgCameraX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
		desnaGranica = ((Component)this).transform.Find("DesnaGranica");
		offset = ((Component)this).transform.position.y - ((Component)Camera.main).transform.position.y;
		startSpeed = speed;
	}

	private void Start()
	{
	}

	private void Update()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)desnaGranica != (Object)null)
		{
			bgCameraX = ((Component)bgCamera).transform.position.x;
			if (desnaGranica.position.x < bgCameraX - bgCamera.orthographicSize * bgCamera.aspect)
			{
				((Component)this).transform.position = new Vector3(bgCameraX + bgCamera.orthographicSize * bgCamera.aspect, ((Component)this).transform.position.y, ((Component)this).transform.position.z);
			}
		}
		if (((playerController.state == MonkeyController2D.State.running || playerController.state == MonkeyController2D.State.jumped) && ((Component)playerController).GetComponent<Rigidbody2D>().velocity.x > 0.05f) || continueMoving)
		{
			if (smooth)
			{
				smoothMove = true;
			}
			if (speed != startSpeed)
			{
				speed = startSpeed;
			}
			if (!dovoljno)
			{
				((MonoBehaviour)this).Invoke("startSpeedDaj", 0.15f);
			}
			((MonoBehaviour)this).StopCoroutine("SmoothMovePlan");
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, new Vector3(((Component)this).transform.position.x - speed, ((Component)this).transform.position.y, ((Component)this).transform.position.z), 5f * Time.deltaTime);
		}
		if (smoothMove && (playerController.state == MonkeyController2D.State.wallhit || playerController.state == MonkeyController2D.State.climbUp))
		{
			smoothMove = false;
			((MonoBehaviour)this).StartCoroutine("SmoothMovePlan");
		}
		if (FollowCameraHeight)
		{
			((Component)this).transform.position = new Vector3(((Component)this).transform.position.x, ((Component)Camera.main).transform.position.y + offset, ((Component)this).transform.position.z);
		}
		if (IskljuciKadIzadjeIzKadra && ((Component)this).transform.position.x + 25f < Camera.main.ViewportToWorldPoint(Vector3.zero).x)
		{
			((Component)this).gameObject.SetActive(false);
		}
	}

	private IEnumerator SmoothMovePlan()
	{
		float targetPos = ((Component)this).transform.position.x - 5f;
		for (float t = 0f; t < 1f; t += Time.deltaTime / 10f)
		{
			yield return null;
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, new Vector3(targetPos, ((Component)this).transform.position.y, ((Component)this).transform.position.z), t);
		}
	}

	private void izracunajOffset()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		offset = ((Component)this).transform.position.y - ((Component)Camera.main).transform.position.y;
		startSpeed = speed;
	}

	private void startSpeedDaj()
	{
		dovoljno = true;
	}
}
