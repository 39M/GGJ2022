using UnityEngine;

public class WaterZone : MonoBehaviour
{
    public bool ignorePlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Npc")
        {
            return;
        }
        if (ignorePlayer && collision.tag == "Player")
        {
            return;
        }
        if (collision.transform.GetComponent<PlayerMove>().ignoreWater)
        {
            return;
        }
        collision.transform.GetComponent<PlayerMove>().SetWaterState(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Npc")
        {
            return;
        }
        if (ignorePlayer && collision.tag == "Player")
        {
            return;
        }
        if (collision.transform.GetComponent<PlayerMove>().ignoreWater)
        {
            return;
        }
        collision.transform.GetComponent<PlayerMove>().SetWaterState(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Npc")
        {
            return;
        }
        if (ignorePlayer && collision.tag == "Player")
        {
            return;
        }
        if (collision.transform.GetComponent<PlayerMove>().ignoreWater)
        {
            return;
        }
        float curH = collision.transform.position.y;
        float waterH = transform.position.y + transform.lossyScale.y * 0.5f;
        collision.transform.GetComponent<PlayerMove>().SetWaterFloat(curH < waterH);
    }
}