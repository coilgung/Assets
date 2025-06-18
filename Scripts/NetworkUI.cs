using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public class NetworkUI : MonoBehaviour
{
    [SerializeField] Button buttonServer;
    [SerializeField] Button buttonHost;
    [SerializeField] Button buttonJoin;

    void Awake()
    {
        buttonServer.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        buttonHost.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        buttonJoin.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
