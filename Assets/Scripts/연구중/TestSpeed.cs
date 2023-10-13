using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpeed : MonoBehaviour
{
    //여기서 플레이어와 Enemy간의 속도를 계산하여 턴을 잡아줄 스크립트를 만들어 줄 것임.
    //그러면 리스트로 그 정보를 가져와야겠네...?

    //그리고 그 오브젝트들이 가져온 값(스피드)를 비교해야겠네?
    //그러면

    public List<Entity> obj;

    public float TurnSpeed;//기초 행동 수치
    public float baseSpeed;//기초속도
    public float speed;
    public float flatSpeed;
    public float finalSpeed;


    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
