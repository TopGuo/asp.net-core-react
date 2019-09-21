using infrastructure.utils;
using Xunit;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var token = "niaowo";
            var key = "xingchenwuxian";
            var userId = "88";
            // var md5Token = SecurityUtil.MD5("88NIAOWOXINGCHENWUXIAN");//B062629BE08F362C9986FAB8627E5C55
            // System.Console.WriteLine($"md5Token={md5Token}");
            
            var sign = SecurityUtil.Sign(token, key, userId);
            System.Console.WriteLine($"sign={sign}");//29BE08F362C9986FAB8627E5


            var valideSign = SecurityUtil.ValidSign(sign, token, key, userId);//sign=29BE08F362C9986FAB8627E5
            System.Console.WriteLine($"valideSign={valideSign}");
        }
    }
}