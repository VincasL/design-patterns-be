using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using BattleshipsApi.Strategies;
using System;

public class Director
{
    public Unit ConstructRussian(IBuilder builder)
    {
        return builder.AddArmour(1).AddDammage(1).AddStrategy(new MoveLeft()).Build();
    }
    public Unit ConstructAmerican(IBuilder builder)
    {
        return builder.AddArmour(2).AddDammage(2).AddStrategy(new MoveUp()).Build();
    }
    public Unit ConstructGerman(IBuilder builder)
    {
        return builder.AddArmour(3).AddDammage(3).AddStrategy(new MoveRight()).Build();
    }
}
