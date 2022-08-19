using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x0200062F RID: 1583
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class ImageEffectBase : MonoBehaviour
	{
		// Token: 0x06003232 RID: 12850 RVA: 0x00164AA8 File Offset: 0x00162CA8
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

		// Token: 0x06003233 RID: 12851 RVA: 0x00164B64 File Offset: 0x00162D64
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

		// Token: 0x06003234 RID: 12852 RVA: 0x00164BD7 File Offset: 0x00162DD7
		private void OnEnable()
		{
			this.isSupported = true;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x00164BE0 File Offset: 0x00162DE0
		private void OnDestroy()
		{
			this.RemoveCreatedMaterials();
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x00164BE8 File Offset: 0x00162DE8
		private void RemoveCreatedMaterials()
		{
			while (this.createdMaterials.Count > 0)
			{
				Object @object = this.createdMaterials[0];
				this.createdMaterials.RemoveAt(0);
				Object.Destroy(@object);
			}
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x00164C17 File Offset: 0x00162E17
		protected bool CheckSupport()
		{
			return this.CheckSupport(false);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x00164C20 File Offset: 0x00162E20
		public virtual bool CheckResources()
		{
			Debug.LogWarning("CheckResources () for " + this.ToString() + " should be overwritten.");
			return this.isSupported;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00164C42 File Offset: 0x00162E42
		protected void Start()
		{
			this.CheckResources();
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x00164C4C File Offset: 0x00162E4C
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

		// Token: 0x0600323B RID: 12859 RVA: 0x00164CBC File Offset: 0x00162EBC
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

		// Token: 0x0600323C RID: 12860 RVA: 0x00164CDD File Offset: 0x00162EDD
		public bool Dx11Support()
		{
			return this.supportDX11;
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x00164CE5 File Offset: 0x00162EE5
		protected void ReportAutoDisable()
		{
			Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x00164D04 File Offset: 0x00162F04
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

		// Token: 0x0600323F RID: 12863 RVA: 0x00164D5C File Offset: 0x00162F5C
		protected void NotSupported()
		{
			base.enabled = false;
			this.isSupported = false;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x00164D6C File Offset: 0x00162F6C
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

		// Token: 0x04002CDC RID: 11484
		protected bool supportHDRTextures = true;

		// Token: 0x04002CDD RID: 11485
		protected bool supportDX11;

		// Token: 0x04002CDE RID: 11486
		protected bool isSupported = true;

		// Token: 0x04002CDF RID: 11487
		private List<Material> createdMaterials = new List<Material>();
	}
}
