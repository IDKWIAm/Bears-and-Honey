using TMPro;
using UnityEngine;

public class CurrencyPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;

    [SerializeField] private Vector2 defaultPosition;

    public void SetParameters(int amount, Vector2 position = new Vector2())
    {
        amountText.text = "+" + amount.ToString();
        if (position == Vector2.zero) position = defaultPosition;
        gameObject.GetComponent<RectTransform>().position = position;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
