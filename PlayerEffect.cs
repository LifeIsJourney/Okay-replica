using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject dotPrefab;
    int dotamount;
    float dotGap;

    GameObject[] dotArray;

    public AnimationCurve speedCurve;

    public float followSpeed;

    TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        dotamount = 10;
        dotGap = 1f / dotamount;
        SpawnDots();
        trail = GetComponentInChildren
            <TrailRenderer>();
    }
   void  SpawnDots()
    {
        dotArray = new GameObject[dotamount];
        for (int i = 0; i < dotamount; i++)
        {
            GameObject obj = Instantiate(dotPrefab);
           
            dotArray[i] = obj;
            obj.SetActive(false);
        }
    }

    public void SetDotPosition(Vector2 startPos, Vector2 endPos)
    {
        for (int i = 0; i < dotamount; i++)
        {
            Vector2 dotPos = dotArray[i].transform.position;

            Vector2 targetPos = Vector2.Lerp(startPos, endPos, i * dotGap);

            float smoothSpeed = (1 - speedCurve.Evaluate(i * dotGap)) * followSpeed;

            dotArray[i].transform.position = Vector2.Lerp(dotPos, targetPos, smoothSpeed * Time.deltaTime);
        }
    }

   public void SetDotState(bool State)
    {
        for (int i = 0; i < dotamount; i++)
        {
            dotArray[i].SetActive(State);
        }
    }

    public void SnapPlayerPos(Vector2 pos)
    {
        Vector2 snapPos = pos;
        for (int i = 0; i < dotamount; i++)
        {
            dotArray[i].transform.position = snapPos;
        }
    }

    public void SetTrailVisible(bool value)
    {
        trail.enabled = value;
    }
}
