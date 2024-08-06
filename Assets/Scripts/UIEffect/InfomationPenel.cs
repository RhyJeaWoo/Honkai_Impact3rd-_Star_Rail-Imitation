using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class InfomationPenel : MonoBehaviour
{
    public Camera mainCamera; // 기존 메인 카메라
    public Camera renderTextureCamera; // 렌더 텍스처용 카메라

    public GameObject uiPanel; // UI 패널

    public Button[] actionButtons; // 캐릭터 정보를 표시할 버튼 배열
    public Image[] ImgactionButtons;//이미지 갱신 버튼 배열

    public Image changeInFoImg;
    public Image hpBarUi;

    public Button closeButton; // 닫기 버튼


    private bool isPanelActive = false; // 패널 활성화 상태 추적
    [SerializeField] private List<PlayerController> playableCharacters;

    public TextMeshProUGUI nameText; // 이름 / LV
    public TextMeshProUGUI healthText; // CurHp / MaxHp
    public TextMeshProUGUI attackText; // 공격력
    public TextMeshProUGUI defendText; // 방어력
    public TextMeshProUGUI energeText; // cureng / maxeng
    public TextMeshProUGUI crtText; // 크리티컬 확률
    public TextMeshProUGUI crtPwText; // 크리티컬 파워
    public TextMeshProUGUI spdPwText; // 속도

    [SerializeField] private Vector3 baseCameraPosition = new Vector3(0.2f, 1.6f, -1.18f);
    [SerializeField] private Vector3 baseCameraRotation = new Vector3(20, -146, 5); // 직렬화된 회전 값

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (renderTextureCamera == null)
        {
            Debug.LogError("Render Texture Camera is not assigned!");
        }
        if (uiPanel == null || actionButtons.Length == 0)
        {
            Debug.LogError("UI Panel is not assigned!");
        }
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseUIPanel); // 닫기 버튼 이벤트 추가
        }
        else
        {
            Debug.LogError("Close button is not assigned!");
        }

    }

    private void Update()
    {
        // 패널이 활성화된 동안 QE나 궁극기 사용 입력 차단
        if (isPanelActive)
        {
            // 입력 차단 로직 (예: QE나 궁극기 사용을 막는 코드)
            return;
        }

        // 여기서 QE나 궁극기 사용 등의 입력을 처리
    }

    //랜더 카메라 위치조정 함수
    private void UpdateRenderTextureCamera(Transform characterTransform)
    {


        // 기본 카메라의 캐릭터와의 상대 위치
        Vector3 relativePosition = baseCameraPosition - new Vector3(0, 0, -2);

        // 선택된 캐릭터의 위치에 맞춰 카메라 위치 조정
        Vector3 newCameraPosition = characterTransform.position + relativePosition;
        renderTextureCamera.transform.position = newCameraPosition;

        // 카메라가 캐릭터를 바라보도록 설정
        renderTextureCamera.transform.LookAt(characterTransform);

        // 카메라 회전 수동 조정 (필요 시)
        Quaternion targetRotation = Quaternion.Euler(baseCameraRotation);
        renderTextureCamera.transform.rotation = targetRotation;

        // 디버깅: 카메라 회전 값 확인
        Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
        Debug.Log("Camera Rotation: " + renderTextureCamera.transform.rotation.eulerAngles);
    }



    // 패널 열기 및 버튼 업데이트
    public void OpenUIPanel(List<PlayerController> players)
    {
        playableCharacters = players; // 플레이어 리스트 저장
        uiPanel.SetActive(true); // 패널 활성화

        if (ImgactionButtons.Length > 0 && ImgactionButtons[0].sprite != null)
        {
            changeInFoImg.sprite = ImgactionButtons[0].sprite;
        }


        UpdateCharacterButtons(); // 버튼 업데이트

        ShowDefaultCharacterInfo();

        // 패널이 열릴 때 입력 차단
        TurnManager.Instance.SetInputEnabled(false); // 입력 비활성화

 
    }

    // 패널 닫기
    public void CloseUIPanel()
    {
        uiPanel.SetActive(false);
        // 패널이 닫힐 때 입력 다시 활성화
        TurnManager.Instance.SetInputEnabled(true); // 입력 활성화

        // PlayerList에서 현재 턴을 가진 플레이어를 찾아서 스킨을 활성화
        foreach (var player in TurnManager.Instance.playable)
        {
            if (player.GetComponent<Entity>().isMyTurn)
            {
                foreach (var skin in player.GetComponent<Entity>().skin)
                {
                    skin.enabled = true;
                }
                break; // 현재 턴을 가진 플레이어가 하나만 있어야 하므로 루프 종료
            }
        }
    }



    // 버튼 업데이트
    private void UpdateCharacterButtons()
    {
        if (ImgactionButtons.Length == 0 || playableCharacters == null)
        {
            Debug.LogWarning("No buttons assigned or no characters provided.");
            return;
        }

        for (int i = 0; i < ImgactionButtons.Length; i++)
        {
            if (i < playableCharacters.Count)
            {
                var player = playableCharacters[i];
                var button = ImgactionButtons[i];
                var entity = player.GetComponent<Entity>();

                if (entity != null && entity.CharSprite != null)
                {
                    button.sprite = entity.CharSprite; // 스프라이트 할당

                    int index = i; // 람다 표현식에서 사용할 변수

                    // 버튼 클릭 시 호출될 메서드를 등록
                    actionButtons[i].onClick.RemoveAllListeners();
                    actionButtons[i].onClick.AddListener(() 
                        => OnCharacterButtonClicked(playableCharacters[index], ImgactionButtons[index]));
                    ImgactionButtons[i].gameObject.SetActive(true); // 버튼 활성화
                }
                else
                {
                    Debug.LogWarning($"Entity or CharSprite is null for player: {player.name}");
                    ImgactionButtons[i].gameObject.SetActive(false); // 버튼 비활성화
                }
            }
            else
            {
                ImgactionButtons[i].gameObject.SetActive(false); // 버튼 비활성화
            }
        }
    }

    private void ShowDefaultCharacterInfo()
    {
        if (playableCharacters.Count > 0 && ImgactionButtons.Length > 0)
        {
            var defaultCharacter = playableCharacters[0]; // 기본 캐릭터 (첫 번째 캐릭터)
            OnCharacterButtonClicked(defaultCharacter, ImgactionButtons[0]); // 버튼 클릭 시 기본 캐릭터 정보 표시

            
        }
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnCharacterButtonClicked(PlayerController character, Image buttonImage)
    {
        if (buttonImage == null)
        {
            Debug.LogError("Button image is null");
            return;
        }

      
        var info = character.GetComponent<Entity>().GetCharacterInfo();
        UpdateCharacterInfoUI(info);
        UpdateRenderTextureCamera(character.transform); // 렌더 카메라 업데이트
        UpdateHpBar(info.curhp, info.maxhp); // 체력바 업데이트



        // 모든 스킨 비활성화
        foreach (var player in playableCharacters)
        {
            foreach (var skin in player.GetComponent<Entity>().skin)
            {
                skin.enabled = false;
            }
        }

        // 선택된 캐릭터의 스킨만 활성화
        foreach (var skin in character.GetComponent<Entity>().skin)
        {
            skin.enabled = true;
        }

        // 버튼 클릭 시 이미지 변경
        changeInFoImg.sprite = buttonImage.sprite; // 버튼 이미지로 변경

    }

    // 체력바 업데이트 메서드
    private void UpdateHpBar(float currentHp, float maxHp)
    {
        if (hpBarUi != null)
        {
            // 체력 비율 계산
            float hpPercentage = currentHp / maxHp;
            // 체력바의 fillAmount를 비율로 설정
            hpBarUi.fillAmount = hpPercentage;
        }
        else
        {
            Debug.LogWarning("hpBarUi is not assigned!");
        }
    }

    // UI 업데이트
    private void UpdateCharacterInfoUI(PlayerStruct info)
    {
        // UI 요소 업데이트
        healthText.text = info.curhp.ToString() + " / " + info.maxhp.ToString();
        nameText.text = info.names + " / LV. " + info.curLevel.ToString();
        defendText.text = info.def.ToString();
        energeText.text = info.cureng.ToString() + " / " + info.maxeng.ToString();
        crtText.text = info.curCrt.ToString()+"%";
        crtPwText.text = (info.criticalPower * 100).ToString() +"%";
        spdPwText.text = info.finalSpeed.ToString();
        attackText.text = info.atk.ToString();
        
    }
}
