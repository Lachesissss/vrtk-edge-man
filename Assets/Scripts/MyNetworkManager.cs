using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public Transform leftPlayerSpawn;
    public Transform rightPlayerSpawn;
    public GameObject player1;
    public GameObject player2;
    GameObject item;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftPlayerSpawn : rightPlayerSpawn;
        

        if(numPlayers == 0)
        {
            GameObject player = Instantiate(player1, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);
        }
        else if(numPlayers == 1)
        {
            GameObject player = Instantiate(player2, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        if (numPlayers == 2)
        {
            // spawn ball if two players
            item = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Object"));
            // initial item's authority to the second player
            NetworkServer.Spawn(item, conn);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // destroy ball
        if (item != null)
            NetworkServer.Destroy(item);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }

}
