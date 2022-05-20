using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderBoard : MonoBehaviour
{
    public Image[] imgMenu = new Image[8];

    Dictionary<int, string> a = new Dictionary<int, string>()
    {
        {2, "순한맛" }, {3, "매운맛" },
        {0, "라면" }, {1, "우동" },
    };
    Dictionary<int, string> b = new Dictionary<int, string>()
    {
        {4, "햄버거" }, {5, "치킨" },
        {2, "감자튀김" }, {3, "치즈스틱" },
        {0, "콜라" }, {1, "오렌지주스" },
    };
    Dictionary<int, string> c = new Dictionary<int, string>()
    {
        {6, "햄버거" }, {7, "핫도그" },
        {4, "피자" }, {5, "치킨" },
        {2, "타코" }, {3, "랩" },
        {0, "뜨아" }, {1, "아아" },
    };
    Dictionary<int, string> d = new Dictionary<int, string>()
    {
        {6, "4/0" }, {7, "4/1" },
        {4, "3/0" }, {5, "3/1" },
        {2, "2/0" }, {3, "2/1" },
        {0, "1/0" }, {1, "1/1" },
    };

    public string SetMenu(int questionNum, int answer)
    {
        for (int i = 0; i < imgMenu.Length; i++)
            imgMenu[i].gameObject.SetActive(false);

        string answerMent = "";
        List<string> answerStrList = new List<string>();
        switch (questionNum)
        {
            case 0:
                var etorA = a.GetEnumerator();
                while (etorA.MoveNext())
                {
                    imgMenu[etorA.Current.Key].sprite = Resources.Load<Sprite>(string.Format("Sprites/MiniGame_Order/a/{0}_{1}", etorA.Current.Key / 2, etorA.Current.Key % 2));
                    imgMenu[etorA.Current.Key].gameObject.SetActive(true);
                }
                answerStrList = GetAnswerWordList(a, answer);
                answerMent = string.Format("{0}. {1} {2} 을 주문하세요.", questionNum + 1, answerStrList[0], answerStrList[1]);
                break;
            case 1:
                var etorB = b.GetEnumerator();
                while (etorB.MoveNext())
                {
                    imgMenu[etorB.Current.Key].sprite = Resources.Load<Sprite>(string.Format("Sprites/MiniGame_Order/b/{0}_{1}", etorB.Current.Key / 2, etorB.Current.Key % 2));
                    imgMenu[etorB.Current.Key].gameObject.SetActive(true);
                }
                answerStrList = GetAnswerWordList(b, answer);
                answerMent = string.Format("{0}. {1}, {2}, {3} 를 주문하세요.", questionNum + 1, answerStrList[0], answerStrList[1], answerStrList[2]);
                break;
            case 2:
                var etorC = c.GetEnumerator();
                while (etorC.MoveNext())
                {
                    imgMenu[etorC.Current.Key].sprite = Resources.Load<Sprite>(string.Format("Sprites/MiniGame_Order/c/{0}_{1}", etorC.Current.Key / 2, etorC.Current.Key % 2));
                    imgMenu[etorC.Current.Key].gameObject.SetActive(true);
                }
                answerStrList = GetAnswerWordList(c, answer);
                answerMent = string.Format("{0}. {1}, {2}, {3}, {4} 를 주문하세요.", questionNum + 1, answerStrList[0], answerStrList[1], answerStrList[2], answerStrList[3]);
                break;
            case 3:
                var etorD = d.GetEnumerator();
                while (etorD.MoveNext())
                {
                    imgMenu[etorD.Current.Key].sprite = Resources.Load<Sprite>(string.Format("Sprites/MiniGame_Order/d/{0}_{1}", etorD.Current.Key / 2, etorD.Current.Key % 2));
                    imgMenu[etorD.Current.Key].gameObject.SetActive(true);
                }
                answerStrList = GetAnswerWordList(d, answer);
                answerMent = string.Format("{0}. {1}, {2}, {3}, {4} 를 주문하세요.", questionNum + 1, answerStrList[0], answerStrList[1], answerStrList[2], answerStrList[3]);
                break;
        }

        return answerMent;
    }

    List<string> GetAnswerWordList(Dictionary<int, string> dic, int answer)
    {
        List<string> answerStrList = new List<string>();

        // 정답과 나의답이 맞는 자리를 체크
        int tmpAnswer = answer;
        for (int i = dic.Count/2 -1; i >= 0; i--)
        {
            int posValue = (int)Mathf.Pow(2, i);
            int posAnswer = 0;
            if (tmpAnswer >= posValue)
            {
                tmpAnswer -= posValue;
                posAnswer = 1;
            }

            answerStrList.Add(dic[i * 2 + posAnswer]);
        }
        return answerStrList;
    }
}
