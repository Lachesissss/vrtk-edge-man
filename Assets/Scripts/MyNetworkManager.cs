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
        Vector3 paddle1Pos = new Vector3(-17.0753994f, -1.21379995f, 49.4790001f);
        Quaternion paddle1Rot = Quaternion.Euler(0, 265.048859f, 0);

        Vector3 paddle2Pos = new Vector3(-14.2397995f, -1.21499991f, 49.4207993f);
        Quaternion paddle2Rot = new Quaternion(0, 0.5100166f, 0, -0.8601646f);

        Vector3 puckPos = new Vector3(-16.6760006f, -1.25760007f, 49.4840012f);
        Quaternion puckRot = Quaternion.Euler(0, 234.388916f, 0);

        if (numPlayers == 0)
        {
            GameObject player = Instantiate(player1, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);

            item = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Paddle1"), paddle1Pos, paddle1Rot);
            NetworkServer.Spawn(item, conn);
        }
        else if (numPlayers == 1)
        {
            GameObject player = Instantiate(player2, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);

            item = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Paddle2"), paddle2Pos, paddle2Rot);
            NetworkServer.Spawn(item, conn);
        }
        if (numPlayers == 2)
        {
            // spawn ball if two players
            // initial item's authority to the second player
            item = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Puck"), puckPos, puckRot);
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
