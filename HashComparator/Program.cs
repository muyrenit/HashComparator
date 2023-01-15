using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;

namespace HashComparator
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                try
                {
                    if (args[0].Length > 0 && args[1].Length > 0)
                    {                       
                        if (GetMD5(args[0]) == GetMD5(args[1]))
                        {
                            Console.WriteLine("hashes are the same ");
                        } else
                        {
                            Console.WriteLine("hashes are not the same!");
                        }                    
                    }
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            } else if (args.Length == 1)
            {
                Console.WriteLine(GetMD5(args[0]));
            } else
            {
                Console.WriteLine("check hashes -> hashcomparator.exe <file|hash|text> <file|hash|text>\nmd5 generate -> hashcomparator.exe <text>");
            }
        }
        static string GetMD5(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                Match result = Regex.Match(text, @"[0-9a-f]{32}", RegexOptions.IgnoreCase);
                if (result.Success)
                {
                    return result.Value;
                }
                else
                {
                    if (File.Exists(text))
                    {
                        return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(File.ReadAllText(text)))).Replace("-", "").ToLower();
                    } else
                    {
                        return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(text))).Replace("-", "").ToLower();
                    }                    
                }                
            }                       
        }
    }
}
