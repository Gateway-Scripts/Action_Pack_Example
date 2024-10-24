namespace ObjectivesAP
{
    public class ObjectiveModel
    {
        public string StructureId { get; internal set; }
        public double Priority { get; internal set; }
        public string Operation { get; internal set; }
        public string Dose { get; internal set; }
        public string Volume { get; internal set; }
    }
}