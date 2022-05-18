using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000921 RID: 2337
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class ImageEffectBase : MonoBehaviour
	{
		// Token: 0x06003B6C RID: 15212 RVA: 0x001AE2FC File Offset: 0x001AC4FC
		protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
		{
			if (!s)
			{
				Debug.Log("Missing shader in " + this.ToString());
				base.enabled = false;
				return null;
			}
			if (s.isSupported && m2Create && m2Create.shader == s)
			{
				return m2Create;
			}
			if (!s.isSupported)
			{
				this.NotSupported();
				Debug.Log(string.Concat(new string[]
				{
					"The shader ",
					s.ToString(),
					" on effect ",
					this.ToString(),
					" is not supported on this platform!"
				}));
				return null;
			}
			m2Create = new Material(s);
			this.createdMaterials.Add(m2Create);
			m2Create.hideFlags = 52;
			return m2Create;
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x001AE3B8 File Offset: 0x001AC5B8
		protected Material CreateMaterial(Shader s, Material m2Create)
		{
			if (!s)
			{
				Debug.Log("Missing shader in " + this.ToString());
				return null;
			}
			if (m2Create && m2Create.shader == s && s.isSupported)
			{
				return m2Create;
			}
			if (!s.isSupported)
			{
				return null;
			}
			m2Create = new Material(s);
			this.createdMaterials.Add(m2Create);
			m2Create.hideFlags = 52;
			return m2Create;
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x0002AF94 File Offset: 0x00029194
		private void OnEnable()
		{
			this.isSupported = true;
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x0002AF9D File Offset: 0x0002919D
		private void OnDestroy()
		{
			this.RemoveCreatedMaterials();
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x0002AFA5 File Offset: 0x000291A5
		private void RemoveCreatedMaterials()
		{
			while (this.createdMaterials.Count > 0)
			{
				Object @object = this.createdMaterials[0];
				this.createdMaterials.RemoveAt(0);
				Object.Destroy(@object);
			}
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x0002AFD4 File Offset: 0x000291D4
		protected bool CheckSupport()
		{
			return this.CheckSupport(false);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x0002AFDD File Offset: 0x000291DD
		public virtual bool CheckResources()
		{
			Debug.LogWarning("CheckResources () for " + this.ToString() + " should be overwritten.");
			return this.isSupported;
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x0002AFFF File Offset: 0x000291FF
		protected void Start()
		{
			this.CheckResources();
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x001AE42C File Offset: 0x001AC62C
		protected bool CheckSupport(bool needDepth)
		{
			this.isSupported = true;
			this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(2);
			this.supportDX11 = (SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders);
			if (!SystemInfo.supportsImageEffects)
			{
				this.NotSupported();
				return false;
			}
			if (needDepth && !SystemInfo.SupportsRenderTextureFormat(1))
			{
				this.NotSupported();
				return false;
			}
			if (needDepth)
			{
				base.GetComponent<Camera>().depthTextureMode |= 1;
			}
			return true;
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x0002B008 File Offset: 0x00029208
		protected bool CheckSupport(bool needDepth, bool needHdr)
		{
			if (!this.CheckSupport(needDepth))
			{
				return false;
			}
			if (needHdr && !this.supportHDRTextures)
			{
				this.NotSupported();
				return false;
			}
			return true;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x0002B029 File Offset: 0x00029229
		public bool Dx11Support()
		{
			return this.supportDX11;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x0002B031 File Offset: 0x00029231
		protected void ReportAutoDisable()
		{
			Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x001AE49C File Offset: 0x001AC69C
		private bool CheckShader(Shader s)
		{
			Debug.Log(string.Concat(new string[]
			{
				"The shader ",
				s.ToString(),
				" on effect ",
				this.ToString(),
				" is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package."
			}));
			if (!s.isSupported)
			{
				this.NotSupported();
				return false;
			}
			return false;
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x0002B04D File Offset: 0x0002924D
		protected void NotSupported()
		{
			base.enabled = false;
			this.isSupported = false;
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x001AE4F4 File Offset: 0x001AC6F4
		protected void DrawBorder(RenderTexture dest, Material material)
		{
			RenderTexture.active = dest;
			bool flag = true;
			GL.PushMatrix();
			GL.LoadOrtho();
			for (int i = 0; i < material.passCount; i++)
			{
				material.SetPass(i);
				float num;
				float num2;
				if (flag)
				{
					num = 1f;
					num2 = 0f;
				}
				else
				{
					num = 0f;
					num2 = 1f;
				}
				float num3 = 0f;
				float num4 = 0f + 1f / ((float)dest.width * 1f);
				float num5 = 0f;
				float num6 = 1f;
				GL.Begin(7);
				GL.TexCoord2(0f, num);
				GL.Vertex3(num3, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num3, num6, 0.1f);
				float num7 = 1f - 1f / ((float)dest.width * 1f);
				num4 = 1f;
				num5 = 0f;
				num6 = 1f;
				GL.TexCoord2(0f, num);
				GL.Vertex3(num7, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num7, num6, 0.1f);
				float num8 = 0f;
				num4 = 1f;
				num5 = 0f;
				num6 = 0f + 1f / ((float)dest.height * 1f);
				GL.TexCoord2(0f, num);
				GL.Vertex3(num8, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num8, num6, 0.1f);
				float num9 = 0f;
				num4 = 1f;
				num5 = 1f - 1f / ((float)dest.height * 1f);
				num6 = 1f;
				GL.TexCoord2(0f, num);
				GL.Vertex3(num9, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num9, num6, 0.1f);
				GL.End();
			}
			GL.PopMatrix();
		}

		// Token: 0x0400362D RID: 13869
		protected bool supportHDRTextures = true;

		// Token: 0x0400362E RID: 13870
		protected bool supportDX11;

		// Token: 0x0400362F RID: 13871
		protected bool isSupported = true;

		// Token: 0x04003630 RID: 13872
		private List<Material> createdMaterials = new List<Material>();
	}
}
