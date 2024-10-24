using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace ObjectivesAP
{
    public class ObjectiveViewModel
    {
        //Constructor (CTOR + TAB + TAB)
        public List<ObjectiveModel> Objectives { get; set; }//make ObjectiveModel public.
        public ObjectiveViewModel(PlanSetup plan)
        {
            Objectives = new List<ObjectiveModel>();
            foreach(var objective in plan.OptimizationSetup.Objectives)
            {
                ObjectiveModel obj = new ObjectiveModel();
                obj.StructureId = objective.StructureId;
                obj.Priority = objective.Priority;
                obj.Operation = objective.Operator.ToString();
                if(objective is OptimizationPointObjective)
                {
                    obj.Dose = (objective as OptimizationPointObjective).Dose.ToString();
                    obj.Volume = (objective as OptimizationPointObjective).Volume.ToString("F1");
                }
                else if(objective is OptimizationMeanDoseObjective)
                {
                    obj.Dose = (objective as OptimizationMeanDoseObjective).Dose.ToString();
                    obj.Volume = "-";// (objective as OptimizationPointObjective).Volume.ToString("F1");
                }
                Objectives.Add(obj);
            }
        }
    }
}
