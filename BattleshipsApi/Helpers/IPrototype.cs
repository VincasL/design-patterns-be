using System;
namespace BattleshipsApi.Helpers
{
    interface IPrototype : ICloneable
    {
        public object ShallowClone();
    }
}

