using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public Transform leftPlayerSpawn;
    public Transform rightPlayerSpawn;
    public GameObject player1;
    public GameObject player2;
    private GameObject airHockey;
    private GameObject paddle1;
    private GameObject paddle2;
    private GameObject itemDisplayer;
    private GameObject gunDisplayer;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftPlayerSpawn : rightPlayerSpawn;

        Vector3 paddle1Pos = new Vector3(-17.0753994f, -1.21379995f, 49.4790001f);
        Quaternion paddle1Rot = Quaternion.Euler(0, 265.048859f, 0);

        Vector3 paddle2Pos = new Vector3(-14.2397995f, -1.21499991f, 49.4207993f);
        Quaternion paddle2Rot = new Quaternion(0, 0.5100166f, 0, -0.8601646f);

        /*
        Vector3 puckPos = new Vector3(-16.6760006f, -1.25760007f, 49.4840012f);
        Quaternion puckRot = Quaternion.Euler(0, 234.388916f, 0);
        */

        Vector3 airHockeyPos = new Vector3(-15.6599998f, -2.00999999f, 49.3800011f);
        Quaternion airHockeyRot = Quaternion.Euler(0, 90.0000076f, 0);

        Vector3 itemDisplayerPos = new Vector3(-22.5359993f, -1.91600001f, 46.9720001f);
        Quaternion itemDisplayerRot = Quaternion.Euler(0, 0, 0);

        Vector3 gunDisplayerPos = new Vector3(-24.257f, -1.91600001f, 46.9720001f);
        Quaternion gunDisplayerRot = Quaternion.Euler(0, 0, 0);

        if (numPlayers == 0)
        {
            GameObject player = Instantiate(player1, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);

            airHockey = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "AirHockey"), airHockeyPos, airHockeyRot);
            NetworkServer.Spawn(airHockey, conn);

            paddle1 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Paddle1"), paddle1Pos, paddle1Rot);
            NetworkServer.Spawn(paddle1, conn);

            paddle2 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Paddle2"), paddle2Pos, paddle2Rot);
            NetworkServer.Spawn(paddle2, conn);

            itemDisplayer = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "ItemDisplayer"), itemDisplayerPos, itemDisplayerRot);
            NetworkServer.Spawn(itemDisplayer, conn);

            gunDisplayer = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "GunDisplayer"), gunDisplayerPos, gunDisplayerRot);
            NetworkServer.Spawn(gunDisplayer, conn);
        }
        else if (numPlayers == 1)
        {
            GameObject player = Instantiate(player2, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // destroy all networked objects
        if (airHockey)
            NetworkServer.Destroy(airHockey);

        if (paddle1)
            NetworkServer.Destroy(paddle1);

        if (paddle2)
            NetworkServer.Destroy(paddle2);

        if (itemDisplayer)
            NetworkServer.Destroy(itemDisplayer);

        if (gunDisplayer)
            NetworkServer.Destroy(gunDisplayer);
        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }

}
