using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public RectTransform rt;
    public Image imgFront;
    public Image imgBack;

    int value = -1;
    CardDirection direction = CardDirection.Back;

    // Update is called once per frame
    void Update()
    {
        // 카드 오브젝트 Y값을 통해 
        // 뒤집힌 상태인지 아닌지 변수에 저장
        if (rt.localEulerAngles.y < 90 || rt.localEulerAngles.y > 270)
        {
            direction = CardDirection.Back;
        }
        else
        {
            direction = CardDirection.Front;
        }

        // 저장된 방향값을 통해 어떤 이미지를 최상단에 보여줄지 정함
        switch (direction)
        {
            case CardDirection.Front:
                imgFront.gameObject.transform.SetAsFirstSibling();
                break;
            case CardDirection.Back:
                imgBack.gameObject.transform.SetAsFirstSibling();
                break;
        }

    }

    public void ResetCard()
    {
        // 초기화
        value = -1;

        rt.DOKill();
        rt.localEulerAngles = new Vector3(0f, 180f, 0f);

    }

    public void SetValue(CardShape shape, int value)
    {
        // 카드의 값이 1,2,4,8 이 아니면 이미지는 그냥 조커로함
        if (value != 1 && value != 2 && value != 4 && value != 8)
        {
            Resources.Load<Image>("Sprites/MiniGame_Card/card_joker");
            return;
        }

        this.value = value;
        this.imgFront.sprite = Resources.Load<Sprite>(string.Format("Sprites/MiniGame_Card/card_{0}_{1}", (int)shape, value));
    }

    public void Shuffle(CardDirection direction, float duration, float delay)
    {
        float addValue = 0;
        if (direction == CardDirection.Front)
            addValue = 180f;
        rt.DOLocalRotate(new Vector3(0f, 360f * 10 + addValue, 0f), duration, RotateMode.LocalAxisAdd).SetDelay(delay);
    }
}
