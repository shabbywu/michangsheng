using System.Collections.Generic;
using UnityEngine;

public class TGPDemo : MonoBehaviour
{
	public bool rotate = true;

	public GameObject rotateGroup;

	public float rotationSpeed = 50f;

	public Texture[] rampTextures;

	public GUITexture rampUI;

	private int rampIndex;

	public GUIText qualityLabel;

	private Material[] matsSimple;

	private Material[] matsOutline;

	private Material[] matsAll;

	private GameObject sceneLight;

	public Shader[] shaders;

	private Vector3 lastMousePos;

	private float zoom = 2f;

	private float rotY;

	private float lightRotX;

	private float lightRotY;

	private float rimo_min = 0.4f;

	private float rimo_max = 0.6f;

	private float rim_pow = 0.5f;

	private bool bump = true;

	private bool spec = true;

	private bool outline = true;

	private bool outline_cst;

	private bool rim;

	private bool rimOutline;

	public GameObject[] actRim;

	public GameObject[] actRimOutline;

	public GUIT_Button subOutlines;

	private void Start()
	{
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		Renderer[] componentsInChildren = GameObject.Find("TGPDemo_Astrella").gameObject.GetComponentsInChildren<Renderer>();
		List<Material> list = new List<Material>();
		List<Material> list2 = new List<Material>();
		List<Material> list3 = new List<Material>();
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Material[] materials = array[i].materials;
			foreach (Material val in materials)
			{
				if (((Object)val.shader).name.Contains("Outline"))
				{
					list2.Add(val);
				}
				else if (((Object)val.shader).name.Contains("Toony"))
				{
					list.Add(val);
				}
				if (((Object)val.shader).name.Contains("Toony"))
				{
					list3.Add(val);
				}
			}
		}
		matsSimple = list.ToArray();
		matsOutline = list2.ToArray();
		matsAll = list3.ToArray();
		sceneLight = GameObject.Find("_Light");
		lightRotX = sceneLight.transform.eulerAngles.x;
		lightRotY = sceneLight.transform.eulerAngles.y;
		qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
		Shader.WarmupAllShaders();
		UpdateGUI();
	}

	private void SwitchRotation()
	{
		rotate = !rotate;
	}

	private void Update()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (rotate)
		{
			rotateGroup.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
			rotY = rotateGroup.transform.eulerAngles.y;
		}
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			zoom -= axis;
			zoom = Mathf.Clamp(zoom, 1f, 3f);
			((Component)Camera.main).transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y, zoom);
		}
		if (Input.mousePosition.x < (float)Screen.width * 0.8f && Input.mousePosition.x > (float)Screen.width * 0.2f && Input.GetMouseButton(0))
		{
			Vector3 val = lastMousePos - Input.mousePosition;
			((Component)Camera.main).transform.Translate(val * Time.deltaTime * 0.2f);
		}
		lastMousePos = Input.mousePosition;
	}

	private void OnGUI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		zoom = GUI.VerticalSlider(new Rect((float)(Screen.width - 24), 16f, 10f, 224f), zoom, 1f, 3f);
		if (GUI.changed)
		{
			((Component)Camera.main).transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y, zoom);
			GUI.changed = false;
		}
		GUI.enabled = !rotate;
		rotY = GUI.HorizontalSlider(new Rect(16f, 170f, 128f, 10f), rotY, 0f, 360f);
		GUI.enabled = true;
		if (GUI.changed && !rotate)
		{
			rotateGroup.transform.eulerAngles = new Vector3(0f, rotY, 0f);
			GUI.changed = false;
		}
		lightRotY = GUI.HorizontalSlider(new Rect(16f, 224f, 128f, 10f), lightRotY, 0f, 360f);
		GUI.enabled = true;
		if (GUI.changed)
		{
			sceneLight.transform.eulerAngles = new Vector3(sceneLight.transform.eulerAngles.x, lightRotY, 0f);
			GUI.changed = false;
		}
		lightRotX = GUI.HorizontalSlider(new Rect(16f, 244f, 128f, 10f), lightRotX, -90f, 90f);
		GUI.enabled = true;
		if (GUI.changed)
		{
			sceneLight.transform.eulerAngles = new Vector3(lightRotX, sceneLight.transform.eulerAngles.y, 0f);
			GUI.changed = false;
		}
		if (rim)
		{
			rim_pow = GUI.HorizontalSlider(new Rect((float)(Screen.width - 150), 320f, 128f, 10f), rim_pow, -1f, 1f);
			GUI.enabled = true;
			if (GUI.changed)
			{
				for (int i = 0; i < matsAll.Length; i++)
				{
					matsAll[i].SetFloat("_RimPower", rim_pow);
				}
				GUI.changed = false;
			}
		}
		if (!rimOutline)
		{
			return;
		}
		rimo_min = GUI.HorizontalSlider(new Rect((float)(Screen.width - 150), 320f, 128f, 10f), rimo_min, 0f, 1f);
		GUI.enabled = true;
		if (GUI.changed)
		{
			for (int j = 0; j < matsOutline.Length; j++)
			{
				matsOutline[j].SetFloat("_RimMin", rimo_min);
			}
			GUI.changed = false;
		}
		rimo_max = GUI.HorizontalSlider(new Rect((float)(Screen.width - 150), 360f, 128f, 10f), rimo_max, 0f, 1f);
		GUI.enabled = true;
		if (GUI.changed)
		{
			for (int k = 0; k < matsOutline.Length; k++)
			{
				matsOutline[k].SetFloat("_RimMax", rimo_max);
			}
			GUI.changed = false;
		}
	}

	private void ReloadShader()
	{
		string text = "Normal";
		if (outline)
		{
			text = (outline_cst ? "OutlineConst" : "Outline");
		}
		string text2 = "Basic";
		if (bump && spec)
		{
			text2 = "Bumped Specular";
		}
		else if (spec)
		{
			text2 = "Specular";
		}
		else if (bump)
		{
			text2 = "Bumped";
		}
		if (rim)
		{
			text2 += " Rim";
		}
		else if (rimOutline)
		{
			text = "Rim Outline";
		}
		string text3 = "Toony Colors Pro/" + text + "/OneDirLight/" + text2;
		Shader val = FindShader(text3);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("SHADER NOT FOUND: " + text3));
			return;
		}
		for (int i = 0; i < matsOutline.Length; i++)
		{
			matsOutline[i].shader = val;
		}
		text3 = "Toony Colors Pro/Normal/OneDirLight/" + text2;
		val = FindShader(text3);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("SHADER NOT FOUND: " + text3));
			return;
		}
		for (int j = 0; j < matsSimple.Length; j++)
		{
			string text4 = "Basic";
			if (spec)
			{
				text4 = "Specular";
			}
			if (rim)
			{
				text4 += " Rim";
			}
			Shader val2 = FindShader("Toony Colors Pro/Normal/OneDirLight/" + text4);
			if ((Object)(object)val2 != (Object)null)
			{
				matsSimple[j].shader = val2;
			}
		}
	}

	private void UpdateGUI()
	{
		GameObject[] array = actRim;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(rim);
		}
		array = actRimOutline;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(rimOutline);
		}
		UpdateGUITButtons();
	}

	private void UpdateGUITButtons()
	{
		GUIT_Button[] array = (GUIT_Button[])(object)Object.FindObjectsOfType(typeof(GUIT_Button));
		foreach (GUIT_Button gUIT_Button in array)
		{
			switch (gUIT_Button.callback)
			{
			case "SwitchOutline":
				gUIT_Button.UpdateState(outline);
				break;
			case "SwitchRim":
				gUIT_Button.UpdateState(rim);
				break;
			case "SwitchRimOutline":
				gUIT_Button.UpdateState(rimOutline);
				break;
			}
		}
	}

	private Shader FindShader(string name)
	{
		Shader[] array = shaders;
		foreach (Shader val in array)
		{
			if (((Object)val).name == name)
			{
				return val;
			}
		}
		Debug.LogError((object)("SHADER NOT FOUND: " + name));
		return null;
	}

	private void SwitchOutline()
	{
		outline = !outline;
		if (outline && rimOutline)
		{
			rimOutline = false;
		}
		ReloadShader();
		UpdateGUI();
	}

	private void SwitchOutlineCst()
	{
		outline_cst = !outline_cst;
		ReloadShader();
	}

	private void SwitchSpec()
	{
		spec = !spec;
		ReloadShader();
	}

	private void SwitchBump()
	{
		bump = !bump;
		ReloadShader();
	}

	private void SwitchRim()
	{
		rim = !rim;
		if (rim && rimOutline)
		{
			rimOutline = false;
		}
		ReloadShader();
		UpdateGUI();
	}

	private void SwitchRimOutline()
	{
		rimOutline = !rimOutline;
		if (rimOutline && rim)
		{
			rim = false;
		}
		if (rimOutline && outline)
		{
			outline = false;
		}
		ReloadShader();
		UpdateGUI();
	}

	private void NextRamp()
	{
		rampIndex++;
		if (rampIndex >= rampTextures.Length)
		{
			rampIndex = 0;
		}
		UpdateRamp();
	}

	private void PrevRamp()
	{
		rampIndex--;
		if (rampIndex < 0)
		{
			rampIndex = rampTextures.Length - 1;
		}
		UpdateRamp();
	}

	private void UpdateRamp()
	{
		rampUI.texture = rampTextures[rampIndex];
		Material[] array = matsAll;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetTexture("_Ramp", rampTextures[rampIndex]);
		}
	}

	private void NextQuality()
	{
		QualitySettings.IncreaseLevel(true);
		qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
	}

	private void PrevQuality()
	{
		QualitySettings.DecreaseLevel(true);
		qualityLabel.text = "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
	}
}
