using infrastructure.extensions;
using Microsoft.AspNetCore.DataProtection;

namespace infrastructure.utils
{
    public static class DataProtectionUtil
    {
        private static IDataProtector _dataProtector => ServiceExtension.ServiceProvider.GetDataProtector("Asp.NetCore", "XingChengWuXian", "NiaoWo");
        public static string Protect(string plaintext)
        {
            return _dataProtector.Protect(plaintext);
        }
        public static string UnProtect(string protectedData)
        {
            return _dataProtector.Unprotect(protectedData);
        }
    }
}