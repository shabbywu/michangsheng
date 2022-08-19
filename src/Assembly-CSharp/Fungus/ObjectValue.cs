using System;
using MarkerMetro.Unity.WinLegacy.Reflection;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DDE RID: 3550
	[Serializable]
	public class ObjectValue
	{
		// Token: 0x060064C0 RID: 25792 RVA: 0x00280744 File Offset: 0x0027E944
		public object GetValue()
		{
			string text = this.typeFullname;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1932852381U)
			{
				if (num <= 1657755862U)
				{
					if (num != 347085918U)
					{
						if (num == 1657755862U)
						{
							if (text == "UnityEngine.Vector3")
							{
								return this.vector3Value;
							}
						}
					}
					else if (text == "System.Boolean")
					{
						return this.boolValue;
					}
				}
				else if (num != 1674533481U)
				{
					if (num != 1798940239U)
					{
						if (num == 1932852381U)
						{
							if (text == "UnityEngine.Texture")
							{
								return this.textureValue;
							}
						}
					}
					else if (text == "UnityEngine.Material")
					{
						return this.materialValue;
					}
				}
				else if (text == "UnityEngine.Vector2")
				{
					return this.vector2Value;
				}
			}
			else if (num <= 3352368075U)
			{
				if (num != 2185383742U)
				{
					if (num != 2494097149U)
					{
						if (num == 3352368075U)
						{
							if (text == "UnityEngine.Sprite")
							{
								return this.spriteValue;
							}
						}
					}
					else if (text == "UnityEngine.Color")
					{
						return this.colorValue;
					}
				}
				else if (text == "System.Single")
				{
					return this.floatValue;
				}
			}
			else if (num != 4111882783U)
			{
				if (num != 4180476474U)
				{
					if (num == 4201364391U)
					{
						if (text == "System.String")
						{
							return this.stringValue;
						}
					}
				}
				else if (text == "System.Int32")
				{
					return this.intValue;
				}
			}
			else if (text == "UnityEngine.GameObject")
			{
				return this.gameObjectValue;
			}
			Type type = ReflectionHelper.GetType(this.typeAssemblyname);
			if (type.IsSubclassOf(typeof(Object)))
			{
				return this.objectValue;
			}
			if (type.IsEnum())
			{
				return Enum.ToObject(type, this.intValue);
			}
			return null;
		}

		// Token: 0x040056A0 RID: 22176
		public string typeAssemblyname;

		// Token: 0x040056A1 RID: 22177
		public string typeFullname;

		// Token: 0x040056A2 RID: 22178
		public int intValue;

		// Token: 0x040056A3 RID: 22179
		public bool boolValue;

		// Token: 0x040056A4 RID: 22180
		public float floatValue;

		// Token: 0x040056A5 RID: 22181
		public string stringValue;

		// Token: 0x040056A6 RID: 22182
		public Color colorValue;

		// Token: 0x040056A7 RID: 22183
		public GameObject gameObjectValue;

		// Token: 0x040056A8 RID: 22184
		public Material materialValue;

		// Token: 0x040056A9 RID: 22185
		public Object objectValue;

		// Token: 0x040056AA RID: 22186
		public Sprite spriteValue;

		// Token: 0x040056AB RID: 22187
		public Texture textureValue;

		// Token: 0x040056AC RID: 22188
		public Vector2 vector2Value;

		// Token: 0x040056AD RID: 22189
		public Vector3 vector3Value;
	}
}
