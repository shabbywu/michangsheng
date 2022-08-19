using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BD8 RID: 3032
	public abstract class MatchBase : Entity
	{
		// Token: 0x0600542A RID: 21546 RVA: 0x00220682 File Offset: 0x0021E882
		public MatchBase()
		{
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0023465C File Offset: 0x0023285C
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_MatchBase(this.id, this.className);
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x00234675 File Offset: 0x00232875
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_MatchBase(this.id, this.className);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0023468E File Offset: 0x0023288E
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00234697 File Offset: 0x00232897
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0023469F File Offset: 0x0023289F
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x002346A8 File Offset: 0x002328A8
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Match"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00234728 File Offset: 0x00232928
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Match"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				switch (property.properUtype)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				}
			}
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00234858 File Offset: 0x00232A58
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Match"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			Vector3 position = this.position;
			Property property2 = idpropertys[1];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
					return;
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
		}

		// Token: 0x0400507A RID: 20602
		public EntityBaseEntityCall_MatchBase baseEntityCall;

		// Token: 0x0400507B RID: 20603
		public EntityCellEntityCall_MatchBase cellEntityCall;
	}
}
