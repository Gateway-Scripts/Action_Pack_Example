using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using VMS.TPS.VisualScripting.ElementInterface;
using System.Windows.Documents;
using System.Windows.Controls;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

namespace ObjectivesAP
{
    // TODO: Replace the existing class name with your own class name. (CTRL + R + R)
    public class ObjectivesPackElement : VisualScriptElement
    {
        public ObjectivesPackElement()
        {
            this.CategorizationTag = "Custom Action Packs";
            this.Description = new ElementDescription
            {
                ElementFunction = "Tabulate the optimization objectives",
                Inputs = "Plan Setup",
                Output = "To Flow Action Pack"
            };
        }
        public ObjectivesPackElement(IVisualScriptElementRuntimeHost host) { }

        public override bool RequiresRuntimeConsole { get { return false; } }
        public override bool RequiresDatabaseModifications { get { return false; } }
        //For custom user Interface Element set RequiresDetailedView => true;
        public override bool RequiresDetailedView => true;//STEP 1


        [ActionPackExecuteMethod]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public IEnumerable<BlockUIContainer> Execute(PlanSetup ps)
        {
            // TODO: Add your code here. Save this until the end.
            List<BlockUIContainer> blocks = new List<BlockUIContainer>();
            //how to get the data from the XML file
            ObjectivesViewModel.ObjectivesModel objSelection =
                (ObjectivesViewModel.ObjectivesModel)(m_designTimeDetails as//cast Data as ObjModel
                ObjectivesViewModel.ObjectivesDesignTimeDetails).Data;//Getting the Data.
            //header
            blocks.Add(
                new BlockUIContainer(
                new TextBlock//for TextBlock you will need using System.Windows.Controls. (lightbulb).
                {
                    Text = $"Optimization Objectives",FontSize=16,FontWeight=FontWeights.Bold,
                    Margin=new Thickness(20,10,0,0)
                }
                )
                );
            if (objSelection.ObjectivesChecked)
            {
                blocks.Add(new BlockUIContainer(new ObjectiveView
                {
                    DataContext = new ObjectiveViewModel(ps)
                }));
            }
            if (objSelection.ParametersChecked)
            {
                blocks.Add(
                new BlockUIContainer(
                new TextBlock//for TextBlock you will need using System.Windows.Controls. (lightbulb).
                {
                    Text = $"Parameters not yet Implemented."
                }));
            }
            return blocks;
        }

        public override string DisplayName
        {
            get
            {
                // TODO: Replace "Element Name" with the name that you want to be displayed in the Visual Scripting UI.
                return "Optimization Objectives";
            }
        }
        //STEP 2
        //Must comment out the Allowed Options to have custom UI. (CTRL + K + C)
        //IDictionary<string, string> m_options = new Dictionary<string, string>();
        //public override void SetOption(string key, string value)
        //{
        //    m_options.Add(key, value);
        //}

        //public override IEnumerable<KeyValuePair<string, IEnumerable<string>>> AllowedOptions
        //{
        //    get
        //    {
        //        return new KeyValuePair<string, IEnumerable<string>>[] {
        //    new KeyValuePair<string, IEnumerable<string>>("TestOption", new string[] { "Test Value" })
        //  };
        //    }
        //}
        //STEP 3 Provide the Design Time Details
        private IDesignTimeDetails m_designTimeDetails { get; set; }
        public override IDesignTimeDetails DesignTimeDetails
        {
            get
            {
                if (m_designTimeDetails == null)
                {
                    //TODO Add The UI to be opened when DesignTime Details is invoked
                    m_designTimeDetails = new ObjectivesViewModel.ObjectivesDesignTimeDetails();
                }
                return m_designTimeDetails;
            }
        }
    }
}
