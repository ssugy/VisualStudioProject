// C++_BitField_Example.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <iostream>
using namespace std;
struct DATA {
    unsigned int a : 8;
    unsigned int b : 8;
    unsigned int c : 8;
    unsigned int d : 8;
};

union uUSEDATA
{
    DATA _data;
    unsigned int _uintData;
};

union uDATA
{
    int a;
    unsigned char b;
    long c;
};

int main()
{
    std::cout << "Hello World!\n";
    DATA data;
    std::cout << "DATA의 크기 : " << sizeof(data) <<std::endl;
    printf("DATA의 크기 %d \n",sizeof(data));

    // 유니온
    uDATA datas;
    datas.a = 100;
    datas.b = 200;
    datas.c = 300;
    cout << "datas의 크기 " << datas.a << endl;
    cout << "datas의 크기 " << datas.b << endl;
    cout << "datas의 크기 " << datas.c << endl;    // 마지막 데이터를 메모리상에 덮어서 기록하는 형식. 같은 타입이나 같은 실수형이면 int도 동일한 값이 나온다. 범위만 안넘으면

    // 유니온 예제 2
    uUSEDATA data_u2;
    data_u2._data.a = 1;
    data_u2._data.b = 1;
    data_u2._data.c = 1;
    data_u2._data.d = 1;
    cout << data_u2._uintData << endl;

    //copy
    uUSEDATA _copyData;
    _copyData._uintData = data_u2._uintData;
    cout << _copyData._data.a << endl;
    cout << _copyData._data.b << endl;
    cout << _copyData._data.c << endl;
    cout << _copyData._data.d << endl;  // 이렇게 하면 위에서 선언한 내용을 살려서 사용 할 수 있다.
    
}

// 프로그램 실행: <Ctrl+F5> 또는 [디버그] > [디버깅하지 않고 시작] 메뉴
// 프로그램 디버그: <F5> 키 또는 [디버그] > [디버깅 시작] 메뉴

// 시작을 위한 팁: 
//   1. [솔루션 탐색기] 창을 사용하여 파일을 추가/관리합니다.
//   2. [팀 탐색기] 창을 사용하여 소스 제어에 연결합니다.
//   3. [출력] 창을 사용하여 빌드 출력 및 기타 메시지를 확인합니다.
//   4. [오류 목록] 창을 사용하여 오류를 봅니다.
//   5. [프로젝트] > [새 항목 추가]로 이동하여 새 코드 파일을 만들거나, [프로젝트] > [기존 항목 추가]로 이동하여 기존 코드 파일을 프로젝트에 추가합니다.
//   6. 나중에 이 프로젝트를 다시 열려면 [파일] > [열기] > [프로젝트]로 이동하고 .sln 파일을 선택합니다.
