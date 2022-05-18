using System;
using MarkerMetro.Unity.WinLegacy.Reflection;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200121F RID: 4639
	[Serializable]
	public class ObjectValue
	{
		// Token: 0x0600714C RID: 29004 RVA: 0x002A52EC File Offset: 0x002A34EC
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

		// Token: 0x040063A7 RID: 25511
		public string typeAssemblyname;

		// Token: 0x040063A8 RID: 25512
		public string typeFullname;

		// Token: 0x040063A9 RID: 25513
		public int intValue;

		// Token: 0x040063AA RID: 25514
		public bool boolValue;

		// Token: 0x040063AB RID: 25515
		public float floatValue;

		// Token: 0x040063AC RID: 25516
		public string stringValue;

		// Token: 0x040063AD RID: 25517
		public Color colorValue;

		// Token: 0x040063AE RID: 25518
		public GameObject gameObjectValue;

		// Token: 0x040063AF RID: 25519
		public Material materialValue;

		// Token: 0x040063B0 RID: 25520
		public Object objectValue;

		// Token: 0x040063B1 RID: 25521
		public Sprite spriteValue;

		// Token: 0x040063B2 RID: 25522
		public Texture textureValue;

		// Token: 0x040063B3 RID: 25523
		public Vector2 vector2Value;

		// Token: 0x040063B4 RID: 25524
		public Vector3 vector3Value;
	}
}
