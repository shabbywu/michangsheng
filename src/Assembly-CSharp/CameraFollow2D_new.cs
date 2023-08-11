using UnityEngine;

public class CameraFollow2D_new : MonoBehaviour
{
	public GameObject cameraTarget;

	public GameObject player;

	public float smoothTime = 0.01f;

	public bool cameraFollowX = true;

	public bool cameraFollowY;

	public bool cameraFollowHeight;

	public float cameraHeight = 2.5f;

	public Vector3 velocity;

	private Transform thisTransform;

	public float borderY;

	public bool moveUp;

	public bool moveDown;

	public bool grounded;

	public float limitY;

	public Transform cameraBottomLimit;

	private float cameraBottomLimit_y = 2.1f;

	[HideInInspector]
	public Transform rotatingPlayer;

	[HideInInspector]
	public bool stopFollow;

	private MonkeyController2D playerController;

	[HideInInspector]
	public bool transition;

	[HideInInspector]
	public float upperLimit = 0.7f;

	private void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		cameraBottomLimit_y = cameraBottomLimit.position.y;
		thisTransform = ((Component)this).transform;
		limitY = Camera.main.ViewportToWorldPoint(Vector3.one * 0.7f).y;
		cameraTarget.transform.position = new Vector3(cameraTarget.transform.position.x, ((Component)Camera.main).transform.position.y, cameraTarget.transform.position.z);
		playerController = player.GetComponent<MonkeyController2D>();
		borderY = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, 0f)).y * 0.8f;
	}

	private void Start()
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		if (Camera.main.aspect < 1.5f)
		{
			((Component)Camera.main).transform.position = new Vector3(((Component)Camera.main).transform.position.x, 7.5f, ((Component)Camera.main).transform.position.z);
			cameraTarget.transform.position = new Vector3(cameraTarget.transform.position.x, ((Component)Camera.main).transform.position.y, cameraTarget.transform.position.z);
			borderY = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, 0f)).y * 0.75f;
		}
		else
		{
			((Component)Camera.main).transform.position = new Vector3(((Component)Camera.main).transform.position.x, 8.5f, ((Component)Camera.main).transform.position.z);
			cameraTarget.transform.position = new Vector3(cameraTarget.transform.position.x, ((Component)Camera.main).transform.position.y, cameraTarget.transform.position.z);
		}
	}

	private void Update()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		if (!stopFollow)
		{
			if (cameraFollowX)
			{
				thisTransform.position = Vector3.Lerp(thisTransform.position, new Vector3(cameraTarget.transform.position.x, thisTransform.position.y, thisTransform.position.z), 5f * Time.deltaTime);
			}
			if (cameraFollowY)
			{
				thisTransform.position = Vector3.SmoothDamp(thisTransform.position, new Vector3(thisTransform.position.x, cameraTarget.transform.position.y, thisTransform.position.z), ref velocity, smoothTime);
			}
			if (!cameraFollowY && cameraFollowHeight)
			{
				((Component)Camera.main).transform.position = new Vector3(((Component)Camera.main).transform.position.x, cameraHeight, ((Component)Camera.main).transform.position.z);
			}
			if ((playerController.state == MonkeyController2D.State.jumped && player.transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0f, (float)Screen.height * upperLimit, 0f)).y) || (playerController.state != MonkeyController2D.State.jumped && player.transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0f, (float)Screen.height * 0.25f, 0f)).y))
			{
				moveUp = true;
			}
			if (moveUp && playerController.heCanJump)
			{
				thisTransform.position = Vector3.SmoothDamp(thisTransform.position, new Vector3(thisTransform.position.x, cameraTarget.transform.position.y, thisTransform.position.z), ref velocity, smoothTime, 2000f * Time.deltaTime, Time.smoothDeltaTime);
			}
			if (playerController.state == MonkeyController2D.State.jumped && Mathf.Abs(cameraTarget.transform.position.y - ((Component)Camera.main).transform.position.y) <= 0.1f && moveUp)
			{
				moveUp = false;
			}
			if (cameraTarget.transform.position.y <= ((Component)Camera.main).transform.position.y)
			{
				moveDown = true;
			}
			if (moveDown && !transition)
			{
				thisTransform.position = new Vector3(thisTransform.position.x, cameraTarget.transform.position.y, thisTransform.position.z);
			}
			if (cameraTarget.transform.position.y >= ((Component)Camera.main).transform.position.y)
			{
				moveDown = false;
			}
		}
	}
}
