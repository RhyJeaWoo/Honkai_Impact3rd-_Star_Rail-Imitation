using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;//�������
    public AudioClip[] bglist;

    public static SoundManager instance;//�̱��� �������� �����Ұ���.

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded; //���� �ε尡 �Ǿ����� ���� �Ŵ����� �Ѿ�鼭 �Ҹ��� ������ �ǰ�.
            //�� ��ũ��Ʈ������ �� �̸��ϰ� �۵� ��ų ��� ������ ���ƾ���. �ֳ��ϸ� �׷��� ­���ϱ�.
            //��Ʋ �ſ����� �������� ������ ����Ѵٰ� �ĵ�, �� �κп��� ��ŭ�� �ϴ� ������ �ʿ��� ����.
        }else
        {
            Destroy(instance);
        }
    }
    

    //�� �׷� ����, ����1 ȿ������ ��� ó���Ұ���???
    //�ϴ� �׳��� �����ΰ�, ���� �Ŵ������� �̱����� �������
    //�� ���� �� �ϴ� ���⼭ �ڵ带 ­���� �װ� Ȱ���� �� �ִٴ� �Ҹ���.

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i< bglist.Length; i++) 
        {
            if (arg0.name == bglist[i].name)
                BgSoundPlay(bglist[i]);
        }
      
    }


    //���� Public AudioClip[] ���� �����ؼ� �ְ� �� �Լ��� ȣ���ϸ� �ɵ���.
    //�������� �����ϳ�
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName+"Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length); //����� ������ �ٷ� �ı�.
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true; //�ݺ� ���
        bgSound.volume = 0.1f;
        bgSound.Play(); //�÷��̾� �Լ� ȣ��
    }



}
