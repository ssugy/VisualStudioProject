﻿using System.Net;
using System.Net.Sockets;
using System.Text;

/**
 * 채팅서버를 구현하자
 * 1. 1:1 server-client 관계 구현 - 여기 완료
 * 2. 1:N server-client 관계 구현
 * 3. 클라이언트 삭제 시 정상적인 접속 종료 및 리스트 제거
 * 4. 비동기 함수를 활용
 * 5. User클래스 생성해서 작업
 * 5. 채팅서버 구현 (Live코드 -> 예상보다 시간이 너무 오버되심 + 학생들이 따라오기 힘들었다.(화면전환이 너무빠르고, 많았다.))
 *  - 패킷을 이용한 데이터 전송 (1:1)
 *  - 패킷을 이용한 데이터 전송 (1:N)
 *  - 서로 다른 유저들의 정보 교환
 *    -- Handler 값만 공유 (수업종료)
 *    -- 대화를 공유해서 채팅 방 처럼 운용하기 (강사님이 해보라고 이야기하신 내용)
 * 6. P2P - 기존의 내용은 서버-클라이언트에 대한 내용이고, 그것보다 실질적으로 p2p가 더 중요하니까 여기에 집중하자.(의견)
 *  - p2p는 peer to peer (사람 대 사람 연결) 이 내용도 결국에는 네트워크 과목의 내용이라서, 
 *    실질적으로 앞에 내용 모르면 뒤에 내용을 따라가기가 많이 힘들 것 같습니다.
 *  - 강사님은 앞에 설명한 내용을 다 안다는 가정하고 진행하는거라서 아마 더 빨라지지 않을 까 싶어요. 
 *  왜이렇게 빠른가?) 강사님이 라이브 코드를 칠 때의 문제점인데, 버그예측이 힘들다. -> 예상하지 못한 질문답변이나, 구현하는데에 버그가 발생하면 시간이 무한정들어간다.
 *  버그잡을때에는 다른 학생들이 코드를 바라보는 상태에서는 학습이 멈춰요. 그리고 처음 들으시면 아애 집중이 깨지고, 생각이 멈추게 된다. 버그잡을 때 까지. 버그잡아도
 *  다시 집중을 하기는 힘들다. (이러한 문제가 발생)
 *  
 *  결론은 어차피 복습해서 다 알고 넘어가야된다. 안그러면 계속 악순환이 반복된다.
 */
namespace TestGround_Server
{
    internal class Program
    {

        static void Main(string[] args)
        {
            
        }
    }
}