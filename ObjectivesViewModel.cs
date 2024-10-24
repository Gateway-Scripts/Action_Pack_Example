using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VMS.TPS.Common.Model.Types;
using VMS.TPS.VisualScripting.ElementInterface;

namespace ObjectivesAP
{
    public class ObjectivesViewModel
    {
        public class ObjectivesModel
        {
            //create a property (prop+TAB+TAB)
            public bool ObjectivesChecked { get; set; }
            public bool ParametersChecked { get; set; }
        }
        // The IDesignTimeDetails is the interface to Visual Scripting. 
        public class ObjectivesDesignTimeDetails : IDesignTimeDetails
        {
            //This property is the viewmodel to my view.
            public ObjectivesModel LocalObjectivesModel { get; set; }
            //This property is the default for my viewmodel in case the user cancels changes.
            public ObjectivesModel KeepObjectivesModel { get; set; }
            //UI that opens when the user clicks the "i"
            public UserControl View
            {
                get
                {
                    //New action pack, need default selections.
                    if(LocalObjectivesModel == null)
                    {
                        LocalObjectivesModel = new ObjectivesModel();
                        LocalObjectivesModel.ObjectivesChecked = true;//default.
                        KeepObjectivesModel = new ObjectivesModel();//instantiate class when UI is open.
                        KeepObjectivesModel.ObjectivesChecked = true;//default.
                    }
                    return new ObjectivesDesignTimeView { DataContext = LocalObjectivesModel };
                }
            }
            //Object going to and from the XML file.
            public object Data 
            {
                //get => save data to the xml file.
                get
                {
                    //if the UI hasn't been opened, set some defaults.
                    if (LocalObjectivesModel == null)
                    {
                        LocalObjectivesModel = new ObjectivesModel();
                        LocalObjectivesModel.ObjectivesChecked = true;//default.
                    }
                    return LocalObjectivesModel;
                }
                //set => pull data from the XML file.
                set
                {
                    ObjectivesModel oModel = value as ObjectivesModel;
                    //if oModel exists, set defaults on oModel, else set normal defaults.
                    if(oModel != null)
                    {
                        LocalObjectivesModel = new ObjectivesModel();
                        LocalObjectivesModel.ObjectivesChecked = oModel.ObjectivesChecked;
                        LocalObjectivesModel.ParametersChecked = oModel.ParametersChecked;
                        //assign also the keep viewmodel
                        KeepObjectivesModel = new ObjectivesModel();
                        KeepObjectivesModel.ObjectivesChecked = oModel.ObjectivesChecked;
                        KeepObjectivesModel.ParametersChecked = oModel.ParametersChecked;
                    }
                    else//no XML file to read from, select the defaults. 
                    {
                        LocalObjectivesModel = new ObjectivesModel();
                        LocalObjectivesModel.ObjectivesChecked = true;
                        KeepObjectivesModel = new ObjectivesModel();
                        KeepObjectivesModel.ObjectivesChecked = true;
                    }
                }
            }
            //XML to know what type of object to save.
            public Type DataType => typeof(ObjectivesModel);

            public Type[] IncomingType { get; set; }
            public DoseValue.DoseUnit EclipseDoseUnit { get; set; }
            //allows to check if user put data in correctly. (set to true for now).
            public bool IsContentValid => true;

            public void DiscardChanges()
            {
                LocalObjectivesModel.ObjectivesChecked = KeepObjectivesModel.ObjectivesChecked;
                LocalObjectivesModel.ParametersChecked = KeepObjectivesModel.ParametersChecked;
            }

            public void SaveChanges()
            {
                KeepObjectivesModel.ObjectivesChecked = LocalObjectivesModel.ObjectivesChecked;
                KeepObjectivesModel.ParametersChecked = LocalObjectivesModel.ParametersChecked;
            }
        }
    }
}
