using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//인터페이스 : 값에 대한 정의만을 가지는 틀
//클래스와 연결해 구현하는 방식으로 사용하며,
//일반 클래스 상속과 다르게 여러 개를 가질 수 있습니다.

//서브젝트 인터페이스 : 옵저버에 대한 관리 , 활용을 위한 형태
public interface Subject
{
    //옵저버 등록 기능
    void Register(Observer _observer);

    //옵저버 해제 기능
    void Remove(Observer _observer);

    //모든 옵저버에 대한 업데이트
    void Notify();
}

//옵저버 인터페이스 (옵저버들이 구현을 해야할 기능)
public interface Observer
{
    //정보의 갱신과 초기화를 진행할 메소드
    void Observer_Update(float player_hp, float enemy_hp);

}


