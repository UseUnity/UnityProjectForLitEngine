using UnityEngine;
using LitEngine;
using LitEngine.ScriptInterface;
using System.Collections.Generic;
public class TestA
{
    public int mindex;
    public string m;
    protected List<float> mList;
    public TestA(int _a)
    {
        mindex = _a;
        m = "测试字符串";
        mList = new List<float>();
    }
   
   
    public void log()
    {
        DLog.Log("测试输出函数调用:mindex = " + mindex+"  m = "+m);
    }

    protected void TestCall()
    {
        DLog.Log("TestCall---0");
    }

    protected void TestCall1(float t)
    {
        DLog.Log("TestCall1---" + t);
    }

    
    protected void TestCal2(float t, float t2)
    {
        DLog.Log("TestCal2---" + t+"--"+t2);
    }
    protected void TestCall3(float t, float t2, float t3)
    {
        DLog.Log("TestCal3---" + t + "--" + t2 + "--"+t3);
    }
    protected void TestCall4(float t, float t2, float t3,float t4)
    {
        DLog.Log("TestCal4---" + t + "--" + t2 + "--" + t3 + "--"+t4);
    }
}

public class ttdd
{
    private ttdd()
    {

    }
    public ttdd(int dd)
    {

    }
}
public class ttdd1 : ttdd
{
    ttdd1(int dd):base(dd)
    {

    }
}

public class TestB 
{
    public Transform transform { get; private set; }
    public GameObject gameobject { get; private set; }
    public BehaviourInterfaceBase Parent { get; private set; }
    public TestB(BehaviourInterfaceBase _parent)
    {
        Parent = _parent;
    }
    ~TestB()
    {
       
    }
    public void Awake()
    {
        transform = Parent.transform;
        gameobject = Parent.gameObject;
    }

    protected void Start()
    {
        DLog.Log("Start-"+ transform.name+"-"+ Parent);
        TestA t = new TestA(6);
        t.log();

    }

    protected void  Update()
    {

    }

    protected void OnDisable()
    {
        
    }
    protected void OnEnable()
    {
        DLog.Log("OnEnable");
    }

    protected void OnDestroy()
    {
        DLog.Log("OnDestroy");
    }

}


public class TestProto
{
    int a = 123;
    float b = 3.3f;
    short c = 0xff;
    byte d = 254;
    string e = "测试proto";
    List<float> f;
    List<TestProto1> g;
    TestProto1 h = new TestProto1();
    public TestProto()
    {
        f = new List<float>();
        f.Add(2.3f);
        f.Add(3.3f);
        g = new List<TestProto1>();
        TestProto1 tg1 = new TestProto1();
        tg1.ttt = 2;
        tg1.bttt1 = 4;
        g.Add(tg1);
        TestProto1 tg2 = new TestProto1();
        tg2.ttt = 6;
        tg2.bttt1 = 7;
        g.Add(tg2);
    }
}

public class TestProto1
{
    public int ttt = 33;
    public short bttt1 = 44;

}
