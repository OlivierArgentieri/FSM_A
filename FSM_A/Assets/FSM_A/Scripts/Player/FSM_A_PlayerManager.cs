using System;
using System.Collections.Generic;
using System.Threading;

public class FSM_A_PlayerManager : TemplateManager<FSM_A_PlayerManager>
{
    
   
    #region f/p

    Dictionary<int, FSM_A_PlayerComponent> players = new Dictionary<int, FSM_A_PlayerComponent>();
    public FSM_A_PlayerComponent PlayerOne => players[0];

    #endregion
    
    
    
    #region unity methods
    #endregion
    
    
    
    
    
    #region custom methods


    private void RegisterHandler(FSM_A_PlayerComponent _player, bool _add)
    {
        if (_player == null || !_player.IsValid) return;
        
        bool _canHandle = _add ? !ContainsPlayer(_player) : ContainsPlayer(_player);

        if (!_canHandle) throw new Exception("Invalid Player component"); 
        
        if(_add)
            players.Add(_player.ID, _player);
        else
            players.Remove(_player.ID);
    }


    public void Register(FSM_A_PlayerComponent _player)
    {
        RegisterHandler(_player, true);
    }
    
    
    
    public void UnRegister(FSM_A_PlayerComponent _player)
    {
        RegisterHandler(_player, false);
    }


    private bool ContainsPlayer(FSM_A_PlayerComponent _player) => players.ContainsKey(_player.ID);

    #endregion
}
