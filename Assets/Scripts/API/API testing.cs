using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APItesting : MonoBehaviour
{
    [SerializeField] private string url;

    private void Start()
    {
        StartCoroutine(SendGetRequest());
    }

    private IEnumerator SendGetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        string json = "{\"playersData\":" + request.downloadHandler.text + "}";

        Response response = JsonUtility.FromJson<Response>(json);

        for (int i = 0; i < response.playersData.Length; i++)
        {
            Debug.Log("name: " + response.playersData[i].name);
            Debug.Log("honey: " + response.playersData[i].resources.honey);
        }
    }
}
