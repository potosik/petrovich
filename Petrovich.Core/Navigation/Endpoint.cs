namespace Petrovich.Core.Navigation
{
    public class Endpoint
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public Endpoint(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }
    }
}
