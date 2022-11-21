using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 이 내용은 IEnumerable에 대한 정확한 이해를 필요로 하며, 인터페이스 중/상급의 활용방법이다.
// 사용자가 임의로 만든 클래스를 foreach문으로 사용하려는 시도이다.
namespace _4._5_CSharp클래스의_확장
{
    class Hardware { }

    class USB
    {
        string name;
        public USB(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }

    class NoteBook2 : Hardware, IEnumerable
    {
        USB[] usbList = new USB[] { new USB("USB1"), new USB("USB2")};
        public IEnumerator GetEnumerator()  // IEnumerator를 구현한 열거자 인스턴스를 반환(클래스 내부 IEnumerator 상속 클래스가 필요)
        {
            return new USBEnumerator(usbList);
        }

        class USBEnumerator : IEnumerator
        {
            int pos = -1; // 위치가 -1부터 시작한다.
            int length = 0;
            object[] list;

            public USBEnumerator(USB[] usb)
            {
                list = usb;
                length = usb.Length;
            }

            public object Current => list[pos]; // 그런데 이러면 최초에 -1위치의 배열로 접근하는게 문제가 있지 않을까?

            public bool MoveNext()  // 다음 순서의 요소를 지정하도록 약속된 메서드
            {
                if (pos >= length - 1) { return false; }    // 현재 pos위치가 배열의 맨 마지막 위치를 가리킨다면 false반환(뒤로 더 못간다는 의미)

                pos++;
                return true; 
            }

            public void Reset()
            {
                pos = -1;   // 처음부터 열거하고 싶으면 -1 위치로 호출
            }
        }
    }

    internal class 인터페이스_IEnumerable
    {
        public static void Main()
        {
            NoteBook2 noteBook= new NoteBook2();
            foreach (USB item in noteBook)
            {
                Console.WriteLine(item);
            }
        }

    }
}
