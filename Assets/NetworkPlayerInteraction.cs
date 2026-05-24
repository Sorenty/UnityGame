using UnityEngine;
using Mirror;

public class NetworkPlayerInteraction : NetworkBehaviour
{
    public PlayerStats stats;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (!isLocalPlayer) return;
        if (stats == null) return;

        // Аптечка
        if (coll.CompareTag("Health pack") && Input.GetKeyDown(KeyCode.E))
        {
            stats.CmdAddHealth(50);
            stats.CmdAddEatHp();

            CmdDestroyObject(coll.gameObject);
        }

        // Топор
        if (coll.CompareTag("Axe") && Input.GetKeyDown(KeyCode.E))
        {
            stats.CmdSetHaveAxe(true);

            CmdDestroyObject(coll.gameObject);
        }

        // Дерево
        if (stats.haveAxe && coll.CompareTag("tree") && Input.GetMouseButtonDown(0))
        {
            stats.CmdAddTree(10);

            CmdDestroyObject(coll.gameObject);
        }

        // Особое дерево
        if (stats.haveAxe && coll.CompareTag("tree1") && Input.GetMouseButtonDown(0))
        {
            stats.CmdAddTree(10);
            stats.CmdAddLestva(10);

            CmdDestroyObject(coll.gameObject);
        }
    }

    [Command]
    void CmdDestroyObject(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }
}