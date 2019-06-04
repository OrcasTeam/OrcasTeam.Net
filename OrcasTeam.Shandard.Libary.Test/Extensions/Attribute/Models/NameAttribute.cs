namespace OrcasTeam.Shandard.Libary.Test.Extensions.Attribute
{
    class NameAttribute:System.Attribute
    {
        public string Name
        {
            get;
            set;
        }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
     class NoAttribute : System.Attribute
    {
    }
}
