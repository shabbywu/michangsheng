using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class TGPDemo : MonoBehaviour
{
	// Token: 0x06001109 RID: 4361 RVA: 0x000AA8B8 File Offset: 0x000A8AB8
	private void Start()
	{
		Renderer[] componentsInChildren = GameObject.Find("TGPDemo_Astrella").gameObject.GetComponentsInChildren<Renderer>();
		List<Material> list = new List<Material>();
		List<Material> list2 = new List<Material>();
		List<Material> list3 = new List<Material>();
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			foreach (Material material in array[i].materials)
			{
				if (material.shader.name.Contains("Outline"))
				{
					list2.Add(material);
				}
				else if (material.shader.name.Contains("Toony"))
				{
					list.Add(material);
				}
				if (material.shader.name.Contains("Toony"))
				{
					list3.Add(material);
				}
			}
		}
		this.matsSimple = list.ToArray();
		this.matsOutline = list2.ToArray();
		this.matsAll = list3.ToArray();
		this.sceneLight = GameObject.Find("_Light");
		this.lightRotX = this.sceneLight.transform.eulerAngles.x;
		this.lightRotY = this.sceneLight.transform.eulerAngles.y;
		this.qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
		Shader.WarmupAllShaders();
		this.UpdateGUI();
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x000109BF File Offset: 0x0000EBBF
	private void SwitchRotation()
	{
		this.rotate = !this.rotate;
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x000AAA20 File Offset: 0x000A8C20
	private void Update()
	{
		if (this.rotate)
		{
			this.rotateGroup.transform.Rotate(Vector3.up * this.rotationSpeed * Time.deltaTime);
			this.rotY = this.rotateGroup.transform.eulerAngles.y;
		}
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			this.zoom -= axis;
			this.zoom = Mathf.Clamp(this.zoom, 1f, 3f);
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.zoom);
		}
		if (Input.mousePosition.x < (float)Screen.width * 0.8f && Input.mousePosition.x > (float)Screen.width * 0.2f && Input.GetMouseButton(0))
		{
			Vector3 vector = this.lastMousePos - Input.mousePosition;
			Camera.main.transform.Translate(vector * Time.deltaTime * 0.2f);
		}
		this.lastMousePos = Input.mousePosition;
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x000AAB70 File Offset: 0x000A8D70
	private void OnGUI()
	{
		this.zoom = GUI.VerticalSlider(new Rect((float)(Screen.width - 24), 16f, 10f, 224f), this.zoom, 1f, 3f);
		if (GUI.changed)
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.zoom);
			GUI.changed = false;
		}
		GUI.enabled = !this.rotate;
		this.rotY = GUI.HorizontalSlider(new Rect(16f, 170f, 128f, 10f), this.rotY, 0f, 360f);
		GUI.enabled = true;
		if (GUI.changed && !this.rotate)
		{
			this.rotateGroup.transform.eulerAngles = new Vector3(0f, this.rotY, 0f);
			GUI.changed = false;
		}
		this.lightRotY = GUI.HorizontalSlider(new Rect(16f, 224f, 128f, 10f), this.lightRotY, 0f, 360f);
		GUI.enabled = true;
		if (GUI.changed)
		{
			this.sceneLight.transform.eulerAngles = new Vector3(this.sceneLight.transform.eulerAngles.x, this.lightRotY, 0f);
			GUI.changed = false;
		}
		this.lightRotX = GUI.HorizontalSlider(new Rect(16f, 244f, 128f, 10f), this.lightRotX, -90f, 90f);
		GUI.enabled = true;
		if (GUI.changed)
		{
			this.sceneLight.transform.eulerAngles = new Vector3(this.lightRotX, this.sceneLight.transform.eulerAngles.y, 0f);
			GUI.changed = false;
		}
		if (this.rim)
		{
			this.rim_pow = GUI.HorizontalSlider(new Rect((float)(Screen.width - 150), 320f, 128f, 10f), this.rim_pow, -1f, 1f);
			GUI.enabled = true;
			if (GUI.changed)
			{
				for (int i = 0; i < this.matsAll.Length; i++)
				{
					this.matsAll[i].SetFloat("_RimPower", this.rim_pow);
				}
				GUI.changed = false;
			}
		}
		if (this.rimOutline)
		{
			this.rimo_min = GUI.HorizontalSlider(new Rect((float)(Screen.width - 150), 320f, 128f, 10f), this.rimo_min, 0f, 1f);
			GUI.enabled = true;
			if (GUI.changed)
			{
				for (int j = 0; j < this.matsOutline.Length; j++)
				{
					this.matsOutline[j].SetFloat("_RimMin", this.rimo_min);
				}
				GUI.changed = false;
			}
			this.rimo_max = GUI.HorizontalSlider(new Rect((float)(Screen.width - 150), 360f, 128f, 10f), this.rimo_max, 0f, 1f);
			GUI.enabled = true;
			if (GUI.changed)
			{
				for (int k = 0; k < this.matsOutline.Length; k++)
				{
					this.matsOutline[k].SetFloat("_RimMax", this.rimo_max);
				}
				GUI.changed = false;
			}
		}
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x000AAEFC File Offset: 0x000A90FC
	private void ReloadShader()
	{
		string str = "Normal";
		if (this.outline)
		{
			str = (this.outline_cst ? "OutlineConst" : "Outline");
		}
		string text = "Basic";
		if (this.bump && this.spec)
		{
			text = "Bumped Specular";
		}
		else if (this.spec)
		{
			text = "Specular";
		}
		else if (this.bump)
		{
			text = "Bumped";
		}
		if (this.rim)
		{
			text += " Rim";
		}
		else if (this.rimOutline)
		{
			str = "Rim Outline";
		}
		string text2 = "Toony Colors Pro/" + str + "/OneDirLight/" + text;
		Shader shader = this.FindShader(text2);
		if (shader == null)
		{
			Debug.LogError("SHADER NOT FOUND: " + text2);
			return;
		}
		for (int i = 0; i < this.matsOutline.Length; i++)
		{
			this.matsOutline[i].shader = shader;
		}
		text2 = "Toony Colors Pro/Normal/OneDirLight/" + text;
		shader = this.FindShader(text2);
		if (shader == null)
		{
			Debug.LogError("SHADER NOT FOUND: " + text2);
			return;
		}
		for (int j = 0; j < this.matsSimple.Length; j++)
		{
			string text3 = "Basic";
			if (this.spec)
			{
				text3 = "Specular";
			}
			if (this.rim)
			{
				text3 += " Rim";
			}
			Shader shader2 = this.FindShader("Toony Colors Pro/Normal/OneDirLight/" + text3);
			if (shader2 != null)
			{
				this.matsSimple[j].shader = shader2;
			}
		}
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x000AB088 File Offset: 0x000A9288
	private void UpdateGUI()
	{
		GameObject[] array = this.actRim;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(this.rim);
		}
		array = this.actRimOutline;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(this.rimOutline);
		}
		this.UpdateGUITButtons();
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x000AB0E4 File Offset: 0x000A92E4
	private void UpdateGUITButtons()
	{
		foreach (GUIT_Button guit_Button in (GUIT_Button[])Object.FindObjectsOfType(typeof(GUIT_Button)))
		{
			string callback = guit_Button.callback;
			if (!(callback == "SwitchOutline"))
			{
				if (!(callback == "SwitchRim"))
				{
					if (callback == "SwitchRimOutline")
					{
						guit_Button.UpdateState(this.rimOutline);
					}
				}
				else
				{
					guit_Button.UpdateState(this.rim);
				}
			}
			else
			{
				guit_Button.UpdateState(this.outline);
			}
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000AB170 File Offset: 0x000A9370
	private Shader FindShader(string name)
	{
		foreach (Shader shader in this.shaders)
		{
			if (shader.name == name)
			{
				return shader;
			}
		}
		Debug.LogError("SHADER NOT FOUND: " + name);
		return null;
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x000109D0 File Offset: 0x0000EBD0
	private void SwitchOutline()
	{
		this.outline = !this.outline;
		if (this.outline && this.rimOutline)
		{
			this.rimOutline = false;
		}
		this.ReloadShader();
		this.UpdateGUI();
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x00010A04 File Offset: 0x0000EC04
	private void SwitchOutlineCst()
	{
		this.outline_cst = !this.outline_cst;
		this.ReloadShader();
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x00010A1B File Offset: 0x0000EC1B
	private void SwitchSpec()
	{
		this.spec = !this.spec;
		this.ReloadShader();
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x00010A32 File Offset: 0x0000EC32
	private void SwitchBump()
	{
		this.bump = !this.bump;
		this.ReloadShader();
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00010A49 File Offset: 0x0000EC49
	private void SwitchRim()
	{
		this.rim = !this.rim;
		if (this.rim && this.rimOutline)
		{
			this.rimOutline = false;
		}
		this.ReloadShader();
		this.UpdateGUI();
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x000AB1B8 File Offset: 0x000A93B8
	private void SwitchRimOutline()
	{
		this.rimOutline = !this.rimOutline;
		if (this.rimOutline && this.rim)
		{
			this.rim = false;
		}
		if (this.rimOutline && this.outline)
		{
			this.outline = false;
		}
		this.ReloadShader();
		this.UpdateGUI();
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x00010A7D File Offset: 0x0000EC7D
	private void NextRamp()
	{
		this.rampIndex++;
		if (this.rampIndex >= this.rampTextures.Length)
		{
			this.rampIndex = 0;
		}
		this.UpdateRamp();
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x00010AAA File Offset: 0x0000ECAA
	private void PrevRamp()
	{
		this.rampIndex--;
		if (this.rampIndex < 0)
		{
			this.rampIndex = this.rampTextures.Length - 1;
		}
		this.UpdateRamp();
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x000AB210 File Offset: 0x000A9410
	private void UpdateRamp()
	{
		this.rampUI.texture = this.rampTextures[this.rampIndex];
		Material[] array = this.matsAll;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetTexture("_Ramp", this.rampTextures[this.rampIndex]);
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x00010AD9 File Offset: 0x0000ECD9
	private void NextQuality()
	{
		QualitySettings.IncreaseLevel(true);
		this.qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x00010B01 File Offset: 0x0000ED01
	private void PrevQuality()
	{
		QualitySettings.DecreaseLevel(true);
		this.qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
	}

	// Token: 0x04000DB3 RID: 3507
	public bool rotate = true;

	// Token: 0x04000DB4 RID: 3508
	public GameObject rotateGroup;

	// Token: 0x04000DB5 RID: 3509
	public float rotationSpeed = 50f;

	// Token: 0x04000DB6 RID: 3510
	public Texture[] rampTextures;

	// Token: 0x04000DB7 RID: 3511
	public GUITexture rampUI;

	// Token: 0x04000DB8 RID: 3512
	private int rampIndex;

	// Token: 0x04000DB9 RID: 3513
	public GUIText qualityLabel;

	// Token: 0x04000DBA RID: 3514
	private Material[] matsSimple;

	// Token: 0x04000DBB RID: 3515
	private Material[] matsOutline;

	// Token: 0x04000DBC RID: 3516
	private Material[] matsAll;

	// Token: 0x04000DBD RID: 3517
	private GameObject sceneLight;

	// Token: 0x04000DBE RID: 3518
	public Shader[] shaders;

	// Token: 0x04000DBF RID: 3519
	private Vector3 lastMousePos;

	// Token: 0x04000DC0 RID: 3520
	private float zoom = 2f;

	// Token: 0x04000DC1 RID: 3521
	private float rotY;

	// Token: 0x04000DC2 RID: 3522
	private float lightRotX;

	// Token: 0x04000DC3 RID: 3523
	private float lightRotY;

	// Token: 0x04000DC4 RID: 3524
	private float rimo_min = 0.4f;

	// Token: 0x04000DC5 RID: 3525
	private float rimo_max = 0.6f;

	// Token: 0x04000DC6 RID: 3526
	private float rim_pow = 0.5f;

	// Token: 0x04000DC7 RID: 3527
	private bool bump = true;

	// Token: 0x04000DC8 RID: 3528
	private bool spec = true;

	// Token: 0x04000DC9 RID: 3529
	private bool outline = true;

	// Token: 0x04000DCA RID: 3530
	private bool outline_cst;

	// Token: 0x04000DCB RID: 3531
	private bool rim;

	// Token: 0x04000DCC RID: 3532
	private bool rimOutline;

	// Token: 0x04000DCD RID: 3533
	public GameObject[] actRim;

	// Token: 0x04000DCE RID: 3534
	public GameObject[] actRimOutline;

	// Token: 0x04000DCF RID: 3535
	public GUIT_Button subOutlines;
}
