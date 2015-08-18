using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.MiscClass
{
    public class ValidateSN
    {

        public static bool Check(string sn)
        {
            int[,] arr_num = new int[,] {
                {3,9,6,2,7,4,8,1,5,9},
                {5,2,8,0,1,4,7,6,9,3}
            };

            if (sn.Length == 12)
            {
                int val = 0;

                char[] SN = sn.ToCharArray();

                for (int i = 0; i < 11; i++)
                {
                    int asc = (int)SN[i];
                    
                    // if is one of --> ['W','B','C','T','H','D']
                    if (asc >= 65 && asc <= 90)
                    {
                        if (asc == 87 || asc == 66 || asc == 67 || asc == 84 || asc == 72 || asc == 68)
                        {
                            switch (asc)
                            {
                                case 87:
                                    val += arr_num[i % 2, 0];
                                    break;
                                case 66:
                                    val += arr_num[i % 2, 1];
                                    break;
                                case 67:
                                    val += arr_num[i % 2, 2];
                                    break;
                                case 84:
                                    val += arr_num[i % 2, 3];
                                    break;
                                case 72:
                                    val += arr_num[i % 2, 4];
                                    break;
                                case 68:
                                    val += arr_num[i % 2, 5];
                                    break;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (asc >= 48 && asc <= 57)
                    {
                        val += arr_num[i % 2, Convert.ToInt32(SN[i].ToString())];
                    }
                    else
                    {
                        continue;
                    }
                }

                // check sum is match the last digit or not
                if (val % 10 == Convert.ToInt32(SN[11].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
