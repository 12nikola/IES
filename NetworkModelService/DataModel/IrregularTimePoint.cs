using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class IrregularTimePoint : IdentifiedObject
    {
        private float time;
        private float value1;
        private float value2;
        private long intervalSchedule = 0;
        public float Time { get => time; set => time = value; }
        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IntervalSchedule { get => intervalSchedule; set => intervalSchedule = value; }

        public IrregularTimePoint(long globalId) : base(globalId)
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
                IrregularTimePoint p = (IrregularTimePoint)x;
                return ((p.time == this.time) && (p.value1 == this.value1) && (p.value2 == this.value2) && (p.intervalSchedule == this.intervalSchedule));

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
                case ModelCode.IRREGULARTIMEPOINT_TIME:
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                case ModelCode.IRREGULARTIMEPOINT_IRINTERVALSCHEDULE:

                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRINTERVALSCHEDULE:
                    property.SetValue(intervalSchedule);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_TIME:
                    property.SetValue(time);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
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
                case ModelCode.IRREGULARTIMEPOINT_IRINTERVALSCHEDULE:
                    intervalSchedule = property.AsReference();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_TIME:
                    time = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
