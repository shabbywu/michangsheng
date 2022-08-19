using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C62 RID: 3170
	public abstract class ShopBase : Entity
	{
		// Token: 0x06005624 RID: 22052 RVA: 0x00220682 File Offset: 0x0021E882
		public ShopBase()
		{
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0023CA68 File Offset: 0x0023AC68
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_ShopBase(this.id, this.className);
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0023CA81 File Offset: 0x0023AC81
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_ShopBase(this.id, this.className);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x0023CA9A File Offset: 0x0023AC9A
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x0023CAA3 File Offset: 0x0023ACA3
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x0023CAAB File Offset: 0x0023ACAB
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x0023CAB4 File Offset: 0x0023ACB4
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Shop"];
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

		// Token: 0x0600562F RID: 22063 RVA: 0x0023CB34 File Offset: 0x0023AD34
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Shop"];
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

		// Token: 0x06005630 RID: 22064 RVA: 0x0023CC64 File Offset: 0x0023AE64
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Shop"].idpropertys;
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

		// Token: 0x04005108 RID: 20744
		public EntityBaseEntityCall_ShopBase baseEntityCall;

		// Token: 0x04005109 RID: 20745
		public EntityCellEntityCall_ShopBase cellEntityCall;
	}
}
