﻿using BattleshipsApi.Entities;
using BattleshipsApi.Entities.Mines;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Factories;

public class MineFactory
{
    public Mine GetMine(MineType type)
    {
        switch (type)
        {
            case MineType.Small:
                return new SmallMine();
            case MineType.Huge:
                return new HugeMine();
            case MineType.RemoteControlled:
                return new RemoteControlledMIne();
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}