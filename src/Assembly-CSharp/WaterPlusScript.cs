using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class WaterPlusScript : MonoBehaviour
{
	// Token: 0x06000BDA RID: 3034 RVA: 0x0000DF30 File Offset: 0x0000C130
	private void Reset()
	{
		this.speed = 3f;
		this.velocity = new Vector2(0.7f, 0f);
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00093FE0 File Offset: 0x000921E0
	private Light FindTheBrightestDirectionalLight()
	{
		Light light = null;
		Light[] array = Object.FindObjectsOfType(typeof(Light)) as Light[];
		List<Light> list = new List<Light>();
		foreach (Light light2 in array)
		{
			if (light2.type == 1)
			{
				list.Add(light2);
			}
		}
		if (list.Count <= 0)
		{
			return null;
		}
		light = list[0];
		foreach (Light light3 in list)
		{
			if (light3.intensity > light.intensity)
			{
				light = light3;
			}
		}
		return light;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00094094 File Offset: 0x00092294
	private void Start()
	{
		this.waterCenter = base.GetComponent<Renderer>().bounds.center;
		if (this.movementType == WaterMovementType.directional)
		{
			this.speed = this.velocity.magnitude;
		}
		float num = base.GetComponent<Renderer>().bounds.size.x / base.gameObject.GetComponent<Renderer>().material.GetTextureScale("_MainTex").x;
		this.speed /= num;
		this.waterMaterial = base.GetComponent<Renderer>().material;
		Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
		Shader.EnableKeyword("WATER_EDGEBLEND_ON");
		if (this.movementType == WaterMovementType.flowmap)
		{
			Shader.DisableKeyword("FLOWMAP_ANIMATION_OFF");
			Shader.EnableKeyword("FLOWMAP_ANIMATION_ON");
			base.gameObject.AddComponent<FlowmapAnimator>().flowSpeed = this.speed;
		}
		else
		{
			Shader.DisableKeyword("FLOWMAP_ANIMATION_ON");
			Shader.EnableKeyword("FLOWMAP_ANIMATION_OFF");
		}
		Light light = this.FindTheBrightestDirectionalLight();
		this.projectedLightDir = light.transform.forward - base.transform.up * Vector3.Dot(base.transform.up, light.transform.forward);
		this.projectedLightDir.Normalize();
		this.anisoDirAnimationOffset = Vector2.zero;
		this.causticsAnimationFrame = 0;
		if (this.animatedNormalmaps)
		{
			this.normalmapAnimation = new Texture2D[60];
			this.dudvfoamAnimation = new Texture2D[60];
			for (int i = 0; i < 60; i++)
			{
				string text = "";
				if (i < 10)
				{
					text = "0";
				}
				text += i;
				this.normalmapAnimation[i] = (Resources.Load("water_hm" + text, typeof(Texture2D)) as Texture2D);
				this.dudvfoamAnimation[i] = (Resources.Load("dudv_foam" + text, typeof(Texture2D)) as Texture2D);
				if (null == this.normalmapAnimation[i])
				{
					Debug.LogError("unable to find normalmap animation file 'water_normal_" + text + "'. Aborting.");
					this.animatedNormalmaps = false;
					break;
				}
				if (null == this.dudvfoamAnimation[i])
				{
					Debug.LogError("unable to find dudv animation file 'dudv_foam" + text + "'. Aborting.");
					this.animatedNormalmaps = false;
					break;
				}
			}
			this.normalmapAnimationFrame = 0;
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x0000DF52 File Offset: 0x0000C152
	private void OnDestroy()
	{
		Shader.DisableKeyword("WATER_EDGEBLEND_ON");
		Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x000942FC File Offset: 0x000924FC
	private void Update()
	{
		switch (this.movementType)
		{
		case WaterMovementType.island:
		{
			Vector3 vector = this.waterCenter - this.target.position;
			this.velocity.x = vector.x;
			this.velocity.y = vector.z;
			this.velocity = this.velocity.normalized * this.speed;
			break;
		}
		case WaterMovementType.still:
			this.velocity = Vector3.zero;
			break;
		}
		if (this.movementType == WaterMovementType.directional | this.movementType == WaterMovementType.island)
		{
			Vector2 textureOffset = this.waterMaterial.GetTextureOffset("_MainTex");
			Vector2 vector2 = textureOffset + this.velocity * Time.deltaTime;
			if ((this.velocity * Time.deltaTime).sqrMagnitude > 1f)
			{
				Vector2 vector3 = this.velocity * Time.deltaTime;
				Vector2 normalized = vector3.normalized;
				while (vector3.sqrMagnitude > 1f)
				{
					vector3 -= normalized;
				}
				vector2 = textureOffset + vector3;
			}
			this.waterMaterial.SetTextureOffset("_MainTex", vector2);
			this.waterMaterial.SetTextureOffset("_Normalmap", vector2);
		}
		this.anisoDirAnimationOffset += new Vector2(this.projectedLightDir.x, this.projectedLightDir.z) * Time.deltaTime * 0.01f;
		Vector4 vector4;
		vector4..ctor(this.anisoDirAnimationOffset.x, this.anisoDirAnimationOffset.y, 0f, 0f);
		this.waterMaterial.SetVector("anisoDirAnimationOffset", vector4);
		int num = this.causticsAnimationFrame / 16;
		float num2 = (float)(this.causticsAnimationFrame % 16 / 4) * 0.25f;
		float num3 = (float)(this.causticsAnimationFrame % 16 % 4) * 0.25f;
		Vector4 vector5;
		vector5..ctor(num3, num2, 0.25f, 0.25f);
		Vector4 vector6;
		switch (num)
		{
		default:
			vector6..ctor(1f, 0f, 0f, 0f);
			break;
		case 1:
			vector6..ctor(0f, 1f, 0f, 0f);
			break;
		case 2:
			vector6..ctor(0f, 0f, 1f, 0f);
			break;
		}
		this.waterMaterial.SetVector("causticsOffsetAndScale", vector5);
		this.waterMaterial.SetVector("causticsAnimationColorChannel", vector6);
		this.causticsAnimationTime += Time.deltaTime;
		if (this.causticsAnimationTime >= 0.04f)
		{
			this.causticsAnimationFrame++;
			this.causticsAnimationTime = 0f;
			if (this.causticsAnimationFrame >= 48)
			{
				this.causticsAnimationFrame = 0;
			}
		}
		if (this.animatedNormalmaps)
		{
			this.normalmapAnimationTime += Time.deltaTime;
			if (this.normalmapAnimationTime >= 0.04f)
			{
				this.normalmapAnimationFrame++;
				this.normalmapAnimationTime = 0f;
				if (this.normalmapAnimationFrame >= 60)
				{
					this.normalmapAnimationFrame = 0;
				}
				this.waterMaterial.SetTexture("_NormalMap", this.normalmapAnimation[this.normalmapAnimationFrame]);
				this.waterMaterial.SetTexture("_DUDVFoamMap", this.dudvfoamAnimation[this.normalmapAnimationFrame]);
			}
		}
	}

	// Token: 0x040008BB RID: 2235
	public WaterMovementType movementType;

	// Token: 0x040008BC RID: 2236
	public Vector2 velocity;

	// Token: 0x040008BD RID: 2237
	public float speed;

	// Token: 0x040008BE RID: 2238
	public Transform target;

	// Token: 0x040008BF RID: 2239
	public bool animatedNormalmaps = true;

	// Token: 0x040008C0 RID: 2240
	private Texture2D[] normalmapAnimation;

	// Token: 0x040008C1 RID: 2241
	private Texture2D[] dudvfoamAnimation;

	// Token: 0x040008C2 RID: 2242
	private float animationValue;

	// Token: 0x040008C3 RID: 2243
	private Vector3 waterCenter;

	// Token: 0x040008C4 RID: 2244
	private Material waterMaterial;

	// Token: 0x040008C5 RID: 2245
	private Vector3 projectedLightDir;

	// Token: 0x040008C6 RID: 2246
	private Vector2 anisoDirAnimationOffset;

	// Token: 0x040008C7 RID: 2247
	private int causticsAnimationFrame;

	// Token: 0x040008C8 RID: 2248
	private float causticsAnimationTime;

	// Token: 0x040008C9 RID: 2249
	private int normalmapAnimationFrame;

	// Token: 0x040008CA RID: 2250
	private float normalmapAnimationTime;
}
