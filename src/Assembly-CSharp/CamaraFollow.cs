using System;
using System.Collections.Generic;
using JiaoYi;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class CamaraFollow : MonoBehaviour
{
	// Token: 0x0600108E RID: 4238 RVA: 0x000615C0 File Offset: 0x0005F7C0
	public void Start()
	{
		CamaraFollow.Inst = this;
		this.maincamera = base.GetComponent<Camera>();
		this.player = AllMapManage.instance.MapPlayerController.gameObject;
		base.Invoke("SetCameraToPlayer", 0.3f);
		this.RegisterBanMove();
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x00061600 File Offset: 0x0005F800
	public void RegisterBanMove()
	{
		this.BanMoveFunc["UINPCLeftList"] = (() => UINPCLeftList.Inst != null && UINPCLeftList.Inst.IsMouseInUI);
		this.BanMoveFunc["UINPCJiaoHu"] = (() => UINPCJiaoHu.Inst.NowIsJiaoHu);
		this.BanMoveFunc["JiaoYiUIMag"] = (() => JiaoYiUIMag.Inst != null);
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0006169C File Offset: 0x0005F89C
	public void SetCameraToPlayer()
	{
		base.transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y, base.transform.position.z);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000616F4 File Offset: 0x0005F8F4
	private void Update()
	{
		CamaraFollow.CanMove = true;
		foreach (KeyValuePair<string, Func<bool>> keyValuePair in this.BanMoveFunc)
		{
			if (keyValuePair.Value != null && keyValuePair.Value())
			{
				CamaraFollow.CanMove = false;
				break;
			}
		}
		if (CamaraFollow.CanMove)
		{
			if (Input.GetKeyUp(32) || this.follwPlayer)
			{
				this.SetCameraToPlayer();
			}
			float num = this.levo.position.x + this.maincamera.orthographicSize * this.maincamera.aspect;
			float num2 = this.desno.position.x - this.maincamera.orthographicSize * this.maincamera.aspect;
			float num3 = this.levo.position.y + this.maincamera.orthographicSize;
			float num4 = this.desno.position.y - this.maincamera.orthographicSize;
			if (Tools.instance.canClick(false, true))
			{
				if (SceneEx.NowSceneName.StartsWith("Sea"))
				{
					if (Input.GetAxis("Mouse ScrollWheel") > 0f && this.maincamera.orthographicSize > 1f)
					{
						CamaraFollow.orthographicSize /= 1.03f;
					}
					if (Input.GetAxis("Mouse ScrollWheel") < 0f && this.maincamera.orthographicSize < 12f && this.maincamera.orthographicSize < num2 - num - 1f)
					{
						CamaraFollow.orthographicSize *= 1.03f;
					}
					this.maincamera.orthographicSize = CamaraFollow.orthographicSize;
				}
				else
				{
					if (Input.GetAxis("Mouse ScrollWheel") > 0f && this.maincamera.orthographicSize > 1f)
					{
						this.maincamera.orthographicSize /= 1.03f;
					}
					if (Input.GetAxis("Mouse ScrollWheel") < 0f && this.maincamera.orthographicSize < 12f && this.maincamera.orthographicSize < num2 - num - 1f)
					{
						this.maincamera.orthographicSize *= 1.03f;
					}
				}
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.firstMousePositon = Input.mousePosition;
				this.firstCameraPositon = this.maincamera.transform.position;
			}
			if (Tools.instance.canClick(false, true) && Input.GetMouseButton(0))
			{
				float num5 = (Input.mousePosition.x - this.firstMousePositon.x) / (float)Screen.width * this.maincamera.aspect * this.maincamera.orthographicSize * 2f;
				float num6 = (Input.mousePosition.y - this.firstMousePositon.y) / (float)Screen.height * this.maincamera.orthographicSize * 2f;
				float num7 = this.firstCameraPositon.x - num5;
				float num8 = this.firstCameraPositon.y - num6;
				this.maincamera.transform.position = new Vector3(num7, num8, this.maincamera.transform.position.z);
			}
			float num9 = Mathf.Clamp(this.maincamera.transform.position.x, num, num2);
			float num10 = Mathf.Clamp(this.maincamera.transform.position.y, num3, num4);
			this.maincamera.transform.position = new Vector3(num9, num10, this.maincamera.transform.position.z);
		}
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x00061AC4 File Offset: 0x0005FCC4
	public Vector2 LimitPos(Vector2 targetPos)
	{
		float num = this.levo.position.x + this.maincamera.orthographicSize * this.maincamera.aspect;
		float num2 = this.desno.position.x - this.maincamera.orthographicSize * this.maincamera.aspect;
		float num3 = this.levo.position.y + this.maincamera.orthographicSize;
		float num4 = this.desno.position.y - this.maincamera.orthographicSize;
		float num5 = Mathf.Clamp(targetPos.x, num, num2);
		float num6 = Mathf.Clamp(targetPos.y, num3, num4);
		return new Vector2(num5, num6);
	}

	// Token: 0x04000BEF RID: 3055
	public static CamaraFollow Inst;

	// Token: 0x04000BF0 RID: 3056
	private GameObject player;

	// Token: 0x04000BF1 RID: 3057
	public Transform levo;

	// Token: 0x04000BF2 RID: 3058
	public Transform desno;

	// Token: 0x04000BF3 RID: 3059
	private Camera maincamera;

	// Token: 0x04000BF4 RID: 3060
	private Vector3 firstMousePositon;

	// Token: 0x04000BF5 RID: 3061
	private Vector3 firstCameraPositon;

	// Token: 0x04000BF6 RID: 3062
	public bool follwPlayer;

	// Token: 0x04000BF7 RID: 3063
	public static bool CanMove = true;

	// Token: 0x04000BF8 RID: 3064
	private Dictionary<string, Func<bool>> BanMoveFunc = new Dictionary<string, Func<bool>>();

	// Token: 0x04000BF9 RID: 3065
	private static float orthographicSize = 8f;
}
