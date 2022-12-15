using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Proxy
{
    public class VisitorProxy : IVisitor
    {
        private readonly ShipVisitor shipVisitor;
        public VisitorProxy(DateTime timeGameStarted) 
        {
            shipVisitor = new ShipVisitor(timeGameStarted);
        }

        public int Visit(IVisitable visitable)
        {
            Console.WriteLine($"Visiting ship.");
            return shipVisitor.Visit(visitable);
        }
    }
}
