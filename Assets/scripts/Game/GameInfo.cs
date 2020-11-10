using System;
using System.Collections.Generic;

public class GameInfo
{
    private readonly List<Player> _players   = new List<Player>();
    private readonly List<Penguin> _penguins = new List<Penguin>();

    public IReadOnlyList<Player> m_players => _players;
    public IReadOnlyList<Penguin> m_penguins => _penguins.AsReadOnly();

    public Action<Penguin> m_onRemovePenguin;
    
    public void AddPenguins(IEnumerable<Penguin> penguins)
    {
        _penguins.AddRange(penguins);
    }
    
    public void RemovePenguin(Penguin penguin)
    {
        _penguins.Remove(penguin);
        m_onRemovePenguin?.Invoke(penguin);
    }

    public void AddPlayers(IEnumerable<Player> players)
    {
        _players.AddRange(players);
    }
}
