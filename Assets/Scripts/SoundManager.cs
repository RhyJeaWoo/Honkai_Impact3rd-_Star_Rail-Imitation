using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;//배경음악
    public AudioClip[] bglist;

    public static SoundManager instance;//싱글톤 패턴으로 관리할거임.

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded; //씬이 로드가 되었을떄 사운드 매니저가 넘어가면서 소리도 보존이 되게.
            //이 스크립트에서는 씬 이름하고 작동 시킬 배경 음악이 같아야함. 왜냐하면 그렇게 짯으니까.
            //배틀 신에서는 랜덤으로 음악을 재생한다고 쳐도, 이 부분에서 만큼은 일단 조정이 필요해 보임.
        }else
        {
            Destroy(instance);
        }
    }
    

    //자 그럼 이제, 과제1 효과음은 어떻게 처리할거임???
    //일단 그나마 다행인건, 사운드 매니저에는 싱글톤을 사용했음
    //그 말은 난 일단 여기서 코드를 짯으면 그걸 활용할 수 있다는 소리임.

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i< bglist.Length; i++) 
        {
            if (arg0.name == bglist[i].name)
                BgSoundPlay(bglist[i]);
        }
      
    }


    //사용법 Public AudioClip[] 으로 선언해서 넣고 이 함수로 호출하면 될듯함.
    //생각보다 간단하네
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName+"Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length); //재생이 끝나면 바로 파괴.
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true; //반복 재생
        bgSound.volume = 0.1f;
        bgSound.Play(); //플레이어 함수 호출
    }



}
