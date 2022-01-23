using UnityEngine;
using UnityEngine.Events;

public class SceneTrigger : MonoBehaviour
{
    public string triggerName = "Player";

    public UnityEvent enterEvent;
    public bool deactiveTriggerWhenEnter = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(triggerName))
        {
            enterEvent.Invoke();
            if (deactiveTriggerWhenEnter)
                gameObject.SetActive(false);
        }
    }

    public UnityEvent exitEvent;
    public bool deactiveTriggerWhenExit = true;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(triggerName))
        {
            exitEvent.Invoke();
            if (deactiveTriggerWhenExit)
                gameObject.SetActive(false);
        }
    }
}
