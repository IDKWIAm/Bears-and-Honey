using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private bool deleteAllSaves;

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private WebRequestManager requestManager;

    private bool isCreated;

    private string gameURL = "https://2025.nti-gamedev.ru/api/games/de781ac2-27da-479a-b5f1-f572f8c9aacb/";

    private void Start()
    {
        if (deleteAllSaves) PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey(title.text))
        {
            buttonText.text = "Load";
            isCreated = true;
        }
        else
        {
            buttonText.text = "Create";
        }
    }

    public void CheckSave()
    {
        if (isCreated)
        {
            LoadSave();
        }
        else
        {
            CreateSave();
        }
    }

    private void CreateSave()
    {
        PlayerPrefs.SetInt(title.text, 1);
        buttonText.text = "Load";
        isCreated = true;
    }

    private void LoadSave()
    {
        PlayerPrefs.SetString("Loaded Save", title.text);
        SceneManager.LoadScene(1);
    }
}
