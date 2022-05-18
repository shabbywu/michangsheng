using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus
{
	// Token: 0x0200121C RID: 4636
	[CommandInfo("Scripting", "Invoke Method", "Invokes a method of a component via reflection. Supports passing multiple parameters and storing returned values in a Fungus variable.", 0)]
	public class InvokeMethod : Command
	{
		// Token: 0x0600713B RID: 28987 RVA: 0x002A49B8 File Offset: 0x002A2BB8
		protected virtual void Awake()
		{
			if (this.componentType == null)
			{
				this.componentType = ReflectionHelper.GetType(this.targetComponentAssemblyName);
			}
			if (this.objComponent == null)
			{
				this.objComponent = this.targetObject.GetComponent(this.componentType);
			}
			if (this.parameterTypes == null)
			{
				this.parameterTypes = this.GetParameterTypes();
			}
			if (this.objMethod == null)
			{
				this.objMethod = UnityEventBase.GetValidMethodInfo(this.objComponent, this.targetMethod, this.parameterTypes);
			}
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x0004CEF9 File Offset: 0x0004B0F9
		protected virtual IEnumerator ExecuteCoroutine()
		{
			yield return base.StartCoroutine((IEnumerator)this.objMethod.Invoke(this.objComponent, this.GetParameterValues()));
			if (this.callMode == CallMode.WaitUntilFinished)
			{
				this.Continue();
			}
			yield break;
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x002A4A48 File Offset: 0x002A2C48
		protected virtual Type[] GetParameterTypes()
		{
			Type[] array = new Type[this.methodParameters.Length];
			for (int i = 0; i < this.methodParameters.Length; i++)
			{
				Type type = ReflectionHelper.GetType(this.methodParameters[i].objValue.typeAssemblyname);
				array[i] = type;
			}
			return array;
		}

		// Token: 0x0600713E RID: 28990 RVA: 0x002A4A94 File Offset: 0x002A2C94
		protected virtual object[] GetParameterValues()
		{
			object[] array = new object[this.methodParameters.Length];
			Flowchart flowchart = this.GetFlowchart();
			for (int i = 0; i < this.methodParameters.Length; i++)
			{
				InvokeMethodParameter invokeMethodParameter = this.methodParameters[i];
				if (string.IsNullOrEmpty(invokeMethodParameter.variableKey))
				{
					array[i] = invokeMethodParameter.objValue.GetValue();
				}
				else
				{
					object obj = null;
					string typeFullname = invokeMethodParameter.objValue.typeFullname;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(typeFullname);
					if (num <= 1932852381U)
					{
						if (num <= 1657755862U)
						{
							if (num != 347085918U)
							{
								if (num != 1657755862U)
								{
									goto IL_3C9;
								}
								if (!(typeFullname == "UnityEngine.Vector3"))
								{
									goto IL_3C9;
								}
								Vector3Variable variable = flowchart.GetVariable<Vector3Variable>(invokeMethodParameter.variableKey);
								if (variable != null)
								{
									obj = variable.Value;
								}
							}
							else
							{
								if (!(typeFullname == "System.Boolean"))
								{
									goto IL_3C9;
								}
								BooleanVariable variable2 = flowchart.GetVariable<BooleanVariable>(invokeMethodParameter.variableKey);
								if (variable2 != null)
								{
									obj = variable2.Value;
								}
							}
						}
						else if (num != 1674533481U)
						{
							if (num != 1798940239U)
							{
								if (num != 1932852381U)
								{
									goto IL_3C9;
								}
								if (!(typeFullname == "UnityEngine.Texture"))
								{
									goto IL_3C9;
								}
								TextureVariable variable3 = flowchart.GetVariable<TextureVariable>(invokeMethodParameter.variableKey);
								if (variable3 != null)
								{
									obj = variable3.Value;
								}
							}
							else
							{
								if (!(typeFullname == "UnityEngine.Material"))
								{
									goto IL_3C9;
								}
								MaterialVariable variable4 = flowchart.GetVariable<MaterialVariable>(invokeMethodParameter.variableKey);
								if (variable4 != null)
								{
									obj = variable4.Value;
								}
							}
						}
						else
						{
							if (!(typeFullname == "UnityEngine.Vector2"))
							{
								goto IL_3C9;
							}
							Vector2Variable variable5 = flowchart.GetVariable<Vector2Variable>(invokeMethodParameter.variableKey);
							if (variable5 != null)
							{
								obj = variable5.Value;
							}
						}
					}
					else if (num <= 3352368075U)
					{
						if (num != 2185383742U)
						{
							if (num != 2494097149U)
							{
								if (num != 3352368075U)
								{
									goto IL_3C9;
								}
								if (!(typeFullname == "UnityEngine.Sprite"))
								{
									goto IL_3C9;
								}
								SpriteVariable variable6 = flowchart.GetVariable<SpriteVariable>(invokeMethodParameter.variableKey);
								if (variable6 != null)
								{
									obj = variable6.Value;
								}
							}
							else
							{
								if (!(typeFullname == "UnityEngine.Color"))
								{
									goto IL_3C9;
								}
								ColorVariable variable7 = flowchart.GetVariable<ColorVariable>(invokeMethodParameter.variableKey);
								if (variable7 != null)
								{
									obj = variable7.Value;
								}
							}
						}
						else
						{
							if (!(typeFullname == "System.Single"))
							{
								goto IL_3C9;
							}
							FloatVariable variable8 = flowchart.GetVariable<FloatVariable>(invokeMethodParameter.variableKey);
							if (variable8 != null)
							{
								obj = variable8.Value;
							}
						}
					}
					else if (num != 4111882783U)
					{
						if (num != 4180476474U)
						{
							if (num != 4201364391U)
							{
								goto IL_3C9;
							}
							if (!(typeFullname == "System.String"))
							{
								goto IL_3C9;
							}
							StringVariable variable9 = flowchart.GetVariable<StringVariable>(invokeMethodParameter.variableKey);
							if (variable9 != null)
							{
								obj = variable9.Value;
							}
						}
						else
						{
							if (!(typeFullname == "System.Int32"))
							{
								goto IL_3C9;
							}
							IntegerVariable variable10 = flowchart.GetVariable<IntegerVariable>(invokeMethodParameter.variableKey);
							if (variable10 != null)
							{
								obj = variable10.Value;
							}
						}
					}
					else
					{
						if (!(typeFullname == "UnityEngine.GameObject"))
						{
							goto IL_3C9;
						}
						GameObjectVariable variable11 = flowchart.GetVariable<GameObjectVariable>(invokeMethodParameter.variableKey);
						if (variable11 != null)
						{
							obj = variable11.Value;
						}
					}
					IL_3EA:
					array[i] = obj;
					goto IL_3EF;
					IL_3C9:
					ObjectVariable variable12 = flowchart.GetVariable<ObjectVariable>(invokeMethodParameter.variableKey);
					if (variable12 != null)
					{
						obj = variable12.Value;
						goto IL_3EA;
					}
					goto IL_3EA;
				}
				IL_3EF:;
			}
			return array;
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x002A4EA4 File Offset: 0x002A30A4
		protected virtual void SetVariable(string key, object value, string returnType)
		{
			Flowchart flowchart = this.GetFlowchart();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(returnType);
			if (num <= 1932852381U)
			{
				if (num <= 1657755862U)
				{
					if (num != 347085918U)
					{
						if (num == 1657755862U)
						{
							if (returnType == "UnityEngine.Vector3")
							{
								flowchart.GetVariable<Vector3Variable>(key).Value = (Vector3)value;
								return;
							}
						}
					}
					else if (returnType == "System.Boolean")
					{
						flowchart.GetVariable<BooleanVariable>(key).Value = (bool)value;
						return;
					}
				}
				else if (num != 1674533481U)
				{
					if (num != 1798940239U)
					{
						if (num == 1932852381U)
						{
							if (returnType == "UnityEngine.Texture")
							{
								flowchart.GetVariable<TextureVariable>(key).Value = (Texture)value;
								return;
							}
						}
					}
					else if (returnType == "UnityEngine.Material")
					{
						flowchart.GetVariable<MaterialVariable>(key).Value = (Material)value;
						return;
					}
				}
				else if (returnType == "UnityEngine.Vector2")
				{
					flowchart.GetVariable<Vector2Variable>(key).Value = (Vector2)value;
					return;
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
							if (returnType == "UnityEngine.Sprite")
							{
								flowchart.GetVariable<SpriteVariable>(key).Value = (Sprite)value;
								return;
							}
						}
					}
					else if (returnType == "UnityEngine.Color")
					{
						flowchart.GetVariable<ColorVariable>(key).Value = (Color)value;
						return;
					}
				}
				else if (returnType == "System.Single")
				{
					flowchart.GetVariable<FloatVariable>(key).Value = (float)value;
					return;
				}
			}
			else if (num != 4111882783U)
			{
				if (num != 4180476474U)
				{
					if (num == 4201364391U)
					{
						if (returnType == "System.String")
						{
							flowchart.GetVariable<StringVariable>(key).Value = (string)value;
							return;
						}
					}
				}
				else if (returnType == "System.Int32")
				{
					flowchart.GetVariable<IntegerVariable>(key).Value = (int)value;
					return;
				}
			}
			else if (returnType == "UnityEngine.GameObject")
			{
				flowchart.GetVariable<GameObjectVariable>(key).Value = (GameObject)value;
				return;
			}
			flowchart.GetVariable<ObjectVariable>(key).Value = (Object)value;
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06007140 RID: 28992 RVA: 0x0004CF08 File Offset: 0x0004B108
		public virtual GameObject TargetObject
		{
			get
			{
				return this.targetObject;
			}
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x002A5124 File Offset: 0x002A3324
		public override void OnEnter()
		{
			try
			{
				if (this.targetObject == null || string.IsNullOrEmpty(this.targetComponentAssemblyName) || string.IsNullOrEmpty(this.targetMethod))
				{
					this.Continue();
				}
				else if (this.returnValueType != "System.Collections.IEnumerator")
				{
					object value = this.objMethod.Invoke(this.objComponent, this.GetParameterValues());
					if (this.saveReturnValue)
					{
						this.SetVariable(this.returnValueVariableKey, value, this.returnValueType);
					}
					this.Continue();
				}
				else
				{
					base.StartCoroutine(this.ExecuteCoroutine());
					if (this.callMode == CallMode.Continue)
					{
						this.Continue();
					}
					else if (this.callMode == CallMode.Stop)
					{
						this.StopParentBlock();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error: " + ex.Message);
			}
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007143 RID: 28995 RVA: 0x002A5208 File Offset: 0x002A3408
		public override string GetSummary()
		{
			if (this.targetObject == null)
			{
				return "Error: targetObject is not assigned";
			}
			if (!string.IsNullOrEmpty(this.description))
			{
				return this.description;
			}
			return string.Concat(new string[]
			{
				this.targetObject.name,
				".",
				this.targetComponentText,
				".",
				this.targetMethodText
			});
		}

		// Token: 0x04006391 RID: 25489
		[Tooltip("A description of what this command does. Appears in the command summary.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04006392 RID: 25490
		[Tooltip("GameObject containing the component method to be invoked")]
		[SerializeField]
		protected GameObject targetObject;

		// Token: 0x04006393 RID: 25491
		[HideInInspector]
		[Tooltip("Name of assembly containing the target component")]
		[SerializeField]
		protected string targetComponentAssemblyName;

		// Token: 0x04006394 RID: 25492
		[HideInInspector]
		[Tooltip("Full name of the target component")]
		[SerializeField]
		protected string targetComponentFullname;

		// Token: 0x04006395 RID: 25493
		[HideInInspector]
		[Tooltip("Display name of the target component")]
		[SerializeField]
		protected string targetComponentText;

		// Token: 0x04006396 RID: 25494
		[HideInInspector]
		[Tooltip("Name of target method to invoke on the target component")]
		[SerializeField]
		protected string targetMethod;

		// Token: 0x04006397 RID: 25495
		[HideInInspector]
		[Tooltip("Display name of target method to invoke on the target component")]
		[SerializeField]
		protected string targetMethodText;

		// Token: 0x04006398 RID: 25496
		[HideInInspector]
		[Tooltip("List of parameters to pass to the invoked method")]
		[SerializeField]
		protected InvokeMethodParameter[] methodParameters;

		// Token: 0x04006399 RID: 25497
		[HideInInspector]
		[Tooltip("If true, store the return value in a flowchart variable of the same type.")]
		[SerializeField]
		protected bool saveReturnValue;

		// Token: 0x0400639A RID: 25498
		[HideInInspector]
		[Tooltip("Name of Fungus variable to store the return value in")]
		[SerializeField]
		protected string returnValueVariableKey;

		// Token: 0x0400639B RID: 25499
		[HideInInspector]
		[Tooltip("The type of the return value")]
		[SerializeField]
		protected string returnValueType;

		// Token: 0x0400639C RID: 25500
		[HideInInspector]
		[Tooltip("If true, list all inherited methods for the component")]
		[SerializeField]
		protected bool showInherited;

		// Token: 0x0400639D RID: 25501
		[HideInInspector]
		[Tooltip("The coroutine call behavior for methods that return IEnumerator")]
		[SerializeField]
		protected CallMode callMode;

		// Token: 0x0400639E RID: 25502
		protected Type componentType;

		// Token: 0x0400639F RID: 25503
		protected Component objComponent;

		// Token: 0x040063A0 RID: 25504
		protected Type[] parameterTypes;

		// Token: 0x040063A1 RID: 25505
		protected MethodInfo objMethod;
	}
}
