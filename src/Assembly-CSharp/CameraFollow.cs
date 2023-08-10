using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject cameraTarget;

	public GameObject player;

	public float smoothTime = 0.1f;

	public bool cameraFollowX = true;

	public bool cameraFollowY = true;

	public bool cameraFollowHeight;

	public float cameraHeight = 2.5f;

	public Vector2 velocity;

	private Transform thisTransform;

	public bool changeHeight;

	private MonkeyController2D monkeyControll;

	private void Start()
	{
		thisTransform = ((Component)this).transform;
		monkeyControll = GameObject.Find("Player").GetComponent<MonkeyController2D>();
	}

	private void Update()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		if (cameraFollowX)
		{
			float num = Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
			thisTransform.position = new Vector3(num, thisTransform.position.y, thisTransform.position.z);
		}
		if (cameraFollowY)
		{
			float num2 = Mathf.SmoothDamp(thisTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime);
			thisTransform.position = new Vector3(thisTransform.position.x, num2, thisTransform.position.z);
		}
		if (!cameraFollowY && cameraFollowHeight)
		{
			((Component)Camera.main).transform.position = new Vector3(((Component)Camera.main).transform.position.x, cameraHeight, ((Component)Camera.main).transform.position.z);
		}
		if (changeHeight)
		{
			((MonoBehaviour)this).StartCoroutine(catchCameraY());
		}
		_ = changeHeight;
	}

	private IEnumerator catchCameraY()
	{
		for (float i = 0f; i < 1f; i += 0.001f)
		{
			Debug.Log((object)("Usao u korutinu: " + i));
			yield return null;
			if (!changeHeight)
			{
				break;
			}
			thisTransform.position = new Vector3(thisTransform.position.x, Mathf.MoveTowards(thisTransform.position.y, cameraTarget.transform.position.y, i), thisTransform.position.z);
		}
		changeHeight = false;
	}
}
