using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamingUI : MonoBehaviour
{
    public GameObject BuleActionPoint;
    public GameObject RedActionPoint;
    public Sprite ActionPoint;
    public Sprite NoActionPoint;
    public GameObject BuleVictoryPoint1;
    public GameObject BuleVictoryPoint2;
    public GameObject RedVictoryPoint1;
    public GameObject RedVictoryPoint2;
    private void Awake()
    {
        BuleActionPoint.GetComponent<Image>().sprite = ActionPoint;
        RedActionPoint.GetComponent <Image>().sprite = NoActionPoint;
    }
    private void Update()
    {
        IsActionPoint();
        Victory();
    }
    public void IsActionPoint()
    {
        if (GameManager.Instance.TeamRound==Team.bule)
        {
            if (GameManager.Instance.ActionPoint)
            {
                BuleActionPoint.GetComponent<Image>().sprite = ActionPoint;
            }
            else
            {
                BuleActionPoint.GetComponent<Image>().sprite = NoActionPoint;
            }
        }else if (GameManager.Instance.TeamRound==Team.red)
        {
            if (GameManager.Instance.ActionPoint)
            {
               RedActionPoint.GetComponent<Image>().sprite = ActionPoint;
            }
            else
            {
                RedActionPoint.GetComponent<Image>().sprite = NoActionPoint;
            }
        }
    }
    public void Victory()
    {
        switch (GameManager.Instance.BuleVictory)
        {
            case 0:
                BuleVictoryPoint1.SetActive(false);
                BuleVictoryPoint2.SetActive(false);
                break;
            case 1:
                BuleVictoryPoint1.SetActive(true);
                BuleVictoryPoint2.SetActive(false);
                break;
            case 2:
                BuleVictoryPoint1.SetActive(true);
                BuleVictoryPoint2.SetActive(true);
                break;
        }
        switch (GameManager.Instance.RedVictory)
        {
            case 0:
                RedVictoryPoint1.SetActive(false);
                RedVictoryPoint2.SetActive(false);
                break;
            case 1:
                RedVictoryPoint1.SetActive(true);
                RedVictoryPoint2.SetActive(false);
                break;
            case 2:
                RedVictoryPoint1.SetActive(true);
                RedVictoryPoint2.SetActive(true);
                break;
        }
    }
}
