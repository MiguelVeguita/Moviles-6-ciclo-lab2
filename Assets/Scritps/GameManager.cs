using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance;
    [SerializeField] private Transform playerprefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        print("chavito si funco");
        print(NetworkManager.Singleton.LocalClientId);
        InstancePlayerRpc(NetworkManager.Singleton.LocalClientId);
    }
    [Rpc(SendTo.Server)]
    public void InstancePlayerRpc(ulong Id)
    {
        Transform player = Instantiate(playerprefab);
      //  player.GetComponent<NetworkObject>().Spawn(true);
        player.GetComponent<NetworkObject>().SpawnWithOwnership(Id, true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameManager Instance => instance;

}
