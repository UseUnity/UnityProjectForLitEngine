using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitEngine;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UnityEngine.Networking;
public class testmono : MonoBehaviour {

    List<object> mList = new List<object>();
    object ttestobj2;
    // Use this for initialization
    unsafe void Awake () {
        
        LogToFile.InitLogCallback();//日志输出
        DLog.LOGColor(DLogType.Error, "testlog", LogColor.YELLO);

        string tapppath = System.IO.Directory.GetCurrentDirectory() + "\\";
        //初始化 APP1
        string tpath = tapppath + "dllproject/testproj/testproj/bin/Release/testproj";
        AppCore.CreatGameCore("MainApp").SManager.LoadProject(tpath);//主app加载dll文件

        //初始化app2
        string tpathapp2 = tapppath+"testapp2/testapp2/bin/Release/testapp2";
        AppCore.CreatGameCore("testapp").SManager.LoadProject(tpathapp2);//其他app创建

        DLog.LOGColor(DLogType.Log, "------------------------testapp-------------------------------", LogColor.YELLO);
        //app2测试
        object tobj = AppCore.App["testapp"].SManager.CodeTool.GetCSLEObjectParmas("TestApp2");
        System.Type ttypedd = tobj.GetType();
     
        DLog.Log(tobj.GetType());
        AppCore.App["testapp"].SManager.CodeTool.CallMethodByName("testCall", tobj);



        //main app测试
        DLog.LOGColor(DLogType.Log, "--------------------------MainApp---------------------------", LogColor.YELLO);
        TestGetILObject();
        TestAddInterface();

        DLog.LOGColor(DLogType.Log, "--------------------------testapp res---------------------------", LogColor.YELLO);
        TestLoadResAndScene();

        //删除app
       // AppCore.DestroyGameCore("testapp");

        //读取写入测试
        int a = 987;
        byte[] tbufftest = LitEngine.NetTool.BufferBase.GetBuffer(a);
        int b = 0;
        LitEngine.NetTool.BufferBase.GetNetValue((byte*)&b, tbufftest, 0, sizeof(int));

        //Http测试
        string testhttpurl = "https://www.baidu.com/";
        LitEngine.NetTool.HttpNet.Send("testapp", testhttpurl, "testkey", testHttpSendCall);


        //下载测试  https://sm.myapp.com/original/game/tencent-SYZS-_1.0.847.123.exe
        //string testurl = "http://dldir1.qq.com/qqfile/qq/TIM2.0.0/22317/TIM2.0.0.exe";
         string testurl = "https://sm.myapp.com/original/game/tencent-SYZS-_1.0.847.123.exe";
        //  LitEngine.DownLoad.DownLoadTask.DownLoadFileAsync(AppCore.App["testapp"].GManager.UpdateList, testurl, Application.dataPath + "/../", false, testdownloadFinished, testdownloadprogress);
        AppCore.App["testapp"].DownLoadFileAsync(testurl, Application.dataPath + "/../", false, testdownloadFinished, testdownloadprogress);
        //PublicUpdateManager.DownLoadFileAsync("testapp", testurl, Application.dataPath + "/../", false, testdownloadFinished, testdownloadprogress);

        //解压缩测试
        //异步任务模式
        // LitEngine.UnZip.UnZipTask.UnZipFileAsync(AppCore.App["testapp"].GManager.UpdateList, Application.dataPath + "/../dd.zip", Application.dataPath + "/../", testunziptask, testunziptaskprogress);

        //同步
        /*  using (LitEngine.UnZipObject tunzipobj = new UnZipObject(Application.dataPath + "/../dd.zip", Application.dataPath + "/../"))
          {
              tunzipobj.StartUnZip();
          }*/


        //异步
        // StartCoroutine(UnzipTest());
    }

    void testHttpSendCall(string _key,byte[] _data)
    {
        DLog.Log(_key + "|" + System.Text.Encoding.UTF8.GetString(_data));
    }

    void testunziptask(string _erro)
    {
        DLog.Log(_erro);
    }

    void testunziptaskprogress(float _progress)
    {
        DLog.Log(_progress);
    }

