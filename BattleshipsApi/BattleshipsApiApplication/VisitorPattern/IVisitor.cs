namespace BattleshipsApi.VisitorPattern
{
    public interface IVisitor
    {
        public int Visit(IVisitable visitable);
    }
}
