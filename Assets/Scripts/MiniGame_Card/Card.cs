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
        // ī�� ������Ʈ Y���� ���� 
        // ������ �������� �ƴ��� ������ ����
        if (rt.localEulerAngles.y < 90 || rt.localEulerAngles.y > 270)
        {
            direction = CardDirection.Back;
        }
        else
        {
            direction = CardDirection.Front;
        }

        // ����� ���Ⱚ�� ���� � �̹����� �ֻ�ܿ� �������� ����
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
        // �ʱ�ȭ
        value = -1;

        rt.DOKill();
        rt.localEulerAngles = new Vector3(0f, 180f, 0f);

    }

    public void SetValue(CardShape shape, int value)
    {
        // ī���� ���� 1,2,4,8 �� �ƴϸ� �̹����� �׳� ��Ŀ����
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
