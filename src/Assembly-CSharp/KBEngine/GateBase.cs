using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F16 RID: 3862
	public abstract class GateBase : Entity
	{
		// Token: 0x06005CA4 RID: 23716 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x0004163B File Offset: 0x0003F83B
		public GateBase()
		{
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x00041656 File Offset: 0x0003F856
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_GateBase(this.id, this.className);
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x0004166F File Offset: 0x0003F86F
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_GateBase(this.id, this.className);
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00041688 File Offset: 0x0003F888
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x00041691 File Offset: 0x0003F891
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00041699 File Offset: 0x0003F899
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x0025DC8C File Offset: 0x0025BE8C
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Gate"];
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

		// Token: 0x06005CB6 RID: 23734 RVA: 0x0025DD0C File Offset: 0x0025BF0C
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Gate"];
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
				ushort properUtype2 = property.properUtype;
				if (properUtype2 <= 40002)
				{
					if (properUtype2 != 89)
					{
						switch (properUtype2)
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
					else
					{
						uint oldValue = this.dialogID;
						this.dialogID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDialogIDChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onDialogIDChanged(oldValue);
						}
					}
				}
				else
				{
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue2 = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue2);
						}
						break;
					}
					case 41004:
					{
						uint oldValue3 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue3);
						}
						break;
					}
					case 41005:
					{
						uint oldValue4 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue4);
						}
						break;
					}
					case 41006:
					{
						uint oldValue5 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue5);
						}
						break;
					}
					case 41007:
					{
						byte oldValue6 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue6);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue6);
						}
						break;
					}
					default:
						if (properUtype2 == 51007)
						{
							uint oldValue7 = this.entityNO;
							this.entityNO = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onEntityNOChanged(oldValue7);
								}
							}
							else if (this.inWorld)
							{
								this.onEntityNOChanged(oldValue7);
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x0025E09C File Offset: 0x0025C29C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Gate"].idpropertys;
			uint oldValue = this.dialogID;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDialogIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDialogIDChanged(oldValue);
			}
			Vector3 direction = this.direction;
			Property property2 = idpropertys[2];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue2 = this.entityNO;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEntityNOChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onEntityNOChanged(oldValue2);
			}
			uint oldValue3 = this.modelID;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue3);
			}
			byte oldValue4 = this.modelScale;
			Property property5 = idpropertys[7];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue4);
			}
			string oldValue5 = this.name;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue5);
			}
			Vector3 position = this.position;
			Property property7 = idpropertys[1];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue6 = this.uid;
			Property property8 = idpropertys[9];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue6);
			}
			uint oldValue7 = this.utype;
			Property property9 = idpropertys[10];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue7);
					return;
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue7);
			}
		}

		// Token: 0x04005A75 RID: 23157
		public EntityBaseEntityCall_GateBase baseEntityCall;

		// Token: 0x04005A76 RID: 23158
		public EntityCellEntityCall_GateBase cellEntityCall;

		// Token: 0x04005A77 RID: 23159
		public uint dialogID;

		// Token: 0x04005A78 RID: 23160
		public uint entityNO;

		// Token: 0x04005A79 RID: 23161
		public uint modelID;

		// Token: 0x04005A7A RID: 23162
		public byte modelScale = 30;

		// Token: 0x04005A7B RID: 23163
		public string name = "";

		// Token: 0x04005A7C RID: 23164
		public uint uid;

		// Token: 0x04005A7D RID: 23165
		public uint utype;
	}
}
