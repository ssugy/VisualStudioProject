using System;
using System.Collections.Generic;

namespace Dot5._0_CodingTestGround
{
    internal class Program
    {
        /**
         * 1. 클래스 상속과 다형성에 대한 기본
         */
        /*
        static void Main(string[] args)
        {
            // 1 다형성 : 하나의 객체가, 여러가지 타입을 가질 수 있다. 객체지향 프로그래밍에서의 중요한 요소 중 하나입니다.
            //Animal ani_01 = new Animal();
            //ani_01.Sound(); // 어떤것이 나올까? "부모"이 나온다.


            // 1-1 이대로 보여주기 : 멍멍
            // 1-2 Dog클래스의 Bark() 주석처리하고 보여주기 : 부모
            //Dog dog = new Dog();
            //dog.Sound();

            // 1-3 다형성을 사용하기 위한 기본형태
            //Animal ani_02 = new Dog();
            //ani_02.Sound();     // 어떤것이 나올까? "멍멍"이 나온다.
            //ani_02.Sound2();    // 이거를 잘봐야됨. virtual-override 사용하는 이유이다.

            // 2. 상위객체는 하위객체를 포함할 수 있지만, 하위객체는 상위객체를 포함 할 수 없다.
            //Dog dog = new Animal();

            //3. 이렇게도 사용 안된다. - 에러나옴
            //Dog dog2 = (Dog)new Animal();
            //dog2.Sound();


            //4. 다형성 사용 예제 - 느슨한 결합과 관련이 있다.
            //Animal dog_03 = new Dog();
            //Animal cat_03 = new Cat();
            //Animal duck_03 = new Duck();
            //cat_03.Sound();

            // 4-1 다형성 사용 예제 : 배열로 사용하기
            //List<Animal> animals = new List<Animal>();
            //animals.Add(dog_03);
            //animals.Add(cat_03);
            //animals.Add(duck_03);

            // 4-2 이렇게 할 때 어떤 결과가 나올지 예상
            //foreach (var animal in animals)
            //{
            //    animal.Sound();
            //}
        }
        */


        /**
         * 2. 추상 클래스의 사용
         */
        /*
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.Sound();

            Cat cat = new Cat();
            cat.Sound();

            Animal animals = new Cat();
            animals.Sound();
        }
        */

        /**
         * 3. Virtual - Override
         */
        /*
        static void Main(string[] args)
        {
            Parent p = new Child();

            Console.WriteLine(p.Some());    // < Parent called (p:그냥, c:그냥)
            Console.WriteLine(p.Other());   // < Parent called (p:virtual, c:그냥)
            Console.WriteLine(p.Another()); // < Child called (p:virtual, c:override) 이거를 해야지 자식함수가 실행됨
        }*/

        /**
         * 4. Delegate
         *  - C언어에서 함수형 포인터의 사용을 말한다.
         *  - 함수를 담을 수 있는 변수의 선언이 딜리게이트이다.
         */
        static void Main(string[] args)
        {
            int a = 10; // 이런식으로 함수를 담아두고 사용하고 싶다.

            //1. delegate가 원하는 그림
            //MyFunc myFunc = MyFunc();  //이런식으로 사용하길 원한다. 그런데 그냥 사용은 안된다.

            // 1-3 딜리게이트 선언. 함수의 형태와 맞췄기 때문에 사용이 가능하다.
            //MyFuncDelegate myFunc;
            //myFunc = MyFunc;
            //myFunc();

            AddDelegate addDelegate;
            addDelegate = Add;
            addDelegate += Multi;
            addDelegate -= Add;

            addDelegate(10, 5);
        }

        public static void MyFunc()
        {
            Console.WriteLine("내 함수 실행");
        }

        // 1-2 딜리게이트 선언
        delegate void MyFuncDelegate();

        // 1-4 딜리게이트 선언2
        public static int Add(int a, int b)
        {
            Console.WriteLine($"a + b : {a+b}");
            return a + b;
        }

        delegate int AddDelegate(int a, int b);

        // 1-5 딜리게이트 추가
        public static int Multi(int a, int b)
        {
            Console.WriteLine($"a * b : {a * b}");
            return a * b;
        }
    }

    
    //////////////////////////// 클래스 선언부
    class Animal
    {
        //울음 메서드를 virtual (가상 메서드)로 선언
        public virtual void Sound()
        {
            Console.WriteLine("이것은 Animal 클래스의 Sound 입니다.");
        }

        public void Sound2()
        {
            Console.WriteLine("이것은 Animal 클래스의 Sound2 입니다.");
        }
    }

    class Dog : Animal
    {
        public override void Sound()
        {
            Console.WriteLine("멍멍");
            //base.Sound();
        }

        public void Sound2()
        {
            Console.WriteLine("왈왈");
        }
    }

    class Cat : Animal
    {
        public override void Sound()
        {
            Console.WriteLine("야옹");
            //base.Sound();
        }
    }

    class Duck : Animal
    {
        public override void Sound()
        {
            Console.WriteLine("꽥꽥꽥");
            //base.Sound();
        }
    }
    

    /**
     * 2. 추상클래스
     *  1) new연산자를 이용해서 인스턴스를 생성 할 수 없습니다.
     *  2) 오직 자식 클래스의 상속을 통해서만 구현 가능
     *  3) 추상 메서드와 추상 프로퍼티를 가질 수 있다.
     */
    /*
    abstract class Animal
    {
        private string name;
        public string name2;

        public abstract void Sound();   // 추상메서드로 선언해야된다. 함수 몸체는 없어야한다.
    }

    class Dog : Animal
    {
        public override void Sound()
        {
            base.name2 = "강아지";
            Console.WriteLine(name2 + " 멍멍");
        }
    }

    class Cat : Animal
    {
        public override void Sound()
        {
            base.name2 = "고양이";
            Console.WriteLine(name2 + " 야옹");
        }
    }
    */

    /**
     * 3. Virtual -Override
     */
    class Parent
    {
        public string Some() => "parent some";
        public virtual string Other() => "parent other";    // virtual 붙임
        public virtual string Another() => "parent another";    // virtual 붙임
    }

    class Child : Parent
    {
        public string Some() => "child some";

        // [Error] 'override' keyword is acceptable for abstract and virtual methods
        //public override string Some() => "child some";

        public string Other() => "child other";

        public override string Another() => "child another";
    }

    /**
     * 4. Delegate 설명
     *  - C언어에서는 함수형 포인터를 말한다.
     *  - 함수를 담을 수 있는 변수의 선언이라고 이해하면 편하다.
     */  
}
