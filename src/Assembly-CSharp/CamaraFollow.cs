using System;
using System.Collections.Generic;
using JiaoYi;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
	public static CamaraFollow Inst;

	private GameObject player;

	public Transform levo;

	public Transform desno;

	private Camera maincamera;

	private Vector3 firstMousePositon;

	private Vector3 firstCameraPositon;

	public bool follwPlayer;

	public static bool CanMove = true;

	private Dictionary<string, Func<bool>> BanMoveFunc = new Dictionary<string, Func<bool>>();

	private static float orthographicSize = 8f;

	public void Start()
	{
		Inst = this;
		maincamera = ((Component)this).GetComponent<Camera>();
		player = ((Component)AllMapManage.instance.MapPlayerController).gameObject;
		((MonoBehaviour)this).Invoke("SetCameraToPlayer", 0.3f);
		RegisterBanMove();
	}

	public void RegisterBanMove()
	{
		BanMoveFunc["UINPCLeftList"] = () => (Object)(object)UINPCLeftList.Inst != (Object)null && UINPCLeftList.Inst.IsMouseInUI;
		BanMoveFunc["UINPCJiaoHu"] = () => UINPCJiaoHu.Inst.NowIsJiaoHu;
		BanMoveFunc["JiaoYiUIMag"] = () => (Object)(object)JiaoYiUIMag.Inst != (Object)null;
	}

	public void SetCameraToPlayer()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.position = new Vector3(player.transform.position.x, player.transform.position.y, ((Component)this).transform.position.z);
	}

	private void Update()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		CanMove = true;
		foreach (KeyValuePair<string, Func<bool>> item in BanMoveFunc)
		{
			if (item.Value != null && item.Value())
			{
				CanMove = false;
				break;
			}
		}
		if (!CanMove)
		{
			return;
		}
		if (Input.GetKeyUp((KeyCode)32) || follwPlayer)
		{
			SetCameraToPlayer();
		}
		float num = levo.position.x + maincamera.orthographicSize * maincamera.aspect;
		float num2 = desno.position.x - maincamera.orthographicSize * maincamera.aspect;
		float num3 = levo.position.y + maincamera.orthographicSize;
		float num4 = desno.position.y - maincamera.orthographicSize;
		if (Tools.instance.canClick())
		{
			if (SceneEx.NowSceneName.StartsWith("Sea"))
			{
				if (Input.GetAxis("Mouse ScrollWheel") > 0f && maincamera.orthographicSize > 1f)
				{
					orthographicSize /= 1.03f;
				}
				if (Input.GetAxis("Mouse ScrollWheel") < 0f && maincamera.orthographicSize < 12f && maincamera.orthographicSize < num2 - num - 1f)
				{
					orthographicSize *= 1.03f;
				}
				maincamera.orthographicSize = orthographicSize;
			}
			else
			{
				if (Input.GetAxis("Mouse ScrollWheel") > 0f && maincamera.orthographicSize > 1f)
				{
					Camera obj = maincamera;
					obj.orthographicSize /= 1.03f;
				}
				if (Input.GetAxis("Mouse ScrollWheel") < 0f && maincamera.orthographicSize < 12f && maincamera.orthographicSize < num2 - num - 1f)
				{
					Camera obj2 = maincamera;
					obj2.orthographicSize *= 1.03f;
				}
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			firstMousePositon = Input.mousePosition;
			firstCameraPositon = ((Component)maincamera).transform.position;
		}
		if (Tools.instance.canClick() && Input.GetMouseButton(0))
		{
			float num5 = (Input.mousePosition.x - firstMousePositon.x) / (float)Screen.width * maincamera.aspect * maincamera.orthographicSize * 2f;
			float num6 = (Input.mousePosition.y - firstMousePositon.y) / (float)Screen.height * maincamera.orthographicSize * 2f;
			float num7 = firstCameraPositon.x - num5;
			float num8 = firstCameraPositon.y - num6;
			((Component)maincamera).transform.position = new Vector3(num7, num8, ((Component)maincamera).transform.position.z);
		}
		float num9 = Mathf.Clamp(((Component)maincamera).transform.position.x, num, num2);
		float num10 = Mathf.Clamp(((Component)maincamera).transform.position.y, num3, num4);
		((Component)maincamera).transform.position = new Vector3(num9, num10, ((Component)maincamera).transform.position.z);
	}

	public Vector2 LimitPos(Vector2 targetPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		float num = levo.position.x + maincamera.orthographicSize * maincamera.aspect;
		float num2 = desno.position.x - maincamera.orthographicSize * maincamera.aspect;
		float num3 = levo.position.y + maincamera.orthographicSize;
		float num4 = desno.position.y - maincamera.orthographicSize;
		float num5 = Mathf.Clamp(targetPos.x, num, num2);
		float num6 = Mathf.Clamp(targetPos.y, num3, num4);
		return new Vector2(num5, num6);
	}
}