    IEnumerator UnzipTest()
    {
        LitEngine.UnZip.UnZipObject tunzipobj = new LitEngine.UnZip.UnZipObject(Application.dataPath + "/../dd.zip", Application.dataPath + "/../");
        tunzipobj.StartUnZipAsync();

        while(!tunzipobj.IsDone)
        {
            DLog.Log(tunzipobj.progress);
            yield return tunzipobj;
        }

        DLog.Log(tunzipobj.progress);
        tunzipobj.Dispose();
    }


    void testdownloadFinished(string _path,string _error)
    {
        DLog.Log(_path + "|" + _error);
    }

    void testdownloadprogress(long _downloadlen, long _alllen,float _progress)
    {
        DLog.Log(string.Format("{0:f2}%|{1}/{2}", _progress*100, _downloadlen, _alllen) );
    }

    void TestLoadResAndScene()
    {

        //异步载入测试 通过  //2次异步资源计数加载释放测试
        // AppCore.App["testapp"].LManager.LoadAssetAsync("test", "handpfb.prefab", TestCallBack);
        // AppCore.App["testapp"].LManager.LoadAssetAsync("test", "handpfb.prefab", TestCallBack);

        //2次同步载入释放测试
        Object ttestload = AppCore.App["testapp"].LManager.LoadAsset("handpfb.prefab");
        GameObject.Instantiate(ttestload);
        // Object ttestload2 = AppCore.App["testapp"].LManager.LoadAsset("handpfb.prefab");
        // GameObject.Instantiate(ttestload2);
        //release测试 通过
        // AppCore.App["testapp"].LManager.ReleaseAsset("handpfb.prefab.bytes");
        //AppCore.App["testapp"].LManager.ReleaseAsset("handpfb.prefab.bytes");

        //场景测试
        //同步
        // Object ttestload = AppCore.App["testapp"].LManager.LoadAsset("exscene.unity");
        //  UnityEngine.SceneManagement.SceneManager.LoadScene("exscene");

        // AppCore.App["testapp"].LManager.ReleaseAsset("exscene.unity");
        // 异步
        //  AppCore.App["testapp"].LManager.LoadAssetAsync("exscene", "exscene.unity", TestSceneCallBack);
    }

