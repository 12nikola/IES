using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class Switch : ConductingEquipment
    {
        private long switchingOperation = 0;

        public long SwitchingOperation { get => switchingOperation; set => switchingOperation = value; }
        public Switch(long globalId): base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return (x.switchingOperation == this.switchingOperation);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {

                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {


                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    property.SetValue(switchingOperation);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    switchingOperation = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }


    }
}
