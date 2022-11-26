using BattleshipsApi.Contracts;

namespace BattleshipsApi.Helpers
{
    interface IPrototype : ICloneable
    {
        public object ShallowClone();
    }
}

