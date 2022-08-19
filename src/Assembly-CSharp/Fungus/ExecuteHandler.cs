using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF6 RID: 3830
	public class ExecuteHandler : MonoBehaviour, IExecuteHandlerConfigurator
	{
		// Token: 0x06006BAC RID: 27564 RVA: 0x0029704E File Offset: 0x0029524E
		protected static string GetPath(Transform current)
		{
			if (current.parent == null)
			{
				return current.name;
			}
			return ExecuteHandler.GetPath(current.parent) + "." + current.name;
		}

		// Token: 0x06006BAD RID: 27565 RVA: 0x00297080 File Offset: 0x00295280
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

		// Token: 0x06006BAE RID: 27566 RVA: 0x002970BA File Offset: 0x002952BA
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

		// Token: 0x06006BAF RID: 27567 RVA: 0x002970C9 File Offset: 0x002952C9
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

		// Token: 0x06006BB0 RID: 27568 RVA: 0x00297103 File Offset: 0x00295303
		protected void OnDisable()
		{
			this.Execute(ExecuteMethod.OnDisable);
		}

		// Token: 0x06006BB1 RID: 27569 RVA: 0x00297110 File Offset: 0x00295310
		protected void OnEnable()
		{
			this.Execute(ExecuteMethod.OnEnable);
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x0029711A File Offset: 0x0029531A
		protected void OnDestroy()
		{
			this.Execute(ExecuteMethod.OnDestroy);
		}

		// Token: 0x06006BB3 RID: 27571 RVA: 0x00297124 File Offset: 0x00295324
		protected void Update()
		{
			if (this.IsExecuteMethodSelected(ExecuteMethod.Update) && this.ShouldExecuteOnFrame())
			{
				this.Execute(ExecuteMethod.Update);
			}
		}

		// Token: 0x06006BB4 RID: 27572 RVA: 0x0029713E File Offset: 0x0029533E
		protected void FixedUpdate()
		{
			this.Execute(ExecuteMethod.FixedUpdate);
		}

		// Token: 0x06006BB5 RID: 27573 RVA: 0x00297147 File Offset: 0x00295347
		protected void LateUpdate()
		{
			this.Execute(ExecuteMethod.LateUpdate);
		}

		// Token: 0x06006BB6 RID: 27574 RVA: 0x00297151 File Offset: 0x00295351
		protected void OnControllerColliderHit()
		{
			this.Execute(ExecuteMethod.OnControllerColliderHit);
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x0029715E File Offset: 0x0029535E
		protected void OnParticleCollision()
		{
			this.Execute(ExecuteMethod.OnParticleCollision);
		}

		// Token: 0x06006BB8 RID: 27576 RVA: 0x0029716B File Offset: 0x0029536B
		protected void OnJointBreak()
		{
			this.Execute(ExecuteMethod.OnJointBreak);
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x00297178 File Offset: 0x00295378
		protected void OnBecameInvisible()
		{
			this.Execute(ExecuteMethod.OnBecameInvisible);
		}

		// Token: 0x06006BBA RID: 27578 RVA: 0x00297185 File Offset: 0x00295385
		protected void OnBecameVisible()
		{
			this.Execute(ExecuteMethod.OnBecameVisible);
		}

		// Token: 0x06006BBB RID: 27579 RVA: 0x00297192 File Offset: 0x00295392
		protected void OnTriggerEnter()
		{
			this.Execute(ExecuteMethod.OnTriggerEnter);
		}

		// Token: 0x06006BBC RID: 27580 RVA: 0x0029719F File Offset: 0x0029539F
		protected void OnTriggerExit()
		{
			this.Execute(ExecuteMethod.OnTriggerExit);
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x002971AC File Offset: 0x002953AC
		protected void OnTriggerStay()
		{
			this.Execute(ExecuteMethod.OnTriggerStay);
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x002971B9 File Offset: 0x002953B9
		protected void OnCollisionEnter()
		{
			this.Execute(ExecuteMethod.OnCollisionEnter);
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x002971C6 File Offset: 0x002953C6
		protected void OnCollisionExit()
		{
			this.Execute(ExecuteMethod.OnCollisionExit);
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x002971D3 File Offset: 0x002953D3
		protected void OnCollisionStay()
		{
			this.Execute(ExecuteMethod.OnCollisionStay);
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x002971E0 File Offset: 0x002953E0
		protected void OnTriggerEnter2D()
		{
			this.Execute(ExecuteMethod.OnTriggerEnter2D);
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x002971ED File Offset: 0x002953ED
		protected void OnTriggerExit2D()
		{
			this.Execute(ExecuteMethod.OnTriggerExit2D);
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x002971FA File Offset: 0x002953FA
		protected void OnTriggerStay2D()
		{
			this.Execute(ExecuteMethod.OnTriggerStay2D);
		}

		// Token: 0x06006BC4 RID: 27588 RVA: 0x00297207 File Offset: 0x00295407
		protected void OnCollisionEnter2D()
		{
			this.Execute(ExecuteMethod.OnCollisionEnter2D);
		}

		// Token: 0x06006BC5 RID: 27589 RVA: 0x00297214 File Offset: 0x00295414
		protected void OnCollisionExit2D()
		{
			this.Execute(ExecuteMethod.OnCollisionExit2D);
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x00297221 File Offset: 0x00295421
		protected void OnCollisionStay2D()
		{
			this.Execute(ExecuteMethod.OnCollisionStay2D);
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x0029722E File Offset: 0x0029542E
		protected void Execute(ExecuteMethod executeMethod)
		{
			if (this.IsExecuteMethodSelected(executeMethod))
			{
				this.Execute();
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06006BC8 RID: 27592 RVA: 0x0029723F File Offset: 0x0029543F
		// (set) Token: 0x06006BC9 RID: 27593 RVA: 0x00297247 File Offset: 0x00295447
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

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06006BCA RID: 27594 RVA: 0x00297250 File Offset: 0x00295450
		// (set) Token: 0x06006BCB RID: 27595 RVA: 0x00297258 File Offset: 0x00295458
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

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06006BCC RID: 27596 RVA: 0x00297261 File Offset: 0x00295461
		// (set) Token: 0x06006BCD RID: 27597 RVA: 0x00297269 File Offset: 0x00295469
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

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06006BCE RID: 27598 RVA: 0x00297272 File Offset: 0x00295472
		// (set) Token: 0x06006BCF RID: 27599 RVA: 0x0029727A File Offset: 0x0029547A
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

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x00297283 File Offset: 0x00295483
		// (set) Token: 0x06006BD1 RID: 27601 RVA: 0x0029728B File Offset: 0x0029548B
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

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x00297294 File Offset: 0x00295494
		// (set) Token: 0x06006BD3 RID: 27603 RVA: 0x0029729C File Offset: 0x0029549C
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

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x002972A5 File Offset: 0x002954A5
		// (set) Token: 0x06006BD5 RID: 27605 RVA: 0x002972AD File Offset: 0x002954AD
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

		// Token: 0x06006BD6 RID: 27606 RVA: 0x002972B6 File Offset: 0x002954B6
		public virtual bool IsExecuteMethodSelected(ExecuteMethod method)
		{
			return method == (this.executeMethods & method);
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x002972C3 File Offset: 0x002954C3
		public virtual void Execute()
		{
			if (this.executeMethodName != "")
			{
				base.SendMessage(this.executeMethodName, 1);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (set) Token: 0x06006BD8 RID: 27608 RVA: 0x0029727A File Offset: 0x0029547A
		public int UpdateExecuteStartOnFrame
		{
			set
			{
				this.executeAfterFrames = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (set) Token: 0x06006BD9 RID: 27609 RVA: 0x0029729C File Offset: 0x0029549C
		public int UpdateExecuteRepeatFrequency
		{
			set
			{
				this.repeatEveryFrame = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (set) Token: 0x06006BDA RID: 27610 RVA: 0x0029728B File Offset: 0x0029548B
		public bool UpdateExecuteRepeat
		{
			set
			{
				this.repeatExecuteFrame = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (set) Token: 0x06006BDB RID: 27611 RVA: 0x00297247 File Offset: 0x00295447
		public float TimeExecuteStartAfter
		{
			set
			{
				this.executeAfterTime = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (set) Token: 0x06006BDC RID: 27612 RVA: 0x00297269 File Offset: 0x00295469
		public float TimeExecuteRepeatFrequency
		{
			set
			{
				this.repeatEveryTime = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (set) Token: 0x06006BDD RID: 27613 RVA: 0x00297258 File Offset: 0x00295458
		public bool TimeExecuteRepeat
		{
			set
			{
				this.repeatExecuteTime = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06006BDE RID: 27614 RVA: 0x00183D24 File Offset: 0x00181F24
		public ExecuteHandler Component
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04005AC3 RID: 23235
		[Tooltip("Execute after a period of time.")]
		[SerializeField]
		protected float executeAfterTime = 1f;

		// Token: 0x04005AC4 RID: 23236
		[Tooltip("Repeat execution after a period of time.")]
		[SerializeField]
		protected bool repeatExecuteTime = true;

		// Token: 0x04005AC5 RID: 23237
		[Tooltip("Repeat forever.")]
		[SerializeField]
		protected float repeatEveryTime = 1f;

		// Token: 0x04005AC6 RID: 23238
		[Tooltip("Execute after a number of frames have elapsed.")]
		[SerializeField]
		protected int executeAfterFrames = 1;

		// Token: 0x04005AC7 RID: 23239
		[Tooltip("Repeat execution after a number of frames have elapsed.")]
		[SerializeField]
		protected bool repeatExecuteFrame = true;

		// Token: 0x04005AC8 RID: 23240
		[Tooltip("Execute on every frame.")]
		[SerializeField]
		protected int repeatEveryFrame = 1;

		// Token: 0x04005AC9 RID: 23241
		[Tooltip("The bitmask for the currently selected execution methods.")]
		[SerializeField]
		protected ExecuteMethod executeMethods = ExecuteMethod.Start;

		// Token: 0x04005ACA RID: 23242
		[Tooltip("Name of the method on a component in this gameobject to call when executing.")]
		[SerializeField]
		protected string executeMethodName = "OnExecute";

		// Token: 0x04005ACB RID: 23243
		protected int m_ExecuteOnFrame;
	}
}
