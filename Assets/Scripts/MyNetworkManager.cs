using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public Transform leftPlayerSpawn;
    public Transform rightPlayerSpawn;
    GameObject item;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftPlayerSpawn : rightPlayerSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

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
