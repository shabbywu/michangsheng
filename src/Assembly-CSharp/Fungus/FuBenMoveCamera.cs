using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C3 RID: 5059
	[CommandInfo("YSFuBen", "FuBenMoveCamera", "移动摄像机位置", 0)]
	[AddComponentMenu("")]
	public class FuBenMoveCamera : Command
	{
		// Token: 0x06007B54 RID: 31572 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x002C38EC File Offset: 0x002C1AEC
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(this.MapID.Value))
			{
				Component component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
				this.wait();
				Vector2 vector = AllMapManage.instance.mapIndex[this.MapID.Value].transform.position;
				TweenSettingsExtensions.SetSpeedBased<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(component.transform, vector, 30f, false)).onComplete = delegate()
				{
					this.removeWait();
				};
			}
			this.Continue();
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x002C3814 File Offset: 0x002C1A14
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x002C37E4 File Offset: 0x002C19E4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069EF RID: 27119
		[Tooltip("场景名称")]
		[SerializeField]
		protected string ScenceName;

		// Token: 0x040069F0 RID: 27120
		[Tooltip("摄像机移动地点的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MapID;

		// Token: 0x040069F1 RID: 27121
		[VariableProperty(new Type[]
		{
			typeof(FloatVariable)
		})]
		[Tooltip("摄像机移动速度")]
		[SerializeField]
		protected FloatVariable speed;
	}
}
