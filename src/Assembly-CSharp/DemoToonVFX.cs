using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class DemoToonVFX : MonoBehaviour
{
	// Token: 0x06000039 RID: 57 RVA: 0x00002A88 File Offset: 0x00000C88
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
		this.animObject.GetComponent<Animator>();
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002B10 File Offset: 0x00000D10
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

	// Token: 0x0600003B RID: 59 RVA: 0x00002D48 File Offset: 0x00000F48
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
		this.Instance.SetActive(false);
		if (this.activationTime.Length == this.Prefabs.Length)
		{
			base.CancelInvoke();
			if (this.activationTime[this.Prefab] > 0.01f)
			{
				base.Invoke("Activate", this.activationTime[this.Prefab]);
			}
			if (this.activationTime[this.Prefab] == 0f)
			{
				this.Instance.SetActive(true);
			}
		}
		this.particleSystems = this.Instance.GetComponentsInChildren<ParticleSystem>();
		this.svList.Clear();
		ParticleSystem[] array = this.particleSystems;
		for (int i = 0; i < array.Length; i++)
		{
			Color color = array[i].main.startColor.color;
			DemoToonVFX.SVA item = default(DemoToonVFX.SVA);
			Color.RGBToHSV(color, ref this.H, ref item.S, ref item.V);
			item.A = color.a;
			this.svList.Add(item);
		}
		if (this.useAnimation)
		{
			this.animObject.SetInteger("toDo", this.Prefab);
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002EDD File Offset: 0x000010DD
	private void Activate()
	{
		this.Instance.SetActive(true);
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002EEC File Offset: 0x000010EC
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
			this.y = DemoToonVFX.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
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

	// Token: 0x0600003E RID: 62 RVA: 0x00003117 File Offset: 0x00001317
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

	// Token: 0x04000011 RID: 17
	public Transform Holder;

	// Token: 0x04000012 RID: 18
	public float currDistance = 5f;

	// Token: 0x04000013 RID: 19
	public float xRotate = 250f;

	// Token: 0x04000014 RID: 20
	public float yRotate = 120f;

	// Token: 0x04000015 RID: 21
	public float yMinLimit = -20f;

	// Token: 0x04000016 RID: 22
	public float yMaxLimit = 80f;

	// Token: 0x04000017 RID: 23
	public float prevDistance;

	// Token: 0x04000018 RID: 24
	private float x;

	// Token: 0x04000019 RID: 25
	private float y;

	// Token: 0x0400001A RID: 26
	[Header("GUI")]
	private float windowDpi;

	// Token: 0x0400001B RID: 27
	public GameObject[] Prefabs;

	// Token: 0x0400001C RID: 28
	private int Prefab;

	// Token: 0x0400001D RID: 29
	private GameObject Instance;

	// Token: 0x0400001E RID: 30
	private float StartColor;

	// Token: 0x0400001F RID: 31
	private float HueColor;

	// Token: 0x04000020 RID: 32
	public Texture HueTexture;

	// Token: 0x04000021 RID: 33
	public float[] activationTime;

	// Token: 0x04000022 RID: 34
	public Animator animObject;

	// Token: 0x04000023 RID: 35
	private ParticleSystem[] particleSystems = new ParticleSystem[0];

	// Token: 0x04000024 RID: 36
	private List<DemoToonVFX.SVA> svList = new List<DemoToonVFX.SVA>();

	// Token: 0x04000025 RID: 37
	private float H;

	// Token: 0x04000026 RID: 38
	public bool useAnimation;

	// Token: 0x020011C4 RID: 4548
	public struct SVA
	{
		// Token: 0x0400634F RID: 25423
		public float S;

		// Token: 0x04006350 RID: 25424
		public float V;

		// Token: 0x04006351 RID: 25425
		public float A;
	}
}
