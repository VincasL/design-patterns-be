﻿using System.Collections;
using BattleshipsApi.Entities;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Handlers;

public class QueueHandler
{
    private LinkedList<Player> _playersQueue = new();
    
    public QueueHandler()
    {
    }
    
    public bool AddPlayerToQueue(Player player)
    {
        if (_playersQueue.Any(x => x.ConnectionId == player.ConnectionId))
        {
            _playersQueue.First(x => x.ConnectionId == player.ConnectionId).Name = player.Name;
            return false;
        }
        
        _playersQueue.AddLast(player);
        return _playersQueue.Count >= 2;
    }

    public Tuple<Player, Player> ReturnLastTwoPlayers()
    {
        if (_playersQueue.Count < 2) throw new Exception();

        var firstPlayer = _playersQueue.First();
        var secondPlayer = _playersQueue.First!.Next?.Value!;
        
        _playersQueue.RemoveFirst();
        _playersQueue.RemoveFirst();
        
        return new Tuple<Player, Player>(firstPlayer,secondPlayer);
    }
}