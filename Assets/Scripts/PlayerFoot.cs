using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    public float footCd = 0.5f;
    private bool playAble = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && playAble)
        {
            AudioMgr.instance.PlayWalk();
            playAble = false;
            MyTimer.instance.WaitTimer(footCd, () =>
            {
                playAble = true;
            });
        }
    }

}