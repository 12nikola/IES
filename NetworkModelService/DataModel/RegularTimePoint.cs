using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int sequenceNumber;
        private float value1;
        private float value2;
        private long intervalSchedule = 0;

        public int SequenceNumber { get => sequenceNumber; set => sequenceNumber = value; }
        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IntervalSchedule { get => intervalSchedule; set => intervalSchedule = value; }

        public RegularTimePoint(long globalId) : base(globalId)
        {
        }
        
        public override bool Equals(object x)
        {
            if (Object.ReferenceEquals(x, null))
            {
                return false;
            }
            else
            {
                RegularTimePoint p = (RegularTimePoint)x;
                return ((p.sequenceNumber == this.sequenceNumber) && (p.value1 == this.value1) && (p.value2 == this.value2) && (p.intervalSchedule == this.intervalSchedule));

            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.REGULARTIMEPOINT_SEQNUM:
                case ModelCode.REGULARTIMEPOINT_VAL1:
                case ModelCode.REGULARTIMEPOINT_VAL2:
                case ModelCode.REGULARTIMEPOINT_RINTERVALSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARTIMEPOINT_RINTERVALSCHEDULE:
                    property.SetValue(intervalSchedule);
                    break;

                case ModelCode.REGULARTIMEPOINT_SEQNUM:
                    property.SetValue(sequenceNumber);
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL1:
                    property.SetValue(value1);
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL2:
                    property.SetValue(value2);
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
                case ModelCode.REGULARTIMEPOINT_RINTERVALSCHEDULE:
                    intervalSchedule = property.AsReference();
                    break;

                case ModelCode.REGULARTIMEPOINT_SEQNUM:
                    sequenceNumber = property.AsInt();
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL2:
                    value2 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