    void TestSceneCallBack(string _key, object res)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_key);
       // AppCore.App["testapp"].LManager.ReleaseAsset("exscene.unity");
    }

    int testcount = 0;
    void TestCallBack(string _key,object res)
    {
        DLog.Log( _key +"|"+res);
        GameObject.Instantiate((Object)res);

      /*  
        testcount++;
        if(testcount == 2)//2次异步资源计数释放测试
        {
            AppCore.App["testapp"].LManager.ReleaseAsset("handpfb.prefab.bytes");
            AppCore.App["testapp"].LManager.ReleaseAsset("handpfb.prefab.bytes");
        }
        */
    }

    void TestAddInterface()
    {
        //ScriptInterfaceOnEnable 是 接口的其中之一,Onenable代表包含OnEnable和OnDisable,可以再脚本中实现响应.ScriptInterface空间为借口的命名空间.请根据后缀选取合适的接口
        //所有接口都会检测Update,FixedUpdate,LateUpdate,OnGUI函数,上述函数不支持参数.默认有0.1秒的间隔时间.如果需要无间隔,需要调用相关设置 SetUpdateInterval 等
        //接口类拥有 Call系列函数,用于调用脚本内任意函数
        //不推荐函数重载,会有查找问题.反射模式不支持重载.LS模式支持参数数量不同的重载,
        //UseScriptType.UseScriptType_LS模式是用脚本解析 Sys模式是用系统反射.android平台下,Sys模式有效率优势,但不支持il2cpp编译.

       LitEngine.ScriptInterface.ScriptInterfaceOnEnable tinterface = gameObject.AddComponent<LitEngine.ScriptInterface.ScriptInterfaceOnEnable>();
       tinterface.InitScript("TestB", "MainApp");
    }

    void TestGetILObject()
    {
        object tobj = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEObjectParmas("TestA", 2);
        DLog.Log( tobj);
        AppCore.App["MainApp"].SManager.CodeTool.CallMethodByName("log", tobj);
        AppCore.App["MainApp"].SManager.CodeTool.SetTargetMember(tobj, 0, 4);
        AppCore.App["MainApp"].SManager.CodeTool.CallMethodByName("log", tobj);

        object tt = AppCore.App["MainApp"].SManager.CodeTool.GetTargetMemberByIndex(1, tobj);
        DLog.Log( tt);

        AppCore.App["MainApp"].SManager.CodeTool.SetTargetMember(tobj, 1, "更改字符串11111");
        tt = AppCore.App["MainApp"].SManager.CodeTool.GetTargetMemberByIndex(1, tobj);
        DLog.Log( tt);

        var ttype = AppCore.App["MainApp"].SManager.CodeTool.GetLType("TestA");

        System.Action tact = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEDelegate<System.Action>("TestCall", ttype, tobj);
        tact();


        System.Action<float> tact1 = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEDelegate<System.Action<float>, float>("TestCall1", ttype, tobj);
        tact1(99.33f);

        System.Action<float, float> tact2 = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEDelegate<System.Action<float, float>, float, float>("TestCal2",  ttype, tobj);
        tact2(99.33f, 88.1f);

        System.Action<float, float, float> tact3 = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEDelegate<System.Action<float, float, float>, float, float, float>("TestCall3",  ttype, tobj);
        tact3(99.33f, 88.1f, 77.1f);

        System.Action<float, float, float, float> tact4 = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEDelegate<System.Action<float, float, float, float>, float, float, float, float>("TestCall4",  ttype, tobj);
        tact4(99.33f, 88.1f, 77.1f, 66.1f);

        object tproto = AppCore.App["MainApp"].SManager.CodeTool.GetCSLEObjectParmas("TestProto");
        DLog.Log( tproto);
        AppCore.App["MainApp"].SManager.CodeTool.SetTargetMember(tproto, 4, "更改字符串测试 IlRuntime : proto");
        //test proto
        LitEngine.ProtoCSLS.ProtoBufferWriterBuilderCSLS twbuilder = new LitEngine.ProtoCSLS.ProtoBufferWriterBuilderCSLS(AppCore.App["MainApp"].SManager.CodeTool,tproto);
        byte[] tbuff =  twbuilder.GetBuffer();

        object tCSLEObject = LitEngine.ProtoCSLS.ProtoBufferReaderBuilderCSLS.GetCSLEObject(AppCore.App["MainApp"].SManager.CodeTool,tbuff, tbuff.Length, "TestProto");
        DLog.Log( AppCore.App["MainApp"].SManager.CodeTool.GetTargetMemberByIndex(4, tCSLEObject));


        //测试c#类的Proto解析  需要在使用前添加程序集到工具类 LitEngine.NetTool.BufferBase.sCodeCS.AddAssemblyByType(typeof(testCSProto));
        LitEngine.NetTool.BufferBase.sCodeCS.AddAssemblyByType(typeof(testCSProto));
        testCSProto tcsproto = new testCSProto();
        tcsproto.name = "测试C#类的Proto,不支持和ILRuntime混合使用";
        LitEngine.ProtoCSLS.ProtoBufferWriterBuilderCSLS tcswbuilder = new LitEngine.ProtoCSLS.ProtoBufferWriterBuilderCSLS(LitEngine.NetTool.BufferBase.sCodeCS, tcsproto);
        byte[] tbuffcs = tcswbuilder.GetBuffer();

        object tCSObject = LitEngine.ProtoCSLS.ProtoBufferReaderBuilderCSLS.GetCSLEObject(LitEngine.NetTool.BufferBase.sCodeCS, tbuffcs, tbuffcs.Length, "testCSProto");
        object tcsprotoname =  LitEngine.NetTool.BufferBase.sCodeCS.GetTargetMemberByIndex(2, tCSObject);
        DLog.Log(tcsprotoname);
    }

    // Update is called once per frame
    void Update () {
       
     

    }
}

public class testa
{
    public void testcall()
    {
        testb tb = new testb();
        tb.testcall();
    }
}

public class testCSProto
{
    public int index = 2;
    public float time = 3.333f;
    public string name = "test";
}

public class testb
{
    public void testcall()
    {
        testc tc = new testc();
        tc.testcall();
    }
}
public class testc
{
    public void testcall()
    {
        object ttttssss = null;
        ttttssss.GetType();
    }
}