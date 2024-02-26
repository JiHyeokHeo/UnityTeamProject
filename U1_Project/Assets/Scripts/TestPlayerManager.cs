using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager 
{
    TestMyPlayer _myplayer;
    Dictionary<int, TestPlayer> _players = new Dictionary<int, TestPlayer>();

    public static TestPlayerManager Instance { get; } = new TestPlayerManager();

    public void Add(S_PlayerList packet)
    {
        Object obj = Resources.Load("TestPref/Player");

        foreach (S_PlayerList.Player p in packet.players)
        {
            GameObject go = Object.Instantiate(obj) as GameObject;

            if (p.isSelf)
            {
                TestMyPlayer myPlayer = go.AddComponent<TestMyPlayer>();
                myPlayer.PlayerId = p.playerId;
                myPlayer.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                myPlayer.transform.rotation = Quaternion.Euler(p.rotationX, p.rotationY, p.rotationZ);
                _myplayer = myPlayer;
            }
            else
            {
                TestPlayer player = go.AddComponent<TestPlayer>();
                player.PlayerId = p.playerId;
                player.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                player.transform.rotation = Quaternion.Euler(p.rotationX, p.rotationY, p.rotationZ);
                _players.Add(p.playerId, player);
            }
        }
    }

    public void Move(S_BroadcastMove packet)
    {
        if (_myplayer.PlayerId == packet.playerId)
        {
            _myplayer.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
            _myplayer.transform.rotation = Quaternion.Euler(packet.rotationX, packet.rotationY, packet.rotationZ);
        }
        else
        {
            TestPlayer player = null;
            if (_players.TryGetValue(packet.playerId, out player))
            {
                player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
                player.transform.rotation = Quaternion.Euler(packet.rotationX, packet.rotationY, packet.rotationZ);
            }
        }
    }

    public void EnterGame(S_BroadcastEnterGame packet)
    {
        if (packet.playerId == _myplayer.PlayerId)
            return;

        Object obj = Resources.Load("TestPref/Player");
        GameObject go = Object.Instantiate(obj) as GameObject;

        TestPlayer player = go.AddComponent<TestPlayer>();
        player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        player.transform.rotation = Quaternion.Euler(packet.rotationX, packet.rotationY, packet.rotationZ);
        _players.Add(packet.playerId, player);
    }

    public void LeaveGame(S_BroadcastLeaveGame packet)
    {
        if (_myplayer.PlayerId == packet.playerId)
        {
            GameObject.Destroy(_myplayer.gameObject);
            _myplayer = null;
        }
        else
        {
            TestPlayer player = null;
            if (_players.TryGetValue(packet.playerId, out player))
            {
                GameObject.Destroy(player.gameObject);
                _players.Remove(packet.playerId);
            }
        }
    }
}
