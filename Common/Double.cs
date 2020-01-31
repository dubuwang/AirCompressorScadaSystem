using System;
using System.Collections.Generic;
using System.Text;


namespace Common
{
    public static class Double
    {
        /// <summary>
        /// 将字节数组中的四个字节转换成float数据，第0个字节为最高位字节数据，第3个字节为最低位字节数据
        /// </summary>
        /// <param name="bResponse"></param>
        /// <returns></returns>
        public static float BytetoFloatByPoint(byte[] bResponse)
        {
         
            //uint nRest = ((uint)response[startByte]) * 256 + ((uint)response[startByte + 1]) + 65536 * ((uint)response[startByte + 2]) * 256 + ((uint)response[startByte + 3]);
            float fValue = 0f;
            uint nRest = ((uint)bResponse[2]) * 256
                + ((uint)bResponse[3]) +
                65536 * (((uint)bResponse[0]) * 256 + ((uint)bResponse[1]));
            //用指针将整形强制转换成float
            unsafe
            {
                float* ptemp;
                ptemp = (float*)(&nRest);
                fValue = *ptemp;
            }
            return fValue;
        }

        #region 将字节数组转换成实型
        //public static double FromByteArray(byte[] bytes)
        //{
        //    byte v1 = bytes[0];
        //    byte v2 = bytes[1];
        //    byte v3 = bytes[2];
        //    byte v4 = bytes[3];

        //    if ((int)v1 + v2 + v3 + v4 == 0)
        //    {
        //        return 0.0;
        //    }
        //    else
        //    {
        //        // nun String bilden
        //        string txt = ValToBinString(v1) + ValToBinString(v2) + ValToBinString(v3) + ValToBinString(v4);
        //        // erstmal das Vorzeichen
        //        int vz = int.Parse(txt.Substring(0, 1));
        //        int exd = Conversion.BinStringToInt(txt.Substring(1, 8));
        //        string ma = txt.Substring(9, 23);
        //        double mantisse = 1;
        //        double faktor = 1.0;

        //        //das ist die Anzahl der restlichen bit's
        //        for (int cnt = 0; cnt <= 22; cnt++)
        //        {
        //            faktor = faktor / 2.0;
        //            //entspricht 2^-y
        //            if (ma.Substring(cnt, 1) == "1")
        //            {
        //                mantisse = mantisse + faktor;
        //            }
        //        }
        //        return Math.Pow((-1), vz) * Math.Pow(2, (exd - 127)) * mantisse;
        //    }
        //}
        #endregion

  
      

  


    }
}
