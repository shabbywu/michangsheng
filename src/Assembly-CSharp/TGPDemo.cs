using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class TGPDemo : MonoBehaviour
{
	// Token: 0x06000EE3 RID: 3811 RVA: 0x0005A6E0 File Offset: 0x000588E0
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

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0005A848 File Offset: 0x00058A48
	private void SwitchRotation()
	{
		this.rotate = !this.rotate;
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0005A85C File Offset: 0x00058A5C
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

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0005A9AC File Offset: 0x00058BAC
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

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0005AD38 File Offset: 0x00058F38
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

	// Token: 0x06000EE8 RID: 3816 RVA: 0x0005AEC4 File Offset: 0x000590C4
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

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0005AF20 File Offset: 0x00059120
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

	// Token: 0x06000EEA RID: 3818 RVA: 0x0005AFAC File Offset: 0x000591AC
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

	// Token: 0x06000EEB RID: 3819 RVA: 0x0005AFF3 File Offset: 0x000591F3
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

	// Token: 0x06000EEC RID: 3820 RVA: 0x0005B027 File Offset: 0x00059227
	private void SwitchOutlineCst()
	{
		this.outline_cst = !this.outline_cst;
		this.ReloadShader();
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x0005B03E File Offset: 0x0005923E
	private void SwitchSpec()
	{
		this.spec = !this.spec;
		this.ReloadShader();
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x0005B055 File Offset: 0x00059255
	private void SwitchBump()
	{
		this.bump = !this.bump;
		this.ReloadShader();
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x0005B06C File Offset: 0x0005926C
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

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0005B0A0 File Offset: 0x000592A0
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

	// Token: 0x06000EF1 RID: 3825 RVA: 0x0005B0F6 File Offset: 0x000592F6
	private void NextRamp()
	{
		this.rampIndex++;
		if (this.rampIndex >= this.rampTextures.Length)
		{
			this.rampIndex = 0;
		}
		this.UpdateRamp();
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x0005B123 File Offset: 0x00059323
	private void PrevRamp()
	{
		this.rampIndex--;
		if (this.rampIndex < 0)
		{
			this.rampIndex = this.rampTextures.Length - 1;
		}
		this.UpdateRamp();
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x0005B154 File Offset: 0x00059354
	private void UpdateRamp()
	{
		this.rampUI.texture = this.rampTextures[this.rampIndex];
		Material[] array = this.matsAll;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetTexture("_Ramp", this.rampTextures[this.rampIndex]);
		}
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x0005B1A8 File Offset: 0x000593A8
	private void NextQuality()
	{
		QualitySettings.IncreaseLevel(true);
		this.qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0005B1D0 File Offset: 0x000593D0
	private void PrevQuality()
	{
		QualitySettings.DecreaseLevel(true);
		this.qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
	}

	// Token: 0x04000B15 RID: 2837
	public bool rotate = true;

	// Token: 0x04000B16 RID: 2838
	public GameObject rotateGroup;

	// Token: 0x04000B17 RID: 2839
	public float rotationSpeed = 50f;

	// Token: 0x04000B18 RID: 2840
	public Texture[] rampTextures;

	// Token: 0x04000B19 RID: 2841
	public GUITexture rampUI;

	// Token: 0x04000B1A RID: 2842
	private int rampIndex;

	// Token: 0x04000B1B RID: 2843
	public GUIText qualityLabel;

	// Token: 0x04000B1C RID: 2844
	private Material[] matsSimple;

	// Token: 0x04000B1D RID: 2845
	private Material[] matsOutline;

	// Token: 0x04000B1E RID: 2846
	private Material[] matsAll;

	// Token: 0x04000B1F RID: 2847
	private GameObject sceneLight;

	// Token: 0x04000B20 RID: 2848
	public Shader[] shaders;

	// Token: 0x04000B21 RID: 2849
	private Vector3 lastMousePos;

	// Token: 0x04000B22 RID: 2850
	private float zoom = 2f;

	// Token: 0x04000B23 RID: 2851
	private float rotY;

	// Token: 0x04000B24 RID: 2852
	private float lightRotX;

	// Token: 0x04000B25 RID: 2853
	private float lightRotY;

	// Token: 0x04000B26 RID: 2854
	private float rimo_min = 0.4f;

	// Token: 0x04000B27 RID: 2855
	private float rimo_max = 0.6f;

	// Token: 0x04000B28 RID: 2856
	private float rim_pow = 0.5f;

	// Token: 0x04000B29 RID: 2857
	private bool bump = true;

	// Token: 0x04000B2A RID: 2858
	private bool spec = true;

	// Token: 0x04000B2B RID: 2859
	private bool outline = true;

	// Token: 0x04000B2C RID: 2860
	private bool outline_cst;

	// Token: 0x04000B2D RID: 2861
	private bool rim;

	// Token: 0x04000B2E RID: 2862
	private bool rimOutline;

	// Token: 0x04000B2F RID: 2863
	public GameObject[] actRim;

	// Token: 0x04000B30 RID: 2864
	public GameObject[] actRimOutline;

	// Token: 0x04000B31 RID: 2865
	public GUIT_Button subOutlines;
}
