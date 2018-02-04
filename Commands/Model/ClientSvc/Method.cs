namespace SharePointPnP.PowerShell.Core.Model.ClientSvc
{
    public class Method
    {
        private string _name;
        public Method(string name)
        {
            _name = name;
        }

        public string Name {get;set;}
    }
}