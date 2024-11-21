using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APItesting : MonoBehaviour
{
    [SerializeField] private string url;

    public requestsTypes requestType;

    private void Start()
    {
        if (requestType == requestsTypes.Get)
            StartCoroutine(SendGetRequest());
        else if (requestType == requestsTypes.POST)
            StartCoroutine(SendPostRequest());
        else if (requestType == requestsTypes.PUT)
            StartCoroutine(SendPutRequest());
        else if (requestType == requestsTypes.DELETE)
            StartCoroutine(SendDeleteRequest());
    }

    private IEnumerator SendGetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }

        string json = "{\"playersData\":" + request.downloadHandler.text + "}";

        Response response = JsonUtility.FromJson<Response>(json);

        for (int i = 0; i < response.playersData.Length; i++)
        {
            Debug.Log("name: " + response.playersData[i].name);
            Debug.Log("honey: " + response.playersData[i].resources.honey);
        }
    }

    private IEnumerator SendPostRequest()
    {
        WWWForm formData = new WWWForm();

        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = "YetAnotherBear",

            resources = new Resources { honey = 123 }
        };

        string json = JsonUtility.ToJson(playerData);

        UnityWebRequest request = UnityWebRequest.PostWwwForm(url, json);

        byte[] playerDataBytes = Encoding.UTF8.GetBytes(json);

        UploadHandler uploadHandler = new UploadHandlerRaw(playerDataBytes);

        request.uploadHandler = uploadHandler;

        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }

        PlayersDataStruct playersDataFromServer = JsonUtility.FromJson<PlayersDataStruct>(request.downloadHandler.text);

        Debug.Log("name: " + playersDataFromServer.name);
        Debug.Log("honey: " + playersDataFromServer.resources.honey);
    }

    private IEnumerator SendPutRequest()
    {
        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = "YetAnotherBear",

            resources = new Resources { honey = 2 }
        };

        string json = JsonUtility.ToJson(playerData);

        UnityWebRequest request = UnityWebRequest.Put(url, json);

        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }

        PlayersDataStruct playersDataFromServer = JsonUtility.FromJson<PlayersDataStruct>(request.downloadHandler.text);

        Debug.Log("updated honey: " + playersDataFromServer.resources.honey);
    }

    private IEnumerator SendDeleteRequest()
    {
        UnityWebRequest request = UnityWebRequest.Delete(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }

        Debug.Log("Responce Code: " + request.responseCode);
    }

    public enum requestsTypes
    {
        Get,
        POST,
        PUT,
        DELETE
    }
}
