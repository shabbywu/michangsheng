using System;
using System.Reflection;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F2 RID: 5106
	[CommandInfo("YSNew/Get", "GetAvatarData", "通过制定字符串获取玩家数据", 0)]
	[AddComponentMenu("")]
	public class GetAvatarData : Command
	{
		// Token: 0x06007C1D RID: 31773 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x002C4728 File Offset: 0x002C2928
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			FieldInfo[] fields = player.GetType().GetFields();
			int i = 0;
			while (i < fields.Length)
			{
				string name = fields[i].FieldType.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 1323747186U)
				{
					if (num <= 765439473U)
					{
						if (num != 697196164U)
						{
							if (num == 765439473U)
							{
								if (name == "Int16")
								{
									goto IL_12A;
								}
							}
						}
						else if (name == "Int64")
						{
							goto IL_12A;
						}
					}
					else if (num != 815609665U)
					{
						if (num == 1323747186U)
						{
							if (name == "UInt16")
							{
								goto IL_12A;
							}
						}
					}
					else if (name == "uInt")
					{
						goto IL_12A;
					}
				}
				else if (num <= 2711245919U)
				{
					if (num != 1324880019U)
					{
						if (num == 2711245919U)
						{
							if (name == "Int32")
							{
								goto IL_12A;
							}
						}
					}
					else if (name == "UInt64")
					{
						goto IL_12A;
					}
				}
				else if (num != 3538687084U)
				{
					if (num == 4168357374U)
					{
						if (name == "Int")
						{
							goto IL_12A;
						}
					}
				}
				else if (name == "UInt32")
				{
					goto IL_12A;
				}
				IL_158:
				i++;
				continue;
				IL_12A:
				if (this.StaticValueID == fields[i].Name)
				{
					this.TempValue.Value = Convert.ToInt32(fields[i].GetValue(player));
					goto IL_158;
				}
				goto IL_158;
			}
			this.Continue();
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A5B RID: 27227
		[Tooltip("变量的名称")]
		[SerializeField]
		protected string StaticValueID = "";

		// Token: 0x04006A5C RID: 27228
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
