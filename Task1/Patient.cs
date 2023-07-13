namespace YourApplicationNamespace
{
    internal class Patient
    {
        public object Name { get; internal set; }
        public int PatientId { get; internal set; }
        public int Age { get; internal set; }
        public string Diagnosis { get; internal set; }
        public string Treatment { get; internal set; }
        public string Gender { get; internal set; }
    }
}