using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

//[RequireComponent(typeof(Button))]    //写在类外面
public class ButtonBoom : MonoBehaviour
{
    [Range(0.01f, 2.0f)]
    public float changeSize = 1.2f;
    private EventTrigger eventTrigger;
    private float dotweenTime = 0.08f;
    private Tween sizeTween;
    public Sprite highLight;
    private Sprite normal;
    public bool alphaHit = false;
    public int audioId;

    private void Start()
    {
        if (alphaHit)
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }
        if (GetComponent<EventTrigger>())
        {
            eventTrigger = GetComponent<EventTrigger>();
        }
        else
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;  //按下按钮
        UnityAction<BaseEventData> callDown = new UnityAction<BaseEventData>(delegate (BaseEventData baseEvent)
        {
            DOTween.Kill(sizeTween);
            sizeTween = transform.DOScale(new Vector3(1, 1, 1) * changeSize, dotweenTime);
            OnDown();
        });
        entry1.callback.AddListener(callDown);
        eventTrigger.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerUp;    //抬起按钮
        UnityAction<BaseEventData> callUp = new UnityAction<BaseEventData>(delegate (BaseEventData baseEvent)
        {
            DOTween.Kill(sizeTween);
            sizeTween = transform.DOScale(new Vector3(1, 1, 1), dotweenTime);
        });
        entry2.callback.AddListener(callUp);
        eventTrigger.triggers.Add(entry2);

        EventTrigger.Entry entry5 = new EventTrigger.Entry();
        entry5.eventID = EventTriggerType.PointerClick; //点击按钮
        UnityAction<BaseEventData> callClick = new UnityAction<BaseEventData>(delegate (BaseEventData baseEvent)
        {
            OnClick();
        });
        entry5.callback.AddListener(callClick);
        eventTrigger.triggers.Add(entry5);

        if (highLight != null)
        {
            normal = GetComponent<Image>().sprite;
            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerEnter;
            UnityAction<BaseEventData> callLight = new UnityAction<BaseEventData>(delegate (BaseEventData baseEvent)
            {
                GetComponent<Image>().sprite = highLight;   //高亮状态
            });
            entry3.callback.AddListener(callLight);
            eventTrigger.triggers.Add(entry3);

            EventTrigger.Entry entry4 = new EventTrigger.Entry();
            entry4.eventID = EventTriggerType.PointerExit;
            UnityAction<BaseEventData> callNormal = new UnityAction<BaseEventData>(delegate (BaseEventData baseEvent)
            {
                GetComponent<Image>().sprite = normal;   //普通状态
            });
            entry4.callback.AddListener(callNormal);
            eventTrigger.triggers.Add(entry4);
        }
    }

    //按下按钮
    private void OnDown()
    {

    }

    //点击按钮
    private void OnClick()
    {
        if (audioId == 0)
        {
            return;
        }
        AudioMgr.instance.PlayAudio(audioId);
    }
}