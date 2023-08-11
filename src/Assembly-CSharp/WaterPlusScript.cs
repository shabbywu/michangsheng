using System.Collections.Generic;
using UnityEngine;

public class WaterPlusScript : MonoBehaviour
{
	public WaterMovementType movementType;

	public Vector2 velocity;

	public float speed;

	public Transform target;

	public bool animatedNormalmaps = true;

	private Texture2D[] normalmapAnimation;

	private Texture2D[] dudvfoamAnimation;

	private float animationValue;

	private Vector3 waterCenter;

	private Material waterMaterial;

	private Vector3 projectedLightDir;

	private Vector2 anisoDirAnimationOffset;

	private int causticsAnimationFrame;

	private float causticsAnimationTime;

	private int normalmapAnimationFrame;

	private float normalmapAnimationTime;

	private void Reset()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		speed = 3f;
		velocity = new Vector2(0.7f, 0f);
	}

	private Light FindTheBrightestDirectionalLight()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Invalid comparison between Unknown and I4
		Light val = null;
		Light[] obj = Object.FindObjectsOfType(typeof(Light)) as Light[];
		List<Light> list = new List<Light>();
		Light[] array = obj;
		foreach (Light val2 in array)
		{
			if ((int)val2.type == 1)
			{
				list.Add(val2);
			}
		}
		if (list.Count <= 0)
		{
			return null;
		}
		val = list[0];
		foreach (Light item in list)
		{
			if (item.intensity > val.intensity)
			{
				val = item;
			}
		}
		return val;
	}

	private void Start()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		Bounds bounds = ((Component)this).GetComponent<Renderer>().bounds;
		waterCenter = ((Bounds)(ref bounds)).center;
		if (movementType == WaterMovementType.directional)
		{
			speed = ((Vector2)(ref velocity)).magnitude;
		}
		bounds = ((Component)this).GetComponent<Renderer>().bounds;
		float num = ((Bounds)(ref bounds)).size.x / ((Component)this).gameObject.GetComponent<Renderer>().material.GetTextureScale("_MainTex").x;
		speed /= num;
		waterMaterial = ((Component)this).GetComponent<Renderer>().material;
		Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
		Shader.EnableKeyword("WATER_EDGEBLEND_ON");
		if (movementType == WaterMovementType.flowmap)
		{
			Shader.DisableKeyword("FLOWMAP_ANIMATION_OFF");
			Shader.EnableKeyword("FLOWMAP_ANIMATION_ON");
			((Component)this).gameObject.AddComponent<FlowmapAnimator>().flowSpeed = speed;
		}
		else
		{
			Shader.DisableKeyword("FLOWMAP_ANIMATION_ON");
			Shader.EnableKeyword("FLOWMAP_ANIMATION_OFF");
		}
		Light val = FindTheBrightestDirectionalLight();
		projectedLightDir = ((Component)val).transform.forward - ((Component)this).transform.up * Vector3.Dot(((Component)this).transform.up, ((Component)val).transform.forward);
		((Vector3)(ref projectedLightDir)).Normalize();
		anisoDirAnimationOffset = Vector2.zero;
		causticsAnimationFrame = 0;
		if (!animatedNormalmaps)
		{
			return;
		}
		normalmapAnimation = (Texture2D[])(object)new Texture2D[60];
		dudvfoamAnimation = (Texture2D[])(object)new Texture2D[60];
		for (int i = 0; i < 60; i++)
		{
			string text = "";
			if (i < 10)
			{
				text = "0";
			}
			text += i;
			ref Texture2D reference = ref normalmapAnimation[i];
			Object obj = Resources.Load("water_hm" + text, typeof(Texture2D));
			reference = (Texture2D)(object)((obj is Texture2D) ? obj : null);
			ref Texture2D reference2 = ref dudvfoamAnimation[i];
			Object obj2 = Resources.Load("dudv_foam" + text, typeof(Texture2D));
			reference2 = (Texture2D)(object)((obj2 is Texture2D) ? obj2 : null);
			if ((Object)null == (Object)(object)normalmapAnimation[i])
			{
				Debug.LogError((object)("unable to find normalmap animation file 'water_normal_" + text + "'. Aborting."));
				animatedNormalmaps = false;
				break;
			}
			if ((Object)null == (Object)(object)dudvfoamAnimation[i])
			{
				Debug.LogError((object)("unable to find dudv animation file 'dudv_foam" + text + "'. Aborting."));
				animatedNormalmaps = false;
				break;
			}
		}
		normalmapAnimationFrame = 0;
	}

	private void OnDestroy()
	{
		Shader.DisableKeyword("WATER_EDGEBLEND_ON");
		Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
	}

	private void Update()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		switch (movementType)
		{
		case WaterMovementType.island:
		{
			Vector3 val = waterCenter - target.position;
			velocity.x = val.x;
			velocity.y = val.z;
			velocity = ((Vector2)(ref velocity)).normalized * speed;
			break;
		}
		case WaterMovementType.still:
			velocity = Vector2.op_Implicit(Vector3.zero);
			break;
		}
		if ((movementType == WaterMovementType.directional) | (movementType == WaterMovementType.island))
		{
			Vector2 textureOffset = waterMaterial.GetTextureOffset("_MainTex");
			Vector2 val2 = textureOffset + velocity * Time.deltaTime;
			Vector2 val3 = velocity * Time.deltaTime;
			if (((Vector2)(ref val3)).sqrMagnitude > 1f)
			{
				Vector2 val4 = velocity * Time.deltaTime;
				Vector2 normalized = ((Vector2)(ref val4)).normalized;
				while (((Vector2)(ref val4)).sqrMagnitude > 1f)
				{
					val4 -= normalized;
				}
				val2 = textureOffset + val4;
			}
			waterMaterial.SetTextureOffset("_MainTex", val2);
			waterMaterial.SetTextureOffset("_Normalmap", val2);
		}
		anisoDirAnimationOffset += new Vector2(projectedLightDir.x, projectedLightDir.z) * Time.deltaTime * 0.01f;
		Vector4 val5 = default(Vector4);
		((Vector4)(ref val5))._002Ector(anisoDirAnimationOffset.x, anisoDirAnimationOffset.y, 0f, 0f);
		waterMaterial.SetVector("anisoDirAnimationOffset", val5);
		int num = causticsAnimationFrame / 16;
		float num2 = (float)(causticsAnimationFrame % 16 / 4) * 0.25f;
		float num3 = (float)(causticsAnimationFrame % 16 % 4) * 0.25f;
		Vector4 val6 = default(Vector4);
		((Vector4)(ref val6))._002Ector(num3, num2, 0.25f, 0.25f);
		Vector4 val7 = default(Vector4);
		switch (num)
		{
		default:
			((Vector4)(ref val7))._002Ector(1f, 0f, 0f, 0f);
			break;
		case 1:
			((Vector4)(ref val7))._002Ector(0f, 1f, 0f, 0f);
			break;
		case 2:
			((Vector4)(ref val7))._002Ector(0f, 0f, 1f, 0f);
			break;
		}
		waterMaterial.SetVector("causticsOffsetAndScale", val6);
		waterMaterial.SetVector("causticsAnimationColorChannel", val7);
		causticsAnimationTime += Time.deltaTime;
		if (causticsAnimationTime >= 0.04f)
		{
			causticsAnimationFrame++;
			causticsAnimationTime = 0f;
			if (causticsAnimationFrame >= 48)
			{
				causticsAnimationFrame = 0;
			}
		}
		if (!animatedNormalmaps)
		{
			return;
		}
		normalmapAnimationTime += Time.deltaTime;
		if (normalmapAnimationTime >= 0.04f)
		{
			normalmapAnimationFrame++;
			normalmapAnimationTime = 0f;
			if (normalmapAnimationFrame >= 60)
			{
				normalmapAnimationFrame = 0;
			}
			waterMaterial.SetTexture("_NormalMap", (Texture)(object)normalmapAnimation[normalmapAnimationFrame]);
			waterMaterial.SetTexture("_DUDVFoamMap", (Texture)(object)dudvfoamAnimation[normalmapAnimationFrame]);
		}
	}
}
