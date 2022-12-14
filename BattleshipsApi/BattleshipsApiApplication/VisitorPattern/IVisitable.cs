namespace BattleshipsApi.VisitorPattern
{
    public interface IVisitable
    {
        public int Accept(IVisitor visitor);
    }
}
