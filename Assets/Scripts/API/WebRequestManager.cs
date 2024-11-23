using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestManager : MonoBehaviour
{
    public IEnumerator SendGetRequest(string url)
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

    public IEnumerator SendPostRequest(string url, string username, Resources resources)
    {
        WWWForm formData = new WWWForm();

        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = username,

            resources = resources
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

    public IEnumerator SendPutRequest(string url, string username, Resources newResources)
    {
        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = username,

            resources = newResources
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

    public IEnumerator SendDeleteRequest(string url)
    {
        UnityWebRequest request = UnityWebRequest.Delete(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }

        Debug.Log("Responce Code: " + request.responseCode);
    }
}
