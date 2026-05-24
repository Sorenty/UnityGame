using Mirror;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    [SyncVar] public int health = 100;
    [SyncVar] public int tree = 0;
    [SyncVar] public int lestva = 0;
    [SyncVar] public bool haveAxe = false;
    [SyncVar] public int num_of_eat_hp = 0;

    [Command]
    public void CmdAddHealth(int value)
    {
        health += value;
    }

    [Command]
    public void CmdAddTree(int value)
    {
        tree += value;
    }

    [Command]
    public void CmdAddLestva(int value)
    {
        lestva += value;
    }

    [Command]
    public void CmdSetHaveAxe(bool value)
    {
        haveAxe = value;
    }

    [Command]
    public void CmdAddEatHp()
    {
        num_of_eat_hp++;
    }

    [Command]
    public void CmdTakeDamage(int value)
    {
        health -= value;
    }
}