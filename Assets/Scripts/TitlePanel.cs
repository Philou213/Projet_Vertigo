using TMPro;
using UnityEngine;

public class TitlePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Label;

    public void SetText(string text)
    {
        Label.text = text;
    }
}
