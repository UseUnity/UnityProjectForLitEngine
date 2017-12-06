using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TestApp2
{
    string teset2appstr = "testapp2测试字符串";
    static TestApp3 teset3 ;
    public TestApp2()
    {
        teset3 = new TestApp3();
    }

    public void testCall()
    {
        DLog.Log(teset2appstr + "--------------");
    }
}

public class TestApp3
{
    string teset2appstr = "testapp3测试字符串";
    public TestApp3()
    {
        testCall();
    }

    public void testCall()
    {
        DLog.Log(teset2appstr + "--------------");
    }
}

