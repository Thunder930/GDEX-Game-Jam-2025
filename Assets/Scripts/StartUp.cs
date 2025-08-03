using TMPro;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI version;
    private void Start()
    {
        version.text = "v. " + Application.version;
    }
}