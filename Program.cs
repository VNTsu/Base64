using System;
using System.Text;

namespace Base64AL
{
    public class Base64Codec
    {
        static public char[] Charset = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '=' };
        static public byte[] Byteset = { 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 43, 47, 61 };
        static public byte Base64CodeOf(byte base64byte)
        {
            if (base64byte == 43)
                return 62;
            else if (base64byte == 47)
                return 63;
            else if (base64byte == 61)
                return 64;
            else if (base64byte <= 57)
                return (byte)(base64byte - 48 + 26 + 26);
            else if (base64byte <= 90)
                return (byte)(base64byte - 65);
            else
                return (byte)(base64byte - 97 + 26);
        }
        static public string Encode(byte[] octetstring)
        {
            Int32 input_group_number = octetstring.Length / 3;
            Int32 last_group_length = octetstring.Length % 3;
            Int32 output_lenght = input_group_number * 4;
            switch (last_group_length)
            {
                case 0: break;
                case 1: output_lenght += (4 + 2); break;
                case 2: output_lenght += (4 + 1); break;
            }
            byte[] outputbytes = new byte[output_lenght];

            Int32 temp24;   
            Int32 temp6;    
            Int32 index_in = 0;
            Int32 index_out = 0;
            for (int i = 0; i < input_group_number; i++)
            {
                temp24 = 0;
                for (int j = 0; j < 3; j++)
                {
                    temp24 <<= 8;
                    temp24 |= octetstring[index_in++];
                }

                for (int j = 0; j < 4; j++)
                {
                    temp6 = temp24 & 0x00FFFFFF;
                    temp6 >>= 18;      
                    temp24 <<= 6;       
                    outputbytes[index_out++] = Byteset[temp6];
                }
            }

            if (last_group_length == 1)
            {
                temp24 = octetstring[index_in++];
                temp24 <<= 16;

                for (int j = 0; j < 4; j++)
                {
                    temp6 = temp24 & 0x00FFFFFF;
                    temp6 >>= 18;      
                    temp24 <<= 6;       
                    outputbytes[index_out++] = Byteset[temp6];
                }

                outputbytes[index_out++] = Byteset[64];
                outputbytes[index_out++] = Byteset[64];
            }
            else if (last_group_length == 2)
            {
                temp24 = octetstring[index_in++];
                temp24 <<= 8;
                temp24 |= octetstring[index_in++];
                temp24 <<= 8;

                for (int j = 0; j < 4; j++)
                {
                    temp6 = temp24 & 0x00FFFFFF;
                    temp6 >>= 18;       
                    temp24 <<= 6;       
                    outputbytes[index_out++] = Byteset[temp6];
                }

                outputbytes[index_out++] = Byteset[64];
            }

            return ASCIIEncoding.ASCII.GetString(outputbytes);
        }
        static void Main(string[] args)
        {
            string text;
            Console.WriteLine("nhap vao text:");
            text = Console.ReadLine();
            var Encoding = Encode(System.Text.Encoding.UTF8.GetBytes(text));
            Console.WriteLine("Sau khi ma hoa:" + Environment.NewLine + Encoding);
        }
    }
}
