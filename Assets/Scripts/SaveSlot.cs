using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject ñreateButton;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject deleteButton;

    [SerializeField] private WebRequestManager requestManager;

    private string gameURL = "https://2025.nti-gamedev.ru/api/games/de781ac2-27da-479a-b5f1-f572f8c9aacb/";

    private void Start()
    {
        if (PlayerPrefs.HasKey(title.text))
        {
            loadButton.SetActive(true);
            deleteButton.SetActive(true);
            ñreateButton.SetActive(false);
        }
        else
        {
            loadButton.SetActive(false);
            deleteButton.SetActive(false);
            ñreateButton.SetActive(true);
        }
    }

    public void CreateSave()
    {
        StartCoroutine(requestManager.SendPostRequest(gameURL + "players/", title.text, new Resources(0)));
        PlayerPrefs.SetInt(title.text, 1);
        loadButton.SetActive(true);
        deleteButton.SetActive(true);
        ñreateButton.SetActive(false);
    }

    public void LoadSave()
    {
        StartCoroutine(requestManager.SendPutRequest(gameURL + "players/" + title.text + "/", title.text, new Resources(PlayerPrefs.GetInt("honey"))));
        PlayerPrefs.SetString("Loaded Save", title.text);
        SceneManager.LoadScene(1);
    }

    public void DeleteSave()
    {
        StartCoroutine(requestManager.SendDeleteRequest(gameURL + "players/" + title.text + "/"));
        PlayerPrefs.DeleteAll();
        loadButton.SetActive(false);
        deleteButton.SetActive(false);
        ñreateButton.SetActive(true);

    }
}
