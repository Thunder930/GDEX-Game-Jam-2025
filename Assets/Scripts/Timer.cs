using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI text;
    private float time;
    private bool isRunning = true;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        time = 0.0f;
        text.text = "Time: " + ((int)time).ToString();
    }

    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            text.text = "Time: " + ((int)time).ToString();
        }
    }

    public void SetRunning(bool running)
    {
        isRunning = running;
    }
}
