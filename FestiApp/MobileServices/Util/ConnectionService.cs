using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;

namespace FestiMS.Util
{
    public class ConnectionService
    {
        public bool IsConnected()
        {
            if (IsInternetAvailableTest1() && IsInternetAvailableTest2() && CheckForInternetConnection())
            {
                return true;
            }
            return false;
        }
        private static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private bool ConnectedOnline()
        {
            return false;
        }

        private bool IsInternetAvailableTest1()
        {
            try
            {
                bool stats;
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    stats = true;
                }
                else
                {
                    stats = false;
                }
                return stats;
            }
            catch
            {
                return false;
            }
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        private bool IsInternetAvailableTest2()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
    }
}