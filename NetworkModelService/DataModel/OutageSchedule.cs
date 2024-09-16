using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class OutageSchedule : IrregularIntervalSchedule
    {
        private List<long> switchingOperations = new List<long>();

        public OutageSchedule(long globalId) : base(globalId)
        {
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                OutageSchedule x = (OutageSchedule)obj;
                return (CompareHelper.CompareLists(x.switchingOperations, this.switchingOperations, true));
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
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {

                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS:
                    prop.SetValue(switchingOperations);
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
                return switchingOperations.Count != 0 || base.IsReferenced;
            }
        }

        public List<long> SwitchingOperations { get => switchingOperations; set => switchingOperations = value; }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    switchingOperations.Add(globalId);
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
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:

                    if (switchingOperations.Contains(globalId))
                    {
                        switchingOperations.Remove(globalId);
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
