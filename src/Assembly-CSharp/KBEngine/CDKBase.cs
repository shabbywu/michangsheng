using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EAF RID: 3759
	public abstract class CDKBase : Entity
	{
		// Token: 0x06005ACF RID: 23247 RVA: 0x00040112 File Offset: 0x0003E312
		public CDKBase()
		{
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x0004011A File Offset: 0x0003E31A
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_CDKBase(this.id, this.className);
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x00040133 File Offset: 0x0003E333
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_CDKBase(this.id, this.className);
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x0004014C File Offset: 0x0003E34C
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x00040155 File Offset: 0x0003E355
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0004015D File Offset: 0x0003E35D
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0024FF60 File Offset: 0x0024E160
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDK"];
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

		// Token: 0x06005ADA RID: 23258 RVA: 0x0024FFE0 File Offset: 0x0024E1E0
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDK"];
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

		// Token: 0x06005ADB RID: 23259 RVA: 0x00250110 File Offset: 0x0024E310
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["CDK"].idpropertys;
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

		// Token: 0x040059FC RID: 23036
		public EntityBaseEntityCall_CDKBase baseEntityCall;

		// Token: 0x040059FD RID: 23037
		public EntityCellEntityCall_CDKBase cellEntityCall;
	}
}
