using System;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("energy: " + response.playersData[i].resources.energy);
            Debug.Log("crystals: " + response.playersData[i].resources.crystals);
            Debug.Log("hats: " + response.playersData[i].resources.hats);
            Debug.Log("dishes: " + response.playersData[i].resources.dishes);
            Debug.Log("spots: " + response.playersData[i].resources.spotsData);
        }
    }

    public IEnumerator SendPostRequest(string url, string username, int energy, int crystals, List<string> hats, Dishes dishes, BuildingSpotData spotData)
    {
        WWWForm formData = new WWWForm();

        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = username,
            resources =
            {
                energy = energy,
                crystals = crystals,
                hats = hats,
                dishes = dishes,
                spotsData = spotData
            }
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
        Debug.Log("energy: " + playersDataFromServer.resources.energy);
        Debug.Log("crystals: " + playersDataFromServer.resources.crystals);
        Debug.Log("hats: " + playersDataFromServer.resources.hats);
        Debug.Log("dishes: " + playersDataFromServer.resources.dishes);
        Debug.Log("spots: " + playersDataFromServer.resources.spotsData);

    }

    public IEnumerator SendPutRequest(string url, string username, int newEnergy, int newCrystals, List<string> newHats, Dishes newDishes, BuildingSpotData newSpotData)
    {
        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = username,
            resources =
            {
                energy = newEnergy,
                crystals = newCrystals,
                hats = newHats,
                dishes = newDishes,
                spotsData = newSpotData
            }
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

        Debug.Log("name: " + playersDataFromServer.name);
        Debug.Log("energy: " + playersDataFromServer.resources.energy);
        Debug.Log("crystals: " + playersDataFromServer.resources.crystals);
        Debug.Log("hats: " + playersDataFromServer.resources.hats);
        Debug.Log("dishes: " + playersDataFromServer.resources.dishes);
        Debug.Log("spots: " + playersDataFromServer.resources.spotsData);
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
