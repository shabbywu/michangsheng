using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001397 RID: 5015
	public class ExecuteHandler : MonoBehaviour, IExecuteHandlerConfigurator
	{
		// Token: 0x0600794B RID: 31051 RVA: 0x00052CD6 File Offset: 0x00050ED6
		protected static string GetPath(Transform current)
		{
			if (current.parent == null)
			{
				return current.name;
			}
			return ExecuteHandler.GetPath(current.parent) + "." + current.name;
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x00052D08 File Offset: 0x00050F08
		protected void Start()
		{
			this.Execute(ExecuteMethod.Start);
			if (this.IsExecuteMethodSelected(ExecuteMethod.AfterPeriodOfTime))
			{
				base.StartCoroutine(this.ExecutePeriodically());
			}
			if (this.IsExecuteMethodSelected(ExecuteMethod.Update))
			{
				this.m_ExecuteOnFrame = Time.frameCount + this.executeAfterFrames;
			}
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x00052D42 File Offset: 0x00050F42
		protected IEnumerator ExecutePeriodically()
		{
			yield return new WaitForSeconds(this.executeAfterTime);
			this.Execute(ExecuteMethod.AfterPeriodOfTime);
			while (this.repeatExecuteTime)
			{
				yield return new WaitForSeconds(this.repeatEveryTime);
				this.Execute(ExecuteMethod.AfterPeriodOfTime);
			}
			yield break;
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x00052D51 File Offset: 0x00050F51
		protected bool ShouldExecuteOnFrame()
		{
			if (Time.frameCount > this.m_ExecuteOnFrame)
			{
				if (this.repeatExecuteFrame)
				{
					this.m_ExecuteOnFrame += this.repeatEveryFrame;
				}
				else
				{
					this.m_ExecuteOnFrame = int.MaxValue;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x00052D8B File Offset: 0x00050F8B
		protected void OnDisable()
		{
			this.Execute(ExecuteMethod.OnDisable);
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x00052D98 File Offset: 0x00050F98
		protected void OnEnable()
		{
			this.Execute(ExecuteMethod.OnEnable);
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x00052DA2 File Offset: 0x00050FA2
		protected void OnDestroy()
		{
			this.Execute(ExecuteMethod.OnDestroy);
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x00052DAC File Offset: 0x00050FAC
		protected void Update()
		{
			if (this.IsExecuteMethodSelected(ExecuteMethod.Update) && this.ShouldExecuteOnFrame())
			{
				this.Execute(ExecuteMethod.Update);
			}
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x00052DC6 File Offset: 0x00050FC6
		protected void FixedUpdate()
		{
			this.Execute(ExecuteMethod.FixedUpdate);
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x00052DCF File Offset: 0x00050FCF
		protected void LateUpdate()
		{
			this.Execute(ExecuteMethod.LateUpdate);
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x00052DD9 File Offset: 0x00050FD9
		protected void OnControllerColliderHit()
		{
			this.Execute(ExecuteMethod.OnControllerColliderHit);
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x00052DE6 File Offset: 0x00050FE6
		protected void OnParticleCollision()
		{
			this.Execute(ExecuteMethod.OnParticleCollision);
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x00052DF3 File Offset: 0x00050FF3
		protected void OnJointBreak()
		{
			this.Execute(ExecuteMethod.OnJointBreak);
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x00052E00 File Offset: 0x00051000
		protected void OnBecameInvisible()
		{
			this.Execute(ExecuteMethod.OnBecameInvisible);
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x00052E0D File Offset: 0x0005100D
		protected void OnBecameVisible()
		{
			this.Execute(ExecuteMethod.OnBecameVisible);
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x00052E1A File Offset: 0x0005101A
		protected void OnTriggerEnter()
		{
			this.Execute(ExecuteMethod.OnTriggerEnter);
		}

		// Token: 0x0600795B RID: 31067 RVA: 0x00052E27 File Offset: 0x00051027
		protected void OnTriggerExit()
		{
			this.Execute(ExecuteMethod.OnTriggerExit);
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x00052E34 File Offset: 0x00051034
		protected void OnTriggerStay()
		{
			this.Execute(ExecuteMethod.OnTriggerStay);
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x00052E41 File Offset: 0x00051041
		protected void OnCollisionEnter()
		{
			this.Execute(ExecuteMethod.OnCollisionEnter);
		}

		// Token: 0x0600795E RID: 31070 RVA: 0x00052E4E File Offset: 0x0005104E
		protected void OnCollisionExit()
		{
			this.Execute(ExecuteMethod.OnCollisionExit);
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x00052E5B File Offset: 0x0005105B
		protected void OnCollisionStay()
		{
			this.Execute(ExecuteMethod.OnCollisionStay);
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x00052E68 File Offset: 0x00051068
		protected void OnTriggerEnter2D()
		{
			this.Execute(ExecuteMethod.OnTriggerEnter2D);
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x00052E75 File Offset: 0x00051075
		protected void OnTriggerExit2D()
		{
			this.Execute(ExecuteMethod.OnTriggerExit2D);
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x00052E82 File Offset: 0x00051082
		protected void OnTriggerStay2D()
		{
			this.Execute(ExecuteMethod.OnTriggerStay2D);
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x00052E8F File Offset: 0x0005108F
		protected void OnCollisionEnter2D()
		{
			this.Execute(ExecuteMethod.OnCollisionEnter2D);
		}

		// Token: 0x06007964 RID: 31076 RVA: 0x00052E9C File Offset: 0x0005109C
		protected void OnCollisionExit2D()
		{
			this.Execute(ExecuteMethod.OnCollisionExit2D);
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x00052EA9 File Offset: 0x000510A9
		protected void OnCollisionStay2D()
		{
			this.Execute(ExecuteMethod.OnCollisionStay2D);
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x00052EB6 File Offset: 0x000510B6
		protected void Execute(ExecuteMethod executeMethod)
		{
			if (this.IsExecuteMethodSelected(executeMethod))
			{
				this.Execute();
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06007967 RID: 31079 RVA: 0x00052EC7 File Offset: 0x000510C7
		// (set) Token: 0x06007968 RID: 31080 RVA: 0x00052ECF File Offset: 0x000510CF
		public virtual float ExecuteAfterTime
		{
			get
			{
				return this.executeAfterTime;
			}
			set
			{
				this.executeAfterTime = value;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06007969 RID: 31081 RVA: 0x00052ED8 File Offset: 0x000510D8
		// (set) Token: 0x0600796A RID: 31082 RVA: 0x00052EE0 File Offset: 0x000510E0
		public virtual bool RepeatExecuteTime
		{
			get
			{
				return this.repeatExecuteTime;
			}
			set
			{
				this.repeatExecuteTime = value;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600796B RID: 31083 RVA: 0x00052EE9 File Offset: 0x000510E9
		// (set) Token: 0x0600796C RID: 31084 RVA: 0x00052EF1 File Offset: 0x000510F1
		public virtual float RepeatEveryTime
		{
			get
			{
				return this.repeatEveryTime;
			}
			set
			{
				this.repeatEveryTime = value;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600796D RID: 31085 RVA: 0x00052EFA File Offset: 0x000510FA
		// (set) Token: 0x0600796E RID: 31086 RVA: 0x00052F02 File Offset: 0x00051102
		public virtual int ExecuteAfterFrames
		{
			get
			{
				return this.executeAfterFrames;
			}
			set
			{
				this.executeAfterFrames = value;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600796F RID: 31087 RVA: 0x00052F0B File Offset: 0x0005110B
		// (set) Token: 0x06007970 RID: 31088 RVA: 0x00052F13 File Offset: 0x00051113
		public virtual bool RepeatExecuteFrame
		{
			get
			{
				return this.repeatExecuteFrame;
			}
			set
			{
				this.repeatExecuteFrame = value;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06007971 RID: 31089 RVA: 0x00052F1C File Offset: 0x0005111C
		// (set) Token: 0x06007972 RID: 31090 RVA: 0x00052F24 File Offset: 0x00051124
		public virtual int RepeatEveryFrame
		{
			get
			{
				return this.repeatEveryFrame;
			}
			set
			{
				this.repeatEveryFrame = value;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06007973 RID: 31091 RVA: 0x00052F2D File Offset: 0x0005112D
		// (set) Token: 0x06007974 RID: 31092 RVA: 0x00052F35 File Offset: 0x00051135
		public virtual ExecuteMethod ExecuteMethods
		{
			get
			{
				return this.executeMethods;
			}
			set
			{
				this.executeMethods = value;
			}
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x00052F3E File Offset: 0x0005113E
		public virtual bool IsExecuteMethodSelected(ExecuteMethod method)
		{
			return method == (this.executeMethods & method);
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x00052F4B File Offset: 0x0005114B
		public virtual void Execute()
		{
			if (this.executeMethodName != "")
			{
				base.SendMessage(this.executeMethodName, 1);
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (set) Token: 0x06007977 RID: 31095 RVA: 0x00052F02 File Offset: 0x00051102
		public int UpdateExecuteStartOnFrame
		{
			set
			{
				this.executeAfterFrames = value;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (set) Token: 0x06007978 RID: 31096 RVA: 0x00052F24 File Offset: 0x00051124
		public int UpdateExecuteRepeatFrequency
		{
			set
			{
				this.repeatEveryFrame = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (set) Token: 0x06007979 RID: 31097 RVA: 0x00052F13 File Offset: 0x00051113
		public bool UpdateExecuteRepeat
		{
			set
			{
				this.repeatExecuteFrame = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (set) Token: 0x0600797A RID: 31098 RVA: 0x00052ECF File Offset: 0x000510CF
		public float TimeExecuteStartAfter
		{
			set
			{
				this.executeAfterTime = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (set) Token: 0x0600797B RID: 31099 RVA: 0x00052EF1 File Offset: 0x000510F1
		public float TimeExecuteRepeatFrequency
		{
			set
			{
				this.repeatEveryTime = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (set) Token: 0x0600797C RID: 31100 RVA: 0x00052EE0 File Offset: 0x000510E0
		public bool TimeExecuteRepeat
		{
			set
			{
				this.repeatExecuteTime = value;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600797D RID: 31101 RVA: 0x0002FB09 File Offset: 0x0002DD09
		public ExecuteHandler Component
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400692C RID: 26924
		[Tooltip("Execute after a period of time.")]
		[SerializeField]
		protected float executeAfterTime = 1f;

		// Token: 0x0400692D RID: 26925
		[Tooltip("Repeat execution after a period of time.")]
		[SerializeField]
		protected bool repeatExecuteTime = true;

		// Token: 0x0400692E RID: 26926
		[Tooltip("Repeat forever.")]
		[SerializeField]
		protected float repeatEveryTime = 1f;

		// Token: 0x0400692F RID: 26927
		[Tooltip("Execute after a number of frames have elapsed.")]
		[SerializeField]
		protected int executeAfterFrames = 1;

		// Token: 0x04006930 RID: 26928
		[Tooltip("Repeat execution after a number of frames have elapsed.")]
		[SerializeField]
		protected bool repeatExecuteFrame = true;

		// Token: 0x04006931 RID: 26929
		[Tooltip("Execute on every frame.")]
		[SerializeField]
		protected int repeatEveryFrame = 1;

		// Token: 0x04006932 RID: 26930
		[Tooltip("The bitmask for the currently selected execution methods.")]
		[SerializeField]
		protected ExecuteMethod executeMethods = ExecuteMethod.Start;

		// Token: 0x04006933 RID: 26931
		[Tooltip("Name of the method on a component in this gameobject to call when executing.")]
		[SerializeField]
		protected string executeMethodName = "OnExecute";

		// Token: 0x04006934 RID: 26932
		protected int m_ExecuteOnFrame;
	}
}
