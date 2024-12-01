using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebRequestManager : MonoBehaviour
{
    private string gameURL = "https://2025.nti-gamedev.ru/api/games/de781ac2-27da-479a-b5f1-f572f8c9aacb/";

    public IEnumerator SendGetRequest(string saveName = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(gameURL);
        if (saveName != null) request = UnityWebRequest.Get(gameURL + "players/" + saveName + "/");

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
            Debug.Log("energyHoney: " + response.playersData[i].resources.energyHoney);
            //Debug.Log("crystals: " + response.playersData[i].resources.crystals);
            Debug.Log("hats: " + response.playersData[i].resources.hats);
            Debug.Log("dishes: " + response.playersData[i].resources.dishes);
            Debug.Log("spots: " + response.playersData[i].resources.spotsData);
            Debug.Log("Responce Code: " + request.responseCode);
        }
    }

    public IEnumerator SendPostRequest(string saveName, int energy, /*int crystals,*/ List<string> hats, List<string> dishes, Dictionary<string, string> spotData)
    {
        WWWForm formData = new WWWForm();

        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = saveName,
            resources =
            {
                energyHoney = energy,
                //crystals = crystals,
                hats = hats,
                dishes = dishes,
                spotsData = spotData
            }
        };

        string json = JsonConvert.SerializeObject(playerData);

        UnityWebRequest request = UnityWebRequest.PostWwwForm(gameURL + "players/", json);

        byte[] playerDataBytes = Encoding.UTF8.GetBytes(json);

        UploadHandler uploadHandler = new UploadHandlerRaw(playerDataBytes);

        request.uploadHandler = uploadHandler;

        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }
        /*
        PlayersDataStruct playersDataFromServer = JsonConvert.DeserializeObject<PlayersDataStruct>(request.downloadHandler.text);
        
        Debug.Log("name: " + playersDataFromServer.name);
        Debug.Log("energy: " + playersDataFromServer.resources.energy);
        Debug.Log("crystals: " + playersDataFromServer.resources.crystals);
        Debug.Log("hats: " + playersDataFromServer.resources.hats);
        Debug.Log("dishes: " + playersDataFromServer.resources.dishes);
        Debug.Log("spots: " + playersDataFromServer.resources.spotsData);
        */
        Debug.Log("Responce Code: " + request.responseCode);
    }

    public IEnumerator SendPutRequest(string saveName, int newEnergy, /*int newCrystals,*/ List<string> newHats, List<string> newDishes, Dictionary<string, string> newSpotData)
    {
        PlayersDataStruct playerData = new PlayersDataStruct
        {
            name = saveName,
            resources =
            {
                energyHoney = newEnergy,
                //crystals = newCrystals,
                hats = newHats,
                dishes = newDishes,
                spotsData = newSpotData
            }
        };

        string json = JsonConvert.SerializeObject(playerData);

        UnityWebRequest request = UnityWebRequest.Put(gameURL + "players/" + saveName + "/", json);

        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }
        /*
        PlayersDataStruct playersDataFromServer = JsonConvert.DeserializeObject<PlayersDataStruct>(request.downloadHandler.text);

        Debug.Log("name: " + playersDataFromServer.name);
        Debug.Log("energy: " + playersDataFromServer.resources.energy);
        Debug.Log("crystals: " + playersDataFromServer.resources.crystals);
        Debug.Log("hats: " + playersDataFromServer.resources.hats);
        Debug.Log("dishes: " + playersDataFromServer.resources.dishes);
        Debug.Log("spots: " + playersDataFromServer.resources.spotsData);
        */
        Debug.Log("Responce Code: " + request.responseCode);
    }

    public IEnumerator SendDeleteRequest(string saveName)
    {
        UnityWebRequest request = UnityWebRequest.Delete(gameURL + "players/" + saveName + "/");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new Exception("No internet connection (" + request.error + ")");
        }

        Debug.Log("Responce Code: " + request.responseCode);
    }
}
