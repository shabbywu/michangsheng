using System;

namespace KBEngine
{
	// Token: 0x0200101D RID: 4125
	public class GameObject : if_Entity_error_use______git_submodule_update_____kbengine_plugins_______open_this_file_and_I_will_tell_you
	{
		// Token: 0x06006292 RID: 25234 RVA: 0x00272248 File Offset: 0x00270448
		public virtual void onHPChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP");
			Event.fireOut("set_HP", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x00043EC5 File Offset: 0x000420C5
		public virtual void onMPChanged(int oldValue)
		{
			this.getDefinedProperty("MP");
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x0027227C File Offset: 0x0027047C
		public virtual void onHP_MaxChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP_Max");
			Event.fireOut("set_HP_Max", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x00043ED3 File Offset: 0x000420D3
		public virtual void onMP_MaxChanged(int oldValue)
		{
			this.getDefinedProperty("MP_Max");
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x00274E48 File Offset: 0x00273048
		public virtual void onLevelChanged(ushort oldValue)
		{
			object definedProperty = this.getDefinedProperty("level");
			Event.fireOut("set_level", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x002722B0 File Offset: 0x002704B0
		public virtual void onNameChanged(string oldValue)
		{
			object definedProperty = this.getDefinedProperty("name");
			Event.fireOut("set_name", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x002722E4 File Offset: 0x002704E4
		public virtual void onStateChanged(sbyte oldValue)
		{
			object definedProperty = this.getDefinedProperty("state");
			Event.fireOut("set_state", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSpaceUTypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x00043EE1 File Offset: 0x000420E1
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
			this.getDefinedProperty("moveSpeed");
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x0004439D File Offset: 0x0004259D
		public virtual void set_modelScale(object old)
		{
			this.getDefinedProperty("modelScale");
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x000443AB File Offset: 0x000425AB
		public virtual void set_modelID(object old)
		{
			this.getDefinedProperty("modelID");
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void set_forbids(object old)
		{
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x0011EE44 File Offset: 0x0011D044
		public virtual void recvDamage(int attackerID, int skillID, int damageType, int damage)
		{
			Entity entity = KBEngineApp.app.findEntity(attackerID);
			Event.fireOut("recvDamage", new object[]
			{
				this,
				entity,
				skillID,
				damageType,
				damage
			});
		}
	}
}
