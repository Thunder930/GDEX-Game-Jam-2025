using TMPro;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI version;
    private void Start()
    {
        GameState.ChangeState(GAME_STATE.RUNNING);
        version.text = "v. " + Application.version;
    }
}