using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class IrregularIntervalSchedule : BasicIntervalSchedule
    {
        private List<long> timePointsIR = new List<long>();

        public IrregularIntervalSchedule(long globalId) : base(globalId)
        {
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                IrregularIntervalSchedule x = (IrregularIntervalSchedule)obj;
                return (CompareHelper.CompareLists(x.TimePointsIR, this.TimePointsIR));
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

       

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.IRREGULARINTERVALSCHEDULE_IRTIMEPOINTS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {

                case ModelCode.IRREGULARINTERVALSCHEDULE_IRTIMEPOINTS:
                    prop.SetValue(TimePointsIR);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        

        public override bool IsReferenced
        {
            get
            {
                return TimePointsIR.Count != 0 || base.IsReferenced;
            }
        }

        public List<long> TimePointsIR { get => timePointsIR; set => timePointsIR = value; }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRINTERVALSCHEDULE:
                    TimePointsIR.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRINTERVALSCHEDULE:

                    if (TimePointsIR.Contains(globalId))
                    {
                        TimePointsIR.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }


    }
}
