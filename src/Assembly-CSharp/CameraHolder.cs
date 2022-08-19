using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class CameraHolder : MonoBehaviour
{
	// Token: 0x0600004A RID: 74 RVA: 0x00003524 File Offset: 0x00001724
	private void Start()
	{
		if (Screen.dpi < 1f)
		{
			this.windowDpi = 1f;
		}
		if (Screen.dpi < 200f)
		{
			this.windowDpi = 1f;
		}
		else
		{
			this.windowDpi = Screen.dpi / 200f;
		}
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		this.Counter(0);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000035A0 File Offset: 0x000017A0
	private void OnGUI()
	{
		if (GUI.Button(new Rect(5f * this.windowDpi, 5f * this.windowDpi, 110f * this.windowDpi, 35f * this.windowDpi), "Previous effect"))
		{
			this.Counter(-1);
		}
		if (GUI.Button(new Rect(120f * this.windowDpi, 5f * this.windowDpi, 110f * this.windowDpi, 35f * this.windowDpi), "Play again"))
		{
			this.Counter(0);
		}
		if (GUI.Button(new Rect(235f * this.windowDpi, 5f * this.windowDpi, 110f * this.windowDpi, 35f * this.windowDpi), "Next effect"))
		{
			this.Counter(1);
		}
		this.StartColor = this.HueColor;
		this.HueColor = GUI.HorizontalSlider(new Rect(5f * this.windowDpi, 45f * this.windowDpi, 340f * this.windowDpi, 35f * this.windowDpi), this.HueColor, 0f, 1f);
		GUI.DrawTexture(new Rect(5f * this.windowDpi, 65f * this.windowDpi, 340f * this.windowDpi, 15f * this.windowDpi), this.HueTexture, 0, false, 0f);
		if (this.HueColor != this.StartColor)
		{
			int num = 0;
			ParticleSystem[] array = this.particleSystems;
			for (int i = 0; i < array.Length; i++)
			{
				ParticleSystem.MainModule main = array[i].main;
				Color color = Color.HSVToRGB(this.HueColor + this.H * 0f, this.svList[num].S, this.svList[num].V);
				main.startColor = new Color(color.r, color.g, color.b, this.svList[num].A);
				num++;
			}
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000037D8 File Offset: 0x000019D8
	private void Counter(int count)
	{
		this.Prefab += count;
		if (this.Prefab > this.Prefabs.Length - 1)
		{
			this.Prefab = 0;
		}
		else if (this.Prefab < 0)
		{
			this.Prefab = this.Prefabs.Length - 1;
		}
		if (this.Instance != null)
		{
			Object.Destroy(this.Instance);
		}
		this.Instance = Object.Instantiate<GameObject>(this.Prefabs[this.Prefab]);
		this.particleSystems = this.Instance.GetComponentsInChildren<ParticleSystem>();
		this.svList.Clear();
		ParticleSystem[] array = this.particleSystems;
		for (int i = 0; i < array.Length; i++)
		{
			Color color = array[i].main.startColor.color;
			CameraHolder.SVA item = default(CameraHolder.SVA);
			Color.RGBToHSV(color, ref this.H, ref item.S, ref item.V);
			item.A = color.a;
			this.svList.Add(item);
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000038E0 File Offset: 0x00001AE0
	private void LateUpdate()
	{
		if (this.currDistance < 2f)
		{
			this.currDistance = 2f;
		}
		this.currDistance -= Input.GetAxis("Mouse ScrollWheel") * 2f;
		if (this.Holder && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
		{
			Vector3 mousePosition = Input.mousePosition;
			if (Screen.dpi < 1f)
			{
			}
			float num;
			if (Screen.dpi < 200f)
			{
				num = 1f;
			}
			else
			{
				num = Screen.dpi / 200f;
			}
			if (mousePosition.x < 380f * num && (float)Screen.height - mousePosition.y < 250f * num)
			{
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = 1;
			this.x += (float)((double)(Input.GetAxis("Mouse X") * this.xRotate) * 0.02);
			this.y -= (float)((double)(Input.GetAxis("Mouse Y") * this.yRotate) * 0.02);
			this.y = CameraHolder.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
			Quaternion quaternion = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position = quaternion * new Vector3(0f, 0f, -this.currDistance) + this.Holder.position;
			base.transform.rotation = quaternion;
			base.transform.position = position;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = 0;
		}
		if (this.prevDistance != this.currDistance)
		{
			this.prevDistance = this.currDistance;
			Quaternion quaternion2 = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position2 = quaternion2 * new Vector3(0f, 0f, -this.currDistance) + this.Holder.position;
			base.transform.rotation = quaternion2;
			base.transform.position = position2;
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003117 File Offset: 0x00001317
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x04000040 RID: 64
	public Transform Holder;

	// Token: 0x04000041 RID: 65
	public float currDistance = 5f;

	// Token: 0x04000042 RID: 66
	public float xRotate = 250f;

	// Token: 0x04000043 RID: 67
	public float yRotate = 120f;

	// Token: 0x04000044 RID: 68
	public float yMinLimit = -20f;

	// Token: 0x04000045 RID: 69
	public float yMaxLimit = 80f;

	// Token: 0x04000046 RID: 70
	public float prevDistance;

	// Token: 0x04000047 RID: 71
	private float x;

	// Token: 0x04000048 RID: 72
	private float y;

	// Token: 0x04000049 RID: 73
	[Header("GUI")]
	private float windowDpi;

	// Token: 0x0400004A RID: 74
	public GameObject[] Prefabs;

	// Token: 0x0400004B RID: 75
	private int Prefab;

	// Token: 0x0400004C RID: 76
	private GameObject Instance;

	// Token: 0x0400004D RID: 77
	private float StartColor;

	// Token: 0x0400004E RID: 78
	private float HueColor;

	// Token: 0x0400004F RID: 79
	public Texture HueTexture;

	// Token: 0x04000050 RID: 80
	private ParticleSystem[] particleSystems = new ParticleSystem[0];

	// Token: 0x04000051 RID: 81
	private List<CameraHolder.SVA> svList = new List<CameraHolder.SVA>();

	// Token: 0x04000052 RID: 82
	private float H;

	// Token: 0x020011C5 RID: 4549
	public struct SVA
	{
		// Token: 0x04006352 RID: 25426
		public float S;

		// Token: 0x04006353 RID: 25427
		public float V;

		// Token: 0x04006354 RID: 25428
		public float A;
	}
}
