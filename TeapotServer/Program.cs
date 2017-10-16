using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeapotServer
{
    class Program
    {
        static string notfound = "<h1>NOT FOUND.</h1>Please check response for error. \n TeapotServer 1.0.0";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to TeapotServer.....");
                Console.WriteLine("Files are served from a \"html\" directory relative to the TeapotServer.exe file.");
                Console.WriteLine("Usage: start <ip> <port>");
                var text = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
                System.Net.HttpListener listener = new System.Net.HttpListener();
                listener.Prefixes.Add("http://" + text[1] + ":" + text[2] + "/");
                listener.Start();
                while (true) { 
                var con = listener.GetContext();
                Console.WriteLine(con.Request.RawUrl);
                    switch (con.Request.HttpMethod)
                    {
                        case "GET":
                            try
                            {
                                var bytes = System.IO.File.ReadAllBytes("html/" + con.Request.RawUrl);
                                con.Response.OutputStream.Write(bytes, 0, bytes.Length);
                                con.Response.OutputStream.Flush();
                            }
                            catch (Exception e)
                            {
                                con.Response.OutputStream.Write(System.Text.Encoding.Default.GetBytes(notfound), 0, System.Text.Encoding.Default.GetBytes(notfound).Length);

                            }
                            break;
                    }
                }

            } catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadLine();
            }
            
        }
    }
}
