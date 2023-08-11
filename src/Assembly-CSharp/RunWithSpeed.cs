using System.Collections;
using UnityEngine;

public class RunWithSpeed : MonoBehaviour
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

	private void Awake()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		player = GameObject.FindGameObjectWithTag("Monkey");
		playerController = player.GetComponent<MonkeyController2D>();
		bgCamera = GameObject.FindGameObjectWithTag("bgCamera").GetComponent<Camera>();
		bgCameraX = GameObject.FindGameObjectWithTag("bgCamera").transform.position.x;
	}

	private void Start()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		offset = ((Component)this).transform.position.y - ((Component)Camera.main).transform.position.y;
		startSpeed = speed;
	}

	private void Update()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)desnaGranica != (Object)null && desnaGranica.position.x < bgCameraX - bgCamera.orthographicSize * bgCamera.aspect)
		{
			((Component)this).transform.position = new Vector3(desnaGranica.position.x + desnaGranica.localPosition.x, ((Component)this).transform.position.y, ((Component)this).transform.position.z);
		}
		if (((playerController.state == MonkeyController2D.State.running || playerController.state == MonkeyController2D.State.jumped) && ((Component)playerController).GetComponent<Rigidbody2D>().velocity.x > 0.05f && !playerController.wallHitGlide) || continueMoving)
		{
			if (smooth)
			{
				smoothMove = true;
			}
			if (speed != startSpeed)
			{
				speed = startSpeed;
			}
			((MonoBehaviour)this).StopCoroutine("SmoothMovePlan");
			((Component)this).transform.Translate(Vector3.right * speed * Time.deltaTime, (Space)0);
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
}
