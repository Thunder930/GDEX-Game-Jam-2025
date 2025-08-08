using UnityEngine;

public class ShowIfNextLevel : MonoBehaviour
{
    [SerializeField] bool showIfNextLevel;

    private void OnEnable()
    {
        if (showIfNextLevel)
        {
            if (LevelManager.IsFinalLevel())
            {
                gameObject.SetActive(false);
            } else
            {
                gameObject.SetActive(true);
            }
        } else
        {
            if (LevelManager.IsFinalLevel())
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
